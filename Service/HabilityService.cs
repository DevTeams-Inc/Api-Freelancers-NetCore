using Model;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class HabilityService : IHabilityService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DateTime _dateTime;
        public HabilityService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dateTime = DateTime.Now;
        }

        public bool Add(Hability entity)
        {
            try
            {
                entity.CreatedAt = _dateTime;
                _dbContext.Add(entity);
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
                var model = _dbContext.Habilities.First(x => x.Id == id);
                            _dbContext.Habilities.Remove(model);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Hability> GetAll()
        {
            var model = new List<Hability>();
            try
            {
                model = _dbContext.Habilities.ToList();
            }
            catch (Exception)
            {
                model = null;
            }
            return model;
        }

        public Hability GetById(int id)
        {
            var result = new Hability();
            try
            {
                result = _dbContext.Habilities.First(x => x.Id == id);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public bool Update(Hability entity)
        {
            try
            {
                var model = _dbContext.Habilities.First(x => x.Id == entity.Id);
                model.Image = entity.Image;
                model.Title = entity.Title;
                model.UpdateAt = _dateTime;
                model.Description = entity.Description;

                _dbContext.Habilities.Update(model);
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
