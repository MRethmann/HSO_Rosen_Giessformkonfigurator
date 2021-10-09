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
    class QuickInfoTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            CompareObject co = item as CompareObject;

            if (co?.Mold.MoldType.ToString() == "SingleMold")
                return QuickInfoSingleMold;
            else if (co?.Mold.MoldType.ToString() == "MultiMold" && co.Mold.ProductType.ToString() == "Disc")
                return QuickInfoMultiMoldDisc;
            else if (co?.Mold.MoldType.ToString() == "MultiMold" && co.Mold.ProductType.ToString() == "Cup")
                return QuickInfoMultiMoldCup;
            else
                return null;
        }
        public DataTemplate QuickInfoMultiMoldDisc { get; set; }
        public DataTemplate QuickInfoMultiMoldCup { get; set; }
        public DataTemplate QuickInfoSingleMold { get; set; }
    }
}

