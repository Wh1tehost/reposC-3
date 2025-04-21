using System;

class Program
{   static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Разность между максимальным и минимальным");
            Console.WriteLine("2 - Поиск минимального элемента");
            Console.WriteLine("3 - Фильтрация строк по первому символу");
            Console.WriteLine("4 - Поиск противоположных чисел в бинарном файле");
            Console.WriteLine("5 - Поиск пассажира с легким багажом");
            Console.WriteLine("6 - Слияние упорядоченных списков");
            Console.WriteLine("7 - Элементы с равными соседями");
            Console.WriteLine("8 - Анализ популярности блюд");
            Console.WriteLine("9 - Поиск уникальных согласных букв");
            Console.WriteLine("10 - Результаты многоборья");
            Console.WriteLine("0 - Выход");
            
            int choice = InputValidator.GetIntInput("Введите номер действия: ", 0, 10);
            
            switch (choice)
            {
                case 1: SolveTask1(); break;
                case 2: SolveTask2(); break;
                case 3: SolveTask3(); break;
                case 4: SolveTask4(); break;
                case 5: SolveTask5(); break;
                case 6: SolveTask6(); break;
                case 7: SolveTask7(); break;
                case 8: SolveTask8(); break;
                case 9: SolveTask9(); break;
                case 10: SolveTask10(); break;
                case 0: 
                    Console.WriteLine("До свидания!");
                    return;
            }
        }
    }
    static void SolveTask1()
    {
        Console.WriteLine("\n--- Задача 1 ---");
        Console.WriteLine("Необходимо найти разность максимального и минимального элементов.");
        
        string filePath = "numbers1.txt";
        
        // Заполняем файл случайными числами
        int count = InputValidator.GetIntInput("Введите количество чисел для генерации: ", minValue: 1);
        int min = InputValidator.GetIntInput("Введите минимальное значение: ");
        int max = InputValidator.GetIntInput("Введите максимальное значение: ");
        
        TaskSolver.GenerateNumbersFile(filePath, count, min, max);
        Console.WriteLine($"Файл '{filePath}' успешно создан с {count} случайными числами.");
        
        // Решаем задачу
        int difference = TaskSolver.CalculateMaxMinDifference(filePath);
        Console.WriteLine($"Разность между максимальным и минимальным элементами: {difference}");
    }
    
    static void SolveTask2()
    {
        Console.WriteLine("\n--- Задача 2 ---");
        Console.WriteLine("Необходимо найти минимальный элемент.");
        
        string filePath = "numbers2.txt";
        
        // Заполняем файл случайными числами
        int count = InputValidator.GetIntInput("Введите количество чисел для генерации: ", minValue: 1);
        int minValue = InputValidator.GetIntInput("Введите минимальное значение: ");
        int maxValue = InputValidator.GetIntInput("Введите максимальное значение: ");
        int numbersPerLine = InputValidator.GetIntInput("Введите количество чисел в строке: ", minValue: 1);
        
        TaskSolver.GenerateMultiNumbersFile(filePath, count, minValue, maxValue, numbersPerLine);
        Console.WriteLine($"Файл '{filePath}' успешно создан с {count} случайными числами.");
        
        // Решаем задачу
        int minElement = TaskSolver.FindMinElement(filePath);
        Console.WriteLine($"Минимальный элемент в файле: {minElement}");
    }
    
   static void SolveTask3()
{
    Console.WriteLine("\n--- Задача 3 ---");
    Console.WriteLine("Необходимо переписать в другой файл строки, начинающиеся с заданного символа.");
    
    string sourceFilePath = "text_source.txt";
    string resultFilePath = "text_result.txt";
    
    // Заполняем файл случайным текстом
    int lineCount = InputValidator.GetIntInput("Введите количество строк для генерации: ", minValue: 1);
    TaskSolver.GenerateTextFile(sourceFilePath, lineCount);
    Console.WriteLine($"Файл '{sourceFilePath}' успешно создан с {lineCount} строками.");
    
    // Получаем символ для фильтрации
    char startChar = InputValidator.GetCharInput("Введите символ для фильтрации строк: ");
    
    // Решаем задачу
    int copiedLines = TaskSolver.CopyLinesStartingWith(sourceFilePath, resultFilePath, startChar);
    Console.WriteLine($"Скопировано {copiedLines} строк в файл '{resultFilePath}'.");
}

static void SolveTask4()
{
    Console.WriteLine("\n--- Задача 4 ---");
    Console.WriteLine("Подсчет количеств пар противоположных чисел среди компонент файла.");

    string filePath = "numbers_binary.dat";
    
    // Заполняем файл случайными числами
    int count = InputValidator.GetIntInput("Введите количество чисел для генерации: ", minValue: 1);
    int minValue = InputValidator.GetIntInput("Введите минимальное значение: ");
    int maxValue = InputValidator.GetIntInput("Введите максимальное значение: ");
    
    TaskSolver.GenerateBinaryFile(filePath, count, minValue, maxValue);
    Console.WriteLine($"Бинарный файл '{filePath}' успешно создан с {count} числами.");
    
    // Решаем задачу
    int oppositePairsCount = TaskSolver.CountOppositePairs(filePath);
    Console.WriteLine($"Найдено пар противоположных чисел: {oppositePairsCount}");
}

