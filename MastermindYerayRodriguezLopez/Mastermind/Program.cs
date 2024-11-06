using System;

namespace Mastermin
{
    public class Mastermind
    {
        const string Title = "888b     d888                   888                                  d8b               888\n" +
                                "8888b   d8888                   888                                  Y8P               888\n" +
                                "88888b.d88888                   888                                                    888\n" +
                                "888Y88888P888  8888b.  .d8888b  888888 .d88b.  888d888 88888b.d88b.  888 88888b.   .d88888\n" +
                                "888 Y888P 888      88b 88K      888   d8P  Y8b 888P    888  888  88b 888 888  88b d88  888\n" +
                                "888  Y8P  888 .d888888  Y8888b. 888   88888888 888     888  888  888 888 888  888 888  888\n" +
                                "888       888 888  888      X88 Y88b. Y8b.     888     888  888  888 888 888  888 Y88b 888\n" +
                                "888       888  Y888888  88888P    Y888  Y8888  888     888  888  888 888 888  888   Y88888";

        const string Menu = " ________________________________ \n" +
                            "|1. Dificultat Novell: 10 intents  |\n" +
                            "|2. Dificultat Aficionat: 6 intents|\n" +
                            "|3. Dificultat Expert: 4 intents   |\n" +
                            "|4. Dificultat Màster: 3 intents   |\n" +
                            "|5. Dificultat Personalitzada      |\n" +
                            "|6. Salir                          |\n" +
                            " ---------------------------------- \n" +
                            "Selecciona la dificultat (1-6): ";
        const string InvalidOption = "Opció no vàlida. Tria entre 1 i 6.";
        const string EnterCustomAttempts = "Introdueix el nombre d'intents personalitzat: ";
        const string PromtCombination = "Introdueix una combinació de 4 números (1-6): ";
        const string InvalidNumbers = "Els números han de ser entre 1 i 6.";
        const string UnexpectedError = "El nombre d'intents ha de ser un número enter positiu.";
        const string InvalidFormat = "Introdueix exactament 4 números separats per espais.";
        const string HintAlmost = "Casi! Pista: ";
        const string Win = "Felicitats! Has endevinat la combinació secreta!";
        const string Lose = "Ho sentim! No has encertat la combinació secreta. La combinació era: ";
        const string AttemptPrefix = "\nIntent:";
        const string AskToContinue = "Vols seguir jugant? (Si/No): ";

        public static void Main(string[] args)
        {
            //We use maxAttempts also as a form for the user to exit.
            int maxAttempts = 10;
            do
            {
                Console.Clear();
                ShowWelcomeMessage();
                maxAttempts = SelectDifficulty();
                maxAttempts = maxAttempts > 0 ? Game(maxAttempts) : maxAttempts;
            } while (maxAttempts > 0); // Repeat if user wants to continue
        }

        public static int Game(int maxAttempts)
        {
            int[] secretCombination = { 2, 4, 6, 1 };
            int[] userCombination = new int[secretCombination.Length];
            bool won = false;
            string hint = "";

            Console.Clear();

            for (int attempt = 0; attempt < maxAttempts && !won; attempt++)
            {
                Console.WriteLine(Title);
                Console.WriteLine(AttemptPrefix + $"{attempt+1}/{maxAttempts}");

                if (attempt > 0 && !won)
                {
                    CompleteHint(hint, userCombination); // Show hint for previous attempts
                }

                maxAttempts = GetUserCombination(userCombination, maxAttempts); // Get user input

                if (userCombination == null)
                {
                    Console.WriteLine("Error inesperat en l'entrada de dades. S'acaba el joc.");
                    return 0;
                }

                hint = GenerateHint(userCombination, secretCombination); // Generate hint based on user's guess

                if (hint == "OOOO")
                {
                    CompleteHint(hint, userCombination);
                    Console.WriteLine(Win);
                    won = true;
                }
                else
                {
                    Console.Clear();
                }
            }

            if (!won)
            {
                Console.WriteLine($"{Lose}{string.Join(" ", secretCombination)}"); // Show correct combination on loss
            }
            Console.WriteLine(AskToContinue);

            return GetContinue();
        }

