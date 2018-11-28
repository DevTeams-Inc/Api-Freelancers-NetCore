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

        public IEnumerable<UserVm> GetAll()
        {
            var result = new List<UserVm>();
            try
            {
                var model = _dbContext.ApplicationUsers.ToList();
                foreach(var i in model)
                {
                    UserVm user = new UserVm
                    {
                        Name = i.Name,
                        LastName = i.LastName,
                        Email = i.Email
                    };
                    result.Add(user);
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
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
