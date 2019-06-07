using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSlotEnumerator
{
    public class TimeSlotCollection : IEnumerator<TimeSpan>
    {
        public int elementInRow = 12;
        
        public int position = -1;
        public List<TimeSpan> elementsList;

        public TimeSlotCollection()
        {
            elementsList = GenerateTimeSlots();
        }

        public List<TimeSpan> GenerateTimeSlots()
        {
            List<TimeSpan> result = new List<TimeSpan>();
            TimeSpan breakfastOpenTime = new TimeSpan(9, 0, 0);

            TimeSpan slotInterval = new TimeSpan(0, 15, 0);


            for (int counter = 1; counter < elementInRow; counter++)
            {
                result.Add(new TimeSpan(breakfastOpenTime.Hours, breakfastOpenTime.Minutes, 0));
                breakfastOpenTime += slotInterval;
            }

            return result;
        }


        public TimeSpan Current
        {
            get { return elementsList[position]; }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Current.ToString(@"hh\:mm");
        }

        public bool MoveNext()
        {
            return true;
        }

        public void Reset()
        {
            position = -1;
        }

        public void Reset(int index)
        {
            position = index;
        }

       
    }
}

