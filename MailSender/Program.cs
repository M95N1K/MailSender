using System;

internal class Program
{
    internal static void Main()
    {
        Console.WriteLine("Здравствуйте. Вас приветствует математическая программа.");
        int intAns = DataInput();

        if (intAns < 0)
            return;

        int fectorial = 1; 
        int sum = 0;
        int maxEven = 0;

        for (int i = 1; i <= intAns; i++)
        {
            fectorial *= i;
            sum += i;
            if ((i < intAns) && (i%2 == 0))
            {
                maxEven = i;
            }
        }
        Console.WriteLine($"Факториал равет {fectorial}"); 
        Console.WriteLine($"Сума от 1 до N равна {sum}");
        Console.WriteLine($"Максимальное четное число, меньше N, равно: {maxEven}");
        Console.WriteLine("Для выхода нажмите Enter");
        Console.ReadLine();
    }

    private static int DataInput()
    {
        int result;
        bool flag;
        do
        {
            Console.WriteLine("Пожалуйста, введите число N больше 0 (для выхода введите 'q'). ");
            string answer = Console.ReadLine();

            if (answer == "q")
            {
                return -1;
            }
            flag = Int32.TryParse(answer, out result);
            if (result <= 0)
                flag = false;
            if (!flag)
            {
                Console.WriteLine("Некорректный ввод!");
            }
        } while (!flag);
        return result;
    }
}
