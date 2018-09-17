using Microsoft.EntityFrameworkCore;
using SmartHR.Domain.Entities;
using SmartHR.Domain.Interfaces;
using SmartHR.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SmartHR.Infra.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private SmartHRContext _context;

        public RepositoryBase(SmartHRContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

        public void Inserir(TEntity obj)
        {
            DbSet.Add(obj);
            _context.SaveChanges();
        }

        public void InserirRange(List<TEntity> objs)
        {
            DbSet.AddRange(objs);
            _context.SaveChanges();
        }

        public void Atualizar(TEntity obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            DbSet.Remove(Obter(id));
            _context.SaveChanges();
        }

        public IList<TEntity> Obter()
        {
            return DbSet.ToList();
        }

        public TEntity Obter(int id)
        {
            return DbSet.Find(id);
        }

        public TEntity Obter(Expression<Func<TEntity, bool>> condicao)
        {
            return DbSet.Where(condicao).FirstOrDefault();
        }

        public TEntity Obter(Expression<Func<TEntity, bool>> condicao, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null && includes.Length != 0)
            {
                var q = DbSet.Include(includes.First());
                includes.Skip(1).ToList().ForEach(p => q = q.Include(p));
                return q.SingleOrDefault(condicao);
            }

            return DbSet.SingleOrDefault(condicao);
        }

        public IEnumerable<TEntity> ObterTodos(Expression<Func<TEntity, bool>> condicao)
        {
            return DbSet.Where(condicao);
        }

        public IQueryable<TEntity> ObterTodos(Expression<Func<TEntity, bool>> condicao, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null && includes.Length != 0)
            {
                var q = DbSet.Include(includes.First());

                foreach (var property in includes.Skip(1))
                {
                    q = q.Include(property);
                }

                return condicao != null ?
                    q.Where(condicao).AsQueryable() :
                    q.AsQueryable();
            }

            var retorno = condicao != null ?
                    DbSet.Where(condicao).AsQueryable() :
                    DbSet.AsQueryable();

            return retorno;
        }

        public bool Excluir(TEntity model)
        {
            var entry = _context.Entry(model);

            DbSet.Attach(model);

            entry.State = EntityState.Deleted;

            return (_context.SaveChanges() > 0);
        }

        public bool Excluir(Expression<Func<TEntity, bool>> condicao)
        {
            var model = DbSet.Where(condicao).FirstOrDefault();

            return (model != null) && Excluir(model);
        }
    }
}
