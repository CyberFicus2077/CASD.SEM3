using System;
using System.Collections;

public class Heap<T> : IEnumerable<T>
{
    protected T[] items;
    protected int count;
    protected readonly IComparer<T> comparator;

    public Heap(int capacity = 10, IComparer<T> comp = null)
    {
        items = new T[Math.Max(10, capacity)];
        count = 0;
        this.comparator = comp ?? Comparer<T>.Default;
    }

    //из массива
    public Heap(T[] array, IComparer<T> comp = null)
    {
        if (array == null) array = Array.Empty<T>();

        items = new T[Math.Max(10, array.Length)];
        Array.Copy(array, items, array.Length);
        count = array.Length;
        this.comparator = comp ?? Comparer<T>.Default;

        // Построение кучи
        for (int i = (count / 2) - 1; i >= 0; i--)
        {
            FixDown(i);
        }
    }

    //копирование
    public Heap(Heap<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        items = new T[other.items.Length];
        Array.Copy(other.items, items, other.count);
        count = other.count;
        comparator = other.comparator;
    }

    public int Count => count;
    public bool IsEmpty => count == 0;

    // наход макс мин
    public T Peek()
    {
        if (count == 0) throw new InvalidOperationException("Куча пуста");
        return items[0];
    }

    //удаления мак мин
    public T RemoveRoot()
    {
        if (count == 0) throw new InvalidOperationException("Куча пуста");
        T root = items[0];
        items[0] = items[count - 1];
        items[count - 1] = default(T); 
        count--;
        if (count > 1) FixDown(0);
        return root;
    }

    public void Add(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (count == items.Length) Resize();

        items[count] = item;
        FixUp(count);
        count++;
    }

    // увеличение/уменьшение ключа
    public void Update(int index, T newValue)
    {
        if (index < 0 || index >= count) throw new ArgumentOutOfRangeException(nameof(index));

        T oldValue = items[index];
        items[index] = newValue;

        if (comparator.Compare(newValue, oldValue) > 0)
        {
            FixUp(index);
        }
        else
        {
            FixDown(index);
        }
    }

    // слияние
    public Heap<T> Combine(Heap<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (!this.comparator.Equals(other.comparator))
            throw new InvalidOperationException("Нельзя объединить кучи с разными компараторами.");

        T[] combinedArray = new T[this.count + other.count];
        Array.Copy(this.items, 0, combinedArray, 0, this.count);
        Array.Copy(other.items, 0, combinedArray, this.count, other.count);

        return new Heap<T>(combinedArray, this.comparator);
    }

    public void Clear()
    {
        Array.Clear(items, 0, count);
        count = 0;
    }

    public T[] ToArray()
    {
        T[] result = new T[count];
        Array.Copy(items, result, count);
        return result;
    }

    protected void FixUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (comparator.Compare(items[index], items[parentIndex]) <= 0) break;

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    protected void FixDown(int index)
    {
        while (true)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int largestChild = index;

            if (leftChild < count && comparator.Compare(items[leftChild], items[largestChild]) > 0)
            {
                largestChild = leftChild;
            }

            if (rightChild < count && comparator.Compare(items[rightChild], items[largestChild]) > 0)
            {
                largestChild = rightChild;
            }

            if (largestChild == index) break;

            Swap(index, largestChild);
            index = largestChild;
        }
    }

    protected int IndexOf(object o)
    {
        if (o == null) return -1;
        for (int i = 0; i < count; i++)
        {
            if (o.Equals(items[i])) return i;
        }
        return -1;
    }

    protected T RemoveAt(int index)
    {
        if (index < 0 || index >= count) throw new ArgumentOutOfRangeException(nameof(index));

        T removedItem = items[index];
        items[index] = items[count - 1];
        items[count - 1] = default(T);
        count--;

        if (index < count)
        {
            T movedItem = items[index];
            FixDown(index);
            if (items[index].Equals(movedItem))
            {
                FixUp(index);
            }
        }

        return removedItem;
    }

    private void Swap(int i, int j)
    {
        (items[i], items[j]) = (items[j], items[i]);
    }

    private void Resize()
    {
        int oldCapacity = items.Length;
        int newCapacity;

        if (oldCapacity < 64)
        {
            newCapacity = oldCapacity + 2;
        }
        else
        {
            newCapacity = oldCapacity + (oldCapacity / 2);
        }

        if (newCapacity <= oldCapacity) newCapacity = oldCapacity + 1;

        T[] newItems = new T[newCapacity];
        Array.Copy(items, newItems, count);
        items = newItems;
    }

    public void Show()
    {
        if (count == 0)
        {
            Console.WriteLine("пусто");
            return;
        }
        for (int i = 0; i < count; i++) Console.Write(items[i] + " ");
        Console.WriteLine();
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}