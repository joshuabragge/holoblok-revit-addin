using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace holoblok
{
    public class ButtonManager
    {
        public PushButton GenerateButton(RibbonPanel PanelCategory, string ExecutablePath, string ButtonName, string NamespaceClass, string TooltipDescription, string TooltipDescriptionLong)
        {
            PushButtonData buttonData = new PushButtonData(ButtonName, ButtonName, ExecutablePath, NamespaceClass);
            PushButton button = PanelCategory.AddItem(buttonData) as PushButton;
            button.LongDescription = string.Format(TooltipDescriptionLong, ButtonName);
            button.ToolTip = string.Format(TooltipDescription, ButtonName);

            return button;
        }

        public PushButton AddButtonImages(PushButton button, string mediumImage, string largeImage, string originalImage)
        {
            // BitmapImage(new Uri("pack://application:,,,/holoblok-revit;component/Resources/holoblok_16x16_size.png"));
            string basePath = "pack://application:,,,/holoblok-revit;component/Resources/";
            BitmapImage ButtonImageMedium = new BitmapImage(new Uri(basePath + mediumImage));
            BitmapImage ButtonImageLarge = new BitmapImage(new Uri(basePath + largeImage));
            BitmapImage ButtonImageOriginal = new BitmapImage(new Uri(basePath + originalImage));

            button.Image = ButtonImageMedium;
            button.LargeImage = ButtonImageLarge;
            button.ToolTipImage = ButtonImageOriginal;

            return button;
        }
    }
}
