using System;
class Heap<X> where X : IComparable<X>
{
    private X[] p;
    private int c;

    public Heap()
    {
        p = new X[10];
        c = 0;
    }

    public Heap(Heap<X> d)
    {
        if (d == null) throw new Exception("Куча не задана");
        p = new X[Math.Max(10, d.c)];
        for (int i = 0; i < d.c; i++) p[i] = d.p[i];
        c = d.c;
    }

    public Heap(X[] v)
    {
        if (v == null) v = new X[0];
        p = new X[Math.Max(10, v.Length)];
        for (int i = 0; i < v.Length; i++) p[i] = v[i];
        c = v.Length;
        for (int i = c / 2 - 1; i >= 0; i--) FixDown(i);
    }

    public X GetMax()
    {
        if (c == 0) throw new Exception("Куча пуста");
        return p[0];
    }

    public X TakeMax()
    {
        if (c == 0) throw new Exception("Куча пуста");
        X max = p[0];
        p[0] = p[c - 1];
        c--;
        if (c > 0) FixDown(0);
        return max;
    }

    public void Update(int pos, X newVal)
    {
        if (pos < 0 || pos >= c) throw new Exception("Нет такого индекса");
        X old = p[pos];
        p[pos] = newVal;
        int cmp = newVal.CompareTo(old);
        if (cmp > 0) FixUp(pos);
        else if (cmp < 0) FixDown(pos);
    }

    public void Push(X value)
    {
        if (c == p.Length) Resize();
        p[c] = value;
        int i = c;
        c++;
        FixUp(i);
    }

    public Heap<X> Combine(Heap<X> d)
    {
        if (d == null) throw new Exception("Нет второй кучи");
        X[] all = new X[c + d.c];
        for (int i = 0; i < c; i++) all[i] = p[i];
        for (int j = 0; j < d.c; j++) all[c + j] = d.p[j];
        return new Heap<X>(all);
    }

    private void Resize()
    {
        int newCap = p.Length == 0 ? 10 : p.Length * 2;
        X[] mas = new X[newCap];
        for (int i = 0; i < c; i++) mas[i] = p[i];
        p = mas;
    }

    private void FixDown(int pos)
    {
        while (true)
        {
            int l = pos * 2 + 1;
            if (l >= c) break;
            int r = l + 1;
            int big = l;
            if (r < c && p[r].CompareTo(p[l]) > 0) big = r;
            if (p[pos].CompareTo(p[big]) >= 0) break;
            Swap(pos, big);
            pos = big;
        }
    }

    private void FixUp(int pos)
    {
        while (pos > 0)
        {
            int parent = (pos - 1) / 2;
            if (p[parent].CompareTo(p[pos]) >= 0) break;
            Swap(parent, pos);
            pos = parent;
        }
    }

    private void Swap(int i, int j)
    {
        X t = p[i];
        p[i] = p[j];
        p[j] = t;
    }
    public void Show()
    {
        if (c == 0)
        {
            Console.WriteLine("пусто");
            return;
        }
        for (int i = 0; i < c; i++) Console.Write(p[i] + " ");
        Console.WriteLine();
    }
}
class HeapSort
{
    static void Heapify(int[] mas)
    {
        int n = mas.Length;
        int s = n / 2 - 1;
        for (int i = s; i >= 0; i--)
        {
            SiftDownMax(mas, i, n);
        }
    }

    static void SiftDownMax(int[] mas, int idx, int size)
    {
        while (idx * 2 + 1 < size)
        {
            int l = 2 * idx + 1;
            int r = 2 * idx + 2;
            int maxIdx = l;
            if (r < size && mas[r] > mas[l])
            {
                maxIdx = r;
            }
            if (mas[idx] >= mas[maxIdx])
            {
                break;
            }
            else
            {
                int t = mas[idx];
                mas[idx] = mas[maxIdx];
                mas[maxIdx] = t;
                idx = maxIdx;
            }
        }
    }

