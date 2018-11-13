using Model;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
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
    }
}
