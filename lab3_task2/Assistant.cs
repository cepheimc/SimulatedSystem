using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3;

namespace lab3_task2
{
    class Assistant : Process2
    {
        private Random r_next;

        public Assistant(double delay, int max) : base(delay, max)
        {
           // queue = new List<int>();
           // stateProcess = new List<int>();
          //  meanq = 0.0;
           // maxState = max;
           // r_next = new Random(this.GetHashCode() * (int)(DateTime.Now.ToFileTime()));
        }

        public override void OutAct()
        {
            base.OutAct();
            int type = 0, queueType;

            SetTimeNext(Double.MaxValue);

            //time.Remove(time.Min());

            if (stateProcess.Count > 0)
            {
                type = stateProcess[0];
                stateProcess.RemoveAt(0);
            }

            if (queue.Count > 0)
            {
                queueType = queue[0];
                queue.RemoveAt(0);
                // Console.WriteLine($" name = {GetName()}     QUEUE OUT {queue}");
                stateProcess.Add(queueType);
                //time.Add(GetTimeCurr() + GetDelay());
            }

            Element e;
            if (type != 0)
            {
                if (type == 2)
                {
                    e = nextElem[0];
                    e.InAct(type);
                }
            }

        }
    }
}
