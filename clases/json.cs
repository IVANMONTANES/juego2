using System.Text.Encodings.Web;
using System.Text.Json;
using EspacioPersonajes;

namespace EspacioJson
{
    public static class PersonajesJson
    {
        public static void GuardarPersonajes(List<Personaje> listaPersonajes, string ruta)
        {
            // serializamos la lista //
            JsonSerializerOptions opcionesSerializado = new JsonSerializerOptions();
            opcionesSerializado.WriteIndented = true;
            opcionesSerializado.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            string json = JsonSerializer.Serialize(listaPersonajes,opcionesSerializado);

            // verificamos que exista el archivo si no existe lo creamos //
            if (Existe(ruta))
            {
                File.WriteAllText(ruta,json);
            }
            else
            {
                File.WriteAllText(ruta,json);
            }
        }

        public static List<Personaje> LeerPersonajes(string ruta)
        {
            // verificamos si el archivo existe //
            if (Existe(ruta))
            {
                string contenido = File.ReadAllText(ruta);
                // deserializamos la cadena //
                List<Personaje> listaPersonajes = JsonSerializer.Deserialize<List<Personaje>>(contenido);
                return listaPersonajes;
            }
            return null;
        }

        public static bool Existe(string ruta)
        {
            if (File.Exists(ruta))
            {
                string contenido = File.ReadAllText(ruta);
                if (!string.IsNullOrEmpty(contenido))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}