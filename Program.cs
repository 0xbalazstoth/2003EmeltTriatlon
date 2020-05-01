using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2003EmeltTriatlon
{
    class Triatlon
    {
        public static List<Triatlon> Adat = new List<Triatlon>();

        public int Sor;
        public string Nev;
        public int Uszas;
        public int Kerekpar;
        public int Futas;

        public Triatlon(int sor, string nev, int uszas, int kerekpar, int futas)
        {
            Sor = sor;
            Nev = nev;
            Uszas = uszas;
            Kerekpar = kerekpar;
            Futas = futas;
        }

        public static void ElsoFeladat(string fajl)
        {
            int sor = 0;

            using (StreamReader olvas = new StreamReader(fajl))
            {
                int db = Convert.ToInt32(olvas.ReadLine());

                while (!olvas.EndOfStream)
                {
                    string nev = olvas.ReadLine();
                    int uszas = Convert.ToInt32(olvas.ReadLine());
                    int kerekpar = Convert.ToInt32(olvas.ReadLine());
                    int futas = Convert.ToInt32(olvas.ReadLine());

                    sor++;

                    Triatlon triatlon = new Triatlon(sor, nev, uszas, kerekpar, futas);

                    Adat.Add(triatlon);
                }
            }
        }

        public static void MasodikFeladat()
        {
            var top = Adat.Select(x => new { Eredmeny = x.Futas + x.Kerekpar + x.Uszas, Nev = x.Nev }).OrderBy(x => x.Eredmeny).ToList();

            Console.WriteLine($"2. feladat: Top 3 versenyző: ");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(top[i].Nev);
            }
        }

        public static void HarmadikFeladat()
        {
            Console.WriteLine("\n3. feladat: Top 1 versenyző átlagsebessége: ");
            var elso = Adat.Select(x => new { Eredmeny = x.Futas + x.Kerekpar + x.Uszas, Futas = x.Futas, Uszas = x.Uszas, Kerekpar = x.Kerekpar, Nev = x.Nev }).OrderBy(x => x.Eredmeny).First();

            TimeSpan futas = TimeSpan.FromSeconds(elso.Futas);
            TimeSpan uszas = TimeSpan.FromSeconds(elso.Uszas);
            TimeSpan kerekpar = TimeSpan.FromSeconds(elso.Kerekpar);

            Console.WriteLine($"Úszás: {Math.Round(1.5 / uszas.TotalHours, 2)} km/h\nKerékpározás: {Math.Round(40 / kerekpar.TotalHours, 2)} km/h\nFutás: {Math.Round(10 / futas.TotalHours, 2)} km/h");
        }

        public static void NOHFeladat()
        {
            Console.WriteLine("\n4. 5. 6. feladat: Versenyzők végső eredményei kiíratva");

            var stat = Adat.Select(x => new { Eredmeny = x.Futas + x.Kerekpar + x.Uszas, Nev = x.Nev }).OrderBy(x => x.Eredmeny).ToList();

            using (StreamWriter ki = new StreamWriter("triatlon.ki"))
            {
                for (int i = 0; i < stat.Count; i++)
                {
                    TimeSpan futas = TimeSpan.FromSeconds(stat[i].Eredmeny);
                    string eredmeny = futas.ToString(@"hh\:mm\:ss");

                    ki.WriteLine($"{stat[i].Nev} {eredmeny}");
                    ki.Flush();
                }
            }
        }

        public static void HetedikFeladat()
        {
            Console.WriteLine("\n7. feladat: Top 3 versenyző kiíratva kategóriánként");

            var topUszas = Adat.Select(x => new { Uszas = TimeSpan.FromSeconds(x.Uszas).ToString(@"hh\:mm\:ss"), Nev = x.Nev }).OrderBy(x => x.Uszas).First();
            var topKerekpar = Adat.Select(x => new { Kerekpar = TimeSpan.FromSeconds(x.Kerekpar).ToString(@"hh\:mm\:ss"), Nev = x.Nev }).OrderBy(x => x.Kerekpar).First();
            var topFutas = Adat.Select(x => new { Futas = TimeSpan.FromSeconds(x.Futas).ToString(@"hh\:mm\:ss"), Nev = x.Nev }).OrderBy(x => x.Futas).First();

            using (StreamWriter ki = new StreamWriter(@"reszer.ki"))
            {
                ki.WriteLine($"{topUszas.Nev} {topUszas.Uszas}");
                ki.WriteLine($"{topKerekpar.Nev} {topKerekpar.Kerekpar}");
                ki.WriteLine($"{topFutas.Nev} {topFutas.Futas}");

                ki.Flush();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Feladatok();

            Console.ReadKey();
        }

        private static void Feladatok()
        {
            Triatlon.ElsoFeladat(@"TRIATLON.BE");
            Triatlon.MasodikFeladat();
            Triatlon.HarmadikFeladat();
            Triatlon.NOHFeladat();
            Triatlon.HetedikFeladat();
        }
    }
}
