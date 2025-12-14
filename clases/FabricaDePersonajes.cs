using EspacioNombres;
using EspacioPersonajes;

namespace EspacioFabricaPersonajes
{

    
    public static class FabricaPersonajes
    {
        private static Random generadorRandom = new Random();

        // metodo para le generacion de valores aleatorios //
        public static int AleatorioAaB(int a, int b)
        {
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
                case "Vampiro": nombre = Nombres.Vampiros[AleatorioAaB(0,Nombres.Vampiros.Length-1)]; break;

                case "Hombre Lobo": nombre = Nombres.HombresLobo[AleatorioAaB(0,Nombres.HombresLobo.Length-1)]; break;

                case "Mago": nombre = Nombres.Magos[AleatorioAaB(0,Nombres.Magos.Length-1)]; break;

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
                case "Vampiro": Apodo = Apodos.Vampiros[AleatorioAaB(0,Apodos.Vampiros.Length-1)]; break;

                case "Hombre Lobo": Apodo = Apodos.HombresLobo[AleatorioAaB(0,Apodos.HombresLobo.Length-1)]; break;

                case "Mago": Apodo = Apodos.Magos[AleatorioAaB(0,Apodos.Magos.Length-1)]; break;

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
            long Aleatorio = generadorRandom.NextInt64(TicksFechaInicio,TicksFechaFinal);
            // generamos la fecha //
            DateTime fechaGenerada = new DateTime(Aleatorio);
            return fechaGenerada;
        }

        public static DateTime ObtenerFechaSegunTipo(string Tipo)
        {
            DateTime fechaGenerada;
            switch (Tipo)
            {
                case "Vampiro":
                    fechaGenerada = ObtenerFecha(new DateTime(1025,1,1), new DateTime(1825,1,1));
                break;

                case "Hombre Lobo":
                    fechaGenerada = ObtenerFecha(new DateTime(1945,1,1),new DateTime(1995,1,1));
                break;

                case "Mago":
                    fechaGenerada = ObtenerFecha(new DateTime(1725,1,1), new DateTime(1905,1,1));
                break;

                default:
                    fechaGenerada = new DateTime(0);
                break;
            }
            return fechaGenerada;
        }

        public static int ObtenerEdad(DateTime fechaNac)
        {
            DateTime fechaActual = DateTime.Now;
            TimeSpan diferencia = fechaActual - fechaNac;
            int edad = (int)(diferencia.TotalDays / 365.25);
            return edad;
        }

        // metodo para generar los personajes //
        public static Personaje CrearPersonaje()
        {
            // caracteristicas //
            int velocidad = AleatorioAaB(1,10);
            int destreza = AleatorioAaB(1,5);
            int fuerza = AleatorioAaB(1,10);
            int nivel = AleatorioAaB(1,10);
            int armadura = AleatorioAaB(1,10);
            int salud = 100;

            // datos //
            string tipo = ObtenerTipo();
            string nombre = ObtenerNombre(tipo);
            string apodo = ObtenerApodo(tipo);
            DateTime FechaNac = ObtenerFechaSegunTipo(tipo);
            int edad = ObtenerEdad(FechaNac);
            
            // creamos las caracterisiticas //
            Caracteristicas caracteristicas = new Caracteristicas(velocidad,destreza,fuerza,nivel,armadura,salud);
            Datos datos = new Datos(tipo,nombre,apodo,FechaNac,edad);

            // creamos el personaje //
            Personaje personajeCreado = new Personaje(datos,caracteristicas);
            return personajeCreado;
        }

    }
}