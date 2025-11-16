using System;

public static class Program5
{
    static void ShowAll(Heap<int> h1, Heap<int> h2, Heap<int> h3)
    {
        Console.WriteLine("\nНА ДАННЫЙ МОМЕНТ:");
        Console.Write("Первая куча: ");
        if (h1 != null) h1.Show(); else Console.WriteLine("не создана");
        Console.Write("Вторая куча: ");
        if (h2 != null) h2.Show(); else Console.WriteLine("не создана");
        Console.Write("Третья куча: ");
        if (h3 != null) h3.Show(); else Console.WriteLine("не создана");
        Console.WriteLine("~~~~~");
    }

    public static void Run()
    {
        Heap<int> h1 = null;
        Heap<int> h2 = null;
        Heap<int> h3 = null;

        while (true)
        {
            Console.WriteLine("\nМЕТОДЫ КУЧИ:");
            Console.WriteLine("1. Создать первую кучу");
            Console.WriteLine("2. Создать вторую кучу");
            Console.WriteLine("3. Найти максимум");
            Console.WriteLine("4. Удалить максимум");
            Console.WriteLine("5. Добавить элемент");
            Console.WriteLine("6. Изменить элемент");
            Console.WriteLine("7. Объединить кучи");
            Console.WriteLine("8. Показать все кучи");
            Console.WriteLine("0. Назад в меню");
            Console.Write("Выберите: ");

            string choice = Console.ReadLine();
            if (choice == "0") break;

            try
            {
                if (choice == "1")
                {
                    Console.Write("Введите числа через пробел: ");
                    string line = Console.ReadLine();
                    string[] s = (line == null ? "" : line).Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    int[] a = new int[s.Length];
                    for (int i = 0; i < s.Length; i++) a[i] = int.Parse(s[i]);
                    h1 = new Heap<int>(a);
                    Console.WriteLine("Первая куча создана");
                    ShowAll(h1, h2, h3);
                }
                else if (choice == "2")
                {
                    Console.Write("Введите числа через пробел: ");
                    string line = Console.ReadLine();
                    string[] s = (line == null ? "" : line).Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    int[] a = new int[s.Length];
                    for (int i = 0; i < s.Length; i++) a[i] = int.Parse(s[i]);
                    h2 = new Heap<int>(a);
                    Console.WriteLine("Вторая куча создана");
                    ShowAll(h1, h2, h3);
                }
                else if (choice == "3")
                {
                    Console.Write("Какую кучу? (1/2/3): ");
                    string h = Console.ReadLine();
                    if (h == "1" && h1 != null) { Console.WriteLine("Максимум: " + h1.Peek()); ShowAll(h1, h2, h3); }
                    else if (h == "2" && h2 != null) { Console.WriteLine("Максимум: " + h2.Peek()); ShowAll(h1, h2, h3); }
                    else if (h == "3" && h3 != null) { Console.WriteLine("Максимум: " + h3.Peek()); ShowAll(h1, h2, h3); }
                    else Console.WriteLine("Куча не создана");
                }
                else if (choice == "4")
                {
                    Console.Write("Из какой кучи? (1/2/3): ");
                    string h = Console.ReadLine();
                    if (h == "1" && h1 != null) { Console.WriteLine("Удален: " + h1.RemoveRoot()); ShowAll(h1, h2, h3); }
                    else if (h == "2" && h2 != null) { Console.WriteLine("Удален: " + h2.RemoveRoot()); ShowAll(h1, h2, h3); }
                    else if (h == "3" && h3 != null) { Console.WriteLine("Удален: " + h3.RemoveRoot()); ShowAll(h1, h2, h3); }
                    else Console.WriteLine("Куча не создана");
                }
                else if (choice == "5")
                {
                    Console.Write("В какую кучу? (1/2/3): ");
                    string h = Console.ReadLine();
                    Console.Write("Какое число добавить? ");
                    int x = int.Parse(Console.ReadLine() ?? "0");
                    if (h == "1" && h1 != null) { h1.Add(x); Console.WriteLine("Добавлено"); ShowAll(h1, h2, h3); }
                    else if (h == "2" && h2 != null) { h2.Add(x); Console.WriteLine("Добавлено"); ShowAll(h1, h2, h3); }
                    else if (h == "3" && h3 != null) { h3.Add(x); Console.WriteLine("Добавлено"); ShowAll(h1, h2, h3); }
                    else Console.WriteLine("Куча не создана");
                }
                else if (choice == "6")
                {
                    Console.Write("В какой куче? (1/2/3): ");
                    string h = Console.ReadLine();
                    Console.Write("Какой индекс? ");
                    int idx = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Новое значение? ");
                    int val = int.Parse(Console.ReadLine() ?? "0");
                    if (h == "1" && h1 != null) { h1.Update(idx, val); Console.WriteLine("Обновлено"); ShowAll(h1, h2, h3); }
                    else if (h == "2" && h2 != null) { h2.Update(idx, val); Console.WriteLine("Обновлено"); ShowAll(h1, h2, h3); }
                    else if (h == "3" && h3 != null) { h3.Update(idx, val); Console.WriteLine("Обновлено"); ShowAll(h1, h2, h3); }
                    else Console.WriteLine("Куча не создана");
                }
                else if (choice == "7")
                {
                    if (h1 != null && h2 != null)
                    {
                        h3 = h1.Combine(h2);
                        Console.WriteLine("Кучи объединены");
                        ShowAll(h1, h2, h3);
                    }
                    else Console.WriteLine("Сначала создайте обе кучи");
                }
                else if (choice == "8") ShowAll(h1, h2, h3);
                else Console.WriteLine("Неизвестная команда");
            }
            catch (Exception e) { Console.WriteLine("Ошибка: " + e.Message); }

            Console.WriteLine("\nНажмите Enter чтобы продолжить...");
            Console.ReadLine();
        }
    }
}