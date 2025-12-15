using EspacioFabricaPersonajes;
using EspacioPersonajes;

namespace EspacioCombate
{
    public static class Combate
    {
        public static int Ataque(Personaje atacante)
        {
            return atacante.caracteristicas.Destreza * atacante.caracteristicas.Fuerza * atacante.caracteristicas.Nivel;
        }

        public static int Defensa(Personaje defensor)
        {
            return defensor.caracteristicas.Armadura * defensor.caracteristicas.Velocidad;
        }

        public static int CalcularDanio(Personaje atacante, Personaje defensor)
        {
            int ataque = Ataque(atacante);
            int defensa = Defensa(defensor);
            int danio = (ataque * FabricaPersonajes.AleatorioAaB(1,100) - defensa)/500;
            return danio;
        }

        public static void MostrarDatosRelevantes(Personaje personaje, int curacionesDisponibles)
        {
            Console.WriteLine($"{personaje.datos.Nombre} {personaje.datos.Apodo} -> vida restante: {personaje.caracteristicas.Salud}");
            Console.WriteLine($"Curaciones Disponibles {curacionesDisponibles}");
            Console.WriteLine("---------------------------------");
        }

        public static Personaje DecidirQuienArranca(Personaje personaje1, Personaje personaje2)
        {
            int turno = FabricaPersonajes.AleatorioAaB(0,1);
            // si sale 0 arranca personaje1, si sale 1 arranca personaje2 //
            Personaje atacante = turno == 0 ? personaje1 : personaje2;
            return atacante;
        }

        public static Personaje DecidirDefensor(Personaje personaje1, Personaje personaje2, Personaje atacante)
        {
            Personaje Defensor = atacante == personaje1 ? personaje2 : personaje1;
            return Defensor;
        }

