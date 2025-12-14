using System.Text;
using System.Text.Json;

namespace EspacioApi
{
    public static class Api
    {
        public static async Task<HttpRequestMessage> HacerPeticion(HttpClient client, string endPoint)
        {
            var filtros = new 
            {
                count = 5,
                start =  "1200-01-01",
                end = "1800-01-01",
                format = "Y-m-d"
            };

            string cadenaJson = JsonSerializer.Serialize(filtros);
            var contenido = new StringContent(cadenaJson,Encoding.UTF8,"application/json");
            HttpResponseMessage
        }
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

        
    }
}