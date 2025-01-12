using System.Collections.Generic;
using System.Xml.Linq;

namespace MyTreeMap
{
    public class MyTreeMap<K, V> where K : IComparable<K>
    {
        private class TreeNode
        {
            public K key;
            public V value;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(K key, V value)
            {
                this.key = key;
                this.value = value;
                left = null;
                right = null;
            }
        }

        private TreeNode? root;
        private int size;
        private IComparer<K> comparator;

        public MyTreeMap() // конструктор для создания пустого отображения, размещающего элементы согласно естественному порядку сортировки
        {
            comparator = Comparer<K>.Default;
            root = null;
            size = 0;
        }

        public MyTreeMap(IComparer<K> comparator) // конструктор  для создания пустого отображения, размещающего элементы согласно указанному компаратору
        {
            this.comparator = comparator;
            root = null;
            size = 0;
        }

        public void Clear() // метод для удаления всех пар «ключ-значение» из отображения
        {
            root = null;
            size = 0;
        }

        public bool ContainsKey(object key) // метод для проверки, содержит ли отображение указанный ключ
        {
            return GetNode((K)key) != null;
        }

        // Воспомогательный метод которы возвращает узел дерева (родителя) по указанному ключу
        private TreeNode GetNode(K key)
        {
            return GetNode(root, key);
        }

        private TreeNode GetNode(TreeNode root, K key)
        {
            if (root == null) return null;
            int comp = comparator.Compare(key, root.key); 
            if (comp < 0) return GetNode(root.left, key);
            else if (comp > 0) return GetNode(root.right, key);
            else return root;
        }

        public bool ContainsValue(object value) // метод для проверки, содержит ли отображение указанное значение
        {
            return ContainsValue(root, value);
        }

        private bool ContainsValue(TreeNode root, object value)
        {
            if (root == null) return false;
            if (EqualityComparer<V>.Default.Equals(root.value, (V)value)) return true;
            return ContainsValue(root.left, value) || ContainsValue(root.right, value);
        }

        public HashSet<KeyValuePair<K, V>> EntrySet() // метод для возврата множества(Set) всех пар «ключ-значение» в отображении
        {
            var entries = new HashSet<KeyValuePair<K, V>>();
            EntrySet(root, entries);
            return entries;
        }

        // воспомогательный метод для рекурсивного добавления всех пар ключей в дереве в множество и в дальнейшем его вернуть
        private void EntrySet(TreeNode root, HashSet<KeyValuePair<K, V>> entries)
        {
            if (root != null)
            {
                entries.Add(new KeyValuePair<K, V>(root.key, root.value)); // Добавляем текущий узел
                EntrySet(root.left, entries);
                EntrySet(root.right, entries);
            }
        }

        public V? Get(object key) // метод для возврата значения, связанного с указанным ключом, или null, если ключ не найден
        {
            TreeNode root = GetNode((K)key);
            if (root.key.Equals(key)) return root.value;
            else return default;
        }

        public bool IsEmpty() // метод для проверки, является ли отображение пустым
        {
            if (size == 0) return true;
            else return false;
        }

        public HashSet<K> KeySet() // метод для возврата множества (Set) всех ключей в отображении
        {
            var keys = new HashSet<K>();
            KeySet(root, keys);
            return keys;
        }

        // воспомогательный метод для рекурсивного добавления всех ключей в дереве в множество 
        private void KeySet(TreeNode root, HashSet<K> keys)
        {
            if (root != null)
            {
                keys.Add(root.key);
                KeySet(root.left, keys);
                KeySet(root.right, keys);
            }
        }

        public void Put(K key, V value) // метод для добавления пары «ключ-значение» в отображение
        {
            root = Put(root, key, value);
            size++;
        }

        private TreeNode Put(TreeNode root, K key, V value)
        {
            if (root == null) return new TreeNode(key, value);

            int comp = comparator.Compare(key, root.key);
            if (comp < 0) root.left = Put(root.left, key, value);
            else if (comp > 0) root.right = Put(root.right, key, value);
            else root.value = value;
            return root;
        }

        public bool Remove(K key) // метод для удаления пары «ключ-значение» с указанным ключом из отображения
        {
            if (ContainsKey(key))
            {
                root = Remove(root, key);
                size--;
                return true;
            }
            else return false;
        }

