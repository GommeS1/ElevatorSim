using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class Converters
    {
        public static double UserStringToDouble(string userString) //Laver en string om til double og return double
        {
            double res;
            while (!double.TryParse(userString, out res))
            {
                Console.WriteLine("Du skrev ikke et gyldigt tal, prøv igen");
                userString = Console.ReadLine();

            }


            return res;
        }
        public static int UserStringToInt(string userString) //Laver en string om til int og return int
        {
            int res;
            while (!int.TryParse(userString, out res))
            {
                Console.WriteLine("Du skrev ikke et gyldigt tal, prøv igen");
                userString = Console.ReadLine();

            }

            return res;
        }
    }
}
