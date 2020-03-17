using System;

namespace KeyboardTrainer {
    class Program {
        static void Main(string[] args) {
            ConsoleInit();

            char[] symbols = { 'f', 'j', 'd', 'k', ' ' };
            int totalSymbols = 50;
            char[] type = new char[totalSymbols];
            int symbolsOnScreen = 15;
            int score = 0;

            DateTime start, finish;
            Random rnd = new Random();

            //Сформировать случайную последовательность из
            //набора разрешенных символов
            for (int i = 0; i < totalSymbols; i++) {
                type[i] = symbols[rnd.Next(0, symbols.Length)];
            }

            start = DateTime.Now;

            //Прокрутка последовательности на экране
            for (int i = 0; i < totalSymbols; i++) {
                WriteHead(score, i, totalSymbols);

                //Выводится не более symbolsOnScreen символов за раз
                for (int k = i; k < Math.Min(i + symbolsOnScreen - 1, totalSymbols); k++) {

                    //Текущий символ выделяется цветом
                    if (k == i)
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    else
                        Console.ForegroundColor = ConsoleColor.Black;

                    //Символ пробела выводится как подчеркивание
                    if (type[k] == ' ')
                        Console.Write("_");
                    else
                        Console.Write(type[k]);

                    Console.Write(" ");
                }

                //Ожидание нажатия клавиши
                ConsoleKeyInfo key = Console.ReadKey(true);

                //Правильная клавиша увеличивает счетчик очков
                if (key.KeyChar.Equals(type[i]))
                    score++;                
            }

            //Определить время, затраченное на прохождение
            finish = DateTime.Now;
            double totalTime = (finish - start).TotalSeconds;

            Console.Clear();
            ShowStats(score, totalSymbols, totalTime);
            while (true) ;
        }

        static void ConsoleInit() {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        static void WriteHead(int score, int currentSymbol, int totalSymbols) {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\tОчки: " + score + "\t\tСимвол: " + (currentSymbol + 1) + "/" + totalSymbols);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("\t\t");
        }

        static void ShowStats(int score, int totalSymbols, double time) {
            double accuracy = (double)score / totalSymbols;

            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Write("\t\tУровень пройден за ");

            if (time <= 30.0)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.Write(Math.Round(time));
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" секунд");

            Console.Write("\t\tТочность: ");

            if (accuracy >= 0.75)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (accuracy <= 0.25)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(Math.Round(accuracy * 100) + "%");
        }
    }
}
