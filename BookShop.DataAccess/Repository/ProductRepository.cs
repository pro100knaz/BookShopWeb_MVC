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

		public void Update(Product obj)
		{
			dbContext.Products.Update(obj); 
			//var objFromDb = dbContext.Products.FirstOrDefault(u => u.Id == obj.Id);
			//if (objFromDb != null)
			//{
			//	objFromDb.Title = obj.Title;
			//	objFromDb.ISBN = obj.ISBN;
			//	objFromDb.Price = obj.Price;
			//	objFromDb.Price50 = obj.Price50;
			//	objFromDb.ListPrice = obj.ListPrice;
			//	objFromDb.Price100 = obj.Price100;
			//	objFromDb.Description = obj.Description;
			//	objFromDb.CategoryId = obj.CategoryId;
			//	objFromDb.Author = obj.Author;
			//	objFromDb.ImageUrl = obj.ImageUrl;
			//	if (obj.ImageUrl != null)
			//	{
			//		objFromDb.ImageUrl = obj.ImageUrl;
			//	}
			//}
		}
	}
}
