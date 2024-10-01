using System;

public struct CompNum //структура комплексных чисел
{
    private double Real; //вещественная часть
    private double Imaginary; //мнимая часть
    public CompNum(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static CompNum operator +(CompNum a, CompNum b) //сложение
    {
        return new CompNum(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }
    public static CompNum operator -(CompNum a, CompNum b) //вычитание
    {
        return new CompNum(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }
    public static CompNum operator *(CompNum a, CompNum b) //умножение
    {
        return new CompNum(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
    }
    public static CompNum operator /(CompNum a, CompNum b) //деление
    {
        double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
        return new CompNum((a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator, (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator);
    }
    public double Modul() //модуль
    {
        return Math.Sqrt(Real * Real + Imaginary * Imaginary);
    }
    public double Argument() //аргумент
    {
        if (Real > 0) return Math.Atan(Imaginary / Real);
        if ((Real < 0) && (Imaginary >= 0)) return Math.PI + Math.Atan(Imaginary / Real);
        if ((Real < 0) && (Imaginary < 0)) return -Math.PI + Math.Atan(Imaginary / Real);
        if ((Real == 0) && (Imaginary > 0)) return Math.PI / 2;
        if ((Real == 0) && (Imaginary < 0)) return -Math.PI / 2;
        return 0;
    }
    public override string ToString() //строковое представление комплексного числа
    {
        return $"{Real} + {Imaginary}i";
    }
}

class Program 
{
    static void Main()
    {
        CompNum number = new CompNum(0, 0);
        while (true) //цикл для взаимодествия с пользователем
        {
            //информация для пользователя
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Создать комплексное число");
            Console.WriteLine("2 - Сложение");
            Console.WriteLine("3 - Вычитание");
            Console.WriteLine("4 - Умножение");
            Console.WriteLine("5 - Деление");
            Console.WriteLine("6 - Нахождение модуля");
            Console.WriteLine("7 - Нахождение аргумента");
            Console.WriteLine("8 - Вывести комплексное число");
            Console.WriteLine("Q или q - Выход");

            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (choice)
            {
                case '1':
                    Console.Write("Введите вещественную часть: ");
                    double real = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть: ");
                    double imaginary = Convert.ToDouble(Console.ReadLine());
                    number = new CompNum(real, imaginary);
                    break;

                case '2':
                    Console.Write("Введите вещественную часть второго числа: ");
                    real = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть второго числа: ");
                    imaginary = Convert.ToDouble(Console.ReadLine());
                    number += new CompNum(real, imaginary);
                    break;

                case '3':
                    Console.Write("Введите вещественную часть второго числа: ");
                    real = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть второго числа: ");
                    imaginary = Convert.ToDouble(Console.ReadLine());
                    number -= new CompNum(real, imaginary);
                    break;

                case '4':
                    Console.Write("Введите вещественную часть второго числа: ");
                    real = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть второго числа: ");
                    imaginary = Convert.ToDouble(Console.ReadLine());
                    number *= new CompNum(real, imaginary);
                    break;

                case '5':
                    Console.Write("Введите вещественную часть второго числа: ");
                    real = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть второго числа: ");
                    imaginary = Convert.ToDouble(Console.ReadLine());
                    number /= new CompNum(real, imaginary);
                    break;

                case '6':
                    Console.WriteLine($"Модуль: {number.Modul()}");
                    break;

                case '7':
                    Console.WriteLine($"Аргумент: {number.Argument()}");
                    break;

                case '8':
                    Console.WriteLine($"Комплексное число: {number}");
                    break;

                case 'Q':
                case 'q':
                    return;

                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
    }
}
