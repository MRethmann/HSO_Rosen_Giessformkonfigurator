//-----------------------------------------------------------------------
// <copyright file="KombinationsObjekt.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WindowsForms
{
#pragma warning disable SA1519 // Braces should not be omitted from multi-line child statement
#pragma warning disable SA1623 // Property summary documentation should match accessors
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gießformkonfigurator.WindowsForms.Main.Db_components;
    using Gießformkonfigurator.WindowsForms.Main.Db_molds;
    using Gießformkonfigurator.WindowsForms.Main.Db_products;
    using Gießformkonfigurator.WindowsForms.Main.Db_supportClasses;
    using Gießformkonfigurator.WindowsForms.Main.Logik;

    /// <summary>
    /// Objekt zur Kombination der Komponenten zur Erstellung der mehrteiligen Gießformen (MGießformen).
    /// </summary>
    public class CombinationJob
    {
        private List<Baseplate> listBaseplates = new List<Baseplate>();
        private List<Ring> listRings = new List<Ring>();
        private List<InsertPlate> listInsertPlates = new List<InsertPlate>();
        private List<Core> listCores = new List<Core>();
        private List<Bolt> listBolts = new List<Bolt>();

        // Frage: Wie kann man das universell umsetzen?
        private ProduktDisc produktDisc;
        private Produkt produktCup;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinationJob"/> class.
        /// </summary>
        /// <param name="produktParam">Das ausgewählte Produkt wird übergeben.
        /// <param name="sapnr">PK in der DB.</param>
        public CombinationJob(int sapnr)
        {
            // TODO: Konstruktur universell für ProduktDisc und ProduktCup gestalten.
            using (var db = new GießformDBContext())
            {
                this.produktDisc = db.ProductDiscs.Find(sapnr);
                Console.WriteLine(sapnr);
                Console.WriteLine("Produkt: " + this.produktDisc);
            }
        }

        /// <summary>
        /// Speichert den aktuellen Listenindex.
        /// </summary>
        public int Index { get; set; }

        public void ArraysTestData()
        {
            this.listBaseplates.Add(new Baseplate() { Bezeichnung_RoCon = "Grundplatte 12", Außendurchmesser = 375.00m, Hoehe = 20.00m, Konus_Außen_Max = 347.89m, Konus_Außen_Min = 342.00m, Konus_Außen_Winkel = 15.00m, Konus_Hoehe = 11.00m, Innendurchmesser = 15.00m, Mit_Lochfuehrung = true });
            this.listBaseplates.Add(new Baseplate() { Bezeichnung_RoCon = "Grundplatte 22", Außendurchmesser = 700.00m, Hoehe = 20.00m, Konus_Außen_Max = 645.58m, Konus_Außen_Min = 639.31m, Konus_Außen_Winkel = 15.00m, Konus_Hoehe = 11.00m, Innendurchmesser = 225.00m, Konus_Innen_Max = 265.31m, Konus_Innen_Min = 259.42m, Konus_Innen_Winkel = 15.00m, Mit_Konusfuehrung = true });
            this.listInsertPlates.Add(new InsertPlate() { Bezeichnung_RoCon = "Einsatz fuer Grundplatte 22in-24in", Außendurchmesser = 265.00m, Hoehe = 20.00m, Konus_Außen_Max = 265.00m, Konus_Außen_Min = 259.11m, Konus_Außen_Winkel = 15.00m, Konus_Hoehe = 11.00m, Innendurchmesser = 30.00m, Mit_Lochfuehrung = true });
            this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Form ring", Außendurchmesser = 375.00m, Hoehe = 31.6m, Konus_Max = 345.43m, Konus_Min = 342.21m, Konus_Winkel = 15.00m, Konus_Hoehe = 6.00m, Innendurchmesser = 315.3m, Gießhoehe_Max = 25.00m, mit_Konusfuehrung = true });
            this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Formring Dichtscheibe d 324", Außendurchmesser = 375.00m, Hoehe = 21.6m, Konus_Max = 345.52m, Konus_Min = 342.3m, Konus_Winkel = 15.00m, Konus_Hoehe = 6.00m, Innendurchmesser = 330.6m, Gießhoehe_Max = 15.00m, mit_Konusfuehrung = true });
            this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Innenring01", Außendurchmesser = 330.3m, Hoehe = 21.6m, Innendurchmesser = 310.7m, Gießhoehe_Max = 15.00m, mit_Konusfuehrung = false });
            this.listRings.Add(new Ring() { Bezeichnung_RoCon = "Innenring02", Außendurchmesser = 310.5m, Hoehe = 21.6m, Innendurchmesser = 250.0m, Gießhoehe_Max = 15.00m, mit_Konusfuehrung = false });
            this.listCores.Add(new Core() { Bezeichnung_RoCon = "Einsatz fuer Innendurchmesser d 40", Außendurchmesser = 41.4m, Hoehe = 40.6m, Konus_Hoehe = 15.00m, Durchmesser_Fuehrung = 15.00m, Gießhoehe_Max = 25.6m, Mit_Fuehrungsstift = true });
            this.listCores.Add(new Core() { Bezeichnung_RoCon = "Einsatz fuer Innendurchmesser d219", Außendurchmesser = 224.4m, Hoehe = 42.00m, Konus_Außen_Max = 210.00m, Konus_Außen_Min = 206.78m, Konus_Außen_Winkel = 15.00m, Konus_Hoehe = 6.00m, Gießhoehe_Max = 36.00m, Mit_Konusfuehrung = true });
            this.listCores.Add(new Core() { Bezeichnung_RoCon = "Einsatz fuer Innendurchmesser d=82", Außendurchmesser = 84.1m, Hoehe = 40.6m, Konus_Hoehe = 15.00m, Durchmesser_Fuehrung = 15.00m, Gießhoehe_Max = 25.6m, Hoehe_Fuehrung = 20.00m, Mit_Fuehrungsstift = true });
        }

        /// <summary>
        /// Stellt eine Verbindung zur Datenbank her und speichert die Komponenten in einer lokalen Objektliste. Die Komponenten werden über die Produktparameter vorgefiltert.
        /// </summary>
        public void FiltereDiscDB()
        {
            if (this.produktDisc != null)
            {
                using (var db = new GießformDBContext())
                {
                    foreach (var grundplatte in db.Baseplates)
                    {
                        if (this.produktDisc.Außendurchmesser < grundplatte.Außendurchmesser)
                        {
                            this.listBaseplates.Add(grundplatte);
                            Console.WriteLine("Grundplatte " + grundplatte + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Grundplatte " + grundplatte + " removed.");
                        }
                    }

                    foreach (var ring in db.Rings)
                    {
                        if ((ring.mit_Konusfuehrung == false && this.produktDisc.Innendurchmesser > ring.Außendurchmesser && this.produktDisc.Außendurchmesser < ring.Außendurchmesser)
                            || (ring.mit_Konusfuehrung && this.produktDisc.Außendurchmesser < ring.Außendurchmesser))
                        {
                            this.listRings.Add(ring);
                            Console.WriteLine("Ring " + ring + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Ring " + ring + " removed.");
                        }
                    }

                    // no filter for insertplates
                    foreach (var einlegeplatte in db.InsertPlates)
                    {
                        this.listInsertPlates.Add(einlegeplatte);
                        Console.WriteLine("Einlegeplatte " + einlegeplatte + " added to the filter.");
                    }

                    foreach (var innenkern in db.Cores)
                    {
                        if (this.produktDisc.Innendurchmesser > innenkern.Außendurchmesser)
                        {
                            this.listCores.Add(innenkern);
                            Console.WriteLine("Innenkern " + innenkern + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Innenkern " + innenkern + " removed.");
                        }
                    }

                    foreach (var bolzen in db.Bolts)
                    {
                        // TODO: Abgleich hinzufügen. Produkt besitzt aktuell nur das Attribut Lochkreis, welches keine Vergleichseigenschaft besitzt. Durchmesser der Löcher benötigt.
                        if (bolzen.Außendurchmesser <= this.produktDisc.Lk1Durchmesser
                            || bolzen.Außendurchmesser <= this.produktDisc.Lk2Durchmesser
                            || bolzen.Außendurchmesser <= this.produktDisc.Lk3Durchmesser)
                        {
                            this.listBolts.Add(bolzen);
                            Console.WriteLine("Bolzen " + bolzen + " added to the filter.");
                        }
                        else
                        {
                            Console.WriteLine("Bolzen " + bolzen + " removed.");
                        }
                    }

                    Console.WriteLine("Baseplates after filter: " + this.listBaseplates.Count());
                    Console.WriteLine("Insertplates after filter: " + this.listInsertPlates.Count());
                    Console.WriteLine("Cores after filter: " + this.listCores.Count());
                    Console.WriteLine("Rings after filter: " + this.listRings.Count());
                    Console.WriteLine("Bolts after filter: " + this.listBolts.Count());
                }
            }
            else
            {
                // TODO: Prüfen ob dieser Teil relevant ist. Soll das Szenario abfangen, dass kein Produkt vorhanden ist und man den Kombinationsalgorithmus trotzdem ausführen möchte.
                using (var db = new GießformDBContext())
                {
                    foreach (var grundplatte in db.Baseplates)
                    {
                        this.listBaseplates.Add(grundplatte);
                    }

                    foreach (var ring in db.Rings)
                    {
                        this.listRings.Add(ring);
                    }

                    foreach (var einlegeplatte in db.InsertPlates)
                    {
                        this.listInsertPlates.Add(einlegeplatte);
                    }

                    foreach (var innenkern in db.Cores)
                    {
                        this.listCores.Add(innenkern);
                    }

                    foreach (var bolzen in db.Bolts)
                    {
                        this.listBolts.Add(bolzen);
                    }
                }
            }
        }

        /// <summary>
        /// Nutzt die vorgefilterte Datenbank um alle möglichen Gieformkombinationen zu finden.
        /// </summary>
        /// <returns>List MGießformenFinal.</returns>
        [STAThread]
        public List<ModularMold> KombiniereMGießformen()
        {
            // Listen, welche zur Zwischenspeicherung der mehrteiligen Gießformen genutzt werden, bevor sie vervollständigt wurden und ausgegeben werden können.
            List<ModularMold> mGießformenTemp01 = new List<ModularMold>();
            List<ModularMold> mGießformenTemp02 = new List<ModularMold>();
            var combinationRuleSet = new CombinationRuleset();

            // Grundplatten --> Fuehrungsringe
            for (int iGP = 0; iGP < this.listBaseplates.Count; iGP++)
            {
                for (int iRinge = 0; iRinge < this.listRings.Count; iRinge++)
                {
                    if (combinationRuleSet.Combine(this.listBaseplates[iGP], this.listRings[iRinge]))
                    {
                        mGießformenTemp01.Add(new ModularMold(this.listBaseplates[iGP], this.listRings[iRinge], null, null));
                    }
                }
            }

            // Mehrteilige Gießformen --> Einlegeplatten
            this.Index = mGießformenTemp01.Count;
            for (int iTemp = 0; iTemp < this.Index; iTemp++)
            {
                if (mGießformenTemp01[iTemp].Grundplatte.Mit_Konusfuehrung)
                {
                    for (int iEP = 0; iEP < this.listInsertPlates.Count; iEP++)
                    {
                        if (combinationRuleSet.Combine(mGießformenTemp01[iTemp].Grundplatte, this.listInsertPlates[iEP]))
                        {
                            mGießformenTemp01.Add(new ModularMold(mGießformenTemp01[iTemp].Grundplatte, mGießformenTemp01[iTemp].Fuehrungsring, this.listInsertPlates[iEP], null));
                        }
                    }
                }
            }

            // Mehrteilige Gießformen --> Kerne
            this.Index = mGießformenTemp01.Count;
            for (int iTemp = 0; iTemp < this.Index; iTemp++)
            {
                for (int iKerne = 0; iKerne < this.listCores.Count; iKerne++)
                {
                    // Einlegeplatte vorhanden
                    if (mGießformenTemp01[iTemp].Einlegeplatte != null)
                    {
                        // Einlegeplatten mit Konusfuehrung
                        if (mGießformenTemp01[iTemp].Einlegeplatte.Mit_Konusfuehrung == true && combinationRuleSet.Combine(mGießformenTemp01[iTemp].Einlegeplatte, this.listCores[iKerne]))
                        {
                            mGießformenTemp02.Add(new ModularMold(mGießformenTemp01[iTemp].Grundplatte, mGießformenTemp01[iTemp].Fuehrungsring, mGießformenTemp01[iTemp].Einlegeplatte, this.listCores[iKerne]));
                        }

                        // Einlegeplatten mit Lochfuehrung
                        else if (mGießformenTemp01[iTemp].Einlegeplatte.Mit_Lochfuehrung == true && combinationRuleSet.Combine(mGießformenTemp01[iTemp].Einlegeplatte, this.listCores[iKerne]))
                        {
                            mGießformenTemp02.Add(new ModularMold(mGießformenTemp01[iTemp].Grundplatte, mGießformenTemp01[iTemp].Fuehrungsring, mGießformenTemp01[iTemp].Einlegeplatte, this.listCores[iKerne]));
                        }
                    }

                    // Ohne Einlegeplatte
                    else if (mGießformenTemp01[iTemp].Einlegeplatte == null)
                    {
                        // Grundplatten mit Konusfuehrung
                        if ((mGießformenTemp01[iTemp].Grundplatte.Mit_Konusfuehrung == true || mGießformenTemp01[iTemp].Grundplatte.Mit_Lochfuehrung == true) && combinationRuleSet.Combine(mGießformenTemp01[iTemp].Grundplatte, this.listCores[iKerne]))
                        {
                            mGießformenTemp02.Add(new ModularMold(mGießformenTemp01[iTemp].Grundplatte, mGießformenTemp01[iTemp].Fuehrungsring, null, this.listCores[iKerne]));
                        }

                        // Grundplatte mit Kern
                        else if (mGießformenTemp01[iTemp].Grundplatte.Mit_Kern == true)
                        {
                            mGießformenTemp02.Add(new ModularMold(mGießformenTemp01[iTemp].Grundplatte, mGießformenTemp01[iTemp].Fuehrungsring, null, null));
                        }
                    }
                }
            }

            // Mehrteilige Gießformen --> Innenringe
            this.Index = mGießformenTemp02.Count;

            // Aktuell drei feste Durchläufe --> tbd.
            for (int i = 0; i < 3; i++)
            {
                for (int iTemp = 0; iTemp < this.Index; iTemp++)
                {
                    if (mGießformenTemp02[iTemp].Fuehrungsring != null)
                    {
                        for (int iRinge = 0; iRinge < this.listRings.Count; iRinge++)
                        {
                            // Betrachtet nur Ringe ohne Konusfuehrung
                            if (this.listRings[iRinge].mit_Konusfuehrung == false)
                            {
                                // Neuer Ring muss größer als der vorhandene Kern sein
                                if (this.listRings[iRinge].Innendurchmesser > mGießformenTemp02[iTemp].Innenkern.Außendurchmesser)
                                {
                                    // Bereits Innenringe vorhanden --> Kombination mit letztem Innenring
                                    if (mGießformenTemp02[iTemp].ListInnerRings.Count > 0)
                                    {
                                        int indexer = mGießformenTemp02[iTemp].ListInnerRings.Count - 1;
                                        if (combinationRuleSet.Combine(mGießformenTemp02[iTemp].ListInnerRings[indexer], this.listRings[iRinge]))
                                        {
                                            mGießformenTemp02[iTemp].ListInnerRings.Add(this.listRings[iRinge]);
                                        }
                                    }

                                    // Keine Innenringe vorhanden --> Kombination mit Fuehrungsring
                                    else
                                    {
                                        if (combinationRuleSet.Combine(mGießformenTemp02[iTemp].Fuehrungsring, this.listRings[iRinge]))
                                        {
                                            mGießformenTemp02[iTemp].ListInnerRings.Add(this.listRings[iRinge]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return mGießformenTemp02;
        }
    }
}