using RM.Data.Svc.Finance;
using RM.Data.Base;

namespace WebApplication1.Data
{
    public interface IRepository
    {
        #region Currency    
        Task<IEnumerable<CurrencyCode>> GetCurrenciesAsync(CancellationToken ct = default);

        Task<CurrencyCode?> GetCurrencyByIdAsync(int currencyId, CancellationToken ct = default);
        Task<CurrencyCode?> GetCurrencyByCodeAsync(string currencyCode, CancellationToken ct = default);

        #endregion Currency

        #region Generic Methods

        Task InsertUpdateEntityAsync<T>(T entity, CancellationToken ct = default) where T : EntityBase;
        void AddUpdateRange<T>(IEnumerable<T> entities) where T : EntityBase;
        void Delete<T>(T entity) where T : EntityBase;

        Task<bool> SaveChangesAsync(CancellationToken ct = default);

        #endregion Generic Methods

    }
}
