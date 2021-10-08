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
        public Ring ring { get; set; }

        public ICommand insertIntoDbCmd { get; set; }


        public Components_RingViewModel()
        {
            this.ring = new Ring();
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateInput());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.Rings.Add(this.ring);
                    db.SaveChanges();
                    MessageBox.Show("Ring erfolgreich hinzugefügt");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen");
                }

            }
        }

        public bool validateInput()
        {
            if (ring.ID.ToString().Length <= 1
                || ring.OuterDiameter == 0
                || ring.Height == 0
                || ring.FillHeightMax == 0
                || ring.InnerDiameter == 0
                || ring.OuterDiameter < ring.InnerDiameter
                || (ring.HasKonus && (((ring.InnerKonusMin ?? 0) == 0) || ((ring.InnerKonusMax ?? 0) == 0) || ((ring.InnerKonusAngle ?? 0) == 0) || ((ring.KonusHeight ?? 0) == 0))))
            {
                return false;
            }

            return true;
        }
    }
}
