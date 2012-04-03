unit ArchResultadoManager;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, crt,
  DOM, XMLRead, XMLWrite, XMLCfg, XMLUtils, XMLStreaming;


procedure CrearArchivoResultados(pathCompletoArchivo : string);
procedure ModificarEntradaArchivoResultados(pathCompletoArchivo : string; nombreNodo : string; valorNuevo : string);
procedure AgregarErrorArchivoResultados(pathCompletoArchivo : string; tipoError : string; descError : string; linea : integer);
procedure CrearNuevaEntradaParcial(pathCompletoArchivo : string; linea : string);
procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : string; nombre : string; variable : integer );
procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : string; nombre : string; variable : string );
procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : string; nombre : string; variable : boolean );
procedure CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo : string; linea : string; nombre : string; tipo : string; valor : string);
 function CrearNodoVariable(xdoc : TXMLDocument ;nombre : string; tipo : string; valor : string) :  TDOMNode ;

implementation

procedure CrearArchivoResultados(pathCompletoArchivo : string);
var
  NodoContra: TDOMNode;
  Doc:      TXMLDocument;
  xdoc:      TXMLDocument;
  NodoRaiz, NodoPadre, NodoHijo, NodoViejo: TDOMNode;
begin
     try
        xdoc := TXMLDocument.create;
        NodoRaiz := xdoc.CreateElement('archivoResultados');      //crear el nodo raíz
        Xdoc.Appendchild(NodoRaiz);                           // guardar nodo raíz

        NodoRaiz := xdoc.DocumentElement;   //crear el nodo padre
        NodoPadre := xdoc.CreateElement('ejercicio');
        NodoHijo := xdoc.CreateTextNode('null');         // insertar el valor del nodo
        NodoPadre.Appendchild(NodoHijo);
        NodoRaiz.Appendchild(NodoPadre);                          // guardar nodo padre

         NodoPadre := xdoc.CreateElement('resultadoFinal');                // crear el nodo hijo
        //TDOMElement(NodoPadre).SetAttribute('sexo', 'M');     // crear los atributos
        NodoRaiz.AppendChild(NodoPadre);       // insertar el nodo hijo en el correspondiente nodo padre

        NodoPadre := xdoc.CreateElement('entradasParciales');
        NodoRaiz.Appendchild(NodoPadre);

        NodoPadre := xdoc.CreateElement('errores');               // crear el nodo hijo
        //TDOMElement(NodoPadre).SetAttribute('anyo', '1976');   // crear los atributos
        //NodoPadre.Appendchild(NodoHijo);                         // guardar nodo
        NodoRaiz.AppendChild(NodoPadre);       // insertar el nodo hijo en el correspondiente nodo padre

        writeXMLFile(xDoc,pathCompletoArchivo);                     // escribir el XML

     finally
        Xdoc.Free;
     end;



  end;

procedure ModificarEntradaArchivoResultados(pathCompletoArchivo : string; nombreNodo : string; valorNuevo : string);
var
  NodoContra: TDOMNode;
  Doc:      TXMLDocument;
  xdoc:      TXMLDocument;
  NodoRaiz, NodoPadre, NodoHijo, NodoViejo: TDOMNode;
begin

     try
       ReadXMLFile(xDoc, pathCompletoArchivo);

       NodoViejo := xDoc.FindNode('archivoResultados').FindNode(nombreNodo);
       NodoViejo.TextContent:= valorNuevo;

       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;

end;


procedure CrearNuevaEntradaParcial(pathCompletoArchivo : string; linea : string);
var
  NodoContra: TDOMNode;
  Doc:      TXMLDocument;
  xdoc:      TXMLDocument;
  NodoRaiz, NodoPadre, NodoHijo, NodoViejo,NodoEntradasParciales: TDOMNode;
begin

     try
       ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoEntradasParciales := xDoc.FindNode('archivoResultados').FindNode('entradasParciales');
        NodoPadre := xdoc.CreateElement('entrada');
        TDOMElement(NodoPadre).SetAttribute('linea', linea);   // crear el atributo linea
        NodoEntradasParciales.Appendchild(NodoPadre);


       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : string; nombre : string; variable : integer );

