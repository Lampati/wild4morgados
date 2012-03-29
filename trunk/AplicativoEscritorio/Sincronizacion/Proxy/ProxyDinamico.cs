using System;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.Xml.Serialization;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Sincronizacion.Proxy
{
    /// <summary>
    /// Crea un Proxy para WS en runtime. Esto se hace para no tener que agregar la WebReference de VisualStudio que algunas
    /// veces, cuando se implementa en otro ambiente, quedan viejas y trae algunos problemas. Basicamente, consulta al WSDL
    /// y arma un Assembly con los métodos que expone el WS.
    /// </summary>
    public class ProxyDinamico
    {
        private Type service;
        private MethodInfo[] metodos;
        private string urlWsdl;

        private string usuario;
        private string password;
        private string dominio;

        private string urlProxy;
        private int puertoProxy;
        private bool bypassLocal;
        private string usuarioProxy;
        private string passwordProxy;
        private string dominioProxy;

        private bool seCargoCredenciales;
        private bool seCargoProxy;
        private bool seCargoCredencialesProxy;

        public ProxyDinamico(string urlWsdl)
        {
            this.urlWsdl = urlWsdl;

            this.seCargoCredenciales = false;
            this.seCargoCredencialesProxy = false;
            this.seCargoProxy = false;

            this.CargarMetodos();
        }

        public ProxyDinamico(string urlWsdl, string usuario, string password, string dominio)
        {
            this.urlWsdl = urlWsdl;
            this.usuario = usuario;
            this.password = password;
            this.dominio = dominio;

            this.seCargoCredenciales = true;
            this.seCargoCredencialesProxy = false;
            this.seCargoProxy = false;

            this.CargarMetodos();
        }

        public ProxyDinamico(string urlWsdl, string usuario, string password, string dominio,
            string urlProxy, int puertoProxy, bool bypassLocal)
        {
            this.urlWsdl = urlWsdl;
            this.usuario = usuario;
            this.password = password;
            this.dominio = dominio;
            this.urlProxy = urlProxy;
            this.puertoProxy = puertoProxy;
            this.bypassLocal = bypassLocal;

            this.seCargoCredenciales = true;
            this.seCargoProxy = true;
            this.seCargoCredencialesProxy = false;

            this.CargarMetodos();
        }

        public ProxyDinamico(string urlWsdl, string usuario, string password, string dominio,
            string urlProxy, int puertoProxy, bool bypassLocal, string usuarioProxy, string passwordProxy,
            string dominioProxy)
        {
            this.urlWsdl = urlWsdl;
            this.usuario = usuario;
            this.password = password;
            this.dominio = dominio;
            this.urlProxy = urlProxy;
            this.puertoProxy = puertoProxy;
            this.bypassLocal = bypassLocal;
            this.usuarioProxy = usuarioProxy;
            this.passwordProxy = passwordProxy;
            this.dominioProxy = dominioProxy;

            this.seCargoCredenciales = true;
            this.seCargoCredencialesProxy = true;
            this.seCargoProxy = true;

            this.CargarMetodos();
        }

        private void CargarMetodos()
        {
            if (!this.urlWsdl.ToLower().EndsWith("?wsdl"))
                this.urlWsdl = this.urlWsdl + "?wsdl";

            Uri uri = new Uri(this.urlWsdl);
            this.metodos = new MethodInfo[100];

            WebRequest webRequest = WebRequest.Create(uri);

            if (this.seCargoCredenciales)
                webRequest.Credentials = new NetworkCredential(this.usuario, this.password, this.dominio);

            if (this.seCargoProxy)
            {
                WebProxy webProxy = new WebProxy(this.urlProxy, this.puertoProxy);
                webProxy.BypassProxyOnLocal = this.bypassLocal;

                if (this.seCargoCredencialesProxy)
                    webProxy.Credentials = new NetworkCredential(this.usuarioProxy, this.passwordProxy, this.dominioProxy);
                else
                    webProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

                webRequest.Proxy = webProxy;
            }

            using (Stream requestStream = webRequest.GetResponse().GetResponseStream())
            {
                ServiceDescription sd = ServiceDescription.Read(requestStream);
                string sdName = sd.Services[0].Name;

                // Initialize a service description servImport
                ServiceDescriptionImporter servImport = new ServiceDescriptionImporter();
                servImport.AddServiceDescription(sd, String.Empty, String.Empty);
                servImport.ProtocolName = "Soap";
                /*servImport.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;*/

                CodeNamespace nameSpace = new CodeNamespace();
                CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
                codeCompileUnit.Namespaces.Add(nameSpace);
                // Set Warnings
                ServiceDescriptionImportWarnings warnings = servImport.Import(nameSpace, codeCompileUnit);

                if (warnings == 0)
                {
                    using (StringWriter stringWriter = new StringWriter(System.Globalization.CultureInfo.CurrentCulture))
                    {
                        using (Microsoft.CSharp.CSharpCodeProvider prov = new Microsoft.CSharp.CSharpCodeProvider())
                        {
                            prov.CreateGenerator().GenerateCodeFromNamespace(nameSpace, stringWriter, new CodeGeneratorOptions());

                            string[] assemblyReferences = new string[3] { "System.Web.Services.dll", "System.Xml.dll", "System.Data.dll" };
                            CompilerParameters param = new CompilerParameters(assemblyReferences);
                            param.GenerateExecutable = false;
                            param.GenerateInMemory = true;
                            param.TreatWarningsAsErrors = false;
                            param.WarningLevel = 4;

                            CompilerResults results = new CompilerResults(new TempFileCollection());
                            results = prov.CreateCompiler().CompileAssemblyFromDom(param, codeCompileUnit);

                            if (results.Errors.Count > 0)
                            {
                                StringBuilder sb = new StringBuilder();
                                foreach (CompilerError err in results.Errors)
                                    sb.Append(err.ErrorText);

                                throw new Exception(sb.ToString());
                            }

                            Assembly assembly = results.CompiledAssembly;
                            service = assembly.GetType(sdName);

                            int i = 0;
                            foreach (MethodInfo t in service.GetMethods())
                            {
                                if (t.Name == "Discover")
                                    break;

                                metodos.SetValue(t, i);
                                i++;
                            }
                        }
                    }
                }
                else
                    throw new Exception("El ServiceDescriptionImportWarnings devolvio " + warnings.ToString());
            }
        }

        internal string[] Metodos
        {
            get
            {
                System.Collections.ArrayList metodos = new System.Collections.ArrayList();
                foreach (MethodInfo me in this.metodos)
                    metodos.Add(me.Name);

                return (string[])metodos.ToArray(typeof(string));
            }
        }

        public object InvocarMetodo(string nombreMetodo)
        {
            return this.InvocarMetodo(nombreMetodo, null);
        }

        public object InvocarMetodo(string nombreMetodo, object[] parametros)
        {
            Object obj = Activator.CreateInstance(service);
            System.Web.Services.Protocols.HttpWebClientProtocol cliente = (System.Web.Services.Protocols.HttpWebClientProtocol)obj;

            if (this.seCargoCredenciales)
                cliente.Credentials = new NetworkCredential(this.usuario, this.password, this.dominio);

            if (this.seCargoProxy)
            {
                WebProxy webProxy = new WebProxy(this.urlProxy, this.puertoProxy);
                webProxy.BypassProxyOnLocal = this.bypassLocal;

                if (this.seCargoCredencialesProxy)
                    webProxy.Credentials = new NetworkCredential(this.usuarioProxy, this.passwordProxy, this.dominioProxy);
                else
                    webProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

                cliente.Proxy = webProxy;
            }

            foreach (MethodInfo m in this.metodos)
            {
                if (m.Name.Equals(nombreMetodo))
                {
                    Object response = m.Invoke(cliente, parametros);
                    return response;
                }
            }

            return null;
        }
    }

}

