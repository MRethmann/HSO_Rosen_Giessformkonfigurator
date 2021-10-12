//-----------------------------------------------------------------------
// <copyright file="Components_RingViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using System;
    using System.Windows;
    using System.Windows.Input;

    class Components_RingViewModel
    {
        public Ring Ring { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public Components_RingViewModel()
        {
            this.Ring = new Ring();
            InsertIntoDbCmd = new RelayCommand(param => InsertIntoDb(), param => ValidateInput());
        }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Rings.Add(this.Ring);
                    db.SaveChanges();
                    MessageBox.Show("Ring erfolgreich hinzugefügt");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen");
                }

            }
        }

        public bool ValidateInput()
        {
            if (Ring.ID.ToString().Length <= 1
                || Ring.OuterDiameter == 0
                || Ring.Height == 0
                || Ring.FillHeightMax == 0
                || Ring.InnerDiameter == 0
                || Ring.OuterDiameter < Ring.InnerDiameter
                || (Ring.HasKonus && (((Ring.InnerKonusMin ?? 0) == 0) || ((Ring.InnerKonusMax ?? 0) == 0) || ((Ring.InnerKonusAngle ?? 0) == 0) || ((Ring.KonusHeight ?? 0) == 0))))
            {
                return false;
            }

            return true;
        }
    }
}
