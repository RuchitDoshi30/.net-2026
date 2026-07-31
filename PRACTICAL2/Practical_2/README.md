# Practical 2: Employee Payroll System

## 📌 AIM
Design an **Employee Payroll System** using Object-Oriented Programming (OOP) concepts in C# including **Inheritance, Interface & Polymorphism**.

---

## 📖 Theoretical Concept Explanation

### 1. Inheritance
- **Definition**: Inheritance is a fundamental OOP concept that allows a class (derived/child class) to acquire the properties and behaviors of another class (base/parent class). It promotes code reusability and establishes an **IS-A** relationship.
- **In this Practical**: The `FullTimeEmployee` class inherits from the `Employee` base class, gaining access to the `Name` and `Id` properties without redefining them.

```csharp
class FullTimeEmployee : Employee, IPayroll
{
    // Inherits Name and Id from Employee
}
```

---

### 2. Interface
- **Definition**: An interface is a contract that defines a set of abstract methods and properties that a class must implement. It supports full abstraction and enables **multiple inheritance** in C#. A class can implement multiple interfaces.
- **Syntax**: Declared using the `interface` keyword; all members are implicitly `public` and `abstract`.
- **In this Practical**: `IPayroll` is an interface with the `CalculateSalary()` method. `FullTimeEmployee` is bound by contract to implement this method.

```csharp
interface IPayroll
{
    void CalculateSalary();
}
```

---

### 3. Polymorphism
- **Definition**: Polymorphism (meaning "many forms") allows objects of different types to be treated through a common interface/base class reference. It enables a single interface to represent different underlying data types or implementations.
- **Types Used**:
  - **Runtime Polymorphism (Method Overriding via Interface)**: The `IPayroll` reference variable `payroll` holds a `FullTimeEmployee` object and calls the overridden `CalculateSalary()` method at runtime.
- **In this Practical**: An `IPayroll` interface reference is assigned a `FullTimeEmployee` object. When `CalculateSalary()` is called through the interface reference, the actual method defined in `FullTimeEmployee` is executed at runtime.

```csharp
FullTimeEmployee employee = new FullTimeEmployee("TONY STARK", 101, 30000);
IPayroll payroll = employee; // Polymorphism: Interface reference to derived object
payroll.CalculateSalary();  // Runtime method dispatch
```

---

### 4. Encapsulation (in Properties)
- **Definition**: Encapsulation wraps data and behavior together. C# properties (`get; private set;`) are used here to expose data in a controlled way — publicly readable but only modifiable within the class itself.
- **In this Practical**: `Name`, `Id`, and `Salary` use `private set` to prevent external modification while remaining publicly readable.

```csharp
public string Name { get; private set; }
public int Id { get; private set; }
public double Salary { get; private set; }
```

---

## 🏗️ Class Hierarchy & UML Overview

```
        IPayroll (Interface)
            |
            | implements
            |
Employee (Base Class)
    - Name : string
    - Id   : int
    + Employee(name, id)
            |
            | inherits
            |
FullTimeEmployee (Derived Class)
    - Salary : double
    + FullTimeEmployee(name, id, salary)
    + CalculateSalary()         ← implements IPayroll
```

---

## 🛠️ Program Features & Workflow

1. **Interface Contract (`IPayroll`)**: Declares the `CalculateSalary()` method as a contract.
2. **Base Class (`Employee`)**: Holds common employee data — `Name` and `Id`.
3. **Derived Class (`FullTimeEmployee`)**: Inherits from `Employee` and implements `IPayroll`. Contains the `Salary` field and the `CalculateSalary()` implementation.
4. **Polymorphism in Action**: A `FullTimeEmployee` object is assigned to an `IPayroll` reference, demonstrating runtime polymorphism.

---

## 🚀 How to Run the Program

### Prerequisites
- [.NET SDK 6.0 or higher](https://dotnet.microsoft.com/download)

### Execution Steps
1. Open terminal inside the `Practical_2` folder:
   ```bash
   cd PRACTICAL2/Practical_2
   ```
2. Build and run the C# project using .NET CLI:
   ```bash
   dotnet run
   ```

---

## 💻 Sample Program Output

```text
Employee: TONY STARK
Employee ID: 101
Salary: 30000
```

---

## 🔑 Key Concepts Summary

| Concept | Applied Through | Purpose |
| :--- | :--- | :--- |
| **Inheritance** | `FullTimeEmployee : Employee` | Reuse `Name` and `Id` from base class |
| **Interface** | `IPayroll` → `CalculateSalary()` | Define a payroll contract |
| **Interface Implementation** | `FullTimeEmployee : IPayroll` | Fulfill the payroll contract |
| **Polymorphism** | `IPayroll payroll = employee` | Treat derived object via interface reference |
| **Encapsulation** | `get; private set;` properties | Protect data from external modification |
