using System;
using System.Collections.Generic;
using System.IO;

namespace Sebesseg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string[]> adatok = new List<string[]>();
            StreamReader sr = new StreamReader("ut.txt");
            int teljesHossz = int.Parse(sr.ReadLine());
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split();
                adatok.Add(sor);
            }
            sr.Close();
            Console.WriteLine("2. feladat");
            Console.WriteLine("A települések neve:");
            foreach (string[] sor in adatok)
            {
                string jel = sor[1];
                if (jel.Length >= 4 && jel != "]" && jel != "#" && jel != "%")
                {
                    Console.WriteLine(jel);
                }
            }

            Console.WriteLine("\n3. feladat");
            Console.Write("Adja meg a vizsgált szakasz hosszát km-ben! ");
            double km = double.Parse(Console.ReadLine());
            int meter = (int)(km * 1000);
            int aktualisSebesseg = 90;
            int minimum = 90;
            bool varosban = false;
            foreach (string[] sor in adatok)
            {
                int tav = int.Parse(sor[0]);
                if (tav > meter)
                {
                    break;
                }
                string jel = sor[1];
                if (jel.Length >= 4 && jel != "]" && jel != "#" && jel != "%")
                {
                    varosban = true;
                    aktualisSebesseg = 50;
                }
                else if (jel == "]")
                {
                    varosban = false;
                    aktualisSebesseg = 90;
                }
                else if (char.IsDigit(jel[0]))
                {
                    aktualisSebesseg = int.Parse(jel);
                }
                else if (jel == "%")
                {
                    if (varosban)
                    {
                        aktualisSebesseg = 50;
                    }
                    else
                    {
                        aktualisSebesseg = 90;
                    }
                }
                if (aktualisSebesseg < minimum)
                {
                    minimum = aktualisSebesseg;
                }
            }
            Console.WriteLine($"Az első {km} km-en {minimum} km/h volt a legalacsonyabb megengedett sebesség.");

            Console.WriteLine("\n4. feladat");
            bool telepulesenBelul = false;
            int elozoTav = 0;
            int varosiHossz = 0;
            foreach (string[] sor in adatok)
            {
                int tav = int.Parse(sor[0]);
                string jel = sor[1];
                if (telepulesenBelul)
                {
                    varosiHossz += tav - elozoTav;
                }
                if (jel.Length >= 4 && jel != "]" && jel != "#" && jel != "%")
                {
                    telepulesenBelul = true;
                }
                else if (jel == "]")
                {
                    telepulesenBelul = false;
                }
                elozoTav = tav;
            }
            double szazalek = (double)varosiHossz / teljesHossz * 100;
            Console.WriteLine($"Az út {szazalek:F2} százaléka vezet településen belül.");

            Console.WriteLine("\n5. feladat");
            Console.Write("Adja meg egy település nevét! ");
            string keresett = Console.ReadLine();
            bool bent = false;
            int tablaDb = 0;
            int kezdet = 0;
            int veg = 0;
            foreach (string[] sor in adatok)
            {
                int tav = int.Parse(sor[0]);
                string jel = sor[1];
                if (jel == keresett)
                {
                    bent = true;
                    kezdet = tav;
                }
                else if (bent && jel == "]")
                {
                    veg = tav;
                    bent = false;
                }
                else if (bent && char.IsDigit(jel[0]))
                {
                    tablaDb++;
                }
            }
            Console.WriteLine($"A sebességkorlátozó táblák száma: {tablaDb}");
            Console.WriteLine($"Az út hossza a településen belül {veg - kezdet} méter.");

            Console.WriteLine("\n6. feladat");
            List<string> telepulesek = new List<string>();
            List<int> kezdoPontok = new List<int>();
            List<int> vegPontok = new List<int>();
            bool varos = false;
            foreach (string[] sor in adatok)
            {
                int tav = int.Parse(sor[0]);
                string jel = sor[1];
                if (jel.Length >= 4 && jel != "]" && jel != "#" && jel != "%")
                {
                    telepulesek.Add(jel);
                    kezdoPontok.Add(tav);
                    varos = true;
                }
                else if (jel == "]" && varos)
                {
                    vegPontok.Add(tav);
                    varos = false;
                }
            }
            int index = telepulesek.IndexOf(keresett);
            string legkozelebbi = "";
            int minTav = int.MaxValue;
            if (index > 0)
            {
                int tav = kezdoPontok[index] - vegPontok[index - 1];
                minTav = tav;
                legkozelebbi = telepulesek[index - 1];
            }
            if (index < telepulesek.Count - 1)
            {
                int tav = kezdoPontok[index + 1] - vegPontok[index];
                if (tav < minTav)
                {
                    minTav = tav;
                    legkozelebbi = telepulesek[index + 1];
                }
            }
            Console.WriteLine($"A legközelebbi település: {legkozelebbi}");
        }
    }
}