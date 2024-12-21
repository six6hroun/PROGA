
    public class MyHashMap<K, V>
    {
        private class Entry
        {
            public K key { get; set; }
            public V value { get; set; }
            public Entry next { get; set; }
            public Entry(K key, V value)
            {
                this.key = key;
                this.value = value;
                next = null;
            }
        }
        Entry[] table;
        int size;
        double loadFactor;
        public MyHashMap() // консруктор для создания пустого отображения с начальной ёмкостью 16 и коэффициентом загрузки 0,75;
        {
            table = new Entry[16];
            size = 0;
            this.loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity) // консруктор для создания пустого отображения с указанной начальной ёмкостью и коэффициентом загрузки 0,75;
        {
            table = new Entry[initialCapacity];
            size = 0;
            this.loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity, double loadFactor) // консруктор для создания пустого отображения с указанной начальной ёмкостью и коэффициентом загрузки;
        {
            table = new Entry[initialCapacity];
            size = 0;
            this.loadFactor = loadFactor;
        }
        public void Clear() // метод для удаления всех пар «ключ-значение» из отображения;
        {
            Array.Clear(table);
            size = 0;
        }
        public bool ContainsKey(K key) // метод для проверки, содержит ли отображение указанный ключ;
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;
            Entry step = table[index];
            while (step != null)
            {
                if (Equals(step.key, key)) return true;
                step = step.next;
            }
            return false;
        }
        public bool ContainsValue(V value) // метод для проверки, содержит ли отображение указанное значение;
        {
            for (int index = 0; index < table.Length; index++)
            {
                Entry step = table[index];
                while (step != null)
                {
                    if (Equals(step.value, value)) return true;
                    step = step.next;
                }
            }
            return false;
        }
        public HashSet<object> EntrySet() // метод для возврата множества (Set) всех пар «ключ-значение» в отображении;

        {
            HashSet<object> entries = new HashSet<object>();
            for (int index = 0; index < table.Length; index++)
            {
                Entry step = table[index];
                while (step != null)
                {
                    entries.Add(step);
                    step = step.next;
                }
            }
            return entries;
        }
        public V Get(K key) // метод для возврата значения, связанного с указанным ключом, или null, если ключ не найден;
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;
            Entry step = table[index];
            while (step != null)
            {
                if (Equals(step.key, key)) return step.value;
                step = step.next;
            }
            throw new Exception("Ключ не найден");
        }
        public bool IsEmpty() // метод для проверки, является ли отображение пустым;
        {
            return size == 0;
        }
        public K[] KeySet() // метод для возврата множества (Set) всех ключей в отображении;
        {
            K[] array = new K[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Entry step = table[i];

                    while (step != null)
                    {
                        array[index] = step.key;
                        index++;
                        step = step.next;
                    }
                }
            }
            return array;
        }

        public void Put(K key, V value) // метод для добавления пары «ключ-значение» в отображение;
        {
            double count = (size + 1) / table.Length;
            if (count >= loadFactor)
            {
                Entry[] newArray = new Entry[table.Length * 3];
                size = 0;

                for (int i = 0; i < table.Length; i++)
                {
                    if (table[i] != null)
                    {
                        Entry val = table[i];
                        while (val != null)
                        {
                            int index = Math.Abs(val.key.GetHashCode()) % newArray.Length;
                            PutInArray(newArray, val.key, val.value);
                            val = val.next;
                        }
                    }
                }
                table = newArray;
            }
            int index1 = Math.Abs(key.GetHashCode()) % table.Length;
            Entry step = table[index1];
            if (step != null)
            {
                int f = 1;
                while (step.next != null)
                {
                    if (Equals(step.key, key))
                    {
                        step.value = value;
                        f = 0;
                    }
                    step = step.next;
                }
                if (f == 1)
                {
                    Entry newNode = new Entry(key, value);
                    step.next = newNode;
                    size++;
                }
            }
            else
            {
                Entry newNode = new Entry(key, value);
                table[index1] = newNode;
                size++;
            }
        }

        private void PutInArray(Entry[] array, K key, V value) // метод для добавления новой пары ключ-значение в массив 
        {
            int index = Math.Abs(key.GetHashCode()) % array.Length;
            Entry newNode = new Entry(key, value);
            if (array[index] != null)
            {
                Entry step = array[index];
                while (step.next != null)
                    step = step.next;
                step.next = newNode;
            }
            else
                array[index] = newNode;
            size++;
        }

        public void Remove(K key) // метод для удаления пары «ключ-значение» с указанным ключом из отображения;
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;
            if (table[index] == null) return;
            if (Equals(key, table[index].key))
            {
                table[index] = table[index].next;
                size--;
                return;
            }
            Entry step = table[index];
            Entry pred = null;
            while (step != null)
            {
                if (Equals(step.key, key))
                {
                    pred.next = step.next;
                    size--;
                    return;
                }
                pred = step;
                step = step.next;
            }
        }

        public int Size() // метод  для возврата количества пар «ключ-значение» в отображении.
        {
            return size;
        }

        public void Print()
        {
            for (int index = 0; index < table.Length; index++)
            {
                Entry step = table[index];
                while (step != null)
                {
                    Console.Write($"{step.key} ");
                    step = step.next;
                }
            }
        }
    }
    public class program
    {
        static void Main(string[] a)
        {
            MyHashMap<string, int> Hash = new MyHashMap<string, int>();
            Hash.Put("Gurgen", 19);
            Hash.Put("Umar", 18);
            Hash.Put("Nikita", 21);
            Hash.Put("Artem", 20);
            Hash.Put("Tigran", 30);
            Console.Write("Элементы хеш-карты - ");
            Hash.Print();
            Hash.Remove("Umar");
            Console.WriteLine($"\nЕсть ли данное значение на данный момент - {Hash.ContainsValue(18)}");
            Console.Write("Элменеты хеш-карты после удаления - ");
            Hash.Print();
            Console.WriteLine($"\nЕсть ли данное значение на данный момент - {Hash.ContainsValue(19)}");
        }
    }





