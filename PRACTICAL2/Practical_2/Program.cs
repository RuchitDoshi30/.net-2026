using System;

interface IPayroll
{
    void CalculateSalary();
}

class Employee
{
    public string Name { get; private set; }
    public int Id { get; private set; }

    public Employee(string name, int id)
    {
        Name = name;
        Id = id;
    }
}

class FullTimeEmployee : Employee, IPayroll
{
    public double Salary { get; private set; }

    public FullTimeEmployee(string name, int id, double salary)
        : base(name, id)
    {
        Salary = salary;
    }

    public void CalculateSalary()
    {
        Console.WriteLine("Employee: " + Name);
        Console.WriteLine("Employee ID: " + Id);
        Console.WriteLine("Salary: " + Salary);
    }
}

class Program
{
    static void Main(string[] args)
    {
        FullTimeEmployee employee = new FullTimeEmployee("TONY STARK", 101, 30000);
        IPayroll payroll = employee;
        payroll.CalculateSalary();
    }
}
