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
        private List<double> time = new List<double>();
        public double meanq;
        private Random r_next;

        public List<int> queue= new List<int>();
        public List<int> stateProcess = new List<int>();

        public void PickNext(int v)
        {
           // Console.WriteLine($"Picing from {nextElem.Count}");
                if (nextElem.ContainsKey(v))
                {
                   // Console.WriteLine($"{nextElem.FirstOrDefault(x => x.Key == v).Value.GetName()}");
                    nextElem.FirstOrDefault(x => x.Key == v).Value.InAct(v);
                }
        }


        public Process2(double delay, int max) : base(delay, max)
        {
            meanq = 0.0;
            maxState = max;
            r_next = new Random(this.GetHashCode() * (int)(DateTime.Now.ToFileTime()));
        }

        public override void InAct(int type)
        {
           // Console.WriteLine($"IN  name {GetName()} process {type}");
            if (stateProcess.Count < maxState)
            {
                stateProcess.Add(type);
                var d = GetDelay();
                meanWorkTime += d;
                meanWorkAmount++;
                time.Add(GetTimeCurr() + d);
               // Console.WriteLine($"Addedtime of : {GetTimeCurr() + d}, now state is {time.Count}");
            }
            else
            {
                if (queue.Count < maxq)
                {
                   // Console.WriteLine($"{GetName()} queue {queue.Count}");
                    queue.Add(type);

                }

                else
                {
                    failure = failure + 1;
                }
            }
           // Console.WriteLine($"Finishing in act, now tnext is {GetTimeNext()}");
           // Console.WriteLine("----");
            
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

        public override void OutAct()
        {
           // base.OutAct();
            quantity++;
            int type, queueType;

           // Console.WriteLine($"OUT   name {GetName()}");
            
          //  Console.WriteLine($"time in OUT: {time.Count}");
            int index = time.IndexOf(time.Min());
            time.Remove(time.Min());
            type = stateProcess[index];
            stateProcess.RemoveAt(index);

            if (queue.Count > 0)
            {
                queueType = queue[0];
                queue.RemoveAt(0);
                    // Console.WriteLine($" name = {GetName()}     QUEUE OUT {queue}");
                stateProcess.Add(queueType);
                time.Add(GetTimeCurr() + GetDelay());
            }

            

            PickNext(type);
        }

        public double MeanWork()
        {
            return meanWorkTime / (meanWorkAmount + 1);
        }

        public void SetMaxQueue(int m)
        {
            maxq = m;
        }

        public int GetMaxQueue()
        {
            return maxq;
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
            workload = workload + (double)(stateProcess.Count) * delta;
            return workload;
        }

        public override double MeanQueue(double delta)
        {

            meanq = meanq + (queue.Count * delta);
            return meanq;
        }
    }
}
