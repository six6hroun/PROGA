public class MyLinkedList<T>
{
    public Node<T> first;
    public Node<T> last;
    public int size;
    
    public MyLinkedList() // для создания пустого двунаправленного списка
    {
        first = null;
        last = null;
        size = 0;
    }
    public MyLinkedList(T[] a) // для создания двунаправленного списка и заполнения его элементами из передаваемого массива a
    {
        foreach (T el in a)
        {
            Add(el);
        }
    }
    public void Add(T e) // для добавления элемента в конец двунаправленного списка
    {
        Node<T> newNode = new Node<T>(e);
        if (size == 0)
        {
            first = newNode;
            last = newNode;
        }
        else
        {
            last.next = newNode;
            newNode.pred = last;
            last = newNode;
        }
        size++;
    }
    public void AddAll(T[] a) // для добавления элементов из массива
    {
        foreach (T e in a)
            Add(e);
    }
    public void Clear() // для удаления всех элементов из двунаправленного списка
    {
        first = null;
        last = null;
        size = 0;
    }
    public bool Contains(object o) // для проверки, находится ли указанный объект в двунаправленном списке
    {
        Node<T>? step = first;
        while (step != null)
        {
            if (Equals(o, step.value)) return true;
            step = step.next;
        }
        return false;
    }
    public bool ContainsAll(T[] a) // для проверки, содержатся ли указанные объекты в двунаправленном списке
    {
        foreach (T e in a)
        {
            if (!Contains(e)) return false;
        }
        return true;
    }
    public bool isEmpty() // для проверки, является ли двунаправленный список пустым
    {
        return size == 0;
    }
        public void Remove(T o) // для удаления указанного объекта из двунаправленного списка, если он есть там
    {
        if (Contains(o))
        {
            if (Equals(o, first.value))
            {
                first = first.next;
                size--;
                return;
            }
            if (Equals(o, last.value))
            {
                last = last.pred;
                size--;
                return;
            }
            Node<T>? step = first;
            while (step.next != null)
            {
                if (Equals(o, step.next.value))
                {
                    step.next = step.next.next;
                    step.next.pred = step;
                    size--;
                    return;
                }
                else step = step.next;
            }
        }
    }
    public void RemoveAll(T[] a) // для удаления указанных объектов из двунаправленного списка
    {
        foreach (T e in a)
            Remove(e);
    }
    public void RetainAll(T[] a) // для оставления в двунаправленном списке только указанных объектов
    {
        for (int index = 0; index < size; index++)
        {
            int f = 0;
            for (int j = 0; j < a.Length; j++)
            {
                if (Equals(a[j], Get(index)))
                {
                    f = 0;
                    break;
                }
                else f = 1;
            }
            if (f == 1)
                Remove(Get(index));
        }
    }
    public int Size() // для получения размера двунаправленного списка в элементах
    {
        return size;
    }
        public T[] ToArray() // для возвращения массива объектов, содержащего все элементы двунаправленного списка
    {
        T[] array = new T[size];
        for (int index = 0; index < size; index++) array[index] = Get(index);
        return array;
    }
    public T[] ToArray(ref T[] a) // для возвращения массива объектов, содержащего все элементы двунаправленного списка.Если аргумент a равен null, то создаётся новый массив, в который копируются элементы
    {
        if (a == null) return ToArray();
        else
        {
            T[] newArray = new T[a.Length + size];
            for (int i = 0; i < a.Length; i++)  newArray[i] = a[i];
            for (int i = a.Length; i < newArray.Length; i++) newArray[i] = Get(i);
            return newArray;
        }
    }
    public void Add(int index, T e) // для добавления элемента в указанную позицию
    {
        if (index <= 0)
        {
            Node<T> step = new Node<T>(e);
            step.next = first;
            first.pred = step;
            first = step;
            return;
        }
        else if (index >= size)
        {
            Node<T> step = new Node<T>(e);
            step.pred = last;
            last.next = step;
            last = step;
            return;
        }
        else
        {
            int i = 0;
            Node<T> step = new Node<T>(e);
            step = first;
            while (i != index)
            {
                step = step.next;
                i++;
            }
            Node<T> el = new Node<T>(e);
            el.next = step;
            el.pred = step.pred;
            step.pred.next = el;
            step.pred = el;
        }
    }
    public void AddAll(int index, T[] a) // для добавления элементов в указанную позицию
    {
        for (int i = 0; i < a.Length; i++) Add(index, a[i]);
    }
    public T Get(int index) // для возвращения элемента в указанной позиции
    {
        int curIndex = 0;
        if (index >= size)
            throw new Exception();
        if (index == size - 1)
            return last.value;
        if (index == 0)
            return first.value;
        Node<T>? step = first;
        while (curIndex != index)
        {
            step = step.next;
            curIndex++;
        }
        return step.value;
    }
    public int IndexOf(T o) // для возвращения индекса указанного объекта, или -1, если его нет в двунаправленном списке
    {
        Node<T> step = new Node<T>(o);
        step = first;
        int index = 0;
        while (step != null)
        {
            if (step.value.Equals(o))
                return index;
            index++;
            step = step.next;
        }
        return -1;
    }
    public int LastIndexOf(T o) // для нахождения последнего вхождения указанного объекта, или -1, если его нет в двунаправленном списке
    {
        Node<T> step = new Node<T>(o);
        step = last;
        int index = size - 1;
        while (step != null)
        {
            if (Equals(o, step.value))
                return index;
            index--;
            step = step.pred;
        }
        return -1;
    }
    public T RemoveIndex(int index) // для удаления и возвращения элемента в указанной позиции
    {
        T temp = Get(index);
        Remove(temp);
        return temp;
    }
    public void Set(int index, T e) // для замены элемента в указанной позиции новым элементом
    {
        Node<T> step = new Node<T>(e);
        step = first;
        int i = 0;
        while (i != index)
        {
            i++;
            step = step.next;
        }
        step.value = e;
    }
    public T[] SubList(int fromIndex, int toIndex) // для возвращения части двунаправленного списка
    {
        if ((fromIndex < 0 || toIndex > size) || (toIndex < fromIndex)) throw new Exception();
        else
        {
            T[] array = new T[toIndex - fromIndex + 1];
            Node<T> step = new Node<T>(first.value);
            step = first;
            int index = 0;
            while (index != fromIndex)
            {
                step = step.next;
                index++;
            }
            int indexOfArray = 0;
            while (index <= toIndex)
            {
                array[indexOfArray] = step.value;
                indexOfArray++;
                index++;
                step = step.next;
            }
            return array;
        }
    }
    public T Element() // для возвращения элемента из головы двунаправленного списка без его удаления
    {
        return first.value;
    }
    public bool Offer(T obj) // для попытки добавления элемента obj в двунаправленный список.Возвращает true, если obj добавлен, и false в противном случае
    {
        Add(obj);
        if (Contains(obj)) return true;
        return false;
    }
    public T Peek() // для возврата элемента из головы двунаправленного списка без его удаления
    {
        if (first == null) return default(T);
        return first.value;
    }
    public T Poll() // для удаления и возврата элемента из головы двунаправленного списка
    {
        T obj = first.value;
        Remove(first.value);
        return obj;
    }
    public void AddFirst(T obj) // для добавления obj в голову двунаправленного списка
    {
        Add(0, obj);
    }
    public void AddLast(T obj) // для добавления obj в хвост двунаправленного списка
    {
        Add(size, obj);
    }
    public T GetFirst() // для возвращения первого элемента двунаправленного списка без его удаления
    {
        if (first == null) throw new Exception();
        return first.value;
    }
    public T GetLast() // для возвращения последнего элемента двунаправленного списка без его удаления
    {
        if (last == null) throw new Exception();
        return last.value;
    }
    public bool OfferFirst(T obj) // для попытки добавления obj в голову двунаправленного списка
    {
        AddFirst(obj);
        if (Contains(obj)) return true;
        return false;
    }
    public bool OfferLast(T obj) // для попытки добавления obj в хвост двунаправленного списка
    {
        AddLast(obj);
        if (Contains(obj)) return true;
        return false;
    }
    public T Pop() //  для возвращения элемента из головы двунаправленного списка с его удалением
    {
        T obj = first.value;
        Remove(first.value);
        return obj;
    }
    public void Push(T obj) // для добавления элемента в голову двунаправленного списка
    {
        AddFirst(obj);
    }
    public T PeekFirst() // для возвращения элемента из головы двунаправленного списка без его удаления
    {
        if (size == 0) return default(T);
        return first.value;
    }
    public T PeekLast() // для возвращения элемента из хвоста двунаправленного списка без его удаления
    {
        if (size == 0) return default(T);
        return first.value;
    }
    public T PollFirst() // для возвращения элемента из головы двунаправленного списка с его удалением
    {
        T obj = first.value;
        Remove(first.value);
        return obj;
    }
    public T PollLast() // для возвращения элемента из хвоста двунаправленного списка с его удалением
    {
        T obj = last.value;
        Remove(last.value);
        return obj;
    }
    public T RemoveFirst() // для возвращения элемента из начала двунаправленного списка с его удалением
    {
        T obj = first.value;
        Remove(first.value);
        return obj;
    }
    public T RemoveLast() // для возвращения элемента из конца двунаправленного списка с его удалением
    {
        T obj = last.value;
        Remove(last.value);
        return obj;
    }
    public bool RemoveLastOccurrence(T obj) //  для удаления последнего вхождения obj из двунаправленного списка
    {
        int ind = LastIndexOf(obj);
        if (ind != -1)
        {
            RemoveIndex(ind);
            return true;
        }
        return false;
    }
    public bool RemoveFirstOccurrence(T obj) // для удаления первого вхождения obj из двунаправленного списка
    {
        int index = IndexOf(obj);
        if (index != -1)
        {
            RemoveIndex(index);
            return true;
        }
        return false;
    }
}
