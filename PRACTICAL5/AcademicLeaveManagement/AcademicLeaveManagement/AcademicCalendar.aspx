<%@ Page Title="Academic Calendar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcademicCalendar.aspx.cs" Inherits="AcademicLeaveManagement.AcademicCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .calendar-legend {
            display: flex;
            flex-wrap: wrap;
            gap: 0.75rem;
            margin-top: 1rem;
            padding: 0.75rem 1rem;
            background: #f8fafc;
            border-radius: var(--radius-md);
            border: 1px solid var(--border-color);
        }
        .legend-item {
            display: flex;
            align-items: center;
            gap: 0.4rem;
            font-size: 0.8rem;
            font-weight: 600;
        }
        .legend-dot {
            width: 12px;
            height: 12px;
            border-radius: 3px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Page Header -->
    <div class="page-header">
        <h1 class="page-title">
            📅 Academic Calendar & Examination Schedule
        </h1>
        <p class="page-description">
            Interactive institutional calendar powered by the ASP.NET <code>&lt;asp:Calendar&gt;</code> Rich Web Server Control. Click any date or event to inspect institutional milestones.
        </p>
    </div>

    <!-- Main Two-Column Calendar Layout -->
    <div class="grid-2" style="grid-template-columns: 1.6fr 1fr; align-items: start;">
        
        <!-- Left Column: Rich Calendar Control -->
        <div class="portal-card">
            <div class="portal-card-header">
                <h2 class="portal-card-title">
                    📆 Institutional Calendar View
                </h2>
                <span style="font-size: 0.82rem; color: var(--text-muted);">
                    Server-Side <code>DayRender</code> & <code>SelectionChanged</code>
                </span>
            </div>

            <!-- ASP.NET Calendar Rich Web Server Control -->
            <div class="calendar-wrapper">
                <asp:Calendar ID="calAcademic" runat="server" 
                    CssClass="asp-calendar"
                    TitleStyle-CssClass="calendar-title"
                    DayHeaderStyle-CssClass="calendar-day-header"
                    TodayDayStyle-CssClass="calendar-today"
                    SelectedDayStyle-CssClass="calendar-selected"
                    OtherMonthDayStyle-CssClass="calendar-other-month"
                    SelectionMode="Day"
                    ShowGridLines="True"
                    NextPrevFormat="FullMonth"
                    OnDayRender="calAcademic_DayRender"
                    OnSelectionChanged="calAcademic_SelectionChanged">
                </asp:Calendar>
            </div>

            <!-- Calendar Color Legend -->
            <div class="calendar-legend">
                <div class="legend-item">
                    <span class="legend-dot" style="background: #dc2626;"></span>
                    <span>Examinations</span>
                </div>
                <div class="legend-item">
                    <span class="legend-dot" style="background: #16a34a;"></span>
                    <span>Holidays</span>
                </div>
                <div class="legend-item">
                    <span class="legend-dot" style="background: #d97706;"></span>
                    <span>Submissions</span>
                </div>
                <div class="legend-item">
                    <span class="legend-dot" style="background: #9333ea;"></span>
                    <span>Practicals & Viva</span>
                </div>
                <div class="legend-item">
                    <span class="legend-dot" style="background: #2563eb;"></span>
                    <span>Milestones</span>
                </div>
            </div>
        </div>

        <!-- Right Column: Event Details Panel & Schedule ListBox -->
        <div>
            <!-- Selected Event Details Panel -->
            <div class="portal-card">
                <div class="portal-card-header">
                    <h2 class="portal-card-title">
                        🔍 Selected Date Particulars
                    </h2>
                </div>

                <asp:Panel ID="pnlEventDetails" runat="server">
                    <div style="margin-bottom: 1rem;">
                        <span style="font-size: 0.82rem; color: var(--text-muted); font-weight: 600; text-transform: uppercase;">
                            Selected Date:
                        </span>
                        <h3 style="font-size: 1.25rem; font-weight: 700; color: var(--primary-dark); margin-top: 0.2rem;">
                            <asp:Label ID="lblSelectedDate" runat="server" Text="Select a date from the calendar"></asp:Label>
                        </h3>
                    </div>

                    <asp:PlaceHolder ID="phEventContent" runat="server">
                        <div style="background: #f8fafc; border: 1px solid var(--border-color); border-radius: var(--radius-md); padding: 1.25rem; margin-bottom: 1rem;">
                            <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 0.5rem;">
                                <h4 style="font-size: 1.05rem; font-weight: 700; color: var(--text-main);">
                                    <asp:Label ID="lblEventName" runat="server"></asp:Label>
                                </h4>
                                <asp:Label ID="lblEventTypeBadge" runat="server" CssClass="badge"></asp:Label>
                            </div>
                            <p style="font-size: 0.9rem; color: var(--text-muted); margin-bottom: 0.75rem;">
                                <asp:Label ID="lblEventDescription" runat="server"></asp:Label>
                            </p>
                            <div style="border-top: 1px dashed var(--border-color); padding-top: 0.5rem; font-size: 0.8rem; color: var(--primary);">
                                📌 <strong>Academic Guideline:</strong> Attendance is strictly mandatory on exam & evaluation dates. Apply for OD / Leave in advance if eligible.
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="phNoEvent" runat="server" Visible="false">
                        <div class="alert-box alert-info">
                            ℹ️ No special academic events or holidays are scheduled on this date. Regular academic timetable applies.
                        </div>
                    </asp:PlaceHolder>

                    <div style="display: flex; gap: 0.5rem; margin-top: 1.25rem;">
                        <asp:HyperLink ID="lnkApplyForSelectedDate" runat="server" 
                            NavigateUrl="~/ApplyLeave.aspx" CssClass="btn btn-primary btn-sm" Width="100%">
                            Apply Leave for this Date &rarr;
                        </asp:HyperLink>
                    </div>
                </asp:Panel>
            </div>

            <!-- Full Academic Events ListBox (Rich Server Control) -->
            <div class="portal-card">
                <div class="portal-card-header">
                    <h2 class="portal-card-title">
                        📑 Semester Event Directory
                    </h2>
                </div>
                <p style="font-size: 0.85rem; color: var(--text-muted); margin-bottom: 0.75rem;">
                    Select an event below to jump the calendar to that month and date:
                </p>
                
                <!-- ASP.NET ListBox Control with AutoPostBack -->
                <asp:ListBox ID="lstAllEvents" runat="server" CssClass="asp-listbox" 
                    Rows="7" AutoPostBack="True" OnSelectedIndexChanged="lstAllEvents_SelectedIndexChanged">
                </asp:ListBox>
            </div>
        </div>

    </div>
</asp:Content>
