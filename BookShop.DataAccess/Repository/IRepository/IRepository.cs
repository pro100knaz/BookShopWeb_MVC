﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T - Category
		IEnumerable<T> GetAll();
		T Get(Expression<Func<T, bool>> filter); //чтобы использовать лямбда
		void Add(T entity);
		//void Update(T entity);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);




	}
}
