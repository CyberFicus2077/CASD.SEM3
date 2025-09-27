using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileApp;

public delegate void StudentAction(Student student);

interface IDisplay

{
    void DisplayInfo();

}

interface IGrade

{

    double CalculateAverage();

}

namespace FileApp

{

    public class Student : IGrade, IDisplay

    {

        public string LastName;

        public List<int> Grades = new List<int>();

        public Student(string lastName) { LastName = lastName; }

        public double CalculateAverage()

        {

            return Grades.Count > 0 ? Grades.Average() : 0;

        }

        public bool HasExcellentGrades()

        {

            return Grades.Count > 0 && Grades.All(grade => grade == 5);

        }

        public void DisplayInfo()

        {

            Console.Write($"Студент: {LastName} | Оценки: ");

            foreach (var grade in Grades) Console.Write($"{grade} ");

            Console.WriteLine($"(средний: {CalculateAverage():F2})");

        }

    }

    public class Group

    {

        public string Name;

        public List<Student> Students = new List<Student>();

        public Group(string name)

        {

            Name = name;

        }

    }

    public class Course

    {

        public int Number;

        public List<Group> Groups = new List<Group>();

        public Course(int number)

        {

            Number = number;

        }

    }

    public class Institute

    {

        public string Name;

        public List<Course> Courses = new List<Course>();

        public Institute(string name)

        {

            Name = name;

        }

    }

    class P

    {

        static List<Institute> institutes = new List<Institute>();

        static void TestWithDelegate()

        {

            StudentAction actions = null;

            actions += DisplayStudInf;

            actions += CheckExcellentStudent;

            actions += CheckFailingStudent;

            Console.WriteLine("=== Проверка всех студентов через делегат ===");

            foreach (var institute in institutes)

            {

                foreach (var course in institute.Courses)

                {

                    foreach (var group in course.Groups)

                    {

                        Console.WriteLine($"Группа: {group.Name}");

                        foreach (var student in group.Students)

                        {

                            actions(student);

                        }

                        Console.WriteLine();

                    }

                }

            }

        }

        static void DisplayStudInf(Student student)

        {

            student.DisplayInfo();

        }

        static void CheckExcellentStudent(Student student)

        {

            if (student.HasExcellentGrades())

                Console.WriteLine($" >>> {student.LastName} - нет двоек :)");

        }

        static void CheckFailingStudent(Student student)

        {

            if (student.Grades.Contains(2))

                Console.WriteLine($" >>> {student.LastName} - имеет двойки :(");

        }

        static void Main()

        {

            while (true)

            {

                Console.WriteLine("1. Добавить институт");

                Console.WriteLine("2. Добавить курс");

                Console.WriteLine("3. Добавить группу");

                Console.WriteLine("4. Добавить студента");

                Console.WriteLine("5. Показать все данные");

                Console.WriteLine("6. Найти группы без двоечников");

                Console.WriteLine("7. Сохранить результат в файл");

                Console.WriteLine("8. Выход");

                Console.WriteLine("9. Делегат и интерфе"); 

                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)

                {

                    case "1": AddInstitute(); break;

                    case "2": AddCourse(); break;

                    case "3": AddGroup(); break;

                    case "4": AddStudent(); break;

                    case "5": ShowAllData(); break;

                    case "6": FindTwo(); break;

                    case "7": SaveToFile(); break;

                    case "10": return;

                    case "9": TestWithDelegate(); break; 

                    default: Console.WriteLine("Нет такого пункта!"); break;

                }

                Console.WriteLine();

            }

        }

        static void AddInstitute()

        {

            Console.Write("Введите название института: ");

            string name = Console.ReadLine();

            institutes.Add(new Institute(name));

            Console.WriteLine("Институт добавлен");

        }

        static void AddCourse()

        {

            if (institutes.Count == 0)

            {

                Console.WriteLine("Сначала добавьте институт");

                return;

            }

            Console.WriteLine("Выберите институт:");

            for (int i = 0; i < institutes.Count; i++)
            {

                Console.WriteLine($"{i + 1}. {institutes[i].Name}");

            }

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= institutes.Count)

