using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Services.Exceptions;

namespace WebAscender.DNN.StarterModule
{
    public class UrlHelper
    {
        public static string UrlForView(ViewNames view)
        {
            return UrlForView(view, new string[] { });
        }

        public static string UrlForView(ViewNames view, string csvParams)
        {
            return UrlForView(view, csvParams.Split(','));
        }

        public static string UrlForView(ViewNames view, string[] queryParams)
        {
            List<string> paramList = new List<string>(queryParams);
            paramList.Add("view=" + view);

            return Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", paramList.ToArray());
        }

        public static string UrlForActiveTab()
        {
            return Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
        }

        public static string UrlForActiveTab(string csvParams)
        {
            return Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "", csvParams.Split(','));
        }

        public static string UrlForTab(int tabId)
        {
            return Globals.NavigateURL(tabId);
        }

        public static string UrlForTab(int tabId, string csvParams)
        {
            return Globals.NavigateURL(tabId, "", csvParams.Split(','));
        }

        public static string UrlForTabPath(string tabPath)
        {
            return UrlForTabPath(tabPath, "");
        }

        public static string UrlForTabPath(string tabPath, string csvParams)
        {
            TabController tabs = new TabController();
            List<TabInfo> tabInfos = new List<TabInfo>((TabInfo[])tabs.GetAllTabs().ToArray(typeof(TabInfo)));
            foreach (TabInfo info in tabInfos)
            {
                if (info.TabPath == tabPath && !info.IsDeleted)
                {
                    return Globals.NavigateURL(info.TabID, "", csvParams.Split(','));
                }
            }

            return "";
        }

        public static string UrlForViewFriendly(ViewNames view, string csvParams, string friendlyPagePath)
        {
            return UrlForTabFriendly(PortalSettings.ActiveTab.TabID, string.Format("view={0},{1}", view, csvParams), friendlyPagePath);
        }

        public static string UrlForTabFriendly(int tabId, string csvParams, string friendlyPagePath)
        {
            string tabUrl = Globals.ApplicationURL(tabId);
            TabInfo tabInfo = new TabController().GetTab(tabId, PortalSettings.PortalId, false);
            string tabUrlWithParams = tabUrl + ConvertCsvParamsToAmpersandParams(csvParams);

                try
                {
                    if (!string.IsNullOrEmpty(friendlyPagePath))
                    {
                        string pageName = friendlyPagePath.Trim();

                        // trim leading and trailing "/"
                        pageName = pageName.TrimStart('/').TrimEnd('/');

                        pageName = StringHelper.FormatForUrl(pageName, true);

                        if(!pageName.EndsWith(".aspx"))
                        {
                            pageName += ".aspx";
                        }
                        //string pageName = FormatForUrl(cityStateZip) + "/" + FormatForUrl(member.BusinessName) + ".aspx";                        

                        return Globals.FriendlyUrl(tabInfo, tabUrlWithParams, pageName, PortalSettings);
                    }
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);

                    return Globals.FriendlyUrl(tabInfo, tabUrlWithParams, PortalSettings);
                }
                return Globals.FriendlyUrl(tabInfo, tabUrlWithParams, PortalSettings);
        }

        private static string ConvertCsvParamsToAmpersandParams(string csvParams)
        {
            if (!string.IsNullOrEmpty(csvParams))
            {
                return "&" + csvParams.Replace(',', '&');
            }
            return "";
        }

        private static PortalSettings PortalSettings
        {
            get
            {
                return PortalController.GetCurrentPortalSettings();
            }
        }
    }
}
