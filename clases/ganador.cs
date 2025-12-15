using EspacioPersonajes;

namespace EspacioGanadores
{
    public class PersonajeGanador
    {
        public PersonajeGanador(Personaje ganador, int turnosNecesarios, int curacionesRestantes)
        {
            Ganador = ganador;
            TurnosNecesarios = turnosNecesarios;
            CuracionesRestantes = curacionesRestantes;
        }

        public Personaje Ganador {get;set;}
        public int TurnosNecesarios {get;set;}
        public int CuracionesRestantes {get;set;}

    }
}