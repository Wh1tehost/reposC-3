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
        
        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length == 0)
        {
            throw new InvalidOperationException("Файл пуст.");
        }
        
        int max = int.MinValue;
        int min = int.MaxValue;
        
        foreach (string line in lines)
        {
            int number = int.Parse(line);
            if (number > max) max = number;
            if (number < min) min = number;
        }
        
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
        
        string[] lines = File.ReadAllLines(filePath);
        int min = int.MaxValue;
        
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            string[] parts = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                int number = int.Parse(part);
                if (number < min) min = number;
            }
        }
        
        if (min == int.MaxValue)
        {
            throw new InvalidOperationException("Файл пуст.");
        }
        
        return min;
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
        string[] allLines = File.ReadAllLines(sourcePath);
        
        foreach (string line in allLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            
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
        
        File.WriteAllLines(destPath, resultLines);
        return resultLines.Count;
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
            throw new FileNotFoundException($"Файл '{filePath}' не найден.");

        List<int> numbers = new List<int>();
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                numbers.Add(reader.ReadInt32());
            }
        }

        int count = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            for (int j = i + 1; j < numbers.Count; j++)
            {
                if (numbers[i] == -numbers[j])
                    count++;
            }
        }
        return count;
    }

     // Методы для задачи 5

[Serializable]
public struct BaggageItem
{
    public string Name;
    public int Weight;
    
    public BaggageItem(string name, int weight)
    {
        Name = name;
        Weight = weight;
    }
}

[Serializable]
public class PassengerData
{
    public List<BaggageItem> Baggage;
    
    public PassengerData()
    {
        Baggage = new List<BaggageItem>();
    }
}
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
            throw new FileNotFoundException($"Файл '{filePath}' не найден.");

        List<PassengerData> passengers;
        XmlSerializer serializer = new XmlSerializer(typeof(List<PassengerData>));
        
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            passengers = (List<PassengerData>)serializer.Deserialize(fs);
        }

        foreach (PassengerData p in passengers)
        {
            if (p.Baggage.Count == 1 && p.Baggage[0].Weight < maxWeight)
                return true;
        }
        return false;
    }


    // Методы для задачи 6
    public static List<int> MergeOrderedLists(List<int> L1, List<int> L2)
    {
        List<int> result = new List<int>();
        int i = 0, j = 0;

        while (i < L1.Count && j < L2.Count)
        {
            if (L1[i] <= L2[j])
                result.Add(L1[i++]);
            else
                result.Add(L2[j++]);
        }

        while (i < L1.Count) result.Add(L1[i++]);
        while (j < L2.Count) result.Add(L2[j++]);

        return result;
    }

    // Методы для задачи 7
    public static int CountElementsWithEqualNeighbors(List<int> list)
    {
        int count = 0;
        for (int i = 1; i < list.Count - 1; i++)
        {
            if (list[i-1] == list[i+1])
                count++;
        }
        return count;
    }

// Методы для задачи 8
public class DishesAnalysisResult
{
    public List<string> AllVisitors { get; set; }
    public List<string> SomeVisitors { get; set; }
    public List<string> NoVisitors { get; set; }
}

public static DishesAnalysisResult AnalyzeDishesPopularity(List<string> allDishes, List<List<string>> orders)
{
    var result = new DishesAnalysisResult()
    {
        AllVisitors = new List<string>(),
        SomeVisitors = new List<string>(),
        NoVisitors = new List<string>()
    };

    foreach (string dish in allDishes)
    {
        int orderedCount = 0;
        foreach (var visitorOrder in orders)
        {
            bool found = false;
            foreach (string orderedDish in visitorOrder)
            {
                if (orderedDish == dish)
                {
                    found = true;
                    break;
                }
            }
            if (found) orderedCount++;
        }

        if (orderedCount == orders.Count)
            result.AllVisitors.Add(dish);
        else if (orderedCount > 0)
            result.SomeVisitors.Add(dish);
        else
            result.NoVisitors.Add(dish);
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

// Метод для задачи 9
public static List<char> FindUniqueConsonants(string filePath)
{
    string consonantsStr = "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ";
    HashSet<char> consonants = new HashSet<char>();
    foreach (char c in consonantsStr) consonants.Add(c);

    Dictionary<char, int> consonantCounts = new Dictionary<char, int>();
    HashSet<string> allWords = new HashSet<string>();

    string[] lines = File.ReadAllLines(filePath);
    foreach (string line in lines)
    {
        string[] words = line.Split(new[] { ' ', ',', '.', '!', '?' }, 
                                 StringSplitOptions.RemoveEmptyEntries);
        
        foreach (string word in words)
        {
            string lowerWord = word.ToLower();
            if (allWords.Contains(lowerWord)) continue;
            allWords.Add(lowerWord);

            HashSet<char> wordConsonants = new HashSet<char>();
            foreach (char c in lowerWord)
            {
                if (consonants.Contains(c))
                    wordConsonants.Add(c);
            }

            foreach (char consonant in wordConsonants)
            {
                if (consonantCounts.ContainsKey(consonant))
                    consonantCounts[consonant]++;
                else
                    consonantCounts[consonant] = 1;
            }
        }
    }

    List<char> result = new List<char>();
    foreach (var pair in consonantCounts)
    {
        if (pair.Value == 1)
            result.Add(pair.Key);
    }

    result.Sort();
    return result;
}

// Метод для задачи 10


public static List<Participant> DetermineWinners(List<Participant> participants)
{
    // Сортировка пузырьком
    for (int i = 0; i < participants.Count-1; i++)
    {
        for (int j = i+1; j < participants.Count; j++)
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

    int thirdScore = participants.Count >= 3 ? 
        participants[2].TotalScore : 
        participants[participants.Count-1].TotalScore;

    List<Participant> winners = new List<Participant>();
    foreach (Participant p in participants)
    {
        if (p.TotalScore >= thirdScore)
            winners.Add(p);
    }

    return winners;
}
}
