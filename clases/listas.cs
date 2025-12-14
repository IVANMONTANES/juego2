using EspacioFabricaPersonajes;
using EspacioPersonajes;

namespace EspacioListas
{
    public static class Listas
    {
        public static bool generarOtroPersonaje(List<Personaje> listaPersonajes, string Nombre)
        {

            if(listaPersonajes.Count != 0)
            {
               for(int i = 0; i < listaPersonajes.Count; i++)
                {
                    if(listaPersonajes[i].datos.Nombre == Nombre)
                    {
                        return true;
                    }
                } 
            }
            
            return false;
        }
        public static List<Personaje> GenerarPersonajes(int numeroPersonajes)
        {
            Personaje personaje = null;
            bool generarOtro;
            List<Personaje> ListaPersonajes = new List<Personaje>();
            // generamos los personajes //
            for(int i = 0; i < numeroPersonajes; i++)
            {
                personaje = FabricaPersonajes.CrearPersonaje();
                generarOtro = generarOtroPersonaje(ListaPersonajes,personaje.datos.Nombre);
                if (generarOtro)
                {
                    i--;
                }
                else
                {
                    ListaPersonajes.Add(personaje);
                }
            }

            return ListaPersonajes;
        }

        public static void MostrarListaPersonajes(List<Personaje> listaPersonajes)
        {
            for(int i = 0; i < listaPersonajes.Count; i++)
            {
                Console.WriteLine($"{i+1}: {listaPersonajes[i].datos.Nombre} {listaPersonajes[i].datos.Apodo}");
            }
        }
    }
}