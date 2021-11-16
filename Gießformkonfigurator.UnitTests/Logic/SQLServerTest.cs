using Giessformkonfigurator.WPF.MVVM.Model.Db_supportClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giessformkonfigurator.UnitTests
{
    
        [TestCategory("Integration")]
        [TestClass]
        public class SQLServerTest
        {

        [TestCategory("Integration")]
        [TestMethod]
            public void CheckBaseplatesSeeded()
            {
                using (var context = new GießformDBContext())
                {
                    var _baseplates = context.Baseplates.ToList();
                Assert.AreNotEqual(_baseplates.Count, 0);
                }
            }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckRingsSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _rings = context.Rings.ToList();
                Assert.AreNotEqual(_rings.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckCoresSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _cores = context.Cores.ToList();
                Assert.AreNotEqual(_cores.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckProductDiscsSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _productDiscs = context.ProductDiscs.ToList();
                Assert.AreNotEqual(_productDiscs.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckProductCupSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _productCups = context.ProductCups.ToList();
                Assert.AreNotEqual(_productCups.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckBoltsSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _bolts = context.Bolts.ToList();
                Assert.AreNotEqual(_bolts.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckBTCSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _btc = context.BoltCircleTypes.ToList();
                Assert.AreNotEqual(_btc.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckCoreSingleMoldsSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _coreSingleMolds = context.CoreSingleMolds.ToList();
                Assert.AreNotEqual(_coreSingleMolds.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckCupformsSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _cupforms = context.Cupforms.ToList();
                Assert.AreNotEqual(_cupforms.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckInsertplatesSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _insertPlates = context.InsertPlates.ToList();
                Assert.AreNotEqual(_insertPlates.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckSingleMoldsCupsSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _singleMoldsCups = context.SingleMoldCups.ToList();
                Assert.AreNotEqual(_singleMoldsCups.Count, 0);
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void CheckSingleMoldsDiscsSeeded()
        {
            using (var context = new GießformDBContext())
            {
                var _singleMoldDiscs = context.SingleMoldDiscs.ToList();
                Assert.AreNotEqual(_singleMoldDiscs.Count, 0);
            }
        }        
    }
    }

