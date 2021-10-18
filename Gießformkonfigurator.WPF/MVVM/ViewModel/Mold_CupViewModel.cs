//-----------------------------------------------------------------------
// <copyright file="Mold_CupViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Giessformkonfigurator.WPF.MVVM.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using Giessformkonfigurator.WPF.Core;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;

    /// <summary>
    /// Used to add SingleMoldCups to database.
    /// </summary>
    public class Mold_CupViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mold_CupViewModel"/> class.
        /// </summary>
        public Mold_CupViewModel()
        {
            this.SingleMoldCup = new SingleMoldCup();
            this.InsertIntoDbCmd = new RelayCommand(param => InsertIntoDb(), param => ValidateInput());
        }

        public SingleMoldCup SingleMoldCup { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.SingleMoldCups.Add(this.SingleMoldCup);
                    db.SaveChanges();
                    MessageBox.Show("Gießform erfolgreich hinzugefügt");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen");
                }
            }
        }

        public bool ValidateInput()
        {
            if (this.SingleMoldCup.ID.ToString().Length <= 1
                || this.SingleMoldCup.OuterDiameter == 0
                || this.SingleMoldCup.InnerDiameter == 0
                || this.SingleMoldCup.Height == 0)
            {
                return false;
            }

            return true;
        }
    }
}
