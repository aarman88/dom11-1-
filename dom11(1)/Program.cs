using System;
using System.Collections.Generic;
using System.Linq;

public interface IEmployee
{
    string FirstName { get; set; }
    string LastName { get; set; }
    DateTime HireDate { get; set; }
    string Position { get; set; }
    decimal Salary { get; set; }
    char Gender { get; set; }
}

public struct Employee : IEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public char Gender { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, {Position}, Salary: {Salary:C}, Gender: {Gender}, Hire Date: {HireDate.ToShortDateString()}";
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите количество сотрудников: ");
        int count = int.Parse(Console.ReadLine());

        IEmployee[] employees = new IEmployee[count];
        for (int i = 0; i < count; i++)
        {
            employees[i] = ReadEmployeeFromConsole();
        }

        // Пример использования методов
        Console.WriteLine("a. Полная информация обо всех сотрудниках:");
        PrintEmployees(employees);

        Console.Write("\nb. Введите должность для отображения информации: ");
        string position = Console.ReadLine();
        PrintEmployeesByPosition(employees, position);

        Console.WriteLine("\nc. Менеджеры с зарплатой выше средней зарплаты клерков:");
        PrintManagersAboveClerkAverageSalary(employees);

        Console.Write("\nd. Введите дату для отображения информации о сотрудниках, принятых позже: ");
        DateTime hireDateFilter = DateTime.Parse(Console.ReadLine());
        PrintEmployeesHiredAfter(employees, hireDateFilter);

        Console.Write("\ne. Введите пол для фильтрации (M/F или оставьте пустым): ");
        string genderFilter = Console.ReadLine();
        PrintEmployeesByGender(employees, genderFilter);
    }

    static IEmployee ReadEmployeeFromConsole()
    {
        Console.Write("Введите имя: ");
        string firstName = Console.ReadLine();

        Console.Write("Введите фамилию: ");
        string lastName = Console.ReadLine();

        Console.Write("Введите дату приема на работу (гггг-мм-дд): ");
        DateTime hireDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Введите должность: ");
        string position = Console.ReadLine();

        Console.Write("Введите зарплату: ");
        decimal salary = decimal.Parse(Console.ReadLine());

        Console.Write("Введите пол (M/F): ");
        char gender = char.Parse(Console.ReadLine());

        return new Employee
        {
            FirstName = firstName,
            LastName = lastName,
            HireDate = hireDate,
            Position = position,
            Salary = salary,
            Gender = gender
        };
    }

    static void PrintEmployees(IEmployee[] employees)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine(employee);
        }
    }

    static void PrintEmployeesByPosition(IEmployee[] employees, string position)
    {
        var filteredEmployees = employees.Where(e => e.Position == position);
        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine(employee);
        }
    }

    static void PrintManagersAboveClerkAverageSalary(IEmployee[] employees)
    {
        var clerks = employees.Where(e => e.Position.ToLower() == "clerk");
        decimal clerkAverageSalary = clerks.Any() ? clerks.Average(e => e.Salary) : 0;

        var managers = employees.Where(e => e.Position.ToLower() == "manager" && e.Salary > clerkAverageSalary)
                               .OrderBy(e => e.LastName);

        foreach (var manager in managers)
        {
            Console.WriteLine(manager);
        }
    }

    static void PrintEmployeesHiredAfter(IEmployee[] employees, DateTime hireDateFilter)
    {
        var filteredEmployees = employees.Where(e => e.HireDate > hireDateFilter)
                                         .OrderBy(e => e.LastName);

        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine(employee);
        }
    }

    static void PrintEmployeesByGender(IEmployee[] employees, string genderFilter)
    {
        var filteredEmployees = string.IsNullOrWhiteSpace(genderFilter)
            ? employees
            : employees.Where(e => char.ToUpper(e.Gender) == char.ToUpper(genderFilter[0]));

        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine(employee);
        }
    }
}
