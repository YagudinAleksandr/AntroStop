using AntroStop.DAL.Context;
using AntroStop.DAL.Entities;
using AntroStop.Domain.Pagination.Paging;
using AntroStop.Domain.Pagination.RequestFeatures;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.DAL.Repositories
{
    public class ViolationRepository<T> : IViolationsRepository<T> where T : Violation, new()
    {
        #region Свойства
        private readonly DataDB db;
        protected DbSet<T> Set { get; }
        protected virtual IQueryable<T> Items => Set;
        #endregion

        public ViolationRepository(DataDB db)
        {
            this.db = db;
            this.Set = this.db.Set<T>();
        }

        #region Методы

        public async Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            if (entity == null || entity.UserID == string.Empty) throw new ArgumentNullException();

            var user = db.Users.Where(i => i.ID == entity.UserID).FirstOrDefault();

            if (user == null) throw new ArgumentNullException();

            entity.User = user;
            entity.Id = Guid.Empty;

            await db.AddAsync(entity, Cancel).ConfigureAwait(false);

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }

        public async Task<int> Count(CancellationToken Cancel = default)
        {
            return await Items.CountAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<T> Delete(Guid ID, CancellationToken Cancel = default)
        {
            var item = Set.Local.FirstOrDefault(x => x.Id == ID);

            if (item == null)
                item = await Set.Select(i => new T { Id = i.Id })
                    .FirstOrDefaultAsync(x => x.Id == ID, Cancel)
                    .ConfigureAwait(false);

            if (item is null) return null;

            db.Entry(item).State = EntityState.Deleted;

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return item;
        }

        public async Task<bool> ExistID(Guid ID, CancellationToken Cancel = default)
        {
            return await Items.AnyAsync(item => item.Id == ID, Cancel).ConfigureAwait(false);
        }

        public async Task<T> Get(Guid ID, CancellationToken Cancel = default)
        {
            return await Items.Include(u => u.User).FirstOrDefaultAsync(item => item.Id == ID, Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken Cancel = default)
        {
            if (count <= 0) return Enumerable.Empty<T>();

            IQueryable<T> query = Items switch
            {
                IOrderedQueryable<T> ordered_query => ordered_query,
                { } q => q.OrderBy(i => i.Id)
            };

            if (skip > 0) query = query.Skip(skip);

            return await query.Take(count).ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default)
        {
            return await Items.Include(u => u.User).ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllByID(string Id, CancellationToken Cancel = default)
        {
            return await Items.Where(x => x.UserID == Id).Include(u => u.User).ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllByStatus(string Status, CancellationToken Cancel = default)
        {
            return await Items.Where(s => s.Status == Status).Include(u => u.User).ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<T> Update(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            entity.UpdatedAt= DateTimeOffset.UtcNow;
            entity.User = null;

            db.Entry(entity).State = EntityState.Modified;

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }

        public async Task<int> GetCountByStatus(string Status, CancellationToken Cancel = default)
        {
            return await Items.Where(s=>s.Status==Status).CountAsync(Cancel).ConfigureAwait(false);
        }

        public Task<PagedList<T>> GetPage(PageParametrs productParameters, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
