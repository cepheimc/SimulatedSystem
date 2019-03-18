using System;
using System.Collections.Generic;
using lab3;

namespace lab3_task1
{
    class Model1
    {
        private List<Element> list = new List<Element>();
        private double tnext, tcurr;
        public double[] workload = new double[10];
        public double[] meanq = new double[10];
        public double[] wait = new double[10];
        int even;
        public double[] timeOutWindow = new double[10];

        public Model1(List<Element> l)
        {
            list = l;
            tcurr = tnext = 0.0;
            even = 0;
        }

        public Element GetElement(int id)
        {
            foreach (Element e in list)
            {
                if (e.GetId() == id)
                {
                    return e;
                }
            }

            return null;
        }

        public void Simulation(double time)
        {
            foreach (Element p in list)
            {
                if (p.GetType() == typeof(Process1))
                {
                    p.SetTimeNext(Double.MaxValue);
                }
            }

            while (tcurr < time)
            {
                tnext = Double.MaxValue;
                foreach (Element e in list)
                {
                    if (e.GetTimeNext() < tnext)
                    {
                        tnext = e.GetTimeNext();
                        even = e.GetId();

                    }
                }
                // Console.WriteLine($"\nEvent in {list[even].GetName()} time = {tnext}\n");

                int i = 0;
                foreach (Element e in list)
                {
                    if (e.GetType() == typeof(Process1))
                    {
                        Process1 p = (Process1)e;
                        double m = e.MeanQueue(tnext - tcurr);
                        workload[i] = e.Workload(tnext - tcurr) / tcurr;
                        meanq[i] = m / tcurr;
                        timeOutWindow[i] = tcurr / e.quantity;
                        wait[i] = m / (e.quantity + p.failure);
                    }
                    
                    i++;
                }

                tcurr = tnext;

                foreach (Element e in list)
                {
                    e.SetTimeCurr(tcurr);
                }

                while (even == 2 && list[even].isBlocked)
                {
                    list[1].isBlocked = true;
                }

                if (!list[even].isBlocked)
                {
                    list[even].OutAct();
                }
                

                foreach (Element e in list)
                {
                    if (e.GetTimeNext() == tcurr)
                    {
                        e.OutAct();
                    }
                }

            }

        }

        public void WriteResult()
        {
            foreach (Element e in list)
            {
                e.WriteResult();

            }
        }
    }
}
