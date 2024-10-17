using System;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        XDocument doc = XDocument.Load("Zavod.xml");

        foreach (var worker in doc.Descendants("Worker"))
        {
            Console.WriteLine($"Номер цеху: {worker.Element("DepNumber").Value}");
            Console.WriteLine($"Посада: {worker.Element("Position").Value}");
            Console.WriteLine($"Стаж роботи: {worker.Element("Experience").Value}");
            Console.WriteLine($"Заробітна плата: {worker.Element("Salary").Value}");
            Console.WriteLine($"Прізвище: {worker.Element("Surname").Value}");
            Console.WriteLine();
        }

        Console.WriteLine("Введіть прізвище для пошуку: ");
        string inputSurname = Console.ReadLine();

        var workerBySurname = doc.Descendants("Worker")
            .Where(w => w.Element("Surname").Value == inputSurname)
            .Select(w => new
            {
                Number = w.Element("DepNumber").Value,
                Position = w.Element("Position").Value,
                Experience = w.Element("Experience").Value,
                Salary = w.Element("Salary").Value
            })
            .FirstOrDefault();

        if (workerBySurname != null)
        {
            Console.WriteLine($"Номер цеху: {workerBySurname.Number}");
            Console.WriteLine($"Посада: {workerBySurname.Position}");
            Console.WriteLine($"Стаж роботи: {workerBySurname.Experience}");
            Console.WriteLine($"Заробітна плата: {workerBySurname.Salary}");
        }
        else
        {
            Console.WriteLine("Робітника з таким прізвищем не знайдено.");
        }

        Console.WriteLine("Введіть номер цеху для обчислення середньої зарплати: ");
        int workshopNumber = int.Parse(Console.ReadLine());

        var averageSalary = doc.Descendants("Worker")
            .Where(w => int.Parse(w.Element("DepNumber").Value) == workshopNumber)
            .Average(w => double.Parse(w.Element("Salary").Value));

        Console.WriteLine($"Середня заробітна плата в цеху {workshopNumber}: {averageSalary}");
    }
}
