using System;

namespace lab3_task2
{
    class Generator
    {
        private Random r;

        public Generator(int seed)
        {
            r = new Random(seed);
        }
        public double Expon(double t)
        {
            double x = r.NextDouble();
            return -t * Math.Log(x);
        }

        public double Normal(double t1, double t2)
        {

            double sum = 0, x;
            for (int j = 0; j < 12; j++)
            {
                x = r.NextDouble();
                sum += x;
            }

            return t1 + t2 * (sum - 6);
        }

        public double Uniform(double t1, double t2)
        {
            double x = new Random().Next(0, 100);

            return ((t1 * x) % t2) / t2;
        }

        public double Erlang(double t1, double t2)
        {
            double x = new Random().NextDouble();
            double lambda = t1 / t2;
            return -(1 / lambda) * Math.Log(x);
        }
    }
}
