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
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly string _imgServer = $"http://192.168.137.103:45455/img/";
        //private readonly string _imgServer = $"http://192.168.1.139:45455/img/";
        //private readonly string _imgServer = $"http://192.168.96.117:45455/img/";

        public RatingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Rating model)
        {
            try
            {
                _dbContext.Ratings.Add(model);
                _dbContext.SaveChanges();
                var f = _dbContext.Freelancers.First(x => x.Id == model.FreelancerId);

               int rate =  Rating(model.FreelancerId);

                f.Rating = rate;
                _dbContext.Freelancers.Update(f);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Rating(int id)
        {
            int result = 0;
            try
            {
                int quantityOfPersons = _dbContext.Ratings.Where(x => x.FreelancerId == 1028).ToList().Count();
                var model = _dbContext.Ratings.Where(x => x.FreelancerId == id).ToList();
                int total = 0;  

                foreach (var i in model)
                {
                    total += i.Rate;
                }

                result = total / quantityOfPersons;
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        public IEnumerable<RatingVm> GetById(int id)
        {
            var model = new List<RatingVm>();
            try
            {
                var rating = _dbContext.Ratings.Where(x => x.FreelancerId == id).Include(x => x.ApplicationUser).ToList();
                foreach (var i in rating)
                {
                    RatingVm r = new RatingVm
                    {
                        Id = i.Id,
                        Coments = i.Comment,
                        FullName = i.ApplicationUser.Name +" "+ i.ApplicationUser.LastName,
                        Rating = i.Rate,
                        Avatar = $"{_imgServer}{i.ApplicationUser.Avatar}"
                    };
                    model.Add(r);
                }

            }
            catch (Exception)
            {
                model = null;
            }
            return model;
        }

        
    }
}
