using Gießformkonfigurator.WPF.MVVM.Model.Logic;
using System.Windows;
using System.Windows.Controls;

namespace Gießformkonfigurator.WPF.MVVM.View.DataTemplates
{
    class RowDetailsTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            CompareObject co = item as CompareObject;

            if (co.Mold.moldType.ToString() == "MultiMold" && co.Mold.productType.ToString() == "Disc")
                return MultiMoldDiscTemplate;
            else if (co.Mold.moldType.ToString() == "MultiMold" && co.Mold.productType.ToString() == "Cup")
                return MultiMoldCupTemplate;
            else if (co.Mold.moldType.ToString() == "SingleMold" && co.Mold.productType.ToString() == "Disc")
                return SingleMoldDiscTemplate;
            else if (co.Mold.moldType.ToString() == "SingleMold" && co.Mold.productType.ToString() == "Cup")
                return SingleMoldCupTemplate;
            else
                return null;
        }
        public DataTemplate MultiMoldDiscTemplate { get; set; }
        public DataTemplate SingleMoldDiscTemplate { get; set; }
        public DataTemplate MultiMoldCupTemplate { get; set; }
        public DataTemplate SingleMoldCupTemplate { get; set; }
    }
}

