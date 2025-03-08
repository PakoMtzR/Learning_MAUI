using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp3_Calculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _expression = "";    // Expresion matematica actual
        private string _result = "";        // Resultado calculado
        private double _memoryValue = 0;    // Valor almacenado en memoria

        public string Expression
        {
            get => _expression;
            set
            {
                if (_expression != value)
                {
                    _expression = value;
                    OnPropertyChanged();

                    // Calcula el resultado dinamicamente
                    CalculateResult();
                }
            }
        }

        public string Result
        {
            get => _result;
            set 
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged();
                }
            }
        }

        public double MemoryValue
        {
            get => _memoryValue;
            set
            {
                if (_memoryValue != value)
                {
                    _memoryValue = value;
                }
            }
        }

        public ICommand AddCharacterCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CalculateCommand { get; }
        public ICommand MemoryCommand { get; } // Comando para manejar la memoria
        public ICommand OperationCommand { get; } // Comando para operaciones especiales

        public CalculatorViewModel()
        {
            AddCharacterCommand = new Command<string>(AddCharacter);
            ClearCommand = new Command(Clear);
            DeleteCommand = new Command(Delete);
            CalculateCommand = new Command(OnCalculatePressed);
            MemoryCommand = new Command(UseMemory);
            OperationCommand = new Command<string>(PerformOperation);
        }

        private void AddCharacter(string character)
        {
            if (Expression == "0" || Result == "Error")
            {
                Expression = ""; // Reiniciar la expresión si hay un resultado previo
                Result = "0";
            }
            Expression += character;
        }

        private void Clear()
        {
            Expression = "0";
            Result = "";
        }

        private void Delete()
        {
            if (Expression.Length > 0)
                Expression = Expression.Substring(0, Expression.Length - 1);
        }

        private void CalculateResult()
        {
            try
            {
                if (!string.IsNullOrEmpty(Expression))
                {
                    // Verificar si la expresión termina con un operador
                    if (Expression.EndsWith("+") || Expression.EndsWith("-") ||
                        Expression.EndsWith("*") || Expression.EndsWith("/") ||
                        Expression.EndsWith("^") || Expression.EndsWith("%") ||
                        Expression.EndsWith("."))
                    {
                        // Si la expresión termina con un operador, no evaluar
                        Result = "";
                        return;
                    }

                    // Procesar operaciones especiales (sqrt y ^)
                    string processedExpression = ProcessSpecialOperations(Expression);

                    // Usar DataTable para evaluar la expresión
                    var dataTable = new DataTable();
                    var result = dataTable.Compute(processedExpression, null);

                    // Actualizar el resultado
                    Result = result.ToString();
                }
                else
                {
                    // Si la expresión está vacía, limpiar el resultado
                    Result = "";
                }
            }
            catch (Exception)
            {
                // Si ocurre un error (por ejemplo, división por cero), mostrar "Error"
                Result = "Error";
            }
        }

        private string ProcessSpecialOperations(string expression)
        {
            // Procesar raíz cuadrada (sqrt)
            expression = ProcessSquareRoot(expression);

            // Procesar potencia (^)
            expression = ProcessPower(expression);

            return expression;
        }

        private string ProcessSquareRoot(string expression)
        {
            // Buscar patrones de sqrt(numero)
            while (expression.Contains("sqrt("))
            {
                int startIndex = expression.IndexOf("sqrt(");
                int endIndex = expression.IndexOf(")", startIndex);

                if (startIndex >= 0 && endIndex > startIndex)
                {
                    // Extraer el número dentro de sqrt()
                    string sqrtContent = expression.Substring(startIndex + 5, endIndex - (startIndex + 5));

                    // Calcular la raíz cuadrada
                    if (double.TryParse(sqrtContent, out double number))
                    {
                        double sqrtResult = Math.Sqrt(number);
                        expression = expression.Replace($"sqrt({sqrtContent})", sqrtResult.ToString());
                    }
                    else
                    {
                        // Si no es un número válido, mostrar error
                        throw new InvalidOperationException("Invalid sqrt operation");
                    }
                }
            }

            return expression;
        }

        private string ProcessPower(string expression)
        {
            // Buscar patrones de base^exponente
            while (expression.Contains("^"))
            {
                int powerIndex = expression.IndexOf("^");

                // Encontrar la base (número antes de ^)
                int baseStartIndex = FindBaseStartIndex(expression, powerIndex);
                string baseNumber = expression.Substring(baseStartIndex, powerIndex - baseStartIndex);

                // Encontrar el exponente (número después de ^)
                int exponentEndIndex = FindExponentEndIndex(expression, powerIndex);
                string exponent = expression.Substring(powerIndex + 1, exponentEndIndex - (powerIndex + 1));

                // Calcular la potencia
                if (double.TryParse(baseNumber, out double baseValue) &&
                    double.TryParse(exponent, out double exponentValue))
                {
                    double powerResult = Math.Pow(baseValue, exponentValue);
                    expression = expression.Replace($"{baseNumber}^{exponent}", powerResult.ToString());
                }
                else
                {
                    // Si no es un número válido, mostrar error
                    throw new InvalidOperationException("Invalid power operation");
                }
            }

            return expression;
        }

        private int FindBaseStartIndex(string expression, int powerIndex)
        {
            // Buscar el inicio del número base
            for (int i = powerIndex - 1; i >= 0; i--)
            {
                if (!char.IsDigit(expression[i]) && expression[i] != '.')
                {
                    return i + 1;
                }
            }
            return 0;
        }

        private int FindExponentEndIndex(string expression, int powerIndex)
        {
            // Buscar el final del número exponente
            for (int i = powerIndex + 1; i < expression.Length; i++)
            {
                if (!char.IsDigit(expression[i]) && expression[i] != '.')
                {
                    return i;
                }
            }
            return expression.Length;
        }

        private void OnCalculatePressed()
        {
            if (!string.IsNullOrEmpty(Result) && Result != "Error")
            {
                // Reemplazar la expresión con el resultado
                Expression = Result;
                MemoryValue = Double.Parse(Result);
                // Result = ""; // Limpiar el resultado
            }
            else
            {
                Expression = "0";
                Result = "";
            }
        }

        private void UseMemory()
        {
            // Mostrar el valor almacenado en memoria
            Expression += MemoryValue.ToString();
        }

        private void PerformOperation(string operation)
        {
            try
            {
                switch (operation)
                {
                    case "%":
                        Expression += "/100"; // Convertir a porcentaje
                        break;
                    case "√":
                        Expression = $"sqrt({Expression})"; // Raíz cuadrada
                        break;
                    case "^":
                        Expression += "^"; // Potencia
                        break;
                    case "1/x":
                        // Calcular el inverso del número actual
                        if (!string.IsNullOrEmpty(Result) && double.TryParse(Result, out double number))
                        {
                            if (number != 0)
                            {
                                Expression = (1 / number).ToString();
                                Result = Expression;
                            }
                            else
                            {
                                Result = "Error"; // No se puede dividir por cero
                            }
                        }
                        else
                        {
                            Result = "Error"; // Si no es un número válido
                        }
                        break;
                }
            }
            catch (Exception)
            {
                Result = "Error";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