using MyHashMap;
using System.Reflection;
using System.Text.RegularExpressions;
namespace MyHashMap
{
    public class Lab5
    {
        static string file = "Z:/input.txt";
        static StreamReader sr = new StreamReader(file);
        static public MyHashMap<string, int> Teg()
        {
            string line = sr.ReadLine();
            if (line == null) throw new Exception("Пустая строка");
            var result = new MyHashMap<string, int>();
            while (line != null)
            {
                bool isOpen = false;
                bool isTeg = false;
                string teg = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '<' && line[i + 1] != null)
                    {
                        if (line[i + 1] == '/' || char.IsLetter(line[i + 1]) && !isOpen) isOpen = true;
                        if (line[i + 1] == '>') isOpen = true;
                    }
                    if (line[i] == '>' && isOpen == true) 
                    { 
                        teg += line[i]; 
                        isTeg = true; 
                        isOpen = false; 
                    }
                    if (isOpen && (line[i] == '<' || line[i] == '/' || char.IsLetter(line[i]) || char.IsDigit(line[i]))) teg += line[i];
                    if (isTeg)
                    {
                        string T1 = teg.Replace("/", "");
                        string T = T1.ToLower();
                        if (result.ContainsKey(T))
                        {
                            int cnt = result.Get(T);
                            result.PutValue(T, cnt + 1);

                        }
                        else
                        {
                            result.Put(T, 1);
                        }
                        teg = "";
                        isTeg = false;
                    }
                }
                line = sr.ReadLine();
            }
            return result;
        }
        static void Main(string[] args)
        {
            var array = Teg();
            var teg = array.KeySet().ToArray();
            for (int i = 0; i < teg.Length; i++)
            {
                Console.WriteLine($"{teg[i]} : {array.Get(teg[i])}");
            }
        }
    }
}
