using System;
using System.Collections.Generic;

namespace lab3_task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Creator2 c = new Creator2(15);
            c.SetName("Creator");
            c.SetGenerator(1);

            Process2 typeOne = new Process2(15, 1);
            typeOne.SetName("Admin1");
            typeOne.SetGenerator(1);

            Process2 typeTwo = new Process2(40, 1);
            typeTwo.SetName("Admin2");
            typeTwo.SetGenerator(1);

            Process2 typeThree = new Process2(30, 1);
            typeThree.SetName("Admin3");
            typeThree.SetGenerator(1); 

            Process2 toPalats = new Process2(3, 3);
            toPalats.SetDelayDev(8);
            toPalats.SetGenerator(3);
            toPalats.SetName("To palatas");

            Process2 toLabs = new Process2(2, 1);
            toLabs.SetDelayDev(5);
            toLabs.SetGenerator(3);
            toLabs.SetName("To labs");

            Process2 toAdmin = new Process2(2, 1);
            toAdmin.SetDelayDev(5);
            toAdmin.SetGenerator(3);
            toAdmin.SetName("To admin");

            Process2 regToLab = new Process2(4.5, 1);
            regToLab.SetDelayDev(3);
            regToLab.SetName("Registration to lab");
            regToLab.SetGenerator(4);

            Process2 labAnalysis = new Process2(4, 2);
            labAnalysis.SetDelayDev(2);
            labAnalysis.SetName("In lab");
            labAnalysis.SetGenerator(4);


            c.SetNextElement(typeOne, 1);
            c.SetNextElement(typeTwo, 2);
            c.SetNextElement(typeThree, 3);
            
            typeOne.SetNextElement(toPalats, 1);
            typeTwo.SetNextElement(toLabs, 2);
            typeThree.SetNextElement(toLabs, 3);

            toLabs.SetNextElement(regToLab, 2);
            toLabs.SetNextElement(regToLab, 3);

            regToLab.SetNextElement(labAnalysis, 2);
            regToLab.SetNextElement(labAnalysis, 3);

            labAnalysis.SetNextElement(toAdmin, 2);

            toAdmin.SetNextElement(typeOne, 1);
            
            List<Element> list = new List<Element>();
            list.Add(c);
            list.Add(typeOne);
            list.Add(typeTwo);
            list.Add(typeThree);
            list.Add(toPalats);
            list.Add(toLabs);
            list.Add(toAdmin);
            list.Add(regToLab);
            list.Add(labAnalysis);

            Model3 m = new Model3(list);
            m.Simulation(1440.0);

            Console.WriteLine($"Mean time for type 1: {m.wait[0] + typeOne.MeanWork() + toPalats.MeanWork() + m.wait[3]}");
            Console.WriteLine($"Mean time for type 2: {m.wait[0] + m.wait[1] + m.wait[3] + m.wait[4] + m.wait[5] + m.wait[6] + m.wait[7] + typeOne.MeanWork() + toPalats.MeanWork()+ typeTwo.MeanWork() + toLabs.MeanWork() + regToLab.MeanWork() + labAnalysis.MeanWork() + toAdmin.MeanWork()}");
            Console.WriteLine($"Mean time for type 3: {m.wait[2] + m.wait[4] + m.wait[6] + m.wait[7] + typeThree.MeanWork() + toLabs.MeanWork() + regToLab.MeanWork() + labAnalysis.MeanWork()}");

            Console.ReadKey();
        }
    }
}
