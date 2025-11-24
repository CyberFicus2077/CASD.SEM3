using System;
using System.IO;

class zadacha10and11
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Задача 10 (Вектор)");
            Console.WriteLine("2. Задача 11 (IP адреса)");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");
            string s = Console.ReadLine();
            if (s == "0") break;
            if (s == "1") z10();
            if (s == "2") z11();
        }
    }

    static void z10()
    {
        MyVector<string> vec = new MyVector<string>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("вектор");
            Console.Write("Элементы: [ ");
            object[] arr = vec.toArray();
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine("]");
            Console.WriteLine("Размер: " + vec.size());
            Console.WriteLine("1. Добавить (add)");
            Console.WriteLine("2. Добавить по индексу");
            Console.WriteLine("3. Получить (get)");
            Console.WriteLine("4. Удалить (remove object)");
            Console.WriteLine("5. Удалить (remove index)");
            Console.WriteLine("6. Первый элемент");
            Console.WriteLine("7. Последний элемент");
            Console.WriteLine("8. Очистить");
            Console.WriteLine("0. Назад");
            Console.Write(">> ");

            string op = Console.ReadLine();
            if (op == "0") break;

            try
            {
                if (op == "1")
                {
                    Console.Write("Строка: ");
                    vec.add(Console.ReadLine());
                }
                else if (op == "2")
                {
                    Console.Write("Индекс: ");
                    int i = int.Parse(Console.ReadLine());
                    Console.Write("Строка: ");
                    vec.add(i, Console.ReadLine());
                }
                else if (op == "3")
                {
                    Console.Write("Индекс: ");
                    int i = int.Parse(Console.ReadLine());
                    Console.WriteLine("Значение: " + vec.get(i));
                    Console.ReadLine();
                }
                else if (op == "4")
                {
                    Console.Write("Строка: ");
                    bool res = vec.remove(Console.ReadLine());
                    Console.WriteLine(res ? "Удалено" : "Не найдено");
                    Console.ReadLine();
                }
                else if (op == "5")
                {
                    Console.Write("Индекс: ");
                    int i = int.Parse(Console.ReadLine());
                    vec.removeElementAt(i);
                }
                else if (op == "6")
                {
                    Console.WriteLine("Первый: " + vec.firstElement());
                    Console.ReadLine();
                }
                else if (op == "7")
                {
                    Console.WriteLine("Последний: " + vec.lastElement());
                    Console.ReadLine();
                }
                else if (op == "8")
                {
                    vec.clear();
                }
            }
            catch
            {
                Console.WriteLine("Ошибка операции.");
                Console.ReadLine();
            }
        }
    }

    static void z11()
    {
        string inFile = "input.txt";
        string outFile = "output.txt";
        //!!!! 
        if (!File.Exists(inFile))
        {
            File.WriteAllText(inFile, "");
            Console.WriteLine("Файл input.txt найден.");
        }

        string[] lines = File.ReadAllLines(inFile);
        MyVector<string> ipList = new MyVector<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int len = line.Length;

            for (int j = 0; j < len; j++)
            {
                char c = line[j];
                if (char.IsDigit(c))
                {
                    int start = j;
                    while (j < len && (char.IsDigit(line[j]) || line[j] == '.'))
                    {
                        j++;
                    }
                    int end = j;

                    string candidate = line.Substring(start, end - start);

                    bool leftOk = (start == 0) || !char.IsDigit(line[start - 1]);
                    bool rightOk = (end == len) || !char.IsDigit(line[end]);

                    if (leftOk && rightOk)
                    {
                        if (CheckIP(candidate))
                        {
                            ipList.add(candidate);
                        }
                    }
                    j--;
                }
            }
        }

        StreamWriter sw = new StreamWriter(outFile);
        for (int i = 0; i < ipList.size(); i++)
        {
            sw.WriteLine(ipList.get(i));
            Console.WriteLine("Найдено: " + ipList.get(i));
        }
        sw.Close();

        Console.WriteLine("Готово. Ваш результат в output.txt :)");
        Console.ReadLine();
    }

    static bool CheckIP(string s)
    {
        int dots = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '.') dots++;
        }
        if (dots != 3) return false;

        string[] parts = s.Split('.');
        if (parts.Length != 4) return false;

        for (int i = 0; i < 4; i++)
        {
            string p = parts[i];
            if (p.Length == 0) return false;

            int num;
            bool isNum = int.TryParse(p, out num);
            if (!isNum) return false;

            if (num < 0 || num > 255) return false;
        }
        return true;
    }
}

public class MyVector<T>
{
    private T[] elementData;
    private int elementCount;
    private int capacityIncrement;

    public MyVector(int initialCapacity, int capacityIncrement)
    {
        if (initialCapacity < 0) initialCapacity = 10;
        this.elementData = new T[initialCapacity];
        this.elementCount = 0;
        this.capacityIncrement = capacityIncrement;
    }

    public MyVector(int initialCapacity)
    {
        this.elementData = new T[initialCapacity];
        this.elementCount = 0;
        this.capacityIncrement = 0;
    }

    public MyVector()
    {
        this.elementData = new T[10];
        this.elementCount = 0;
        this.capacityIncrement = 0;
    }

    public MyVector(T[] a)
    {
        this.elementCount = a.Length;
        this.elementData = new T[this.elementCount];
        for (int i = 0; i < a.Length; i++) this.elementData[i] = a[i];
        this.capacityIncrement = 0;
    }

    private void Grow()
    {
        int oldCap = elementData.Length;
        int newCap = 0;

        if (capacityIncrement > 0)
        {
            newCap = oldCap + capacityIncrement;
        }
        else
        {
            newCap = oldCap * 2;
            if (newCap == 0) newCap = 1;
        }

        T[] newArr = new T[newCap];
        for (int i = 0; i < elementCount; i++)
        {
            newArr[i] = elementData[i];
        }
        elementData = newArr;
    }

