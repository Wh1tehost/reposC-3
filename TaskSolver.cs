using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public static class TaskSolver
{
    // Методы для задачи 1
    public static void GenerateNumbersFile(string filePath, int count, int minValue, int maxValue)
    {
        Random random = new Random();
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(random.Next(minValue, maxValue + 1));
            }
        }
    }
    
    public static int CalculateMaxMinDifference(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл '{filePath}' не найден.");
        }
        
        var numbers = File.ReadAllLines(filePath)
                         .Select(line => int.Parse(line))
                         .ToList();
        
        if (numbers.Count == 0)
        {
            throw new InvalidOperationException("Файл пуст.");
        }
        
        int max = numbers.Max();
        int min = numbers.Min();
        
        return max - min;
    }
    
    // Методы для задачи 2
    public static void GenerateMultiNumbersFile(string filePath, int count, int minValue, int maxValue, int numbersPerLine)
    {
        Random random = new Random();
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(random.Next(minValue, maxValue + 1));
                
                if ((i + 1) % numbersPerLine == 0 || i == count - 1)
                {
                    writer.WriteLine();
                }
                else
                {
                    writer.Write(" ");
                }
            }
        }
    }
    
    public static int FindMinElement(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл '{filePath}' не найден.");
        }
        
        var numbers = File.ReadAllLines(filePath)
                         .Where(line => !string.IsNullOrWhiteSpace(line))
                         .SelectMany(line => line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                         .Select(num => int.Parse(num))
                         .ToList();
        
        if (numbers.Count == 0)
        {
            throw new InvalidOperationException("Файл пуст.");
        }
        
        return numbers.Min();
    }
    
    // Методы для задачи 3
    public static void GenerateTextFile(string filePath, int lineCount)
    {
        string[] sampleLines = {
            "Apple", "Banana", "Cherry", "Date", "Elderberry",
            "Fig", "Grape", "Honeydew", "Kiwi", "Lemon",
            "Mango", "Nectarine", "Orange", "Peach", "Quince",
            "Raspberry", "Strawberry", "Tangerine", "Watermelon"
        };
        
        Random random = new Random();
        
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < lineCount; i++)
                {
                    string line = sampleLines[random.Next(sampleLines.Length)];
                    // Добавляем случайные пробелы в начале строки с вероятностью 30%
                    if (random.NextDouble() < 0.3)
                    {
                        int spacesCount = random.Next(1, 5);
                        line = new string(' ', spacesCount) + line;
                    }
                    writer.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при создании файла: {ex.Message}");
        }
    }

    public static int CopyLinesStartingWith(string sourcePath, string destPath, char startChar)
    {
        if (!File.Exists(sourcePath))
        {
            throw new FileNotFoundException($"Файл '{sourcePath}' не найден.");
        }
        
        List<string> resultLines = new List<string>();
        
        try
        {
            // Читаем все строки из файла
            string[] allLines = File.ReadAllLines(sourcePath);
            
            foreach (string line in allLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                
                // Находим первый непробельный символ
                int firstCharIndex = 0;
                while (firstCharIndex < line.Length && char.IsWhiteSpace(line[firstCharIndex]))
                {
                    firstCharIndex++;
                }
                
                if (firstCharIndex < line.Length && 
                    char.ToLower(line[firstCharIndex]) == char.ToLower(startChar))
                {
                    resultLines.Add(line);
                }
            }
            
            // Записываем результат в файл
            File.WriteAllLines(destPath, resultLines);
            
            return resultLines.Count;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при обработке файлов: {ex.Message}");
        }
    }

