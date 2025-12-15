using System.Text.Encodings.Web;
using System.Text.Json;
using EspacioGanadores;
using EspacioPersonajes;

namespace EspacioHistorialJson
{
    public static class HistorialJson
    {
        public static void GuardarGanador(Personaje ganador, int turnosNecesarios, int curacionesRestantes, string ruta)
        {
            // objeto que vamos a serializar //
            PersonajeGanador personajeGanador = new PersonajeGanador(ganador,turnosNecesarios,curacionesRestantes);

            // serializamos el objeto //
            JsonSerializerOptions opcionesSerializado = new JsonSerializerOptions();
            opcionesSerializado.WriteIndented = true;
            opcionesSerializado.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            string json = JsonSerializer.Serialize(personajeGanador,opcionesSerializado);

            if(Existe(ruta)){
                File.WriteAllText(ruta,json);
            }
            else
            {
                File.WriteAllText(ruta,json);
            }
        }

        public static List<Personaje> ObtenerGanadoresRonda()
        {
            string ruta = "json/ganadoresRonda.json";
            if(Existe(ruta)){
                // leemos el archivo //
                string json = File.ReadAllText(ruta);

                // deserializamos a una lista //
                List<Personaje> ganadoresDeRonda = JsonSerializer.Deserialize<List<Personaje>>(json);

                return ganadoresDeRonda;
            }
            return null;
        }

        public static void BorrarGanadoresRonda()
        {
            string ruta = "json/ganadoresRonda.json";
            if (Existe(ruta))
            {
                File.Delete(ruta);
            }
        }


        public static List<PersonajeGanador> LeerGanadores(string ruta)
        {
            if (Existe(ruta))
            {
                // leemos el archivo //
                string json = File.ReadAllText(ruta);

                // deserializamos a una lista //
                List<PersonajeGanador> ListaGanadores = JsonSerializer.Deserialize<List<PersonajeGanador>>(json);
                return ListaGanadores;
            }
            else
            {
                return null;
            }
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