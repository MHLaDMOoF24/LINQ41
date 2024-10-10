using BonusApp;

namespace BonusAppUnitTest
{

    [TestClass]
    public class UnitTest1
    {
        Order order;
        // Exercise 6
        BonusProvider bonus20 = b => b * 0.2;

        [TestInitialize]
        public void InitializeTest()
        {
            order = new Order();
            order.AddProduct(new Product
            {
                Name = "Mælk",
                Value = 10.0,
                AvailableFrom = new DateTime(2024, 3, 1),
                AvailableTo = new DateTime(2024, 3, 5)
            });
            order.AddProduct(new Product
            {
                Name = "Smør",
                Value = 15.0,
                AvailableFrom = new DateTime(2024, 3, 3),
                AvailableTo = new DateTime(2024, 3, 4)
            });
            order.AddProduct(new Product
            {
                Name = "Pålæg",
                Value = 20.0,
                AvailableFrom = new DateTime(2024, 3, 4),
                AvailableTo = new DateTime(2024, 3, 7)
            });
        }
        [TestMethod]
        public void TenPercent_Test()
        {
            Assert.AreEqual(4.5, Bonuses.TenPercent(45.0));
            Assert.AreEqual(40.0, Bonuses.TenPercent(400.0));
        }
        [TestMethod]
        public void FlatTwoIfAMountMoreThanFive_Test()
        {
            Assert.AreEqual(2.0, Bonuses.FlatTwoIfAmountMoreThanFive(10.0));
            Assert.AreEqual(0.0, Bonuses.FlatTwoIfAmountMoreThanFive(4.0));
        }
        [TestMethod]
        public void GetValueOfProducts_Test()
        {
            Assert.AreEqual(45.0, order.GetValueOfProducts());
        }
        [TestMethod]
        public void GetBonus_Test()
        {
            order.Bonus = Bonuses.TenPercent;
            Assert.AreEqual(4.5, order.GetBonus());

            order.Bonus = Bonuses.FlatTwoIfAmountMoreThanFive;
            Assert.AreEqual(2.0, order.GetBonus());
        }
        [TestMethod]
        public void GetTotalPrice_Test()
        {
            order.Bonus = Bonuses.TenPercent;
            Assert.AreEqual(40.5, order.GetTotalPrice());

            order.Bonus = Bonuses.FlatTwoIfAmountMoreThanFive;
            Assert.AreEqual(43.0, order.GetTotalPrice());
        }

        // Exercise 3
        [TestMethod]
        public void GetValueOfProductsByDate_Test()
        {
            Assert.AreEqual(0.0, order.GetValueOfProducts(new DateTime(2024, 2, 28)));

            Assert.AreEqual(10.0, order.GetValueOfProducts(new DateTime(2024, 3, 2)));

            Assert.AreEqual(25.0, order.GetValueOfProducts(new DateTime(2024, 3, 3)));

            Assert.AreEqual(45.0, order.GetValueOfProducts(new DateTime(2024, 3, 4)));
        }

        // Exercise 4
        [TestMethod]
        public void SortByAvailableToTest()
        {
            List<Product> result = order.SortProductOrderByAvailableTo();



            Assert.AreEqual(3, result.Count);

            Assert.AreEqual("Smør", result[0].Name);

            Assert.AreEqual("Mælk", result[1].Name);

            Assert.AreEqual("Pålæg", result[2].Name);
        }

        // Exercise 6
        [TestMethod]
        public void GetTotalPriceByDate_Test()
        {
            //order.Bonus = Bonuses.TwentyPercent;
            Assert.AreEqual(0.0, order.GetTotalPrice(new DateTime(2024, 2, 28), Bonuses.TwentyPercent));
            Assert.AreEqual(0.0, order.GetTotalPrice(new DateTime(2024, 2, 28), amount => amount * 0.2));
            Assert.AreEqual(0.0, order.GetTotalPrice(new DateTime(2024, 2, 28), bonus20));

            Assert.AreEqual(8.0, order.GetTotalPrice(new DateTime(2024, 3, 2), Bonuses.TwentyPercent));
            Assert.AreEqual(8.0, order.GetTotalPrice(new DateTime(2024, 3, 2), amount => amount * 0.2));
            Assert.AreEqual(8.0, order.GetTotalPrice(new DateTime(2024, 3, 2), bonus20));

            Assert.AreEqual(20.0, order.GetTotalPrice(new DateTime(2024, 3, 3), Bonuses.TwentyPercent));
            Assert.AreEqual(20.0, order.GetTotalPrice(new DateTime(2024, 3, 3), amount => amount * 0.2));
            Assert.AreEqual(20.0, order.GetTotalPrice(new DateTime(2024, 3, 3), bonus20));

            Assert.AreEqual(36.0, order.GetTotalPrice(new DateTime(2024, 3, 4), Bonuses.TwentyPercent));
            Assert.AreEqual(36.0, order.GetTotalPrice(new DateTime(2024, 3, 4), amount => amount * 0.2));
            Assert.AreEqual(36.0, order.GetTotalPrice(new DateTime(2024, 3, 4), bonus20));
        }

        // Exercise 7
        [TestMethod]
        public void SortByAnyTest()
        {
            List<Product> resultName = order.SortProductOrderBy(p => p.Name);
            List<Product> resultValue = order.SortProductOrderBy(p => p.Value);
            List<Product> resultAvailableFrom = order.SortProductOrderBy(p => p.AvailableFrom);
            List<Product> resultAvailableTo = order.SortProductOrderBy(p => p.AvailableTo);

            Assert.AreEqual("Mælk", resultName[0].Name);
            Assert.AreEqual("Pålæg", resultName[1].Name);
            Assert.AreEqual("Smør", resultName[2].Name);

            Assert.AreEqual(10.0, resultValue[0].Value);
            Assert.AreEqual(15.0, resultValue[1].Value);
            Assert.AreEqual(20.0, resultValue[2].Value);

            Assert.AreEqual(new DateTime(2024, 3, 1), resultAvailableFrom[0].AvailableFrom);
            Assert.AreEqual(new DateTime(2024, 3, 3), resultAvailableFrom[1].AvailableFrom);
            Assert.AreEqual(new DateTime(2024, 3, 4), resultAvailableFrom[2].AvailableFrom);

            Assert.AreEqual(new DateTime(2024, 3, 4), resultAvailableTo[0].AvailableTo);
            Assert.AreEqual(new DateTime(2024, 3, 5), resultAvailableTo[1].AvailableTo);
            Assert.AreEqual(new DateTime(2024, 3, 7), resultAvailableTo[2].AvailableTo);
        }
    }
}