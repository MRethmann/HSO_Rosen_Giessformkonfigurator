//-----------------------------------------------------------------------
// <copyright file="Search_MainView.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.View
{
    using System.Windows;
    using System.Windows.Controls;
    using Gießformkonfigurator.WPF.MVVM.Model.Logic;
    using Gießformkonfigurator.WPF.MVVM.ViewModel;

    /// <summary>
    /// Interaktionslogik für AnsichtZwei.xaml
    /// </summary>
    public partial class Search_MainView : UserControl
    {
        public Search_MainView()
        {
            InitializeComponent();
            DataContext = new Search_MainViewModel();
        }

    }

    public class MyCustomRowDetailsTemplateSelector : DataTemplateSelector
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
