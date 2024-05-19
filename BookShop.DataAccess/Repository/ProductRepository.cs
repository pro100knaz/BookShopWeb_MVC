using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using BookShop.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DataAccess.Repository
{
	internal class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext dbContext;

		public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Update(Product product)
		{
			dbContext.Products.Update(product); 
		}
	}
}
