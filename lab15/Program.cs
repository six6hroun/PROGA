using MyArrayDeque;
namespace Labibibibi15
{
    class program
    {
        public static int chislo(string line)
        {
            int k = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (Char.IsDigit(line[i])) k++;
            }
            return k;
        }
        public static int space(string line)
        {
            int k = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ') k++;
            }
            return k;
        }
        static void Main(string[] args)
        {
            string file1 = "Z:/input.txt";
            string file2 = "Z:/output.txt";
            StreamReader sr = new StreamReader(file1);
            MyArrayDeque<string> deque = new MyArrayDeque<string>();
            string line = sr.ReadLine();
            if (line != null) { deque.add(line); }
            while (line != null)
            {
                line = sr.ReadLine();
                if (line != null)
                {
                    if (chislo(line) > space(deque.getFirst())) deque.addLast(line);
                    else deque.addFirst(line);
                }
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(file2);
            for (int i = deque.indexOfHead(); i < deque.size(); i++)
            {
                sw.WriteLine(deque.get(i));
            }
            sw.Close();

            Console.Write("Введите кол-во пробелов: ");
            int N = Convert.ToInt32(Console.ReadLine());
            for (int i = deque.indexOfHead(); i < deque.size(); i++)
            {
                if (space(deque.get(i)) > N)
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
