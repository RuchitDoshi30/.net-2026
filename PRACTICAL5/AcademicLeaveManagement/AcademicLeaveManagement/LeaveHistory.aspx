<%@ Page Title="Leave History" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeaveHistory.aspx.cs" Inherits="AcademicLeaveManagement.LeaveHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .details-action-bar {
            display: flex;
            gap: 0.75rem;
            margin-top: 1.25rem;
            padding: 1rem;
            background: #f8fafc;
            border-radius: var(--radius-md);
            border: 1px dashed var(--border-color);
            align-items: center;
            flex-wrap: wrap;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Page Header -->
    <div class="page-header">
        <h1 class="page-title">
            📋 Leave Application History & Tracking
        </h1>
        <p class="page-description">
            View submitted applications, monitor real-time approval status, and inspect full records using ASP.NET <code>&lt;asp:GridView&gt;</code> and <code>&lt;asp:DetailsView&gt;</code> Rich Controls.
        </p>
    </div>

    <!-- Status Alert Notification -->
    <asp:Label ID="lblHistoryMessage" runat="server" EnableViewState="false"></asp:Label>

    <!-- Filter & Summary Bar -->
    <div class="portal-card" style="padding: 1.25rem 1.5rem; margin-bottom: 1.5rem;">
        <div style="display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap; gap: 1rem;">
            <div style="display: flex; align-items: center; gap: 0.75rem;">
                <label style="font-weight: 700; font-size: 0.88rem; color: var(--text-main);">
                    Filter by Status:
                </label>
                <!-- ASP.NET DropDownList with AutoPostBack -->
                <asp:DropDownList ID="ddlFilterStatus" runat="server" CssClass="asp-dropdown" 
                    AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged" Width="180px">
                    <asp:ListItem Value="ALL" Text="All Applications"></asp:ListItem>
                    <asp:ListItem Value="Pending" Text="⏳ Pending Only"></asp:ListItem>
                    <asp:ListItem Value="Approved" Text="✅ Approved Only"></asp:ListItem>
                    <asp:ListItem Value="Rejected" Text="❌ Rejected Only"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div>
                <asp:HyperLink ID="lnkNewLeave" runat="server" NavigateUrl="~/ApplyLeave.aspx" CssClass="btn btn-primary btn-sm">
                    + Apply New Leave
                </asp:HyperLink>
            </div>
        </div>
    </div>

    <!-- Main GridView Table Card -->
    <div class="portal-card">
        <div class="portal-card-header">
            <h2 class="portal-card-title">
                📑 Applications Record (GridView Control)
            </h2>
            <span style="font-size: 0.82rem; color: var(--text-muted);">
                Click <strong>"Select"</strong> on any row to inspect complete details
            </span>
        </div>

        <div class="table-container">
            <!-- ASP.NET GridView Rich Web Server Control -->
            <asp:GridView ID="gvLeaveHistory" runat="server" 
                CssClass="asp-gridview" 
                AutoGenerateColumns="False" 
                DataKeyNames="LeaveId"
                GridLines="None"
                SelectedRowStyle-CssClass="selected-row"
                OnSelectedIndexChanged="gvLeaveHistory_SelectedIndexChanged"
                EmptyDataText="No leave applications found matching the selected criteria.">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="🔍 Select" 
                        ButtonType="Link" HeaderText="Action" />
                    
                    <asp:BoundField DataField="LeaveId" HeaderText="Application ID" />
                    <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Duration" HeaderText="Duration" />
                    <asp:BoundField DataField="AppliedDate" HeaderText="Applied On" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                    
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

    <!-- Selected Application DetailsView Card -->
    <asp:Panel ID="pnlDetails" runat="server" Visible="false" CssClass="portal-card">
        <div class="portal-card-header">
            <h2 class="portal-card-title">
                🔍 Application Detailed View (DetailsView Control)
            </h2>
            <asp:Button ID="btnCloseDetails" runat="server" Text="✕ Close Details" 
                CssClass="btn btn-secondary btn-sm" OnClick="btnCloseDetails_Click" CausesValidation="false" />
        </div>

        <p style="font-size: 0.85rem; color: var(--text-muted); margin-bottom: 1rem;">
            Full application particulars bound to the ASP.NET <code>&lt;asp:DetailsView&gt;</code> control:
        </p>

        <!-- ASP.NET DetailsView Rich Web Server Control -->
        <asp:DetailsView ID="dvLeaveDetails" runat="server" 
            CssClass="asp-detailsview" 
            AutoGenerateRows="False" 
            GridLines="None">
            <Fields>
                <asp:BoundField DataField="LeaveId" HeaderText="Application ID" />
                <asp:BoundField DataField="Username" HeaderText="Applicant Username" />
                <asp:BoundField DataField="LeaveType" HeaderText="Leave Category" />
                <asp:BoundField DataField="StartDate" HeaderText="Leave Start Date" DataFormatString="{0:dddd, MMMM dd, yyyy}" />
                <asp:BoundField DataField="EndDate" HeaderText="Leave End Date" DataFormatString="{0:dddd, MMMM dd, yyyy}" />
                <asp:BoundField DataField="Duration" HeaderText="Session Duration" />
                <asp:BoundField DataField="Reason" HeaderText="Applicant Reason" />
                <asp:BoundField DataField="SupportingInformation" HeaderText="Enclosed Documents" />
                <asp:BoundField DataField="AppliedDate" HeaderText="Submission Timestamp" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:TemplateField HeaderText="Current Status">
                    <ItemTemplate>
                        <span class='<%# GetStatusBadgeCss(Eval("Status").ToString()) %>'>
                            <%# Eval("Status") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>

        <!-- Faculty Workflow / Practical Status Simulation Panel -->
        <div class="details-action-bar">
            <div style="flex: 1;">
                <strong style="font-size: 0.88rem; color: var(--primary-dark); display: block;">
                    ⚡ Practical Viva Demonstration & Status Simulation:
                </strong>
                <span style="font-size: 0.8rem; color: var(--text-muted);">
                    Simulate faculty advisor approval or rejection actions directly in Session state:
                </span>
            </div>
            <div style="display: flex; gap: 0.5rem;">
                <asp:Button ID="btnSimulateApprove" runat="server" Text="✔ Approve Leave" 
                    CssClass="btn btn-success btn-sm" OnClick="btnSimulateApprove_Click" />
                
                <asp:Button ID="btnSimulateReject" runat="server" Text="✖ Reject Leave" 
                    CssClass="btn btn-danger btn-sm" OnClick="btnSimulateReject_Click" />
                
                <asp:Button ID="btnSimulatePending" runat="server" Text="⏳ Mark as Pending" 
                    CssClass="btn btn-secondary btn-sm" OnClick="btnSimulatePending_Click" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
