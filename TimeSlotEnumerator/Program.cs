using System;
using System.Collections.Generic;

namespace TimeSlotEnumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSlotCollection timeSlotCollection = new TimeSlotCollection();
            List<TimeSpan> slots = timeSlotCollection.elementsList;
            
            Console.Write("Time: ");
            for (int i = 0; i < slots.Count; i++)
            {
                Console.Write(slots[i].ToString(@"hh\:mm") + " ");
            }
            Console.WriteLine();

            Console.Write("Time slots shown: ");
            int slotsToShow = Convert.ToInt32(Console.ReadLine());

            Console.Write("Customer reques: "); //HH:MM format
            TimeSpan customerRequestTime = TimeSpan.Parse(Console.ReadLine());

            int index = slots.FindIndex(a => a.Hours == customerRequestTime.Hours && a.Minutes == customerRequestTime.Minutes);


            DisplaySlots(timeSlotCollection, index, slotsToShow);

            Console.ReadKey();
        }


        public static void DisplaySlots(TimeSlotCollection collection, int index, int slotsToShow)
        {
            collection.position = index;
            Console.Write(collection.Current.ToString(@"hh\:mm") + ' ');

            for (int i = 1; i < slotsToShow / 2 + 1; i++)
            {

                collection.position -= i;
                if (collection.position >= 0 && collection.position < collection.elementInRow - 1)
                {
                    Console.Write(collection.Current.ToString(@"hh\:mm") + ' ');
                }
                else
                {
                    slotsToShow++;
                }
                collection.position += i * 2;
                if (collection.position >= 0 && collection.position < collection.elementInRow - 1)
                {
                    Console.Write(collection.Current.ToString(@"hh\:mm") + ' ');
                }
                else
                {
                    slotsToShow++;
                }

                collection.Reset(index);
            }
        }
    }
}

