
namespace lab3
{
    public class Creator : Element
    {
        public Creator(double delay) : base(delay)
        {

        }


        public override void OutAct()
        {
            base.OutAct();
            // Console.WriteLine($"name = {GetName()}     state - {state}");
            var delay = GetDelay();
            SetTimeNext(GetTimeCurr() + delay);
            GetNextElement().InAct();
        }

        public override double Workload(double delta)
        {
            return 0.0;

        }

        public override double MeanQueue(double delta)
        {
            return 0.0;

        }
    }
}
