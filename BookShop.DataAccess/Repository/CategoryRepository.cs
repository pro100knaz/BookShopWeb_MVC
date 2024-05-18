using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using BookShop.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _DbContext;
		public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_DbContext = dbContext;
		}

		public void Update(Category category)
		{
			_DbContext.Categories.Update(category);
		}

		public void Save()
		{
			_DbContext.SaveChanges();
		}
	}
}
