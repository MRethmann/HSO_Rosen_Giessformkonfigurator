using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
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
    /// <summary>
    /// Not in use. Could be used to avoid binding errors on RowDetails --> Additional Inner/Core Rings
    /// </summary>
    class AdditionalRingTemplateSelector : DataTemplateSelector
    {
        class DetailsTemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                CompareObject co = item as CompareObject;

                if (((ModularMold)co?.Mold).ListCoreRings.Count > 0)
                    return SingleMoldDetails;
                else
                    return null;
                

            }
            public DataTemplate MultiMoldDiscDetails { get; set; }
            public DataTemplate MultiMoldCupDetails { get; set; }
            public DataTemplate SingleMoldDetails { get; set; }
        }

    }
}
