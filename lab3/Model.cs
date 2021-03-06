﻿using System;
using System.Collections.Generic;

namespace lab3
{
    public class Model
    {
        private List<Element> list = new List<Element>();
        private double tnext, tcurr;
        public double[] workload = new double[10];
        public double[] meanq = new double[10];
        public double[] wait = new double[10];
        int even;

        public Model(List<Element> l)
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
                if (p.GetType() == typeof(Process))
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
                    if (e.GetType() == typeof(Process))
                    {
                        Process p = (Process)e;
                        double m = e.MeanQueue(tnext - tcurr);
                        Console.WriteLine($"mean q {m}");
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

                //Console.WriteLine($"{list[even].GetName()} tcurr { tcurr}");
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
