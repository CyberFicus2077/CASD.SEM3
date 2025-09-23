using System;
using System.Collections.Generic;
using System.IO;
namespace FileApp
{
    public class Student
    {
        public string LastName;
        public List<int> Grades = new List<int>();
        public Student(string lastName)
        {
            LastName = lastName;
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
    class Program
    {
        static List<Institute> institutes = new List<Institute>();
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
                Console.WriteLine("8. Сохранить все данные в файл");
                Console.WriteLine("9. Выход");
                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":AddInstitute(); break;
                    case "2":AddCourse(); break;
                    case "3":AddGroup(); break;
                    case "4":AddStudent(); break;
                    case "5":ShowAllData(); break;
                    case "6":Two(); break;
                    case "7":SaveToFile(); break;
                    case "8":SaveAllDataToFile(); break;
                    case "9":return;
                    default: Console.WriteLine("Нет такого пункта!"); break;
                }
            }
        }
        static void AddInstitute()
        {
            Console.Write("Введите название института: ");
            string name = Console.ReadLine();
            institutes.Add(new Institute(name));
            Console.WriteLine("Институт добавлен!");
        }
        static void AddCourse()
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
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <=
            institutes.Count)
            {
                Console.Write("Введите номер курса: ");
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    institutes[index - 1].Courses.Add(new Course(number));
                    Console.WriteLine("Курс добавлен!");
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
            if (int.TryParse(Console.ReadLine(), out int instituteIndex) && instituteIndex >= 1 &&
            instituteIndex <= institutes.Count)
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
                if (int.TryParse(Console.ReadLine(), out int courseIndex) && courseIndex >= 1 &&
                courseIndex <= institute.Courses.Count)
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
            if (int.TryParse(Console.ReadLine(), out int instituteIndex) && instituteIndex >= 1 &&
            instituteIndex <= institutes.Count)
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
                if (int.TryParse(Console.ReadLine(), out int courseIndex) && courseIndex >= 1 &&
courseIndex <= institute.Courses.Count)
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
                    if (int.TryParse(Console.ReadLine(), out int groupIndex) && groupIndex >= 1 &&
                    groupIndex <= course.Groups.Count)
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
                            Console.Write($" {student.LastName} - оценки: ");
                            foreach (var grade in student.Grades)
                            {
                                Console.Write($"{grade} ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        static void Two()
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
                            result.Add($"{institute.Name}, курс {course.Number}, группа{group.Name} ");
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
                            result.Add($"{institute.Name}, курс {course.Number}, группа { group.Name}");
                        }
                    }
                }
            }
        }
        static void SaveAllDataToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("alldata.txt", false,
                System.Text.Encoding.Default))
                {
                    foreach (var institute in institutes)
                    {
                        writer.WriteLine($"Институт: {institute.Name}");
                        foreach (var course in institute.Courses)
                        {
                            writer.WriteLine($" Курс: {course.Number}");
                            foreach (var group in course.Groups)
                            {
                                writer.WriteLine($" Группа: {group.Name}");
                                foreach (var student in group.Students)
                                {
                                    writer.Write($" Студент: {student.LastName} | Оценки: ");
                                    foreach (var grade in student.Grades)
                                    {
                                        writer.Write($"{grade} ");
                                    }
                                    writer.WriteLine();
                                }
                            }
                        }
                        writer.WriteLine();
                    }
                }
                Console.WriteLine("Все данные сохранены в файл alldata.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
}