//-----------------------------------------------------------------------
// <copyright file="Components_InsertPlateViewModel.cs" company="PlaceholderCompany">
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

    class Components_InsertPlateViewModel : ObservableObject, IDataErrorInfo
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public string ToleranceOuterDiameter { get; set; }

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

        public decimal? Hc1Diameter { get; set; }

        public int? Hc1Holes { get; set; }

        public string Hc1Thread { get; set; }

        public decimal? Hc2Diameter { get; set; }

        public int? Hc2Holes { get; set; }

        public string Hc2Thread { get; set; }

        public decimal? Hc3Diameter { get; set; }

        public int? Hc3Holes { get; set; }

        public string Hc3Thread { get; set; }

        public ICommand insertIntoDbCmd { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(ID):
                        if (this.ID == 0)
                            error = "SAP-Nr. muss ausgefüllt werden.";
                        break;
                }

                return error;
            }
        }
        public string Error => string.Empty;

        public Components_InsertPlateViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    InsertPlate ep = new InsertPlate()
                    {
                        ID = this.ID,
                        Description = this.Description,
                        OuterDiameter = this.OuterDiameter,
                        ToleranceOuterDiameter = this.ToleranceOuterDiameter,
                        Height = this.Height,
                        OuterKonusMax = this.OuterKonusMax,
                        OuterKonusMin = this.OuterKonusMin,
                        OuterKonusAngle = this.OuterKonusAngle,
                        KonusHeight = this.KonusHeight,
                        HasKonus = this.HasKonus,
                        InnerKonusMax = this.InnerKonusMax,
                        InnerKonusMin = this.InnerKonusMin,
                        InnerKonusAngle = this.InnerKonusAngle,
                        HasHoleguide = this.HasHoleguide,
                        InnerDiameter = this.InnerDiameter,
                        ToleranceInnerDiameter = this.ToleranceInnerDiameter,
                        HasCore = this.HasCore,
                        Hc1Holes = this.Hc1Holes,
                        Hc1Diameter = this.Hc1Diameter,
                        Hc1Thread = this.Hc1Thread,
                        Hc2Holes = this.Hc2Holes,
                        Hc2Diameter = this.Hc2Diameter,
                        Hc2Thread = this.Hc2Thread,
                        Hc3Holes = this.Hc3Holes,
                        Hc3Diameter = this.Hc3Diameter,
                        Hc3Thread = this.Hc3Thread
                    };
                    db.InsertPlates.Add(ep);
                    db.SaveChanges();
                    MessageBox.Show("Einlegeplatte erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e + "Fehler beim Hinzufügen.");
                }

            }
        }
        private bool validateData()
        {
            return true;
        }
    }
}
