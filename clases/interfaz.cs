using EspacioApi;
using EspacioCombate;
using EspacioFabricaPersonajes;
using EspacioHistorialJson;
using EspacioJson;
using EspacioListas;
using EspacioPersonajes;
using EspacioSorteo;

namespace EspacioInterfaz
{
    public static class Interfaz
    {
        
        public static List<Personaje> ManejarPersonajes(string ruta)
        {
            // verificamos si existe el archivo con los personajes //
            if (PersonajesJson.Existe(ruta))
            {
                return PersonajesJson.LeerPersonajes(ruta);
            }
            else
            {
                // creamos la lista y la guaradmos en un archivo json //
                List<Personaje> listaPersonajes = Listas.GenerarPersonajes(10);
                PersonajesJson.GuardarPersonajes(listaPersonajes,ruta);
                return listaPersonajes;
            }
        }

        public static Personaje SeleccionarPersonaje(List<Personaje> listaPersonajes)
        {
            bool yaSeleccionado = false;
            int seleccionado = default;
            while (!yaSeleccionado)
            {
                Console.WriteLine("seleccione un personaje:");
                string elegidoCadena = Console.ReadLine();
                bool conversionExitosa = int.TryParse(elegidoCadena,out seleccionado);
                if (conversionExitosa && seleccionado > 0 && seleccionado <= listaPersonajes.Count)
                {
                    yaSeleccionado = true;
                }
                else
                {
                    Console.WriteLine("!!! opcion no valida !!!");
                }
            }
            return listaPersonajes[seleccionado-1];
        }

        public static void MostrarPersonajeElegido(Personaje jugador)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("---------- PERSONAJE SELECCIONADO ------------");
            jugador.MostrarPersonaje();
        }

        public static void eliminarDosPrimeros(List<int> sorteos)
        {
            for(int i = 0; i < 2; i++)
            {
                sorteos.RemoveAt(0);
            }
        }
        public static void MostrarEnfretamientos(List<Personaje> listaPersonajes,List<int> sorteos)
        {
            int numeroDePelea = 1;
            List<int> auxiliar = new List<int>(sorteos);

            while(auxiliar.Count != 0 && auxiliar.Count != 1)
            {
                if(auxiliar.Count != 1)
                {
                    Console.WriteLine($"pelea {numeroDePelea}: {listaPersonajes[auxiliar[0]].datos.Nombre} {listaPersonajes[auxiliar[0]].datos.Apodo} vs {listaPersonajes[auxiliar[1]].datos.Nombre} {listaPersonajes[auxiliar[1]].datos.Apodo}");
                    
                    auxiliar.RemoveAt(0);
                    auxiliar.RemoveAt(0);
                    numeroDePelea++;
                }
                if(auxiliar.Count == 1)
                {
                    Console.WriteLine($"{listaPersonajes[auxiliar[0]].datos.Nombre} {listaPersonajes[auxiliar[0]].datos.Apodo} avanza a la siguiente ronda por sorteo");
                }
            }
        }

        public static async Task JugarRonda(List<Personaje> listaPersonajes,List<int> sorteos, Personaje jugador,HttpClient client, string endPoint)
        {
            // mientras la lista con sorteos no esta vacia realizamos las peleas //
            while(sorteos.Count != 0 && sorteos.Count != 1)
            {
                if(listaPersonajes[sorteos[0]] != jugador && listaPersonajes[sorteos[1]] != jugador)
                {
                    await Combate.SimularCombate(listaPersonajes[sorteos[0]],listaPersonajes[sorteos[1]],client,endPoint);
                    // eliminamos la primera pelea //
                    eliminarDosPrimeros(sorteos);
                }
                else
                {
                    if(jugador == listaPersonajes[sorteos[0]])
                    {
                        await Combate.JugarCombate(jugador,listaPersonajes[sorteos[1]],client,endPoint);
                    }
                    else
                    {
                        await Combate.JugarCombate(jugador,listaPersonajes[sorteos[0]],client,endPoint);
                    }
                    eliminarDosPrimeros(sorteos);
                }

                if(sorteos.Count == 1)
                {
                    Console.WriteLine($"{listaPersonajes[sorteos[0]].datos.Nombre} {listaPersonajes[sorteos[0]].datos.Apodo} avanza a la siguiente ronda por sorteo");
                    Combate.GuardarOAgregarGanador(listaPersonajes[sorteos[0]]);
                }
            }
        }
        public static async Task Menu(string endPoint, string ruta)
        {
            HttpClient client = new HttpClient();
            // variable para determinar si se incluyen los insultos o no segun el exito de la api //
            // llamamos a la funcion para manejar la lista de personajes //
            List<Personaje> listaPersonajes = ManejarPersonajes(ruta);
            // mostramos los datos y caracteristicas de los personajes //
            Listas.MostrarListaPersonajes(listaPersonajes);
            Personaje jugador = SeleccionarPersonaje(listaPersonajes);
            MostrarPersonajeElegido(jugador);
            while(listaPersonajes.Count != 1)
            {
                List<int> sorteos = Sorteo.SortearPeleas(listaPersonajes.Count);
                MostrarEnfretamientos(listaPersonajes,sorteos);
                await JugarRonda(listaPersonajes,sorteos,jugador,client,endPoint);
                listaPersonajes = HistorialJson.ObtenerGanadoresRonda();
                // borramos los ganadores de la ronda del json //
                HistorialJson.BorrarGanadoresRonda();
            }

            // borramos de vuelta los gandores para que no influyan en la siguiente partida //
            HistorialJson.BorrarGanadoresRonda();
            
        }
    }
}