//-----------------------------------------------------------------------
// <copyright file="Mold_DiscViewModel.cs" company="PlaceholderCompany">
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
    /// Used to add SingleMoldDiscs to database.
    /// </summary>
    public class Mold_DiscViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mold_DiscViewModel"/> class.
        /// </summary>
        public Mold_DiscViewModel()
        {
            this.SingleMoldDisc = new SingleMoldDisc();
            this.InsertIntoDbCmd = new RelayCommand(param => InsertIntoDb(), param => ValidateInput());
        }

        public SingleMoldDisc SingleMoldDisc { get; set; }

        public ICommand InsertIntoDbCmd { get; set; }

        public void InsertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    db.SingleMoldDiscs.Add(this.SingleMoldDisc);
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
            if (this.SingleMoldDisc.ID.ToString().Length <= 1
                || this.SingleMoldDisc.OuterDiameter == 0
                || this.SingleMoldDisc.InnerDiameter == 0
                || this.SingleMoldDisc.Height == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
