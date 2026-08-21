<%@ Page Title="Student Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AcademicLeaveManagement.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Page Header & Welcome Greeting -->
    <div class="page-header">
        <h1 class="page-title">
            👋 Hello, <asp:Label ID="lblStudentName" runat="server" Text="Student"></asp:Label>!
        </h1>
        <p class="page-description">
            Welcome to the Academic Calendar & Leave Management Portal. Monitor your academic schedule, track leave status, and submit new leave requests.
        </p>
    </div>

    <!-- Stat Metric Cards (KPIs) -->
    <div class="grid-4">
        <div class="stat-card stat-primary">
            <div class="stat-icon-wrapper">📄</div>
            <div class="stat-content">
                <span class="stat-label">Total Applied</span>
                <asp:Label ID="lblTotalLeaves" runat="server" CssClass="stat-value" Text="0"></asp:Label>
            </div>
        </div>

        <div class="stat-card stat-warning">
            <div class="stat-icon-wrapper">⏳</div>
            <div class="stat-content">
                <span class="stat-label">Pending Approval</span>
                <asp:Label ID="lblPendingLeaves" runat="server" CssClass="stat-value" Text="0"></asp:Label>
            </div>
        </div>

        <div class="stat-card stat-success">
            <div class="stat-icon-wrapper">✅</div>
            <div class="stat-content">
                <span class="stat-label">Approved Leaves</span>
                <asp:Label ID="lblApprovedLeaves" runat="server" CssClass="stat-value" Text="0"></asp:Label>
            </div>
        </div>

        <div class="stat-card stat-danger">
            <div class="stat-icon-wrapper">❌</div>
            <div class="stat-content">
                <span class="stat-label">Rejected Leaves</span>
                <asp:Label ID="lblRejectedLeaves" runat="server" CssClass="stat-value" Text="0"></asp:Label>
            </div>
        </div>
    </div>

    <!-- Two Column Dashboard Layout -->
    <div class="grid-2">
        <!-- Left Column: Upcoming Academic Schedule Widget (ListBox Demonstration) -->
        <div class="portal-card">
            <div class="portal-card-header">
                <h2 class="portal-card-title">
                    📅 Upcoming Academic Milestones
                </h2>
                <asp:HyperLink ID="lnkViewFullCalendar" runat="server" NavigateUrl="~/AcademicCalendar.aspx" CssClass="btn btn-sm btn-secondary">
                    Full Calendar &rarr;
                </asp:HyperLink>
            </div>
            <p style="font-size: 0.85rem; color: var(--text-muted); margin-bottom: 0.75rem;">
                Select any upcoming milestone below to inspect details on the rich calendar:
            </p>
            
            <!-- ASP.NET ListBox Rich Server Control Demonstration -->
            <asp:ListBox ID="lstUpcomingEvents" runat="server" CssClass="asp-listbox" 
                Rows="6" AutoPostBack="false"></asp:ListBox>
            
            <div style="margin-top: 1rem; display: flex; gap: 0.5rem; justify-content: flex-end;">
                <asp:Button ID="btnJumpToCalendar" runat="server" Text="Open in Rich Calendar" 
                    CssClass="btn btn-sm btn-primary" OnClick="btnJumpToCalendar_Click" />
            </div>
        </div>

        <!-- Right Column: Quick Leave Operations & Shortcuts -->
        <div class="portal-card">
            <div class="portal-card-header">
                <h2 class="portal-card-title">
                    ⚡ Quick Operations
                </h2>
            </div>
            
            <div style="display: flex; flex-direction: column; gap: 1rem;">
                <div style="background: #f8fafc; border: 1px solid var(--border-color); border-radius: var(--radius-md); padding: 1rem; display: flex; align-items: center; justify-content: space-between;">
                    <div>
                        <strong style="display: block; font-size: 0.95rem; color: var(--primary-dark);">Need to take time off?</strong>
                        <span style="font-size: 0.82rem; color: var(--text-muted);">Apply for medical, casual, or duty leaves with supporting documents.</span>
                    </div>
                    <asp:HyperLink ID="lnkApplyLeaveBtn" runat="server" NavigateUrl="~/ApplyLeave.aspx" CssClass="btn btn-primary btn-sm">
                        Apply Now
                    </asp:HyperLink>
                </div>

                <div style="background: #f8fafc; border: 1px solid var(--border-color); border-radius: var(--radius-md); padding: 1rem; display: flex; align-items: center; justify-content: space-between;">
                    <div>
                        <strong style="display: block; font-size: 0.95rem; color: var(--primary-dark);">Check Leave Applications</strong>
                        <span style="font-size: 0.82rem; color: var(--text-muted);">View status updates, sanction details, and administrative remarks.</span>
                    </div>
                    <asp:HyperLink ID="lnkViewHistoryBtn" runat="server" NavigateUrl="~/LeaveHistory.aspx" CssClass="btn btn-secondary btn-sm">
                        View History
                    </asp:HyperLink>
                </div>
                
                <div style="background: var(--primary-50); border: 1px dashed var(--primary-light); border-radius: var(--radius-md); padding: 0.85rem; font-size: 0.82rem; color: var(--primary-dark);">
                    💡 <strong>Tip for Practical Evaluation:</strong> Use the <em>Apply Leave</em> form to submit a new request, then visit <em>Leave History</em> to select the row and test the <em>DetailsView</em> control.
                </div>
            </div>
        </div>
    </div>

    <!-- Recent Leave Applications GridView -->
    <div class="portal-card">
        <div class="portal-card-header">
            <h2 class="portal-card-title">
                📋 Recent Leave Applications (Summary)
            </h2>
            <asp:HyperLink ID="lnkAllLeaves" runat="server" NavigateUrl="~/LeaveHistory.aspx" CssClass="btn btn-sm btn-secondary">
                View Detailed History &rarr;
            </asp:HyperLink>
        </div>

        <div class="table-container">
            <!-- ASP.NET GridView Server Control Demonstration -->
            <asp:GridView ID="gvRecentLeaves" runat="server" CssClass="asp-gridview" 
                AutoGenerateColumns="False" GridLines="None" 
                EmptyDataText="No leave applications recorded in current session.">
                <Columns>
                    <asp:BoundField DataField="LeaveId" HeaderText="Application ID" />
                    <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Duration" HeaderText="Duration" />
                    <asp:BoundField DataField="AppliedDate" HeaderText="Applied On" DataFormatString="{0:yyyy-MM-dd}" />
                    
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='<%# GetStatusBadgeCss(Eval("Status").ToString()) %>'>
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
