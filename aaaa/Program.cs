using System;
using System.IO;
using System.Linq;

namespace UniversitySystem
{ //интерфейски
    public interface IPrintable
    {
        void Print();
    }

    public interface IGrad
    {
        double CalculateAverage();
    }

    public class StudentEventArgs : EventArgs
    {
        public string Message {get;}
        public StudentEventArgs(string message) => Message = message;
    }
    //ивентик
    public class Student : IPrintable, IGrad
    {
        public string LastName {get; set;}
        public List<int> Grades {get; set;} = new List<int>();

        public event EventHandler<StudentEventArgs> GradeAdded;
        public event EventHandler<StudentEventArgs> ErrorOccurred;

        public Student(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Фамилия не может быть пустой");
            LastName = lastName;
        }

        public double CalculateAverage()
        {
            try
            {
                if (Grades.Count == 0)
                    throw new DivideByZeroException("Нет оценок для расчета среднего");

                return Grades.Average();
            }
            catch (DivideByZeroException ex)
            {
                OnError($"Ошибка расчета: {ex.Message}");
                return 0;
            }
        }

        public void Print()
        {
            Console.Write($"Студент: {LastName} | Оценки: ");
            foreach (var grade in Grades) Console.Write($"{grade} ");
            Console.WriteLine($"(средний: {CalculateAverage():F2})");
        }

        public void AddGrade(int grade)
        {
            try
            {
                if (grade < 2 || grade > 5)
                    throw new ArgumentOutOfRangeException("Оценка должна быть от 2 до 5");

                Grades.Add(grade);
                OnGradeAdded($"Добавлена оценка {grade}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                OnError(ex.Message);
            }
        }

        public bool HasExcellentGrades() => Grades.Count > 0 && Grades.All(grade => grade == 5);
        public bool HasFailingGrades() => Grades.Contains(2);
        //пример исп событий
        protected virtual void OnGradeAdded(string message) =>
            GradeAdded?.Invoke(this, new StudentEventArgs(message));

        protected virtual void OnError(string message) =>
            ErrorOccurred?.Invoke(this, new StudentEventArgs(message));
    }

    public class Group
    {
        public string Name {get; set;}
        public List<Student> Students {get; set;} = new List<Student>();

        public Group(string name) => Name = name;

