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

    class Components_CoreViewModel : ObservableObject, IDataErrorInfo
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public string ToleranceOuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal FillHeightMax { get; set; }

        private bool _HasKonus;
        public bool HasKonus
        {
            get { return _HasKonus; }
            set
            {
                _HasKonus = value;
                if (_HasKonus)
                {
                    this.HasGuideBolt = false;
                    this.HasHoleguide = false;
                }
            }
        }

        public decimal OuterKonusMax { get; set; }

        public decimal OuterKonusMin { get; set; }

        public decimal OuterKonusAngle { get; set; }

        public decimal KonusHeight { get; set; }

        private bool _HasHoleguide;
        public bool HasHoleguide
        {
            get { return _HasHoleguide; }
            set
            {
                _HasHoleguide = value;
                if (_HasHoleguide)
                {
                    this.HasGuideBolt = false;
                    this.HasKonus = false;
                }
            }
        }

        public decimal? AdapterDiameter { get; set; }

        private bool _HasGuideBolt;
        public bool HasGuideBolt
        {
            get { return _HasGuideBolt; }
            set
            {
                _HasGuideBolt = value;
                if (_HasGuideBolt)
                {
                    this.HasKonus = false;
                    this.HasHoleguide = false;
                }
            }
        }

        public decimal? GuideHeight { get; set; }

        public decimal? GuideDiameter { get; set; }

        public string ToleranceGuideDiameter { get; set; }

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

        public Components_CoreViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    Core core = new Core()
                    {
                        ID = this.ID,
                        Description = this.Description,
                        OuterDiameter = this.OuterDiameter,
                        ToleranceOuterDiameter = this.ToleranceOuterDiameter,
                        Height = this.Height,
                        FillHeightMax = this.FillHeightMax,
                        HasKonus = this.HasKonus,
                        OuterKonusMax = this.OuterKonusMax,
                        OuterKonusMin = this.OuterKonusMin,
                        OuterKonusAngle = this.OuterKonusAngle,
                        KonusHeight = this.KonusHeight,
                        HasGuideBolt = this.HasGuideBolt,
                        GuideHeight = this.GuideHeight,
                        GuideDiameter = this.GuideDiameter,
                        ToleranceGuideDiameter = this.ToleranceGuideDiameter,
                        HasHoleguide = this.HasHoleguide,
                        AdapterDiameter = this.AdapterDiameter
                    };

                    db.Cores.Add(core);
                    db.SaveChanges();
                    MessageBox.Show("Kern erfolgreich hinzugefügt.");
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