        private TreeNode Remove(TreeNode root, K key)
        {
            if (root == null) return null;
            int comp = comparator.Compare(key, root.key);
            if (comp < 0) root.left = Remove(root.left, key);
            else if (comp > 0) root.right = Remove(root.right, key);
            else // нашел узел, в котором нужно удалить дочерний с указанным ключом (ключ равен данному ключу)
            {
                // удаление узла
                if (root.left == null) return root.right;
                if (root.right == null) return root.left;
                // узел с двумя поддеревами (имеет и левое и правое))
                TreeNode minNode = GetMin(root.right); // Находит минимальный узел в правом поддереве
                // заменяем узел с указанным ключом на минимальный в правом поддереве
                root.key = minNode.key;
                root.value = minNode.value;
                root.right = Remove(root.right, minNode.key); // Затем удаляем минимальный узел ибо он стал лишним
            }
            return root;
        }

        // Воспомогательный метод для получение левого поддерева (наименьшего)
        private TreeNode GetMin(TreeNode root)
        {
            while (root.left != null) root = root.left;
            return root;
        }

        // Воспомогательный метод для получение правого поддерева (наибольшего)
        private TreeNode GetMax(TreeNode root)
        {
            while (root.right != null) root = root.right;
            return root;
        }

        public int Size() // метод для возврата количества пар «ключ-значение» в отображении
        {
            return size;
        }

        public K FirstKey() // метод для возврата первого ключа отображения;
        {
            if (IsEmpty()) throw new InvalidOperationException("TreeMap is empty.");
            return GetMin(root).key;
        }

        public K LastKey() // метод для возврата последнего ключа отображения
        {
            if (IsEmpty()) throw new InvalidOperationException("TreeMap is empty.");
            return GetMax(root).key;
        }

        public MyTreeMap<K, V> HeadMap(K end) // метод для возврата сортированного отображения, содержащего элементы, ключ которых меньше end
        {
            MyTreeMap<K, V> result = new MyTreeMap<K, V>(comparator);
            HeadMap(root, end, result);
            return result;
        }

        private void HeadMap(TreeNode root, K end, MyTreeMap<K, V> result)
        {
            if (root == null) return;

            int comp = comparator.Compare(root.key, end);
            // Если текущий ключ меньше заданного 'end', то начинаем сортировать дерево. Иначе возвращаемся в левое поддерево
            if (comp < 0)
            {
                result.Put(root.key, root.value);
                HeadMap(root.left, end, result);
                HeadMap(root.right, end, result);
            }
            else HeadMap(root.left, end, result);
        }

        public MyTreeMap<K, V> SubMap(K start, K end) // метод для возврата отображения, содержащего элементы, чей ключ больше или равен start и меньше end
        {
            MyTreeMap<K, V> result = new MyTreeMap<K, V>(comparator);
            SubMap(root, start, end, result);
            return result;
        }

        private void SubMap(TreeNode root, K start, K end, MyTreeMap<K, V> result)
        {
            if (root == null) return;

            int compStart = comparator.Compare(root.key, start);
            int compEnd = comparator.Compare(root.key, end);
            // Если текущий ключ удовлетворяет условиям наших заданных ключей 'start' и 'end', то добавляем этот узел в результат
            if (compStart >= 0 && compEnd < 0) result.Put(root.key, root.value);
            // Иначе совершаем обход текущего дерева
            if (compStart < 0) SubMap(root.left, start, end, result);
            if (compEnd > 0) SubMap(root.right, start, end, result);
        }

        public MyTreeMap<K, V> TailMap(K start) // метод для возврата сортированного отображения, содержащего элементы, ключ которых больше start
        {
            MyTreeMap<K, V> result = new MyTreeMap<K, V>(comparator);
            TailMap(root, start, result);
            return result;
        }

        private void TailMap(TreeNode root, K start, MyTreeMap<K, V> result)
        {
            if (root == null) return;

            int comp = comparator.Compare(root.key, start);
            if (comp > 0) // Если текущий ключ больше заданного 'start', то начинаем сортировать дерево. Иначе возвращаемся в правое поддерево
            {
                result.Put(root.key, root.value);
                TailMap(root.left, start, result);
                TailMap(root.right, start, result);
            }
            else TailMap(root.right, start, result);
        }

        public KeyValuePair<K, V>? LowerEntry(K key) // метод для возврата пары «ключ-значение», где ключ меньше заданного

        {
            return LowerEntry(root, key);
        }

