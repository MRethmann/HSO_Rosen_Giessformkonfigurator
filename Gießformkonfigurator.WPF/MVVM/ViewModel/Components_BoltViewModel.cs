//-----------------------------------------------------------------------
// <copyright file="Components_BoltViewModel.cs" company="PlaceholderCompany">
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
    class Components_BoltViewModel : ObservableObject, IDataErrorInfo
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public decimal OuterDiameter { get; set; }

        public decimal Height { get; set; }

        public decimal FillHeightMax { get; set; }

        private bool _HasThread;
        public bool HasThread
        {
            get { return _HasThread; }
            set
            {
                _HasThread = value;
                if (_HasThread)
                {
                    this.HasGuideBolt = false;
                }
            }
        }

        public string Thread { get; set; }

        private bool _HasGuideBolt;
        public bool HasGuideBolt
        {
            get { return _HasGuideBolt; }
            set
            {
                _HasGuideBolt = value;
                if (_HasGuideBolt)
                {
                    this.HasThread = false;
                }
            }
        }

        public decimal? GuideHeight { get; set; }

        public decimal? GuideOuterDiameter { get; set; }

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

        public Components_BoltViewModel()
        {
            insertIntoDbCmd = new RelayCommand(param => insertIntoDb(), param => validateData());
        }

        public void insertIntoDb()
        {
            using (var db = new GießformDBContext())
            {
                try
                {
                    Bolt bolt = new Bolt()
                    {
                        ID = this.ID,
                        Description = this.Description,
                        OuterDiameter = this.OuterDiameter,
                        Height = this.Height,
                        FillHeightMax = this.FillHeightMax,
                        HasThread = this.HasThread,
                        Thread = this.Thread,
                        HasGuideBolt = this.HasGuideBolt,
                        GuideHeight = this.GuideHeight,
                        GuideOuterDiameter = this.GuideOuterDiameter
                    };
                    db.Bolts.Add(bolt);
                    db.SaveChanges();
                    MessageBox.Show("Grundplatte erfolgreich hinzugefügt.");
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
