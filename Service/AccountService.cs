using Model;
using Model.Vm;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        public AccountService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool UpdateByFreelancer(UpdateByFreelancerUserVm entity)
        {
            try
            {
                var model = _dbContext.ApplicationUsers.Single(x => x.Id == entity.Id);
                model.Avatar = entity.Avatar;
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
