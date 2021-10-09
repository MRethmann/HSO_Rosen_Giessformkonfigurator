using Gießformkonfigurator.WPF.MVVM.Model.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gießformkonfigurator.WPF.MVVM.View.DataTemplates
{
    class DetailsTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            CompareObject co = item as CompareObject;

            if (co?.Mold.MoldType.ToString() == "SingleMold")
                return SingleMoldDetails;
            else if (co?.Mold.MoldType.ToString() == "MultiMold" && co.Mold.ProductType.ToString() == "Disc")
                return MultiMoldDiscDetails;
            else if (co?.Mold.MoldType.ToString() == "MultiMold" && co.Mold.ProductType.ToString() == "Cup")
                return MultiMoldCupDetails;
            else
                return null;
            
        }
        public DataTemplate MultiMoldDiscDetails { get; set; }
        public DataTemplate MultiMoldCupDetails { get; set; }
        public DataTemplate SingleMoldDetails { get; set; }
    }
}
