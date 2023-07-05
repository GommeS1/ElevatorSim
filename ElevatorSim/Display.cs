using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class Display
    {
        public int floorCount { get; set; } //Antal etager

        public Elevator elevator { get; set; } //Håndter alt hvad der har med elevatoren at gører 
        public List<Personer> personers { get; set; } //Liste over alle de mennesker der er i programmet 

        public Display(int antalPersoner, int antalFloors)
        {
            personers = new List<Personer>(); 

            floorCount = antalFloors; //Sætter brugerens ønske om hvor mange etager der skal være

            //Opretter alle menneskerne, de får et random sted at starte og sætter deres slutpunkt
            Random random = new Random();
            for (int i = 0; i < antalPersoner; i++)
            {
                personers.Add(new Personer(floorCount, random));
            }

            elevator = new Elevator(personers); //Opretter elevatoren

            elevator.currentFloor = 0; //Elevatoren starter alt på stueetage/etage 0
            
        }

        public void showButtons() //Udskriver etager samt mennesker 
        {
            for (int i = floorCount; i >= 0; i--)
            {
                Console.Write("| ");
                if (i == elevator.currentFloor) Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" | ");
                showPersons(i);
                Console.WriteLine();
            }
        }

        void showPersons(int i) 
        {
            foreach (var item in personers)
            {
                if (item.currentFloor == i && item.state != Personer.State.INSIDE)
                {
                    if (item.state == Personer.State.SAD) Console.ForegroundColor = ConsoleColor.Red;
                    else Console.ForegroundColor = ConsoleColor.Green;
 
                    Console.Write($" P{item.targetFloor}");
                    Console.ForegroundColor = ConsoleColor.White;
                }


            }
        }

        void animMove(int i) 
        {
            Console.Clear();
            for (int j = floorCount; j >= 0; j--)
            {
                Console.Write("| ");
                if (j == i) Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{j}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" |");
                showPersons(j);
                Console.WriteLine();
            }
            for (int k = 0; k < personers.Count; k++)
            {
                if (personers[k].currentFloor == elevator.currentFloor && personers[k].state == Personer.State.SAD)
                {
                    elevator.Load(personers[k]);
                }

                if (personers[k].targetFloor == elevator.currentFloor && personers[k].state == Personer.State.INSIDE)
                {
                    elevator.Unload(personers[k]);

                }
                if (personers[k].state == Personer.State.INSIDE)
                {
                    personers[k].currentFloor = elevator.currentFloor;
                }
            }

            Console.WriteLine($"Current floor: {elevator.currentFloor}");
            Console.WriteLine($"Target floor: {elevator.targetFloor}");
            Console.WriteLine($"Total ppl waiting: {personers.Where(x => x.state == Personer.State.SAD).Count()}");
            Console.WriteLine($"Total ppl inside: {elevator.personInside.Count}");
            for (int k = 0; k < elevator.personInside.Count; k++)
            {
                Console.Write($" P{elevator.personInside[k].targetFloor}");
            }
            Thread.Sleep(10);

        }
        public void Move() //Kører elevatoren op eller ned, loader og undloader mennesker
        {
            bool stillIndside; //Til hvis der er stadig nogle inde i elevatoren
            bool waiting = true;//Til folk der står og venter på elevator
 

            do
            {
                if (elevator.targetFloor < elevator.currentFloor)
                {
                    for (int i = elevator.currentFloor; i >= elevator.targetFloor; i--)
                    {
                        animMove(i);
                        elevator.currentFloor--;
                    }
                }
                else
                {
                    for (int i = elevator.currentFloor; i <= elevator.targetFloor; i++)
                    {
                        animMove(i);
                        elevator.currentFloor++;
                    }
                }
                stillIndside = elevator.NextInside();
                if (!stillIndside) 
                {
                    waiting = elevator.waitingPerson(personers);
                }
                

            } while (elevator.personInside.Count != 0 || waiting);
                
        }
            
    }
            
}

