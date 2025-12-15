using EspacioCombate;
using EspacioFabricaPersonajes;
using EspacioInterfaz;
using EspacioPersonajes;
using EspacioSorteo;
string endPoint ="https://evilinsult.com/generate_insult.php?lang=es&type=json";
string ruta = "json/listaPersonajes.json";
/*
await Interfaz.Menu(endPoint,ruta);
*/
Personaje personaje1 = FabricaPersonajes.CrearPersonaje();
Personaje personaje2 = FabricaPersonajes.CrearPersonaje();
Combate.JugarCombate(personaje1,personaje2);