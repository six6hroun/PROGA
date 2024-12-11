using MyArrayDeque;
namespace Lab15
{
    class program
    {
        public static int CountDigit(string line)
        {
            int count = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (Char.IsDigit(line[i])) count++;
            }
            return count;
        }
        public static int CountSpace(string line)
        {
            int count = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ') count++;
            }
            return count;
        }
        static void Main(string[] args)
        {
            string path1 = "input.txt";
            string path2 = "output.txt";
            StreamReader sr = new StreamReader(path1);
            MyArrayDeque<string> deque = new MyArrayDeque<string>();
            string line = sr.ReadLine();
            if (line != null) { deque.add(line); }
            while (line != null)
            {
                line = sr.ReadLine();
                if (line != null)
                {
                    if (CountDigit(line) > CountDigit(deque.getFirst())) deque.addLast(line);
                    else deque.addFirst(line);
                }
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(path2);
            for (int i = deque.indexOfHead(); i < deque.size(); i++)
            {
                sw.WriteLine(deque.get(i));
            }
            sw.Close();

            Console.WriteLine("Введите кол-во пробелов: ");
            int N = Convert.ToInt32(Console.ReadLine());
            for (int i = deque.indexOfHead(); i < deque.size(); i++)
            {
                if (CountSpace(deque.get(i)) > N)
                {
                    deque.remove(deque.get(i));
                    i--;
                }
            }
            for (int i = deque.indexOfHead(); i < deque.size(); i++)
                Console.WriteLine(deque.get(i));
        }
    }
}
