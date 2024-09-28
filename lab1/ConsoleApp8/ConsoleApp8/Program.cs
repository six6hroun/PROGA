string text = "Z://xd.txt";
StreamReader sr = new StreamReader(text);

int n;
n = Convert.ToInt32(sr.ReadLine()); // считываем размероность

double[,] matr = new double[n, n]; // считываем матрицу
for (int i = 0; i < n; i++)
{
    string[] line1 = sr.ReadLine().Split(" ");
    for (int j = 0; j < n; j++)
    {
        matr[i, j] = double.Parse(line1[j]);
    }
}

double[] vector = new double[n]; // считываем вектор
string[] line2 = sr.ReadLine().Split(" ");
for (int i = 0; i < n; i++)
{
    vector[i] = double.Parse(line2[i]);
}

bool Simetr(double[,] matr) // проверка на симетричность
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (matr[i, j] != matr[j, i]) return false;
        }
    }
    return true;
}

double length = 0;
if (Simetr(matr))
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            length += vector[i] * matr[i, j] * vector[j]; //длина вектора
        }
    }
    length = Math.Sqrt(length); // извлекаем корень
Console.WriteLine($"Vector length: {length}");
}
else Console.WriteLine("h");