static void SolveTask5()
{
    Console.WriteLine("\n--- Задача 5 ---");
    Console.WriteLine("Поиск пассажира с одной единицей багажа менее заданного веса.");
    
    string filePath = "passengers_baggage.dat";
    
    // Заполняем файл случайными данными
    int passengerCount = InputValidator.GetIntInput("Введите количество пассажиров: ", minValue: 1);
    int maxBaggageItems = InputValidator.GetIntInput("Введите максимальное количество единиц багажа на пассажира: ", minValue: 1);
    int maxWeight = InputValidator.GetIntInput("Введите максимальный вес единицы багажа (кг): ", minValue: 1);
    
    TaskSolver.GeneratePassengersFile(filePath, passengerCount, maxBaggageItems, maxWeight);
    Console.WriteLine($"Файл '{filePath}' успешно создан с данными {passengerCount} пассажиров.");
    
    // Получаем критерий поиска
    int maxSingleBaggageWeight = InputValidator.GetIntInput("Введите максимальный вес для поиска (кг): ", minValue: 1);
    
    // Решаем задачу
    bool found = TaskSolver.HasPassengerWithSingleLightBaggage(filePath, maxSingleBaggageWeight);
    Console.WriteLine(found 
        ? "Найден пассажир с одной единицей багажа менее указанного веса."
        : "Пассажиров с одной единицей багажа менее указанного веса не найдено.");
}

static void SolveTask6()
{
    Console.WriteLine("\n--- Задача 6 ---");
    Console.WriteLine("Вставка элементов упорядоченного списка L2 в упорядоченный список L1");
    // Получаем списки от пользователя
    List<int> L1 = InputValidator.GetOrderedList("Введите элементы списка L1 (через пробел): ");
    List<int> L2 = InputValidator.GetOrderedList("Введите элементы списка L2 (через пробел): ");

    // Выполняем слияние
    List<int> mergedList = TaskSolver.MergeOrderedLists(L1, L2);

    // Выводим результат
    Console.WriteLine("\nРезультат слияния:");
    Console.WriteLine(string.Join(" ", mergedList));
}

static void SolveTask7()
{
    Console.WriteLine("\n--- Задача 7 ---");
    Console.WriteLine("Подсчет элементов списка, у которых равные соседи.");

    // Получаем список от пользователя
    List<int> L = InputValidator.GetList("Введите элементы списка (через пробел): ");

    // Подсчитываем элементы с равными соседями
    int count = TaskSolver.CountElementsWithEqualNeighbors(L);

    // Выводим результат
    Console.WriteLine($"\nКоличество элементов с равными соседями: {count}");
}

static void SolveTask8()
{
    Console.WriteLine("\n--- Задача 8 ---");
    // Получаем список всех блюд
    List<string> allDishes = InputValidator.GetStringList("Введите все блюда кафе (через запятую): ");
    
    // Получаем данные о посетителях
    List<List<string>> visitorsOrders = new List<List<string>>();
    int visitorCount = InputValidator.GetIntInput("Введите количество посетителей: ", minValue: 1);
    
    for (int i = 0; i < visitorCount; i++)
    {
        visitorsOrders.Add(InputValidator.GetStringList($"Введите блюда посетителя {i+1} (через запятую): "));
    }

    // Анализируем данные
    var result = TaskSolver.AnalyzeDishesPopularity(allDishes, visitorsOrders);

    // Выводим результаты
    Console.WriteLine("\nРезультаты анализа:");
    Console.WriteLine("Блюда, которые заказывали все посетители: " + string.Join(", ", result.AllVisitors));
    Console.WriteLine("Блюда, которые заказывали некоторые посетители: " + string.Join(", ", result.SomeVisitors));
    Console.WriteLine("Блюда, которые не заказывал никто: " + string.Join(", ", result.NoVisitors));
}

static void SolveTask9()
{
    Console.WriteLine("\n--- Задача 9 ---");
    Console.WriteLine("Поиск согласных букв, встречающихся ровно в одном слове текста.");
    
    string filePath = "text_input.txt";
    
    // Заполняем файл текстом
    Console.WriteLine($"Введите текст (для завершения введите пустую строку):");
    TaskSolver.CreateTextFile(filePath);
    
    // Анализируем текст
    List<char> result = TaskSolver.FindUniqueConsonants(filePath);
    
    // Выводим результат
    if (result.Count > 0)
    {
        Console.WriteLine("\nСогласные буквы, встречающиеся ровно в одном слове:");
        Console.WriteLine(string.Join(" ", result));
    }
    else
    {
        Console.WriteLine("\nНе найдено согласных букв, встречающихся ровно в одном слове.");
    }
}

static void SolveTask10()
{
    Console.WriteLine("\n--- Задача 10 ---");
    // Получаем данные об участниках
    int n = InputValidator.GetIntInput("Введите количество участников: ", 1, 100);
    List<Participant> participants = new List<Participant>();

    for (int i = 0; i < n; i++)
    {
        participants.Add(InputValidator.GetParticipant($"Введите данные участника {i+1}: "));
    }

    // Определяем победителей
    var winners = TaskSolver.DetermineWinners(participants);

    // Выводим результаты
    Console.WriteLine("\nРезультаты соревнований:");
    for (int i = 0; i < winners.Count; i++)
    {
        if (i < 3)
        {
            Console.WriteLine($"Место {i+1}: {winners[i].LastName} {winners[i].FirstName} - {winners[i].TotalScore} баллов");
        }
        else
        {
            Console.WriteLine($"Также {winners[i].LastName} {winners[i].FirstName} - {winners[i].TotalScore} баллов");
        }
    }
}
}
