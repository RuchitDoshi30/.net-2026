using System;
using System.Collections.Generic;
using System.Linq;

namespace AcademicLeaveManagement.Models
{
    /// <summary>
    /// Provides in-memory seed data and lookup methods for college academic events and examination schedules.
    /// </summary>
    public static class AcademicEventRepository
    {
        private static readonly List<AcademicEvent> _events;

        static AcademicEventRepository()
        {
            DateTime now = DateTime.Today;
            int year = now.Year;
            int month = now.Month;

            _events = new List<AcademicEvent>
            {
                new AcademicEvent(1, new DateTime(year, month, 2), "Semester Commencement", "Official inauguration of Odd Semester classes & orientation session.", "Milestone", "#2563eb"),
                new AcademicEvent(2, new DateTime(year, month, 8), "Assignment 1 Submission", "Submission deadline for DNT Unit 1 & 2 laboratory worksheets.", "Submission", "#d97706"),
                new AcademicEvent(3, new DateTime(year, month, 15), "Independence Day / Institutional Holiday", "National celebration and cultural ceremony in auditorium.", "Holiday", "#16a34a"),
                new AcademicEvent(4, new DateTime(year, month, 22), "Mid-Semester Examination (Theory)", "Internal written assessment covering Units 1 to 3.", "Exam", "#dc2626"),
                new AcademicEvent(5, new DateTime(year, month, 25), "Mid-Semester Assessment Review", "Display of evaluated marks and faculty counseling.", "Milestone", "#4f46e5"),
                new AcademicEvent(6, new DateTime(year, month, 28), "Practical Lab Evaluation Phase 1", "Hands-on continuous evaluation for ASP.NET Web Forms.", "Practical", "#9333ea"),
                
                // Next month events
                new AcademicEvent(7, (new DateTime(year, month, 1)).AddMonths(1).AddDays(4), "Assignment 2 & Project Milestone", "Submission of mini-project synopsis and lab file check.", "Submission", "#d97706"),
                new AcademicEvent(8, (new DateTime(year, month, 1)).AddMonths(1).AddDays(11), "Pre-University Examination (Theory)", "Comprehensive 70-mark preparatory test.", "Exam", "#dc2626"),
                new AcademicEvent(9, (new DateTime(year, month, 1)).AddMonths(1).AddDays(18), "University Practical Exam & Viva", "External examiner practical viva and code verification.", "Practical", "#9333ea"),
                new AcademicEvent(10, (new DateTime(year, month, 1)).AddMonths(1).AddDays(24), "Semester Concluding Meeting", "Term end declaration and issuance of hall tickets.", "Milestone", "#2563eb"),

                // Previous month sample event (if looking back)
                new AcademicEvent(11, (new DateTime(year, month, 1)).AddMonths(-1).AddDays(14), "Academic Council & Syllabus Release", "Publishing of teaching plan and academic calendar.", "Milestone", "#64748b")
            };
        }

        /// <summary>
        /// Retrieves all scheduled academic events.
        /// </summary>
        /// <returns>A read-only list of academic events.</returns>
        public static List<AcademicEvent> GetAllEvents()
        {
            return _events.OrderBy(e => e.EventDate).ToList();
        }

        /// <summary>
        /// Retrieves an academic event occurring on a specific date, if one exists.
        /// </summary>
        /// <param name="date">The calendar date to check.</param>
        /// <returns>The academic event matching the date, or null if none is scheduled.</returns>
        public static AcademicEvent GetEventByDate(DateTime date)
        {
            return _events.FirstOrDefault(e => e.EventDate.Date == date.Date);
        }

        /// <summary>
        /// Retrieves upcoming academic events from the current date forward.
        /// </summary>
        /// <returns>A list of future academic events.</returns>
        public static List<AcademicEvent> GetUpcomingEvents()
        {
            DateTime today = DateTime.Today;
            return _events.Where(e => e.EventDate >= today)
                          .OrderBy(e => e.EventDate)
                          .ToList();
        }
    }
}
