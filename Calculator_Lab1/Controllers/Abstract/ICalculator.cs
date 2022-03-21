
namespace Calculator_Lab1
{
    public interface ICalculator
    {
        double CurrentNumber { get; set; }
        void Plus(double number);
        void Divide(double number);
        void Multiply(double number);
        void Substruct(double number);
    }
}
