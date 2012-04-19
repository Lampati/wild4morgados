unit ArchResultadoManager;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, crt,
  DOM, XMLRead, XMLWrite, XMLCfg, XMLUtils, XMLStreaming;


procedure CrearArchivoResultados(pathCompletoArchivo : string);
procedure ModificarEntradaArchivoResultados(pathCompletoArchivo : string; nombreNodo : string; valorNuevo : string);
procedure AgregarErrorArchivoResultados(pathCompletoArchivo : string; tipoError : string; descError : string; linea : integer);
procedure CrearNuevaEntradaParcial(pathCompletoArchivo : string; linea : integer);
procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; variable : real );
procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; variable : string );
procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; variable : boolean );
procedure CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; tipo : string; valor : string);
procedure CrearNuevoArregloEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; arreglo : array of real; topeArr : real);
procedure CrearNuevoArregloEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; arreglo : array of string; topeArr : real);
procedure CrearNuevoArregloEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; arreglo : array of boolean; topeArr : real);

procedure CrearNuevaVariableEnResultado(pathCompletoArchivo : string; nombre : string; contexto : string; variable : real );
procedure CrearNuevaVariableEnResultado(pathCompletoArchivo : string; nombre : string; contexto : string; variable : string );
procedure CrearNuevaVariableEnResultado(pathCompletoArchivo : string; nombre : string; contexto : string; variable : boolean );
procedure CrearNuevaVariableEnResultadoGenerico(pathCompletoArchivo : string; nombre : string; contexto : string;tipo : string; valor : string);
procedure CrearNuevoArregloEnResultado(pathCompletoArchivo : string;  nombre : string; contexto : string; arreglo : array of real; topeArr : real);
procedure CrearNuevoArregloEnResultado(pathCompletoArchivo : string;  nombre : string; contexto : string; arreglo : array of string; topeArr : real);
procedure CrearNuevoArregloEnResultado(pathCompletoArchivo : string;  nombre : string; contexto : string; arreglo : array of boolean; topeArr : real);

 function CrearNodoVariable(xdoc : TXMLDocument ;nombre : string; contexto : string; tipo : string; valor : string) :  TDOMNode ;
 function ObtenerNodoEntradaCorrespondiente( NodoEntradasParciales : TDOMNode ; linea : integer) : TDOMNode;
 function CrearNodoValorArreglo( xdoc : TXMLDocument ;pos : string ; valor : string) : TDOMNode;

 procedure ColocarResultadoIncorrectoEnArch(pathCompletoArchivo : string);
 procedure ColocarResultadoCorrectoEnArch(pathCompletoArchivo : string);

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
         NodoHijo :=   xdoc.CreateElement('variables');
        NodoPadre.AppendChild(NodoHijo);       // insertar el nodo hijo en el correspondiente nodo padre

        NodoHijo :=   xdoc.CreateElement('res');
        NodoPadre.AppendChild(NodoHijo);
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


procedure ColocarResultadoCorrectoEnArch(pathCompletoArchivo : string);
var
  xdoc:      TXMLDocument;
  NodoViejo: TDOMNode;
begin

     try
       ReadXMLFile(xDoc, pathCompletoArchivo);


       NodoViejo := xDoc.FindNode('archivoResultados').FindNode('resultadoFinal').FindNode('res');
       NodoViejo.TextContent:= 'True';

       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;

end;

procedure ColocarResultadoIncorrectoEnArch(pathCompletoArchivo : string);
var
  xdoc:      TXMLDocument;
  NodoViejo: TDOMNode;
begin

     try
       ReadXMLFile(xDoc, pathCompletoArchivo);


       NodoViejo := xDoc.FindNode('archivoResultados').FindNode('resultadoFinal').FindNode('res');
       NodoViejo.TextContent:= 'False';

       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
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


procedure CrearNuevaEntradaParcial(pathCompletoArchivo : string; linea : integer);
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
        TDOMElement(NodoPadre).SetAttribute('linea', IntToStr(linea));   // crear el atributo linea
        NodoEntradasParciales.Appendchild(NodoPadre);


       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; variable : real );

begin
        CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo, linea, nombre, contexto,'Integer', FloatToStr(variable));
end;

procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; variable : string );

begin
        CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo, linea, nombre, contexto,'String', variable);
end;

procedure CrearNuevaVariableEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; variable : boolean );
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

        CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo, linea, nombre, contexto,'Boolean', aux);
end;

