using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Log.EventLog;
using EntitySpaces.Interfaces;

namespace Angel.DNN.CareCenter
{
    public class ModuleBase : PortalModuleBase
    {
        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                InitEntitySpacesConnection();
            }
        }

        protected void Page_Load(System.Object sender, System.EventArgs e)
        {
        }
      
        public DotNetNuke.Framework.CDefault BasePage
        {
            get { return (DotNetNuke.Framework.CDefault) this.Page; }
        }

        public bool IsUserLoggedIn
        {
            get { return UserId > 0; }
        }

        /// <summary>
        /// Base web path to the module's images folder
        /// </summary>
        public string ModuleImagePath
        {
            get { return ModuleBaseWebPath + "images/"; }
        }

        public string ModuleBaseWebPath
        {
            get { return string.Format("/DesktopModules/{0}/", ModuleConfiguration.FolderName); }
        }

        public string ModuleBaseFilePath
        {
            get { return string.Format("{1}{0}DesktopModules{0}{2}{0}", Path.DirectorySeparatorChar, Request.PhysicalApplicationPath, ModuleConfiguration.FolderName); }
        }
        
        protected void RegisterJavascript(string fullPath)
        {
            HtmlGenericControl script = new HtmlGenericControl("script");
            script.Attributes.Add("type", "text/javascript");
            script.Attributes.Add("src", fullPath);

            Page.Header.Controls.Add(script);
        }

        protected void RegisterCSS(string fullPath, string mediaType)
        {
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("media", mediaType);
            cssLink.Attributes.Add("href", fullPath);

            Page.Header.Controls.Add(cssLink);
        }        

        protected void LogMessageToEventLog(string msg)
        {
            EventLogController eventLog = new EventLogController();
            eventLog.AddLog(ModuleConfiguration.ModuleName, msg, PortalSettings, -1, EventLogController.EventLogType.ADMIN_ALERT);
        }

        #region EntitySpaces Connection Logic

        protected void InitEntitySpacesConnection()
        {
            if (esConfigSettings.ConnectionInfo.Default != "SiteSqlServer")
            {
                esConfigSettings ConnectionInfoSettings = esConfigSettings.ConnectionInfo;
                foreach (esConnectionElement connection in ConnectionInfoSettings.Connections)
                {
                    //if there is a SiteSqlServer in es connections set it default
                    if (connection.Name == "SiteSqlServer")
                    {
                        esConfigSettings.ConnectionInfo.Default = connection.Name;
                        return;
                    }
                }

                //no SiteSqlServer found grab dnn cnn string and create
                string dnnConnection = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;

                // Manually register a connection
                esConnectionElement conn = new esConnectionElement();
                conn.ConnectionString = dnnConnection;
                conn.Name = "SiteSqlServer";
                conn.Provider = "EntitySpaces.SqlClientProvider";
                conn.ProviderClass = "DataProvider";
                conn.SqlAccessType = esSqlAccessType.DynamicSQL;
                conn.ProviderMetadataKey = "esDefault";
                conn.DatabaseVersion = "2005";

                // Assign the Default Connection
                esConfigSettings.ConnectionInfo.Connections.Add(conn);
                esConfigSettings.ConnectionInfo.Default = "SiteSqlServer";

                // Register the Loader
                esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();
            }
        }

        #endregion
    }
}