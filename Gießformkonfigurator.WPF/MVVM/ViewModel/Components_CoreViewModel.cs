//-----------------------------------------------------------------------
// <copyright file="Components_CoreViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;

    class Components_CoreViewModel : ObservableObject
    {
        public Core Core { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public Components_CoreViewModel()
        {
            this.Core = new Core() { HasKonus = true };
            InsertIntoDbCmd = new RelayCommand(param => InsertIntoDb(), param => ValidateInput());
        }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Cores.Add(this.Core);
                    db.SaveChanges();
                    MessageBox.Show("Kern erfolgreich hinzugefügt");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen");
                }

            }
        }

        public bool ValidateInput()
        {
            if (Core.ID.ToString().Length <= 1
                || Core.OuterDiameter == 0
                || Core.Height == 0
                || Core.FillHeightMax == 0
                || (Core.HasKonus && (((Core.OuterKonusMin ?? 0) == 0) || ((Core.OuterKonusMax ?? 0) == 0) || ((Core.OuterKonusAngle ?? 0) == 0) || ((Core.KonusHeight ?? 0) == 0)))
                || (Core.HasHoleguide && ((Core.AdapterDiameter ?? 0) == 0))
                || (Core.HasGuideBolt && (((Core.GuideHeight ?? 0) == 0) || ((Core.GuideDiameter ?? 0) == 0))))
            {
                return false;
            }

            return true;
        }
    }
}
