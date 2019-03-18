using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3_task2
{
    class Creator2 : Element
    {
        private Random r_next;

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
               // Console.WriteLine($"Creator passes to {nextElem.ElementAt(0).Value.GetName()}");
                nextElem.ElementAt(0).Value.InAct(1);
            }
            else if (value < (0.5 + 0.1))
            {
               // Console.WriteLine($"Creator passes to {nextElem.ElementAt(1).Value.GetName()}");
                nextElem.ElementAt(1).Value.InAct(2);
            }
            else
            {
               // Console.WriteLine($"Creator passes to {nextElem.ElementAt(2).Value.GetName()}");
                nextElem.ElementAt(2).Value.InAct(3);
            }
        }

        public override void OutAct()
        {
            base.OutAct();

            var d = GetDelay();
            SetTimeNext(GetTimeCurr() + d);
            PickNext();
        }

        public override double Workload(double delta)
        {
            return 0.0;

        }

        public override double MeanQueue(double delta)
        {
            return 0.0;

        }
    }
}
