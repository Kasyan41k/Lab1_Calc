

namespace Calculator_Lab1
{
    public class SubstructCommand : ICommand
    {
        private ICalculator calculator;
        public double Number { get; set; }
        public double PreviousNumber { get; set; }

        public SubstructCommand(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public void Execute()
        {
            PreviousNumber = calculator.CurrentNumber;
            calculator.Substruct(Number);
        }

        public void Undo()
        {
            calculator.CurrentNumber = PreviousNumber;
        }
    }
}
