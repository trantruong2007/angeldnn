using System;
using System.Web.UI;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;

namespace Angel.DNN.CareCenter
{
    public partial class Dispatch : ModuleBase, IActionable
    {
        protected string RequestedView
        {
            get { return Request.QueryString["view"] ?? ""; }
        }

        /// <summary>
        /// Specify your "Views" here
        /// </summary>
        /// <returns></returns>
        protected string GetCustomControlToLoad()
        {
            ViewNames view = ViewNames.Default;

            if (RequestedView != "")
            {
                view = Enum<ViewNames>.Parse(RequestedView);
            }

            // Specify your Views here
            switch (view)
            {
                case ViewNames.CareAvailability:
                    return "Views/CareAvailability.ascx";
                case ViewNames.UnassignedRequests:
                    return "Views/UnassignedRequests.ascx";
                default:
                    return "Views/Default.ascx";
            }
        }

        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
            string control = GetCustomControlToLoad();
            LoadCustomControl(control);
        }

        private void LoadCustomControl(string controlPath)
        {
            PortalModuleBase module = (PortalModuleBase) LoadControl(controlPath);
            if (module != null)
            {
                // load the control into the placeholder
                module.ModuleConfiguration = ModuleConfiguration;
                module.ID = System.IO.Path.GetFileNameWithoutExtension(controlPath);
                plhControl.Controls.Add(module);
            }
        }

        #region IActionable Members

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection moduleActions = new ModuleActionCollection();
                //moduleActions.Add(GetNextActionID(), "Administration", ModuleActionType.EditContent, "", "", UrlHelper.ViewUrl(ViewNames.AdminDefault), false, SecurityAccessLevel.Edit, true, false);

                return moduleActions;
            }
        }

        #endregion
    }
}