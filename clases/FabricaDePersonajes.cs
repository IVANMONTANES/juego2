using EspacioNombres;

namespace EspacioFabricaPersonajes
{

    public static class FabricaPersonajes
    {
        // metodo para le generacion de valores aleatorios //
        public static int AleatorioAaB(int a, int b)
        {
            Random generadorRandom = new Random();
            return generadorRandom.Next(a,b+1);
        }
    
        // metodo para aleatoriamente uno de los 3 tipos //
        public static string ObtenerTipo()
        {
            int opcion = AleatorioAaB(1,3);
            string tipo;
            switch (opcion)
            {
                case 1: tipo = "Vampiro"; break;
                case 2: tipo = "Hombre Lobo"; break;
                case 3: tipo = "Mago"; break;
                default: tipo = null; break;
            }
            return tipo;
        }

        // metodo para obtener aleatoriamente un nombre //
        public static string ObtenerNombre(string tipo)
        {
            string nombre;
            switch (tipo)
            {
                case "Vampiro": nombre = Nombres.Vampiros[AleatorioAaB(0,Nombres.Vampiros.Length)]; break;

                case "Hombre Lobo": nombre = Nombres.HombresLobo[AleatorioAaB(0,Nombres.HombresLobo.Length)]; break;

                case "Mago": nombre = Nombres.Magos[AleatorioAaB(0,Nombres.Magos.Length)]; break;

                default: nombre = null; break;
            }
            return nombre;
        }

        // metodo para obtener aleatoriamente un nombre apodo //
        public static string ObtenerApodo(string tipo)
        {
            string Apodo;
            switch (tipo)
            {
                case "Vampiro": Apodo = Nombres.Vampiros[AleatorioAaB(0,Apodos.Vampiros.Length)]; break;

                case "Hombre Lobo": Apodo = Nombres.HombresLobo[AleatorioAaB(0,Apodos.HombresLobo.Length)]; break;

                case "Mago": Apodo = Nombres.Magos[AleatorioAaB(0,Apodos.Magos.Length)]; break;

                default: Apodo = null; break;
            }
            return Apodo;
        }

        // metodo para generar fechas aleatorios //
        public static DateTime ObtenerFecha(DateTime fechaInicio, DateTime fechaFinal)
        {
            // obtenemos el numero de ticks de cada fecha //
            long TicksFechaInicio = fechaInicio.Ticks;
            long TicksFechaFinal = fechaFinal.Ticks;
            // generamos un numero aleatorio de ticks entre ese rango //
            long Aleatorio = new Random().NextInt64(TicksFechaInicio,TicksFechaFinal);
            // generamos la fecha //
            DateTime fechaGenerada = new DateTime(Aleatorio);
            return fechaGenerada;
        }

    }
}