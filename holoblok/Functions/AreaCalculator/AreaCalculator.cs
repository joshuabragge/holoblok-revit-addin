using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;

namespace holoblok.Functions.AreaCalculator
{
    [Autodesk.Revit.Attributes.TransactionAttribute(TransactionMode.Manual)]
    public partial class AreaCalculator : IExternalCommand
    {
        public static List<string> roomNames;
        public static List<string> levelNames;
        public static List<string> areaType;
        public static List<string> areaValue;

        public Result Execute(
              ExternalCommandData commandData,
              ref string message,
              ElementSet elements)
        {

            return AreaCalculator.ExecuteCalculations(commandData);
        }

        public static Result ExecuteCalculations(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<string> roomNames = new List<string>();
            List<string> areaType = new List<string>();
            List<string> areaValue = new List<string>();
            List<string> levelNames = new List<string>();
            List<string> areaValueCalc = new List<string>();

            using (Transaction t = new Transaction(doc, "Turn on volume calculation"))
            {
                t.Start();
                AreaVolumeSettings settings = AreaVolumeSettings.GetAreaVolumeSettings(doc);
                settings.ComputeVolumes = true;
                t.Commit();
            }

            // setup spacial properties
            SpatialElementBoundaryOptions sebOptions = new SpatialElementBoundaryOptions();

            

            // get all rooms
            FilteredElementCollector room_collector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Rooms).WhereElementIsNotElementType();
            IList<ElementId> room_eids = room_collector.ToElementIds() as IList<ElementId>;
            foreach (ElementId eid in room_eids)
            {
                Room room = doc.GetElement(eid) as Room;
                {
                    // grab room spacial calculations
                    sebOptions.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Center;
                    SpatialElementGeometryCalculator centerCalc = new SpatialElementGeometryCalculator(doc, sebOptions);
                    SpatialElementGeometryResults center = centerCalc.CalculateSpatialElementGeometry(room);

                    sebOptions.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;
                    SpatialElementGeometryCalculator finishCalc = new SpatialElementGeometryCalculator(doc, sebOptions);
                    SpatialElementGeometryResults finish = finishCalc.CalculateSpatialElementGeometry(room);

                    // get room properties
                    roomNames.Add(room.Name);
                    areaType.Add("Center");
                    levelNames.Add(room.Level.Name);
                    areaValue.Add(center.GetGeometry().SurfaceArea.ToString());

                    TaskDialog.Show("Revit", room.Name + room.Level.Name + center.GetGeometry().SurfaceArea.ToString());

                    
                    roomNames.Add(room.Name);
                    areaType.Add("Finish");
                    levelNames.Add(room.Level.Name);
                    areaValue.Add(finish.GetGeometry().SurfaceArea.ToString());

                    TaskDialog.Show("Revit", room.Name + room.Level.Name + finish.GetGeometry().SurfaceArea.ToString());


                }
            }

           


           
            
           
            return Result.Succeeded;
        }
    }
}
             
