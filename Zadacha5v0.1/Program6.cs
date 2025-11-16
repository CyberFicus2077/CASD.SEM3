using System;
using System.Linq;

public static class Program6
{
    static void ShowQueue(MyPriorityQueue<int> pq)
    {
        Console.WriteLine("\nТекущее состояние очереди:");
        if (pq == null)
        {
            Console.WriteLine("Очередь не создана.");
            return;
        }

        if (pq.IsEmpty)
        {
            Console.WriteLine("Очередь пуста.");
        }
        else
        {
            Console.WriteLine($"Размер: {pq.Size()}");
            Console.WriteLine("Элементы: " + string.Join(" ", pq.ToArray()));
            Console.WriteLine($"Peek: {pq.Peek()}");
            try
            {
                Console.WriteLine($"Element: {pq.Element()}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Element: очередь пуста");
            }
        }
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");
    }

    static int[] ReadIntArrayFromConsole()
    {
        Console.Write("Введите числа через пробел: ");
        string line = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(line)) return new int[0];
        return line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(int.Parse).ToArray();
    }

    public static void Run()
    {
        MyPriorityQueue<int> pq = null;

        while (true)
        {
            Console.WriteLine("\nМЕТОДЫ ОЧЕРЕДИ С ПРИОРИТЕТОМ:");
            Console.WriteLine("1. Создать пустую очередь (емкость 11)");
            Console.WriteLine("2. Создать очередь из массива");
            Console.WriteLine("3. Создать очередь с указанной емкостью");
            Console.WriteLine("4. Создать очередь с емкостью и компаратором");
            Console.WriteLine("5. Создать очередь копированием");
            Console.WriteLine("6. Добавить элемент (add)");
            Console.WriteLine("7. Добавить все элементы из массива (addAll)");
            Console.WriteLine("8. Очистить очередь (clear)");
            Console.WriteLine("9. Проверить наличие элемента (contains)");
            Console.WriteLine("10. Проверить наличие всех элементов (containsAll)");
            Console.WriteLine("11. Проверить на пустоту (isEmpty)");
            Console.WriteLine("12. Удалить указанный элемент (remove)");
            Console.WriteLine("13. Удалить все указанные элементы (removeAll)");
            Console.WriteLine("14. Оставить только указанные элементы (retainAll)");
            Console.WriteLine("15. Получить размер (size)");
            Console.WriteLine("16. Преобразовать в массив (toArray)");
            Console.WriteLine("17. Преобразовать в заданный массив (toArray with param)");
            Console.WriteLine("18. Получить элемент из головы (element)");
            Console.WriteLine("19. Попытка добавления (offer)");
            Console.WriteLine("20. Посмотреть элемент в голове (peek)");
            Console.WriteLine("21. Извлечь элемент из головы (poll)");
            Console.WriteLine("22. Показать текущее состояние");
            Console.WriteLine("0. Назад в меню");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            if (pq == null && !"12345".Contains(choice) && choice != "0")
            {
                Console.WriteLine("\nСначала создайте очередь!");
                Console.ReadLine();
                continue;
            }

            try
            {
                switch (choice)
                {
                    case "1":
                        pq = new MyPriorityQueue<int>();
                        Console.WriteLine("Пустая очередь создана.");
                        break;
                    case "2":
                        int[] arr = ReadIntArrayFromConsole();
                        pq = new MyPriorityQueue<int>(arr);
                        Console.WriteLine("Очередь создана на основе массива.");
                        break;
                    case "3":
                        Console.Write("Введите начальную емкость: ");
                        int capacity = int.Parse(Console.ReadLine());
                        pq = new MyPriorityQueue<int>(capacity);
                        Console.WriteLine($"Очередь создана с емкостью {capacity}.");
                        break;
                    case "4":
                        Console.Write("Введите начальную емкость: ");
                        int cap = int.Parse(Console.ReadLine());
                        // Для int компаратор по умолчанию создаст min-кучу.
                        // Для max-кучи можно было бы передать Comparer<int>.Create((x, y) => y.CompareTo(x))
                        pq = new MyPriorityQueue<int>(cap, System.Collections.Generic.Comparer<int>.Default);
                        Console.WriteLine($"Очередь создана с емкостью {cap} и компаратором.");
                        break;
                    case "5":
                        if (pq != null)
                        {
                            var copy = new MyPriorityQueue<int>(pq);
                            pq = copy;
                            Console.WriteLine("Очередь скопирована.");
                        }
                        else Console.WriteLine("Нет очереди для копирования.");
                        break;
                    case "6":
                        Console.Write("Введите число для добавления: ");
                        int toAdd = int.Parse(Console.ReadLine());
                        pq.Add(toAdd);
                        Console.WriteLine($"Элемент {toAdd} добавлен.");
                        break;
                    case "7":
                        int[] toAddAll = ReadIntArrayFromConsole();
                        pq.AddAll(toAddAll);
                        Console.WriteLine($"Добавлено {toAddAll.Length} элементов.");
                        break;
                    case "8":
                        pq.Clear();
                        Console.WriteLine("Очередь очищена.");
                        break;
                    case "9":
                        Console.Write("Какой элемент проверить? ");
                        int toCheck = int.Parse(Console.ReadLine());
                        Console.WriteLine(pq.Contains(toCheck) ? "Элемент найден." : "Элемент не найден.");
                        break;
                    case "10":
                        int[] toCheckAll = ReadIntArrayFromConsole();
                        Console.WriteLine(pq.ContainsAll(toCheckAll) ? "Все элементы найдены." : "Не все элементы найдены.");
                        break;
                    case "11":
                        Console.WriteLine(pq.IsEmpty ? "Очередь пуста." : "Очередь не пуста.");
                        break;
                    case "12":
                        Console.Write("Какой элемент удалить? ");
                        int toRemove = int.Parse(Console.ReadLine());
                        if (pq.Remove(toRemove))
                            Console.WriteLine($"Элемент {toRemove} удален.");
                        else
                            Console.WriteLine($"Элемент {toRemove} не найден.");
                        break;
                    case "13":
                        int[] toRemoveAll = ReadIntArrayFromConsole();
                        if (pq.RemoveAll(toRemoveAll))
                            Console.WriteLine("Элементы удалены.");
                        else
                            Console.WriteLine("Элементы не найдены.");
                        break;
                    case "14":
                        int[] toRetainAll = ReadIntArrayFromConsole();
                        if (pq.RetainAll(toRetainAll))
                            Console.WriteLine("Очередь изменена.");
                        else
                            Console.WriteLine("Очередь не изменена.");
                        break;
                    case "15":
                        Console.WriteLine($"Размер очереди: {pq.Size()}");
                        break;
                    case "16":
                        var array = pq.ToArray();
                        Console.WriteLine("Массив: " + string.Join(" ", array));
                        break;
                    case "17":
                        Console.Write("Введите размер целевого массива: ");
                        int arraySize = int.Parse(Console.ReadLine());
                        int[] targetArray = new int[arraySize];
                        var result = pq.ToArray(targetArray);
                        Console.WriteLine("Результат: " + string.Join(" ", result));
                        break;
                    case "18":
                        try
                        {
                            Console.WriteLine($"Element: {pq.Element()}");
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine($"Ошибка: {e.Message}");
                        }
                        break;
                    case "19":
                        Console.Write("Введите число для попытки добавления: ");
                        int toOffer = int.Parse(Console.ReadLine());
                        bool offered = pq.Offer(toOffer);
                        Console.WriteLine(offered ? $"Элемент {toOffer} добавлен." : $"Не удалось добавить элемент {toOffer}.");
                        break;
                    case "20":
                        Console.WriteLine($"Peek: {pq.Peek()}");
                        break;
                    case "21":
                        int polled = pq.Poll();
                        Console.WriteLine(polled == 0 && pq.Size() == 0 ? "Очередь пуста" : $"Извлечен элемент: {polled}");
                        break;
                    case "22":
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }

                ShowQueue(pq);
                Console.WriteLine("Нажмите Enter чтобы продолжить...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ОШИБКА: {e.Message}");
                Console.WriteLine("Нажмите Enter...");
                Console.ReadLine();
            }
        }
    }
}