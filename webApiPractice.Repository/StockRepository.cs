using Microsoft.EntityFrameworkCore;
using webApiPractice.Contracts;
using webApiPractice.Data;
using webApiPractice.Dto.Stock;
using webApiPractice.Helper;




namespace webApiPractice.Repository
{
    public class StockRepository : IStockRepository
    {
        
        private readonly DataContext _context;
        public StockRepository(DataContext context)
        {
            
            _context = context;
            
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null) return null;
            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject Query)
        {
            var stocks =  _context.Stocks.AsQueryable();
            if(!string.IsNullOrWhiteSpace(Query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(Query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(Query.Symbols))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(Query.Symbols));
            }
            if(!string.IsNullOrWhiteSpace(Query.SortBy))
            {
                if (Query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    stocks = Query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            var skipNumber = (Query.PageNumber -1) * Query.PageSize;
            return await stocks.Skip(skipNumber).Take(Query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
                return null;
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();
            return existingStock;
        }

       
    }
}
