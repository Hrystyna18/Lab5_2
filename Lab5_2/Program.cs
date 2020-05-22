using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace L5_2
{
    class Program
    {
        public static Workday[] all = new Workday[5];
        public static bool[] delete = new bool[100];
        static void Main(string[] args)
        {
            PutKey.Key();
        }
    }



    abstract class Pool
    {
        public string Name;
        public string Address;
        abstract public int Average(int Number);
        public static void Add()
        {
            Console.WriteLine("\nВведiть данi:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            PutKey.P(elements, true);

            PutKey.Key();
        }

        public static void Remove()
        {
            Console.Write("\nНазвiть басейн, який хочете видалити : ");

            string name = Console.ReadLine();

            bool[] write = new bool[Program.all.Length];

            for (int i = 0; i < Program.all.Length; ++i)
            {
                if (Program.all[i].Name != null)
                {
                    if (Program.all[i].Name == name)
                    {
                        Console.WriteLine("{0,-15} {1, -15} {2, -15} {3, -20} {4,-20} ", Program.all[i].Name, Program.all[i].Address, Program.all[i].Data, Program.all[i].Visitors, Program.all[i].Number);

                        Program.delete[i] = true;

                    }
                }
            }
            PutKey.Key();

        }
        public static void Write(Workday[] s)
        {
            Console.WriteLine("{0,-15} {1, -15}\t {2, -20} {3, -20} {4,-30} ", "Назва", "Адреса", "Дата", "Кiлькiсть вiдвiдувачiв", "Кiлькiсть доступних дорiжок");

            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] != null)
                {
                    Console.WriteLine("{0,-15} {1, -10}\t {2, -20} {3, -20} \t\t{4,-30} ", Program.all[i].Name, Program.all[i].Address, Program.all[i].Data, Program.all[i].Visitors, Program.all[i].Number);
                }
            }
            PutKey.Key();
        }

        public static void Write(Workday[] s, bool[] write)
        {
            Console.WriteLine("{0,-15} {1, -15} {2, -15} {3, -20} {4,-20} {5,-15} {6,-15} ", "Назва", "Адреса", "Дата", "Кiлькiсть вiдвiдувачiв", "Кiлькiсть доступних дорiжок");

            for (int i = 0; i < s.Length; ++i)
            {
                if ((write[i]) && (!Program.delete[i]))
                {
                    Console.WriteLine("{0,-15} {1, -15} {2, -15} {3, -20} {4,-20} ", Program.all[i].Name, Program.all[i].Address, Program.all[i].Data, Program.all[i].Visitors, Program.all[i].Number);
                }
            }
        }
        public static void Edit()
        {
            Console.Write("\nНазва басейну: ");

            string name = Console.ReadLine();

            bool[] write = new bool[Program.all.Length];

            for (int i = 0; i < Program.all.Length; ++i)
            {
                if (Program.all[i] != null)
                {
                    if (Program.all[i].Name == name)
                    {

                        Console.WriteLine("\nВведiть нову iнформацiю: ");

                        string str = Console.ReadLine();

                        string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        Program.all[i] = new Workday(elements[i], elements[i + 1], DateTime.Parse(elements[i + 2]), elements[i + 3], int.Parse(elements[i + 4]));
                    }
                }
            }
            PutKey.Key();
        }


    }
    class Workday : Pool
    {
        public DateTime Data;
        public string Visitors;
        public int Number;
        public Workday(string name, string address, DateTime data, string visitors, int number)
        {
            Name = name;
            Address = address;
            Data = data;
            Visitors = visitors;
            Number = number;
        }
        public static void Find()
        {
            Console.WriteLine("\nЗапишiть: ");
            string str = Console.ReadLine();

            for (int i = 0; i < Program.all.Length; i++)
            {
                if (Program.all[i].Visitors.Contains(str))
                {
                    Console.WriteLine(Program.all[i].Visitors);
                }

            }
            PutKey.Key();
        }
        public override int Average(int Number)
        {
            int s = 0;
            int k = 0;
            for (int i = 0; i < Program.all.Length; i++)
            {
                s += Program.all[i].Number;
                k++;
            }
            Console.WriteLine("\nСередня к-сть вiдвiдувань в день за перiод: ");
            return s / k;
        }
        public static void Min()
        {
            int min = Program.all[0].Number;
            int k = 0;
            for (int i = 0; i < Program.all.Length; i++)
            {

                if (Program.all[i].Number <= min)
                {
                    min = Program.all[i].Number;
                    k = i;
                }
            }
            Console.WriteLine("\nМiнiмальна к-сть доступних дорiжок " + min + "\tПо вiдношеннi до робочого дня " + Program.all[k].Name);
        }

    }


    class PutKey
    {
        public static void Key()
        {
            PutKey.P(PutKey.Read(), false);

            Console.WriteLine("Додати записи: +");
            Console.WriteLine("Редагувати записи: E");
            Console.WriteLine("Знищити записи: R");
            Console.WriteLine("Показати записи: Enter");
            Console.WriteLine("Пошук днiв з мiнiмальною к-стю доступних дорiжок: K");
            Console.WriteLine("Середня к-сть вiдвiдувань в день за перiод: D ");
            Console.WriteLine("К-сть днiв, коли було доступно не менше зазначеної к-стi дорiжок: B ");
            Console.WriteLine("Вихiд: Esc");
            int k = 0;
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.B:
                    Workday.Find();
                    break;

                case ConsoleKey.D:
                    Console.WriteLine(Program.all[0].Average(k));
                    break;

                case ConsoleKey.K:
                    Workday.Min();
                    break;

                case ConsoleKey.OemPlus:
                    Workday.Add();
                    break;

                case ConsoleKey.Enter:
                    Workday.Write(Program.all);
                    break;

                case ConsoleKey.Escape:
                    return;

                case ConsoleKey.E:
                    Workday.Edit();
                    break;

                case ConsoleKey.R:
                    Workday.Remove();
                    break;

            }

        }

        private static string[] Read()
        {
            StreamReader fromFile = new StreamReader("pool.txt");

            return fromFile.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

        }
        private static void Save(Workday n)
        {
            StreamWriter save = new StreamWriter("pool1.txt", true);

            save.WriteLine(n.Name);
            save.WriteLine(n.Address);
            save.WriteLine(n.Data);
            save.WriteLine(n.Visitors);
            save.WriteLine(n.Number);

            save.Close();
        }

        public static void P(string[] elements, bool save)
        {
            int counter = 0;

            while (Program.all[counter] != null)
            {
                ++counter;
            }

            for (int i = 0; i < elements.Length; i += 5)
            {
                Program.all[counter + i / 5] = new Workday(elements[i], elements[i + 1], DateTime.Parse(elements[i + 2]), elements[i + 3], int.Parse(elements[i + 4]));
                if (save)
                {
                    Save(Program.all[counter + i / 5]);
                }
            }
        }

    }
}