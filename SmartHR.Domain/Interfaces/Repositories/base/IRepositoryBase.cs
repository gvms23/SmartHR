using SmartHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SmartHR.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Inserir(TEntity obj);

        void InserirRange(List<TEntity> obj);

        void Atualizar(TEntity obj);

        void Excluir(int id);

        bool Excluir(Expression<Func<TEntity, bool>> condicao);

        bool Excluir(TEntity model);

        TEntity Obter(int id);

        IList<TEntity> Obter();

        TEntity Obter(Expression<Func<TEntity, bool>> condicao);

        TEntity Obter(Expression<Func<TEntity, bool>> condicao, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> ObterTodos(Expression<Func<TEntity, bool>> condicao);

        IQueryable<TEntity> ObterTodos(Expression<Func<TEntity, bool>> condicao, params Expression<Func<TEntity, object>>[] includes);
        
    }
}

