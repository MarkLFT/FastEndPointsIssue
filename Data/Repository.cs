using Microsoft.EntityFrameworkCore;
using RM.Data.Svc.Finance;
using RM.Data.Base;

namespace WebApplication1.Data;

public class Repository(IFinanceDbContext contextLive) : IRepository
{
    #region Generic Methods

    public async Task InsertUpdateEntityAsync<T>(T entity, CancellationToken ct = default) where T : EntityBase
    {
        var entry = contextLive.Entry(entity);

        if (entry.State == EntityState.Detached)
        {
            // Get the entity type
            var entityType = contextLive.Entry(entity).Metadata.GetKeys();
            if (!entityType.Any())
                throw new InvalidOperationException($"No primary key defined for entity {typeof(T).Name}");

            // Create key values dictionary
            var keyValues = new Dictionary<string, object>();
            foreach (var key in entityType.SelectMany(k => k.Properties))
            {
                var value = (key.PropertyInfo?.GetValue(entity)) ?? throw new InvalidOperationException($"Cannot get value for key property {key.Name}");
                keyValues[key.Name] = value;
            }

            // Try to find existing entity
            var existingEntity = await contextLive.Set<T>().FindAsync([.. keyValues.Values], ct);

            if (existingEntity == null)
            {
                // New entity - add it
                await contextLive.AddAsync(entity, ct);
            }
            else
            {
                // Existing entity - update it
                contextLive.Entry(existingEntity).CurrentValues.SetValues(entity);
                entry = contextLive.Entry(existingEntity);

                // Handle navigation properties (collections)
                foreach (var navigationEntry in entry.Collections)
                {
                    // Load the current collection if not loaded
                    if (!navigationEntry.IsLoaded)
                        await navigationEntry.LoadAsync(ct);


                    if (navigationEntry.CurrentValue is IEnumerable<object> existingCollection && navigationEntry.Metadata.PropertyInfo!
                        .GetValue(entity) is IEnumerable<object> newCollection)
                    {
                        var collectionType = navigationEntry.Metadata.TargetEntityType;
                        var relatedKeys = collectionType.GetKeys().SelectMany(k => k.Properties).ToList();

                        if (relatedKeys.Count != 0)
                        {
                            // Compare and update collection items
                            foreach (var newItem in newCollection)
                            {
                                var itemKeyValues = relatedKeys
                                    .Select(k => k.PropertyInfo?.GetValue(newItem))
                                    .ToArray();

                                var existingItem = existingCollection.FirstOrDefault(e =>
                                    relatedKeys.Select(k => k.PropertyInfo?.GetValue(e))
                                        .SequenceEqual(itemKeyValues));

                                if (existingItem == null)
                                {
                                    // New item - add to collection
                                    await contextLive.AddAsync(newItem, ct);
                                }
                                else
                                {
                                    // Update existing item
                                    contextLive.Entry(existingItem).CurrentValues.SetValues(newItem);
                                }
                            }

                            // Remove items that don't exist in the new collection
                            foreach (var existingItem in existingCollection)
                            {
                                var itemKeyValues = relatedKeys
                                    .Select(k => k.PropertyInfo?.GetValue(existingItem))
                                    .ToArray();

                                if (!newCollection.Any(e =>
                                    relatedKeys.Select(k => k.PropertyInfo?.GetValue(e))
                                        .SequenceEqual(itemKeyValues)))
                                {
                                    contextLive.Remove(existingItem);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void AddUpdateRange<T>(IEnumerable<T> entities) where T : EntityBase
    {
        if (entities is null || !entities.Any())
            return;

        contextLive.UpdateRange(entities);
    }

    public void Delete<T>(T entity) where T : EntityBase
    {
        if (entity is null || entity.Id == 0)
            return;

        contextLive.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct = default)
    {
        var result = await contextLive.SaveChangesAsync(ct);
        return result > 0;
    }

    #endregion Generic Methods

    #region Currencies

    public async Task<IEnumerable<CurrencyCode>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return await contextLive.CurrencyCodes.AsNoTracking().ToListAsync(ct);
    }

    public async Task<CurrencyCode?> GetCurrencyByIdAsync(int currencyId, CancellationToken ct = default)
    {
        return await contextLive.CurrencyCodes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == currencyId, ct);
    }

    public async Task<CurrencyCode?> GetCurrencyByCodeAsync(string currencyCode, CancellationToken ct = default)
    {
        return await contextLive.CurrencyCodes.AsNoTracking().FirstOrDefaultAsync(c => c.Code == currencyCode, ct);
    }


    #endregion Currencies

}
