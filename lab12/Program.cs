using MyArrayList;
using MyPriorityDeque;

public class PriorityQueue : IComparable<PriorityQueue>
{
    int prioritet {get;set;}
    int nomer {get;set;}
    int step {get;set;} 

    public PriorityQueue(int prioritets, int number, int step)
    {
        this.prioritet = prioritets;
        this.nomer = number;
        this.step = step;
    }

    // Метод для сравнения приоритета текущей заявки с приоритетом заявки A
    public int CompareTo(PriorityQueue a)
    {
        return prioritet.CompareTo(a.prioritet);
    }

    static void Main(string[] args)
    {
        string file = "B:/pip.txt";
        MyPriorityQueue<PriorityQueue> zayvki = new MyPriorityQueue<PriorityQueue>();
        Console.Write("Введите количество шагов для добавления заявок: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int k = 0;
        StreamWriter sw = new StreamWriter(file);

        for (int i = 0; i < n; i++)
        {
            Random random = new Random();
            int number = random.Next(1, 11);
            for (int j = 0; j < number; j++)
            {
                int priorities = random.Next(1, 6);
                PriorityQueue list1 = new PriorityQueue(priorities, j, i+1);
                zayvki.Add(list1);
                sw.WriteLine($"Add: {list1.prioritet} {list1.nomer} {list1.step} ");
                k++;
            }
            PriorityQueue list2 = zayvki.Poll();
            sw.WriteLine($"Remove: {list2.prioritet} {list2.nomer} {list2.step} ");
            k--;
        }

        for (int i = 0; i < k; i++)
        {
            PriorityQueue list3 = zayvki.Peek(); 
            sw.WriteLine($"Remove: {list3.prioritet} {list3.nomer} {list3.step} ");
            zayvki.Remove(zayvki.Peek());
        }
    sw.Close();
    }
}