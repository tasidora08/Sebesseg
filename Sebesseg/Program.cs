namespace Sebesseg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string[]> adatok = new List<string[]>();
            StreamReader sr = new StreamReader("ut.txt");
            string[] elsoSor = sr.ReadLine().Split();
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split();
                adatok.Add(sor);
            }
            Console.WriteLine($"2. feladat\nA települések neve:");
            foreach (string[] sor in adatok)
            {
                string sorMasodik = sor[1];
                if (sorMasodik.Length >= 4)
                {
                    Console.WriteLine(sor[1]);
                }
            }
            Console.WriteLine("\n3. feladat");
        }
    }
}
