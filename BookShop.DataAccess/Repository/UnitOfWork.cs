using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models.Data;

namespace BookShop.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ICategoryRepository Category { get; private set; }
		public IProductRepository Products { get; private set; }

		private readonly ApplicationDbContext _DbContext;
		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_DbContext = dbContext;
			Category = new CategoryRepository(dbContext);
			Products = new ProductRepository(dbContext);
		}

		public void Save()
		{
			_DbContext.SaveChanges();
		}
	}
}
