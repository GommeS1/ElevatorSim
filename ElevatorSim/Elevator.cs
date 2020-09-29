using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class Elevator
    {
        public int currentFloor { get; set; } //Hvilken etage elevator er på
        public int targetFloor { get; set; }//Hvor elevator skal hen
        public List<Personer> personInside { get; set; }//En liste over mennesker der er inde i elevatoren

        int capacity { get; set; } //Hvor mange der kan være i elevatoren

        Random random = new Random();

        public Elevator(List<Personer> personers) 
        {
            capacity = 5;

            personInside = new List<Personer>();

            //Vælger en random person som skal være starten på programmet
            int randomTal = random.Next(0, personers.Count);
            personers[randomTal].currentFloor = 0;
            personers[randomTal].state = Personer.State.INSIDE;
            personInside.Add(personers[randomTal]);
            targetFloor = personInside[0].targetFloor;
        }
        public void Unload(Personer person) //Smider mennesker af 
        {
            personInside.Remove(person);
            person.state = Personer.State.HAPPY;
            person.currentFloor = currentFloor;
        }

        public void Load(Personer person) //Tager mennesker ind i elevatoren
        {
            if (personInside.Count < capacity) 
            {
                personInside.Add(person);
                person.state = Personer.State.INSIDE;
            }
 
        }
        public bool NextInside()//Får den næste person når den første er sat af
        {
            if (personInside.Count > 0) 
            {
                targetFloor = personInside[0].targetFloor;
                return true;
            }

            return false;
        }
        public bool waitingPerson(List<Personer> personers) //Kigger på om der er nogle der mangler og hvis der er så tager elevatoren der hen
        {
            for (int i = 0; i < personers.Count; i++)
            {
                if (personers[i].state == Personer.State.SAD) 
                {
                    targetFloor = personers[i].currentFloor;
                    return false;
                }
            }
            return true;
        }

    }
}