procedure CrearNuevaVariableEnEntradaEnLineaGenerico(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; tipo : string; valor : string);
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
        NodoEntrada := ObtenerNodoEntradaCorrespondiente( NodoEntradasParciales, linea);

        if (NodoEntrada <> nil) then
        begin
             NodoPadre := CrearNodoVariable(xdoc, nombre, contexto, tipo, valor);
             NodoEntrada.Appendchild(NodoPadre);
        end;


       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

function CrearNodoVariable(xdoc : TXMLDocument ;nombre : string;contexto : string; tipo : string; valor : string) :  TDOMNode ;
var
NodoRetorno, NodoValor: TDOMNode;
begin
     NodoRetorno := xdoc.CreateElement('variable');
     TDOMElement(NodoRetorno).SetAttribute('EsArreglo', 'False');
     TDOMElement(NodoRetorno).SetAttribute('Tipo', tipo);
     TDOMElement(NodoRetorno).SetAttribute('Nombre', nombre);
     TDOMElement(NodoRetorno).SetAttribute('Contexto', contexto);
     NodoValor := xdoc.CreateElement('valor');
     NodoValor.TextContent := valor;
     NodoRetorno.Appendchild(NodoValor);
     CrearNodoVariable :=  NodoRetorno;
end;


procedure CrearNuevoArregloEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; arreglo : array of real; topeArr : real);
var
  NodoContra: TDOMNode;
  xdoc:      TXMLDocument;
  NodoPadre, NodoHijo, NodoEntrada,NodoEntradasParciales, NodoValor: TDOMNode;
  j : integer;
  aux : string;
begin

     try
        ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoEntradasParciales := xDoc.FindNode('archivoResultados').FindNode('entradasParciales');
        if (NodoEntradasParciales.HasChildNodes) then
        begin

             NodoEntrada := ObtenerNodoEntradaCorrespondiente( NodoEntradasParciales, linea);

             if (NodoEntrada <> nil) then
             begin
                     NodoPadre := xdoc.CreateElement('variable');
                     TDOMElement(NodoPadre).SetAttribute('EsArreglo', 'True');
                     TDOMElement(NodoPadre).SetAttribute('Tipo', 'Integer');
                     TDOMElement(NodoPadre).SetAttribute('Nombre', nombre);
                     TDOMElement(NodoPadre).SetAttribute('Contexto', contexto);
                     TDOMElement(NodoPadre).SetAttribute('Tope', FloatToStr(topeArr) );
                     j := 0;
                     while (j < topeArr)  do
                     begin
                          NodoValor := CrearNodoValorArreglo(xdoc, IntToStr(j+1),FloatToStr(arreglo[j]));
                          NodoPadre.Appendchild(NodoValor);
                          j := j+1;
                     end;

                     NodoEntrada.Appendchild(NodoPadre);
             end;
        end;

       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

procedure CrearNuevoArregloEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; arreglo : array of string; topeArr : real);
var
  NodoContra: TDOMNode;
  xdoc:      TXMLDocument;
  NodoPadre, NodoHijo, NodoEntrada,NodoEntradasParciales, NodoValor: TDOMNode;
  j : integer;
  aux : string;
begin

     try
        ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoEntradasParciales := xDoc.FindNode('archivoResultados').FindNode('entradasParciales');
        if (NodoEntradasParciales.HasChildNodes) then
        begin

             NodoEntrada := ObtenerNodoEntradaCorrespondiente( NodoEntradasParciales, linea);

             if (NodoEntrada <> nil) then
             begin
                     NodoPadre := xdoc.CreateElement('variable');
                     TDOMElement(NodoPadre).SetAttribute('EsArreglo', 'True');
                     TDOMElement(NodoPadre).SetAttribute('Tipo', 'String');
                     TDOMElement(NodoPadre).SetAttribute('Nombre', nombre);
                     TDOMElement(NodoPadre).SetAttribute('Contexto', contexto);
                     TDOMElement(NodoPadre).SetAttribute('Tope', FloatToStr(topeArr));
                     j := 0;
                     while (j < topeArr)  do
                     begin
                          NodoValor := CrearNodoValorArreglo(xdoc, IntToStr(j+1) ,arreglo[j]);
                          NodoPadre.Appendchild(NodoValor);
                          j := j+1;
                     end;

                     NodoEntrada.Appendchild(NodoPadre);
             end;
        end;

       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

procedure CrearNuevoArregloEnEntradaEnLinea(pathCompletoArchivo : string; linea : integer; nombre : string; contexto : string; arreglo : array of boolean; topeArr : real);
var
  NodoContra: TDOMNode;
  xdoc:      TXMLDocument;
  NodoPadre, NodoHijo, NodoEntrada,NodoEntradasParciales, NodoValor: TDOMNode;
  j : integer;
  aux, valorBooleano : string;
