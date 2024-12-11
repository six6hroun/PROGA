using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyArrayDeque
{
    public class MyArrayDeque<T>
    {
        private T[] elemenst;
        private int head;
        private int tail;

        public MyArrayDeque() // конструктор для создания пустой двунаправленной очереди с нач.вместимостью 16
        {
            elemenst = new T[16];
            tail = 0;
            head = 0;
        }

        public MyArrayDeque(T[] a) // конструктор для создания двунаправленной очереди из эл. передаваемого массива a
        {
            elemenst = new T[a.Length];
            head = 0;
            int i = 0;
            foreach (T item in a)
            {
                elemenst[i] = item;
                i++;
            }
            tail = i;
        }

        public MyArrayDeque(int numElements) // конструтор для создания пустой двунаправленной очереди с указанной вместимостью
        {
            elemenst = new T[numElements];
            tail = 0;
            head = 0;
        }

        public void add(T e) // метод для добавления эл. в конец двунаправленной очереди
        {
            if (tail == elemenst.Length - 1)
            {
                T[] newElements = new T[elemenst.Length * 2];
                for (int i = head; i < elemenst.Length; i++)
                {
                    newElements[i] = elemenst[i];
                }
                elemenst = newElements;
                elemenst[tail++] = e;
            }
            else elemenst[tail++] = e;
        }

        public void addAll(T[] a) // метод для добавления элементов из массива
        {
            for (int i = 0; i < a.Length; i++)
            {
                add(a[i]);
            }
        }

        public void clear() // метод для удаления всех элементов из двунаправленной очереди
        {
            head = 0;
            tail = 0;
        }

        public bool contains(object o) // метод для проверки, находится ли указанный объекст в двунаправленном списке
        {
            for (int i = head; i <= tail; i++)
            {
                if (elemenst[i].Equals(o)) return true;
            }
            return false;
        }

        public bool containsAll(T[] a) // метод для проверки, содержатся ли указанные объекты в двунаправленной очереди
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (!contains(a[i])) return false;
            }
            return true;
        }

        public bool isEmpty() // метод для проверки, является ли двунаправленная очередь пустой
        {
            if (tail == 0) return true;
            else return false;
        }

        public void remove(object o) // метод для уаления указанного объекта
        {
            if (contains(o))
            {
                int index = 0;
                for (int i = head; i < tail; i++)
                {
                    if (elemenst[i].Equals(o)) index = i;
                }
                if (index == head)
                {
                    head++; return;
                }
                else if (index == tail)
                {
                    tail--; return;
                }
                else
                {
                    T[] newElements = new T[--tail];
                    for (int i = head; i < index; i++)
                        newElements[i] = elemenst[i];
                    for (int i = index; i < tail; i++)
                        newElements[i] = elemenst[i + 1];
                    elemenst = newElements;
                }
            }
            else throw new Exception("Элемент отсутствует");
        }

        public void removeAll(T[] a) // метод для удаления указанных объектов из двунаправленной очереди
        {
            for (int i = 0; i < a.Length; i++)
            {
                remove(a[i]);
            }
        }

        public void retainAll(T[] a) // метод для оставления в двунаправленной очереди только указанных объектов
        {
            int newTail = 0;
            T[] newElements = new T[elemenst.Length];
            for (int i = 0; i < a.Length; i++)
                for (int j = head; j <= tail; j++)
                    if (elemenst[j].Equals(a[i]))
                    {
                        newElements[newTail] = elemenst[j];
                        newTail++;
                    }
            elemenst = newElements;
            tail = newTail;
        }

        public int size() // метод для получения размера двунаправленной очереди в элементах
        {
            return tail - head;
        }

        public T[] toArray() // метод для возвращения массива объектов, содержащего все элементы двунаправленной очереди
        {
            T[] a = new T[tail + 1];
            for (int i = 0; i <= tail; i++) a[i] = elemenst[i];
            return a;
        }
        public void toArray(ref T[] a) // метод для возвращения массива объектов, содержащего все элементы двунаправленной очереди
        {
            if (a == null) a = toArray();
            Array.Copy(elemenst, a, tail + 1);
        }
        public T element() // метод для возвращения элемента из головы двунаправленной очереди без его удаления
        {
            return elemenst[head];
        }
        public bool offer(T obj) // метод для попытки добавления элемента obj в двунаправленную очередь.Возвращает true, если obj добавлен, и false в противном случае
        {
            add(obj);
            if (contains(obj)) return true;
            return false;
        }
        public T peek() // метод для возврата элемента из головы двунаправленной очереди без его удаления
        {
            if (tail == 0) return default(T);
            else return elemenst[head];
        }
        public T poll() // метод для удаления и возврата элемента из головы двунаправленной очереди
        {
            if (tail == 0) return default(T);
            else
            {
                T item = elemenst[head];
                head++;
                return item;
            }
        }
        public void addFirst(T obj) // метод для добавления obj в голову двунаправленной очереди
        {
            T[] newElements = new T[elemenst.Length + 1];
            newElements[head] = obj;
            for (int i = head; i <= tail; i++) newElements[i + 1] = elemenst[i];
            elemenst = newElements;
            tail++;
        }
        public void addLast(T obj) // метод для добавления obj в хвост двунаправленной очереди
        {
            add(obj);
        }
        public T getFirst() // метод для возвращения первого элемента двунаправленной очереди без его удаления
        {
            return elemenst[head];
        }
        public T getLast() // метод для возвращения последнего элемента двунаправленной очереди без его удаления
        {
            return elemenst[tail];
        }
        public bool offerFirst(T obj) // метод для попытки добавления obj в голову двунаправленной очереди.Возвращает true, если obj добавлен, и false при попытке добавить obj в полную двунаправленную очередь ограниченной ёмкости
        {
            addFirst(obj);
            if (contains(obj)) return true;
            else return false;
        }
        public bool offerLast(T obj) // метод для попытки добавления obj в хвост двунаправленной очереди.Возвращает true, если obj добавлен, и false в противном случае
        {
            addLast(obj);
            if (contains(obj)) return true;
            else return false;
        }
        public T pop() // метод для возвращения элемента из головы двунаправленной очереди с его удалением
        {
            T item = elemenst[head];
            head++;
            return item;
        }
        public void push(T obj) // метод для добавления элемента в голову двунаправленной очереди
        {
            addFirst(obj);
        }
        public T peekFirst() // метод для возвращения элемента из головы двунаправленной очереди без его удаления
        {
            if (tail == 0) return default(T);
            else return elemenst[head];
        }
        public T peekLast() // метод для возвращения элемента из хвоста двунаправленной очереди без его удаления
        {
            if (tail == 0) return default(T);
            else return elemenst[tail];
        }
        public T pollFirst() // метод для возвращения элемента из головы двунаправленной очереди с его удалением
        {
            if (tail == 0) return default(T);
            else
            {
                T item = elemenst[head];
                head++;
                return item;
            }
        }
        public T pollLast() // метод для возвращения элемента из хвоста двунаправленной очереди с его удалением
        {
            if (tail == 0) return default(T);
            else
            {
                T item = elemenst[tail];
                tail--;
                return item;
            }
        }
        public T removeLast() // метод для возвращения элемента из конца двунаправленной очереди с его удалением
        {
            T item = elemenst[tail];
            tail--;
            return item;
        }
        public T removeFirst() // метод для возвращения элемента из головы двунаправленной очереди с его удалением
        {
            return pop();
        }
        public bool removeLastOccurrence(T obj) // метод для удаления последнего вхождения obj из двунаправленной очереди
        {
            for (int i = tail; i >= head; i--)
                if (elemenst[i].Equals(obj))
                {
                    remove(obj);
                    return true;
                }
            return false;
        }
        public bool removeFirstOccurrence(T obj) // метод для удаления первого вхождения obj из двунаправленной очереди
        {
            for (int i = head; i <= tail; i++)
                if (elemenst[i].Equals(obj))
                {
                    remove(obj);
                    return true;
                }
            return false;
        }

        public int indexOfHead()
        {
            return head;
        }

        public T get(int index)
        {
            return elemenst[index];
        }

        public void print()
        {
            for (int i = 0; i < tail; i++) Console.WriteLine(elemenst[i]);
        }

    }
}