        public void AddStudent(Student student)
        {
            try
            {
                if (Students.Count >= 31)
                    throw new OutOfMemoryException("Слишком много студентов в группе");

                Students.Add(student);

                student.ErrorOccurred += (s, e) =>
                    Console.WriteLine($"Ошибка у студента {student.LastName}: {e.Message}");
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public double GetAverage() => Students.Count > 0 ?
            Students.SelectMany(s => s.Grades).Average() : 0;

        public bool HasFailingStudents() => Students.Any(s => s.HasFailingGrades());
    }

    public class Course
    {
        public int Number {get; set;}
        public List<Group> Groups {get; set;} = new List<Group>();
        public Course(int number) => Number = number;

        public void AddGroup(Group group)
        {
            try
            {
                if (Groups.Count >= 50)
                    throw new IndexOutOfRangeException("Слишком много групп на курсе");

                Groups.Add(group);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    public class Institute
    {
        public string Name {get; set;}
        public List<Course> Courses {get; set;} = new List<Course>();

        public Institute(string name) => Name = name;

        public void AddCourse(Course course) => Courses.Add(course);
    }
    //обработочка исключений
    public class ExceptionTester
    {
        public void TestAllExceptions()
        {
            Console.WriteLine("Проверка обработки исключений:");

            TestDivideByZero();
            TestIndexOutOfRange();
            TestInvalidCast();
            TestOverflow();
            TestArrayTypeMismatch();
            TestOutOfMemory();
            TestStackOverflow();

            Console.WriteLine("Проверка завершена");
        }

        private void TestDivideByZero()
        {
            try
            {
                int result = 10 / int.Parse("0");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Обработано: деление на ноль");
            }
        }

        private void TestIndexOutOfRange()
        {
            try
            {
                int[] arr = new int[1];
                int value = arr[10];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Обработано: выход за границы массива");
            }
        }

        private void TestInvalidCast()
        {
            try
            {
                object text = "текст";
                int number = (int)text;
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Обработано: неверное приведение типа");
            }
        }

        private void TestOverflow()
        {
            try
            {
                checked
                {
                    int max = int.MaxValue;
                    max++;
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("Обработано: переполнение");
            }
        }

        private void TestArrayTypeMismatch()
        {
            try
            {
                object[] objects = new string[5];
                objects[0] = 123;
            }
            catch (ArrayTypeMismatchException)
            {
                Console.WriteLine("Обработано: несоответствие типа массива");
            }
        }

        private void TestOutOfMemory()
        {
            try
            {
                throw new OutOfMemoryException("Тестовая ошибка памяти");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Обработано: нехватка памяти");
            }
        }

        private void TestStackOverflow()
        {
            try
            {
                throw new StackOverflowException("Тестовая ошибка стека");
            }
            catch (StackOverflowException)
            {
                Console.WriteLine("Обработано: переполнение стека");
            }
        }
    }

    class Program
    {
        private static List<Institute> institutes = new List<Institute>();
        private static ExceptionTester tester = new ExceptionTester();

        static void Main()
        {
            InitializeData();

            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddInstitute(); break;
                    case "2": AddCourse(); break;
                    case "3": AddGroup(); break;
                    case "4": AddStudent(); break;
                    case "5": ShowAllData(); break;
                    case "6": FindGoodGroups(); break;
                    case "7": SaveReport(); break;
                    case "8": TestExceptions(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор"); break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("Управление университетом");
            Console.WriteLine("1. Добавить институт");
            Console.WriteLine("2. Добавить курс");
            Console.WriteLine("3. Добавить группу");
            Console.WriteLine("4. Добавить студента");
            Console.WriteLine("5. Показать все данные");
            Console.WriteLine("6. Найти группы без двоечников");
            Console.WriteLine("7. Сохранить отчет");
            Console.WriteLine("8. Проверить исключения");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите: ");
        }

        static void InitializeData()
        {
            var institute = new Institute("КубГу");
            var course = new Course(1);
            var group = new Group("ФИ22");

            var student1 = new Student("Яковенко");
            student1.AddGrade(5);
            student1.AddGrade(4);

            var student2 = new Student("Бедменов");
            student2.AddGrade(3);
            student2.AddGrade(4);

            student1.GradeAdded += (s, e) =>
                Console.WriteLine($"Событие: {e.Message}");

            group.AddStudent(student1);
            group.AddStudent(student2);
            course.AddGroup(group);
            institute.AddCourse(course);
            institutes.Add(institute);
        }

        static void AddInstitute()
        {
            Console.Write("Название института: ");
            string name = Console.ReadLine();

            try
            {
                institutes.Add(new Institute(name));
                Console.WriteLine("Институт добавлен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void AddCourse()
        {
            var institute = SelectInstitute();
            if (institute == null) return;

            Console.Write("Номер курса: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                institute.AddCourse(new Course(number));
                Console.WriteLine("Курс добавлен");
            }
        }

        static void AddGroup()
        {
            var course = SelectCourse(SelectInstitute());
            if (course == null) return;

            Console.Write("Название группы: ");
            course.AddGroup(new Group(Console.ReadLine()));
            Console.WriteLine("Группа добавлена");
        }

        static void AddStudent()
        {
            var group = SelectG(SelectCourse(SelectInstitute()));
            if (group == null) return;

            Console.Write("Фамилия студента: ");
            var student = new Student(Console.ReadLine());

            Console.Write("Оценки через пробел: ");
            var grades = Console.ReadLine().Split(' ');

            foreach (var grade in grades)
            {
                if (int.TryParse(grade, out int g))
                    student.AddGrade(g);
            }

            group.AddStudent(student);
            Console.WriteLine("Студент добавлен");
        }

        static void ShowAllData()
        {
            if (!institutes.Any())
            {
                Console.WriteLine("Данных нет");
                return;
            }

            foreach (var institute in institutes)
            {
                Console.WriteLine($"\nИнститут: {institute.Name}");
                foreach (var course in institute.Courses)
                {
                    Console.WriteLine($"  Курс {course.Number}:");
                    foreach (var group in course.Groups)
                    {
                        Console.WriteLine($"    Группа {group.Name}:");
                        foreach (var student in group.Students)
                        {
                            student.Print();
                        }
                    }
                }
            }
        }

        static void FindGoodGroups()
        {
            var goodGroups = institutes
                .SelectMany(i => i.Courses
                    .SelectMany(c => c.Groups
                        .Where(g => !g.HasFailingStudents() && g.Students.Any())
                        .Select(g => $"{i.Name}, курс {c.Number}, {g.Name}")));

            Console.WriteLine("\nГруппы без двоечников:");
            foreach (var group in goodGroups)
            {
                Console.WriteLine(group);
            }

            if (!goodGroups.Any())
            {
                Console.WriteLine("Не найдено");
            }
        }

        static void SaveReport()
        {
            try
            {
                using (var writer = new StreamWriter("report.txt"))
                {
                    writer.WriteLine("Отчет по группам без двоечников");
                    writer.WriteLine($"Дата: {DateTime.Now}");

                    var goodGroups = institutes
                        .SelectMany(i => i.Courses
                            .SelectMany(c => c.Groups
                                .Where(g => !g.HasFailingStudents() && g.Students.Any())));

                    foreach (var group in goodGroups)
                    {
                        writer.WriteLine($"{group.Name} - средний: {group.GetAverage():F2}");
                    }
                }
                Console.WriteLine("Отчет сохранен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

        static void TestExceptions() => tester.TestAllExceptions();

        static Institute SelectInstitute()
        {
            if (!institutes.Any()) return null;

            for (int i = 0; i < institutes.Count; i++)
                Console.WriteLine($"{i + 1}. {institutes[i].Name}");

            Console.Write("Выберите институт: ");
            return int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= institutes.Count
                ? institutes[index - 1]
                : null;
        }

        static Course SelectCourse(Institute institute)
        {
            if (institute?.Courses.Count == 0) return null;

            for (int i = 0; i < institute.Courses.Count; i++)
                Console.WriteLine($"{i + 1}. Курс {institute.Courses[i].Number}");

            Console.Write("Выберите курс: ");
            return int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= institute.Courses.Count
                ? institute.Courses[index - 1]
                : null;
        }

        static Group SelectG(Course course)
        {
            if (course?.Groups.Count == 0) return null;

            for (int i = 0; i < course.Groups.Count; i++)
                Console.WriteLine($"{i + 1}. {course.Groups[i].Name}");

            Console.Write("Выберите группу: ");
            return int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= course.Groups.Count
                ? course.Groups[index - 1]
                : null;
        }
    }
}