begin

     try
        ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoEntradasParciales := xDoc.FindNode('archivoResultados').FindNode('entradasParciales');
        if (NodoEntradasParciales.HasChildNodes) then
        begin

             NodoEntrada := ObtenerNodoEntradaCorrespondiente( NodoEntradasParciales, linea);

             if (NodoEntrada <> nil) then
             begin
                     NodoPadre := xdoc.CreateElement('variable');
                     TDOMElement(NodoPadre).SetAttribute('EsArreglo', 'True');
                     TDOMElement(NodoPadre).SetAttribute('Tipo', 'Boolean');
                     TDOMElement(NodoPadre).SetAttribute('Nombre', nombre);
                     TDOMElement(NodoPadre).SetAttribute('Contexto', contexto);
                     TDOMElement(NodoPadre).SetAttribute('Tope', FloatToStr(topeArr));
                     j := 0;
                     while (j < topeArr)  do
                     begin
                          if (arreglo[j]) then
                          begin
                               valorBooleano := 'True';
                          end
                          else
                          begin
                               valorBooleano := 'False';
                          end;
                          NodoValor := CrearNodoValorArreglo(xdoc, IntToStr(j+1) ,valorBooleano);
                          NodoPadre.Appendchild(NodoValor);
                          j := j+1;
                     end;

                     NodoEntrada.Appendchild(NodoPadre);
             end;
        end;

       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

function CrearNodoValorArreglo( xdoc : TXMLDocument ;pos : string ; valor : string) : TDOMNode;
var
  NodoValor: TDOMNode;
  begin
       NodoValor := xdoc.CreateElement('valor');
       TDOMElement(NodoValor).SetAttribute('posicion', pos);
       NodoValor.TextContent := valor;

       CrearNodoValorArreglo := NodoValor;
  end;

function ObtenerNodoEntradaCorrespondiente( NodoEntradasParciales : TDOMNode ; linea : integer) : TDOMNode;
var
  NodoContra: TDOMNode;
  Doc:      TXMLDocument;
  xdoc:      TXMLDocument;
  NodoRaiz, NodoPadre, NodoHijo, NodoEntrada, NodoValor: TDOMNode;
  ListaNodosHijos : TDOMNodeList;
  i, tope, j : integer;
  parar : boolean;
  atributos : TDOMNamedNodeMap;
  aux : string;
  begin
       ListaNodosHijos := NodoEntradasParciales.GetChildNodes();
       i := 0;
       parar := false;
       tope := ListaNodosHijos.Count;
       while ((i < tope) and (parar = false)) do
       begin
            atributos := ListaNodosHijos[i].Attributes;
            aux := atributos.GetNamedItem('linea').TextContent;
            parar := aux = IntToStr(linea);
            if (parar = false) then
            begin
                 i:= i +1;
            end;
       end;

       if (i < tope) then
       begin
            NodoEntrada := ListaNodosHijos[i];
       end
       else
       begin
            NodoEntrada := nil;
       end;

       ObtenerNodoEntradaCorrespondiente := NodoEntrada;
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
       TDOMElement(NodoError).SetAttribute('linea', IntToStr(linea));
       TDOMElement(NodoError).SetAttribute('tipo', tipoError);
       NodoError.TextContent := descError;

       //Agrego el error a errores
       NodoErrores.Appendchild(NodoError);


       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;


procedure CrearNuevaVariableEnResultado(pathCompletoArchivo : string; nombre : string; contexto : string; variable : real );

begin
        CrearNuevaVariableEnResultadoGenerico(pathCompletoArchivo,  nombre,  contexto,'Real', FloatToStr(variable));
end;

procedure CrearNuevaVariableEnResultado(pathCompletoArchivo : string;  nombre : string; contexto : string; variable : string );

begin
        CrearNuevaVariableEnResultadoGenerico(pathCompletoArchivo,  nombre,  contexto,'String', variable);
end;

procedure CrearNuevaVariableEnResultado(pathCompletoArchivo : string;  nombre : string; contexto : string; variable : boolean );
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

        CrearNuevaVariableEnResultadoGenerico(pathCompletoArchivo,  nombre, contexto, 'Boolean', aux);
end;

procedure CrearNuevaVariableEnResultadoGenerico(pathCompletoArchivo : string; nombre : string; contexto : string; tipo : string; valor : string);
var
  xdoc:      TXMLDocument;
  NodoPadre,  NodoResFinal: TDOMNode;
  ListaNodosHijos : TDOMNodeList;
  i, tope : integer;
  parar : boolean;
  atributos : TDOMNamedNodeMap;
  aux : string;
