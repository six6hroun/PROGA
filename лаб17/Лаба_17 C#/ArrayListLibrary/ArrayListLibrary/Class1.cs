using System.Collections.Generic;
using System.Linq;
using System;

namespace ArrayListLibrary
{
    public class MyArrayList<T>
    {
        private T[] elementData; // Внутренний массив 
        private int size; // Размер 

        // Создание пустого динамического массива
        public MyArrayList()
        {
            size = 0;
            elementData = new T[0];
        }

        // Заполнение динамического массива из передаваемого массива 'a'
        public MyArrayList(T[] a)
        {
            elementData = a;
            size = a.Length;
            Array.Copy(a, elementData, size);
        }

        // Создание пустого динамического массива с внутренним массивом, размер которого равен значению параметра 'capacity'
        public MyArrayList(int capacity)
        {
            size = 0;
            elementData = new T[capacity];
        }

        // Добавление элемента в конец динамического массива с условием.
        public void add(T e)
        {
            if (size >= elementData.Length)
            {
                T[] NewElementData = new T[size + 1 + (size / 2)];
                for (int i = 0; i < size; i++)
                {
                    NewElementData[i] = elementData[i];
                }
                elementData = NewElementData;
            }
            elementData[size] = e;
            size++;
        }

        // Добавление элементов из массива
        public void addAll(T[] a)
        {
            foreach (T e in a) // из динамического массива 'T' добавляем элементы 'e' в коллекцию 'a' 
            {
                this.add(e);
            }
        }

        // Удаление всех элементов из динамического массива
        public void clear()
        {
            size = 0;
        }

        // Проверка, находится ли указанный объект в динамическом массиве
        public bool contains(object o)
        {
            bool f = false;
            foreach (T e in elementData)
            {
                if (e.Equals(o)) return f == true;
            }
            return f == false;
        }

        // Проверка, содержатся ли указанные объекты в динамическом массиве
        public bool containsAll(T[] a)
        {
            bool f = false;
            foreach (T e in a)
            {
                if (elementData.Contains(e)) return f == true;
            }
            return f == false;
        }

        // Проверка, является ли динамический массив пустым
        public bool isEmpty()
        {
            if (size == 0) return true;
            else return false;
        }

        // Удаление указанного объекта из динамического массива, если он есть там
        public void remove(params object[] o)
        {
            foreach (object array in o)
            {
                int i = 0;
                while (i < size)
                {
                    if (array.Equals((object)elementData[i]))
                    {
                        for (int j = i; j < size - 1; j++) elementData[j] = elementData[j + 1];
                        size--;
                    }
                    i++; ;
                }
            }
        }


        // Удаление указанных объектов из динамического массива
        public void removeAll(T[] a)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < elementData.Length; i++)
            {
                if (!a.Contains(elementData[i])) list.Add(elementData[i]);
            }

            T[] NewElementData = new T[list.Count];
            for (int i = 0; i < list.Count; i++) NewElementData[i] = list[i];
            elementData = NewElementData;
        }

        // Оставление в динамическом массиве только указанных объектов
        public void retainAll(T[] a)
        {
            List<T> NewElementData = new List<T>();
            foreach (T e in a)
            {
                NewElementData.Add(e);
            }

            for (int i = size - 1; i >= 0; i--)
            {
                if (!NewElementData.Contains(elementData[i]))
                {
                    for (int j = i; j < size - 1; j++)
                    {
                        elementData[j] = elementData[j + 1];
                    }
                    size--;
                }
            }
        }

        // Возвращение размера динамического массива в элементах
        public int _size()
        {
            return size;
        }

        // Возвращение массива объектов, содержащего все элементы динамического массива
        public T[] toArray()
        {
            T[] NewElementData = new T[size];
            Array.Copy(elementData, NewElementData, size);
            return NewElementData;
        }

        // Тот же метод, что и предыдущий, но с условием - если аргумент a равен null, то создаётся новый массив, в который копируются элементы.
        public T[] toArray(T[] a)
        {
            if (a == null)
            {
                T[] NewElementData = new T[size];
                Array.Copy(elementData, NewElementData, size);
                return NewElementData;
            }
            else
            {
                Array.Copy(elementData, a, size);
                return a;
            }
        }

        // Добавление элемента в указанную позицию
        public void add(int index, T e)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимого диапазона.");
            }

            for (int i = size; i > index; i--)
            {
                elementData[i] = elementData[i - 1];
            }
            elementData[index] = e;
            size++;
        }

        // Добавление элементов в указанную позицию
        public void addAll(int index, T[] a)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимого диапазона.");
            }

            for (int i = size; i > index; i--)
            {
                elementData[i] = elementData[i - 1];
            }
            for (int i = 0; i < a.Length; i++)
            {
                elementData[index + i] = a[i];
            }
            size += a.Length;
        }

        // Возвращение элемента в указанной позиции
        public T get(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимого диапазона.");
            }
            return elementData[index];
        }

        // Возвращение индекса указанного объекта, или -1, если его нет в динамическом массиве
        public int indexOf(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }

        // Нахождение последнего вхождения указанного объекта, или -1, если его нет в динамическом массиве
        public int lastIndexOf(object o)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }

        // Удаление и возвращения элемента в указанной позиции
        public object removeIndex(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимого диапазона.");
            }

            object RemovedElement = elementData[index];
            for (int i = index; i < size - 1; i++)
            {
                elementData[i] = elementData[i + 1];
            }
            size--;
            return RemovedElement;
        }

        // Замена элемента в указанной позиции новым элементом
        public void set(int index, T e)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Индекс вне допустимого диапазона.");
            }

            elementData[index] = e;
        }

        // Возвращение элементов из указанного диапазона
        public MyArrayList<T> subList(int start, int end)
        {
            if (start < 0 || end < 0 || start >= size || end >= size || start > end)
            {
                throw new IndexOutOfRangeException("Указанный диапазон вне допустимого диапазона.");
            }

            MyArrayList<T> subList = new MyArrayList<T>();
            for (int i = start; i <= end; i++)
            {
                subList.add(elementData[i]);
            }
            return subList;
        }

        // Вывод нашего массива
        public void Print()
        {
            for (int i = 0; i < size; i++) Console.Write(elementData[i] + " ");
            Console.WriteLine();
        }
    }
}
