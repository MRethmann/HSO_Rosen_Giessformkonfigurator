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
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public string ToleranceOuterDiameter { get; set; }

        public decimal InnerDiameter { get; set; }

        public string ToleranceInnerDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal FillHeightMax { get; set; }

        private bool _HasKonus;
        public bool HasKonus
        {
            get { return _HasKonus; }
            set { _HasKonus = value; }
        }

        public decimal? InnerKonusMax { get; set; }

        public decimal? InnerKonusMin { get; set; }

        public decimal? InnerKonusAngle { get; set; }

        public decimal? KonusHeight { get; set; }

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

        public Components_RingViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    Ring ring = new Ring()
                    {
                        ID = this.ID,
                        Description = this.Description,
                        OuterDiameter = this.OuterDiameter,
                        ToleranceOuterDiameter = this.ToleranceOuterDiameter,
                        Height = this.Height,
                        FillHeightMax = this.FillHeightMax,
                        InnerDiameter = this.InnerDiameter,
                        ToleranceInnerDiameter = this.ToleranceInnerDiameter,
                        KonusHeight = this.KonusHeight,
                        InnerKonusMax = this.InnerKonusMax,
                        InnerKonusMin = this.InnerKonusMin,
                        InnerKonusAngle = this.InnerKonusAngle,
                        HasKonus = this.HasKonus
                    };

                    db.Rings.Add(ring);
                    db.SaveChanges();
                    MessageBox.Show("Ring erfolgreich hinzugefügt.");
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
