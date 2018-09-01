#region Namespaces
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;
using System.Reflection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
#endregion

// creates the buttons and ribbions our functions will sit in

namespace archSmarter
{
    class App : IExternalApplication
    {
        public const string Caption
          = "Export to CNC Fabrication";

        static string _namespace_prefix
          = typeof(App).Namespace + ".";

        const string _name = "Export All\r\nIn View";

        const string _class_name = "RunFontReplace";

        const string _tooltip_format
          = "Export to CNC Fabrication in {0} format";

        const string _tooltip_long_description_format
          = "Export Revit parts to CNC Fabrication in {0} format.";

        BitmapImage NewBitmapImage(
        Assembly a,
        string imageName)
            {

            Stream s = a.GetManifestResourceStream(
              _namespace_prefix + imageName);

            BitmapImage img = new BitmapImage();

            img.BeginInit();
            img.StreamSource = s;
            img.EndInit();

            return img;
        }


        public Result OnStartup(
          UIControlledApplication a)
        {
            Assembly exe = Assembly.GetExecutingAssembly();
            string path = exe.Location;

            RibbonPanel p = a.CreateRibbonPanel(Caption);

            PushButtonData d = new PushButtonData(
              _name, _name, path,
              _namespace_prefix + _class_name);

            d.ToolTip = string.Format(_tooltip_format, _name);
            d.Image = NewBitmapImage(exe, "cnc_icon_16x16_size.png");

            d.LargeImage = NewBitmapImage(exe, "cnc_icon_32x32_size.png");
            d.LongDescription = string.Format(_tooltip_long_description_format, _name);
            d.ToolTipImage = NewBitmapImage(exe, "cnc_icon_full_size.png");

            p.AddItem(d);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
