using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3;

namespace lab3_task2
{
    class Doctor : Process2
    {
        private Random r_next;
        public int toLab;
        public List<Element> nextElem;

        public Doctor(double delay, int max) : base(delay, max)
        {
            // queue = new List<int>();
            // stateProcess = new List<int>();
            //  meanq = 0.0;
            // maxState = max;
            // r_next = new Random(this.GetHashCode() * (int)(DateTime.Now.ToFileTime()));
            toLab = 0;
        }

      /*  public void InAct(int type)
        {
            Console.WriteLine($"IN   name {GetName()} process {type}");
            if (stateProcess.Count < maxState)
            {
                stateProcess.Add(type);
                var d = GetDelay();
                SetTimeNext(GetTimeCurr() + d);
            }
            else
            {
                if (queue.Count < maxq)
                {
                    queue.Add(type);

                }

                else
                {
                    failure = failure + 1;
                }
            }

            Console.WriteLine($"DOCTOR IN stateProcess[] count {stateProcess.Count}   queue count {queue.Count}");
        }*/

        public override void OutAct()
        {
            //base.OutAct();
            Console.WriteLine($"111111111");
            
            int type = 0, queueType;

            SetTimeNext(Double.MaxValue);

            //time.Remove(time.Min());
            Console.WriteLine($"DOCTOR    OUT   stateProcess[] count {stateProcess.Count}   queue count {queue.Count}");
            if (stateProcess.Count > 0)
            {
                for (int i = 0; i < stateProcess.Count; i++)
                {
                    if (stateProcess[i] == 1)
                    {
                        type = stateProcess[i];
                        stateProcess.RemoveAt(i);
                        Console.WriteLine($"Doctors type {type}");
                        break;
                    }
                }

                if (type == 0)
                {
                    type = stateProcess[0];
                    stateProcess.RemoveAt(0);
                }
            }

            if (queue.Count > 0)
            {
                queueType = queue[0];
                queue.RemoveAt(0);
                // Console.WriteLine($" name = {GetName()}     QUEUE OUT {queue}");
                stateProcess.Add(queueType);
                //time.Add(GetTimeCurr() + GetDelay());
            }

            //Console.WriteLine($"{type}");

            Element e;
            if (type != 0)
            {
                if (type == 1)
                {
                    e = nextElem[0];
                }
                else
                {
                    e = nextElem[1];
                    toLab++;
                }

                Console.WriteLine($"name = {e.GetName()} type {type}");
                e.InAct(type);
            }

        }

        public void SetNextElement(List<Element> list)
        {
            nextElem = list;

        }
    }
}
