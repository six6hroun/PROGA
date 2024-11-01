using System.Drawing;

namespace MyVector
{
    public class MyVector<T>
    {
        private T[] elementData;
        private int elementCount;
        private int capacityIncrement;

        public MyVector(int initialCapacity, int newCapacityIncrement) //№1 метод для создания пустоговектора с начальной ёмкостью initialCapacity и значением приращения ёмкости capacityIncrement.
        {
            elementData = new T[initialCapacity];
            capacityIncrement = newCapacityIncrement;
            elementCount = 0;
        }
        public MyVector(int initialCapacity) //№2 метод для создания пустого вектора с начальной ёмкостью initialCapacity и значением приращения ёмкости по умолчанию(0).

        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector() //№3 метод для создания пустого вектора с начальной ёмкостью по умолчанию(10) и значением приращения ёмкости по умолчанию(0).
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector(T[] a) //№4 метод для создания вектора и заполнения его элементами изпередаваемого массива a.

        {
            elementData = new T[a.Length];
            elementCount = a.Length;
            capacityIncrement = 0;
            for (int i = 0; i < a.Length; i++)
            {
                elementData[i] = a[i];
            }
        }
        public void Add(T e) //№5 метод для добавления элемента в конец вектора
        {
            if (elementCount == elementData.Length)
            {
                T[] newElementData;
                if (capacityIncrement == 0)
                {
                    newElementData = new T[elementData.Length * 2];
                }
                else newElementData = new T[elementData.Length + capacityIncrement];
                for (int i = 0; i < elementCount; i++)
                    newElementData[i] = elementData[i];
                elementData = newElementData;
            }
            elementData[elementCount] = e;
            elementCount++;
        }
        public void AddAll(T[] a) //№6 метод для добавления элементов из массива
        {
            foreach (var element in a)
            {
                Add(element);
            }
        }
        public void Clear() //№7 метод для удаления всех элементов из вектора.
        {
            elementCount = 0;
        }
        public bool Contains(T e) //№8 метод для проверки, находится ли указанный объект в векторе.
        {
            for (int i = 0; i < elementCount; i++)
                if (elementData[i].Equals(e)) return true;
            return false;
        }
        public bool ContainsAll(T[] a) //№9 метод для проверки, содержатся ли указанные объекты в векторе.
        {
            foreach (var element in a)
            {
                if (!Contains(element)) return false;
            }
            return true;
        }
        public bool IsEmpty() //№10 метод для проверки, является ли вектор пустым.
        {
            if (elementCount == 0) return true;
            else return false;
        }
        public void Remove(T e) //№11 метод для удаления указанного объекта из вектора, если он есть там.
        {
            for (int i = 0; i < elementCount; i++)
                if (elementData[i].Equals (e))
                {
                    for (int j = i; j < elementCount - 1; j++)
                    {
                        elementData[i] = elementData[j + 1];
                    }
                    elementCount--;
                }
        }
        public void RemoveAll(T[] a) //№12 метод для удаления указанных объектов из вектора
        {
            for (int i = 0; i < a.Length; i++)
                Remove(a[i]);
        }
        public void RetainAll(T[] a) //№13 метод для оставления в векторе только указанных объектов.
        {
            int newElementCount = 0;
            T[] newElementData = new T[elementCount];
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < elementCount; j++)
                    if (elementData[j].Equals (a[i]))
                    {
                        newElementData[newElementCount] = elementData[j];
                        newElementCount++;
                    }
            elementData = newElementData;
            elementCount = newElementCount;
        }
        public int Size() //№14 метод для получения размера вектора в элементах.
        {
            return elementCount;
        }
        public T[] ToArray() //№15 метод для возвращения массива объектов, содержащего все элементы вектора.
        {
            T[] array = new T[elementCount];
            for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
            return array;
        }
        public void ToArray(ref T[] a) //№16 метод для возвращения массива объектов, содержащего все элементы вектора
        {
            if (a == null) a = ToArray();
            Array.Copy(elementData, a, elementCount);
        }
        public void Add(int index, T e) //№17 метод для добавления элемента в указанную позицию.
        {
            if (index > elementCount) 
            { 
                Add(e); 
                return; 
            }
            T[] NewElementData = new T[elementData.Length];
            if (elementCount == elementData.Length)
            {
                if (capacityIncrement == 0) NewElementData = new T[elementCount * 2];
                else NewElementData = new T[elementCount + capacityIncrement];
            }
            for (int i = 0; i < index; i++)
            {
                NewElementData[i] = elementData[i];
            }
            NewElementData[index] = e;
            for (int i = index + 1; i < elementCount; i++)
            {
                NewElementData[i] = elementData[i - 1];
            }
            elementData = NewElementData;
            elementCount++;
        }
        public void AddAll(int index, T[] a) //№18 метод для добавления элементов в указанную позицию.
        {
            if (index > elementCount) 
            { 
                AddAll(a); 
                return; 
            }
            T[] newElementData = new T[elementData.Length];
            while (newElementData.Length - a.Length < elementCount)
            {
                if (capacityIncrement == 0) newElementData = new T[newElementData.Length * 2];
                else newElementData = new T[newElementData.Length + capacityIncrement];
            }
            for (int i = 0; i < index; i++)
            {
                newElementData[i] = elementData[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                newElementData[i + index] = a[i];
            }
            for (int i = index; i < elementCount; i++)
            {
                newElementData[i + index] = elementData[i];
            }
            elementData = newElementData;
            elementCount += a.Length;
        }
        public T Get(int index) //№19 метод для возвращения элемента в указанной позиции.
        {
            return elementData[index];
        }
        public int IndexOF(T e) //№20 метод для возвращения индекса указанного объекта, или -1, если его нет в векторе.
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (elementData[i].Equals(e)) return i;
            }
            return -1;
        }
        public int LastIndexOf(object o) //№21 метод для нахождения последнего вхождения указанного объекта, или -1, если его нет в векторе.
        {
            for (int i = elementCount - 1; i >= 0; i--)
            {
                if (elementData[i].Equals(o)) return i;
            }
            return -1;
        }
        public T Remove(int index) //№22 метод для удаления и возвращения элемента в указанной позиции.
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            Remove(elementData[index]);
            return elementData[index];
        }
        public void Set(int index, T e) //№23 метод для замены элемента в указанной позиции новым элементом.
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            elementData[index] = e;
        }
        public T[] SubList(int fromindex, int toindex) //№24 метод для возвращения части вектора, т.е. элементов в диапазоне[fromIndex; toIndex).
        {
            if ((fromindex < 0 || fromindex > elementCount) || (toindex < 0 || toindex > elementCount)) throw new ArgumentOutOfRangeException("fromindex", "toindex");
            T[] Result = new T[toindex - fromindex];
            for (int i = fromindex; i < toindex; i++)
            {
                Result[i - fromindex] = elementData[i];
            }
            return Result;
        }
        public T FirstElement() //№25 метод для обращения к первому элементу вектора.
        { 
            return elementData[0]; 
        }
        public T LastElement() //№26  метод для обращения к последнему элементу вектора.
        { 
            return elementData[elementCount - 1]; 
        }
        public void RemoveElementAt(int pos) //№27 метод для удаления элемента в заданной позиции.
        {
            if (pos < 0 || pos > elementCount) throw new ArgumentOutOfRangeException("index");
            Remove(elementData[pos]);
        }
        public void RemoveRange(int begin, int end) //№28 метод для удаления нескольких подряд идущих элементов.

        {
            if ((begin < 0 || begin > elementCount) || (end < 0 || end > elementCount)) throw new ArgumentOutOfRangeException("begin", "end");
            T[] newArray = new T[end - begin + 1];
            int index = 0;
            for (int i = begin; i < end; i++)
            {
                newArray[index] = elementData[i];
                index++;
            }
            RemoveAll(newArray);
        }
    }
}