        // Display hint for previous attempts
        public static void CompleteHint(string hint, int[] lastAttempt)
        {
            Console.Write("\n{" + hint + "}|{");
            Hint(lastAttempt, "");
            Console.WriteLine("}\n");
        }

        // Display last attempt numbers with colored background
        public static void Hint(int[] lastAttempt, string attempt)
        {
            for (int i = 0; i < lastAttempt.Length; i++)
            {
                switch (lastAttempt[i])
                {
                    case 1: Console.BackgroundColor = ConsoleColor.DarkBlue; break;
                    case 2: Console.BackgroundColor = ConsoleColor.DarkGreen; break;
                    case 3: Console.BackgroundColor = ConsoleColor.DarkYellow; break;
                    case 4: Console.BackgroundColor = ConsoleColor.DarkCyan; break;
                    case 5: Console.BackgroundColor = ConsoleColor.DarkRed; break;
                    case 6: Console.BackgroundColor = ConsoleColor.DarkGray; break;
                    default: Console.BackgroundColor = ConsoleColor.Black; break;
                }

                Console.Write(lastAttempt[i]);
                attempt += lastAttempt[i];
                Console.BackgroundColor = ConsoleColor.Black; 
            }
        }

        public static int GetContinue()
        {
            int tries = 0;
            do
            {
                switch (Console.ReadLine())
                {
                    case "Si": case "si": return 1;
                    case "No": case "no": return 0;
                    default:
                        Console.WriteLine("Valor no valid, respon amb Si o No");
                        tries++;
                        break;
                }
            } while (tries < 10);
            return 0;
        }

        public static void ShowWelcomeMessage()
        {
            Console.WriteLine(Title);
            Console.Write(Menu);
        }

        public static int SelectDifficulty()
        {
            int tries = 0;
            while (tries < 10)
            {
                switch (Console.ReadLine())
                {
                    case "1": return 10;
                    case "2": return 6;
                    case "3": return 4;
                    case "4": return 3;
                    case "5": return GetCustomAttempts();
                    case "6": return 0;
                    default:
                        Console.WriteLine(InvalidOption);
                        tries++;
                        break;
                }
            }
            return 0;
        }

        public static int GetCustomAttempts()
        {
            int tries = 0;
            while (tries < 10)
            {
                Console.Write(EnterCustomAttempts);
                if (int.TryParse(Console.ReadLine(), out int attempts) && attempts > 0)
                {
                    return attempts;
                }
                else
                {
                    Console.WriteLine(UnexpectedError);
                    tries++;
                }
            }
            return 0;
        }

        public static int GetUserCombination(int[] combination, int maxAttempts)
        {
            int tries = 0;
            while (tries < 10)
            {
                Console.Write(PromtCombination);
                string input = Console.ReadLine();
                string[] numbers = input.Split(' ');

                if (numbers.Length != 4)
                {
                    Console.WriteLine(InvalidFormat);
                    tries++;
                }
                else
                {
                    bool validInput = true;

                    for (int i = 0; i < 4; i++)
                    {
                        if (int.TryParse(numbers[i], out int num) && num >= 1 && num <= 6)
                        {
                            combination[i] = num;
                        }
                        else
                        {
                            Console.WriteLine(InvalidNumbers);
                            validInput = false;
                            tries++;
                        }
                    }

                    if (validInput)
                    {
                        return maxAttempts;
                    }
                }
            }
            return 0;
        }

        public static string GenerateHint(int[] userCombination, int[] secretCombination)
        {
            char[] hint = new char[4];
            bool[] usedSecret = new bool[4];
            bool[] usedUser = new bool[4];

            for (int i = 0; i < 4; i++)
            {
                if (userCombination[i] == secretCombination[i])
                {
                    hint[i] = 'O';
                    usedSecret[i] = true;
                    usedUser[i] = true;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (!usedUser[i])
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (!usedSecret[j] && userCombination[i] == secretCombination[j])
                        {
                            hint[i] = 'Ø';
                            usedSecret[j] = true;
                            usedUser[i] = true;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (hint[i] != 'O' && hint[i] != 'Ø')
                {
                    hint[i] = '×';
                }
            }

            return new string(hint);
        }
    }
}
