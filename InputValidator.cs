using System;
using System.Collections.Generic;

public static class InputValidator
{
    public static int GetIntInput(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
            {
                if (value >= minValue && value <= maxValue)
                {
                    return value;
                }
                Console.WriteLine($"Ошибка: число должно быть в диапазоне от {minValue} до {maxValue}.");
            }
            else
            {
                Console.WriteLine("Ошибка: введите целое число.");
            }
        }
    }
    
    public static string GetStringInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
    
    public static char GetCharInput(string prompt)
    {
        char value;
        while (true)
        {
            Console.Write(prompt);
            if (char.TryParse(Console.ReadLine(), out value))
            {
                return value;
            }
            Console.WriteLine("Ошибка: введите один символ.");
        }
    }

    public static List<int> GetOrderedList(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            
            try
            {
                string[] parts = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                List<int> list = new List<int>();
                
                foreach (string part in parts)
                {
                    list.Add(int.Parse(part));
                }
                
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (list[i] > list[i + 1])
                    {
                        throw new ArgumentException("Список должен быть упорядочен по возрастанию");
                    }
                }
                
                return list;
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введите целые числа, разделенные пробелами");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    public static List<int> GetList(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            
            try
            {
                string[] parts = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                List<int> list = new List<int>();
                
                foreach (string part in parts)
                {
                    list.Add(int.Parse(part));
                }
                
                return list;
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введите целые числа, разделенные пробелами");
            }
        }
    }

    public static List<string> GetStringList(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: ввод не может быть пустым");
                continue;
            }

            string[] parts = input.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            
            foreach (string part in parts)
            {
                string trimmed = part.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    result.Add(trimmed);
                }
            }
            
            return result;
        }
    }

    public static Participant GetParticipant(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: ввод не может быть пустым");
                continue;
            }

            string[] parts = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length < 6)
            {
                Console.WriteLine("Ошибка: неверный формат ввода. Ожидается: Фамилия Имя балл1 балл2 балл3 балл4");
                continue;
            }

            try
            {
                int[] scores = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    scores[i] = int.Parse(parts[i + 2]);
                    if (scores[i] < 0 || scores[i] > 10)
                    {
                        throw new ArgumentException("Баллы должны быть от 0 до 10");
                    }
                }

                return new Participant
                {
                    LastName = parts[0],
                    FirstName = parts[1],
                    Scores = scores
                };
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: баллы должны быть целыми числами");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
