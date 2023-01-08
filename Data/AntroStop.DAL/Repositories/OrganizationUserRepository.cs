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
    public class OrganizationUserRepository<T> : IIntRepository<T> where T : OrganizationUser, new()
    {
        #region Поля и Свойства
        private readonly DataDB db;
        protected DbSet<T> Set { get; }
        protected virtual IQueryable<T> Items => Set;
        #endregion
        public OrganizationUserRepository(DataDB db)
        {
            this.db = db;
            Set = db.Set<T>();
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

        public async Task<T> Update(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            entity.UpdatedAt = DateTime.UtcNow;

            db.Entry(entity).State = EntityState.Modified;

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }
        #endregion
    }
}
