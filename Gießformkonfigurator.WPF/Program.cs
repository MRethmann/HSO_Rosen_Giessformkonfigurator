//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Gießformkonfigurator.WPF
{
#pragma warning disable SA1519 // Braces should not be omitted from multi-line child statement
#pragma warning disable SA1623 // Property summary documentation should match accessors
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
    using Gießformkonfigurator.WPF.MVVM.Model.Logic;

    /// <summary>
    /// Program Entry.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Speichert den aktuellen Listenindex.
        /// </summary>
        public static int Index { get; set; }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // Listen, welche zur Zwischenspeicherung der mehrteiligen Gießformen genutzt werden, bevor sie vervollständigt wurden und ausgegeben werden können.
            List<ModularMold> mGießformenFinal = new List<ModularMold>();

            //CombinationJob co = new CombinationJob(0);
            //co.FiltereDiscDB();
            //co.ArraysTestData();
            //mGießformenFinal = co.KombiniereMGießformen();

            foreach (ModularMold mGießform in mGießformenFinal)
            {
                Console.Write(mGießform.baseplate?.Description + " + ");
                Console.Write(mGießform.insertPlate?.Description + " + ");
                Console.Write(mGießform.guideRing?.Description + " + ");
                if (mGießform.ListOuterRings.Count > 0)
                {
                    for (int i = 0; i < mGießform.ListOuterRings.Count; i++)
                    {
                        Console.Write(mGießform.ListOuterRings[i]?.Description + " + ");
                    }
                }

                Console.WriteLine(mGießform.core?.Description);
            }

            Console.Write("Am Ende");
            Console.ReadLine();

            // fillDatabase();
        }
    }
}