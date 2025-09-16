using System;

namespace Lab1_Var6

{

    public class PrintEdition

    {

        private string _title;

        private int _year;

        private string _publisher;

        public string Title

        {

            get { return _title; }

            set { _title = value; }

        }

        public int Year

        {

            get { return _year; }

            set

            {

                if (value > 1000 && value <= 2025)

                    _year = value;

                else

                {

                    Console.WriteLine($"Некорректный год: {value}.");

                    _year = GetValidYear();

                }

            }

        }

        public string Publisher

        {

            get { return _publisher; }

            set { _publisher = value; }

        }

        private int GetValidYear()

        {

            int validYear;

            while (true)

            {

                Console.Write("Введите корректный год издания для демонстрации: ");

                if (int.TryParse(Console.ReadLine(), out validYear) && validYear > 1400 && validYear <= 2025)

                {

                    return validYear;

                }

                Console.WriteLine("Некорректный ввод. Попробуйте снова.");

            }

        }

        public PrintEdition()

        {

            Title = "Неизвестно";

            Year = 2025;

            Publisher = "Неизвестно";

        }

        public PrintEdition(string title, int year, string publisher)

        {

            Title = title;

            Year = year;

            Publisher = publisher;

        }

        public virtual void PrintInfo()

        {

            Console.WriteLine($"Печатное издание: \"{Title}\"");

            Console.WriteLine($"Год издания: {Year}");

            Console.WriteLine($"Издательство: {Publisher}");

        }

    }

    public class Book : PrintEdition

    {

        private string _author;

        private int _numberOfPages;

        public string Author

        {

            get { return _author; }

            set { _author = value; }

        }

        public int NumberOfPages

        {

            get { return _numberOfPages; }

            set

            {

                if (value > 0)

                    _numberOfPages = value;

                else

                {

                    Console.WriteLine("Количество страниц должно быть положительным.");

                    _numberOfPages = GetValidPage();

                }

            }

        }

        private int GetValidPage()

        {

            int validPages;

            while (true)

            {

                Console.Write("Введите корректное количество страниц: ");

                if (int.TryParse(Console.ReadLine(), out validPages) && validPages > 0)

                {

                    return validPages;

                }

                Console.WriteLine("Некорректный ввод. Попробуйте снова.");

            }

        }

        public Book() : base()

        {

            Author = "Неизвестен";

            NumberOfPages = 1;

        }

        public Book(string title, int year, string publisher, string author, int pages) : base(title, year, publisher)

        {

            Author = author;

            NumberOfPages = pages;

        }

        public override void PrintInfo()

        {

            base.PrintInfo();

            Console.WriteLine($"Автор: {Author}");

            Console.WriteLine($"Количество страниц: {NumberOfPages}");

        }

    }

    public class Magazine : PrintEdition

    {

        private int _issueNum;

        public int IssueNumber

        {

            get { return _issueNum; }

            set { _issueNum = value; }

        }

        public Magazine() : base()

        {

            IssueNumber = 0;

        }

        public Magazine(string title, int year, string publisher, int issue) : base(title, year, publisher)

        {

            IssueNumber = issue;

        }

        public override void PrintInfo()

        {

            base.PrintInfo();

            Console.WriteLine($"Номер выпуска: {IssueNumber}");

        }

    }

    public class Textbook : Book

    {

        private string _subject;

        private int _gradeLevel;

        public string Subject

        {

            get { return _subject; }

            set { _subject = value; }

        }

        public int GradeLevel

        {

            get { return _gradeLevel; }

            set { _gradeLevel = value; }

        }

        public Textbook() : base()

        {

            Subject = "Не указан";

            GradeLevel = 0;

        }

        public Textbook(string title, int year, string publisher, string author, int pages, string subject, int grade) : base(title, year, publisher, author, pages)

        {

            Subject = subject;

            GradeLevel = grade;

        }

        public override void PrintInfo()

        {

            base.PrintInfo();

            Console.WriteLine($"Предмет: {Subject}");

            Console.WriteLine($"Курс: {GradeLevel}");

        }

    }

    class Program

    {

        static void Main(string[] args)

        {

            Console.WriteLine("Демонстрация иерархии классов печатных изданий:\n");

            PrintEdition edition = new PrintEdition("Газета", 2025, "Пресс Холд");

            Book book = new Book("451° по Фаренгейту", -1953, "Ballantine Books", "Рэй Брэдбери", 256);

            Magazine magazine = new Magazine("National Geographic", 2020, "National Geographic Society", 157);

            Textbook textbook = new Textbook("Сборник задач и упражнений по математическому анализу.", 2025, "Лань", "Демидович Борис Павлович", 200, "Математический анализ", 2);

            Console.WriteLine("\nРезультаты после корректировки:");

            Console.WriteLine("----------------------");

            PrintEdition[] editions = { edition, book, magazine, textbook };

            foreach (var item in editions)

            {

                item.PrintInfo();

                Console.WriteLine("----------------------");

            }

            Console.ReadLine();

        }

    }

}