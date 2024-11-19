using System;
using stek;
using MyVector;
using System.Collections.Generic;
using System.Numerics;
namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите математическое выражение: ");
            string primer = Console.ReadLine();
            Console.WriteLine(Calc(primer));
        }

        static private double InitOperaci(string operation, params double[] x) => operation switch
        {
            "+" => x[0] + x[1],
            "-" => x[0] - x[1],
            "*" => x[0] * x[1],
            "/" => x[0] / x[1],
            "^" => Math.Pow(x[0], x[1]),
            "koren" => Math.Sqrt(x[0]),
            "abs" => Math.Abs(x[0]),
            "sin" => Math.Sin(x[0]),
            "cos" => Math.Cos(x[0]),
            "tg" => Math.Tan(x[0]),
            "ln" => Math.Log(x[0]),
            "log" => Math.Log10(x[0]),
            "min" => x[0] < x[1] ? x[0] : x[1],
            "max" => x[0] > x[1] ? x[0] : x[1],
            "mod" => (int)x[1] % (int)x[0],
            "//" => Math.Floor(x[0] / x[1]),
            "exp" => Math.Exp(x[0]),
            "trunc" => Math.Truncate(x[0]),
            _ => throw new Exception("Неизвестная операция")
        };

        static public int Prioritet(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "//":
                    return 2;
                case "^":
                    return 3;
                case "koren":
                case "ln":
                case "cos":
                case "sin":
                case "tg":
                case "abs":
                case "log":
                case "min":
                case "max":
                case "mod":
                case "exp":
                case "trunc":
                    return 4;
                default: return 0;
            }
        }

        static private string Tmp(string expression)
        {
            MyVector<string> vectorOfItem = new MyVector<string>();
            for (int i = 0; i < expression.Length; i++)
            {
                string item = "";
                while (i < expression.Length && Char.IsLetter(expression[i]))
                {
                    item += expression[i];
                    i++;
                }
                if (item.Length > 0)
                {
                    switch (item)
                    {
                        case "koren":
                        case "ln":
                        case "cos":
                        case "sin":
                        case "tg":
                        case "abs":
                        case "log":
                        case "min":
                        case "max":
                        case "mod":
                        case "exp":
                        case "trunc":
                            break;
                        default:
                            vectorOfItem.Add(item);
                            break;
                    }
                }
            }

            for (int i = 0; i < vectorOfItem.Size(); i++)
            {
                Console.WriteLine($"Введите значение {vectorOfItem.Get(i)}: ");
                expression = expression.Replace(vectorOfItem.Get(i), Console.ReadLine());
            }
            return expression;
        }

        static public MyVector<string> PolskNotacia(string expression)
        {
            expression = Tmp(expression);
            MyStack<string> stack = new MyStack<string>();
            MyVector<string> result = new MyVector<string>();

            char[] basicOperations = new char[] { '+', '-', '*', '^', '/' };

            for (int i = 0; i < expression.Length; i++)
            {
                string number = "";
                if ((result.IsEmpty() && expression[i] == '-') || (i > 0 && expression[i] == '-' && expression[i - 1] == '('))
                {
                    number += expression[i];
                    i++;
                }
                while (i < expression.Length && (Char.IsDigit(expression[i]) || expression[i] == '.'))
                {
                    number += expression[i];
                    i++;
                }
                if (number.Length > 0) { result.Add(number); }

                string func = "";
                while (i < expression.Length && Char.IsLetter(expression[i]))
                {
                    func += expression[i];
                    i++;
                }
                if (func.Length > 0 && func == "pi") { result.Add(Math.PI.ToString()); }

                else if (func.Length > 0) { stack.Push(func); }

                if (i < expression.Length && basicOperations.Contains(expression[i]))
                {
                    if (stack.Empty()) { stack.Push(expression[i].ToString()); }
                    else
                    {
                        while (!stack.Empty() && (Prioritet(stack.Peek()) > Prioritet(expression[i].ToString())))
                        {
                            string b = stack.Peek();
                            result.Add(b.ToString());
                            stack.Pop();
                        }
                        stack.Push(expression[i].ToString());
                    }
                }
                else if (i < expression.Length && expression[i] == '(') stack.Push(expression[i].ToString());
                else if (i < expression.Length && expression[i] == ')')
                {
                    while (!stack.Empty())
                    {
                        string b = stack.Peek();
                        if (b == "(")
                        {
                            stack.Pop();
                            break;
                        }
                        result.Add(b.ToString());
                        stack.Pop();
                    }
                }
            }
            while (!stack.Empty())
            {
                string b = stack.Peek();
                if (b != "(") result.Add(b.ToString());
                if (b == ")") throw new Exception("Количество скобок не совпадает");
                stack.Pop();
            }

            return result;
        }

        static public double Calc(string expression)
        {
            if (expression == null) throw new Exception("Пусто");
            MyVector<string> postfixForm = PolskNotacia(expression);
            MyStack<double> stack = new MyStack<double>();

            char[] basicOperator = new char[] { '+', '-', '*', '^', '/' };

            for (int i = 0; i < postfixForm.Size(); i++)
            {
                string element = postfixForm.Get(i);
                if (Char.IsDigit(element[0]) || (element.Length > 1 && Char.IsDigit(element[1])))
                {
                    element = element.Replace('.', ',');
                    double number = Convert.ToDouble(element);
                    stack.Push(number);
                }
                else if (Char.IsLetter(element[0]))
                {
                    if (element == "max" || element == "min" || element == "mod")
                    {
                        double number1 = stack.Peek();
                        stack.Pop();
                        double number2 = stack.Peek();
                        stack.Pop();
                        stack.Push(InitOperaci(element, number1, number2));
                    }
                    else
                    {
                        double number = stack.Peek();
                        stack.Pop();
                        stack.Push(InitOperaci(element, number));
                    }
                }
                else if (basicOperator.Contains(element[0]))
                {
                    if (i < postfixForm.Size() - 1 && element == "/" && postfixForm.Get(i + 1) == "/")
                    {
                        i++;
                        element += postfixForm.Get(i);
                    }
                    double number2 = stack.Peek();
                    stack.Pop();
                    double number1 = stack.Peek();
                    stack.Pop();
                    stack.Push(InitOperaci(element, number1, number2));
                }
            }
            double answer = stack.Peek();
            stack.Pop();
            return answer;
        }
    }
}
