# Practical 4: Online Event Registration Portal using ASP.NET Controls & Validation

## 📌 AIM
Build an **Online Event Registration Portal** using ASP.NET Core Controls, Tag Helpers, and Data Annotation Validations (`[Required]`, `[EmailAddress]`, `[Compare]`, `[RegularExpression]`, `[Range]`, `[DataType]`, `ModelState.IsValid`).

---

## 📖 Theoretical Concept Explanation

### 1. ASP.NET Controls & Tag Helpers
- **ASP.NET Form Controls**: In ASP.NET Core Razor Pages, form elements (`<input>`, `<select>`, `<textarea>`, `<button>`) interact directly with PageModel properties using **Tag Helpers**.
- **Tag Helpers** (e.g., `asp-for`, `asp-validation-for`, `asp-validation-summary`): Enable server-side code to participate in creating and rendering HTML elements in Razor files.

| Tag Helper | Purpose | HTML Rendered |
| :--- | :--- | :--- |
| `asp-for="Registration.FullName"` | Binds HTML input to model property | `<input name="Registration.FullName" id="..." value="..." />` |
| `asp-validation-for="..."` | Displays field-specific validation error | `<span class="field-validation-error">Error message</span>` |
| `asp-validation-summary="All"` | Renders a bulleted list of all validation errors | `<div class="validation-summary-errors">...</div>` |

---

### 2. Validation Attributes (Data Annotations)
Validation attributes are applied directly to Model properties in `System.ComponentModel.DataAnnotations` to enforce rules.

| Validation Attribute | Purpose / Applied To | Applied Rule in Practical 4 |
| :--- | :--- | :--- |
| **`[Required]`** | Mandates field entry | Full Name, Email, Event, Ticket Type, Date |
| **`[StringLength]`** | Sets min & max string length | Full Name (3 to 50 chars) |
| **`[EmailAddress]`** | Enforces valid email syntax | Email (`user@domain.com`) |
| **`[Compare]`** | Compares two properties | Confirm Email matches Email |
| **`[RegularExpression]`** | Pattern matching via Regex | Phone Number must be 10 digits (`^[0-9]{10}$`) |
| **`[Range]`** | Numeric / Value range boundaries | Tickets count between 1 and 10 |
| **`[DataType]`** | Specifies data type hint | `DataType.Date` for date picker |

---

### 3. Server-Side vs Client-Side Validation
- **Client-Side Validation**: Executed in the browser using jQuery Unobtrusive Validation (`_ValidationScriptsPartial`). Prevents unnecessary server round-trips when fields are invalid.
- **Server-Side Validation**: Executed in the PageModel (`ModelState.IsValid`). Guarantees security and data integrity even if JavaScript is disabled.

```csharp
if (!ModelState.IsValid)
{
    return Page(); // Re-renders form with error messages
}
```

---

## 🏗️ Model & Component Architecture

```
                 EventRegistration (Model)
                            |
   +------------------------+------------------------+
   |                        |                        |
Data Annotations      Razor View (Index.cshtml)   PageModel (Index.cshtml.cs)
- [Required]          - <form method="post">      - [BindProperty] Registration
- [EmailAddress]      - asp-for Tag Helpers       - Custom Date Validation
- [Compare]           - asp-validation-summary    - ModelState.IsValid check
- [RegularExpression] - asp-validation-for        - Ticket Calculation Logic
- [Range]             - Partial Validation Scripts
```

---

## 🛠️ Program Features & Workflow

1. **Attendee Details Input**: Collects Full Name, Email, Confirm Email, and 10-digit Mobile Number.
2. **Event Selection**: Dropdown menu for selecting events (.NET Tech Summit, AI Expo, etc.).
3. **Pass Selection & Pricing**: Ticket Type dropdown (`Standard (₹499)`, `VIP (₹1499)`, `Student Pass (₹199)`).
4. **Ticket Count**: Range-validated count selector (1–10 tickets).
5. **Real-time & Server Validation**: Validates all input constraints; blocks submission if invalid.
6. **Registration Confirmation**: Calculates total cost, generates unique Registration ID (`REG-XXXXXX`), and displays confirmation summary card.

---

## 🚀 How to Run the Program

### Prerequisites
- [.NET SDK 9.0 or higher](https://dotnet.microsoft.com/download)

### Execution Steps
1. Open terminal inside the `Practical_4` directory:
   ```bash
   cd PRACTICAL4/Practical_4
   ```
2. Run the ASP.NET Core web application:
   ```bash
   dotnet run
   ```
3. Open your browser and navigate to the local server URL displayed in terminal (e.g., `http://localhost:5000` or `http://localhost:5247`).

---

## 💻 Sample Validation & Confirmation Scenarios

### Scenario A: Validation Summary & Field Errors
- **Input**: Email = `test@dom`, Confirm Email = `other@dom`, Phone = `123`, Tickets = `15`
- **Output**:
  - Validation Summary displays errors for mismatching email, invalid phone format, and out-of-range ticket count.
  - Form submission is blocked.

### Scenario B: Successful Registration
- **Input**:
  - Name: `Ruchit Doshi`
  - Email: `ruchit@example.com`
  - Event: `Annual .NET Tech Summit 2026`
  - Pass Type: `VIP Pass (₹1499)`
  - No. of Tickets: `2`
- **Output**:
  - **Registration Reference ID**: `REG-847291`
  - **Total Amount Paid**: `₹2,998.00`
  - Styled Confirmation Card displayed on UI.

---

## 🎓 Viva Preparation Notes & Q&A

### Key Concepts & Code Map

| Concept | Code Reference | Viva Explanation |
| :--- | :--- | :--- |
| **`asp-for`** | `<input asp-for="Registration.FullName" />` | Binds HTML input element to model property; populates `id`, `name`, and current value. |
| **`asp-validation-summary`** | `<div asp-validation-summary="All"></div>` | ASP.NET Control rendering a consolidated list of all model validation errors. |
| **`[Compare]`** | `[Compare("Email")]` | Validates that `ConfirmEmail` equals `Email`. |
| **`[RegularExpression]`** | `[RegularExpression(@"^[0-9]{10}$")]` | Ensures phone number consists of exactly 10 numeric digits. |
| **`ModelState.IsValid`** | `if (!ModelState.IsValid)` | Server-side check verifying whether all data annotation rules passed. |
| **Partial Scripts** | `<partial name="_ValidationScriptsPartial" />` | Loads jQuery validation scripts for client-side instant validation. |

### Quick Viva Q&A

**Q1: What is the purpose of Data Annotations in ASP.NET?**
- Data Annotations are attributes in `System.ComponentModel.DataAnnotations` used to define validation rules, formatting, and display metadata directly on model properties.

**Q2: What is the difference between Client-Side and Server-Side Validation?**
- **Client-Side Validation** runs in the browser using JavaScript before form submission, improving UX. **Server-Side Validation** runs on the web server inside `ModelState.IsValid`, ensuring security if client-side validation is bypassed.

**Q3: How does the `Compare` attribute work?**
- `[Compare("OtherProperty")]` checks if the value of the decorated property matches the value of `OtherProperty` (e.g. `ConfirmEmail` vs `Email`).

**Q4: What are Tag Helpers in ASP.NET Core?**
- Tag Helpers are server-side components in Razor syntax (like `asp-for`, `asp-validation-for`) that generate standard HTML markup dynamically based on C# model bindings.