    public void add(T e)
    {
        if (elementCount >= elementData.Length)
        {
            Grow();
        }
        elementData[elementCount] = e;
        elementCount++;
    }

    public void addAll(T[] a)
    {
        if (a == null) return;
        for (int i = 0; i < a.Length; i++)
        {
            add(a[i]);
        }
    }

    public void clear()
    {
        for (int i = 0; i < elementCount; i++)
        {
            elementData[i] = default(T);
        }
        elementCount = 0;
    }

    public bool contains(object o)
    {
        return indexOf(o) >= 0;
    }

    public bool containsAll(T[] a)
    {
        if (a == null) return true;
        for (int i = 0; i < a.Length; i++)
        {
            if (!contains(a[i])) return false;
        }
        return true;
    }

    public bool isEmpty()
    {
        return elementCount == 0;
    }

    public bool remove(object o)
    {
        int idx = indexOf(o);
        if (idx >= 0)
        {
            removeElementAt(idx);
            return true;
        }
        return false;
    }

    public bool removeAll(T[] a)
    {
        bool changed = false;
        if (a == null) return false;
        for (int i = 0; i < a.Length; i++)
        {
            while (contains(a[i]))
            {
                remove(a[i]);
                changed = true;
            }
        }
        return changed;
    }

    public bool retainAll(T[] a)
    {
        bool changed = false;
        if (a == null) return false;
        for (int i = elementCount - 1; i >= 0; i--)
        {
            bool found = false;
            for (int j = 0; j < a.Length; j++)
            {
                if (Object.Equals(elementData[i], a[j]))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                removeElementAt(i);
                changed = true;
            }
        }
        return changed;
    }

    public int size()
    {
        return elementCount;
    }

    public object[] toArray()
    {
        object[] res = new object[elementCount];
        for (int i = 0; i < elementCount; i++) res[i] = elementData[i];
        return res;
    }

    public T[] toArray(T[] a)
    {
        if (a == null || a.Length < elementCount)
        {
            a = new T[elementCount];
        }
        for (int i = 0; i < elementCount; i++) a[i] = elementData[i];
        if (a.Length > elementCount) a[elementCount] = default(T);
        return a;
    }

    public void add(int index, T e)
    {
        if (index < 0 || index > elementCount) throw new IndexOutOfRangeException();
        if (elementCount >= elementData.Length) Grow();

        for (int i = elementCount; i > index; i--)
        {
            elementData[i] = elementData[i - 1];
        }
        elementData[index] = e;
        elementCount++;
    }

    public void addAll(int index, T[] a)
    {
        if (index < 0 || index > elementCount) throw new IndexOutOfRangeException();
        if (a == null || a.Length == 0) return;

        while (elementCount + a.Length > elementData.Length) Grow();

        for (int i = elementCount - 1; i >= index; i--)
        {
            elementData[i + a.Length] = elementData[i];
        }
        for (int i = 0; i < a.Length; i++)
        {
            elementData[index + i] = a[i];
        }
        elementCount += a.Length;
    }

    public T get(int index)
    {
        if (index < 0 || index >= elementCount) throw new IndexOutOfRangeException();
        return elementData[index];
    }

    public int indexOf(object o)
    {
        for (int i = 0; i < elementCount; i++)
        {
            if (o == null)
            {
                if (elementData[i] == null) return i;
            }
            else
            {
                if (o.Equals(elementData[i])) return i;
            }
        }
        return -1;
    }

    public int lastIndexOf(object o)
    {
        for (int i = elementCount - 1; i >= 0; i--)
        {
            if (o == null)
            {
                if (elementData[i] == null) return i;
            }
            else
            {
                if (o.Equals(elementData[i])) return i;
            }
        }
        return -1;
    }

    public T remove(int index)
    {
        if (index < 0 || index >= elementCount) throw new IndexOutOfRangeException();
        T val = elementData[index];
        removeElementAt(index);
        return val;
    }

    public T set(int index, T e)
    {
        if (index < 0 || index >= elementCount) throw new IndexOutOfRangeException();
        T old = elementData[index];
        elementData[index] = e;
        return old;
    }

    public MyVector<T> subList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex > elementCount || fromIndex > toIndex) throw new IndexOutOfRangeException();
        MyVector<T> sub = new MyVector<T>(toIndex - fromIndex);
        for (int i = fromIndex; i < toIndex; i++)
        {
            sub.add(elementData[i]);
        }
        return sub;
    }

    public T firstElement()
    {
        if (elementCount == 0) throw new Exception("Vector is empty");
        return elementData[0];
    }

    public T lastElement()
    {
        if (elementCount == 0) throw new Exception("Vector is empty");
        return elementData[elementCount - 1];
    }

    public void removeElementAt(int pos)
    {
        if (pos < 0 || pos >= elementCount) throw new IndexOutOfRangeException();
        for (int i = pos; i < elementCount - 1; i++)
        {
            elementData[i] = elementData[i + 1];
        }
        elementCount--;
        elementData[elementCount] = default(T);
    }

    public void removeRange(int begin, int end)
    {
        if (begin < 0 || end > elementCount || begin > end) throw new IndexOutOfRangeException();
        int numMoved = elementCount - end;
        int newCount = elementCount - (end - begin);

        for (int i = 0; i < numMoved; i++)
        {
            elementData[begin + i] = elementData[end + i];
        }

        for (int i = newCount; i < elementCount; i++)
        {
            elementData[i] = default(T);
        }
        elementCount = newCount;
    }
}