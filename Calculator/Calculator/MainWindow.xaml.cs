using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber;
        double newNumber;
        double storedNumber;
        BasicOperations @operator;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            pointButton.Click += PointButton_Click;
            equalsButton.Click += EqualsButton_Click;
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            double result = CalculateLogic(@operator);

            resultLabel.Content = $"{result}";
            lastNumber = result;
            newNumber = 0;
            storedNumber = 0;
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains(","))
                resultLabel.Content = $"{resultLabel.Content},";
        }

        private void NumberButtons_Click(object sender, RoutedEventArgs e)
        {
            newNumber = double.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() != "0")
                lastNumber = double.Parse($"{resultLabel.Content}{newNumber}");
            else
                lastNumber = newNumber;

            resultLabel.Content = $"{lastNumber}";
        }

        private void OperationButtons_Click(object sender, RoutedEventArgs e)
        {
            var buttonContent = (sender as Button).Content.ToString();
            @operator = SetOperator(buttonContent);

            resultLabel.Content = "0";
            storedNumber = lastNumber;
        }

        private double CalculateLogic(BasicOperations @operator)
        {
            switch (@operator)
            {
                case BasicOperations.Adition: return MathBasicOperations.Sum(storedNumber, lastNumber);
                case BasicOperations.Substraction: return MathBasicOperations.Minus(storedNumber, lastNumber);
                case BasicOperations.Division: return MathBasicOperations.Divide(storedNumber, lastNumber);
                case BasicOperations.Multiplier: return MathBasicOperations.Multiply(storedNumber, lastNumber);
                case BasicOperations.NotSetted: return 0;
                default: return 0;
            }
        }

        private BasicOperations SetOperator(string buttonContent)
        {
            switch (buttonContent)
            {
                case "+": return BasicOperations.Adition;
                case "-": return BasicOperations.Substraction;
                case "*": return BasicOperations.Multiplier;
                case "/": return BasicOperations.Division;
                default: return BasicOperations.NotSetted;
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double temporaryNumber = lastNumber;

            if (lastNumber != 0)
            {
                temporaryNumber /= 100;

                if (lastNumber != 0)
                    lastNumber = temporaryNumber * storedNumber;
            }

            resultLabel.Content = $"{lastNumber}";
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastNumber != 0)
                lastNumber *= -1;

            resultLabel.Content = lastNumber;
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }
    }

    public enum BasicOperations
    {
        Adition,
        Substraction,
        Division,
        Multiplier,
        NotSetted
    }

    public class MathBasicOperations
    {
        public static double Sum(double numberOne, double numberTwo) => numberOne + numberTwo;
        public static double Minus(double numberOne, double numberTwo) => numberOne - numberTwo;
        public static double Multiply(double numberOne, double numberTwo) => numberOne * numberTwo;
        public static double Divide(double numberOne, double numberTwo) => numberOne / numberTwo;
    }
}