using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class Program
    {
        static void Main(string[] args)
        {
            //Udskriver Menu og får brugerinput til config af elevator
            Console.WriteLine("Velkommen til elevator sim. Her kan du se hvordan min elevator vil virke med mennekser");

            Console.Write("Hvor mange mennesker vil du have?: ");
            int antalmennesker = Converters.UserStringToInt(Console.ReadLine());

            Console.Write("Hvor mange etager vil du have?: ");
            int antalFloor = Converters.UserStringToInt(Console.ReadLine());

            Display display = new Display(antalmennesker, antalFloor); //Opretter display class

            display.showButtons(); //Viser alle etagerne samt mennesker der er i programmet

            Console.ReadKey();

            display.Move(); //Starter sim. Den kører indtil alle menneker er på deres rigtige 

            Console.WriteLine("Done!");

            Console.ReadKey();



        }
    }
}
