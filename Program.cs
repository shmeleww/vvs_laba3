using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {

        bool vihod = false;

        while (!vihod)
        {
            Console.WriteLine("Выберите пункт меню:\n 1. Введите число\n 2. Информация об авторе\n 3. Сортировка массива\n 4. Крестики-нолики\n 5. Выход");

            bool proverka = int.TryParse(Console.ReadLine(), out int ViborVMenu);

            while (!proverka || ViborVMenu < 1 || ViborVMenu > 5)
            {
                Console.WriteLine("Введено некорректное значение.");
                proverka = int.TryParse(Console.ReadLine(), out ViborVMenu);
            }

            switch (ViborVMenu)
            {
                case 1:
                    Ugadayka();
                    break;

                case 2:
                    Info();
                    break;

                case 3:
                    Sort();
                    break;

                case 4:
                    Game();
                    break;

                case 5:
                    vihod = Vihod();
                    break;
            }
        }
    }

    static double ReadFirstNumber()
    {
        double odin = 0;
        bool isAvalid = false;

        while (!isAvalid)
        {
            Console.WriteLine("Введите первое число:");

            if (double.TryParse(Console.ReadLine(), out odin))
            {
                if (Math.Cos(2 * odin) <= 0)
                {
                    Console.WriteLine("Число не попадает в ОДЗ");
                }
                else
                {
                    isAvalid = true;
                }
            }
            else
            {
                Console.WriteLine("Введите число");
            }
        }

        return odin;
    }

    static double ReadSecondNumber()
    {
        double dva = 0;

        while (true)
        {
            Console.WriteLine("Введите второе число:");

            if (double.TryParse(Console.ReadLine(), out dva) && dva > 0)
            {
                return dva;
            }

            Console.WriteLine("Значение не попадает в ОДЗ");
        }
    }

    static double Calculation()
    {
        double odin = ReadFirstNumber();
        double dva = ReadSecondNumber();

        double result = Math.Round(
            Math.Pow(
                Math.Sin(Math.Log(dva, 5) / Math.Sqrt(Math.Cos(2 * odin))), 2), 2);

        return result;
    }

    static void Ugadayka()
    {
        double result = Calculation();

        bool ugadal = UgadatResult(result);

        if (!ugadal)
        {
            Console.WriteLine(
                $"Не угадали. Правильным ответом было {result}"
            );
        }
    }

    static bool UgadatResult(double result, int maxAttempts = 3)
    {
        bool ugadal = false;
        int popitka = 1;

        while (popitka <= maxAttempts && !ugadal)
        {
            if (popitka == 1)
            {
                Console.WriteLine("Предположите ответ:");
            }
            else
            {
                Console.WriteLine(
                    $"Не угадали, осталось попыток: {maxAttempts - popitka + 1}."
                );
            }

            if (double.TryParse(Console.ReadLine(), out double otvet)
                && otvet == result)
            {
                Console.WriteLine("Вы угадали!");
                ugadal = true;
            }
            else
            {
                popitka++;
            }
        }

        return ugadal;
    }

    static void Info()
    {
        Console.WriteLine("Автор: Шмелев Александр, 6106-090301d");
    }

    static int[] GenerateArray(int n)
    {
        int[] mass = new int[n];
        Random rand = new Random();

        for (int i = 0; i < n; i++)
        {
            mass[i] = rand.Next(-50, 51);
        }

        return mass;
    }

    static void VivodMass(int[] mass)
    {
        for (int i = 0; i < mass.Length; i++)
        {
            Console.Write(mass[i] + " ");
        }
    }

    static void BubbleSort(int[] bubble)
    {
        for (int i = 0; i < bubble.Length - 1; i++)
        {
            for (int j = 0; j < bubble.Length - i - 1; j++)
            {
                if (bubble[j] > bubble[j + 1])
                {
                    int temp = bubble[j];
                    bubble[j] = bubble[j + 1];
                    bubble[j + 1] = temp;
                }
            }
        }
    }

    static void ShellSort(int[] shell)
    {
        for (int d = shell.Length / 2; d > 0; d /= 2)
        {
            for (int i = d; i < shell.Length; i++)
            {
                int temp = shell[i];
                int j = i;

                while (j >= d && shell[j - d] > temp)
                {
                    shell[j] = shell[j - d];
                    j -= d;
                }

                shell[j] = temp;
            }
        }
    }

    static void Sort()
    {
        int n = 0;
        bool check1 = false;
        bool check2 = false;

        Console.WriteLine("Определите размер массива:");

        while (!check1 || !check2)
        {
            check1 = int.TryParse(Console.ReadLine(), out n);
            check2 = check1 && n > 0 && n <= 10;

            if (!check1)
            {
                Console.WriteLine("Введено некорректное значение");
            }
            else if (!check2)
            {
                Console.WriteLine("Число должно находиться в диапазоне от 1 до 10.");
            }
        }

        int[] mass = GenerateArray(n);

        Console.WriteLine("Сгенерированный массив:");
        VivodMass(mass);

        Console.WriteLine("\n");

        int[] bubble = (int[])mass.Clone();
        int[] shell = (int[])mass.Clone();

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();

        BubbleSort(bubble);

        stopwatch.Stop();

        double bubbleMs = stopwatch.Elapsed.TotalMilliseconds;

        Console.WriteLine("Массив, отсортированный пузырьком:");
        VivodMass(bubble);

        Console.WriteLine(
            $"\nВремя сортировки пузырьком: {bubbleMs} мс\n"
        );

        stopwatch.Restart();

        ShellSort(shell);

        stopwatch.Stop();

        double shellMs = stopwatch.Elapsed.TotalMilliseconds;

        Console.WriteLine("Массив, отсортированный методом Шелла:");
        VivodMass(shell);

        Console.WriteLine(
            $"\nВремя сортировки Шелла: {shellMs} мс\n"
        );

        if (bubbleMs < shellMs)
        {
            Console.WriteLine("Сортировка пузырьком быстрее");
        }
        else if (shellMs < bubbleMs)
        {
            Console.WriteLine("Сортировка Шелла быстрее");
        }
        else
        {
            Console.WriteLine("Время сортировки одинаково");
        }
    }

    static bool Vihod()
    {
        Console.WriteLine("Выйти из программы? (д/н): ");

        string podtverzhdenie = Console.ReadLine();

        if (podtverzhdenie == "д")
        {
            Console.WriteLine("Завершение работы");
            return true;
        }
        else
        {
            Console.WriteLine("Возврат в меню");
            return false;
        }
    }

    // создание карты
    static string[,] GenerateMap()
    {
        int i, g;
        string[,] Map = new string[10, 10];
        for (i = 0; i < 10; i++)
        {
            for (g = 0; g < 10; g++)
            {
                Map[i, g] = "-";
            }
        }
        return Map;
    }

    // вывод карты
    static void PrintMap(string[,] Map)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int g = 0; g < 10; g++)
            {
                Console.Write(Map[i, g] + " ");
            }
            Console.Write("\n");
        }
    }

    // чтение позиции
    static (int p1, int p2) Readpos()
    {
        string pos;
        int p1 = 0;
        int p2 = 0;
        bool check = false;
        while (!check)
        {

            pos = Console.ReadLine();
            if ((pos != null) & ((pos.Length == 2) || (pos.Length == 3)))
            {
                char linet = pos[0];
                p1 = linet - 'a' + 1;
                if ((p1 >= 1) & (p1 <= 10))
                {
                    if (int.TryParse(pos.Substring(1), out p2))
                    {
                        if ((p2 >= 1) & (p2 <= 10))
                        {
                            check = true;
                        }
                        else
                        {
                            Console.WriteLine("Столбцы нумеруются от 1 до 10");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Для столбца введено не число");
                    }
                }
                else
                {
                    Console.WriteLine("Строки нумеруются от a до j");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод, попробуйте ещё раз");
            }

        }
        return (p1 - 1, p2 - 1);
    }

    // проверка победы

    static bool CheckWin(string[,] Map, string symbol)
    {
        // по горизонтали
        for (int p1 = 0; p1 < 10; p1++)
        {
            for (int p2 = 0; p2 <= 5; p2++)
            {
                bool LineWin = true;

                for (int k = 0; k < 5; k++)
                {
                    if (Map[p1, p2 + k] != symbol)
                    {
                        LineWin = false;
                    }
                }

                if (LineWin)
                {
                    return true;
                }
            }
        }

        // по вертикали
        for (int p2 = 0; p2 < 10; p2++)
        {
            for (int p1 = 0; p1 <= 5; p1++)
            {
                bool ColumnWin = true;

                for (int k = 0; k < 5; k++)
                {
                    if (Map[p1 + k, p2] != symbol)
                    {
                        ColumnWin = false;
                    }
                }

                if (ColumnWin)
                {
                    return true;
                }
            }
        }


        // сверху слева
        for (int p1 = 0; p1 <= 5; p1++)
        {
            for (int p2 = 0; p2 <= 5; p2++)
            {
                bool Win = true;

                for (int k = 0; k < 5; k++)
                {
                    if (Map[p1 + k, p2 + k] != symbol)
                    {
                        Win = false;
                    }
                }

                if (Win)
                {
                    return true;
                }
            }
        }

        // сверху справа
        for (int p1 = 0; p1 <= 5; p1++)
        {
            for (int p2 = 9; p2 >= 4; p2--)
            {
                bool Win = true;

                for (int k = 0; k < 5; k++)
                {
                    if (Map[p1 + k, p2 - k] != symbol)
                    {
                        Win = false;
                    }
                }

                if (Win)
                {
                    return true;
                }
            }
        }
        return false;
    }



    static void Game()
    {
        string[,] Map = GenerateMap();
        Console.WriteLine("Пример ввода: a1 (a - строка, 1 - столбец)");
        bool win1 = false;
        bool win2 = false;
        bool draw = false;
        int p1;
        int p2;
        int i = 1;
        while ((win1 == false) & (win2 == false) & (draw == false))
        {
            PrintMap(Map);
            if (i % 2 == 1)
            {
                Console.WriteLine("Первый игрок выбирает позицию:");
                (p1, p2) = Readpos();
                if (Map[p1, p2] == "-")
                {
                    Map[p1, p2] = "X";
                    i++;
                }
                else
                {
                    Console.WriteLine("Клетка уже занята");
                }

                // запуск проверки1
                win1 = CheckWin(Map, "X");
                if (win1 == true)
                {
                    Console.WriteLine("Первый игрок победил");
                    PrintMap(Map);
                }
            }
            else
            {
                Console.WriteLine("Второй игрок выбирает позицию:");
                (p1, p2) = Readpos();
                if (Map[p1, p2] == "-")
                {
                    Map[p1, p2] = "0";
                    i++;
                }
                else
                {
                    Console.WriteLine("Клетка уже занята");
                }

                // запуск проверки2
                win2 = CheckWin(Map, "0");
                if (win2 == true)
                {
                    Console.WriteLine("Второй игрок победил");
                    PrintMap(Map);
                }
                if ((i == 101) & (win1 == false) & (win2 == false))
                {
                    draw = true;
                    Console.WriteLine("Ничья, все поля заполнены");
                }

            }

        }
    }
}











