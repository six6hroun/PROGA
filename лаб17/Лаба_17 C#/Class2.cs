using System;

namespace LinkedListLibrary
{
    public class MyLinkedList<T>
    {
        private class Element<T>
        {
            public T value;
            public Element<T> next;
            public Element<T> pred;
            public Element(T element)
            {
                next = null;
                pred = next;
                value = element;
            }
        }

        private Element<T> first;
        private Element<T> last;
        private int size;

        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }

        public MyLinkedList(T[] a)
        {
            foreach (T item in a)
            {
                Add(item);
            }
        }

        public MyLinkedList(int capacity)
        {
            first = null;
            last = null;
            size = capacity;
        }

        public void Add(T e)
        {
            Element<T> newLinkedList = new Element<T>(e);
            if (size == 0)
            {
                first = newLinkedList;
                last = newLinkedList;
            }
            else
            {
                last.next = newLinkedList;
                newLinkedList.pred = last;
                last = newLinkedList;
            }
            size++;
        }

        public void AddAll(T[] a)
        {
            foreach (T item in a)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }

        public bool Contains(object o)
        {
            Element<T> step = first;
            while (step != null)
            {
                if (step.value.Equals(o)) return true;
                step = step.next;
            }
            return false;
        }

        public bool Contains(T[] a)
        {
            bool[] check = new bool[a.Length];
            Element<T> step = first;
            while (step != null)
            {
                int i = 0;
                if (step.Equals(a[i])) check[i] = true;
                i++;
                step = step.next;
            }
            for (int i = 0; i < check.Length; i++)
            {
                if (!check[i]) return false;
            }
            return true;
        }

        public bool IsEmpty()
        {
            if (size == 0) return true;
            else return false;
        }

        public void Remove(T o)
        {
            if (Contains(o))
            {
                if (first.value.Equals(o))
                {
                    first = first.next;
                    size--;
                    return;
                }
                Element<T> step = first;
                while (step != null)
                {
                    if (step.next.value.Equals(o))
                    {
                        step.next = step.next.next;
                        size--;
                        return;
                    }
                    else step = step.next;
                }
            }
        }

        public void RemoveAll(T[] a)
        {
            foreach (T item in a)
            {
                Remove(item);
            }
        }

        public T Get(int index)
        {
            int currentIndex = 0;
            if (index >= size) throw new IndexOutOfRangeException();
            if (index == size - 1) return last.value;
            if (index == 0) return first.value;
            Element<T> step = first;
            while (currentIndex != index)
            {
                step = step.next;
                currentIndex++;
            }
            return step.value;
        }

        public void RetainAll(T[] a)
        {
            for (int i = 0; i < size; i++)
            {
                int flag = 0;
                for (int j = 0; j < a.Length; j++)
                {
                    if (Get(i).Equals(a[j]))
                    {
                        flag = 0;
                        break;
                    }
                    else flag = 1;
                }
                if (flag == 1) Remove(Get(i));
            }
        }

        public int Size()
        {
            return size;
        }

        public T[] ToArray()
        {
            T[] newArray = new T[size];
            for (int i = 0; i < size; i++) newArray[i] = Get(i);
            return newArray;
        }

        public T[] ToArray(T[] a)
        {
            if (a == null) return ToArray();
            else
            {
                T[] newArray = new T[a.Length + size];
                for (int i = 0; i < a.Length; i++) newArray[i] = a[i];
                for (int i = a.Length; i < newArray.Length; i++) newArray[i] = Get(i);
                return newArray;
            }
        }

        public void Add(int index, T e)
        {
            if (index == 0)
            {
                Element<T> step = new Element<T>(e);
                step.next = first;
                first.pred = step;
                first = step;
                return;
            }
            else if (index == size - 1)
            {
                Element<T> step = new Element<T>(e);
                step.pred = last;
                last.next = step;
                last = step;
                return;
            }
            else
            {
                int tind = 0;
                Element<T> step = new Element<T>(e);
                step = first;
                while (tind != index)
                {
                    step = step.next;
                    tind++;
                }
                if (tind == index)
                {
                    Element<T> element = new Element<T>(e);
                    element.next = step;
                    element.pred = step.pred;
                    step.pred.next = element;
                    step.pred = element;
                }
            }
        }

        public void AddAll(int index, T[] a)
        {
            for (int i = a.Length - 1; i >= 0; i--) Add(index, a[i]);
        }

        public int IndexOf(T o)
        {
            Element<T> step = new Element<T>(o);
            step = first;
            int i = 0;
            while (step != null)
            {
                if (step.value.Equals(o)) return i;
                i++;
                step = step.next;
            }
            return -1;
        }

        public int LastIndexOf(T o)
        {
            Element<T> step = new Element<T>(o);
            step = first;
            int lastI = -1;
            int i = 0;
            while (step != null)
            {
                if (step.value.Equals(o)) lastI = i;
                i++;
                step = step.next;
            }
            return lastI;
        }

        public T Remove(int index)
        {
            T item = Get(index);
            Remove(item);
            return item;
        }

        public void Set(int index, T e)
        {
            Element<T> step = new Element<T>(e);
            step = first;
            int i = 0;
            while (i != index)
            {
                i++;
                step = step.next;
            }
            step.value = e;
        }

        public T[] SubList(int fromIndex, int toIndex)
        {
            T[] a = new T[toIndex - fromIndex + 1];
            Element<T> step = new Element<T>(first.value);
            step = first;
            int i1 = 0;
            while (i1 != fromIndex)
            {
                i1++;
                step = step.next;
            }
            int i2 = 0;
            while (i1 <= toIndex)
            {
                i2++;
                i1++;
                a[i2] = step.value;
                step = step.next;
            }
            return a;
        }

        public T Element_()
        {
            return first.value;
        }

        public bool Offer(T obj)
        {
            Add(obj);
            if (Contains(obj)) return true;
            return false;
        }

        public T Peek()
        {
            if (first == null) return default;
            return first.value;
        }

        public T Poll()
        {
            if (first == null) return default;
            T item = first.value;
            Remove(item);
            return item;
        }

        public void AddFirst(T obj)
        {
            Add(0, obj);
        }

        public void AddLast(T obj)
        {
            Add(size - 1, obj);
        }

        public T GetFirst()
        {
            return first.value;
        }

        public T GetLast()
        {
            return last.value;
        }

        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            if (Contains(obj)) return true;
            return false;
        }

        public bool OfferLast(T obj)
        {
            AddLast(obj);
            if (Contains(obj)) return true;
            return false;
        }

        public T Pop()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        public void Push(T obj)
        {
            AddFirst(obj);
        }

        public T PeekFirst()
        {
            if (first == null) return default;
            return first.value;
        }

        public T PeekLast()
        {
            if (first == null) return default;
            return last.value;
        }

        public T PollFirst()
        {
            if (first == null) return default;
            T item = first.value;
            Remove(item);
            return item;
        }

        public T PollLast()
        {
            if (first == null) return default;
            T item = last.value;
            Remove(item);
            return item;
        }

        public T RemoveFirst()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        public T RemoveLast()
        {
            T item = last.value;
            Remove(item);
            return item;
        }

        public bool RemoveLastOccurrence(T obj)
        {
            int i = LastIndexOf(obj);
            if (i != -1)
            {
                Remove(i);
                return true;
            }
            return false;
        }

        public bool RemoveFirstOccurrence(T obj)
        {
            int i = IndexOf(obj);
            if (i != -1)
            {
                Remove(i);
                return true;
            }
            return false;
        }
    }
}
