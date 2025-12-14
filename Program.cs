using EspacioInterfaz;
HttpClient client = new HttpClient();
string endPoint = "https://set.world/api/roll/character";
await Interfaz.Menu(client,endPoint);