using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class Personer
    {
        public int currentFloor { get; set; }
        public int targetFloor { get; set; }
        public State state { get; set; }

        public Personer(int floorCount, Random random) 
        {
            state = State.SAD;

            currentFloor = random.Next(0,floorCount);
            do
            {
                targetFloor = random.Next(0, floorCount);
            } while (targetFloor == currentFloor);
            
        }

        public enum State 
        {
            HAPPY,
            SAD,
            INSIDE
        }
    }
}
