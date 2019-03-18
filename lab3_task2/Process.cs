using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_task2
{
    public class Process : Element
    {
        public int queue,
            maxq = Int32.MaxValue,
            failure,
            maxState, state;

        public double workload;
        private Dictionary<Element, int> nextElem;
        private List<double> time = new List<double>();
        public double meanq;
        private Random r_next;
        public double meanWorkTime = 0, meanWorkAmount = 0;


        public Process(double delay, int max) : base(delay)
        {
            state = 0;
            queue = 0;
            meanq = 0.0;
            maxState = max;
            var seed = this.GetHashCode() * (int)(DateTime.Now.ToFileTime());
            r_next = new Random(seed);
        }

        public void InAct()
        {
            int i = 0;
            if (state < maxState)
            {
                state = state + 1;
                var d = GetDelay();
                meanWorkTime += d;
                meanWorkAmount++;
                time.Add(GetTimeCurr() + d);
            }
            else
            {
                if (queue < maxq)
                {
                   queue = queue + 1;

                }
                else
                {
                    failure = failure + 1;
                }
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
            workload = workload + (double)(state) * delta;
            return workload;
        }

        public override double MeanQueue(double delta)
        {

            meanq = meanq + (queue * delta);

            return meanq;
        }*/
    }
}