// Методы для задачи 4
public static void GenerateBinaryFile(string filePath, int count, int minValue, int maxValue)
{
    if (count <= 0)
    {
        throw new ArgumentException("Количество чисел должно быть положительным");
    }

    Random random = new Random();
    
    try
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            for (int i = 0; i < count; i++)
            {
                int number = random.Next(minValue, maxValue + 1);
                writer.Write(number);
            }
        }
    }
    catch (Exception ex)
    {
        throw new Exception($"Ошибка при создании бинарного файла: {ex.Message}");
    }
}

    public static int CountOppositePairs(string filePath)
    {
    if (!File.Exists(filePath))
    {
        throw new FileNotFoundException($"Файл '{filePath}' не найден.");
    }

    List<int> numbers = new List<int>();
    int oppositePairsCount = 0;

    try
    {
        // Читаем числа из бинарного файла
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();
                numbers.Add(number);
            }
        }

        // Подсчитываем пары противоположных чисел
        for (int i = 0; i < numbers.Count; i++)
        {
            for (int j = i + 1; j < numbers.Count; j++)
            {
                if (numbers[i] == -numbers[j])
                {
                    oppositePairsCount++;
                }
            }
        }

        return oppositePairsCount;
    }
    catch (Exception ex)
    {
        throw new Exception($"Ошибка при обработке бинарного файла: {ex.Message}");
    }
    }

     // Методы для задачи 5
    public static void GeneratePassengersFile(string filePath, int passengerCount, int maxBaggageItems, int maxWeight)
    {
        if (passengerCount <= 0 || maxBaggageItems <= 0 || maxWeight <= 0)
        {
            throw new ArgumentException("Все параметры должны быть положительными числами");
        }

        string[] baggageNames = { "Чемодан", "Сумка", "Рюкзак", "Коробка", "Пакет", "Кейс", "Контейнер" };
        Random random = new Random();
        
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PassengerData>));
            
            List<PassengerData> passengers = new List<PassengerData>();
            
            for (int i = 0; i < passengerCount; i++)
            {
                PassengerData passenger = new PassengerData();
                int baggageCount = random.Next(1, maxBaggageItems + 1);
                
                for (int j = 0; j < baggageCount; j++)
                {
                    string name = baggageNames[random.Next(baggageNames.Length)];
                    int weight = random.Next(1, maxWeight + 1);
                    passenger.Baggage.Add(new BaggageItem(name, weight));
                }
                
                passengers.Add(passenger);
            }
            
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, passengers);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при создании файла пассажиров: {ex.Message}");
        }
    }

    public static bool HasPassengerWithSingleLightBaggage(string filePath, int maxWeight)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл '{filePath}' не найден.");
        }

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PassengerData>));
            List<PassengerData> passengers;
            
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                passengers = (List<PassengerData>)serializer.Deserialize(fs);
            }

            foreach (PassengerData passenger in passengers)
            {
                if (passenger.Baggage.Count == 1 && passenger.Baggage[0].Weight < maxWeight)
                {
                    return true;
                }
            }
            
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при обработке файла пассажиров: {ex.Message}");
        }
    }


public static List<int> MergeOrderedLists(List<int> L1, List<int> L2)
{
    List<int> result = new List<int>(L1.Count + L2.Count);
    int i = 0, j = 0;

    while (i < L1.Count && j < L2.Count)
    {
        if (L1[i] <= L2[j])
        {
            result.Add(L1[i]);
            i++;
        }
        else
        {
            result.Add(L2[j]);
            j++;
        }
    }

    // Добавляем оставшиеся элементы
    while (i < L1.Count)
    {
        result.Add(L1[i]);
        i++;
    }

    while (j < L2.Count)
    {
        result.Add(L2[j]);
        j++;
    }

    return result;
}

public static int CountElementsWithEqualNeighbors(List<int> list)
{
    int count = 0;

    for (int i = 1; i < list.Count - 1; i++)
    {
        if (list[i - 1] == list[i + 1])
        {
            count++;
        }
    }

    return count;
}

public class DishesAnalysisResult
{
    public List<string> AllVisitors { get; set; }
    public List<string> SomeVisitors { get; set; }
    public List<string> NoVisitors { get; set; }
}

