using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbContext _DbContext;
		internal DbSet<T> DbSet;

		public Repository(DbContext dbContext)
		{
			_DbContext = dbContext;
			DbSet = _DbContext.Set<T>();
			// DbSet == _DbContext.Categories
			//DbSet.Add() == _DbContext.Categories.Add()
		}
		public bool AutoSaveChanges { get; set; } = true;
		public virtual IQueryable<T> Items => DbSet;

		public IEnumerable<T> GetAll() => Items.ToList();

		public T Get(Expression<Func<T, bool>> filter) => Items.Where(filter).FirstOrDefault() 
			?? throw new ArgumentOutOfRangeException("NOTHING");

		public void Add(T entity) => DbSet.Add(entity);

		public void Delete(T entity) => DbSet.Remove(entity);

		public void DeleteRange(IEnumerable<T> entities) => DbSet.RemoveRange(entities);
	}
}
