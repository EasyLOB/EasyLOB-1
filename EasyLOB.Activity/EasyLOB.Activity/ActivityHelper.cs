using System;

namespace EasyLOB.Activity
{
    public static class ActivityHelper
    {
        #region Methods Activity

        public static string DashboardActivity(string domain, string dashboardDirectory, string dashboardName)
        {
            // Domain-Dashboard-DashboardName
            // Domain-Dashboard-DashboardDirectory-DashboardName
            string activity = "";

            if (!String.IsNullOrEmpty(domain))
            {
                activity = activity + (activity == "" ? "" : "-") + domain;
            }

            activity = activity + (activity == "" ? "" : "-") + "Dashboard";

            if (!String.IsNullOrEmpty(dashboardDirectory))
            {
                activity = activity + (activity == "" ? "" : "-") + dashboardDirectory;
            }

            if (String.IsNullOrEmpty(dashboardName))
            {
                activity = activity + (activity == "" ? "" : "-") + dashboardName;
            }

            return activity;
        }

        public static string EntityActivity(string domain, string entity)
        {
            // Domain-Entity
            string activity = "";

            if (!String.IsNullOrEmpty(domain))
            {
                activity = activity + (activity == "" ? "" : "-") + domain;
            }

            if (!String.IsNullOrEmpty(entity))
            {
                activity = activity + (activity == "" ? "" : "-") + entity;
            }

            return activity;
        }

        public static string ReportActivity(string domain, string reportDirectory, string reportName)
        {
            // Domain-Report-ReportName
            // Domain-Report-ReportDirectory-ReportName
            string activity = "Report";

            if (!String.IsNullOrEmpty(domain))
            {
                activity = activity + (activity == "" ? "" : "-") + domain;
            }

            activity = activity + (activity == "" ? "" : "-") + "Report";

            if (!String.IsNullOrEmpty(reportDirectory))
            {
                activity = activity + (activity == "" ? "" : "-") + reportDirectory;
            }

            if (String.IsNullOrEmpty(reportName))
            {
                activity = activity + (activity == "" ? "" : "-") + reportName;
            }

            return activity;
        }

        public static string TaskActivity(string domain, string taskName)
        {
            // Domain-Task-TaskName
            string activity = "";

            if (!String.IsNullOrEmpty(domain))
            {
                activity = activity + (activity == "" ? "" : "-") + domain;
            }

            activity = activity + (activity == "" ? "" : "-") + "Report";

            if (!String.IsNullOrEmpty(taskName))
            {
                activity = activity + (activity == "" ? "" : "-") + taskName;
            }

            return activity;
        }

        #endregion Methods Activity
    }
}