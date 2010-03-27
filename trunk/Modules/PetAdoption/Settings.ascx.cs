using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace WebAscender.DNN.StarterModule
{
    public partial class Settings : DotNetNuke.Entities.Modules.ModuleSettingsBase
    {
        public override void LoadSettings()
        {
            if (!IsPostBack)
            {
                txtMySetting1.Text = Convert.ToString(ModuleSettings[ModuleSettingNames.MySetting1]);
            }
        }

        public override void UpdateSettings()
        {
            ModuleController settings = new ModuleController();

            settings.UpdateModuleSetting(ModuleId, ModuleSettingNames.MySetting1, txtMySetting1.Text);

            //refresh cache
            ModuleController.SynchronizeModule(ModuleId);
        }
    }

    /// <summary>
    /// Just a quick-and-dirty strongly typed way to set/get module settings by name
    /// </summary>
    public static class ModuleSettingNames
    {
        public const string MySetting1 = "MySetting1";
    }
}