using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        string filePath = @"G:\Outros computadores\Meu laptop\Projetos\Back-End\CSharp\Livros\EntendendoAlgoritmos\Cap1-BuscaBinaria\base.txt";
        Stopwatch stopwatch = new Stopwatch();

        try
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 0; i <= 1000000; i++)
                    {
                        writer.WriteLine(i + 1);
                    }
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Erro para criar o arquivo");
            Console.WriteLine($"Mensagem: {e.Message}");
        }

        List<int> lstInt = new List<int>();

        try
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                while (!sr.EndOfStream)
                {
                    lstInt.Add(int.Parse(sr.ReadLine()));
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Erro para ler o arquivo");
            Console.WriteLine($"Mensagem: {e.Message}");
        }

        Console.Clear();
        Console.WriteLine("\n\tTeste de Eficiência com Busca Binária\n");

        int target = 999999;

        stopwatch.Start();
        BinarySearch(lstInt, target);
        stopwatch.Stop();
        TimeSpan durationBuscaBinaria = stopwatch.Elapsed;

        stopwatch.Start();
        lstInt.Find(x => x == target);
        stopwatch.Stop();
        TimeSpan durationBuscaNormal = stopwatch.Elapsed;

        stopwatch.Start();
        lstInt.Where(t => t == target);
        stopwatch.Stop();
        TimeSpan durationWhere = stopwatch.Elapsed;

        Console.WriteLine($"\tTempo de Execução" +
            $"\n\tBusca Binária: {durationBuscaBinaria.TotalSeconds}." +
            $"\n\tBusca Normal: {durationBuscaNormal.TotalSeconds}." +
            $"\n\tBusca Where: {durationWhere.TotalSeconds}");

        Console.WriteLine("\n\tFim do Teste");
    }
    private static int? BinarySearch(IList<int> list, int item)
    {
        var low = 0;
        var high = list.Count() - 1;

        while (low <= high)
        {
            var mid = (low + high) / 2;
            var guess = list[mid];

            if (guess == item) 
                return mid;

            if (guess > item)
                high = mid - 1;
            else
                low = mid + 1;
        }

        return null;
    }
}