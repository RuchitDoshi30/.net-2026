# Practical 5: Academic Calendar & Leave Management System 📅🏛️

## 📌 Overview
The **Academic Calendar & Leave Management System** (`AcademicLeaveManagement`) is a complete, enterprise-styled **ASP.NET Web Forms** university portal application built on the **.NET Framework 4.8**. It demonstrates core concepts of server-side Web Forms development: **Rich Web Server Controls** (such as `Calendar`, `GridView`, `DetailsView`, `DropDownList`, `RadioButtonList`, `CheckBoxList`, and `ListBox`), **Session State Management**, **Persistent HTTP Cookies ("Remember Me")**, **Multi-Layer Validation Controls**, **Master Page Architecture**, and **Event-Driven Programming**.

---

## 🎨 Key Features & Architecture

- **Master Page Layout & Consistent Navigation (`Site.Master`)**:
  - Centralized university portal header with student greeting and session indicator.
  - Active tab highlighting across `Dashboard.aspx`, `AcademicCalendar.aspx`, `ApplyLeave.aspx`, and `LeaveHistory.aspx`.
  - Secure session-clearing logout button.
- **Rich Interactive Academic Calendar (`AcademicCalendar.aspx`)**:
  - Leverages the ASP.NET `<asp:Calendar>` Rich Control.
  - **Dynamic `DayRender` Event**: Highlights exam dates, practical evaluations, submissions, and institutional holidays directly inside calendar cells with custom badges and tooltips.
  - **Interactive `SelectionChanged` Event**: Displays instant details (date, name, category badge, guidelines) when clicking on any date.
  - **Two-Way Synchronization**: Includes an `<asp:ListBox>` schedule directory with `AutoPostBack="true"` to select and jump to any semester milestone.
- **Robust Multi-Layer Leave Application (`ApplyLeave.aspx`)**:
  - **Rich Controls**: `DropDownList` (leave type), `RadioButtonList` (session duration), and `CheckBoxList` (supporting document checklist).
  - **Server-Side Validation**: `RequiredFieldValidator`, `CustomValidator` for server-side date comparisons (ensuring end date $\ge$ start date and reason $\ge 10$ characters), and `ValidationSummary`.
  - Generates unique tracking IDs (`LV-2026-XXXX`) and persists applications in `Session["LeaveApplications"]`.
- **Leave History & Inspection (`LeaveHistory.aspx`)**:
  - **`<asp:GridView>` Control**: Formatted data grid with custom template status badges (`Pending`, `Approved`, `Rejected`), date formatting, and row selection commands.
  - **`<asp:DetailsView>` Control**: Deep inspection of selected leave records including submission timestamps and attached documentation.
  - **Practical Status Simulation**: Interactive buttons to simulate faculty approval/rejection workflows in memory during lab evaluations.
- **Session & Cookie State Management (`Login.aspx`)**:
  - **Session State**: Maintains user authentication (`Session["IsLoggedIn"]`), username (`Session["Username"]`), and student applications (`Session["LeaveApplications"]`).
  - **Persistent Cookies**: Implements `StudentUsername` cookie with a 15-day expiration when "Remember Me" is checked, automatically pre-filling credentials on return visits.

---

## 📁 Project Structure

```text
PRACTICAL5/
├── AcademicLeaveManagement/
│   ├── AcademicLeaveManagement.sln          # Visual Studio 2022 Solution File
│   └── AcademicLeaveManagement/
│       ├── Content/
│       │   └── Site.css                     # University Glassmorphic CSS Theme
│       ├── Models/
│       │   ├── LeaveApplication.cs          # Leave Data Model
│       │   ├── AcademicEvent.cs             # Academic Event Data Model
│       │   └── AcademicEventRepository.cs   # In-Memory Event Seed Repository
│       ├── Properties/
│       │   └── AssemblyInfo.cs              # Assembly Metadata
│       ├── AcademicCalendar.aspx            # Rich Calendar Control Page
│       ├── AcademicCalendar.aspx.cs         # Calendar Code-Behind (DayRender / SelectionChanged)
│       ├── AcademicCalendar.aspx.designer.cs
│       ├── ApplyLeave.aspx                  # Leave Application Form
│       ├── ApplyLeave.aspx.cs               # Validation & Submission Code-Behind
│       ├── ApplyLeave.aspx.designer.cs
│       ├── Dashboard.aspx                   # Student Overview & KPIs
│       ├── Dashboard.aspx.cs                # Metric Calculation Code-Behind
│       ├── Dashboard.aspx.designer.cs
│       ├── Global.asax                      # Application & Session Lifecycle Hooks
│       ├── Global.asax.cs
│       ├── LeaveHistory.aspx                # GridView & DetailsView Tracking Page
│       ├── LeaveHistory.aspx.cs             # Data-Binding & Simulation Code-Behind
│       ├── LeaveHistory.aspx.designer.cs
│       ├── Login.aspx                       # Authentication & Cookie Management
│       ├── Login.aspx.cs                    # Session & Cookie Logic
│       ├── Login.aspx.designer.cs
│       ├── Site.Master                      # Master Layout Page
│       ├── Site.Master.cs                   # Master Page Code-Behind & Logout
│       ├── Site.Master.designer.cs
│       ├── Web.config                       # .NET Framework 4.8 Configuration
│       └── AcademicLeaveManagement.csproj   # Visual Studio Project File
└── README.md
```

