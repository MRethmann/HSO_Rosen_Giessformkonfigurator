using Gießformkonfigurator.WPF.MVVM.Model.Db_components;
using Gießformkonfigurator.WPF.MVVM.Model.Db_molds;
using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
using Gießformkonfigurator.WPF.MVVM.Model.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gießformkonfigurator.UnitTests.Logic
{
    [TestClass]
    public class CombinationJobTest
    {
        [TestCategory("Unit-Test")]
        [TestMethod]
        public void CombineSingleDiscMold_Combine_Combined()
        {
            // Arrange
            var product1 = new ProductDisc();
            product1.ID = 3339;
            product1.OuterDiameter = 257;
            product1.InnerDiameter = 41;
            product1.Height = 25;
            product1.Description = "10'";
            product1.FactorPU = (decimal?)1.017;

            var singleMold1 = new SingleMoldDisc();
            singleMold1.ID = 344651;
            singleMold1.Description = "10'";
            singleMold1.OuterDiameter = (decimal?)261.32;
            singleMold1.InnerDiameter = (decimal?)41.7;
            singleMold1.Height = (decimal?)25.43;

            var coreSingleMold = new CoreSingleMold();
            coreSingleMold.ID = 371436;
            coreSingleMold.Description = "F06";
            coreSingleMold.OuterDiameter = (decimal) 41.7175;
            coreSingleMold.InnerDiameter = (decimal) 21.3675;
            coreSingleMold.Height = (decimal) 20.35;
            coreSingleMold.FactorPU = (decimal)1.0175;

            
            var filterJob = new FilterJob(product1);
            var combinationJob = new CombinationJob(product1, filterJob);

            combinationJob.listCoresSingleMold.Clear();
            combinationJob.listSingleMoldDiscs.Clear(); 

            combinationJob.listCoresSingleMold.Add(coreSingleMold);
            combinationJob.listSingleMoldDiscs.Add(singleMold1);


            // Act
            combinationJob.CombineSingleDiscMold();

            // Assert
            Assert.IsNotNull(combinationJob.singleMoldDiscOutput);
        }

        [TestCategory("Unit-Test")]
        [TestMethod]
        public void CombineModularDiscMold_Combine_Combined()
        {
            // Arrange
            var product1 = new ProductDisc();
            product1.ID = 001;
            product1.OuterDiameter = 257;
            product1.InnerDiameter = 41;
            product1.Height = 25;
            product1.Description = "10'";
            product1.FactorPU = (decimal?)1.017;

            var baseplate = new Baseplate();
            baseplate.ID = 21875;
            baseplate.OuterDiameter = 700;
            baseplate.Height = 20;
            baseplate.OuterKonusMax = (decimal)645.58;
            baseplate.OuterKonusMin = (decimal)639.69;
            baseplate.OuterKonusAngle = 15;
            baseplate.KonusHeight = 11;
            baseplate.InnerDiameter = 225;
            baseplate.InnerKonusMax = (decimal)265.31;
            baseplate.InnerKonusMin = (decimal)259.42;
            baseplate.InnerKonusAngle = 15;
            baseplate.Hc1Diameter = (decimal)337.59;
            baseplate.Hc1Holes = 12;
            baseplate.Hc1Thread = "M10";
            baseplate.Hc2Diameter = (decimal)286.44;
            baseplate.Hc2Holes = 12;
            baseplate.Hc2Thread = "M10";
            baseplate.HasCore = false;
            baseplate.HasKonus = true;
            baseplate.HasHoleguide = false;

            var ring = new Ring();
            //ring.ID = ;

            var insertplate = new InsertPlate();
            insertplate.ID = 21874;
            insertplate.OuterDiameter = 265;
            insertplate.Height = 20;
            insertplate.InnerKonusMax = 265;
            insertplate.OuterKonusMin = (decimal)259.11;
            insertplate.OuterKonusAngle = 15;
            insertplate.InnerDiameter = 30;
            insertplate.ToleranceInnerDiameter = "H7";
            insertplate.HasCore = false;
            insertplate.HasHoleguide = true;
            insertplate.HasKonus = false;

            var core = new Core();
            core.ID =  21838;
            core.OuterDiameter = (decimal)224.4;
            core.Height = 42;
            core.OuterKonusMax = 210;
            core.OuterKonusMin = (decimal)206.78;
            core.OuterKonusAngle = 15;
            core.KonusHeight = 6;
            core.FillHeightMax = 36;
            core.HasKonus = true;
            core.HasHoleguide = false;
            core.HasGuideBolt = false;      
           
            var bolt = new Bolt();
            //bolt.ID = ;

            var filterJob = new FilterJob(product1);
            var combinationJob = new CombinationJob(product1, filterJob);

            combinationJob.listBaseplates.Clear();
            combinationJob.listCores.Clear();
            combinationJob.listInsertPlates.Clear();

            combinationJob.listBaseplates.Add(baseplate);
            //combinationJob.listRings.Add(ring);
            combinationJob.listInsertPlates.Add(insertplate);
            combinationJob.listCores.Add(core);
            //combinationJob.listBolts.Add(bolt);

            // Act
            combinationJob.CombineModularDiscMold();

            // Assert
            Assert.IsNotNull(combinationJob.modularMoldsOutput);
        }
    }
}
