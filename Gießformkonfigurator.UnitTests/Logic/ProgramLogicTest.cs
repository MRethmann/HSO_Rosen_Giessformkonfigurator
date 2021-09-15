namespace Gießformkonfigurator.UnitTests.Logic
{
    using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
    using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ProgramLogicTest
    {
        [TestMethod]
        public void newProgramLogic_usingTestdata_returnsTrue()
        {
            Product product = new ProductDisc()
            {
                ID = 000,
                OuterDiameter = 310.00m,
                InnerDiameter = 42.00m,
                Height = 30.00m,
                Hc1Holes = 8,
                Hc1Diameter = 10,
                Hc1HoleDiameter = 1,
                Hc2Holes = 8,
                Hc2Diameter = 20,
                Hc2HoleDiameter = 2,
                Hc3Holes = 8,
                Hc3Diameter = 30,
                Hc3HoleDiameter = 3
            };

            Baseplate baseplate = new Baseplate()
            {
                ID = 001,
                Description = "Baseplate_Test01",
                OuterDiameter = 700m,
                Height = 20,
                OuterKonusMax = 645.58m,
                OuterKonusMin = 639.31m,
                OuterKonusAngle = 15.0m,
                KonusHeight = 11m,
                HasKonus = true,
                InnerKonusMax = 265.31m,
                InnerKonusMin = 259.42m,
                InnerKonusAngle = 15m,
                HasHoleguide = false,
                InnerDiameter = 225m,
                ToleranceInnerDiameter = null,
                HasCore = false,
                Hc1Diameter = 337.59m,
                Hc1Holes = 12,
                Hc1Thread = "M10",
                Hc2Diameter = 286.44m,
                Hc2Holes = 12,
                Hc2Thread = "M10",
                Hc3Diameter = null,
                Hc3Holes = null,
                Hc3Thread = null
            };

            InsertPlate insertPlate = new InsertPlate()
            {
                ID = 002,
                Description = "InsertPlate_Test02",
                OuterDiameter = 265m,
                ToleranceOuterDiameter = null,
                Height = 20,
                OuterKonusMax = 265m,
                OuterKonusMin = 259.11m,
                OuterKonusAngle = 15,
                KonusHeight = 11,
                HasKonus = false,
                InnerKonusMax = null,
                InnerKonusMin = null,
                InnerKonusAngle = null,
                HasHoleguide = true,
                InnerDiameter = 30,
                ToleranceInnerDiameter = "H7",
                HasCore = false,
                Hc1Diameter = null,
                Hc1Holes = null,
                Hc1Thread = null,
                Hc2Diameter = null,
                Hc2Holes = null,
                Hc2Thread = null,
                Hc3Diameter = null,
                Hc3Holes = null,
                Hc3Thread = null
            };

            Ring ring = new Ring()
            {
                ID = 003,
                Description = "Ring_Test03",
                OuterDiameter = 700m,
                ToleranceOuterDiameter = null,
                InnerDiameter = 630m,
                ToleranceInnerDiameter = "0.2",
                Height = 20m,
                FillHeightMax = 18m,
                HasKonus = true,
                InnerKonusMax = 643.71m,
                InnerKonusMin = 639.94m,
                InnerKonusAngle = 15.0m,
                KonusHeight = 11m
            };

            Core core = new Core()
            {
                ID = 004,
                Description = "Core_Test04",
                OuterDiameter = 224.4m,
                ToleranceOuterDiameter = null,
                Height = 42,
                FillHeightMax = 36,
                HasKonus = true,
                OuterKonusMax = 210,
                OuterKonusMin = 206.78m,
                OuterKonusAngle = 15,
                KonusHeight = 6,
                HasGuideBolt = false,
                GuideHeight = null,
                GuideDiameter = null,
                ToleranceGuideDiameter = null,
                HasHoleguide = false,
                AdapterDiameter = null,
            };

            Bolt bolt = new Bolt()
            {
                ID = 005,
                Description = "Bolt_Test05",
                Height = 55,
                OuterDiameter = 14,
                FillHeightMax = 40,
                HasThread = true,
                Thread = "m10",
                HasGuideBolt = false,
                GuideHeight = 15,
                GuideOuterDiameter = 10,
            };
        }
    }
}
