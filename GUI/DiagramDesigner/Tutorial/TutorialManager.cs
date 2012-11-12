using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Ragnarok.Tutorial
{
    // flanzani 11/11/2012
    // IDC_APP_5
    // Tutorial para la aplicacion
    // Clase que maneja los tutoriales a lo largo de toda la aplicacion
    public static class TutorialManager
    {
        enum Tutoriales
        {
            AperturaAplicacion,
            ModoGrafico,
            ModoTexto
        }

        static PopUpGlobo popUp;

        static Tutoriales _tutorialActual;
        static int _pasoActual;

        static List<Tutoriales> _listaTutorialesYaHechos = new List<Tutoriales>();

        private static bool activado;
        public static bool Activado 
        {
            get
            {
                return activado;
            }
            set
            {
                if (value)
                {
                    _listaTutorialesYaHechos.Clear();
                }
                else
                {
                    CancelarTutorialActual();
                }

                activado = value;
            }
        }

        static RagnarokWindow _ragnarokWindow;

        public static Point UbicacionVentanaRagnarok { get; set; }

        public static void MostrarTutorialAperturaAplicacion(RagnarokWindow ragnarokWindow)
        {
            _ragnarokWindow = ragnarokWindow;
            _tutorialActual = Tutoriales.AperturaAplicacion;

            ComenzarTutorial();

        }

        public static void MostrarTutorialModoGrafico(RagnarokWindow ragnarokWindow)
        {
            _ragnarokWindow = ragnarokWindow;
            _tutorialActual = Tutoriales.ModoGrafico;
            
            ComenzarTutorial();
        }  

        public static void MostrarTutorialModoTexto(RagnarokWindow ragnarokWindow)
        {
            _ragnarokWindow = ragnarokWindow;
            _tutorialActual = Tutoriales.ModoTexto;

            ComenzarTutorial();
        }

        private static void ComenzarTutorial()
        {
            CancelarTutorialActual();

            if (!_listaTutorialesYaHechos.Contains(_tutorialActual))
            {
                _pasoActual = 1;
                _listaTutorialesYaHechos.Add(_tutorialActual);
                RealizarPaso();
            }
        }

        private static void CancelarTutorialActual()
        {
            if (activado && popUp != null && popUp.IsVisible)
            {
                popUp.Closed -= popUp_Closed;
                popUp.Close();
            }
        }

        private static void RealizarPaso()
        {
            if (Activado)
            {
                bool mostrar = true;
                Point punto;
                switch (_tutorialActual)
                {
                    case Tutoriales.AperturaAplicacion:
                        switch (_pasoActual)
	                    {
                            case 1:
                                popUp = new PopUpGlobo("¡Bienvenido a Ragnarok! El tutorial se encuentra activado, asi que veras distintas indicaciones a lo largo del programa. Puedes cerrar cualquiera de estas indicaciones haciendo click sobre ella. Puedes desactivar este tutorial en cualquier momento yendo a solapa de Configuración y desactivando la funcion de \"Activar Tutorial\".", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.menuAplicacion, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 2:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo("En este espacio podrás acceder a todas las funcionalidades de la aplicacion. Ragnarok agrupa en solapas las funcionalidades similares. Abrir o crear un ejercicio o resolución de ejercicio, hara que más solapas aparezcan disponibles.", true);
                                popUp.ColocarPosicion(new Point(punto.X + 60, punto.Y + 14), UbicacionVentanaRagnarok);
                                break;

                            case 3:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo( "Cada tab contiene distintos componentes que aparecen debajo de su cabecera. Estos componentes tambien estan agrupados acorde a su funcionalidad y son usados para interactuar con el programa. Cada componente tiene una explicación de su funcionalidad que aparece si mantienes tu mouse sobre ella.", true);
                                popUp.ColocarPosicion(new Point(punto.X + 10, punto.Y + 50), UbicacionVentanaRagnarok);
                                break;

                            case 4:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo("Este espacio es el area de trabajo que brinda la aplicación para trabajar con los ejercicios. Si no hay ejercicio abierto, este area se encontrará vacia.", true);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 2, _ragnarokWindow.ActualHeight / 2), UbicacionVentanaRagnarok);
                                break;
                            case 5:
                                 popUp = new PopUpGlobo("¡Haga click aqui en el menu, y seleccione la primer opcion para crear un ejercicio y empezar a programar!",false);
                                 popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.menuAplicacion, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            default:
                                mostrar = false;
                                break;
	                    }
                        break;
                    case Tutoriales.ModoTexto:
                        if (!_ragnarokWindow.ToolbarAplicacion.TabGeneral.IsSelected)
                        {
                            _ragnarokWindow.ToolbarAplicacion.TabGeneral.IsSelected = true;
                        }
                        switch (_pasoActual)
                        {
                            case 1:
                                popUp = new PopUpGlobo("Este es el modo texto para programacion que tiene Ragnarok. Sirve para programar código GarGar desde tu teclado.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonTexto, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 2:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);
                                 
                                popUp = new PopUpGlobo("El espacio de trabajo muestra un editor de texto pensado exclusivamente para el lenguaje GarGar para que puedas escribir tu programa.", true);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 2, _ragnarokWindow.ActualHeight / 2), UbicacionVentanaRagnarok);
                                break;
                            case 3:
                                popUp = new PopUpGlobo("Para compilar el programa que estas escribiendo en modo texto se puede usar este boton, o la tecla F3 del teclado. Los errores de compilacion se muestran en la parte inferior de la pantalla.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonCompilacion, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 4:
                                popUp = new PopUpGlobo("Para ejecutar el programa que estas escribiendo en modo texto se puede usar este boton, o la tecla F4 del teclado.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonEjecucion, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 5:
                                popUp = new PopUpGlobo("En este area se encuentran las funcionalidades basicas de copiar, cortar y pegar texto para el modo texto.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonCopiar, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 6:
                                popUp = new PopUpGlobo("En este area podrás usar la funcionalidad de deshacer y rehacer lo que vayas escrito en el editor de texto.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonRehacer, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 7:
                                popUp = new PopUpGlobo("La función de identar organiza tu texto en el formato sugerido para que GarGar sea mas legible.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonIdentar, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 8:
                                popUp = new PopUpGlobo("¡Es posible transformar todo el codigo que escribas en un diagrama de flujo al hacer click en el boton para cambiar a modo gráfico. Para poder cambiar de modo, el codigo escrito debe compilar correctamente.", false);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonFlujo, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            default:
                                mostrar = false;
                                break;
                        }
                        break;
                    case Tutoriales.ModoGrafico:
                        if (!_ragnarokWindow.ToolbarAplicacion.TabGeneral.IsSelected)
                        {
                            _ragnarokWindow.ToolbarAplicacion.TabGeneral.IsSelected = true;
                        }
                        switch (_pasoActual)
                        {
                            case 1:
                                popUp = new PopUpGlobo("Este es el modo gráfico de programacion que tiene Ragnarok. En este modo, se programa arrastrando figuras al espacio de trabajo.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonFlujo, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 2:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo("El espacio de trabajo muestra un area de trabajo donde existen cajas denominadas \"Secuencia\", en las cuales se programa arrastrando figuras.", true);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 2, _ragnarokWindow.ActualHeight / 1.5), UbicacionVentanaRagnarok);
                                break;
                            case 3:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo("En este area se encuentra la lista de todas las figuras que se pueden arrastrar al area de trabajo. Cada figura representa una instruccion en lenguaje GarGar. Las figuras de mientras y si, admiten que se le arrastren otras figuras dentro de ellas", true);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 10, _ragnarokWindow.ActualHeight / 4), UbicacionVentanaRagnarok);
                                break;
                            case 4:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo("El espacio de trabajo esta separado en distintas solapas, las cuales corresponden a un procedimiento, función, espacio de declaracion de constantes o el espacio de declaracion de variables globales", true);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 4, _ragnarokWindow.ActualHeight / 4.2), UbicacionVentanaRagnarok);
                                break;
                            case 5:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo("Cada solapa puede tener 1 o 2 cajas de secuencia raices para arrastrar figuras. En caso de haber 2, la caja de la izquierda siempre corresponde a una caja de declaraciones. Tanto CONSTANTES como VARIABLES, solo tienen una caja de declaraciones", true);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 3, _ragnarokWindow.ActualHeight / 4.2), UbicacionVentanaRagnarok);
                                break;
                            case 6:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo("Las solapas de PRINCIPAL, SALIDA, VARIABLES y CONSTANTES, no pueden ser eliminadas, ya que son requeridas por el programa", true);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 4, _ragnarokWindow.ActualHeight / 4.2), UbicacionVentanaRagnarok);
                                break;
                            case 7:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);

                                popUp = new PopUpGlobo("Las solapas de PROC + y FUNC +, sirven para agregar un procedimiento o una funcion respectivamente", true);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 2.2, _ragnarokWindow.ActualHeight / 4.2), UbicacionVentanaRagnarok);
                                break;
                            case 8:
                                popUp = new PopUpGlobo("Para compilar el programa que estas diagramando en modo gráfico se puede usar este boton, o la tecla F3 del teclado. Los errores de compilacion se muestran en la parte inferior de la pantalla.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonCompilacion, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 9:
                                popUp = new PopUpGlobo("Para ejecutar el programa que estas  diagramando en modo gráfico se puede usar este boton, o la tecla F4 del teclado.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonEjecucion, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 10:
                                popUp = new PopUpGlobo("En este area se encuentran las funcionalidades basicas de copiar, cortar y pegar texto para las distintas figuras del modo gráfico.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonCopiar, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 11:
                                popUp = new PopUpGlobo("En este area podrás usar la funcionalidad de deshacer y rehacer lo que vayas escrito o diagramado en el modo gráfico.", true);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonRehacer, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 12:
                                popUp = new PopUpGlobo("¡Es posible transformar todo lo que diagramas en codigo GarGar al hacer click en el boton para cambiar a modo texto. Para poder cambiar de modo, el codigo diagramado debe compilar correctamente.", false);
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.ButtonTexto, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            default:
                                mostrar = false;
                                break;
                        }
                        break;
                    default:
                        mostrar = false;
                        break;
                }


                if (mostrar)
                {
                    popUp.Owner = _ragnarokWindow;
                    popUp.Closed += new EventHandler(popUp_Closed);
                    popUp.Show();
                }
            }
        }

        static void popUp_Closed(object sender, EventArgs e)
        {
            _pasoActual++;

            RealizarPaso();
        }

        public static void Reposicionar()
        {
            if (Activado && popUp != null && popUp.IsVisible)
            {
                Point punto;
                switch (_tutorialActual)
                {
                    case Tutoriales.AperturaAplicacion:
                        switch (_pasoActual)
                        {
                            case 1:
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.menuAplicacion, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            case 2:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);
                                popUp.ColocarPosicion(new Point(punto.X + 30, punto.Y), UbicacionVentanaRagnarok);
                                break;

                            case 3:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);
                                popUp.ColocarPosicion(new Point(punto.X + 10, punto.Y + 50), UbicacionVentanaRagnarok);
                                break;

                            case 4:
                                punto = _ragnarokWindow.ToolbarAplicacion.menuAplicacion.TranslatePoint(new Point(0, 0), _ragnarokWindow);
                                popUp.ColocarPosicion(new Point(_ragnarokWindow.ActualWidth / 2, _ragnarokWindow.ActualHeight / 2), UbicacionVentanaRagnarok);
                                break;
                            case 5:
                                popUp.ColocarPosicion(_ragnarokWindow.ToolbarAplicacion.menuAplicacion, _ragnarokWindow, UbicacionVentanaRagnarok);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
