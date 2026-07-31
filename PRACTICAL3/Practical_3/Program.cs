using System;
using System.Collections.Generic;

namespace ExpenseTrackingSystem
{
    // Custom Exception 1: Invalid Expense Amount Exception
    public class InvalidExpenseAmountException : Exception
    {
        public InvalidExpenseAmountException(string message) : base(message)
        {
        }
    }

    // Custom Exception 2: Budget Exceeded Exception
    public class BudgetExceededException : Exception
    {
        public double ExceededAmount { get; private set; }

        public BudgetExceededException(string message, double exceededAmount) : base(message)
        {
            ExceededAmount = exceededAmount;
        }
    }

    // Class representing an Expense entity
    public class Expense
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Category { get; private set; }
        public double Amount { get; private set; }
        public DateTime Date { get; private set; }

        public Expense(int id, string title, string category, double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidExpenseAmountException($"Invalid Amount: ₹{amount}. Expense amount must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Expense title cannot be empty or null.");
            }

            Id = id;
            Title = title;
            Category = category;
            Amount = amount;
            Date = DateTime.Now;
        }

        public void DisplayExpense()
        {
            Console.WriteLine($"[ID: {Id}] {Title,-15} | Category: {Category,-12} | Amount: ₹{Amount,-8:F2} | Date: {Date:yyyy-MM-dd HH:mm}");
        }
    }

    // Class managing Expense Tracker operations
    public class ExpenseTracker
    {
        private List<Expense> expenses;
        public double TotalBudget { get; private set; }
        private int nextId;

        public ExpenseTracker(double totalBudget)
        {
            if (totalBudget <= 0)
            {
                throw new InvalidExpenseAmountException($"Invalid Budget: ₹{totalBudget}. Total budget must be greater than zero.");
            }

            TotalBudget = totalBudget;
            expenses = new List<Expense>();
            nextId = 101;
        }

        // Method to add expense with exception handling
        public void AddExpense(string title, string category, double amount)
        {
            double currentTotal = GetTotalExpenses();
            if (currentTotal + amount > TotalBudget)
            {
                double overflow = (currentTotal + amount) - TotalBudget;
                throw new BudgetExceededException($"Expense of ₹{amount:F2} exceeds total budget! Shortfall: ₹{overflow:F2}", overflow);
            }

            Expense newExpense = new Expense(nextId++, title, category, amount);
            expenses.Add(newExpense);
            Console.WriteLine($"✅ Expense '{title}' (₹{amount:F2}) added successfully.");
        }

        // Method to calculate total expenses
        public double GetTotalExpenses()
        {
            double total = 0;
            foreach (var item in expenses)
            {
                total += item.Amount;
            }
            return total;
        }

        // Method to calculate remaining budget
        public double GetRemainingBudget()
        {
            return TotalBudget - GetTotalExpenses();
        }

        // Method to display all recorded expenses
        public void DisplayAllExpenses()
        {
            Console.WriteLine("\n=================== EXPENSE SUMMARY ===================");
            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses recorded yet.");
            }
            else
            {
                foreach (var exp in expenses)
                {
                    exp.DisplayExpense();
                }
            }
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"Total Budget      : ₹{TotalBudget,10:F2}");
            Console.WriteLine($"Total Spent       : ₹{GetTotalExpenses(),10:F2}");
            Console.WriteLine($"Remaining Balance : ₹{GetRemainingBudget(),10:F2}");
            Console.WriteLine("=======================================================\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=======================================================");
            Console.WriteLine("        EXPENSE TRACKING SYSTEM (PRACTICAL 3)          ");
            Console.WriteLine("=======================================================\n");

            ExpenseTracker tracker = null!;

            // Step 1: Set Initial Budget with Exception Handling (try-catch-finally)
            while (tracker == null)
            {
                try
                {
                    Console.Write("Enter Monthly Budget (₹): ");
                    string budgetInput = Console.ReadLine() ?? "";
                    double budget = Convert.ToDouble(budgetInput);

                    tracker = new ExpenseTracker(budget);
                    Console.WriteLine($"✅ Monthly Budget set to ₹{tracker.TotalBudget:F2} successfully.\n");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"❌ Format Error: Invalid number format. Details: {ex.Message}");
                    Console.WriteLine("   Please enter a valid numeric value.\n");
                }
                catch (InvalidExpenseAmountException ex)
                {
                    Console.WriteLine($"❌ Custom Exception Caught: {ex.Message}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Unexpected Error: {ex.Message}\n");
                }
                finally
                {
                    Console.WriteLine("[System Log] Budget initialization attempt completed.");
                }
            }

            // Step 2: Interactive Menu Loop with Robust Exception Handling
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n----- MENU OPTIONS -----");
                Console.WriteLine("1. Add New Expense");
                Console.WriteLine("2. View All Expenses & Balance");
                Console.WriteLine("3. Simulate System Error (DivideByZeroException Test)");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option (1-4): ");

                try
                {
                    string choiceInput = Console.ReadLine() ?? "";
                    int choice = Convert.ToInt32(choiceInput);

                    switch (choice)
                    {
                        case 1:
                            AddExpenseUI(tracker);
                            break;
                        case 2:
                            tracker.DisplayAllExpenses();
                            break;
                        case 3:
                            // Demonstration of handling standard runtime exception
                            TestDivideByZero();
                            break;
                        case 4:
                            running = false;
                            Console.WriteLine("\nExiting Expense Tracking Module. Thank you!");
                            break;
                        default:
                            Console.WriteLine("❌ Invalid Choice! Please select a number between 1 and 4.");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"❌ Input Format Error: {ex.Message}. Option must be an integer.");
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine($"❌ Overflow Error: Input number is too large or too small. Details: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ General Error: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("[System Log] Operation menu step completed.");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Helper method to gather user input and handle expense addition exceptions
        static void AddExpenseUI(ExpenseTracker tracker)
        {
            try
            {
                Console.Write("\nEnter Expense Title: ");
                string title = Console.ReadLine() ?? "";

                Console.Write("Enter Category (e.g., Food, Travel, Bills): ");
                string category = Console.ReadLine() ?? "";

                Console.Write("Enter Expense Amount (₹): ");
                double amount = Convert.ToDouble(Console.ReadLine());

                tracker.AddExpense(title, category, amount);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"❌ Invalid Amount Input: Amount must be numeric. Details: {ex.Message}");
            }
            catch (InvalidExpenseAmountException ex)
            {
                Console.WriteLine($"❌ Validation Error: {ex.Message}");
            }
            catch (BudgetExceededException ex)
            {
                Console.WriteLine($"❌ Budget Limit Error: {ex.Message}");
                Console.WriteLine($"   Exceeded Amount: ₹{ex.ExceededAmount:F2}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Argument Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error Adding Expense: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("[System Log] Add expense operation finalized.");
            }
        }

        // Method demonstrating Division by Zero Exception
        static void TestDivideByZero()
        {
            try
            {
                Console.WriteLine("\n[Test] Executing Division: 100 / 0 ...");
                int zero = 0;
                int result = 100 / zero;
                Console.WriteLine($"Result: {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"❌ Caught Built-in Exception: {ex.GetType().Name}");
                Console.WriteLine($"   Message: {ex.Message}");
                Console.WriteLine($"   StackTrace snippet: {ex.StackTrace?.Split('\n')[0]}");
            }
            finally
            {
                Console.WriteLine("[System Log] DivideByZero test finished execution.");
            }
        }
    }
}
