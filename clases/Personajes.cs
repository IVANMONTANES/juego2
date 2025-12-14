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

        // metodos //
        public void MostrarCaracteristicas()
        {
            Console.WriteLine("- CARACTERISTICAS");
            Console.WriteLine($"Velocidad: {this.Velocidad}");
            Console.WriteLine($"Destreza: {this.Destreza}");
            Console.WriteLine($"Fuerza: {this.Fuerza}");
            Console.WriteLine($"Nivel: {this.Nivel}");
            Console.WriteLine($"Armadura: {this.Armadura}");
            Console.WriteLine($"Salud: {this.Salud}");
        }
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

        // metodos //
        public void MostrarDatos()
        {
            Console.WriteLine("- DATOS");
            Console.WriteLine($"Tipo: {this.Tipo}");
            Console.WriteLine($"Fecha Nacimiento: {this.FechaNac.ToString("d MMM yyyy")}");
            Console.WriteLine($"Edad: {this.Edad}\n");
        }
    }

    public class Personaje
    {
        public Personaje(Datos datos, Caracteristicas caracteristicas)
        {
            this.datos = datos;
            this.caracteristicas = caracteristicas;
        }

        public Datos datos {get;set;}
        public Caracteristicas caracteristicas {get;set;}

        // metodos //
        public void MostrarPersonaje()
        {
            Console.WriteLine($"========== {this.datos.Nombre} {this.datos.Apodo} ==========");
            this.datos.MostrarDatos();
            this.caracteristicas.MostrarCaracteristicas();
        }
    }
}