        public static int DecidirMovimiento(Personaje atacante, int curacionesDisponiblesPersonaje1, int curacionesDisponiblesPersonaje2,Personaje personaje1, Personaje personaje2)
        {
            bool puedeCurarse1 = curacionesDisponiblesPersonaje1 > 0 ? true: false;
            bool puedeCurarse2 = curacionesDisponiblesPersonaje2 > 0 ? true: false;
            // 0 -> atacar || 1 -> curarse (si se puede) //
            int aleatorio = FabricaPersonajes.AleatorioAaB(1,100);
            if(atacante == personaje1)
            {
                if(aleatorio <= 78){
                    return 0;
                }else if(aleatorio > 78 && atacante.caracteristicas.Salud >= 100)
                {
                    return 0;
                }
                else if(aleatorio > 78 && atacante.caracteristicas.Salud < 100 && !puedeCurarse1)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if(aleatorio <= 78){
                    return 0;
                }else if(aleatorio > 78 && atacante.caracteristicas.Salud >= 100)
                {
                    return 0;
                }
                else if(aleatorio > 78 && atacante.caracteristicas.Salud < 100 && !puedeCurarse2)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public static void DescontarCuraciones(Personaje atacante,Personaje personaje1, Personaje personaje2, ref int curacionesDisponiblesPersonaje1, ref int curacionesDisponiblesPersonaje2)
        {
            if(atacante == personaje1)
            {
                curacionesDisponiblesPersonaje1--;
            }
            else
            {
                curacionesDisponiblesPersonaje2--;
            }
        }
        public static void EfectuarMovimiento(Personaje atacante, Personaje defensor, int movimiento, ref int curacionesDisponiblesPersonaje1, ref int curacionesDisponiblesPersonaje2, Personaje personaje1, Personaje personaje2, int turnos)
        {
            int aumentarAtaque = 1;
            if(turnos >= 8)
            {
                aumentarAtaque = 2;
            }else if(turnos >= 13)
            {
                aumentarAtaque = 3;
            }else if(turnos >= 18)
            {
                aumentarAtaque = 4;
            }
            else if(turnos >= 23)
            {
                aumentarAtaque = 5;
            }

            // cuando ataca //
            if(movimiento == 0)
            {
                int danio = CalcularDanio(atacante,defensor);
                danio *= aumentarAtaque;
                defensor.caracteristicas.Salud -= danio;
                Console.WriteLine($"{atacante.datos.Nombre} {atacante.datos.Apodo} ataca por {danio}pts");
            }
            else
            {
                DescontarCuraciones(atacante,personaje1,personaje2,ref curacionesDisponiblesPersonaje1,ref curacionesDisponiblesPersonaje2);
                // curamos de 1 a 10 puntos aleatoriamente //
                int curacionRandom = FabricaPersonajes.AleatorioAaB(1,10);
                // tambien verificamos que no se sobrepase los 100 de salud //
                int vidaResultante = atacante.caracteristicas.Salud + curacionRandom;
                if(vidaResultante > 100)
                {
                    int curacionEfectiva = 100- atacante.caracteristicas.Salud;
                    Console.WriteLine($"{atacante.datos.Nombre} {atacante.datos.Apodo} se cura por {curacionEfectiva}pts");
                    atacante.caracteristicas.Salud = 100;
                }
                else
                {
                    Console.WriteLine($"{atacante.datos.Nombre} {atacante.datos.Apodo} se cura por {curacionRandom}pts");
                    atacante.caracteristicas.Salud += curacionRandom;
                }
            }
        }

        public static void CambiarRoles(ref Personaje atacante,ref Personaje defensor, Personaje personaje1, Personaje personaje2)
        {
            if(personaje1 == atacante)
            {
                atacante = personaje2;
                defensor = personaje1;
            }
            else
            {
                atacante = personaje1;
                defensor = personaje2;
            }
        }

        public static void PresentarPelea(Personaje personaje1,Personaje personaje2)
        {
            Console.WriteLine("==================== PELEA ====================");
            Console.WriteLine($"{personaje1.datos.Nombre} {personaje1.datos.Apodo} vs {personaje2.datos.Nombre} {personaje2.datos.Apodo}");
        }

        public static void ResultadoPelea(Personaje personaje1, Personaje personaje2)
        {
            if(personaje1.caracteristicas.Salud <= 0)
            {
                Console.WriteLine($"{personaje1.datos.Nombre} {personaje1.datos.Apodo} Ha muerto");
                Console.WriteLine($"{personaje2.datos.Nombre} {personaje2.datos.Apodo} avanza de ronda");
            }
            else
            {
                Console.WriteLine($"{personaje2.datos.Nombre} {personaje2.datos.Apodo} Ha muerto");
                Console.WriteLine($"{personaje1.datos.Nombre} {personaje1.datos.Apodo} avanza de ronda");
            }
        }

        public static void DibujarLinea()
        {
            Console.WriteLine("===============================================");
        }

        public static void MostrarTurnos(ref int turnos, int movimientosEfectivosRealizados, Personaje atacante)
        {
            if(movimientosEfectivosRealizados % 2 == 0 && movimientosEfectivosRealizados >= 2)
            {
                turnos++;
            }
            if(turnos >= 8 && turnos < 13)
            {
                Console.WriteLine("los personajes ahora atacan un 100% mas fuerte");
            }else if(turnos >= 13 && turnos < 18)
            {
                Console.WriteLine("los personaje ahora pegan un 200% mas fuerte");
            }else if(turnos >= 18 && turnos < 23)
            {
                Console.WriteLine("los personajes ahora pegan un 300% mas fuerte");
            }else if(turnos >= 23)
            {
                Console.WriteLine("los personajes ahora pegan un 400% mas fuerte");
            }
            Console.WriteLine($"-- TURNO {turnos} DE {atacante.datos.Nombre} {atacante.datos.Apodo}");
        }

        public static void AvanzarSiguienteTurno()
        {
            Console.WriteLine("presione cualquier tecla para ir al siguiente turno");
            Console.ReadKey();
        }

        

        public static void SimularCombate(Personaje personaje1, Personaje personaje2)
        {
            // presentamos la pelea y decidimos quien arranca y quien defiende //
            PresentarPelea(personaje1,personaje2);
            Personaje atacante = DecidirQuienArranca(personaje1,personaje2);
            Personaje defensor = DecidirDefensor(personaje1,personaje2,atacante);
            Console.WriteLine($"Arranca:{atacante.datos.Nombre} {atacante.datos.Apodo}");
            DibujarLinea();

            // variable para controlar el numero de curaciones que pueden usar cada personaje //
            int curacionesDisponiblesPersonaje1 = 3;
            int curacionesDisponiblesPersonaje2 = 3;

            // variables para controlar el numero de movimientos efectivos realizados y el numero de turnos //
            int turnos = 1;
            int movimientosEfectivosRealizados = 0;
            // bucle del combate //
            while(personaje1.caracteristicas.Salud > 0 && personaje2.caracteristicas.Salud > 0)
            {
                MostrarTurnos(ref turnos,movimientosEfectivosRealizados,atacante);
                // mostramos los datos relavantes de cada personaje //
                MostrarDatosRelevantes(personaje1,curacionesDisponiblesPersonaje1);
                MostrarDatosRelevantes(personaje2,curacionesDisponiblesPersonaje2);
                DibujarLinea();
                // variable para guardar el movimiento del atacante //
                int movimiento = DecidirMovimiento(atacante,curacionesDisponiblesPersonaje1,curacionesDisponiblesPersonaje2,personaje1,personaje2);
                EfectuarMovimiento(atacante,defensor,movimiento,ref curacionesDisponiblesPersonaje1,ref curacionesDisponiblesPersonaje2,personaje1,personaje2,turnos);
                movimientosEfectivosRealizados++;
                CambiarRoles(ref atacante,ref defensor,personaje1,personaje2);
                AvanzarSiguienteTurno();
                Console.WriteLine("\n");
                DibujarLinea();
            }

            // mostramos el resultado de la pelea //
            ResultadoPelea(personaje1,personaje2);
        }

    }

}
