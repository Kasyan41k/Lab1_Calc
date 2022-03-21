using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator_Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CalculatorClient calculatorClient;
        private string action = "";

        public MainWindow()
        {
            InitializeComponent();
            calculatorClient = new CalculatorClient(new Calculator());
            calculatorClient.OperationChangedEvent += UpdateProblemField;
        }

        private void BackOperationButton_Click(object sender, RoutedEventArgs e)
        {
            calculatorClient.UndoCommand();

            labelResult.Content = calculatorClient.GetCurrentNumber();
        }

        private void NumberButtons_Click(object sender, RoutedEventArgs e)
        {
            string? button = ((Button)sender).Content.ToString();

            if (button != null) AddSymbolToTextBox(button);
        }

        private void AddSymbolToTextBox(string symbol)
        {
            int inputField = Double.MaxValue.ToString().Length;

            if (textBoxInput.Text.Length == inputField)
                return;

            if (textBoxInput.Text.Length == inputField - 1 && symbol == ".")
                return;

            if (symbol == "." && textBoxInput.Text.IndexOf(".") > 0)
                return;

            textBoxInput.Text += symbol;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            action = "";
            textBoxInput.Text = "";
            labelResult.Content = 0;

            calculatorClient.Clear();
        }

        private void DeleteSymbolButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxInput.Text.Length == 0)
                return;

            textBoxInput.Text = textBoxInput.Text.Substring(0, textBoxInput.Text.Length - 1);
        }

        private void OperationButtons_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            action = (string)button.Content;

            calculatorClient.AddOperation(textBoxInput.Text, action);
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            action = "";
            calculatorClient.CalcResult(textBoxInput.Text);  
        }

        private void UpdateProblemField()
        {
            labelResult.Content = calculatorClient.GetCurrentNumber() + " " + action;
            textBoxInput.Text = "";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ResultButton_Click(sender, e);
            }

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                AddSymbolToTextBox(e.Key.ToString().Remove(0, 1));
            }

        }
    }
}
