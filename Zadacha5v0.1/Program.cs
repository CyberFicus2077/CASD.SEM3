using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Главное меню:");
            Console.WriteLine("1. Задача с Кучами (Задача 5)");
            Console.WriteLine("2. Задача с Очередью (Задача 6)");
            Console.WriteLine("3. Задача с Заявками (Задача 7)");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Program5.Run();
                    break;
                case "2":
                    Program6.Run();
                    break;
                case "3":
                    Program7.Run();
                    break;
                case "0":
                    Console.WriteLine("Выход из программы.");
                    return;
                default:
                    Console.WriteLine("Неверный ввод. Нажмите Enter, чтобы попробовать снова.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}