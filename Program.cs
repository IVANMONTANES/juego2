using EspacioCombate;
using EspacioFabricaPersonajes;
using EspacioInterfaz;
using EspacioPersonajes;
using EspacioSorteo;
string endPoint ="https://quotes-api-three.vercel.app/api/randomquote?language=es";
string ruta = "json/listaPersonajes.json";
await Interfaz.Menu(endPoint,ruta);
