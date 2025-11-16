using System;
using System.IO;
using System.Collections.Generic;

public class Application : IComparable<Application>
{
    public int Priority { get;}
    public int ApplicationNumber {get;}
    public int CreationStep {get;}

    public Application(int priority, int applicationNumber, int creationStep)
    {
        Priority = priority;
        ApplicationNumber = applicationNumber;
        CreationStep = creationStep;
    }

    public int CompareTo(Application other)
    {
        if (other == null) return 1;
        int priorityComparison = this.Priority.CompareTo(other.Priority);
        if (priorityComparison != 0) return priorityComparison;
        return other.ApplicationNumber.CompareTo(this.ApplicationNumber);
    }

    public override string ToString()
    {
        return $"Заявка №{ApplicationNumber} (Приоритет: {Priority}, Создана на шаге: {CreationStep})";
    }
}

public class ApplicationComparer : IComparer<Application>
{
    public int Compare(Application x, Application y)
    {
        if (x == null && y == null) return 0;
        if (y == null) return 1;
        if (x == null) return -1;

        int priorityComparison = x.Priority.CompareTo(y.Priority);
        if (priorityComparison != 0)
        {
            return priorityComparison;
        }
        return y.ApplicationNumber.CompareTo(x.ApplicationNumber);
    }
}

public static class Program7
{
    static void LogAction(StreamWriter logFile, string action, Application application, int currentStep)
    {
        string logMessage = $"{action} {application.ApplicationNumber} {application.Priority} {currentStep}";
        logFile.WriteLine(logMessage);
        Console.WriteLine($"  {action}: {application}");
    }

    public static void Run()
    {
        Console.Write("Введите количество шагов добавления заявок (N): ");
        if (!int.TryParse(Console.ReadLine(), out int generationSteps) || generationSteps <= 0)
        {
            Console.WriteLine("Некорректный ввод. Установлено значение 5.");
            generationSteps = 5;
        }

        MyPriorityQueue<Application> priorityQueue = new MyPriorityQueue<Application>(11, new ApplicationComparer());
        Random random = new Random();
        int totalApplicationCounter = 1;
        Application longestWaitingApplication = null;
        int maxWaitTime = -1;

        using (StreamWriter logFile = new StreamWriter("log.txt", false))
        {
            logFile.WriteLine($"Шагов генерации: {generationSteps}");

            for (int currentStep = 1; currentStep <= generationSteps; currentStep++)
            {
                Console.WriteLine($"\n- Шаг {currentStep}");

                int applicationsToAddCount = random.Next(1, 11);
                Console.WriteLine($"Добавляется {applicationsToAddCount} новых заявок:");

                for (int i = 0; i < applicationsToAddCount; i++)
                {
                    int priority = random.Next(1, 6);
                    Application newApplication = new Application(priority, totalApplicationCounter, currentStep);
                    priorityQueue.Add(newApplication);
                    LogAction(logFile, "ADD", newApplication, currentStep);
                    totalApplicationCounter++;
                }

                if (!priorityQueue.IsEmpty)
                {
                    Application removedApplication = priorityQueue.Poll();
                    LogAction(logFile, "REMOVE", removedApplication, currentStep);
                    int waitTime = currentStep - removedApplication.CreationStep;
                    if (waitTime > maxWaitTime)
                    {
                        maxWaitTime = waitTime;
                        longestWaitingApplication = removedApplication;
                    }
                }
                else Console.WriteLine("  Очередь пуста - нечего обрабатывать");
            }

            Console.WriteLine("\n--- Генерация завершена, обрабатываем оставшиеся заявки");
            int finalStep = generationSteps;

            while (!priorityQueue.IsEmpty)
            {
                finalStep++;
                Console.WriteLine($"\n-Шаг {finalStep}");
                Application removedApplication = priorityQueue.Poll();
                LogAction(logFile, "REMOVE", removedApplication, finalStep);
                int waitTime = finalStep - removedApplication.CreationStep;
                if (waitTime > maxWaitTime)
                {
                    maxWaitTime = waitTime;
                    longestWaitingApplication = removedApplication;
                }
            }

            logFile.WriteLine($"Симуляция завершена. Обработано заявок: {totalApplicationCounter - 1}");
        }

        Console.WriteLine("Очередь пуста. Все заявки обработаны");

        if (longestWaitingApplication != null)
        {
            Console.WriteLine($"\nМаксимальное время ожидания в системе: {maxWaitTime} шагов.");
            Console.WriteLine("Информация о заявке, которая ждала дольше всех:");
            Console.WriteLine($"   {longestWaitingApplication}");
        }
        else Console.WriteLine("\nНе было обработано ни одной заявки.");

        Console.WriteLine("\nЗапись в файл: log.txt");
        Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}