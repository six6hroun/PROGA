using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayList
{
    public class MyArrayList<T>
    {
        private T[] elementData;
        private int size;
        public MyArrayList() // №1 конструктор для создания пустого динамического массива
        {
            elementData = new T[10];
            size = 0;
        }

        public MyArrayList(T[] a) // №2 конструктор для создания динамического массива из переданного массива
        {
            elementData = new T[a.Length];
            for (int i = 0; i < elementData.Length; i++) elementData[i] = a[i];
            size = a.Length;
        }

        public MyArrayList(int capacity) // №3 конструктор для создания пустого динамического массива с заданной емкостью
        {
            elementData = new T[capacity];
            size = 0;
        }

        public void Add(T e) // №4 метод для добавления элемента в конец массива
        {
            if (size == elementData.Length)
            {
                T[] NewelementData = new T[(size / 2) + size + 1];
                for (int i = 0; i < elementData.Length; i++) NewelementData[i] = elementData[i];
                elementData = NewelementData;
            }
            elementData[size] = e;
            size++;
        }

        public void AddAll(T[] a) // №5 метод для добавления элементов из другого массива
        {
            foreach (var element in a)
            {
                Add(element);
            }
        }

        public void Clear() // №6 метод для очистки массива
        {
            size = 0;
        }

        public bool Contains(object o) // №7 метод для проверки наличия элемента в массиве
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o)) return true;
            }
            return false;
        }

        public bool ContainsAll(T[] a) // №8 метод для проверки наличия всех элементов из другого массива
        {
            foreach (var element in a)
            {
                if (!Contains(element)) return false;
            }
            return true;
        }

        public bool IsEmpty() // №9 метод для проверки, пуст ли массив
        {
            if (size == 0) return true;
            else return false;
        }

        public void Remove(object o) // №10 метод для удаления указанного объекта из массива
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o))
                {
                    for (int j = i; j < size - 1; j++) elementData[j] = elementData[j + 1];
                    size--;
                }
            }
        }

        public void RemoveAll(T[] a) // №11 метод для удаления всех указанных объектов из массива
        {
            foreach (var element in a)
            {
                Remove(element);
            }
        }

        public void RetainAll(T[] a) // №12 метод для оставления в массиве только указанных объектов
        {
            int newSize = 0;
            T[] newElementData = new T[size];
            for (int k = 0; k < a.Length; k++)
            {
                for (int i = 0; i < size; i++)
                    if (elementData[i].Equals(a[k]))
                    {
                        newElementData[newSize] = a[k];
                        newSize++;
                    }
            }
            size = newSize;
            elementData = newElementData;
        }

        public int Size() // №13 метод для получения размера массива
        {
            return size;
        }

        public T[] ToArray() // №14 метод для преобразования в массив
        {
            T[] result = new T[size];
            for (int i = 0; i < size; i++) result[i] = elementData[i];
            return result;
        }

        public T[] ToArray(T[] a) // №15 метод для преобразования в массив с заданным типом
        {
            if (a == null || a.Length < size) return ToArray();
            for (int i = 0; i < size; i++) a[i] = elementData[i];
            return a;
        }

        public void Add(int index, T e) // №16 метод для добавления элемента по индексу
        {
            if (index > size)
            {
                Add(e);
                return;
            }
            T[] NewElementData = new T[size + 1];
            for (int i = 0; i < index; i++) NewElementData[i] = elementData[i];
            NewElementData[index] = e;
            for (int i = index + 1; i < size; i++) NewElementData[i] = elementData[i - 1];
            elementData = NewElementData;
            size++;
        }

        public void AddAll(int index, T[] a) // №17 метод для добавления элементов из другого массива по индексу
        {
            foreach (var element in a)
            {
                Add(index++, element);
            }
        }

        public T Get(int index) // №18 метод для получения элемента по индексу
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("Index out of range");
            return elementData[index];
        }

        public int IndexOf(object o) // №19 метод для нахождения индекса указанного объекта
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }

        public int LastIndexOf(object o) // №20 метод для нахождения последнего вхождения указанного объекта
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }

        public T Remove(int index) // №21 метод для удаления элемента по индексу
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("Index out of range");
            T removedElement = elementData[index];
            for (int i = index; i < size - 1; i++) elementData[i] = elementData[i + 1];
            size--;
            return removedElement;
        }

        public void Set(int index, T e) // №22 метод для замены элемента по индексу новым элементом
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("Index out of range");
            elementData[index] = e;
        }

        public T[] SubList(int fromIndex, int toIndex) // №23 метод для получения подсписка элементов
        {
            if (fromIndex < 0 || toIndex > size || fromIndex > toIndex) throw new ArgumentOutOfRangeException("Index out of range");
            T[] subList = new T[toIndex - fromIndex];
            int index = 0;
            for (int i = fromIndex; i < toIndex; i++)
            {
                subList[index] = elementData[i];
                index++;
            }
            return subList;
        }
    }
}