    static void Heapsort(int[] mas)
    {
        int n = mas.Length;
        Heapify(mas);
        for (int i = n - 1; i > 0; i--)
        {
            int t = mas[0];
            mas[0] = mas[i];
            mas[i] = t;
            SiftDownMax(mas, 0, i);
        }
    }
}
class Program
{
    static void ShowAll(Heap<int> h1, Heap<int> h2, Heap<int> h3)
    {
        Console.WriteLine("\nНА ДАННЫЙ МОМЕНТ:");
        Console.Write("Первая куча: ");
        if (h1 != null) h1.Show(); else Console.WriteLine("не создана");

        Console.Write("Вторая куча: ");
        if (h2 != null) h2.Show(); else Console.WriteLine("не создана");

        Console.Write("Третья (объединенная) куча: ");
        if (h3 != null) h3.Show(); else Console.WriteLine("не создана");
        Console.WriteLine("~~~~~");
    }

    static void Main()
    {
        Heap<int> h1 = null;
        Heap<int> h2 = null;
        Heap<int> h3 = null;

        while (true)
        {
            Console.WriteLine("МЕТОДЫ:");
            Console.WriteLine("1. Создать первую кучу");
            Console.WriteLine("2. Создать вторую кучу");
            Console.WriteLine("3. Найти максимум");
            Console.WriteLine("4. Удалить максимум");
            Console.WriteLine("5. Добавить элемент");
            Console.WriteLine("6. Изменить элемент");
            Console.WriteLine("7. Объединить кучи");
            Console.WriteLine("0. Выход");
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
                    Console.WriteLine("Первая куча создана :)");
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
                    Console.WriteLine("Вторая куча создана :)");
                    ShowAll(h1, h2, h3);
                }
                else if (choice == "3")
                {
                    Console.Write("Какую кучу? (1/2/3): ");
                    string h = Console.ReadLine();
                    if (h == "1" && h1 != null) { Console.WriteLine("Максимум: " + h1.GetMax()); ShowAll(h1, h2, h3); }
                    else if (h == "2" && h2 != null) { Console.WriteLine("Максимум: " + h2.GetMax()); ShowAll(h1, h2, h3); }
                    else if (h == "3" && h3 != null) { Console.WriteLine("Максимум: " + h3.GetMax()); ShowAll(h1, h2, h3); }
                    else Console.WriteLine("Куча не создана");
                }
                else if (choice == "4")
                {
                    Console.Write("Из какой кучи? (1/2/3): ");
                    string h = Console.ReadLine();
                    if (h == "1" && h1 != null) { Console.WriteLine("Удален: " + h1.TakeMax()); ShowAll(h1, h2, h3); }
                    else if (h == "2" && h2 != null) { Console.WriteLine("Удален: " + h2.TakeMax()); ShowAll(h1, h2, h3); }
                    else if (h == "3" && h3 != null) { Console.WriteLine("Удален: " + h3.TakeMax()); ShowAll(h1, h2, h3); }
                    else Console.WriteLine("Куча не создана");
                }
                else if (choice == "5")
                {
                    Console.Write("В какую кучу? (1/2/3): ");
                    string h = Console.ReadLine();
                    Console.Write("Какое число добавить? ");
                    int x = int.Parse(Console.ReadLine() ?? "0");
                    if (h == "1" && h1 != null) { h1.Push(x); ShowAll(h1, h2, h3); }
                    else if (h == "2" && h2 != null) { h2.Push(x); ShowAll(h1, h2, h3); }
                    else if (h == "3" && h3 != null) { h3.Push(x); ShowAll(h1, h2, h3); }
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
                    if (h == "1" && h1 != null) { h1.Update(idx, val); ShowAll(h1, h2, h3); }
                    else if (h == "2" && h2 != null) { h2.Update(idx, val); ShowAll(h1, h2, h3); }
                    else if (h == "3" && h3 != null) { h3.Update(idx, val); ShowAll(h1, h2, h3); }
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
                    else
                    {
                        Console.WriteLine("Сначала создайте обе кучи >:^");
                    }
                }
                else
                {
                    Console.WriteLine("Неизвестная команда");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
        }
    }
}