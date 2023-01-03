using AntroStop.DAL.Context;
using AntroStop.DAL.Entities;
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
    public class ElementRepository<T> : IElemetAdditionalRepository<T> where T : Element, new()
    {
        #region Поля и Свойства
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
        /// <summary>
        /// Метод создания нарушения
        /// </summary>
        /// <param name="entity">Нарушение</param>
        /// <param name="Cancel"></param>
        /// <returns>Нарушение</returns>
        /// <exception cref="ArgumentNullException">Аргумент равен NULL</exception>
        public async Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            if (entity == null) throw new ArgumentNullException();

            await db.AddAsync(entity, Cancel).ConfigureAwait(false);

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }
        
        /// <summary>
        /// Метод подсчета всех нарушений
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns>Возвращает количество нарушений</returns>
        public async Task<int> Count(CancellationToken Cancel = default)
        {
            return await Items.CountAsync(Cancel).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод удаления нарушения
        /// </summary>
        /// <param name="ID">ID в формате GUID для удаления</param>
        /// <param name="Cancel"></param>
        /// <returns>Возвращает удаленное нарушение</returns>
        public  async Task<T> Delete(Guid ID, CancellationToken Cancel = default)
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

        /// <summary>
        /// Метод проверки существования ID в базе данных
        /// </summary>
        /// <param name="ID">ID в формате GUID</param>
        /// <param name="Cancel"></param>
        /// <returns>Возвращает TRUE, если существует. FALSE - объект не найден в БД</returns>
        public async Task<bool> ExistID(Guid ID, CancellationToken Cancel = default)
        {
            return await Items.AnyAsync(item => item.Id == ID, Cancel).ConfigureAwait(false);
        }

        /// <summary>
        /// Метод возврата объекта по ID
        /// </summary>
        /// <param name="ID">ID в формате GUID</param>
        /// <param name="Cancel"></param>
        /// <returns>Возвращает найденное нарушение</returns>
        public async Task<T> Get(Guid ID, CancellationToken Cancel = default)
        {
            return await Items.FirstOrDefaultAsync(item => item.Id == ID, Cancel).ConfigureAwait(false);
        }

        /// <summary>
        /// Возвращает ысе нарушения
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns>Коллекция нарушений</returns>
        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default)
        {
            return await Items.ToArrayAsync(Cancel).ConfigureAwait(false);
        }


        /// <summary>
        /// Производит обновление нарушений
        /// </summary>
        /// <param name="entity">Нарушение</param>
        /// <param name="Cancel"></param>
        /// <returns>Обновленное нарушение</returns>
        /// <exception cref="ArgumentNullException">Аргумент равен NULL</exception>
        public async Task<T> Update(T entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            db.Entry(entity).State = EntityState.Modified;

            await db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            return entity;
        }

        public async Task<IEnumerable<T>> GetAllByID(Guid Id, CancellationToken Cancel = default)
        {
            return await Items.Where(violation => violation.Violation.Id == Id).ToArrayAsync(Cancel).ConfigureAwait(false);
        }
        #endregion
    }
}
