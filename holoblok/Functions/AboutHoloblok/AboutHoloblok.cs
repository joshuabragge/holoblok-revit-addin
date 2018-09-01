using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace holoblok.revit.addindll.about
{
    [Transaction(TransactionMode.Manual)]
    public class AboutHoloblok : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Application app = commandData.Application.Application;
            Document activeDoc = commandData.Application.ActiveUIDocument.Document;

            // Creates a Revit task dialog to communicate information to the user.
            TaskDialog mainDialog = new TaskDialog("holo-blok");
            mainDialog.MainInstruction = "what's in the blok?";
            mainDialog.MainContent =
                    "holo-blok is a technology service company. The idea behind holo-blok is simple:"
                    + " connect any two points (a need and a solution) with a straight line of expertise."
                    + "Do it well and do it fast.";

            // Add commmandLink options to task dialog
            mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Contact");
            mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Build Version");

            // Set common buttons and default button. If no CommonButton or CommandLink is added,
            // task dialog will show a Close button by default
            mainDialog.CommonButtons = TaskDialogCommonButtons.Close;
            mainDialog.DefaultButton = TaskDialogResult.Close;

            // Set footer text. Footer text is usually used to link to the help document.
            //mainDialog.FooterText = "" + "Click here for the Revit API Developer Center";

            TaskDialogResult tResult = mainDialog.Show();

            // If the user clicks the first command link, a simple Task Dialog 
            // with only a Close button shows information about the Revit installation. 
            if (TaskDialogResult.CommandLink1 == tResult)
            {
                TaskDialog dialog_CommandLink1 = new TaskDialog("Contact");
                dialog_CommandLink1.MainInstruction =
                        "find out more at" + " "
                        + "http://holo-blok.com/";

                dialog_CommandLink1.Show();
            }

            // If the user clicks the second command link, a simple Task Dialog 
            // created by static method shows information about the active document
            else if (TaskDialogResult.CommandLink2 == tResult)
            {
                TaskDialog.Show("Build Version", 
                    "Version 1.00" + "\n" 
                    + "Complied Dec. 4 2017" + "\n" 
                    + "josh@holo-blok.com");
            }

            return Result.Succeeded;
        }
    }
}
