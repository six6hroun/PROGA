using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Библиотека сортировок и генераций тестовых данных
namespace SortingAndGenerationLibrary
{
    public class TreeNode
    {
        public int Data;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public void Insert(int value)
        {
            if (value < Data)
            {
                if (Left == null) Left = new TreeNode(value);
                else Left.Insert(value);
            }
            else
            {
                if (Right == null) Right = new TreeNode(value);
                else Right.Insert(value);
            }
        }

        public int[] Transform(List<int> elements = null)
        {
            if (elements == null) elements = new List<int>();
            if (Left != null) Left.Transform(elements);
            elements.Add(Data);
            if (Right != null) Right.Transform(elements);
            return elements.ToArray();
        }
    }

    public class TreeNodeDouble
    {
        public double Data;
        public TreeNodeDouble Left;
        public TreeNodeDouble Right;

        public TreeNodeDouble(double data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public void Insert(double value)
        {
            if (value < Data)
            {
                if (Left == null) Left = new TreeNodeDouble(value);
                else Left.Insert(value);
            }
            else
            {
                if (Right == null) Right = new TreeNodeDouble(value);
                else Right.Insert(value);
            }
        }

        public List<double> Transform(List<double> elements = null)
        {
            if (elements == null) elements = new List<double>();
            if (Left != null) Left.Transform(elements);
            elements.Add(Data);
            if (Right != null) Right.Transform(elements);
            return elements;
        }
    }

    public class TreeNodeString
    {
        public string Data;
        public TreeNodeString Left;
        public TreeNodeString Right;

        public TreeNodeString(string data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public void Insert(string value)
        {
            if (string.Compare(value, Data) < 0)
            {
                if (Left == null) Left = new TreeNodeString(value);
                else Left.Insert(value);
            }
            else
            {
                if (Right == null) Right = new TreeNodeString(value);
                else Right.Insert(value);
            }
        }

        public List<string> Transform(List<string> elements = null)
        {
            if (elements == null) elements = new List<string>();
            if (Left != null) Left.Transform(elements);
            elements.Add(Data);
            if (Right != null) Right.Transform(elements);
            return elements;
        }
    }

    public static class SortingsAndGenerations
    {
        // Битонная сортировка
        static void Swap<T>(ref T current, ref T next)
        {
            T temp;
            temp = current;
            current = next;
            next = temp;
        }

        public static void ComparisonAndSwap(int[] numbers, int i, int j, bool dir)
        {
            bool flag;
            if (numbers[i] > numbers[j]) flag = true;
            else flag = false;
            if (dir == flag) Swap(ref numbers[i], ref numbers[j]);
        }

        public static void BitonicMerge(int[] numbers, int low, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++) ComparisonAndSwap(numbers, i, i + k, dir);
                BitonicMerge(numbers, low, k, dir);
                BitonicMerge(numbers, low + k, k, dir);
            }
        }

        public static void BitonicSortPart(int[] numbers, int low, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSortPart(numbers, low, k, true);
                BitonicSortPart(numbers, low + k, k, false);
                BitonicMerge(numbers, low, count, dir);
            }
        }

        public static int[] BitonicSort(int[] numbers, bool reverse = false)
        {
            double size = numbers.Length;
            int pow = 0;
            while (size > 1)
            {
                size /= 2;
                pow++;
            }
            if (size != 1) pow++;

            int[] newNumbers = new int[(int)Math.Pow(2, pow)];
            for (int i = 0; i < newNumbers.Length; i++)
            {
                if (i < numbers.Length)
                {
                    newNumbers[i] = numbers[i];
                    continue;
                }
                newNumbers[i] = -1;
            }

            BitonicSortPart(newNumbers, 0, newNumbers.Length, reverse);

            for (int i = 0; i < newNumbers.Length; i++)
            {
                if (!reverse && newNumbers[i] != -1) numbers[i] = newNumbers[i];
                if (reverse && newNumbers[newNumbers.Length - 1 - i] != -1) numbers[numbers.Length - 1 - i] = newNumbers[newNumbers.Length - 1 - i];
            }
            return numbers;
        }

        // Сортировка Шелла
        public static int[] ShellSort(int[] numbers, bool reverse = false)
        {
            for (int interval = numbers.Length / 2; interval > 0; interval /= 2)
            {
                for (int i = interval; i < numbers.Length; i++)
                {
                    int j = i;
                    while (j >= interval && ((reverse && numbers[j - interval] > numbers[j]) || (!reverse && numbers[j - interval] < numbers[j])))
                    {
                        Swap(ref numbers[j], ref numbers[j - interval]);
                        j -= interval;
                    }
                }
            }
            return numbers;
        }

        // Сортировка деревом
        public static int[] TreeSort(int[] numbers, bool reverse = false)
        {
            if (numbers == null || numbers.Length == 0) return new int[0];

            TreeNode Root = new TreeNode(numbers[0]);
            for (int i = 1; i < numbers.Length; i++) Root.Insert(numbers[i]);
            int[] newNumbers = Root.Transform();
            if (reverse)
            {
                Array.Reverse(newNumbers);
            }
            return newNumbers;
        }