---

## 🚀 How to Open and Run the Project

### Option A: Antigravity IDE (Single-Command Run)
Open a terminal in the `PRACTICAL5` folder and execute the automated build & run script:
```powershell
.\run.ps1
```
This automatically compiles the project using MSBuild, starts IIS Express on port `5055`, and displays the URL.

### Option B: Microsoft Visual Studio 2022
1. Launch **Visual Studio 2022**.
2. Click **Open a project or solution**.
3. Select:
   `PRACTICAL5\AcademicLeaveManagement\AcademicLeaveManagement.sln`
   (or the root `Lab.sln`).
4. Ensure the startup project is set to **`AcademicLeaveManagement`** and the startup page is **`Login.aspx`**.
5. Press **F5** (or click **IIS Express (Google Chrome / Microsoft Edge)**) to build and run.

### Option C: Manual Command-Line (MSBuild & IIS Express)
1. Build the solution using MSBuild:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "PRACTICAL5\AcademicLeaveManagement\AcademicLeaveManagement.sln" /t:Build /p:Configuration=Debug
   ```
2. Start IIS Express:
   ```powershell
   & "C:\Program Files\IIS Express\iisexpress.exe" /path:"$PWD\AcademicLeaveManagement\AcademicLeaveManagement" /port:5055
   ```
3. Open your browser at: **`http://localhost:5055/Login.aspx`**

---

## 🔐 Credentials & Demonstration Workflow

### Demo Credentials
* **Username**: `student`
* **Password**: `12345`

### Complete Demonstration Flow
```text
Open Login.aspx
       ↓
Enter student / 12345 & Check "Remember Me"
       ↓
Session["Username"] and Session["IsLoggedIn"] created
"StudentUsername" persistent cookie stored
       ↓
Dashboard.aspx (Displays Welcome message, KPI metrics, upcoming events)
       ↓
AcademicCalendar.aspx (View Calendar, DayRender highlighted cells, select dates)
       ↓
ApplyLeave.aspx (Fill form, DropDownList, RadioButtonList, CheckBoxList, Validators run)
       ↓
Leave submitted and stored into Session["LeaveApplications"]
       ↓
LeaveHistory.aspx (GridView binds data, click "Select" to inspect in DetailsView)
       ↓
Click "Approve Leave" or "Reject Leave" to test workflow simulation
       ↓
Click "Logout" (Session.Clear() & Session.Abandon() executed)
       ↓
Redirected back to Login.aspx (Cookie pre-fills "student" username)
```

---

## 🎛️ Rich Controls & Web Server Controls Used

