using Gießformkonfigurator.WPF.MVVM.Model.Db_products;
using Gießformkonfigurator.WPF.MVVM.Model.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gießformkonfigurator.UnitTests.Logic
{
    [TestClass]
    public class FilterJobTest
    {
        [TestCategory("Unit-Test")]
        [TestMethod]
        public void GetFilteredDatabase_ProductDisc_FindBaseplate()
        {
            // Arrange
            var product1 = new ProductDisc();
            product1.ID = 1312;
            product1.OuterDiameter = 50;
            product1.InnerDiameter = 35;
            product1.Height = 15;
            product1.HcDiameter = 10;
            product1.HcHoleDiameter = 3;
            var filterJob = new FilterJob(product1);


            // Act
            filterJob.GetFilteredDatabase();

            // Assert
            Assert.IsNotNull(filterJob.listBaseplates);
        }

        [TestCategory("Unit-Test")]
        [TestMethod]
        public void ArraystestData_Baseplate_IsInList()
        {
            // Arrange
            var product1 = new ProductDisc();
            product1.ID = 1312;
            product1.OuterDiameter = 50;
            product1.InnerDiameter = 35;
            product1.Height = 15;
            product1.HcDiameter = 10;
            product1.HcHoleDiameter = 3;
            var filterJob = new FilterJob(product1);


            // Act
            //filterJob.ArraysTestData();

            // Assert
            //Assert.IsNotNull(filterJob.listBaseplates);

        }
    }
}
