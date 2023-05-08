﻿using AntroStop.DAL.Context;
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
    public class ElementRepository<T> : IElementRepository<T> where T : Element, new()
    {
        #region Свойства
        private readonly DataDB db;
        protected DbSet<T> Set { get; }
        protected virtual IQueryable<T> Items => Set;
        #endregion

        public ElementRepository(DataDB db)
        {
            this.db = db;
            this.Set = this.db.Set<T>();
        }

        #region Методы
        public async Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            if (entity == null || entity.ViolationID == Guid.Empty) throw new ArgumentNullException();

            var violation = db.Violations.Where(i => i.Id == entity.ViolationID).FirstOrDefault();

            if (violation == null) throw new ArgumentNullException();

            entity.Violation = violation;
            entity.Id = Guid.Empty;
            entity.CreatedAt = entity.UpdatedAt = DateTime.UtcNow;

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
            return await Items.Include(v => v.Violation).FirstOrDefaultAsync(item => item.Id == ID, Cancel).ConfigureAwait(false);
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
            return await Items.Include(v => v.Violation).ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllByID(Guid Id, CancellationToken Cancel = default)
        {
            return await Items.Where(x => x.ViolationID == Id).Include(v => v.Violation).ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<T> Update(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.Violation = null;

            db.Entry(entity).State = EntityState.Modified;

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }

        public Task<PagedList<T>> GetPage(PageParametrs productParameters, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
