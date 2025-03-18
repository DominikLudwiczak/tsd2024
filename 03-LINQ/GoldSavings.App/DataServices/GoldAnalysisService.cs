using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldAnalysisService
    {
        private readonly List<GoldPrice> _goldPrices;

        public GoldAnalysisService(List<GoldPrice> goldPrices)
        {
            _goldPrices = goldPrices;
        }
        public List<GoldPrice> GetAveragePrice(List<int> years)
        {
            return (from price in _goldPrices
                where years.Contains(price.Date.Year)
                group price by price.Date.Year into g
                select new GoldPrice()
                {
                    Date = new DateTime(g.Key, 1, 1),
                    Price = g.Average(x => x.Price)
                }).ToList();
        }
        
        public List<GoldPrice> Top3GoldPrices()
        {
            return (from price in _goldPrices
                where price.Date >= DateTime.Now.AddYears(-1)
                orderby price.Price descending
                select price).Take(3).ToList();
        }
        
        public List<GoldPrice> Bottom3GoldPrices()
        {
            return (from price in _goldPrices
                where price.Date >= DateTime.Now.AddYears(-1)
                orderby price.Price
                select price).Take(3).ToList();
        }
        
        public List<GoldPrice> EarnedMoreThan(DateTime buyDate, int percentage, DateTime endDate)
        {
            var prices = _goldPrices.Where(x => x.Date.Year >= buyDate.Year && x.Date.Month >= buyDate.Month)
                .AsQueryable();
            var startingPrice = prices.Where(x => x.Date.Year == buyDate.Year && x.Date.Month == buyDate.Month).OrderBy(x => x.Price).FirstOrDefault();
            if (startingPrice == null)
            {
                throw new Exception("No price found for the given date.");
            }
            
            var threshold = startingPrice.Price * (100.0 + percentage) / 100.0;
            
            return prices.Where(x => x.Date.Year <= endDate.Year && x.Date.Month <= endDate.Month &&
                                                 x.Price > threshold).ToList();
        }

        public List<GoldPrice> GetSecondTen(DateTime startDate, DateTime endDate)
        {
            return _goldPrices.Where(x => x.Date >= startDate && x.Date <= endDate)
                .OrderByDescending(x => x.Price).Skip(10).Take(3).ToList();
        }
        
        public List<GoldPrice> BestToBuyAndSell(DateTime startTime, DateTime endTime)
        {
            var prices = _goldPrices.Where(x => x.Date >= startTime && x.Date <= endTime).ToList();
            var maxProfit = 0.0;
            var buyDate = new DateTime();
            var sellDate = new DateTime();
            for (int i = 0; i < prices.Count; i++)
            {
                for (int j = i + 1; j < prices.Count; j++)
                {
                    var profit = prices[j].Price - prices[i].Price;
                    if (profit > maxProfit)
                    {
                        maxProfit = profit;
                        buyDate = prices[i].Date;
                        sellDate = prices[j].Date;
                    }
                }
            }
            return new List<GoldPrice>()
            {
                new GoldPrice()
                {
                    Date = buyDate,
                    Price = maxProfit
                },
                new GoldPrice()
                {
                    Date = sellDate,
                    Price = maxProfit
                }
            };
        }
    }
}