begin
        CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo, linea, nombre, 'Integer', IntToStr(variable));
end;

procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : string; nombre : string; variable : string );

begin
        CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo, linea, nombre, 'String', variable);
end;

procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : string; nombre : string; variable : boolean );
var
  aux : string;
begin
        if (variable) then
        begin
           aux := 'True';
        end
        else
        begin
           aux := 'False';
        end;

        CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo, linea, nombre, 'Boolean', aux);
end;

procedure CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo : string; linea : string; nombre : string; tipo : string; valor : string);
var
  NodoContra: TDOMNode;
  Doc:      TXMLDocument;
  xdoc:      TXMLDocument;
  NodoRaiz, NodoPadre, NodoHijo, NodoEntrada,NodoEntradasParciales: TDOMNode;
  ListaNodosHijos : TDOMNodeList;
  i, tope : integer;
  parar : boolean;
  atributos : TDOMNamedNodeMap;
  aux : string;
begin

     try
        ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoEntradasParciales := xDoc.FindNode('archivoResultados').FindNode('entradasParciales');
        if (NodoEntradasParciales.HasChildNodes) then
        begin
                ListaNodosHijos := NodoEntradasParciales.GetChildNodes();
                i := 0;
                parar := false;
                tope := ListaNodosHijos.Count;
                while ((i < tope) and (parar = false)) do
                begin
                     atributos := ListaNodosHijos[i].Attributes;
                     aux := atributos.GetNamedItem('linea').TextContent;
                     parar := aux = linea;
                     if (parar = false) then
                     begin
                          i:= i +1;
                     end;

                end;

                if (i < tope) then
                begin
                     NodoEntrada := ListaNodosHijos[i];
                     NodoPadre := CrearNodoVariable(xdoc, nombre, tipo, valor);
                     NodoEntrada.Appendchild(NodoPadre);
                end;
        end;

       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

function CrearNodoVariable(xdoc : TXMLDocument ;nombre : string; tipo : string; valor : string) :  TDOMNode ;
var
NodoRetorno, NodoValor: TDOMNode;
begin
     NodoRetorno := xdoc.CreateElement('variable');
     TDOMElement(NodoRetorno).SetAttribute('EsArreglo', 'False');
     TDOMElement(NodoRetorno).SetAttribute('Tipo', tipo);
     TDOMElement(NodoRetorno).SetAttribute('Nombre', nombre);
     NodoValor := xdoc.CreateElement('valor');
     NodoValor.TextContent := valor;
     NodoRetorno.Appendchild(NodoValor);
     CrearNodoVariable :=  NodoRetorno;
end;

procedure AgregarErrorArchivoResultados(pathCompletoArchivo : string; tipoError : string; descError : string; linea : integer );
var
  NodoContra: TDOMNode;
  Doc:      TXMLDocument;
  xdoc:      TXMLDocument;
  NodoRaiz, NodoHijo, NodoError, NodoDesc, NodoTipo, NodoLinea, NodoErrores: TDOMNode;
begin

     try

       ReadXMLFile(xDoc, pathCompletoArchivo);

       NodoErrores := xDoc.FindNode('archivoResultados').FindNode('errores');


       NodoError := xdoc.CreateElement('error');
       NodoTipo := xdoc.CreateElement('tipo');
       NodoDesc := xdoc.CreateElement('descripcion');
       NodoLinea := xdoc.CreateElement('linea');

       NodoHijo := xdoc.CreateTextNode(descError);         // insertar el valor del nodo desc
       NodoDesc.Appendchild(NodoHijo);

       NodoHijo := xdoc.CreateTextNode(tipoError);         // insertar el valor del nodo tipo
       NodoTipo.Appendchild(NodoHijo);

       NodoHijo := xdoc.CreateTextNode(IntToStr(linea));         // insertar el valor del nodo linea
       NodoLinea.Appendchild(NodoHijo);

       //Agrego tipo y desc, al nodo error
       NodoError.Appendchild(NodoTipo);
       NodoError.Appendchild(NodoDesc);
       NodoError.Appendchild(NodoLinea);

       //Agrego el error a errores
       NodoErrores.Appendchild(NodoError);


       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

end.

