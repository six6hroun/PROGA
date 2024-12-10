using MyArrayDeque;
    class SortDeque
    {
        static int Kdigits(string line)
        {
            int k = 0;
            foreach (char c in line)
            {
                if (char.IsDigit(c))
                {
                    k++;
                }
            }
            return k;
        }

        static int Kspaces(string line)
        {
            int k = 0;
            foreach (char c in line)
            {
                if (c == ' ')
                {
                    k++;
                }
            }
            return k;
        }

        public void sortirovka(int n)
        {
            MyArrayDeque<string> deque = new MyArrayDeque<string>();
            string vxod = "B:/input.txt";
            string vixod = "B:/output.txt";
            StreamReader sr = new StreamReader(vxod);
            StreamWriter sw = new StreamWriter(vixod);
            string? lines = sr.ReadLine();

            if (lines != null)
            {
                deque.addFirst(lines);
            }

            while (lines != null)
            {
                lines = sr.ReadLine();
                if (lines != null)
                {
                    if (Kdigits(lines) > Kdigits(deque.getFirst())) deque.addLast(lines);
                    else deque.addFirst(lines);
                }
            }

            for (int i = 0; i < deque.size(); i++) sw.WriteLine(deque.getIndex(i));
            sw.Close();

            for (int i = 0; i < deque.size(); i++)
            {
                string line = deque.getIndex(i);
                if (Kspaces(line) > n) deque.remove(line);
            }
            deque.print();
        }

        static void Main(string[] args)
        {
            SortDeque sort = new SortDeque();
            Console.Write("Напишите количество пробелов: ");
            string numberSpace = Console.ReadLine();
            int n = Convert.ToInt32(numberSpace);
            sort.sortirovka(n);
        }
    }