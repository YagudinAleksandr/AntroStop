using AntroStop.DAL.Context;
using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.DAL.Repositories
{
    public class RoleRepository<T> : IIntRepository<T> where T : Role, new()
    {
        #region Поля и Свойства
        private readonly DataDB db;
        protected DbSet<T> Set { get; }
        protected virtual IQueryable<T> Items => Set;
        #endregion

        public RoleRepository(DataDB db)
        {
            this.db = db;
            this.Set = this.db.Set<T>();
        }

        #region Методы
        public async Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            if (entity == null) throw new ArgumentNullException();

            await db.AddAsync(entity, Cancel).ConfigureAwait(false);

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }

        public async Task<int> Count(CancellationToken Cancel = default)
        {
            return await Items.CountAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<T> Delete(int ID, CancellationToken Cancel = default)
        {
            var item = Set.Local.FirstOrDefault(x => x.ID == ID);

            if (item == null)
                item = await Set.Select(i => new T { ID = i.ID })
                    .FirstOrDefaultAsync(x => x.ID == ID, Cancel)
                    .ConfigureAwait(false);

            if (item is null) return null;

            db.Entry(item).State = EntityState.Deleted;

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return item;
        }

        public async Task<bool> ExistID(int ID, CancellationToken Cancel = default)
        {
            return await Items.AnyAsync(item => item.ID == ID, Cancel).ConfigureAwait(false);
        }

        public async Task<T> Get(int ID, CancellationToken Cancel = default)
        {
            return await Items.FirstOrDefaultAsync(item => item.ID == ID, Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default)
        {
            return await Items.ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IPaget<T>> GetPage(int PageIndex, int PageSize, CancellationToken Cancel = default)
        {
            if (PageSize <= 0)
                return new Page(Enumerable.Empty<T>(), PageSize, PageIndex, PageSize);

            var query = Items;

            var totalCount = await query.CountAsync(Cancel).ConfigureAwait(false);

            if (totalCount == 0)
                return new Page(Enumerable.Empty<T>(), 0, PageIndex, PageSize);

            if (query is not IOrderedQueryable<T>)
                query = query.OrderBy(item => item.ID);

            if (PageIndex > 0)
                query = query.Skip(PageIndex * PageSize);

            query = query.Take(PageSize);

            var items = await query.ToArrayAsync(Cancel).ConfigureAwait(false);

            return new Page(items, totalCount, PageIndex, PageSize);
        }

        public async Task<T> Update(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            entity.UpdatedAt= DateTime.UtcNow;

            db.Entry(entity).State = EntityState.Modified;

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }

        #endregion

        #region Page class
        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPaget<T>
        {
            public int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
        }
        #endregion
    }
}
