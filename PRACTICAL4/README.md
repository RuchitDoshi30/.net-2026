# Practical 4: CampusFest 2026 - College Event Registration Portal 🎓

## 📌 Overview
**CampusFest 2026** is a modern, responsive, light-themed **College Event Registration Portal** built with **ASP.NET Core Razor Pages (.NET 9)**. It features robust client-side & server-side form validations using Data Annotations, dynamic event track switching, duplicate registration prevention, and a **fully dynamic QRCoder-based digital event pass system**.

---

## 🎨 Key Features & Architecture

- **Light Glassmorphism Aesthetic**: Custom University CSS theme styled with curated modern palettes, Google Fonts (`Plus Jakarta Sans`, `Inter`, `JetBrains Mono`), rounded section cards, soft elevation, and responsive layouts.
- **Robust Multi-Layer Validation**:
  - **Data Annotations**: `[Required]`, `[StringLength]`, `[RegularExpression]`, `[EmailAddress]`, `[Compare]`, `[Range]`, `[Url]`.
  - **Server-Side ModelState Rules**: Automatic string input trimming, thread-safe duplicate enrollment check via `HashSet<string>`, and conditional category clearing (`ModelState.Remove`) depending on track selection (Technical vs. Non-Technical).
  - **Client-Side Unobtrusive Validation**: jQuery Validation & Unobtrusive scripts for real-time instant user feedback.
- **Dynamic QRCoder Ticketing System**:
  - Automatically generates a unique Pass ID: `CAMPUS-2026-XXXXXX`.
  - Encodes a secure, dynamic verification URL into a PNG Base64 QR code using `QRCoder` (ECC Level Q).
  - Scanned QR codes open a mobile-friendly digital event ticket view (`/Ticket/View?id=CAMPUS-2026-XXXXXX`) containing full student and event particulars, expiry indicators, verification badges, print features, and image downloads.
  - Returns a dedicated mobile error view for invalid or non-existent ticket IDs.

---

## 📁 Project Structure

```text
PRACTICAL4/
├── Practical_4/
│   ├── Models/
│   │   ├── EventRegistration.cs      # Core Form Input Data Model & Annotations
│   │   └── TicketPass.cs             # Digital Ticket Pass Model & Thread-Safe TicketStore
│   ├── Pages/
│   │   ├── Index.cshtml              # Main Registration Form & Pass View
│   │   ├── Index.cshtml.cs           # PageModel Server-Side Validation & Logic
│   │   ├── Ticket/
│   │   │   ├── View.cshtml           # Mobile-Friendly QR Ticket View & Error Card
│   │   │   └── View.cshtml.cs        # Ticket Fetch & Validation Handler
│   │   └── Shared/
│   │       ├── _Layout.cshtml        # University Layout & Typography Fonts
│   │       └── _ValidationScriptsPartial.cshtml
│   ├── wwwroot/
│   │   └── css/
│   │       └── site.css              # Custom Light Theme Glassmorphic CSS System
│   ├── Properties/
│   │   └── launchSettings.json       # Kestrel Server Port & Network Binding Configuration
│   └── Practical_4.csproj            # .NET 9 Project File with QRCoder Dependency
└── README.md
```

---

## 🚀 How to Run the Project

### Local Development Server
1. Open PowerShell or Command Prompt in the `PRACTICAL4` folder:
   ```bash
   cd Practical_4
   dotnet restore
   dotnet run
   ```
2. Open your web browser at: **`http://localhost:5051`**

### Mobile Phone Testing (Local Network)
1. Ensure your laptop and phone are on the same Wi-Fi.
2. Find your laptop's IP address (`ipconfig` in terminal, e.g., `10.80.7.223`).
3. Open **`http://10.80.7.223:5051`** on your phone or laptop.
4. Scanning the generated QR code with your phone camera will load the ticket view on your phone!

---

## 📚 Viva & Technical Q&A Notes

### Q1: What are Data Annotations in ASP.NET Core?
**Answer**: Data Annotations are attributes in `System.ComponentModel.DataAnnotations` applied to model properties (e.g. `[Required]`, `[RegularExpression]`, `[EmailAddress]`). They enforce validation rules both on the client side (injected into HTML `data-val-*` attributes) and on the server side via `ModelState.IsValid`.

### Q2: Why is Server-Side Validation mandatory even if Client-Side Validation exists?
**Answer**: Client-side validation runs in the user's browser (JavaScript) and can easily be bypassed or disabled by malicious users. Server-side validation (`ModelState.IsValid` in C#) acts as the final secure firewall ensuring corrupted or invalid data never enters the system.

### Q3: How does Conditional Validation work in ASP.NET Core Razor Pages?
**Answer**: When a single form has radio options (e.g. Technical vs. Non-Technical events), hidden fields from the unselected track would normally fail `ModelState.IsValid`. In `Index.cshtml.cs`, we conditionally execute `ModelState.Remove("Registration.NonTechnicalEvent")` before checking `ModelState.IsValid`, allowing clean dynamic validation.

### Q4: How is the QR Code generated dynamically?
**Answer**: We use the `QRCoder` NuGet package with Error Correction Level Q (`ECCLevel.Q`). It converts the unique ticket verification URL (`http://domain/Ticket/View?id=CAMPUS-2026-XXXXXX`) into a PNG byte array, which is then encoded into a Data URI string (`data:image/png;base64,...`) and rendered directly inside `<img src="@Model.Ticket.QrCodeBase64">`.

### Q5: Why shouldn't sensitive user data be stored inside the QR code directly?
**Answer**: Placing plain text sensitive data (such as passwords, mobile numbers, or emails) inside a QR code poses a security risk because anyone scanning the QR code with a basic scanner can read it. The correct approach is to place only a unique URL reference (`https://domain/Ticket/View?id=ID`) inside the QR code, requiring server-side validation to fetch particulars.

### Q6: How does the application prevent duplicate registrations?
**Answer**: In `Index.cshtml.cs`, a thread-safe `HashSet<string>` (`RegisteredEnrollments`) maintains registered enrollment numbers. During form submission, the system checks if the enrollment number already exists and attaches a `ModelState.AddModelError` if a duplicate is detected.

---

## 🛠️ Tech Stack & Dependencies
- **Framework**: ASP.NET Core Razor Pages (.NET 9)
- **Language**: C# 13, HTML5, Vanilla CSS3, JavaScript (ES6)
- **NuGet Packages**:
  - `QRCoder` (v1.8.0)
- **Styling & Icons**: Bootstrap 5, Bootstrap Icons, Google Fonts (`Plus Jakarta Sans`, `Inter`, `JetBrains Mono`)
