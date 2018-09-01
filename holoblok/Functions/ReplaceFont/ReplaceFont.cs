#region Namespaces
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using System.Diagnostics;
using Autodesk.Revit.Attributes;

#endregion

namespace font
{
    [TransactionAttribute(TransactionMode.Manual)]
    public partial class ReplaceFont : IExternalCommand
    {
        public Result Execute(
              ExternalCommandData commandData,
              ref string message,
              ElementSet elements)
        {
        
            return ReplaceFont.RunFontReplace(commandData);
        }

        public static Result RunFontReplace(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            using (frmSelectFonts curForm = new frmSelectFonts(doc))
            {
                //show form
                curForm.ShowDialog();

                if (curForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    //if Dialog box is closed
                }
                else
                {
                        if ((curForm.getNewFont() ?? curForm.getOldFont()) != null)
                        {
                            //replace fonts

                            string oldFont = curForm.getOldFont();
                            string newFont = curForm.getNewFont();

                            int counter = 0;

                            //get all text styles in current model
                            FilteredElementCollector textStyleCol = new FilteredElementCollector(doc);
                            textStyleCol.OfClass(typeof(TextNoteType));

                            //loop through text styles and look for matching font
                            using (Transaction curTrans = new Transaction(doc, "Replace Fonts"))
                            {
                                if (curTrans.Start() == TransactionStatus.Started)
                                {

                                    foreach (TextNoteType curType in textStyleCol)
                                    {
                                        Debug.Print(curType.Name);

                                        //get font
                                        string curFont = functions.getParameterValue((Element)curType, "Text Font");

                                        if (curFont == oldFont)
                                        {
                                            Debug.Print("Found matching font");

                                            //do something
                                            functions.setParameterValueString((Element)curType, "Text Font", newFont);

                                            //increment counter
                                            counter = counter + 1;
                                        }

                                    }
                                }

                                //commit changes
                                curTrans.Commit();

                                //alert user
                                if (counter == 1)
                                {
                                    TaskDialog.Show("Complete", "Replaced font in " + counter + " text style.");
                                }
                                else
                                {
                                    TaskDialog.Show("Complete", "Replaced font in " + counter + " text styles.");
                                }
                            }
                        }
                       else
                        {
                            TaskDialog.Show("Error", "Please select a new font.");
                        }
                    }
                }
            return Result.Succeeded;
        }
            
        }

	
}
