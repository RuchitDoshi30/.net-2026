# Practical 1: Student Admission Management Module

## 📌 AIM
Develop a **Student Admission Management Module** using Object-Oriented Programming (OOP) concepts in C# including **Classes, Objects, Constructors, Access Modifiers, and Encapsulation**.

---

## 📖 Theoretical Concept Explanation

### 1. Class
- **Definition**: A class is a user-defined blueprint or prototype from which objects are created. It groups related data fields (attributes) and methods (behaviors) into a single logical unit.
- **In this Practical**: The `Student` class represents a student seeking admission. It defines attributes such as `studentId`, `studentName`, `age`, `gender`, `course`, `fees`, `paidFees`, and `admissionStatus`.

```csharp
class Student
{
    // Class members definition
}
```

---

### 2. Object
- **Definition**: An object is a real-world entity created (instantiated) from a class. It holds state (data) in its fields and exhibits behavior through its methods.
- **In this Practical**: An instance `s1` of class `Student` is created dynamically at runtime using the `new` keyword based on user input.

```csharp
Student s1 = new Student(id, name, age, gender, course, fees);
```

---

### 3. Constructor & Parameterized Constructor
- **Definition**: A constructor is a special member function executed automatically when an object of a class is created. It has the same name as the class and no return type.
- **Parameterized Constructor**: Accepts parameters to initialize object attributes with custom values at the time of creation.
- **`this` Keyword**: Refers to the current instance of the class, helping distinguish class instance members from constructor parameters when names overlap.
- **In this Practical**: 

```csharp
public Student(int id, string name, int age, string gender, string course, double fees)
{
    this.studentId = id;
    this.studentName = name;
    this.age = age;
    this.gender = gender;
    this.course = course;
    this.fees = fees;
    this.paidFees = 0;          // Initial default value
    this.admissionStatus = "Pending"; // Initial default status
}
```

---

### 4. Access Modifiers
Access modifiers define the accessibility/visibility of class members from outside the class.

| Access Modifier | Description | Usage in Code |
| :--- | :--- | :--- |
| **`private`** | Accessible only within the same class. Used to restrict direct external access to critical data. | Data members (`studentId`, `fees`, `paidFees`, etc.) |
| **`public`** | Accessible from any part of the program (outside the class). Used to expose operations/interfaces. | Methods (`ApplyAdmission()`, `PayFees()`, `DisplayStudent()`, etc.) |

---

### 5. Encapsulation & Data Hiding
- **Definition**: Encapsulation is the mechanism of wrapping data (variables) and code (methods) together as a single unit while restricting direct access to internal states.
- **Benefits**:
  - Prevents illegal or accidental state modifications from outside code.
  - Ensures data integrity (e.g., updating `paidFees` only through `PayFees()` method).

---

## 🛠️ Program Features & Workflow

1. **Student Registration**: Prompts user for Student ID, Name, Age, Gender, Course, and Total Fees.
2. **Apply for Admission**: Invokes `ApplyAdmission()` to change status to `"Applied"`.
3. **Fee Payment**: Updates paid fee amount and displays the remaining balance.
4. **Course Update (Optional)**: Allows updating the registered course dynamically.
5. **Confirm Admission**: Updates admission status to `"Confirmed"`.
6. **Display Summary**: Prints full student profile and current status.

---

## 🚀 How to Run the Program

### Prerequisites
- [.NET SDK 6.0 or higher](https://dotnet.microsoft.com/download)

### Execution Steps
1. Open terminal inside the `Practical_1` folder:
   ```bash
   cd Practical_1
   ```
2. Build and run the C# project using .NET CLI:
   ```bash
   dotnet run
   ```

---

## 💻 Sample Program Output

```text
Enter Student ID: 101
Enter Student Name: Ruchit Doshi
Enter Age: 20
Enter Gender: Male
Enter Course: Computer Science
Enter Total Fees: 55000

Admission Application Submitted Successfully.

Enter Fee Amount to Pay: 30000

Fee Paid Successfully.
Remaining Fees: 25000

Do you want to change the course? (Y/N): Y
Enter New Course: Data Science
Course Updated Successfully.
Admission Confirmed.

========== Student Admission Details ==========
Student ID        : 101
Student Name      : Ruchit Doshi
Age               : 20
Gender            : Male
Course            : Data Science
Total Fees        : 55000
Paid Fees         : 30000
Admission Status  : Confirmed

Press any key to exit...
```
