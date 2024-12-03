using MyArrayList;
public class MyPriorityQueue<T> where T : IComparable<T>
{
    private MyArrayList<T> queue;
    private int size;
    private Comparer<T> comparator;

    public MyPriorityQueue() // конструктор для создания пустой очереди с приоритетами
    {
        queue = new MyArrayList<T>(11);
        size = 0;
        comparator = Comparer<T>.Default;
    }

    public void Queuelify(int index)
    {
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        int parent = index;

        if (left < size && comparator.Compare(queue.Get(left), queue.Get(parent)) > 0)
        {
            parent = left;
        }

        if (right < size && comparator.Compare(queue.Get(right), queue.Get(parent)) > 0)
        {
            parent = right;
        }

        if (parent != index)
        {
            Swap(index, parent);
            Queuelify(parent);
        }
    }

    public void Swap(int index1, int index2) // конструктор  для создания пустой очереди с приоритетами
    {
        T temp1 = queue.Get(index1);
        T temp2 = queue.Get(index2);
        queue.Set(index2, temp1);
        queue.Set(index1, temp2);
    }

    public MyPriorityQueue(T[] a) // конструктор для создания очереди с приоритетами
    {
        queue = new MyArrayList<T>(a.Length);
        size = a.Length;
        comparator = Comparer<T>.Default;
        for (int i = 0; i < size; i++)
        {
            queue.Add(a[i]);
        }
        for (int i = size / 2; i >= 0; i--)
        {
            Queuelify(i);
        }
    }

    public MyPriorityQueue(int initialCcapacity) // конструктор для создания пустой очереди с приоритетами с указанной начальной ёмкостью
    {
        queue = new MyArrayList<T>(initialCcapacity);
        size = 0;
        comparator = Comparer<T>.Default;
    }

    public MyPriorityQueue(int initialCcapacity, Comparer<T> comparator) // конструктор для создания пустой очереди с приоритетами с указанной начальной ёмкостью и компаратором
    {
        queue = new MyArrayList<T>(initialCcapacity);
        size = initialCcapacity;
        this.comparator = comparator;
    }

    public MyPriorityQueue(MyPriorityQueue<T> c) // конструктор для создания очереди с приоритетами, содержащей элементы указанной очереди с приоритетами
    {
        queue = new MyArrayList<T>(c.size);
        size = c.size;
        comparator = c.comparator;
        for (int i = 0; i < c.size; i++)
        {
            queue.Add(c.queue.Get(i));
        }
        size = c.size;

        for (int i = size / 2; i >= 0; i--)
        {
            Queuelify(i);
        }
    }

    public void Add(T e) // метод для добавления элемента в конец очереди с приоритетами
    {
        queue.Add(e);
        size++;
        for (int i = size / 2; i >= 0; i--)
        {
            Queuelify(i);
        }
    }

    public void AddAll(T[] a) // метод для добавления элементов из массива
    {
        queue.AddAll(a);
        size += a.Length;
        for (int i = size / 2; i >= 0; i--)
        {
            Queuelify(i);
        }
    }

    public void Clear() // метод для удаления всех элементов из очереди с приоритетами
    {
        queue.Clear();
        size = 0;
    }

    public bool Contains(object o) // метод для проверки, находится ли указанный объект в очереди с приоритетами
    {
        return queue.Contains(o);
    }

    public bool ContainsAll(T[] a) // метод для  проверки, содержатся ли указанные объекты в очереди с приоритетами
    {
        return queue.ContainsAll(a);
    }

    public bool IsEmpty() // метод для проверки, является ли очередь с приоритетами пустой
    {
        return queue.IsEmpty();
    }

    public void Remove(object o) // метод для удаления указанного объекта из очереди с приоритетами, если он есть там
    {
        queue.Remove(o);
        size--;
        for (int i = size / 2; i >= 0; i--)
        {
            Queuelify(i);
        }
    }

    public void RemoveAll(T[] a) // метод для удаления указанных объектов из очереди с приоритетами
    {
        queue.RemoveAll(a);
        size -= a.Length;
        for (int i = size / 2; i >= 0; i--)
        {
            Queuelify(i);
        }
    }

    public void RetainAll(T[] a) // метод для оставления в очереди с приоритетами только указанных объектов
    {
        queue.RetainAll(a);
        size = a.Length;
        for (int i = size / 2; i >= 0; i--)
        {
            Queuelify(i);
        }
    }

    public int Size() // метод для получения размера очереди с приоритетами в элементах.
    {
        return queue.Size();
    }

    public T[] ToArray() // метод для возвращения массива объектов, содержащего все элементы очереди с приоритетами
    {
        return queue.ToArray();
    }

    public T[] ToArray(T[] a) // метод для возвращения массива объектов, содержащего все элементы очереди с приоритетами

    {
        return queue.ToArray(a);
    }

    public T Element() // метод для возвращения элемента из головы очереди с приоритетами без его удаления
    {
        return queue.Get(0);
    }

    public bool Offer(T obj) // метод для попытки добавления элемента obj в очередь с приоритетами. Возвращает true если добавлен и false если нет
    {
        queue.Add(obj);
        size++;
        for (int i = size / 2; i >= 0; i--)
        {
            Queuelify(i);
        }

        bool f = true;
        if (queue.Contains(obj)) return f;
        else return f == false;
    }

    public T Peek() // метод для возврата элемента из головы очереди с приоритетами без его удаления
    {
        if (size == 0) throw new ArgumentOutOfRangeException();
        else return queue.Get(0);
    }

    public T Poll() // метод для удаления и возврата элемента из головы очереди с приоритетами
    {
        if (size == 0) throw new ArgumentOutOfRangeException();
        else
        {
            T element = queue.Get(0);
            queue.Remove(element);
            size--;
            for (int i = size / 2; i >= 0; i--)
            {
                Queuelify(i);
            }
            return element;
        }
    }

    public void Print()
    {
        for (int i = 0; i < size; i++)
            Console.WriteLine(queue.Get(i));
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Введите элементы кучи:");
        int[] array = new int[8];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine();
        MyPriorityQueue<int> queue = new MyPriorityQueue<int>(array);
        queue.Print();
        Console.WriteLine(queue.Offer(30));
        queue.Print();
        Console.WriteLine("Удаленный и возвращенный элемент головы");
        Console.WriteLine(queue.Poll());
        Console.WriteLine();
        queue.Print();
    }
}