using System;
using System.Collections.Generic;
using System.Linq;
using lab3;

namespace lab3_task1
{
    class Process1 : Element
    {
        public int queue,
            maxq = Int32.MaxValue,
            failure,
            maxState;

        public double workload;
        private Dictionary<Element, double> nextElem;
        private List<double> time = new List<double>();
        public double meanq;
        private Random r_next;
        public Process1 otherProcess;
        public int changeQueue;
        //public bool isBlocked;


        public Process1(double delay, int max) : base(delay)
        {
            queue = 0;
            meanq = 0.0;
            maxState = max;
            r_next = new Random(this.GetHashCode() * (int)(DateTime.Now.ToFileTime()));
            changeQueue = 0;
            isBlocked = false;
        }

        public override void InAct()
        {
            int i = 0;
            isBlocked = false;
            if (state < maxState)
            {
                state = state + 1;
                var d = GetDelay();
                time.Add(GetTimeCurr() + d);
                isBlocked = false;
            }
            else
            {
                if (queue < maxq)
                {
                    queue = queue + 1;
                    isBlocked = false;

                }

                else
                {
                    failure = failure + 1;
                    isBlocked = true;
                }
            }
            
        }

        public override void OutAct()
        {
            
                base.OutAct();
                // Console.WriteLine($"name = {GetName()}     stateOut1 - {state}");
                state = state - 1;

                time.Remove(time.Min());

                if ((queue - otherProcess.queue) >= 2)
                {
                    queue = queue - 1;
                    otherProcess.InAct();
                    changeQueue++;
                }

                if (queue > 0)
                {
                    queue = queue - 1;
                    state = state + 1;
                    time.Add(GetTimeCurr() + GetDelay());
                }

                Element e = GetNextElement();

                if (e != null)
                {

                    e.InAct();
                }

        }

        public override void SetNextElement(Dictionary<Element, double> list)
        {
            nextElem = list;

        }

        public override Element GetNextElement()
        {
            int i;

            if (nextElem != null)
            {
                i = r_next.Next(nextElem.Count);
                return nextElem.Keys.ElementAt(i);
            }

            else
            {
                return base.GetNextElement();
            }
            
        }

        public override double GetTimeNext()
        {
            if (time.Count == 0)
            {
                return Double.MaxValue;
            }
            else
            {
                return time.Min();
            }
        }

        public void SetMaxQueue(int m)
        {
            maxq = m;
        }

        public int GetMaxQueue()
        {
            return maxq;
        }

        public void SetQueue(int q)
        {
            queue = q;
        }

        public int GetQueue()
        {
            return queue;
        }

        public override void WriteInfo()
        {
            base.WriteInfo();
            Console.WriteLine($"   IN: maxState = {maxState}  maxQueue = {maxq}");
            Console.WriteLine($"   OUT: failure = {failure}   queue = {queue}\n");
        }

        public double GetFailure()
        {
            return failure;
        }

        public double GetMeanQueue()
        {
            return meanq;
        }

        public override double Workload(double delta)
        {
            workload = workload + (double)(state) * delta;
            return workload;
        }

        public override double MeanQueue(double delta)
        {
            
            meanq = meanq + (queue * delta);
            return meanq;
        }
    }
}
