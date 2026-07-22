using System;

namespace StudentAdmissionManagement
{
    class Student
    {
        // Private Data Members (Encapsulation & Access Modifiers)
        private int studentId;
        private string studentName;
        private int age;
        private string gender;
        private string course;
        private double fees;
        private double paidFees;
        private string admissionStatus;

        // Parameterized Constructor
        public Student(int id, string name, int age, string gender, string course, double fees)
        {
            this.studentId = id;
            this.studentName = name;
            this.age = age;
            this.gender = gender;
            this.course = course;
            this.fees = fees;
            this.paidFees = 0;
            this.admissionStatus = "Pending";
        }

        // Method: Apply for Admission
        public void ApplyAdmission()
        {
            admissionStatus = "Applied";
            Console.WriteLine("\nAdmission Application Submitted Successfully.");
        }

        // Method: Confirm Admission
        public void ConfirmAdmission()
        {
            admissionStatus = "Confirmed";
            Console.WriteLine("Admission Confirmed.");
        }

        // Method: Pay Fees
        public void PayFees(double amount)
        {
            paidFees += amount;

            Console.WriteLine("\nFee Paid Successfully.");

            if (paidFees >= fees)
            {
                Console.WriteLine("All Fees Paid.");
            }
            else
            {
                Console.WriteLine("Remaining Fees: " + (fees - paidFees));
            }
        }

        // Method: Update Course
        public void UpdateCourse(string newCourse)
        {
            course = newCourse;
            Console.WriteLine("Course Updated Successfully.");
        }

        // Method: Display Student Details
        public void DisplayStudent()
        {
            Console.WriteLine("\n========== Student Admission Details ==========");
            Console.WriteLine("Student ID        : " + studentId);
            Console.WriteLine("Student Name      : " + studentName);
            Console.WriteLine("Age               : " + age);
            Console.WriteLine("Gender            : " + gender);
            Console.WriteLine("Course            : " + course);
            Console.WriteLine("Total Fees        : " + fees);
            Console.WriteLine("Paid Fees         : " + paidFees);
            Console.WriteLine("Admission Status  : " + admissionStatus);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // User Input
            Console.Write("Enter Student ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Age: ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Gender: ");
            string gender = Console.ReadLine() ?? "";

            Console.Write("Enter Course: ");
            string course = Console.ReadLine() ?? "";

            Console.Write("Enter Total Fees: ");
            double fees = Convert.ToDouble(Console.ReadLine());

            // Create Object using Parameterized Constructor
            Student s1 = new Student(id, name, age, gender, course, fees);

            // Admission Process
            s1.ApplyAdmission();

            Console.Write("\nEnter Fee Amount to Pay: ");
            double amount = Convert.ToDouble(Console.ReadLine());
            s1.PayFees(amount);

            Console.Write("\nDo you want to change the course? (Y/N): ");
            string choice = Console.ReadLine() ?? "";

            if (choice.ToUpper() == "Y")
            {
                Console.Write("Enter New Course: ");
                string newCourse = Console.ReadLine() ?? "";
                s1.UpdateCourse(newCourse);
            }

            s1.ConfirmAdmission();

            // Display Details
            s1.DisplayStudent();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