        // Поразрядная сортировка
        public static int GetMaximum(int[] numbers)
        {
            var max = numbers[0];
            for (int i = 1; i < numbers.Length; i++) if (numbers[i] > max) max = numbers[i];
            return max;
        }

        public static void CountingSort(int[] numbers, int exponent, bool reverse = false)
        {
            int[] outputNumbers = new int[numbers.Length];
            int[] countNumbers = new int[10];

            for (int i = 0; i < 10; i++) countNumbers[i] = 0;

            for (int i = 0; i < numbers.Length; i++) countNumbers[(numbers[i] / exponent) % 10]++;

            for (int i = 1; i < 10; i++) countNumbers[i] += countNumbers[i - 1];

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                outputNumbers[countNumbers[(numbers[i] / exponent) % 10] - 1] = numbers[i];
                countNumbers[(numbers[i] / exponent) % 10]--;
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                if (!reverse)
                {
                    numbers[i] = outputNumbers[numbers.Length - i - 1];
                    continue;
                }
                numbers[i] = outputNumbers[i];
            }
        }

        public static int[] RadixSort(int[] numbers, bool reverse = false)
        {
            var max = GetMaximum(numbers);
            for (int exponent = 1; max / exponent > 0; exponent *= 10) CountingSort(numbers, exponent, reverse);
            return numbers;
        }

        // Сортировка пузырьком
        public static int[] BubbleSort(int[] numbers, bool reverse = false)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (reverse)
                    {
                        if (numbers[i] > numbers[j])
                        {
                            int temp = numbers[i];
                            numbers[i] = numbers[j];
                            numbers[j] = temp;
                        }
                        continue;
                    }
                    if (numbers[i] < numbers[j])
                    {
                        int temp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
            return numbers;
        }

        // Сортировка вставками
        public static int[] InsertionSort(int[] numbers, bool reverse = false)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                int j = i;
                while ((j > 0) && ((!reverse && numbers[j] < numbers[j - 1]) || (reverse && numbers[j] > numbers[j - 1])))
                {
                    int temp = numbers[j];
                    numbers[j] = numbers[j - 1];
                    numbers[j - 1] = temp;
                    j--;
                }
            }
            return numbers;
        }

        // Сортировка выбором
        public static int[] SelectionSort(int[] numbers, bool reverse = false)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int maxMinIndex = i;
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if ((reverse && numbers[j] > maxMinIndex) || (!reverse && numbers[j] < maxMinIndex))
                    {
                        maxMinIndex = j;
                    }
                }
                int temp = numbers[maxMinIndex];
                numbers[maxMinIndex] = numbers[i];
                numbers[i] = temp;
            }
            return numbers;
        }

        // Шейкерная сортировка
        public static int[] ShakerSort(int[] numbers, bool reverse = false)
        {
            bool swapped = true;
            int start = 0;
            int end = numbers.Length;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end - 1; i++)
                {
                    if ((!reverse && numbers[i] > numbers[i + 1]) || (reverse && numbers[i] < numbers[i + 1]))
                    {
                        int temp = numbers[i];
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end--;

                for (int i = end - 1; i >= start; i--)
                {
                    if ((!reverse && numbers[i] > numbers[i + 1]) || (reverse && numbers[i] < numbers[i + 1]))
                    {
                        int temp = numbers[i];
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = temp;
                        swapped = true;
                    }
                }
                start++;
            }
            return numbers;
        }

        // Гномья сортировка
        public static int[] GnomeSort(int[] numbers, bool reverse = false)
        {
            var index = 0;
            var nextIndex = index + 1;

            while (index < numbers.Length)
            {
                if (index == 0)
                {
                    index = nextIndex;
                    nextIndex++;
                }
                if ((!reverse && numbers[index] >= numbers[index - 1]) || (reverse && numbers[index] <= numbers[index - 1]))
                {
                    index = nextIndex;
                    nextIndex++;
                }
                else
                {
                    int temp = numbers[index];
                    numbers[index] = numbers[index - 1];
                    numbers[index - 1] = temp;
                    index--;
                }
            }

            return numbers;
        }

        // Сортировка расчёской
        private static int GetNextStep(int s)
        {
            s = s * 1000 / 1247;
            return s > 1 ? s : 1;
        }
        public static int[] CombSort(int[] numbers, bool reverse = false)
        {
            int arrayLength = numbers.Length;
            int currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < arrayLength; i++)
                {
                    if (numbers[i] < numbers[i + currentStep])
                    {
                        int temp = numbers[i];
                        numbers[i] = numbers[i + currentStep];
                        numbers[i + currentStep] = temp;
                    }
                }
                currentStep = GetNextStep(currentStep);
            }
            BubbleSort(numbers, reverse);
            return numbers;
        }

        // Пирамидальная сортировка
        public static void Heapify(int[] numbers, int i, int n)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && numbers[left] > numbers[largest]) largest = left;
            if (right < n && numbers[right] > numbers[largest]) largest = right;

            if (largest != i)
            {
                int temp = numbers[largest];
                numbers[largest] = numbers[i];
                numbers[i] = temp;

                Heapify(numbers, largest, n);
            }
        }
        public static int[] HeapSort(int[] array, bool Order)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, i, n);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                Heapify(array, 0, i);
            }

            if (Order)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    int temp = (int)array[i];
                    array[i] = array[n - i - 1];
                    array[n - i - 1] = temp;
                }
            }
            return array;
        }

        // Быстрая сортировка
        private static int Partition(int[] numbers, int minIndex, int maxIndex, bool reverse = false)
        {
            int piv = numbers[minIndex];
            int pivotIndex = 0;
            int left = minIndex;
            if (minIndex + 1 >= maxIndex) return left;

            for (int i = minIndex + 1; i < maxIndex; i++)
            {
                if ((!reverse && numbers[i] < piv) || (reverse && numbers[i] > piv))
                {
                    int temp = numbers[i];
                    numbers[i] = numbers[left];
                    numbers[left] = temp;
                    left++;
                }
                if (numbers[i] == piv)
                {
                    pivotIndex = i;
                }
                else if (numbers[left] == piv)
                {
                    pivotIndex = left;
                }
            }

            numbers[pivotIndex] = numbers[left];
            numbers[left] = piv;
            return left;
        }
        public static void QuickSort(this int[] numbers, int minIndex, int maxIndex, bool reverse = false)
        {
            if (minIndex < maxIndex)
            {
                int point = Partition(numbers, minIndex, maxIndex, reverse);
                QuickSort(numbers, minIndex, point, reverse);
                QuickSort(numbers, point + 1, maxIndex, reverse);
            }
        }
        public static int[] QuickSort(int[] numbers, bool reverse = false)
        {
            QuickSort(numbers, 0, numbers.Length, reverse);
            return numbers;
        }

        // Сортировка слиянием
        private static void Merge(int[] numbers, int lowIndex, int middleIndex, int highIndex, bool reverse = false)
        {
            int length1 = middleIndex - lowIndex + 1;
            int length2 = highIndex - middleIndex;

            int[] numbersLeft = new int[length1];
            int[] numbersRight = new int[length2];

            for (int i = 0; i < length1; i++) numbersLeft[i] = numbers[lowIndex + i];
            for (int i = 0; i < length2; i++) numbersRight[i] = numbers[middleIndex + i + 1];

            int indexLeft = 0, indexRight = 0, index = lowIndex;

            while (indexLeft < length1 && indexRight < length2)
            {
                if ((!reverse && numbersLeft[indexLeft] < numbersRight[indexRight]) || (reverse && numbersLeft[indexLeft] > numbersRight[indexRight]))
                {
                    numbers[index] = numbersLeft[indexLeft];
                    indexLeft++;
                }
                else
                {
                    numbers[index] = numbersRight[indexRight];
                    indexRight++;
                }
                index++;
            }

            while (indexLeft < length1)
            {
                numbers[index] = numbersLeft[indexLeft];
                indexLeft++;
                index++;
            }
            while (indexRight < length2)
            {
                numbers[index] = numbersRight[indexRight];
                indexRight++;
                index++;
            }

        }
        public static void MergeSort(this int[] numbers, int lowIndex, int highIndex, bool reverse = false)
        {
            if (lowIndex >= highIndex) return;
            int middleIndex = lowIndex + (highIndex - lowIndex) / 2; MergeSort(numbers, lowIndex, middleIndex, reverse);
            MergeSort(numbers, middleIndex + 1, highIndex, reverse); Merge(numbers, lowIndex, middleIndex, highIndex, reverse);
        }
        public static int[] MergeSort(int[] numbers, bool reverse = false)
        {
            MergeSort(numbers, 0, numbers.Length - 1, reverse);
            return numbers;
        }

        // Сортировка подсчетом
        public static int[] CountSort(int[] numbers, bool reverse = false)
        {

            int max = numbers[0];
            foreach (int item in numbers) max = Math.Max(max, item);

            int[] countNumbers = new int[max + 1];
            foreach (int item in numbers) countNumbers[item]++;

            for (int i = 1; i <= max; i++) countNumbers[i] += countNumbers[i - 1];

            int[] answerNumbers = new int[numbers.Length];
            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                answerNumbers[countNumbers[numbers[i]] - 1] = numbers[i];
                countNumbers[numbers[i]]--;
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                if (reverse)
                {
                    numbers[i] = answerNumbers[numbers.Length - i - 1];
                    continue;
                }
                numbers[i] = answerNumbers[i];
            }
            return countNumbers;
        }

        // Генерация целых чисел
        public static int[] RandomNumbers(int size)
        {
            Random rand = new Random();
            int[] numbers = new int[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = rand.Next(0, 1000);
            }
            return numbers;
        }

        // Генерация дробных чисел
        public static double[] RandomDoubles(int size)
        {
            Random rand = new Random();
            double[] numbers = new double[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = rand.NextDouble() * 100;
            }
            return numbers;
        }

        // Генерация случайных строк
        public static string[] RandomStrings(int size)
        {
            Random rand = new Random();
            string[] numbers = new string[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = Path.GetRandomFileName();
            }
            return numbers;
        }

        public static void ComparisonAndSwap(double[] numbers, int i, int j, bool dir)
        {
            bool flag;
            if (numbers[i] > numbers[j]) flag = true;
            else flag = false;
            if (dir == flag) Swap(ref numbers[i], ref numbers[j]);
        }

        public static void BitonicMerge(double[] numbers, int low, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++) ComparisonAndSwap(numbers, i, i + k, dir);
                BitonicMerge(numbers, low, k, dir);
                BitonicMerge(numbers, low + k, k, dir);
            }
        }

        public static void BitonicSortPart(double[] numbers, int low, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSortPart(numbers, low, k, true);
                BitonicSortPart(numbers, low + k, k, false);
                BitonicMerge(numbers, low, count, dir);
            }
        }

        public static double[] BitonicSortDouble(double[] numbers, bool reverse = false)
        {
            double size = numbers.Length;
            int pow = 0;
            while (size > 1)
            {
                size /= 2;
                pow++;
            }
            if (size != 1) pow++;

            double[] newNumbers = new double[(int)Math.Pow(2, pow)];
            for (int i = 0; i < newNumbers.Length; i++)
            {
                if (i < numbers.Length)
                {
                    newNumbers[i] = numbers[i];
                    continue;
                }
                newNumbers[i] = -1;
            }

            BitonicSortPart(newNumbers, 0, newNumbers.Length, reverse);

            for (int i = 0; i < newNumbers.Length; i++)
            {
                if (!reverse && newNumbers[i] != -1) numbers[i] = newNumbers[i];
                if (reverse && newNumbers[newNumbers.Length - 1 - i] != -1) numbers[numbers.Length - 1 - i] = newNumbers[newNumbers.Length - 1 - i];
            }
            return numbers;
        }

        public static void ComparisonAndSwap(string[] numbers, int i, int j, bool dir)
        {
            bool flag = string.Compare(numbers[i], numbers[j]) > 0;
            if (dir == flag) Swap(ref numbers[i], ref numbers[j]);
        }

        public static void BitonicMerge(string[] numbers, int low, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++)
                {
                    ComparisonAndSwap(numbers, i, i + k, dir);
                }
                BitonicMerge(numbers, low, k, dir);
                BitonicMerge(numbers, low + k, k, dir);
            }
        }

        public static void BitonicSortPart(string[] numbers, int low, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSortPart(numbers, low, k, true);
                BitonicSortPart(numbers, low + k, k, false);
                BitonicMerge(numbers, low, count, dir);
            }
        }

        public static string[] BitonicSortString(string[] numbers, bool reverse = false)
        {
            int size = numbers.Length;
            int pow = 0;

            while ((1 << pow) < size) pow++;

            int newSize = 1 << pow;
            string[] newNumbers = new string[newSize];

            for (int i = 0; i < newSize; i++)
            {
                if (i < size)
                {
                    newNumbers[i] = numbers[i];
                }
                else
                {
                    newNumbers[i] = null;
                }
            }

            BitonicSortPart(newNumbers, 0, newNumbers.Length, !reverse);

            for (int i = 0; i < size; i++) numbers[i] = newNumbers[i];
            return numbers;
        }

        public static double[] ShellSortDouble(double[] numbers, bool reverse = false)
        {
            for (int interval = numbers.Length / 2; interval > 0; interval /= 2)
            {
                for (int i = interval; i < numbers.Length; i++)
                {
                    int j = i;
                    while (j >= interval && ((reverse && numbers[j - interval] > numbers[j]) || (!reverse && numbers[j - interval] < numbers[j])))
                    {
                        Swap(ref numbers[j], ref numbers[j - interval]);
                        j -= interval;
                    }
                }
            }
            return numbers;
        }

        public static string[] ShellSortString(string[] numbers, bool reverse = false)
        {
            for (int interval = numbers.Length / 2; interval > 0; interval /= 2)
            {
                for (int i = interval; i < numbers.Length; i++)
                {
                    int j = i;
                    while (j >= interval && ((reverse && string.Compare(numbers[j - interval], numbers[j]) > 0) || (!reverse && string.Compare(numbers[j - interval], numbers[j]) < 0)))
                    {
                        Swap(ref numbers[j], ref numbers[j - interval]);
                        j -= interval;
                    }
                }
            }
            return numbers;
        }

        public static double[] TreeSortDouble(double[] numbers, bool reverse = false)
        {
            if (numbers == null || numbers.Length == 0) return new double[0];

            TreeNodeDouble root = new TreeNodeDouble(numbers[0]);
            for (int i = 1; i < numbers.Length; i++) root.Insert(numbers[i]);

            List<double> sortedList = root.Transform();
            double[] newNumbers = sortedList.ToArray();
            if (reverse)
            {
                Array.Reverse(newNumbers);
            }
            return newNumbers;
        }

        public static string[] TreeSortString(string[] numbers, bool reverse = false)
        {
            if (numbers == null || numbers.Length == 0) return new string[0];

            TreeNodeString root = new TreeNodeString(numbers[0]);
            for (int i = 1; i < numbers.Length; i++) root.Insert(numbers[i]);

            List<string> sortedList = root.Transform();
            string[] newNumbers = sortedList.ToArray();
            if (reverse)
            {
                Array.Reverse(newNumbers);
            }
            return newNumbers;
        }

        public static double GetMaximum(double[] numbers)
        {
            var max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max) max = numbers[i];
            }
            return max;
        }

        public static void CountingSort(double[] numbers, int exponent, bool reverse = false)
        {
            double[] outputNumbers = new double[numbers.Length];
            int[] countNumbers = new int[10];

            for (int i = 0; i < 10; i++) countNumbers[i] = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                int index = (int)(numbers[i] / exponent) % 10;
                countNumbers[index]++;
            }

            for (int i = 1; i < 10; i++) countNumbers[i] += countNumbers[i - 1];

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                int index = (int)(numbers[i] / exponent) % 10;
                outputNumbers[countNumbers[index] - 1] = numbers[i];
                countNumbers[index]--;
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = reverse ? outputNumbers[numbers.Length - i - 1] : outputNumbers[i];
            }
        }

        public static double[] RadixSortDouble(double[] numbers, bool reverse = false)
        {
            var max = GetMaximum(numbers);
            for (int exponent = 1; (int)(max / exponent) > 0; exponent *= 10)
            {
                CountingSort(numbers, exponent, reverse);
            }
            return numbers;
        }

        public static string GetMaximum(string[] numbers)
        {
            string max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (string.Compare(numbers[i], max) > 0) max = numbers[i];
            }
            return max;
        }

        public static void CountingSort(string[] numbers, int position, bool reverse = false)
        {
            string[] outputNumbers = new string[numbers.Length];
            int[] countNumbers = new int[256];

            for (int i = 0; i < 256; i++) countNumbers[i] = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                char c = position < numbers[i].Length ? numbers[i][position] : '\0';
                countNumbers[c]++;
            }

            for (int i = 1; i < 256; i++) countNumbers[i] += countNumbers[i - 1];

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                char c = position < numbers[i].Length ? numbers[i][position] : '\0';
                outputNumbers[countNumbers[c] - 1] = numbers[i];
                countNumbers[c]--;
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = reverse ? outputNumbers[numbers.Length - i - 1] : outputNumbers[i];
            }
        }

        public static string[] RadixSortString(string[] numbers, bool reverse = false)
        {
            int maxLen = GetMaximum(numbers).Length;
            for (int position = maxLen - 1; position >= 0; position--)
            {
                CountingSort(numbers, position, reverse);
            }
            return numbers;
        }

        public static double[] BubbleSortDouble(double[] numbers, bool reverse = false)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (reverse)
                    {
                        if (numbers[i] > numbers[j])
                        {
                            Swap(ref numbers[i], ref numbers[j]);
                        }
                    }
                    else
                    {
                        if (numbers[i] < numbers[j])
                        {
                            Swap(ref numbers[i], ref numbers[j]);
                        }
                    }
                }
            }
            return numbers;
        }

        public static string[] BubbleSortString(string[] numbers, bool reverse = false)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (reverse)
                    {
                        if (string.Compare(numbers[i], numbers[j]) > 0)
                        {
                            Swap(ref numbers[i], ref numbers[j]);
                        }
                    }
                    else
                    {
                        if (string.Compare(numbers[i], numbers[j]) < 0)
                        {
                            Swap(ref numbers[i], ref numbers[j]);
                        }
                    }
                }
            }
            return numbers;
        }

        public static double[] InsertionSortDouble(double[] numbers, bool reverse = false)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                int j = i;
                while ((j > 0) && ((!reverse && numbers[j] < numbers[j - 1]) || (reverse && numbers[j] > numbers[j - 1])))
                {
                    Swap(ref numbers[j], ref numbers[j - 1]);
                    j--;
                }
            }
            return numbers;
        }

        public static string[] InsertionSortString(string[] numbers, bool reverse = false)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                int j = i;
                while ((j > 0) && ((!reverse && string.Compare(numbers[j], numbers[j - 1]) < 0) || (reverse && string.Compare(numbers[j], numbers[j - 1]) > 0)))
                {
                    Swap(ref numbers[j], ref numbers[j - 1]);
                    j--;
                }
            }
            return numbers;
        }

        public static double[] SelectionSortDouble(double[] numbers, bool reverse = false)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int maxMinIndex = i;
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if ((reverse && numbers[j] > numbers[maxMinIndex]) || (!reverse && numbers[j] < numbers[maxMinIndex]))
                    {
                        maxMinIndex = j;
                    }
                }
                Swap(ref numbers[maxMinIndex], ref numbers[i]);
            }
            return numbers;
        }

        public static string[] SelectionSortString(string[] numbers, bool reverse = false)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int maxMinIndex = i;
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if ((reverse && string.Compare(numbers[j], numbers[maxMinIndex]) > 0) || (!reverse && string.Compare(numbers[j], numbers[maxMinIndex]) < 0))
                    {
                        maxMinIndex = j;
                    }
                }
                Swap(ref numbers[maxMinIndex], ref numbers[i]);
            }
            return numbers;
        }

        public static double[] ShakerSortDouble(double[] numbers, bool reverse = false)
        {
            bool swapped = true;
            int start = 0;
            int end = numbers.Length;

            while (swapped)
            {
                swapped = false;
                for (int i = start; i < end - 1; i++)
                {
                    if ((!reverse && numbers[i] > numbers[i + 1]) || (reverse && numbers[i] < numbers[i + 1]))
                    {
                        double temp = numbers[i];
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end--;

                for (int i = end - 1; i >= start; i--)
                {
                    if ((!reverse && numbers[i] > numbers[i + 1]) || (reverse && numbers[i] < numbers[i + 1]))
                    {
                        double temp = numbers[i];
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = temp;
                        swapped = true;
                    }
                }
                start++;
            }
            return numbers;
        }

        public static string[] ShakerSortString(string[] strings, bool reverse = false)
        {
            bool swapped = true;
            int start = 0;
            int end = strings.Length;

            while (swapped)
            {
                swapped = false;
                for (int i = start; i < end - 1; i++)
                {
                    if ((!reverse && string.Compare(strings[i], strings[i + 1]) > 0) ||
                        (reverse && string.Compare(strings[i], strings[i + 1]) < 0))
                    {
                        string temp = strings[i];
                        strings[i] = strings[i + 1];
                        strings[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end--;

                for (int i = end - 1; i >= start; i--)
                {
                    if ((!reverse && string.Compare(strings[i], strings[i + 1]) > 0) ||
                        (reverse && string.Compare(strings[i], strings[i + 1]) < 0))
                    {
                        string temp = strings[i];
                        strings[i] = strings[i + 1];
                        strings[i + 1] = temp;
                        swapped = true;
                    }
                }
                start++;
            }
            return strings;
        }

        public static double[] GnomeSortDouble(double[] numbers, bool reverse = false)
        {
            var index = 0;
            var nextIndex = index + 1;

            while (index < numbers.Length)
            {
                if (index == 0)
                {
                    index = nextIndex;
                    nextIndex++;
                }
                if ((!reverse && numbers[index] >= numbers[index - 1]) || (reverse && numbers[index] <= numbers[index - 1]))
                {
                    index = nextIndex;
                    nextIndex++;
                }
                else
                {
                    double temp = numbers[index];
                    numbers[index] = numbers[index - 1];
                    numbers[index - 1] = temp;
                    index--;
                }
            }

            return numbers;
        }

        public static string[] GnomeSortString(string[] strings, bool reverse = false)
        {
            var index = 0;
            var nextIndex = index + 1;

            while (index < strings.Length)
            {
                if (index == 0)
                {
                    index = nextIndex;
                    nextIndex++;
                }
                if ((!reverse && string.Compare(strings[index], strings[index - 1]) >= 0) ||
                    (reverse && string.Compare(strings[index], strings[index - 1]) <= 0))
                {
                    index = nextIndex;
                    nextIndex++;
                }
                else
                {
                    string temp = strings[index];
                    strings[index] = strings[index - 1];
                    strings[index - 1] = temp;
                    index--;
                }
            }

            return strings;
        }

        public static double[] CombSortDouble(double[] numbers, bool reverse = false)
        {
            int arrayLength = numbers.Length;
            int currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < arrayLength; i++)
                {
                    if ((!reverse && numbers[i] > numbers[i + currentStep]) || (reverse && numbers[i] < numbers[i + currentStep]))
                    {
                        double temp = numbers[i];
                        numbers[i] = numbers[i + currentStep];
                        numbers[i + currentStep] = temp;
                    }
                }
                currentStep = GetNextStep(currentStep);
            }
            BubbleSortDouble(numbers, reverse);
            return numbers;
        }

        public static string[] CombSortString(string[] strings, bool reverse = false)
        {
            int arrayLength = strings.Length;
            int currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < arrayLength; i++)
                {
                    if ((!reverse && string.Compare(strings[i], strings[i + currentStep]) > 0) ||
                        (reverse && string.Compare(strings[i], strings[i + currentStep]) < 0))
                    {
                        string temp = strings[i];
                        strings[i] = strings[i + currentStep];
                        strings[i + currentStep] = temp;
                    }
                }
                currentStep = GetNextStep(currentStep);
            }
            BubbleSortString(strings, reverse);
            return strings;
        }

        public static void Heapify(double[] numbers, int i, int n)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && numbers[left] > numbers[largest]) largest = left;
            if (right < n && numbers[right] > numbers[largest]) largest = right;

            if (largest != i)
            {
                double temp = numbers[largest];
                numbers[largest] = numbers[i];
                numbers[i] = temp;

                Heapify(numbers, largest, n);
            }
        }

        public static double[] HeapSortDouble(double[] array, bool ascending)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, i, n);

            for (int i = n - 1; i > 0; i--)
            {
                double temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                Heapify(array, 0, i);
            }

            if (!ascending)
            {
                Array.Reverse(array);
            }

            return array;
        }

        public static void Heapify(string[] strings, int i, int n)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && string.Compare(strings[left], strings[largest]) > 0) largest = left;
            if (right < n && string.Compare(strings[right], strings[largest]) > 0) largest = right;

            if (largest != i)
            {
                string temp = strings[largest];
                strings[largest] = strings[i];
                strings[i] = temp;

                Heapify(strings, largest, n);
            }
        }

        public static string[] HeapSortString(string[] array, bool ascending)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, i, n);

            for (int i = n - 1; i > 0; i--)
            {
                string temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                Heapify(array, 0, i);
            }

            if (!ascending)
            {
                Array.Reverse(array);
            }

            return array;
        }

        private static int Partition(double[] numbers, int minIndex, int maxIndex, bool reverse = false)
        {
            double piv = numbers[minIndex];
            int left = minIndex;

            if (minIndex + 1 >= maxIndex) return left;

            for (int i = minIndex + 1; i < maxIndex; i++)
            {
                if ((!reverse && numbers[i] < piv) || (reverse && numbers[i] > piv))
                {
                    double temp = numbers[i];
                    numbers[i] = numbers[left];
                    numbers[left] = temp;
                    left++;
                }
            }

            numbers[minIndex] = numbers[left];
            numbers[left] = piv;
            return left;
        }

        public static void QuickSort(this double[] numbers, int minIndex, int maxIndex, bool reverse = false)
        {
            if (minIndex < maxIndex)
            {
                int point = Partition(numbers, minIndex, maxIndex, reverse);
                QuickSort(numbers, minIndex, point - 1, reverse);
                QuickSort(numbers, point + 1, maxIndex, reverse);
            }
        }

        public static double[] QuickSortDouble(double[] numbers, bool reverse = false)
        {
            QuickSort(numbers, 0, numbers.Length, reverse);
            return numbers;
        }

        private static int Partition(string[] strings, int minIndex, int maxIndex, bool reverse = false)
        {
            string piv = strings[minIndex];
            int left = minIndex;

            if (minIndex + 1 >= maxIndex) return left;

            for (int i = minIndex + 1; i < maxIndex; i++)
            {
                if ((!reverse && string.Compare(strings[i], piv) < 0) || (reverse && string.Compare(strings[i], piv) > 0))
                {
                    string temp = strings[i];
                    strings[i] = strings[left];
                    strings[left] = temp;
                    left++;
                }
            }

            strings[minIndex] = strings[left];
            strings[left] = piv;
            return left;
        }

        public static void QuickSort(this string[] strings, int minIndex, int maxIndex, bool reverse = false)
        {
            if (minIndex < maxIndex)
            {
                int point = Partition(strings, minIndex, maxIndex, reverse);
                QuickSort(strings, minIndex, point - 1, reverse);
                QuickSort(strings, point + 1, maxIndex, reverse);
            }
        }

        public static string[] QuickSortString(string[] strings, bool reverse = false)
        {
            QuickSort(strings, 0, strings.Length, reverse);
            return strings;
        }

        private static void Merge(double[] numbers, int lowIndex, int middleIndex, int highIndex, bool reverse = false)
        {
            int length1 = middleIndex - lowIndex + 1;
            int length2 = highIndex - middleIndex;

            double[] numbersLeft = new double[length1];
            double[] numbersRight = new double[length2];

            for (int i = 0; i < length1; i++) numbersLeft[i] = numbers[lowIndex + i];
            for (int i = 0; i < length2; i++) numbersRight[i] = numbers[middleIndex + i + 1];

            int indexLeft = 0, indexRight = 0, index = lowIndex;

            while (indexLeft < length1 && indexRight < length2)
            {
                if ((!reverse && numbersLeft[indexLeft] < numbersRight[indexRight]) || (reverse && numbersLeft[indexLeft] > numbersRight[indexRight]))
                {
                    numbers[index] = numbersLeft[indexLeft];
                    indexLeft++;
                }
                else
                {
                    numbers[index] = numbersRight[indexRight];
                    indexRight++;
                }
                index++;
            }

            while (indexLeft < length1)
            {
                numbers[index] = numbersLeft[indexLeft];
                indexLeft++;
                index++;
            }
            while (indexRight < length2)
            {
                numbers[index] = numbersRight[indexRight];
                indexRight++;
                index++;
            }
        }

        public static void MergeSort(this double[] numbers, int lowIndex, int highIndex, bool reverse = false)
        {
            if (lowIndex >= highIndex) return;
            int middleIndex = lowIndex + (highIndex - lowIndex) / 2;
            MergeSort(numbers, lowIndex, middleIndex, reverse);
            MergeSort(numbers, middleIndex + 1, highIndex, reverse);
            Merge(numbers, lowIndex, middleIndex, highIndex, reverse);
        }

        public static double[] MergeSortDouble(double[] numbers, bool reverse = false)
        {
            MergeSort(numbers, 0, numbers.Length - 1, reverse);
            return numbers;
        }

        private static void Merge(string[] strings, int lowIndex, int middleIndex, int highIndex, bool reverse = false)
        {
            int length1 = middleIndex - lowIndex + 1;
            int length2 = highIndex - middleIndex;

            string[] stringsLeft = new string[length1];
            string[] stringsRight = new string[length2];

            for (int i = 0; i < length1; i++) stringsLeft[i] = strings[lowIndex + i];
            for (int i = 0; i < length2; i++) stringsRight[i] = strings[middleIndex + i + 1];

            int indexLeft = 0, indexRight = 0, index = lowIndex;

            while (indexLeft < length1 && indexRight < length2)
            {
                if ((!reverse && string.Compare(stringsLeft[indexLeft], stringsRight[indexRight]) < 0) || (reverse && string.Compare(stringsLeft[indexLeft], stringsRight[indexRight]) > 0))
                {
                    strings[index] = stringsLeft[indexLeft];
                    indexLeft++;
                }
                else
                {
                    strings[index] = stringsRight[indexRight];
                    indexRight++;
                }
                index++;
            }

            while (indexLeft < length1)
            {
                strings[index] = stringsLeft[indexLeft];
                indexLeft++;
                index++;
            }
            while (indexRight < length2)
            {
                strings[index] = stringsRight[indexRight];
                indexRight++;
                index++;
            }
        }

        public static void MergeSort(this string[] strings, int lowIndex, int highIndex, bool reverse = false)
        {
            if (lowIndex >= highIndex) return;
            int middleIndex = lowIndex + (highIndex - lowIndex) / 2;
            MergeSort(strings, lowIndex, middleIndex, reverse);
            MergeSort(strings, middleIndex + 1, highIndex, reverse);
            Merge(strings, lowIndex, middleIndex, highIndex, reverse);
        }

        public static string[] MergeSortString(string[] strings, bool reverse = false)
        {
            MergeSort(strings, 0, strings.Length - 1, reverse);
            return strings;
        }

        public static double[] CountSortDouble(double[] numbers, bool reverse = false)
        {
            double min = numbers[0];
            double max = numbers[0];
            foreach (double item in numbers)
            {
                if (item < min) min = item;
                if (item > max) max = item;
            }

            int range = (int)(max - min) + 1;
            int[] countNumbers = new int[range];

            foreach (double item in numbers)
            {
                countNumbers[(int)(item - min)]++;
            }

            for (int i = 1; i < range; i++)
            {
                countNumbers[i] += countNumbers[i - 1];
            }

            double[] answerNumbers = new double[numbers.Length];
            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                answerNumbers[countNumbers[(int)(numbers[i] - min)] - 1] = numbers[i];
                countNumbers[(int)(numbers[i] - min)]--;
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = reverse ? answerNumbers[numbers.Length - i - 1] : answerNumbers[i];
            }

            return numbers;
        }

        public static string[] CountSortString(string[] strings, bool reverse = false)
        {
            Dictionary<string, int> countDict = new Dictionary<string, int>();
            foreach (string item in strings)
            {
                if (countDict.ContainsKey(item))
                    countDict[item]++;
                else
                    countDict[item] = 1;
            }

            List<string> sortedList = new List<string>();
            foreach (var kvp in countDict)
            {
                for (int i = 0; i < kvp.Value; i++)
                {
                    sortedList.Add(kvp.Key);
                }
            }

            if (reverse)
            {
                sortedList.Reverse();
            }

            return sortedList.ToArray();
        }

        //// Массивы, разбитые на несколько отсортированных подмассивов разного размера
        //public static int[] RandomSortedSubnumbers(int size)
        //{
        //    Random r = new Random();
        //    int module = r.Next(0, size);
        //    int newSize = r.Next(2, size) % module;
        //    if (newSize < 2) newSize = 2;
        //    int[] numbers = new int[size];
        //    int count = 0;
        //    int i = 0;
        //    while (i < size)
        //    {
        //        int exp = r.Next(0, 1000);
        //        int elements = 0;
        //        count++;
        //        while (i < size && i < newSize * count)
        //        {
        //            elements++;
        //            numbers[i] = elements * exp;
        //            i++;
        //        }
        //    }
        //    return numbers;
        //}

        //// Изначально отсортированные массивы случайных чисел с некоторым числом перестановок двух случайных элементов
        //public static int[] RandomSwappedNumbers(int size)
        //{
        //    int[] numbers = new int[size];
        //    Random r = new Random();
        //    int countSwap = r.Next(0, size / 3);
        //    for (int i = 0; i < size; i++) numbers[i] = i;
        //    for (int i = 0; i < countSwap; i++)
        //    {
        //        int first = r.Next(0, numbers.Length - 1);
        //        int second = r.Next(0, numbers.Length - 1);
        //        int temp = numbers[first];
        //        numbers[first] = numbers[second];
        //        numbers[second] = temp;
        //    }
        //    return numbers;
        //}

        //// Полностью отсортированные массивы с несколькими замененными элементами и с большим количеством повторений одного элемента
        //public static int[] RandomSwappedAndRepeatedNumbers(int size)
        //{
        //    int[] numbers = new int[size];
        //    Random r = new Random();
        //    int countRepeat = r.Next(0, size / 3);
        //    int indexRepeat = r.Next(0, size - 1);
        //    while (countRepeat > 0)
        //    {
        //        int indexRandom = r.Next(0, numbers.Length - 1);
        //        if (numbers[indexRandom] != numbers[indexRepeat])
        //        {
        //            numbers[indexRandom] = numbers[indexRepeat];
        //            countRepeat--;
        //        }
        //    }
        //    return numbers;
        //}
    }
}
