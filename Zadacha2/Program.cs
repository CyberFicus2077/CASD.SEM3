using System;
struct Complex
{
    public double Re;
    public double Im;

    public Complex(double re, double im)
    {
        Re = re;
        Im = im;
    }

    public static Complex Add(Complex a, Complex b)
    {
        return new Complex(a.Re + b.Re, a.Im + b.Im);
    }

    public static Complex Sub(Complex a, Complex b)
    {
        return new Complex(a.Re - b.Re, a.Im - b.Im);
    }

    public static Complex Mult(Complex a, Complex b)
    {
        return new Complex(
            a.Re * b.Re - a.Im * b.Im,
            a.Re * b.Im + a.Im * b.Re
        );
    }

    public static Complex Dive(Complex a, Complex b)
    {
        double den = b.Re * b.Re + b.Im * b.Im;
        if (den == 0)
            throw new DivideByZeroException("Делить на ноль нельязя ;)");

        return new Complex(
            (a.Re * b.Re + a.Im * b.Im) / den,
            (a.Im * b.Re - a.Re * b.Im) / den
        );
    }

    public double Magnitude()
    {
        return Math.Sqrt(Re * Re + Im * Im);
    }

    public double Arg()
    {
        return Math.Atan2(Im, Re);
    }

    public void Print()
    {
        if (Im >= 0)
            Console.WriteLine($"{Re:F3} + {Im:F3}i");
        else
            Console.WriteLine($"{Re:F3} - {-Im:F3}i");
    }
}

class Program
{
    static Complex current = new Complex(0, 0); 

    static void Main()
    {
        Console.WriteLine("Калькулятор комплексных чисел");
        Console.WriteLine("Текущее число: 0");

        while (true)
        {
            PrintMenu();
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (char.ToLower(choice))
            {
                case '1': 
                    InputNum();
                    break;
                case '2': 
                    PerformOperation('+');
                    break;
                case '3':
                    PerformOperation('-');
                    break;
                case '4': 
                    PerformOperation('*');
                    break;
                case '5':
                    PerformOperation('/');
                    break;
                case '6': 
                    Console.WriteLine($"Модуль: {current.Magnitude():F3}");
                    break;
                case '7': 
                    Console.WriteLine($"Аргумент: {current.Arg():F3} радиан");
                    break;
                case '8': 
                    Console.WriteLine($"Вещественная часть: {current.Re:F3}");
                    break;
                case '9':
                    Console.WriteLine($"Мнимая часть: {current.Im:F3}");
                    break;
                case 'p': 
                    Console.Write("Текущее число: ");
                    current.Print();
                    break;
                case 'q':
                    Console.WriteLine("Выход из программы");
                    return;
                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("Меню:");
        Console.Write("Выберите действие: ");
        Console.WriteLine("1) Ввод нового числа");
        Console.WriteLine("2) +");
        Console.WriteLine("3) -");
        Console.WriteLine("4) *");
        Console.WriteLine("5) :");
        Console.WriteLine("6) | |");
        Console.WriteLine("7) Аргумент");
        Console.WriteLine("8) Вещественная часть");
        Console.WriteLine("9) Мнимая часть");
        Console.WriteLine("p) Вывод текущего числа");
        Console.WriteLine("Q) Выход");
        
    }

    static void InputNum()
    {
        Console.Write("Введите вещественную часть: ");
        double rea = double.Parse(Console.ReadLine());

        Console.Write("Введите мнимую часть: ");
        double imag= double.Parse(Console.ReadLine());

        current = new Complex(rea, imag);
        Console.Write("Новое число: ");
        current.Print();
    }

    static void PerformOperation(char operation)
    {
        Console.Write("Введите вещественную часть второго числа: ");
        double real = double.Parse(Console.ReadLine());

        Console.Write("Введите мнимую часть второго числа: ");
        double imaginary = double.Parse(Console.ReadLine());

        Complex second = new Complex(real, imaginary);

        try
        {
            switch (operation)
            {
                case '+':
                    current = Complex.Add(current, second);
                    break;
                case '-':
                    current = Complex.Sub(current, second);
                    break;
                case '*':
                    current = Complex.Mult(current, second);
                    break;
                case '/':
                    current = Complex.Dive(current, second);
                    break;
            }

            Console.Write("Результат: ");
            current.Print();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}