using Microsoft.EntityFrameworkCore;
using WebEntryModel;
using WebRepository.Interface;

namespace WebRepository.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDatabase _dbContext;
        

        public UnitOfWork(AppDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task Rollback()
        {
            try
            {
                var changedEntries = _dbContext.ChangeTracker.Entries()
                    .Where(x => x.State != EntityState.Unchanged).ToList();

                foreach (var entry in changedEntries)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                await _dbContext.DisposeAsync();
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}