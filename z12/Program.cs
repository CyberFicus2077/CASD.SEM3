using System;
public class MyStack<T> : MyVector<T>
{
    public void Push(T item)
    {
        this.add(item);
    }

    public T Pop()
    {
        if (Empty())
        {
            throw new InvalidOperationException("Стек пуст");
        }

        int lastIndex = this.size() - 1;
        T item = this.get(lastIndex);
        this.removeElementAt(lastIndex);
        return item;
    }

    public T Peek()
    {
        if (Empty())
        {
            throw new InvalidOperationException("Стек пуст");
        }

        return this.get(this.size() - 1);
    }

    public bool Empty()
    {
        return this.isEmpty();
    }

    public int Search(T item)
    {
        for (int i = this.size() - 1, depth = 1; i >= 0; i--, depth++)
        {
            T current = this.get(i);

            if ((item == null && current == null) ||
                (item != null && item.Equals(current)))
            {
                return depth;
            }
        }

        return -1;
    }
}

public class MyVector<T>
{
    private T[] elementData;
    private int elementCount;
    private int capacityIncrement;

    public MyVector(int initialCapacity, int capacityIncrement)
    {
        if (initialCapacity < 0) initialCapacity = 8;
        this.elementData = new T[initialCapacity];
        this.elementCount = 0;
        this.capacityIncrement = capacityIncrement;
    }

    public MyVector(int initialCapacity) : this(initialCapacity, 0) { }

    public MyVector() : this(8, 0) { }

    public MyVector(T[] a)
    {
        this.elementCount = a.Length;
        this.elementData = new T[this.elementCount];
        for (int i = 0; i < a.Length; i++)
            this.elementData[i] = a[i];
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

    public void add(int index, T e)
    {
        if (index < 0 || index > elementCount)
            throw new IndexOutOfRangeException();

        if (elementCount >= elementData.Length)
            Grow();

        for (int i = elementCount; i > index; i--)
        {
            elementData[i] = elementData[i - 1];
        }
        elementData[index] = e;
        elementCount++;
    }

    public T get(int index)
    {
        if (index < 0 || index >= elementCount)
            throw new IndexOutOfRangeException();

        return elementData[index];
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

    public void removeElementAt(int pos)
    {
        if (pos < 0 || pos >= elementCount)
            throw new IndexOutOfRangeException();

        for (int i = pos; i < elementCount - 1; i++)
        {
            elementData[i] = elementData[i + 1];
        }
        elementCount--;
        elementData[elementCount] = default(T);
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

    public void clear()
    {
        for (int i = 0; i < elementCount; i++)
        {
            elementData[i] = default(T);
        }
        elementCount = 0;
    }

    public bool isEmpty()
    {
        return elementCount == 0;
    }

    public int size()
    {
        return elementCount;
    }

    public T firstElement()
    {
        if (elementCount == 0) throw new Exception("Вектор пуст");
        return elementData[0];
    }

    public T lastElement()
    {
        if (elementCount == 0) throw new Exception("Вектор пуст");
        return elementData[elementCount - 1];
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите тип стека:");
        Console.WriteLine("1. Стек целых чисел");
        Console.WriteLine("2. Стек строк");
        Console.Write("Ваш выбор: ");

        string typeChoice = Console.ReadLine();

        if (typeChoice == "1")
        {
            Stack();
        }
        else if (typeChoice == "2")
        {
            StringStack();
        }
        else
        {
            Console.WriteLine("Неверный выбор");
        }
    }

    static void Stack()
    {
        MyStack<int> stack = new MyStack<int>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Стек целых чисел");
            Console.WriteLine("Текущий стек (сверху вниз):");

            if (stack.Empty())
            {
                Console.WriteLine("пусто :(");
            }
            else
            {
                for (int i = stack.size() - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{stack.size() - i}. {stack.get(i)}");
                }
            }

            Console.WriteLine("\nВыберите операцию:");
            Console.WriteLine("1. Push (добавить элемент)");
            Console.WriteLine("2. Pop (извлечь элемент)");
            Console.WriteLine("3. Peek (посмотреть верхний элемент)");
            Console.WriteLine("4. Empty (проверить пустоту)");
            Console.WriteLine("5. Search (найти элемент)");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите целое число для добавления: ");
                    if (int.TryParse(Console.ReadLine(), out int num))
                    {
                        stack.Push(num);
                        Console.WriteLine($"Элемент {num} добавлен в стек");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода числа");
                    }
                    break;

                case "2":
                    try
                    {
                        int popped = stack.Pop();
                        Console.WriteLine($"Извлечен элемент: {popped}");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Ошибка: {e.Message}");
                    }
                    break;

                case "3":
                    try
                    {
                        int peeked = stack.Peek();
                        Console.WriteLine($"Верхний элемент: {peeked}");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Ошибка: {e.Message}");
                    }
                    break;

                case "4":
                    Console.WriteLine($"Стек пуст: {stack.Empty()}");
                    break;

                case "5":
                    Console.Write("Введите целое число для поиска: ");
                    if (int.TryParse(Console.ReadLine(), out int searchNum))
                    {
                        int depth = stack.Search(searchNum);
                        if (depth == -1)
                        {
                            Console.WriteLine($"Элемент {searchNum} не найден в стеке");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент {searchNum} найден на глубине {depth}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода числа");
                    }
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор операции");
                    break;
            } 
            Console.WriteLine("\nНажмите любую клавишу для продолжения.");
            Console.ReadKey();
        }
    }

    static void StringStack()
    {

        MyStack<string> stack = new MyStack<string>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Стек целых чисел");
            Console.WriteLine("Текущий стек (сверху вниз):");

            if (stack.Empty())
            {
                Console.WriteLine("пусто :( ");
            }
            else
            {
                for (int i = stack.size() - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{stack.size() - i}. {stack.get(i)}");
                }
            }

            Console.WriteLine("\nВыберите операцию:");
            Console.WriteLine("1. Push (добавить элемент)");
            Console.WriteLine("2. Pop (извлечь элемент)");
            Console.WriteLine("3. Peek (посмотреть верхний элемент)");
            Console.WriteLine("4. Empty (проверить пустоту)");
            Console.WriteLine("5. Search (найти элемент)");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите элемент для добавления: ");
                    string str = Console.ReadLine();
                    if (!string.IsNullOrEmpty(str))
                    {
                        stack.Push(str);
                        Console.WriteLine($"Элемент '{str}' добавлен в стек");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: строка не может быть пустой");
                    }
                    break;

                case "2":
                    try
                    {
                        string popped = stack.Pop();
                        Console.WriteLine($"Извлечена строка: '{popped}'");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Ошибка: {e.Message}");
                    }
                    break;

                case "3":
                    try
                    {
                        string peeked = stack.Peek();
                        Console.WriteLine($"Верхний элемент: '{peeked}'");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Ошибка: {e.Message}");
                    }
                    break;

                case "4":
                    Console.WriteLine($"Стек пуст: {stack.Empty()}");
                    break;

                case "5":
                    Console.Write("Введите элемент для поиска: ");
                    string searchStr = Console.ReadLine();
                    int depth = stack.Search(searchStr);
                    if (depth == -1)
                    {
                        Console.WriteLine($"Строка '{searchStr}' не найдена в стеке");
                    }
                    else
                    {
                        Console.WriteLine($"Строка '{searchStr}' найдена на глубине {depth}");
                    }
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор операции");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения.");
            Console.ReadKey();
        }
    }
}