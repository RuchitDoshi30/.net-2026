<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AcademicLeaveManagement.Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Student Portal Login - Academic Calendar & Leave Management</title>
    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
</head>
<body class="login-page-bg">
    <form id="form1" runat="server">
        <div class="login-card">
            <div class="login-header">
                <div class="login-brand-icon">🎓</div>
                <h1 class="login-title">Student Portal Login</h1>
                <p class="login-subtitle">Academic Calendar & Leave Management</p>
            </div>

            <!-- Server & Validation Alerts -->
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
            
            <asp:ValidationSummary ID="valSummaryLogin" runat="server" CssClass="validation-summary-box" 
                HeaderText="Please correct the following errors:" DisplayMode="BulletList" />

            <!-- Username Field -->
            <div class="form-group">
                <label class="form-label" for="txtUsername">
                    Student Username / Enrollment ID <span class="required-star">*</span>
                </label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" 
                    placeholder="e.g. student" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                    ControlToValidate="txtUsername" Display="Dynamic" 
                    ErrorMessage="Student Username is required." 
                    CssClass="validator-error">
                    * Student username is required
                </asp:RequiredFieldValidator>
            </div>

            <!-- Password Field -->
            <div class="form-group">
                <label class="form-label" for="txtPassword">
                    Password <span class="required-star">*</span>
                </label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" 
                    TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                    ControlToValidate="txtPassword" Display="Dynamic" 
                    ErrorMessage="Password is required." 
                    CssClass="validator-error">
                    * Password is required
                </asp:RequiredFieldValidator>
            </div>

            <!-- Remember Me Cookie Checkbox -->
            <div class="form-group" style="display: flex; align-items: center; justify-content: space-between;">
                <label style="display: flex; align-items: center; cursor: pointer; font-size: 0.88rem; color: var(--text-main);">
                    <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-checkbox" />
                    <span style="margin-left: 0.4rem;">Remember me on this device (Cookie)</span>
                </label>
            </div>

            <!-- Login Action Button -->
            <div style="margin-top: 1.5rem;">
                <asp:Button ID="btnLogin" runat="server" Text="Sign In to Portal" 
                    CssClass="btn btn-primary" Width="100%" OnClick="btnLogin_Click" />
            </div>

            <!-- Practical Testing Hint -->
            <div class="login-footer-hint">
                <strong>Demo Credentials:</strong><br />
                Username: <code>student</code> &bull; Password: <code>12345</code><br />
                <span style="font-size: 0.75rem; color: var(--text-muted);">(Demonstrates Session + Persistent Cookie)</span>
            </div>
        </div>
    </form>
</body>
</html>
