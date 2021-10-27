using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.AsyncConcept
{
    public class StockPrices
    {
        private Dictionary<string, decimal> _stockPrices;
        public async Task<decimal> GetStockPriceFOrAsync(string companyId)
        {
            await InitializeMapIfNeededAsync();
            _stockPrices.TryGetValue(companyId, out var result);
            return result;
        }

        private async Task InitializeMapIfNeededAsync()
        {
            if (_stockPrices != null)
                return;

            await Task.Delay(42);
            _stockPrices = new Dictionary<string, decimal> { { "MSFT", 42 } };
        }
    }
}
