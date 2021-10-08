//-----------------------------------------------------------------------
// <copyright file="Components_CoreViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    class Components_CoreViewModel : ObservableObject
    {
        public Core core { get; set; }
        public ICommand insertIntoDbCmd { get; set; }

        public Components_CoreViewModel()
        {
            this.core = new Core() { HasKonus = true };
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateInput());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Cores.Add(this.core);
                    db.SaveChanges();
                    MessageBox.Show("Kern erfolgreich hinzugefügt");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen");
                }

            }
        }

        public bool validateInput()
        {
            if (core.ID.ToString().Length <= 1
                || core.OuterDiameter == 0
                || core.Height == 0
                || core.FillHeightMax == 0
                || (core.HasKonus && (((core.OuterKonusMin ?? 0) == 0) || ((core.OuterKonusMax ?? 0) == 0) || ((core.OuterKonusAngle ?? 0) == 0)))
                || (core.HasGuideBolt && ((core.AdapterDiameter ?? 0) == 0))
                || (core.HasHoleguide && (((core.GuideHeight ?? 0) == 0) || ((core.GuideDiameter ?? 0) == 0))))
            {
                return false;
            }

            return true;
        }
    }
}
