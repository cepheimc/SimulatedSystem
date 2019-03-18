using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3;

namespace lab3_task2
{
    class Creator2 : Creator
    {
        private Random r_next;
       // private Element[] nextElem;

        private double[] time = {15, 40, 30};

        public Creator2(double delay) : base(delay)
        {
            var seed = this.GetHashCode() * (int)(DateTime.Now.ToFileTime());
            r_next = new Random(seed);
        }

        public void InAct()
        {

        }

        public void PickNext()
        {
            double value = r_next.NextDouble();
            if (value < 0.5)
            {
                nextElem[Doctor]
            }
            if (value < (0.5 + 0.1))
            {
                nextElem[0].InAct(2);
            }
            else
            {
                nextElem[0].InAct(3);
            }
        }

       /* public override void OutAct()
        {
            base.OutAct();
            
            double[] values = {0.5, 0.1, 0.4};
            int patient_type = GetType(values);
            Console.WriteLine($"Creator {patient_type}");
            var delay = GetDelay();
            SetTimeNext(GetTimeCurr() + delay);
            GetNextElement().SetDelayMean(time[patient_type-1]);
            GetNextElement().InAct(patient_type);
        }*/

        public override double Workload(double delta)
        {
            return 0.0;

        }

        public override double MeanQueue(double delta)
        {
            return 0.0;

        }

        public int GetType(double[] values)
        {
            double value = r_next.NextDouble();
            for (int i = 0; i < values.Length; i++)
            {
                value -= values[i];
                if(value <= 0)
                {
                    return i+1;
                }
            }

            return 0;
        }

      /*  public void SetNextElement(Element[] element)
        {
            nextElem = element;

        }*/

       /* public Doctor GetNextElement()
        {
            return nextElem;
        }*/
    }
}
