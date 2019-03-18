using System;
using System.Collections.Generic;
using System.Linq;
using lab3;

namespace lab3_task1
{
    class Creator1 : Element
    {
        private Dictionary<Process1, double> nextElem;
        public Creator1(double delay) : base(delay)
        {
            
        }
        
        public override void OutAct()
        {
            base.OutAct();
            var delay = GetDelay();
            SetTimeNext(GetTimeCurr() + delay);
            GetNextElement().InAct();
        }

        public override double Workload(double delta)
        {
            return 0.0;

        }

        public override double MeanQueue(double delta)
        {
            return 0.0;

        }

        public void SetNextElement(Dictionary<Process1, double> list)
        {
            nextElem = list;

        }

        public Process1 GetNextElement()
        {
            int[] queueProcess = { nextElem.Keys.ElementAt(0).queue, nextElem.Keys.ElementAt(1).queue };
           // Console.WriteLine($"process 1 {nextElem.Keys.ElementAt(0).queue}  process2 {nextElem.Keys.ElementAt(1).queue}");
            if (queueProcess[0] == queueProcess[1])
            {
                return nextElem.Keys.ElementAt(0);
            }
            else
            {
                if (queueProcess[0] > queueProcess[1])
                {
                    return nextElem.Keys.ElementAt(1);
                }

                return nextElem.Keys.ElementAt(0);
            }
        }
    }
}
