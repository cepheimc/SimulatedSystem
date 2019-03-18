using System;
using System.Collections.Generic;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            double[] delay = { 2.0, 0.6, 0.3, 0.4, 0.1 };
            double[] maxS = { 1.0, 1.0, 1.0, 2.0 };
           // double[] maxQ = { 2.0, 3.0, 2.0, 2.0 };
            double[] diff1 = new double[10];
            double[] diff2 = new double[10];
            double[] meanQ = {0.0, 1.786, 0.003, 0.004, 0.00001};
            double[] work = {0.0, 0.714, 0.054, 0.062, 0.036};


            Creator c = new Creator(delay[0]);
            //Console.ReadKey();
            Process p1 = new Process(delay[1], (int)maxS[0]);
            Process p12 = new Process(delay[2], (int)maxS[1]);
            Process p13 = new Process(delay[3], (int)maxS[2]);
            Process p14 = new Process(delay[4], (int)maxS[3]);


            c.SetName("Creator");
            p1.SetName("Process1");
            p12.SetName("Process12");
            p13.SetName("Process13");
            p14.SetName("Process14");

           // p1.SetMaxQueue((int)maxQ[0]);
            //p21.SetMaxQueue((int)maxQ[1]);
           // p22.SetMaxQueue((int)maxQ[2]);
           // p3.SetMaxQueue((int)maxQ[3]);

            c.SetGenerator(1);
            p1.SetGenerator(1);
            p12.SetGenerator(1);
            p13.SetGenerator(1);
            p14.SetGenerator(1);

            Dictionary<Element, double> p2 = new Dictionary<Element, double>();
            p2.Add(p12, 0.15);
            p2.Add(p13, 0.13);
            p2.Add(p14, 0.3);

            c.SetNextElement(p1);
            p1.SetNextElement(p2);
            p12.SetNextElement(p1);
            p13.SetNextElement(p1);
            p14.SetNextElement(p1);

            List<Element> list = new List<Element>();
            list.Add(c);
            list.Add(p1);
            list.Add(p12);
            list.Add(p13);
            list.Add(p14);

           Model m = new Model(list);
            m.Simulation(100000.0);
            int ii = 0;
           
            foreach (Element e in list)
            {
                if (e.GetType() == typeof(Process))
                {
                    
                    Process p = (Process) e;
                    double mq = m.meanq[ii];
                    double w = m.workload[ii];
                    string outputinfo = ($"name = {e.GetName()}  mean queue = {mq:f4}   " +
                                         $"workload = {w:f4}  wait = {m.wait[ii]:f4}   failure = {p.failure}" +
                                         $"    quantity = {e.quantity}");

                    diff1[ii] = (Math.Abs(meanQ[ii] - mq) / meanQ[ii]) * 100; 
                    diff2[ii] = (Math.Abs(work[ii] - w) / work[ii]) * 100;
                    
                    Console.WriteLine(outputinfo);
                    Console.WriteLine($"diff mean queue = {diff1[ii]} %");
                    Console.WriteLine($"diff workload = {diff2[ii]} %\n");
                }
                else
                {
                    
                    string outputinfo = ($"name = {e.GetName()}  quantity = {e.quantity}");
                    Console.WriteLine(outputinfo);
                }
                ii++;

            }
                                                                                                                      //diff1 = diff1 / 100; diff1 = diff1 + 1;

           // Console.WriteLine($"diff mean queue = {diff1} %");
           // Console.WriteLine($"diff workload = {diff2} %");

            Console.ReadKey();
        }
    }
}