            {

                Console.Write("Введите номер курса: ");

                if (int.TryParse(Console.ReadLine(), out int number))

                {

                    institutes[index - 1].Courses.Add(new Course(number));

                    Console.WriteLine("Курс добавлен");

                }

            }

        }

        static void AddGroup()

        {

            if (institutes.Count == 0)

            {

                Console.WriteLine("Сначала добавьте институт!");

                return;

            }

            Console.WriteLine("Выберите институт:");

            for (int i = 0; i < institutes.Count; i++)

            {

                Console.WriteLine($"{i + 1}. {institutes[i].Name}");

            }

            if (int.TryParse(Console.ReadLine(), out int instituteIndex) && instituteIndex >= 1 && instituteIndex <= institutes.Count)

            {

                Institute institute = institutes[instituteIndex - 1];

                if (institute.Courses.Count == 0)

                {

                    Console.WriteLine("Сначала добавьте курс!");

                    return;

                }

                Console.WriteLine("Выберите курс:");

                for (int i = 0; i < institute.Courses.Count; i++)

                {

                    Console.WriteLine($"{i + 1}. {institute.Courses[i].Number} курс");

                }

                if (int.TryParse(Console.ReadLine(), out int courseIndex) && courseIndex >= 1 && courseIndex <= institute.Courses.Count)

                {

                    Console.Write("Введите название группы: ");

                    string name = Console.ReadLine();

                    institute.Courses[courseIndex - 1].Groups.Add(new Group(name));

                    Console.WriteLine("Группа добавлена!");

                }

            }

        }

        static void AddStudent()

        {

            if (institutes.Count == 0)

            {

                Console.WriteLine("Сначала добавьте институт!");

                return;

            }

            Console.WriteLine("Выберите институт:");

            for (int i = 0; i < institutes.Count; i++)

            {

                Console.WriteLine($"{i + 1}. {institutes[i].Name}");

            }

            if (int.TryParse(Console.ReadLine(), out int instituteIndex) && instituteIndex >= 1 && instituteIndex <= institutes.Count)

            {

                Institute institute = institutes[instituteIndex - 1];

                if (institute.Courses.Count == 0)

                {

                    Console.WriteLine("Сначала добавьте курс!");

                    return;

                }

                Console.WriteLine("Выберите курс:");

                for (int i = 0; i < institute.Courses.Count; i++)

                {

                    Console.WriteLine($"{i + 1}. {institute.Courses[i].Number} курс");

                }

                if (int.TryParse(Console.ReadLine(), out int courseIndex) && courseIndex >= 1 && courseIndex <= institute.Courses.Count)

                {

                    Course course = institute.Courses[courseIndex - 1];

                    if (course.Groups.Count == 0)

                    {

                        Console.WriteLine("Сначала добавьте группу!");

                        return;

                    }

                    Console.WriteLine("Выберите группу:");

                    for (int i = 0; i < course.Groups.Count; i++)

                    {

                        Console.WriteLine($"{i + 1}. {course.Groups[i].Name}");

                    }

                    if (int.TryParse(Console.ReadLine(), out int groupIndex) && groupIndex >= 1 && groupIndex <= course.Groups.Count)

                    {

                        Console.Write("Введите фамилию студента: ");

                        string lastName = Console.ReadLine();

                        Console.Write("Введите оценки через пробел: ");

                        string[] gradesInput = Console.ReadLine().Split(' ');

                        Student student = new Student(lastName);

                        foreach (string grade in gradesInput)

                        {

                            if (int.TryParse(grade, out int g))

                            {

                                student.Grades.Add(g);

                            }

                        }

                        course.Groups[groupIndex - 1].Students.Add(student);

                        Console.WriteLine("Студент добавлен!");

                    }

                }

            }

        }

        static void ShowAllData()

        {

            if (institutes.Count == 0)

            {

                Console.WriteLine("Данных нет!");

                return;

            }

            foreach (var institute in institutes)

            {

                Console.WriteLine($"Институт: {institute.Name}");

                foreach (var course in institute.Courses)

                {

                    Console.WriteLine($" Курс {course.Number}:");

                    foreach (var group in course.Groups)

                    {

                        Console.WriteLine($" Группа {group.Name}:");

                        foreach (var student in group.Students)

                        {

                            student.DisplayInfo(); 

                        }

                    }

                }

            }

        }

        static void FindTwo()

        {

            List<string> result = new List<string>();

            foreach (var institute in institutes)

            {

                foreach (var course in institute.Courses)

                {

                    foreach (var group in course.Groups)

                    {

                        bool hasFailing = false;

                        foreach (var student in group.Students)

                        {

                            if (student.Grades.Contains(2))

                            {

                                hasFailing = true;

                                break;

                            }

                        }

                        if (!hasFailing && group.Students.Count > 0)

                        {

                            result.Add($"{institute.Name}, курс {course.Number}, группа {group.Name}");

                        }

                    }

                }

            }

            Console.WriteLine("Группы без двоечников:");

            if (result.Count == 0)

            {

                Console.WriteLine("Таких групп нет");

            }

            else

            {

                foreach (string group in result)

                {

                    Console.WriteLine(group);

                }

            }

        }

        static void SaveToFile()

        {

            List<string> result = new List<string>();

            foreach (var institute in institutes)

            {

                foreach (var course in institute.Courses)

                {

                    foreach (var group in course.Groups)

                    {

                        bool hasFailing = false;

                        foreach (var student in group.Students)

                        {

                            if (student.Grades.Contains(2))

                            {

                                hasFailing = true;

                                break;

                            }

                        }

                        if (!hasFailing && group.Students.Count > 0)

                        {

                            result.Add($"{institute.Name}, курс {course.Number}, группа {group.Name}");

                        }

                    }

                }

            }

            try

            {

                using (StreamWriter writer = new StreamWriter("result.txt", false, System.Text.Encoding.Default))

                {

                    writer.WriteLine("Группы без двоечников:");

                    if (result.Count == 0)

                    {

                        writer.WriteLine("Таких групп нет");

                    }

                    else

                    {

                        foreach (string group in result)

                        {

                            writer.WriteLine(group);

                        }

                    }

                }

                Console.WriteLine("Результат сохранен в файл result.txt");

            }

            catch (Exception ex)

            {

                Console.WriteLine($"Ошибка: {ex.Message}");

            }

        }

    }

}