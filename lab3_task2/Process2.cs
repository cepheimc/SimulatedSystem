using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3_task2
{
    public class Process2 : Process
    {
        public int maxq = Int32.MaxValue,
            failure,
            maxState;

        public double workload;
        public List<Element> nextElem;
        private List<double> time = new List<double>();
        public double meanq;
        private Random r_next;

        public List<int> queue= new List<int>();
        public List<int> stateProcess = new List<int>();

        public void PickNext(int v)
        {

        }


        public Process2(double delay, int max) : base(delay, max)
        {
           // queue = new List<int>();
           // stateProcess = new List<int>();
            meanq = 0.0;
            maxState = max;
            r_next = new Random(this.GetHashCode() * (int)(DateTime.Now.ToFileTime()));
        }

        public override void InAct(int type)
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

            Console.WriteLine($"PROCESs IN stateProcess[] count {stateProcess.Count}   queue count {queue.Count}");
        }

        public override void OutAct()
        {
            base.OutAct();
            int type, queueType;

            SetTimeNext(Double.MaxValue);
            
            //if (stateProcess.Count > 0)
            //{
                type = stateProcess[0];
                stateProcess.RemoveAt(0);
           // }

            if (queue.Count > 0)
            {
                queueType = queue[0];
                queue.RemoveAt(0);
                    // Console.WriteLine($" name = {GetName()}     QUEUE OUT {queue}");
                stateProcess.Add(queueType);
                //time.Add(GetTimeCurr() + GetDelay());
            }

            Console.WriteLine($"OUT   name {GetName()} process {type}");

            PickNext(type);
            

        }

       /* public override void SetNextElement(List<Element> list)
        {
            nextElem = list;

        }

        public override Element GetNextElement()
        {
            int i;

            if (nextElem != null)
            {
                i = r_next.Next(nextElem.Count);
                return nextElem[i];
            }

            else
            {
                return base.GetNextElement();
            }

        }

       /* public override double GetTimeNext()
        {
            if (time.Count == 0)
            {
                return Double.MaxValue;
            }
            else
            {
                return time.Min();
            }
        }*/

        public void SetMaxQueue(int m)
        {
            maxq = m;
        }

        public int GetMaxQueue()
        {
            return maxq;
        }

       /* public void SetQueue(int q)
        {
            queue = q;
        }

        public int GetQueue()
        {
            return queue;
        }*/

        /*public override void WriteInfo()
        {
            base.WriteInfo();
            Console.WriteLine($"   IN: maxState = {maxState}  maxQueue = {maxq}");
            Console.WriteLine($"   OUT: failure = {failure}   queue = {queue}\n");
        }*/

        public double GetFailure()
        {
            return failure;
        }

        public double GetMeanQueue()
        {
            return meanq;
        }

       /* public override double Workload(double delta)
        {
            workload = workload + (double)(stateProcess.Count) * delta;
            return workload;
        }

        public override double MeanQueue(double delta)
        {

            meanq = meanq + (queue.Count * delta);
            return meanq;
        }*/
    }
}
