# Practical 3: Expense Tracking Module with Exception Handling

## 📌 AIM
Develop an **Expense Tracking Module** in C# showcasing robust **Exception Handling** techniques, including `try`, `catch`, `finally` blocks, built-in exception catching (`FormatException`, `DivideByZeroException`, `OverflowException`), throwing exceptions, and defining **Custom Application Exceptions** (`InvalidExpenseAmountException`, `BudgetExceededException`).

---

## 📖 Theoretical Concept Explanation

### 1. Exception Handling Architecture in .NET
- **Exception**: An exception is an unexpected error event occurring during program execution that disrupts the normal flow of instructions.
- **`System.Exception`**: The base class for all exceptions in .NET.
- **Handling Hierarchy**:
  - `System.Object` → `System.Exception`
    - `System.SystemException` (Built-in runtime errors: `FormatException`, `DivideByZeroException`, `OverflowException`, `ArgumentException`)
    - `System.ApplicationException` / User-defined Custom Exceptions (`InvalidExpenseAmountException`, `BudgetExceededException`)

---

### 2. The `try-catch-finally` Mechanism

```csharp
try
{
    // Code that might throw an exception
}
catch (FormatException ex)
{
    // Specific exception handler
}
catch (InvalidExpenseAmountException ex)
{
    // Custom exception handler
}
catch (Exception ex)
{
    // General exception handler (Catch-all)
}
finally
{
    // Code that ALWAYS runs, regardless of whether an exception occurred
}
```

- **`try` block**: Encloses code that might generate/throw an exception.
- **`catch` block**: Handles specific types of exceptions thrown from the `try` block. Multiple catch blocks can be chained from most specific to most general.
- **`finally` block**: Guarantees cleanup code execution (e.g., closing file streams, DB connections, or logging system steps) even if an exception occurs or a return statement is reached.

---

### 3. Custom Exceptions (User-Defined Exceptions)
- **Definition**: Custom exceptions are created by deriving a new class from `System.Exception`.
- **Purpose**: Represents domain-specific errors (e.g., business rules like invalid monetary amount or budget overflow) rather than system-level errors.

```csharp
public class InvalidExpenseAmountException : Exception
{
    public InvalidExpenseAmountException(string message) : base(message)
    {
    }
}

public class BudgetExceededException : Exception
{
    public double ExceededAmount { get; private set; }

    public BudgetExceededException(string message, double exceededAmount) : base(message)
    {
        ExceededAmount = exceededAmount;
    }
}
```

---

### 4. Throwing Exceptions (`throw` keyword)
- **`throw`**: Used to explicitly raise an exception when a condition or rule is violated.

```csharp
if (amount <= 0)
{
    throw new InvalidExpenseAmountException($"Invalid Amount: ₹{amount}. Expense amount must be greater than zero.");
}
```

---

## 🏗️ Class Architecture & Flowchart

```
                 System.Exception
                        |
        +---------------+---------------+
        |                               |
InvalidExpenseAmountException  BudgetExceededException
  (Custom Exception)             (Custom Exception)
        ^                               ^
        | thrown by                     | thrown by
        +--------------+----------------+
                       |
               ExpenseTracker Class
               - TotalBudget : double
               - expenses : List<Expense>
               + AddExpense(title, category, amount)
               + GetTotalExpenses() : double
               + GetRemainingBudget() : double
               + DisplayAllExpenses()
```

---

## 🛠️ Key Features of the Module

1. **Budget Setup**: Prompts user for initial budget and catches non-numeric format errors (`FormatException`) and non-positive numbers (`InvalidExpenseAmountException`).
2. **Expense Management**: Validates title, category, and amount. Throws custom `BudgetExceededException` if expense exceeds remaining balance.
3. **Built-in Exception Test**: Interactive test option demonstrating `DivideByZeroException`.
4. **Log Clean-up**: `finally` blocks log every step completion.

---

## 🚀 How to Run the Program

### Prerequisites
- [.NET SDK 6.0 or higher](https://dotnet.microsoft.com/download)

### Execution Steps
1. Open terminal inside the `Practical_3` directory:
   ```bash
   cd PRACTICAL3/Practical_3
   ```
2. Build and run the program:
   ```bash
   dotnet run
   ```

---

## 💻 Sample Output Scenarios

### Scenario A: Format Exception Handling
```text
Enter Monthly Budget (₹): abc
❌ Format Error: Invalid number format. Details: The input string 'abc' was not in a correct format.
   Please enter a valid numeric value.

[System Log] Budget initialization attempt completed.
Enter Monthly Budget (₹): 10000
✅ Monthly Budget set to ₹10000.00 successfully.
```

### Scenario B: Custom Budget Exceeded Exception
```text
Enter Expense Title: Laptop Repair
Enter Category (e.g., Food, Travel, Bills): Electronics
Enter Expense Amount (₹): 15000
❌ Budget Limit Error: Expense of ₹15000.00 exceeds total budget! Shortfall: ₹5000.00
   Exceeded Amount: ₹5000.00
[System Log] Add expense operation finalized.
```

### Scenario C: Successful Expense Addition & Summary
```text
Enter Expense Title: Groceries
Enter Category (e.g., Food, Travel, Bills): Food
Enter Expense Amount (₹): 2500
✅ Expense 'Groceries' (₹2500.00) added successfully.
[System Log] Add expense operation finalized.

=================== EXPENSE SUMMARY ===================
[ID: 101] Groceries       | Category: Food         | Amount: ₹2500.00  | Date: 2026-07-31 11:15
-------------------------------------------------------
Total Budget      : ₹  10000.00
Total Spent       : ₹   2500.00
Remaining Balance : ₹   7500.00
=======================================================
```
