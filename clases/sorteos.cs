namespace EspacioSorteo
{
    public static class Sorteo
    {
        public static List<int> SortearPeleas(int personajesRestantes)
        {
            Random generadorRandom = new Random();
            List<int> enfrentamientos = new List<int>();
            int numerosCargados = 0;
            int random;
            while(numerosCargados != personajesRestantes)
            {
                random = generadorRandom.Next(0,personajesRestantes);
                if (!enfrentamientos.Contains(random))
                {
                    enfrentamientos.Add(random);
                    numerosCargados++;
                }
            }
            return enfrentamientos;
        }
    }
}