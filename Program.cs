using EspacioFabricaPersonajes;
using EspacioInterfaz;
for(int i = 0; i < 20; i++)
{
   var fecha =FabricaPersonajes.ObtenerFecha(new DateTime(1025,1,1), new DateTime(1825,1,1)); 
   Console.WriteLine(fecha);
}

