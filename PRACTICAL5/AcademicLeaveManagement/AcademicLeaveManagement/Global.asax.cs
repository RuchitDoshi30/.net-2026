using System;
using System.Web;

namespace AcademicLeaveManagement
{
    /// <summary>
    /// Application lifecycle event handler.
    /// Manages application-level and session-level lifecycle events for ASP.NET Web Forms.
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// Code that runs on application startup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Code that runs when a new user session is initiated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
            // Set 30 minute session duration
            Session.Timeout = 30;
        }

        /// <summary>
        /// Code that runs when an unhandled exception occurs.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            // Log or handle application error gracefully
        }

        /// <summary>
        /// Code that runs when a session ends or abandons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Session_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Code that runs on application shutdown.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
