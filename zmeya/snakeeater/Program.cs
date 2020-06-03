using System;
using System.Globalization;
using System.Threading;

namespace snakeeater
{
    class Program
    {
        public static char[,] Arr;
        public static int x, y, q, z, n = 0, field = 0, tikrate, border=0;
        public static ConsoleKeyInfo memory;
        public static int[] snakeX = new int[256];
        public static int[] snakeY = new int[256];
        public static bool death = false;

        static void Main(string[] args)
        {
            CreateArray();
            OutArr();
            SnakeEater();
            Console.ReadKey();
        }
        public static ConsoleKeyInfo moves()
        {
            ConsoleKeyInfo knop;
            knop = Console.ReadKey();
            memory = knop;
            return knop;
        }

        public static char[,] SnakeEater()
        {
            while (x != 0 && y != 0 && x != field-1 && y != field-1 && death == false)
            {
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo knop = moves();
                    if (knop.Key.ToString() == "LeftArrow")
                    {
                        TailMoves();
                        Arr[x, y - 1] = Arr[x, y];
                        if (n == 0)
                            Arr[x, y] = '_';
                        y -= 1;
                        Arr[x, y] = '9';
                    }
                    else if (knop.Key.ToString() == "UpArrow")
                    {
                        TailMoves();
                        Arr[x - 1, y] = Arr[x, y];
                        if (n == 0)
                            Arr[x, y] = '_';
                        x -= 1;
                        Arr[x, y] = '9';
                    }
                    else if (knop.Key.ToString() == "DownArrow")
                    {
                        TailMoves();
                        Arr[x + 1, y] = Arr[x, y];
                        if (n == 0)
                            Arr[x, y] = '_';
                        x += 1;
                        Arr[x, y] = '9';
                    }
                    else if (knop.Key.ToString() == "RightArrow")
                    {
                        TailMoves();
                        Arr[x, y + 1] = Arr[x, y];
                        if (n == 0)
                            Arr[x, y] = '_';
                        y += 1;
                        Arr[x, y] = '9';
                    }

                }
                else if (Console.KeyAvailable == false)
                {
                    if (memory.Key.ToString() == "LeftArrow")
                    {
                        TailMoves();
                        Arr[x, y - 1] = Arr[x, y];
                        if (n == 0)
                            Arr[x, y] = '_';
                        y -= 1;
                        Arr[x, y] = '9';
                    }
                    else if (memory.Key.ToString() == "UpArrow")
                    {
                        TailMoves();
                        Arr[x - 1, y] = Arr[x, y];
                        if (n == 0)
                            Arr[x, y] = '_';
                        x -= 1;
                        Arr[x, y] = '9';
                    }
                    else if (memory.Key.ToString() == "DownArrow")
                    {
                        TailMoves();
                        Arr[x + 1, y] = Arr[x, y];
                        if (n == 0)
                            Arr[x, y] = '_';
                        x += 1;
                        Arr[x, y] = '9';
                    }
                    else if (memory.Key.ToString() == "RightArrow")
                    {
                        TailMoves();
                        Arr[x, y + 1] = Arr[x, y];
                        if (n == 0)
                            Arr[x, y] = '_';
                        y += 1;
                        Arr[x, y] = '9';
                    }
                }

                if (x == q && y == z)
                {
                    DwarfWine(Arr);
                    AddTailtiles();
                }

                Console.SetCursorPosition(0, 0);
                OutArr();

                for (int i = 1; i < n - 1; i++)
                {
                    if (x == snakeX[i] && y == snakeY[i])
                    {
                        death = true;
                    }
                }

                Thread.Sleep(tikrate);
            }
            Console.WriteLine("Ну всё");
            return Arr;
        }
        public static void TailMoves()
        {
            for (int i = 0; i < n; i++)
            {
                Arr[snakeX[i], snakeY[i]] = '_';
            }

            for (int i = n; i >= 1; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
            }

            snakeX[0] = x;
            snakeY[0] = y;

            for (int i = 0; i < n; i++)
            {
                Arr[snakeX[i], snakeY[i]] = 's';
            }
        }
        public static void AddTailtiles()
        {
            snakeX[n] = x;
            snakeY[n] = y;
            n++;
        }
        public static char[,] DwarfWine(char[,] mass)
        {
            Random rand = new Random();
        SpawnFood:
            q = rand.Next(1, field - 1);
            z = rand.Next(1, field - 1);

            for (int i = 0; i < n; i++)
            {
                if (q == snakeX[i] && z == snakeY[i])
                    goto SpawnFood;
            }

            if (x == q && y == z)
            {
                goto SpawnFood;
            }

            else
            {
                mass[q, z] = '@';
            }
            return mass;
        }
        
        public static char[,] CreateArray()
        {
            int difficulty;
        Enters:
            Console.WriteLine("Введите уровень сложности 1-лёгкий 2-средний 3-сложный)");
            difficulty = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            switch (difficulty)
            {
                case 1:
                    field = 10;
                    tikrate = 350;
                    break;
                case 2:
                    field = 15;
                    tikrate = 250;
                    break;
                case 3:
                    field = 20;
                    tikrate = 150;
                    break;
                default:
                    Console.WriteLine("Неверно");
                    goto Enters;
            }         
            Arr = new char[field, field];
            for (int i = 0; i < field; i++)
            {
                for (int j = 0; j < field; j++)
                {
                    if (i == 0 || j == 0 || i == field - 1 || j == field - 1)
                    {
                        Arr[i, j] = '#';
                    }
                    else
                    {
                        Arr[i, j] = ' ';
                    }
                }
            }


            Random rand = new Random();
         FoodTicket:
            x = rand.Next(2, field - 2);
            y = rand.Next(2, field - 2);
            q = rand.Next(2, field - 2);
            z = rand.Next(2, field - 2);
            if (x == q && y == z)
            {
                goto FoodTicket;
            }
            else
            {
                Arr[x, y] = '9';
                Arr[q, z] = '@';
            }

            return Arr;
        }
        public static char[,] OutArr()
        {
            for (int i = 0; i < field; i++)
            {
                for (int j = 0; j < field; j++)
                {
                    Console.Write(Arr[i, j] + " ");
                }
                Console.WriteLine();
            }                              
            return Arr;
        }
    }
}