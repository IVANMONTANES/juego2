namespace EspacioPersonajes
{
    public class Caracteristicas
    {
        public Caracteristicas(int velocidad, int destreza, int fuerza, int nivel, int armadura, int salud)
        {
            Velocidad = velocidad;
            Destreza = destreza;
            Fuerza = fuerza;
            Nivel = nivel;
            Armadura = armadura;
            Salud = salud;
        }

        public int Velocidad {get;set;}
        public int Destreza {get;set;}
        public int Fuerza {get;set;}
        public int Nivel {get;set;}
        public int Armadura {get;set;}
        public int Salud {get;set;}
    }

    public class Datos
    {
        public Datos(string tipo, string nombre, string apodo, DateTime fechaNac, int edad)
        {
            Tipo = tipo;
            Nombre = nombre;
            Apodo = apodo;
            FechaNac = fechaNac;
            Edad = edad;
        }

        public string Tipo {get;set;}
        public string Nombre {get;set;}
        public string Apodo {get;set;}
        public DateTime FechaNac {get;set;}
        public int Edad {get;set;}
    }

    public class Personaje
    {
        public Datos datos {get;set;}
        public Caracteristicas caracteristicas {get;set;}
    }
}