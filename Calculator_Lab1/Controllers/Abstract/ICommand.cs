

namespace Calculator_Lab1
{
    public interface ICommand
    {
        double Number { get; set; }
        double PreviousNumber { get; set; }
        void Execute();
        void Undo();
    }
}
