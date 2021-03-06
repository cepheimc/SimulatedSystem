﻿using System;
using System.Collections.Generic;

namespace lab3_task2
{
    public class Element
    {
        private double tnext,
            tcurr,
            delayMean,
            delayDev;

        private static int nextId;
        
        private Generator g;

       public int quantity,
            id,
            generator;

        private string name;

        public Dictionary<int, Element> nextElem;

        public Element()
        {
            tcurr = tnext = 0.0;
            delayMean = 1.0;
           // state = 0;
            nextElem = null;
            id = nextId;
            nextId++;
        }

        public Element(double daley)
        {
            tcurr = tnext = 0.0;
            nextElem = new Dictionary<int, Element>();
            id = nextId;
            nextId++;
            delayMean = daley;
            quantity = 0;
            var seed = this.GetHashCode() * (int)(DateTime.Now.ToFileTime());

            int i = 0;
            //for (int k = 0; k < 1000000; k++){i++;}
            g = new Generator(seed);
        }

        public double GetDelay()
        {
            double d = GetDelayMean();

            if (generator == 1)
                d = g.Expon(delayMean);
            if (generator == 2)
                d = g.Normal(GetDelayMean(), GetDelayDev());
            if (generator == 3)
                d = g.Uniform(GetDelayMean(), GetDelayDev());
            if (generator == 4)
                d = g.Erlang(GetDelayMean(), GetDelayDev());

            return d;
        }

        public void SetDelayDev(double delay)
        {
            delayDev = delay;
        }

        public double GetDelayDev()
        {
            return delayDev;
        }

        public void SetDelayMean(double delay)
        {
            delayMean = delay;
        }

        public double GetDelayMean()
        {
            return delayMean;
        }

        public virtual void OutAct()
        {
            quantity++;

        }

        public virtual void InAct()
        {

        }

        public virtual void InAct(int type)
        {

        }

        public int GetQuantity()
        {
            return quantity;
        }

        public void SetTimeCurr(double t)
        {
            tcurr = t;
        }

        public double GetTimeCurr()
        {
            return tcurr;
        }

       /* public void SetState(int s)
        {
            state = s;
        }*/

       /* public int GetState()
        {
            return state;
        }*/

        public void SetNextElement(Element e, int type)
        {
           // Console.WriteLine($"this: {GetName()} id : {GetId()} next: {e.GetName()} id {e.GetId()}");
            nextElem.Add(type, e);
        }

        public void PickNext()
        {

        }

        public void SetTimeNext(double t)
        {
            tnext = t;
        }

        public virtual double GetTimeNext()
        {
            return tnext;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }

        public void SetGenerator(int g)
        {
            generator = g;
        }

        public void WriteResult()
        {
            Console.WriteLine($"{name} quantity: {quantity}");
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string n)
        {
            name = n;
        }

        public virtual double Workload(double delta)
        {
            return 0.0;
        }

        public virtual double MeanQueue(double delta)
        {
            return 0.0;
        }
    }

    
}
