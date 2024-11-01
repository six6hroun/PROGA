using MyVector;
using System.Numerics;

namespace Lab7
{
    public class Lab7
    {
        static string fInput = "B:/input.txt";
        static string fOutput = "B:/output.txt";
        static StreamReader sr = new StreamReader(fInput);
        static StreamWriter sw = new StreamWriter(fOutput);

        static MyVector <string> Ip()
        {
            string line = sr.ReadLine();
            if (line == null) throw new Exception("Empty line");
            var result = new MyVector<string>(10);
            while (line != null)
            {
                string[] ipArray = line.Split(' ');
                foreach (string ip in ipArray)
                {
                    bool isIp = true;
                    string[] helpArray = ip.Split('.').ToArray();
                    int[] ipBlock = new int[helpArray.Length];
                    for (int i = 0; i < helpArray.Length; i++)
                    {
                        ipBlock[i] = Convert.ToInt32(helpArray[i]);
                    }
                    foreach (int i in ipBlock)
                    {
                        if (i > 255 || i < 0) isIp = false;
                    }
                    if (isIp && ipBlock.Length == 4) result.Add(ip);
                }
                line = sr.ReadLine();
            }
            return result;
        }
        static void WriteToFile(MyVector<string> result)
        {
            for (int i = 0; i < result.Size(); i++)
            {
                sw.WriteLine(result.Get(i));
            }
            sw.Close();
        }
        static void Main(string[] args)
        {
            MyVector<string> ip = new MyVector<string>(10);
            ip = Ip();
            WriteToFile(ip);
        }
    }
}