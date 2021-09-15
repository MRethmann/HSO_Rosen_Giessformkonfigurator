//-----------------------------------------------------------------------
// <copyright file="Components_BaseplateViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF.MVVM.ViewModel
{
    using Gießformkonfigurator.WPF.Core;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    class Components_BaseplateViewModel : ObservableObject, IDataErrorInfo
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal OuterKonusMax { get; set; }

        public decimal OuterKonusMin { get; set; }

        public decimal OuterKonusAngle { get; set; }

        public decimal KonusHeight { get; set; }

        private bool _HasKonus;
        public bool HasKonus 
        { 
            get { return _HasKonus; } 
            set
            {
                _HasKonus = value;
                if (_HasKonus)
                {
                    this.HasCore = false;
                    this.HasHoleguide = false;
                }
            }
        }

        public decimal? InnerKonusMax { get; set; }

        public decimal? InnerKonusMin { get; set; }

        public decimal? InnerKonusAngle { get; set; }

        private bool _HasHoleguide;
        public bool HasHoleguide 
        {
            get { return _HasHoleguide; }
            set
            {
                _HasHoleguide = value;
                if (_HasHoleguide)
                {
                    this.HasCore = false;
                    this.HasKonus = false;
                }
            }
        }

        public decimal? InnerDiameter { get; set; }

        public string ToleranceInnerDiameter { get; set; }

        private bool _HasCore;
        public bool HasCore
        {
            get { return _HasCore; }
            set
            {
                _HasCore = value;
                if (_HasCore)
                {
                    this.HasKonus = false;
                    this.HasHoleguide = false;
                }
            }
        }

        public decimal Hc1Diameter { get; set; }

        public decimal Hc1Holes { get; set; }

        public string Hc1Thread { get; set; }

        public decimal Hc2Diameter { get; set; }

        public decimal Hc2Holes { get; set; }

        public string Hc2Thread { get; set; }

        public decimal Hc3Diameter { get; set; }

        public decimal Hc3Holes { get; set; }

        public string Hc3Thread { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public string this[string columnName]
        {
            // Doesnt work yet.
            get
            {
                string error = string.Empty;

                if (columnName.ToString() == "0" | columnName == null)
                    error = columnName + " darf nicht leer oder 0 sein.";

                return error;
            }
        }
        public string Error => string.Empty;

        public Components_BaseplateViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        // TODO: Implement some kind of validation that prevents wrong input.
        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    Baseplate bp = new Baseplate() {ID = this.ID, OuterDiameter = this.OuterDiameter, Description = this.Description, Height = this.Height, InnerDiameter = this.InnerDiameter, 
                        OuterKonusMax = this.OuterKonusMax, OuterKonusMin = this.OuterKonusMin, OuterKonusAngle = this.OuterKonusAngle, KonusHeight = this.KonusHeight, InnerKonusMax = this.InnerKonusMax, 
                        InnerKonusMin = this.InnerKonusMin, InnerKonusAngle = this.InnerKonusAngle, Hc1Holes = this.Hc1Holes, Hc1Diameter = this.Hc1Diameter, Hc1Thread = this.Hc1Thread, 
                        Hc2Holes = this.Hc2Holes, Hc2Diameter = this.Hc2Diameter, Hc2Thread = this.Hc2Thread, Hc3Holes = this.Hc3Holes, Hc3Diameter = this.Hc3Diameter, Hc3Thread = this.Hc3Thread, 
                        ToleranceInnerDiameter = this.ToleranceInnerDiameter, HasCore = this.HasCore, HasKonus = this.HasKonus, HasHoleguide = this.HasHoleguide };
                    db.Baseplates.Add(bp);
                    db.SaveChanges();
                    MessageBox.Show("Grundplatte erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen.");
                }

            }
        }

        /// <summary>
        /// Validates if all required fields are filled out and activates the button if true.
        /// </summary>
        /// <returns>True if all required fields are filled.</returns>
        private bool validateData()
        {
            if (this.ID != 0
                && this.OuterDiameter != 0
                && this.Description != null
                && this.Height != 0
                && this.OuterKonusMax != 0
                && this.OuterKonusMin != 0
                && this.OuterKonusAngle != 0
                && this.KonusHeight != 0
                && ((this.HasKonus == true && ((this.InnerKonusMax != 0 || this.InnerKonusMax != null) && (this.InnerKonusMin != 0 || this.InnerKonusMin != null) && (this.InnerKonusAngle != 0 || this.InnerKonusAngle != null)))
                || (this.HasHoleguide == true && (this.InnerDiameter != 0 || this.InnerDiameter != null))
                || (this.HasCore == true)))
            {
                return true;
            }
            else
                return false;
        }

    }
}
