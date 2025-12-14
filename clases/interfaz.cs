using EspacioApi;

namespace EspacioInterfaz
{
    public static class Interfaz
    {
        

        public static async Task Menu(HttpClient client, string endPoint)
        {
            string respuestaString = await Api.VerificarExitoApi(client,endPoint);
            // flag para decidir si puede iniciar el juego o no //
            bool ApiLeidaCorrectamente = !string.IsNullOrEmpty(respuestaString) ? true : false;
            if (ApiLeidaCorrectamente)
            {
                Console.WriteLine("la api se leyo correctamente");
            }
            else
            {
                Console.WriteLine("no se obtuvo nada de la api");
            }
        }
    }
}