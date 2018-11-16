using Model;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DateTime _dateTime;
        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dateTime = DateTime.Now;
            
        }
        public bool Add(Category entity)
        {
            try
            {
                entity.CreatedAt = _dateTime;
                _dbContext.Categories.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var model = _dbContext.Categories.First(x => x.Id == id);
                _dbContext.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            var result = new List<Category>();
            try
            {
                result = _dbContext.Categories.ToList();
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public Category GetById(int id)
        {
            var result = new Category();
            try
            {
                result = _dbContext.Categories.First(x => x.Id == id);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public bool Update(Category entity)
        {
            try
            {
                var model = _dbContext.Categories.First(x => x.Id == entity.Id);
                model.Img = entity.Img;
                model.UpdateAt = _dateTime;
                model.Descripcion = entity.Descripcion;
                model.Area = entity.Area;
                _dbContext.Categories.Update(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
