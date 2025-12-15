using EspacioApi;
using EspacioFabricaPersonajes;
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

        public static void MostrarEnfretamientos(List<Personaje> listaPersonajes,List<int> sorteos)
        {
            int numeroDePelea = 1;
            sorteos = Sorteo.SortearPeleas(listaPersonajes.Count);
            List<Personaje> auxiliar = new List<Personaje>(listaPersonajes);

            while(auxiliar.Count != 0)
            {
                if(auxiliar.Count != 1)
                {
                    Console.WriteLine($"pelea {numeroDePelea}: {auxiliar[0].datos.Nombre} {auxiliar[0].datos.Apodo} vs {auxiliar[1].datos.Nombre} {auxiliar[1].datos.Apodo}");
                    auxiliar.RemoveAt(0);
                    auxiliar.RemoveAt(0);
                    numeroDePelea++;
                }
            }
        }
        public static async Task Menu(string endPoint, string ruta)
        {
            HttpClient client = new HttpClient();
            // variable para determinar si se incluyen los insultos o no segun el exito de la api //
            bool insultos;
            string respuestaString = await Api.VerificarExitoApi(client,endPoint);
            // flag para decidir si puede iniciar el juego o no //
            insultos = !string.IsNullOrEmpty(respuestaString) ? true : false;
            // llamamos a la funcion para manejar la lista de personajes //
            List<Personaje> listaPersonajes = ManejarPersonajes(ruta);
            // mostramos los datos y caracteristicas de los personajes //
            Listas.MostrarListaPersonajes(listaPersonajes);
            Personaje jugador = SeleccionarPersonaje(listaPersonajes);
            MostrarPersonajeElegido(jugador);
            List<int> sorteos = Sorteo.SortearPeleas(listaPersonajes.Count);
            MostrarEnfretamientos(listaPersonajes,sorteos);
        }
    }
}