using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BookHaven_DataBaseAccess.Repository.IRepository
{
    // Паттерн Репозиторий используется для того, чтобы можно было менять источник данных (использовать несколько БД)
    //абстрагироваться от конкретных подключений к источникам данных, с которыми работает программа, и является промежуточным звеном между классами,непосредственно взаимодействующими с данными, и остальной программой.
    // https://habr.com/ru/articles/335856/
    public interface IRepository<T> where T : class
    {
        // T - Categiry
        //IEnumerable<T> GetAll(string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        //T GetFirstOrDefault();
        T Get(Expression<Func<T,bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
