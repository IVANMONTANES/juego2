using System.Text.Json.Nodes;
using EspacioFabricaPersonajes;
using EspacioInterfaz;
using EspacioJson;
using EspacioListas;
using EspacioPersonajes;
List<Personaje> ListaPersonajes = PersonajesJson.LeerPersonajes("json/listapersonajes");
Listas.MostrarListaPersonajes(ListaPersonajes);
