using Microsoft.EntityFrameworkCore;
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
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly string _imgServer = $"http://192.168.137.103:45455/img/";
        //private readonly string _imgServer = $"http://192.168.96.117:45455/img/";

        //private readonly string _imgServer = $"http://192.168.1.139:45455/img/";
        public AdminService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;       
        }

        public int FreelancersRegisters()
        {
            int result = 0;
            try
            {
                result = _dbContext.Freelancers.ToList().Count();
            }
            catch (Exception)
            {

                result = 0;
            }

            return result;
        }

        public int ProjectsPublics()
        {
            int result = 0;

            try
            {
                result = _dbContext.Proyects.ToList().Count();
            }
            catch (Exception)
            {

                result = 0;
            }
            return result;
        }
        
        public int TotalOfCategories()
        {
            int result = 0;

            try
            {
                result = _dbContext.Categories.ToList().Count();
            }
            catch (Exception)
            {

                result = 0;
            }
            return result;
        }

        public int TotalOfHabilities()
        {
            int result = 0;

            try
            {
                result = _dbContext.Habilities.ToList().Count();
            }
            catch (Exception)
            {

                result = 0;
            }
            return result;
        }

        public IEnumerable<FreelancerVm> BestFreelancer()
        {
            var model = new List<Freelancer>();
            var result = new List<FreelancerVm>();
            try
            {
                model = _dbContext.Freelancers
                        .Include(x => x.ApplicationUser)
                        .OrderByDescending(x => x.Rating)
                        .Take(5).ToList();

                foreach (var i in model)
                {
                    var freelancer = new FreelancerVm
                    {
                        ApplicationUserId = i.ApplicationUserId,
                        Avatar = $"{_imgServer}{i.ApplicationUser.Avatar}",
                        Name = i.ApplicationUser.Name,
                        LastName = i.ApplicationUser.LastName,
                        Rating = i.Rating
                    };
                    result.Add(freelancer);
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
    }
}
