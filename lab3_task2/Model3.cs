using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3;

namespace lab3_task2
{
    class Model3
    {
        private List<Element> list = new List<Element>();
        private double tnext, tcurr;
        public double[] workload = new double[10];
        public double[] meanq = new double[10];
        public double[] wait = new double[10];
        int even;

        public Model3(List<Element> l)
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
                if (p.GetType() == typeof(Process2))
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
                       // Console.WriteLine($"id {e.GetId()}");
                        even = e.GetId();
                    }
                }
               // Console.WriteLine($"\nEvent in even {even} {list[even].GetName()} time = {tnext}\n");

                int i = 0;
                foreach (Element e in list)
                {
                    if (e.GetType() == typeof(Process2))
                    {
                        Process2 p = (Process2)e;
                        double m = e.MeanQueue(tnext - tcurr);
                        //Console.WriteLine($"mean q {m}");
                        workload[i] = e.Workload(tnext - tcurr) / tcurr;
                        meanq[i] = m / tcurr;
                        wait[i] = m / (e.quantity + p.failure);
                    }
                    i++;
                }

                tcurr = tnext;

                foreach (Element e in list)
                {
                    e.SetTimeCurr(tcurr);
                }

                list[even].OutAct();

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
