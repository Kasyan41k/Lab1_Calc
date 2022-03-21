using System;
using System.Collections.Generic;

namespace Calculator_Lab1
{
    public class CalculatorClient
    {
        private ICalculator calculator;
        private bool resultReceived = false;
        private Stack<ICommand> commandsHistory;
        public event Action OperationChangedEvent;

        public CalculatorClient(ICalculator calculator)
        {
            this.calculator = calculator;
            commandsHistory = new Stack<ICommand>();
        }

        public void UndoCommand()
        {
            if (commandsHistory.Count == 0)
                return;

            ICommand command = commandsHistory.Pop();

            command.Undo();
        }

        public void Clear()
        {
            commandsHistory.Clear();
            calculator.CurrentNumber = 0;
        }

        public void AddOperation(string number, string action)
        {
            if (number == "")
            {
                if (resultReceived)
                    resultReceived = false;
                ChangeOperation(action);
                OperationChangedEvent?.Invoke();
                return;
            }

            if (resultReceived && number != "" && GetCountOperation() > 1)
            {
                Clear();
                resultReceived = false;
            }

            ICommand command;

            if (commandsHistory.Count == 0)
            {
                calculator.CurrentNumber = Convert.ToDouble(number);
            }

            if (commandsHistory.Count >= 1)
            {
                command = commandsHistory.Peek();
                command.Number = Convert.ToDouble(number);
                command.Execute();
            }         

            command = GetOperation(action);

            commandsHistory.Push(command);

            OperationChangedEvent?.Invoke();
        }


        public void ChangeOperation(string action)
        {
            ICommand command = GetOperation(action);
            command.Number = commandsHistory.Pop().Number;
            commandsHistory.Push(command);
        }

        private ICommand GetOperation(string action)
        {
            switch (action)
            {
                case "+":
                    return new PlusCommand(calculator);
                case "-":
                    return new SubstructCommand(calculator);
                case "x":
                    return new MultiplyCommand(calculator);
                case "/":
                    return new DivideCommand(calculator);
            }

            return null;
        }

        public void CalcResult(string number)
        {
            if (commandsHistory.Count == 0)
                return;

            if (number == "")
            {
                commandsHistory.Pop();
                OperationChangedEvent?.Invoke();
                return;
            }                

            ICommand command;
            command = commandsHistory.Peek();
            command.Number = Convert.ToDouble(number);
            command.Execute();

            resultReceived = true;

            OperationChangedEvent?.Invoke();
        }


        public int GetCountOperation()
        {
            return commandsHistory.Count;
        }

        public double GetCurrentNumber()
        {
            return calculator.CurrentNumber;
        }
    }
}
