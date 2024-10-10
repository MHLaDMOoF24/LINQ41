using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusApp
{
    public class Order
    {
        public BonusProvider Bonus { get; set; }
        private List<Product> _products = new List<Product>();

        public void AddProduct(Product p)
        {
            _products.Add(p);
        }

        public double GetValueOfProducts()
        {
            double valueOfProducts = _products
                .Select(p => p.Value)
                .Sum();

            return valueOfProducts;
        }
        public double GetValueOfProducts(DateTime date)
        {
            // Exercise 3
            //double valueOfProducts = _products
            //    .Where(p => p.AvailableFrom <= date && p.AvailableTo >= date)
            //    .Select(p => p.Value)
            //    .Sum();

            // Exercise 5
            double valueOfProducts = _products
                .Select(p => (p.AvailableFrom <= date && p.AvailableTo >= date) ? p.Value : 0)
                .Sum();

            return valueOfProducts;
        }

        public double GetBonus()
        {
            return Bonus(GetValueOfProducts());
        }
        public double GetBonus(DateTime date, BonusProvider bonus)
        {
            return bonus(GetValueOfProducts(date));
        }

        public double GetTotalPrice()
        {
            return GetValueOfProducts() - GetBonus();
        }
        public double GetTotalPrice(DateTime date, BonusProvider bonus)
        {
            return GetValueOfProducts(date) - GetBonus(date, bonus);
        }

        public List<Product> SortProductOrderByAvailableTo()
        {
            var query = _products
                .OrderBy(p => p.AvailableTo)
                .ToList();

            return query;
        }

        public List<Product> SortProductOrderBy(Func<Product, object> keySelector)
        {
            return _products.OrderBy(keySelector).ToList();
        }
    }
}
