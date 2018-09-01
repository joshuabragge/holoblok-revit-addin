#region Namespaces
using System;
using System.Windows.Media.Imaging;
using System.Reflection;
using Autodesk.Revit.UI;
using holoblok.Data;
#endregion

/* Holo blok
 * Revit 2018 DLL Addin
 * Last updated: 2018-08-31
 * Joshua Bragge
 */ 
namespace holoblok
{
    class App : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {

            Assembly exe = Assembly.GetExecutingAssembly();
            string path = exe.Location;

            // Creates ribbon panel
            application.CreateRibbonTab(NameConstants.ribbonTabOne);
            // Creates categories in ribbon panel
            RibbonPanel ribbonOneCategoryOne = application.CreateRibbonPanel(NameConstants.ribbonTabOne, NameConstants.categoryOne);
            RibbonPanel ribbonOneCategoryTwo = application.CreateRibbonPanel(NameConstants.ribbonTabOne, NameConstants.categoryTwo);

            var ButtonManager = new ButtonManager();
            // About Us Button
            PushButton _buttonAbout = ButtonManager.GenerateButton(ribbonOneCategoryTwo, path, NameConstants.ButtonNameAbout, NameConstants.NamespaceClassAbout, NameConstants.TooltipDescriptionAbout, NameConstants.TooltipDescriptionLongAbout);
            PushButton buttonAbout = ButtonManager.AddButtonImages(_buttonAbout, NameConstants.ButtonImageMediumAbout, NameConstants.ButtonImageLargeAbout, NameConstants.ButtonImageOriginalAbout);

            // ReplaceFont Button
            PushButton _buttonReplaceFont = ButtonManager.GenerateButton(ribbonOneCategoryOne, path, NameConstants.ButtonNameReplaceFont, NameConstants.NamespaceClassReplaceFont, NameConstants.TooltipDescriptionReplaceFont, NameConstants.TooltipDescriptionLongReplaceFont);
            PushButton buttonReplaceFont = ButtonManager.AddButtonImages(_buttonReplaceFont, NameConstants.ButtonImageMediumReplaceFont, NameConstants.ButtonImageLargeReplaceFont, NameConstants.ButtonImageOriginalReplaceFont);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
