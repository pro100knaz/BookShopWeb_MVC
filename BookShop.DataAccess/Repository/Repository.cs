using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _DbContext;
		internal DbSet<T> DbSet;

		public Repository(ApplicationDbContext dbContext)
		{
			_DbContext = dbContext;
			DbSet = _DbContext.Set<T>();
			// DbSet == _DbContext.Categories
			//DbSet.Add() == _DbContext.Categories.Add()
			_DbContext.Products.Include(u => u.Category);
		}
		public bool AutoSaveChanges { get; set; } = true;
		public virtual IQueryable<T> Items => DbSet;

		//Category, CoverType - то что нужно включить
		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = DbSet;
			if(!string.IsNullOrEmpty(includeProperties))
			{
				foreach(var includeProp in includeProperties
					.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return query.ToList();
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = DbSet;
			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.FirstOrDefault()
			?? throw new ArgumentOutOfRangeException("NOTHING");
		}

		public void Add(T entity) => DbSet.Add(entity);

		public void Delete(T entity) => DbSet.Remove(entity);

		public void DeleteRange(IEnumerable<T> entities) => DbSet.RemoveRange(entities);
	}
}
