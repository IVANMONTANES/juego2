using System.Text;
using System.Text.Json;
using ClasesApis;

namespace EspacioApi
{
    public static class Api
    {
        
        public static async Task<HttpResponseMessage> ObtenerRespuesta(HttpClient client, string endPoint)
        {

            HttpResponseMessage respuesta;
            try
            {
                respuesta = await client.GetAsync(endPoint);
                return respuesta;
            }catch(InvalidOperationException ex)
            {
                Console.WriteLine($"Debe indicar una url absoluta");
                Console.WriteLine($"Mas informacion: {ex.Message}");
                return null;       
            }catch(HttpRequestException ex)
            {
                Console.WriteLine("hubo un error en la peticion");
                Console.WriteLine($"Mas informacion: {ex.Message}");
            }catch(TaskCanceledException ex)
            {
                Console.WriteLine("se excedio el tiempó de espera");
                Console.WriteLine($"Mas informacion: {ex.Message}");
            }catch(Exception ex)
            {
                Console.WriteLine("ocurrio un error inesperado");
                Console.WriteLine($"Mas informacion: {ex.Message}");
            }

            return null;
        }

        public static async Task<string> ManejarRespuesta(HttpResponseMessage respuesta)
        {
            if(respuesta != null)
            {
                try
                {
                    respuesta.EnsureSuccessStatusCode();
                    string respuestaString = await respuesta.Content.ReadAsStringAsync();
                    return respuestaString;
                }catch(HttpRequestException excepcion)
                {
                    Console.WriteLine("ocurrio un error");
                    Console.WriteLine(excepcion.Message);
                    return null;
                }
            }
            return null;
        }

        public static async Task<string> VerificarExitoApi(HttpClient client, string endPoint)
        {

            HttpResponseMessage respuesta = await ObtenerRespuesta(client,endPoint);
            string respuestaString = await ManejarRespuesta(respuesta);
            return respuestaString;
        }

        public static Frase ObtenerFrase(string respuestaString)
        {
            Frase frase = JsonSerializer.Deserialize<Frase>(respuestaString);
            return frase;
        }

        
    }
}