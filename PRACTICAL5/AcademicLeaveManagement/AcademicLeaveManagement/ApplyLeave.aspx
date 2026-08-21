<%@ Page Title="Apply Leave" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplyLeave.aspx.cs" Inherits="AcademicLeaveManagement.ApplyLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Page Header -->
    <div class="page-header">
        <h1 class="page-title">
            📝 Student Leave Application Form
        </h1>
        <p class="page-description">
            Submit an academic leave request. Demonstrates ASP.NET Rich Controls (DropDownList, RadioButtonList, CheckBoxList) and Server-Side Validation Controls.
        </p>
    </div>

    <!-- Feedback & Result Banner -->
    <asp:Label ID="lblStatusMessage" runat="server" EnableViewState="false"></asp:Label>

    <!-- ASP.NET Validation Summary Server Control -->
    <asp:ValidationSummary ID="valSummaryApply" runat="server" 
        CssClass="validation-summary-box" 
        HeaderText="Please correct the following errors before submitting:" 
        DisplayMode="BulletList" />

    <!-- Application Form Card -->
    <div class="portal-card" style="max-width: 900px; margin: 0 auto 2rem auto;">
        <div class="portal-card-header">
            <h2 class="portal-card-title">
                📋 Leave Request Form
            </h2>
            <span style="font-size: 0.82rem; color: var(--text-muted);">
                Fields marked with <span class="required-star">*</span> are mandatory
            </span>
        </div>

        <!-- Student Info Row (Read Only) -->
        <div class="form-row">
            <div class="form-col form-group">
                <label class="form-label">
                    Student Username / Applicant ID
                </label>
                <asp:TextBox ID="txtStudentUsername" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <!-- Leave Type DropDownList (Rich Web Server Control) -->
            <div class="form-col form-group">
                <label class="form-label" for="ddlLeaveType">
                    Leave Category / Type <span class="required-star">*</span>
                </label>
                <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="asp-dropdown">
                    <asp:ListItem Value="" Text="-- Select Leave Type --"></asp:ListItem>
                    <asp:ListItem Value="Casual Leave" Text="Casual Leave"></asp:ListItem>
                    <asp:ListItem Value="Medical Leave" Text="Medical Leave"></asp:ListItem>
                    <asp:ListItem Value="Personal Leave" Text="Personal Leave"></asp:ListItem>
                    <asp:ListItem Value="Emergency Leave" Text="Emergency Leave"></asp:ListItem>
                    <asp:ListItem Value="Academic Duty / OD" Text="Academic Duty / OD (On-Duty)"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvLeaveType" runat="server" 
                    ControlToValidate="ddlLeaveType" InitialValue="" 
                    ErrorMessage="Leave Type must be selected." 
                    Display="Dynamic" CssClass="validator-error">
                    * Please choose a leave category
                </asp:RequiredFieldValidator>
            </div>
        </div>

        <!-- Date Range Controls -->
        <div class="form-row">
            <!-- Start Date Input -->
            <div class="form-col form-group">
                <label class="form-label" for="txtStartDate">
                    Start Date <span class="required-star">*</span>
                </label>
                <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" 
                    ControlToValidate="txtStartDate" 
                    ErrorMessage="Start Date is required." 
                    Display="Dynamic" CssClass="validator-error">
                    * Start date is required
                </asp:RequiredFieldValidator>
            </div>

            <!-- End Date Input -->
            <div class="form-col form-group">
                <label class="form-label" for="txtEndDate">
                    End Date <span class="required-star">*</span>
                </label>
                <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" 
                    ControlToValidate="txtEndDate" 
                    ErrorMessage="End Date is required." 
                    Display="Dynamic" CssClass="validator-error">
                    * End date is required
                </asp:RequiredFieldValidator>
            </div>
        </div>

        <!-- ASP.NET CustomValidator for Server-Side Date Comparison & Business Rules -->
        <asp:CustomValidator ID="cvDateRange" runat="server" 
            ErrorMessage="End Date cannot be earlier than Start Date." 
            Display="Dynamic" CssClass="validator-error" 
            OnServerValidate="cvDateRange_ServerValidate">
            * End Date cannot be earlier than Start Date
        </asp:CustomValidator>

        <!-- Duration Mode: RadioButtonList Rich Server Control -->
        <div class="form-group" style="margin-top: 1rem;">
            <label class="form-label">
                Leave Duration / Session Mode <span class="required-star">*</span>
            </label>
            <asp:RadioButtonList ID="rblDuration" runat="server" CssClass="asp-choice-list" RepeatLayout="Table">
                <asp:ListItem Value="Full Day" Text="Full Day (Entire Working Hours)" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Half Day (Morning Session)" Text="Half Day (Morning Academic Sessions: 09:00 AM - 01:00 PM)"></asp:ListItem>
                <asp:ListItem Value="Half Day (Afternoon Session)" Text="Half Day (Afternoon Lab / Project Sessions: 01:30 PM - 05:30 PM)"></asp:ListItem>
            </asp:RadioButtonList>
        </div>

        <!-- Reason TextBox -->
        <div class="form-group">
            <label class="form-label" for="txtReason">
                Reason for Leave Application <span class="required-star">*</span>
            </label>
            <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="4" 
                CssClass="form-control" 
                placeholder="State specific reason for requested absence (minimum 10 characters)..."></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvReason" runat="server" 
                ControlToValidate="txtReason" 
                ErrorMessage="Reason for leave is mandatory." 
                Display="Dynamic" CssClass="validator-error">
                * Reason is required
            </asp:RequiredFieldValidator>
        </div>

        <!-- Supporting Documents: CheckBoxList Rich Server Control -->
        <div class="form-group">
            <label class="form-label">
                Enclosed Supporting Documents & Approvals (Optional Checklist)
            </label>
            <asp:CheckBoxList ID="cblSupportingDocs" runat="server" CssClass="asp-choice-list" RepeatLayout="Table">
                <asp:ListItem Value="Medical Certificate / Doctor's Prescription" Text="Medical Certificate / Registered Doctor's Prescription"></asp:ListItem>
                <asp:ListItem Value="Parent / Guardian Acknowledgment Note" Text="Parent / Guardian Signed Acknowledgment Letter"></asp:ListItem>
                <asp:ListItem Value="HOD / Faculty Advisor Recommendation" Text="HOD / Faculty Advisor Endorsement"></asp:ListItem>
                <asp:ListItem Value="Official Duty / Sports Participation Letter" Text="Official Duty / Inter-College Hackathon / Sports Letter"></asp:ListItem>
            </asp:CheckBoxList>
        </div>

        <!-- Action Buttons -->
        <div style="display: flex; gap: 1rem; margin-top: 2rem; border-top: 1px solid var(--border-color); padding-top: 1.5rem;">
            <asp:Button ID="btnSubmit" runat="server" Text="🚀 Submit Leave Application" 
                CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            
            <asp:Button ID="btnReset" runat="server" Text="🔄 Reset Form" 
                CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnReset_Click" />
        </div>
    </div>
</asp:Content>
