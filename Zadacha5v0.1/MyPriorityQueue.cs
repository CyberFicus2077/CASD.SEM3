using System;

public class MyPriorityQueue<T> : Heap<T>
{
    public MyPriorityQueue() : base(20, null) { }

    public MyPriorityQueue(T[] a) : base(a, null) { }

    public MyPriorityQueue(int initialCapacity) : base(initialCapacity, null) { }

    public MyPriorityQueue(int initialCapacity, IComparer<T> comparator) : base(initialCapacity, comparator) { }

    public MyPriorityQueue(MyPriorityQueue<T> c) : base(c) { }

    public void AddAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a));
        foreach (var item in a)
        {
            Add(item);
        }
    }


    public bool Contains(object o)
    {
        return IndexOf(o) != -1;
    }

    public bool ContainsAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a));
        foreach (var item in a)
        {
            if (!Contains(item)) return false;
        }
        return true;
    }


    public bool Remove(object o)
    {
        int index = IndexOf(o);
        if (index == -1) return false;

        RemoveAt(index);
        return true;
    }

    public bool RemoveAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a));
        bool modified = false;
        var toRemoveSet = new HashSet<T>(a);
        for (int i = count - 1; i >= 0; i--)
        {
            if (toRemoveSet.Contains(items[i]))
            {
                RemoveAt(i);
                modified = true;
            }
        }
        return modified;
    }

    public bool RetainAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a));
        var retainSet = new HashSet<T>(a);
        bool modified = false;

        for (int i = count - 1; i >= 0; i--)
        {
            if (!retainSet.Contains(items[i]))
            {
                RemoveAt(i);
                modified = true;
            }
        }
        return modified;
    }

    public int Size() => base.Count;

    public T[] ToArray(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a));

        if (a.Length < count)
        {
            return base.ToArray();
        }

        Array.Copy(items, a, count);
        if (a.Length > count)
        {
            a[count] = default(T);
        }
        return a;
    }

    public T Element() => base.Peek();

    public bool Offer(T obj)
    {
        try
        {
            Add(obj);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public new T Peek() 
    {
        return IsEmpty ? default(T) : items[0];
    }

    public T Poll()
    {
        return IsEmpty ? default(T) : RemoveRoot();
    }
}