using Model;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class FreeelancerHabilityService : IFreelancerHabilityService
    {
        private readonly ApplicationDbContext _dbContext;
        public FreeelancerHabilityService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(FreelancerHability model)
        {
            try
            {
                _dbContext.FreelancerHabilities.Add(model);
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
                var model = _dbContext.FreelancerHabilities.First(x => x.HabilityId == id);
                _dbContext.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteByFreelancerAndHability(int freelancerId, int habilityId)
        {
            try
            {
                var model = _dbContext.FreelancerHabilities.First(x => x.HabilityId == habilityId && x.FreelancerId ==  freelancerId);
                _dbContext.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Exist(int freelancerId, int habilityId)
        {
            try
            {
                var model = _dbContext.FreelancerHabilities.First(x => x.HabilityId == habilityId && x.FreelancerId == freelancerId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<FreelancerHability> GetAll()
        {
            throw new NotImplementedException();
        }

        public FreelancerHability GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(FreelancerHability entity)
        {
            try
            {
                var model = _dbContext.FreelancerHabilities.First(x => x.FreelancerId == entity.FreelancerId);
                model.HabilityId = entity.HabilityId;
                _dbContext.Update(model);
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
