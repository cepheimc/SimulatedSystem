using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3
{
    class Process : Element
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
        

        public Process(double delay, int max) : base(delay)
        {
            queue = 0;
            meanq = 0.0;
            maxState = max;
            var seed = this.GetHashCode() * (int) (DateTime.Now.ToFileTime());
            r_next = new Random(seed);
        }

        public override void InAct()
        {
            int i = 0;
            if (state < maxState)
            {
                state = state + 1;
                var d = GetDelay();
                time.Add(GetTimeCurr() + d);
                //Console.WriteLine($"New tnext with delay: {d}");
            }
            else
            {
                if (queue < maxq)
                {
                  //  Console.WriteLine($"{GetName()} Got new, queue {queue}");
                    queue = queue + 1;

                }
                else
                {
                    failure = failure + 1;
                }
            }

            // Console.WriteLine($"stateIn = {state}\n");

        }

        public override void OutAct()
        {
            base.OutAct();
            // Console.WriteLine($"name = {GetName()}     stateOut1 - {state}");
            state = state - 1;

            time.Remove(time.Min());

            if (queue > 0)
            {
                queue = queue - 1;
                // Console.WriteLine($" name = {GetName()}     QUEUE OUT {queue}");
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
            if (nextElem != null)
            {
                double value = r_next.NextDouble();
                for (int i = 0; i < nextElem.Count; i++)
                {
                    value -= nextElem.Values.ElementAt(i);
                    if (value <= 0)
                    {
                       return nextElem.Keys.ElementAt(i);
                    }
                }
               
                return null; 
            }

            else
            {
                return base.GetNextElement();
            }

            //return null;
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
