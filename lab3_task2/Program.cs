using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3;

namespace lab3_task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Creator2 c = new Creator2(15);
            c.SetName("Creator");
            c.SetGenerator(1);

            Doctor d = new Doctor(0,2);
            d.SetName("Doctors");
            d.SetGenerator(1);

            Assistant a = new Assistant(4,2);
            a.SetDelayDev(2);
            a.SetDelayDev(2);
            a.SetName("Lab");
            a.SetGenerator(4);

            Process2 p1 = new Process2(3,3);
            p1.SetDelayDev(8);
            p1.SetGenerator(3);
            p1.SetName("To palata");

            Process2 p2 = new Process2(4.5,1);
            p2.SetDelayDev(3);
            p2.SetName("Registration to lab");
            p2.SetGenerator(4);

            c.SetNextElement(d);
            List<Element> p = new List<Element>();
            p.Add(p1);
            p.Add(p2);

            d.SetNextElement(p);
            p2.SetNextElement(a);
            a.SetNextElement(d);

            List<Element> list = new List<Element>();
            list.Add(c);
            list.Add(d);
            list.Add(p1);
            list.Add(p2);
            list.Add(a);

            Model3 m = new Model3(list);
            m.Simulation(144.0);

            int ii = 0;

            foreach (Element e in list)
            {
                if (e.GetType() == typeof(Process2))
                {
                    Process2 pp = (Process2)e;
                    string outputinfo = ($"name = {e.GetName()}  mean queue = {m.meanq[ii]:f8}   " +
                                         $"workload = {m.workload[ii]:f8}  wait = {m.wait[ii]:f8}   failure = {pp.failure}" +
                                         $"    quantity = {e.quantity}");
                    Console.WriteLine(outputinfo);
                    
                }
                else
                {
                    string outputinfo = ($"name = {e.GetName()}  quantity = {e.quantity}");
                    Console.WriteLine(outputinfo);
                }

                ii++;
            }

            Console.ReadKey();
        }
    }
}
