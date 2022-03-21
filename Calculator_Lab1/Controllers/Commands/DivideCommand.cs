

namespace Calculator_Lab1
{
    public class DivideCommand : ICommand
    {
        private ICalculator calculator;
        public double Number { get; set; }
        public double PreviousNumber { get; set; }

        public DivideCommand(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public void Execute()
        {
            PreviousNumber = calculator.CurrentNumber;
            calculator.Divide(Number);
        }

        public void Undo()
        {
            calculator.CurrentNumber = PreviousNumber;
        }
    }
}