| Control Name | Type | Implementation Location | Practical Purpose |
| :--- | :--- | :--- | :--- |
| **`asp:Calendar`** | Rich Control | `AcademicCalendar.aspx` | Interactive monthly calendar with dynamic `DayRender` event coloring and `SelectionChanged` event handling. |
| **`asp:GridView`** | Data Control | `LeaveHistory.aspx`, `Dashboard.aspx` | Formatted multi-column data table bound to strongly-typed in-memory session collections. |
| **`asp:DetailsView`**| Data Control | `LeaveHistory.aspx` | Detailed record inspection for the selected row in GridView. |
| **`asp:DropDownList`**| List Control | `ApplyLeave.aspx`, `LeaveHistory.aspx` | Leave type selection and status filtering with `AutoPostBack`. |
| **`asp:RadioButtonList`**| List Control | `ApplyLeave.aspx` | Leave session mode (Full Day vs. Half Day Morning/Afternoon). |
| **`asp:CheckBoxList`** | List Control | `ApplyLeave.aspx` | Multi-select supporting document checklist. |
| **`asp:ListBox`** | List Control | `AcademicCalendar.aspx`, `Dashboard.aspx` | Semester milestone list synchronized with the calendar. |
| **`asp:RequiredFieldValidator`** | Validation | `Login.aspx`, `ApplyLeave.aspx` | Ensures mandatory fields (username, password, leave type, dates, reason) are populated. |
| **`asp:CustomValidator`** | Validation | `ApplyLeave.aspx` | Server-side validation verifying that end date $\ge$ start date and reason length $\ge 10$ characters. |
| **`asp:ValidationSummary`** | Validation | `Login.aspx`, `ApplyLeave.aspx` | Consolidated bulleted summary of all client/server validation errors. |

---

## 📚 Practical Viva & Technical Q&A Notes

### Q1: What is the difference between Session State and Cookies in ASP.NET?
**Answer**: 
* **Session State**: Stored on the **server** (in memory by default as `InProc`). It maintains user data across multiple pages during an active session and expires automatically (e.g., after 30 minutes of inactivity or upon calling `Session.Abandon()`). Sensitive data is kept secure on the server.
* **Cookies**: Stored on the **client browser**. Persistent cookies have an explicit expiration date (`Expires = DateTime.Now.AddDays(15)`) and survive browser restarts, making them ideal for "Remember Me" features.

### Q2: How does the `DayRender` event of the `Calendar` control work?
**Answer**: The `DayRender` event fires for each individual cell as the calendar is rendered on the server. In `calAcademic_DayRender(object sender, DayRenderEventArgs e)`, `e.Day.Date` provides the current date and `e.Cell` represents the HTML table cell. We can programmatically check our event repository, inject badges via `e.Cell.Controls.Add()`, set cell background colors (`e.Cell.Style.Add`), and assign tooltips.

### Q3: Why is server-side validation using `CustomValidator` necessary for date comparisons?
**Answer**: Standard HTML inputs or client-side scripts can easily be modified or bypassed by users. `CustomValidator` with `OnServerValidate` guarantees that date parsing, logical range checks ($\text{EndDate} \ge \text{StartDate}$), and business constraints are enforced securely on the web server before modifying session state.

### Q4: What happens during `Session.Clear()` vs `Session.Abandon()`?
**Answer**:
* `Session.Clear()` (or `Session.RemoveAll()`): Removes all keys and stored values from the session dictionary but keeps the current session ID alive.
* `Session.Abandon()`: Destroys the entire session object on the server and terminates the session lifecycle.

### Q5: How do `GridView` and `DetailsView` work together for Master-Detail views?
**Answer**: `GridView` has `DataKeyNames="LeaveId"` and `AutoGenerateSelectButton="True"` (or a `CommandField`). When the user clicks "Select", the `SelectedIndexChanged` event fires. In the code-behind, `gvLeaveHistory.SelectedDataKey.Value` retrieves the selected record's ID, which is then fetched from the session list and bound to `dvLeaveDetails.DataSource` for deep row inspection.

### Q6: How does the "Remember Me" Cookie persist across browser sessions?
**Answer**: When the student checks "Remember Me", `new HttpCookie("StudentUsername", username)` is created with `cookie.Expires = DateTime.Now.AddDays(15)` and sent via `Response.Cookies.Add(cookie)`. When the browser visits `Login.aspx` again, `Request.Cookies["StudentUsername"]` is read during `Page_Load` to pre-fill the username input box.

---

## 🛠️ Tech Stack & Target Environment
- **Framework**: Microsoft .NET Framework 4.8
- **Technology**: ASP.NET Web Forms (.aspx / .aspx.cs / .designer.cs / Site.Master)
- **Language**: C#
- **Web Server**: IIS Express / IIS
- **IDE Compatibility**: Microsoft Visual Studio 2022 / Antigravity IDE
- **Styling**: Vanilla CSS3 (Custom University Theme with Google Fonts `Plus Jakarta Sans` & `JetBrains Mono`)