public static DishesAnalysisResult AnalyzeDishesPopularity(List<string> allDishes, List<List<string>> visitorsOrders)
{
    var result = new DishesAnalysisResult
    {
        AllVisitors = new List<string>(),
        SomeVisitors = new List<string>(),
        NoVisitors = new List<string>()
    };

    foreach (string dish in allDishes)
    {
        int orderedCount = 0;
        
        foreach (var visitorOrder in visitorsOrders)
        {
            if (visitorOrder.Contains(dish))
            {
                orderedCount++;
            }
        }

        if (orderedCount == visitorsOrders.Count)
        {
            result.AllVisitors.Add(dish);
        }
        else if (orderedCount > 0)
        {
            result.SomeVisitors.Add(dish);
        }
        else
        {
            result.NoVisitors.Add(dish);
        }
    }

    return result;
}

public static void CreateTextFile(string filePath)
{
    using (StreamWriter writer = new StreamWriter(filePath))
    {
        while (true)
        {
            string line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
                break;
            writer.WriteLine(line);
        }
    }
}

public static List<char> FindUniqueConsonants(string filePath)
{
    // Множество русских согласных букв
    HashSet<char> consonants = new HashSet<char>("бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ");
    
    // Словарь для подсчета: ключ - согласная, значение - количество слов, где она встречается
    Dictionary<char, int> consonantCounts = new Dictionary<char, int>();
    
    // Множество всех слов
    HashSet<string> allWords = new HashSet<string>();
    
    // Чтение файла и обработка слов
    using (StreamReader reader = new StreamReader(filePath))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            // Разбиваем строку на слова (игнорируем знаки препинания)
            string[] words = line.Split(new[] { ' ', ',', '.', '!', '?', ';', ':', '-', '(', ')' }, 
                                     StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string word in words)
            {
                string lowerWord = word.ToLower();
                if (allWords.Contains(lowerWord))
                    continue;
                    
                allWords.Add(lowerWord);
                
                // Множество согласных в текущем слове (для избежания повторного подсчета в одном слове)
                HashSet<char> wordConsonants = new HashSet<char>();
                
                foreach (char c in lowerWord)
                {
                    if (consonants.Contains(c))
                    {
                        wordConsonants.Add(c);
                    }
                }
                
                // Обновляем счетчики для найденных согласных
                foreach (char consonant in wordConsonants)
                {
                    if (consonantCounts.ContainsKey(consonant))
                    {
                        consonantCounts[consonant]++;
                    }
                    else
                    {
                        consonantCounts[consonant] = 1;
                    }
                }
            }
        }
    }
    
    // Отбираем согласные, которые встретились ровно в одном слове
    List<char> result = new List<char>();
    foreach (var pair in consonantCounts)
    {
        if (pair.Value == 1)
        {
            result.Add(pair.Key);
        }
    }
    
    // Сортируем результат в алфавитном порядке
    result.Sort();
    
    return result;
}

public static List<Participant> DetermineWinners(List<Participant> participants)
{
    // Сортируем участников по убыванию общего балла
    for (int i = 0; i < participants.Count - 1; i++)
    {
        for (int j = i + 1; j < participants.Count; j++)
        {
            if (participants[j].TotalScore > participants[i].TotalScore)
            {
                var temp = participants[i];
                participants[i] = participants[j];
                participants[j] = temp;
            }
        }
    }

    if (participants.Count == 0)
        return new List<Participant>();

    // Находим минимальный балл среди топ-3
    int thirdPlaceScore = participants.Count >= 3 ? 
        participants[2].TotalScore : 
        participants[participants.Count - 1].TotalScore;

    // Отбираем победителей
    List<Participant> winners = new List<Participant>();
    foreach (var p in participants)
    {
        if (p.TotalScore >= thirdPlaceScore)
        {
            winners.Add(p);
        }
    }

    return winners;
}
}