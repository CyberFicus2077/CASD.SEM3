using System;
using System.IO;

class Zadacha8
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Задача 8 (Динамический массив)");
            Console.WriteLine("2. Задача 9 (Поиск тегов)");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            string key = Console.ReadLine();

            if (key == "0") break;
            if (key == "1") Z8();
            if (key == "2") Z9();
        }
    }

    static void Z8()
    {
        MyList<string> list = new MyList<string>();

        while (true)
        {
            Console.WriteLine();
            Console.Write("Список: ");
            if (list.isEmpty())
            {
                Console.WriteLine("(пусто)");
            }
            else
            {
                object[] arr = list.toArray();
                Console.Write("[ ");
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
                Console.WriteLine("]");
            }
            Console.WriteLine("Размер: " + list.size());

            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Добавить по индексу");
            Console.WriteLine("3. Получить (get)");
            Console.WriteLine("4. Удалить (remove index)");
            Console.WriteLine("5. Удалить (remove object)");
            Console.WriteLine("6. Найти индекс");
            Console.WriteLine("7. Очистить");
            Console.WriteLine("0. Назад");
            Console.Write("> ");

            string op = Console.ReadLine();
            if (op == "0") break;

            try
            {
                if (op == "1")
                {
                    Console.Write("Введите строку: ");
                    list.add(Console.ReadLine());
                }
                else if (op == "2")
                {
                    Console.Write("Индекс: ");
                    int idx = int.Parse(Console.ReadLine());
                    Console.Write("Строка: ");
                    list.add(idx, Console.ReadLine());
                }
                else if (op == "3")
                {
                    Console.Write("Индекс: ");
                    int idx = int.Parse(Console.ReadLine());
                    Console.WriteLine("Элемент: " + list.get(idx));
                }
                else if (op == "4")
                {
                    Console.Write("Индекс: ");
                    int idx = int.Parse(Console.ReadLine());
                    Console.WriteLine("Удалено: " + list.remove(idx));
                }
                else if (op == "5")
                {
                    Console.Write("Строка: ");
                    bool res = list.remove(Console.ReadLine());
                    if (res) Console.WriteLine("Удалено успешно");
                    else Console.WriteLine("Не найдено");
                }
                else if (op == "6")
                {
                    Console.Write("Строка: ");
                    int idx = list.indexOf(Console.ReadLine());
                    Console.WriteLine("Индекс: " + idx);
                }
                else if (op == "7")
                {
                    list.clear();
                    Console.WriteLine("Очищено");
                }
            }
            catch
            {
                Console.WriteLine("Ошибка ввода или индекса");
            }
        }
    }

    static void Z9()
    {
        Console.WriteLine();
        string fName = "input.txt";

        if (!File.Exists(fName))
        {
            File.WriteAllText(fName, "Test <html> data <BODY> with <br> tags </HTML> and <1bad>");
            Console.WriteLine("Создан файл input.txt с тестовыми данными.");
        }

        string[] allLines = File.ReadAllLines(fName);
        MyList<string> finalTags = new MyList<string>();

        for (int i = 0; i < allLines.Length; i++)
        {
            string s = allLines[i];
            for (int j = 0; j < s.Length; j++)
            {
                if (s[j] == '<')
                {
                    int close = -1;
                    for (int k = j + 1; k < s.Length; k++)
                    {
                        if (s[k] == '>')
                        {
                            close = k;
                            break;
                        }
                    }

                    if (close != -1)
                    {
                        int len = close - j + 1;
                        string raw = s.Substring(j, len);

                        if (CheckTag(raw))
                        {
                            bool isDup = false;
                            string cleanNew = CleanStr(raw);

                            for (int x = 0; x < finalTags.size(); x++)
                            {
                                if (CleanStr(finalTags.get(x)) == cleanNew)
                                {
                                    isDup = true;
                                    break;
                                }
                            }

                            if (!isDup)
                            {
                                finalTags.add(raw);
                            }
                        }
                        j = close;
                    }
                }
            }
        }

        Console.WriteLine("Уникальные теги из файла:");
        for (int i = 0; i < finalTags.size(); i++)
        {
            Console.WriteLine(finalTags.get(i));
        }
        Console.WriteLine("Нажмите что-то");
        Console.ReadLine();
    }

    static bool CheckTag(string t)
    {
        if (t.Length < 3) return false;
        int p = 1;
        if (t[p] == '/')
        {
            p++;
            if (p >= t.Length - 1) return false;
        }

        char c = t[p];
        bool isLet = (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        if (!isLet) return false;

        for (int k = p + 1; k < t.Length - 1; k++)
        {
            char ch = t[k];
            bool ok = (ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
            if (!ok) return false;
        }
        return true;
    }

    static string CleanStr(string s)
    {
        string res = "";
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if (c != '<' && c != '>' && c != '/')
            {
                if (c >= 'A' && c <= 'Z') res += (char)(c + 32);
                else res += c;
            }
        }
        return res;
    }
}