        private KeyValuePair<K, V>? LowerEntry(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
           
            if (comp > 0) // Если текущий ключ меньше заданного, то переходим в правое поддерево и там ищем наименьшее, ибо может быть ключ еще меньше заданного
            {
                var rightResult = LowerEntry(root.right, key);
                // Возвращаем подходящий узел. Если его нет, то текущий, ибо он будет наименьшим искомого
                return rightResult ?? new KeyValuePair<K, V>(root.key, root.value);
            }
            return LowerEntry(root.left, key); // Если больше или равен, то ищем в левом поддереве
        }

        public KeyValuePair<K, V>? FloorEntry(K key) // метод для возврата пары «ключ-значение», где ключ меньше или равен заданному;

        {
            return FloorEntry(root, key);
        }

        private KeyValuePair<K, V>? FloorEntry(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
            if (comp == 0) return new KeyValuePair<K, V>(root.key, root.value);
            if (comp > 0)
            {
                var rightResult = FloorEntry(root.right, key);
                return rightResult ?? new KeyValuePair<K, V>(root.key, root.value);
            }
            return FloorEntry(root.left, key);
        }

        public KeyValuePair<K, V>? HigherEntry(K key) // метод для возврата пары «ключ-значение», где ключ больше заданного

        {
            return HigherEntry(root, key);
        }

        private KeyValuePair<K, V>? HigherEntry(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
            if (comp < 0)
            {
                var leftResult = HigherEntry(root.left, key);
                return leftResult ?? new KeyValuePair<K, V>(root.key, root.value);
            }
            return HigherEntry(root.right, key);
        }

        public KeyValuePair<K, V>? CeilingEntry(K key) // метод для возврата пары «ключ-значение», где ключ больше или равен заданному

        {
            return CeilingEntry(root, key);
        }

        private KeyValuePair<K, V>? CeilingEntry(TreeNode root, K key)
        {
            if (root == null) return null;

            int comp = comparator.Compare(key, root.key);
            if (comp == 0) return new KeyValuePair<K, V>(root.key, root.value);
            if (comp < 0)
            {
                var leftResult = CeilingEntry(root.left, key);
                return leftResult ?? new KeyValuePair<K, V>(root.key, root.value);
            }
            return CeilingEntry(root.right, key);
        }

        public K? LowerKey(K key) // метод для возврата ключа, который меньше заданного
        {
            var entry = LowerEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public K? FloorKey(K key) // метод для возврата ключа, который меньше или равен заданному
        {
            var entry = FloorEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public K? HigherKey(K key) // метод для возврата ключа, который больше заданного
        {
            var entry = HigherEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public K? CeilingKey(K key) // метод для возврата ключа, который больше или равен заданному
        {
            var entry = CeilingEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public KeyValuePair<K, V>? PollFirstEntry() //  метод для удаления и возврата первого элемента отображения
        {
            if (IsEmpty()) return null;
            var firstEntry = FirstEntry();
            Remove(firstEntry.Key);
            return firstEntry;
        }

        public KeyValuePair<K, V>? PollLastEntry() // метод для удаления и возврата последнего элемента отображения
        {
            if (IsEmpty()) return null;
            var lastEntry = LastEntry();
            Remove(lastEntry.Key);
            return lastEntry;
        }

        public KeyValuePair<K, V> FirstEntry() // метод для возврата первого элемента отображения без удаления
        {
            if (IsEmpty()) throw new InvalidOperationException("TreeMap is empty.");
            TreeNode minNode = GetMin(root);
            return new KeyValuePair<K, V>(minNode.key, minNode.value);
        }

        public KeyValuePair<K, V> LastEntry() // метод для возврата последнего элемента отображения без удаления
        {
            if (IsEmpty()) throw new InvalidOperationException("TreeMap is empty.");
            TreeNode maxNode = GetMax(root);
            return new KeyValuePair<K, V>(maxNode.key, maxNode.value);
        }

        public void PrintTree()
        {
            PrintTree(root);
        }

        private void PrintTree(TreeNode root)
        {
            if (root == null) return;
            PrintTree(root.left);
            Console.WriteLine(root.key);
            PrintTree(root.right);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyTreeMap<int, int> tree = new MyTreeMap<int, int>();
            tree.Put(1, 1);
            tree.Put(2, 2);
            tree.Put(3, 3);
            tree.Put(4, 4);
            tree.Put(5, 5);
            tree.PrintTree();
            Console.WriteLine(tree.ContainsKey(1));
            Console.WriteLine(tree.ContainsValue(3));
            tree.Remove(2);
            Console.WriteLine();
            tree.PrintTree();
            Console.WriteLine();
            Console.WriteLine(tree.PollFirstEntry());
            Console.WriteLine();
            tree.PrintTree();
        }
    }

}