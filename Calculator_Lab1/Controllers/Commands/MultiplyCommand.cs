
namespace Calculator_Lab1
{
    public class MultiplyCommand : ICommand
    {
        private ICalculator calculator;
        public double Number { get; set; }
        public double PreviousNumber { get; set; }

        public MultiplyCommand(ICalculator calculator)
        {
            this.calculator = calculator;
        }
        public void Execute()
        {
            PreviousNumber = calculator.CurrentNumber;
            calculator.Multiply(Number);
        }

        public void Undo()
        {
            calculator.CurrentNumber = PreviousNumber;
        }
    }
}
