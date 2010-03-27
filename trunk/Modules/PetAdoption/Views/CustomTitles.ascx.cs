using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebAscender.DNN.StarterModule.Views
{
    public partial class CustomTitles : ModuleBase
    {
        protected new void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);

            // Customizing the title must be done in "Page_Init" event

            // Page Title
            BasePage.Title = "My Customized PAGE Title";
            
            // Module Title
            ModuleConfiguration.ModuleTitle = "My Customized MODULE Title";
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            // YOUR CODE HERE
        }
    }
}