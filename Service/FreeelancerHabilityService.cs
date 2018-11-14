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
