namespace Calculator.Services
{
    public interface ICalculatorLogger
    {
        void Log(string action);
        void Clear();
    }
}
