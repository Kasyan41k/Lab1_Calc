
namespace Calculator_Lab1
{
    public class PlusCommand : ICommand
    {
        private ICalculator calculator;
        public double Number { get; set; }
        public double PreviousNumber { get; set; }

        public PlusCommand(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public void Execute()
        {
            PreviousNumber = calculator.CurrentNumber;
            calculator.Plus(Number);
        }

        public void Undo()
        {
            calculator.CurrentNumber = PreviousNumber;
        }
    }
}
