using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Venta.Domain.Entities;
using Venta.Domain.Interfaces;
using Venta.Infrastructure.Context;

namespace Venta.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T :  class
    {
        private readonly ApplicationDbContext _context;
        DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Agregar  objeto generico 
        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);
            await Save();
        }

        public async Task Delete(T id)
        {
            var obj = await _context.Set<T>().FindAsync(id);

            if (obj != null)
            {
                _context.Set<T>().Remove(obj);
                await Save();
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser null.");
            }

            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;

        }
        public async Task DeleteById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await Save();
            }
        }
    }
}