begin

     try
        ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoResFinal := xDoc.FindNode('archivoResultados').FindNode('resultadoFinal').FindNode('variables');


        NodoPadre := CrearNodoVariable(xdoc, nombre, contexto, tipo, valor);
        NodoResFinal.Appendchild(NodoPadre);



       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

procedure CrearNuevoArregloEnResultado(pathCompletoArchivo : string;  nombre : string; contexto : string; arreglo : array of real; topeArr : real);
var
  NodoContra: TDOMNode;
  xdoc:      TXMLDocument;
  NodoPadre, NodoHijo, NodoEntrada,NodoResFinal, NodoValor: TDOMNode;
  j : integer;
  aux : string;
begin

     try
        ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoResFinal := xDoc.FindNode('archivoResultados').FindNode('resultadoFinal').FindNode('variables');


        NodoPadre := xdoc.CreateElement('variable');
        TDOMElement(NodoPadre).SetAttribute('EsArreglo', 'True');
        TDOMElement(NodoPadre).SetAttribute('Tipo', 'Integer');
        TDOMElement(NodoPadre).SetAttribute('Nombre', nombre);
        TDOMElement(NodoPadre).SetAttribute('Contexto', contexto);
        TDOMElement(NodoPadre).SetAttribute('Tope', FloatToStr(topeArr));
        j := 0;
        while (j < topeArr)  do
        begin
             NodoValor := CrearNodoValorArreglo(xdoc, IntToStr(j+1),FloatToStr(arreglo[j]));
             NodoPadre.Appendchild(NodoValor);
             j := j+1;
        end;

        NodoResFinal.Appendchild(NodoPadre);


       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

procedure CrearNuevoArregloEnResultado(pathCompletoArchivo : string;  nombre : string; contexto : string; arreglo : array of string; topeArr : real);
var
  NodoContra: TDOMNode;
  xdoc:      TXMLDocument;
  NodoPadre, NodoHijo, NodoEntrada,NodoResFinal, NodoValor: TDOMNode;
  j : integer;
  aux : string;
begin

      try
        ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoResFinal := xDoc.FindNode('archivoResultados').FindNode('resultadoFinal').FindNode('variables');


        NodoPadre := xdoc.CreateElement('variable');
        TDOMElement(NodoPadre).SetAttribute('EsArreglo', 'True');
        TDOMElement(NodoPadre).SetAttribute('Tipo', 'String');
        TDOMElement(NodoPadre).SetAttribute('Nombre', nombre);
        TDOMElement(NodoPadre).SetAttribute('Contexto', contexto);
        TDOMElement(NodoPadre).SetAttribute('Tope', FloatToStr(topeArr));
        j := 0;
        while (j < topeArr)  do
        begin
             NodoValor := CrearNodoValorArreglo(xdoc, IntToStr(j+1),arreglo[j]);
             NodoPadre.Appendchild(NodoValor);
             j := j+1;
        end;

        NodoResFinal.Appendchild(NodoPadre);


       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;
end;

procedure CrearNuevoArregloEnResultado(pathCompletoArchivo : string; nombre : string; contexto : string; arreglo : array of boolean; topeArr : real);
var
  NodoContra: TDOMNode;
  xdoc:      TXMLDocument;
  NodoPadre, NodoHijo, NodoEntrada,NodoResFinal, NodoValor: TDOMNode;
  j : integer;
  aux, valorBooleano : string;
begin
     try
        ReadXMLFile(xDoc, pathCompletoArchivo);

        NodoResFinal := xDoc.FindNode('archivoResultados').FindNode('resultadoFinal').FindNode('variables');


        NodoPadre := xdoc.CreateElement('variable');
        TDOMElement(NodoPadre).SetAttribute('EsArreglo', 'True');
        TDOMElement(NodoPadre).SetAttribute('Tipo', 'Boolean');
        TDOMElement(NodoPadre).SetAttribute('Nombre', nombre);
        TDOMElement(NodoPadre).SetAttribute('Contexto', contexto);
        TDOMElement(NodoPadre).SetAttribute('Tope', FloatToStr(topeArr));
        j := 0;
        while (j < topeArr)  do
        begin
             if (arreglo[j]) then
             begin
                  valorBooleano := 'True';
             end
             else
             begin
                  valorBooleano := 'False';
             end;
             NodoValor := CrearNodoValorArreglo(xdoc, IntToStr(j+1),valorBooleano);
             NodoPadre.Appendchild(NodoValor);
             j := j+1;
        end;

        NodoResFinal.Appendchild(NodoPadre);


       writeXMLFile(xDoc,pathCompletoArchivo);
     finally
       Xdoc.free;
     end;


end;

end.