public class MyList<T>
{
    private T[] arr;
    private int count;

    public MyList()
    {
        arr = new T[10];
        count = 0;
    }

    public MyList(T[] input)
    {
        if (input == null)
        {
            arr = new T[10];
            count = 0;
        }
        else
        {
            count = input.Length;
            arr = new T[count];
            for (int i = 0; i < count; i++) arr[i] = input[i];
        }
    }

    public MyList(int cap)
    {
        arr = new T[cap];
        count = 0;
    }

    private void Expand()
    {
        if (count >= arr.Length)
        {
            int n = (int)(arr.Length * 1.5) + 1;
            T[] temp = new T[n];
            for (int i = 0; i < count; i++) temp[i] = arr[i];
            arr = temp;
        }
    }

    public void add(T e)
    {
        Expand();
        arr[count] = e;
        count++;
    }

    public void addAll(T[] a)
    {
        if (a == null) return;
        for (int i = 0; i < a.Length; i++) add(a[i]);
    }

    public void clear()
    {
        for (int i = 0; i < count; i++) arr[i] = default(T);
        count = 0;
    }

    public bool contains(object o)
    {
        if (indexOf(o) != -1) return true;
        return false;
    }

    public bool containsAll(T[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (!contains(a[i])) return false;
        }
        return true;
    }

    public bool isEmpty()
    {
        return count == 0;
    }

    public bool remove(object o)
    {
        int i = indexOf(o);
        if (i >= 0)
        {
            remove(i);
            return true;
        }
        return false;
    }

    public bool removeAll(T[] a)
    {
        bool flag = false;
        for (int i = 0; i < a.Length; i++)
        {
            while (contains(a[i]))
            {
                remove(a[i]);
                flag = true;
            }
        }
        return flag;
    }

    public bool retainAll(T[] a)
    {
        bool flag = false;
        for (int i = count - 1; i >= 0; i--)
        {
            bool found = false;
            for (int j = 0; j < a.Length; j++)
            {
                if (Object.Equals(arr[i], a[j]))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                remove(i);
                flag = true;
            }
        }
        return flag;
    }

    public int size()
    {
        return count;
    }

    public object[] toArray()
    {
        object[] r = new object[count];
        for (int i = 0; i < count; i++) r[i] = arr[i];
        return r;
    }

    public T[] toArray(T[] a)
    {
        if (a == null || a.Length < count) a = new T[count];
        for (int i = 0; i < count; i++) a[i] = arr[i];
        if (a.Length > count) a[count] = default(T);
        return a;
    }

    public void add(int index, T e)
    {
        if (index < 0 || index > count) return;
        Expand();
        for (int i = count; i > index; i--) arr[i] = arr[i - 1];
        arr[index] = e;
        count++;
    }

    public void addAll(int index, T[] a)
    {
        if (index < 0 || index > count) return;
        if (a == null) return;
        while (count + a.Length > arr.Length)
        {
            int n = (int)(arr.Length * 1.5) + 1;
            T[] temp = new T[n];
            for (int k = 0; k < count; k++) temp[k] = arr[k];
            arr = temp;
        }
        for (int i = count - 1; i >= index; i--) arr[i + a.Length] = arr[i];
        for (int i = 0; i < a.Length; i++) arr[index + i] = a[i];
        count += a.Length;
    }

    public T get(int index)
    {
        if (index < 0 || index >= count) throw new Exception();
        return arr[index];
    }

    public int indexOf(object o)
    {
        for (int i = 0; i < count; i++)
        {
            if (o == null)
            {
                if (arr[i] == null) return i;
            }
            else
            {
                if (o.Equals(arr[i])) return i;
            }
        }
        return -1;
    }

    public int lastIndexOf(object o)
    {
        for (int i = count - 1; i >= 0; i--)
        {
            if (o == null)
            {
                if (arr[i] == null) return i;
            }
            else
            {
                if (o.Equals(arr[i])) return i;
            }
        }
        return -1;
    }

    public T remove(int index)
    {
        if (index < 0 || index >= count) throw new Exception("Неверный индекс");
        T val = arr[index];
        for (int i = index; i < count - 1; i++)
        {
            arr[i] = arr[i + 1];
        }
        count--;
        arr[count] = default(T);
        return val;
    }

    public T set(int index, T e)
    {
        if (index < 0 || index >= count) throw new Exception();
        T old = arr[index];
        arr[index] = e;
        return old;
    }

    public MyList<T> subList(int from, int to)
    {
        if (from < 0 || to > count || from > to) return null;
        MyList<T> sub = new MyList<T>(to - from);
        for (int i = from; i < to; i++) sub.add(arr[i]);
        return sub;
    }
}