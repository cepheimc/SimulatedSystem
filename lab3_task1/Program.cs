using System;
using System.Collections.Generic;
using lab3;

namespace lab3_task1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] delay = { 0.5, 0.6, 0.6 };
            double[] maxS = {1.0};

            Creator1 c = new Creator1(delay[0]);
            Process1 p11 = new Process1(delay[1], (int)maxS[0]);
            Process1 p12 = new Process1(delay[2], (int)maxS[0]);
            

            c.SetName("Creator");
            p11.SetName("Process11");
            p12.SetName("Process12");
            
            p11.SetMaxQueue(3);
            p12.SetMaxQueue(3);
            
            c.SetGenerator(1);
            p11.SetGenerator(1);
            p12.SetGenerator(1);

            Dictionary<Process1, double> p1 = new Dictionary<Process1, double>();
            p1.Add(p11, 0.5);
            p1.Add(p12, 0.5);
            p11.otherProcess = p12;
            p12.otherProcess = p11;

            c.SetNextElement(p1);
            p11.queue = 2;
            p12.queue = 2;
            c.SetTimeNext(0.1);

            List<Element> list = new List<Element>();
            list.Add(c);
            list.Add(p11);
            list.Add(p12);
            
            Model1 m = new Model1(list);
            m.Simulation(100000.0);
            int ii = 0;
            int totalQuantity = 0, totalFailure = 0;
            double avgInBank = 0;

            foreach (Element e in list)
            {
                if (e.GetType() == typeof(Process1))
                {
                    Process1 p = (Process1)e;
                    string outputinfo = ($"name = {e.GetName()}  mean queue = {m.meanq[ii]:f4}   " +
                                         $"workload = {m.workload[ii]:f4}  wait = {m.wait[ii]:f4}  " +
                                         $" avg time out from window {m.timeOutWindow[ii]:f4} ");
                    totalQuantity += p.quantity;
                   // Console.WriteLine($"totalq {totalQuantity}");
                    totalFailure += p.failure;
                   // Console.WriteLine($"totalf {totalFailure}");
                    avgInBank += (m.meanq[ii] + m.workload[ii]);
                    Console.WriteLine(outputinfo);
                    Console.WriteLine($"change queue {p.changeQueue}\n");
                }
                else
                {
                    string outputinfo = ($"name = {e.GetName()}  quantity = {e.quantity}");
                    Console.WriteLine(outputinfo);
                }

                ii++;
            }

           // Console.WriteLine($"Avg time out from window {m.timeOutWindow}");
            Console.WriteLine($"Avg numbers of clients {avgInBank}");
            Console.WriteLine($"Failure {(double) totalFailure / (totalQuantity + totalFailure) * 100} %");

            Console.ReadKey();
        }
    }
}
