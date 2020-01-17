using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using NorthwindApi.Model.Context;
using NorthwindApi.Model.Entity;

namespace NorthwindApi.Model.Repository
{ 
    public interface ICategoryRepository
    {
        Task<int> Save(Category obj);
        Task<int> Update(Category obj);
        Task<int> Delete(Category obj);

        Task<Category> GetById(int id);
        Task<List<Category>> GetAll();
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _context;

        public CategoryRepository(ILoggerFactory loggerFactory, IDbContext context)
        {
            _logger = loggerFactory.CreateLogger<CategoryRepository>();
            _context = context;
        }

        public Task<List<Category>> GetAll()
        {
            IEnumerable<Category> list = new List<Category>();

            try
            {
                list = _context.Conn.GetAllAsync<Category>().Result;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(list.ToList());
        }

        public Task<Category> GetById(int id)
        {
            Category obj = null;

            try
            {
                obj = _context.Conn.GetAsync<Category>(id).Result;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(obj);
        }

        public Task<int> Save(Category obj)
        {
            var result = 0;

            try
            {
                result = _context.Conn.InsertAsync<Category>(obj).Result;
                if (result > 0) result = obj.CategoryId;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(result);
        }

        public Task<int> Update(Category obj)
        {
            var result = 0;

            try
            {
                result = _context.Conn.UpdateAsync<Category>(obj).Result ? 1 : 0;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(result);
        }

        public Task<int> Delete(Category obj)
        {
            var result = 0;

            try
            {
                result = _context.Conn.DeleteAsync<Category>(obj).Result ? 1 : 0;
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(result);
        }
    }
}
