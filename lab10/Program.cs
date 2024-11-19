using MyArrayList;

namespace Lab10
{
    public class Kucha<T> where T : IComparable<T>
    {
        private int size;
        private MyArrayList<T> heap = new MyArrayList<T>(10);

        public Kucha(T[] array) //№1 конструктор, принимающий на вход массив элементов и создающий из них кучу.
        {
            size = array.Length;
            for (int i = 0; i < size; i++)
            {
                heap.Add(array[i]);
            }

            for (int i = (size / 2) - 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void Heapify(int index) 
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int parent = index;

            if (left < size && heap.Get(left).CompareTo(heap.Get(parent)) > 0)
            {
                parent = left;
            }

            if (right < size && heap.Get(right).CompareTo(heap.Get(parent)) > 0)
            {
                parent = right;
            }

            if (parent != index)
            {
                Swap(index, parent);
                Heapify(parent);
            }
        }

        public void Swap(int index1, int index2) 
        {
            T temp1 = heap.Get(index1);
            T temp2 = heap.Get(index2);
            heap.Set(index2, temp1);
            heap.Set(index1, temp2);
        }

        public T Search() //№2 метод для нахождения максимума
        {
            return heap.Get(0);
        }

        public T Extract() //№3 метод для удаления максимума
        {
            T exElement = Search();
            heap.Remove(0);
            size--;
            Heapify(0);
            return exElement;
        }

        public void IncreaseKey(int index, T newKey) //№4 метод для увелечиения ключа
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Index");
            }

            heap.Set(index, newKey);
            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void AddElement(T element) //№5 метод для добавления
        {
            heap.Add(element);
            size++;

            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void MergeHeaps(Kucha<T> newHeap) //№6 метод для слияния
        {
            while (newHeap.size > 0)
            {
                T element = newHeap.Search();
                AddElement(element);
            }

            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(heap.Get(i));
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите элементы первой кучи");
            int[] kucha1 = new int[8];
            for (int i = 0; i < kucha1.Length; i++)
            {
                kucha1[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("\nВведите элементы второй кучи");
            int[] kucha2 = new int[6];
            for (int j = 0; j < kucha2.Length; j++)
            {
                kucha2[j] = Convert.ToInt32(Console.ReadLine());
            }
            Kucha<int> heap1 = new Kucha<int>(kucha1);
            Kucha<int> heap2 = new Kucha<int>(kucha2);
            Console.WriteLine("\nРезультат:");
            Console.WriteLine("Первая куча:");
            heap1.Print();
            Console.WriteLine("Вторая куча:");
            heap2.Print();
            Console.WriteLine("Максимальный элемент первой кучи:");
            Console.WriteLine($"{heap1.Search()}");
            Console.WriteLine("Максимальный элемент второй кучи:");
            Console.WriteLine($"{heap2.Extract()}\n");
            Console.WriteLine("Вторая куча с удаленным максимальным элементом:");
            heap2.Print();
            heap1.AddElement(52);
            heap1.Print();
        }
    }
}