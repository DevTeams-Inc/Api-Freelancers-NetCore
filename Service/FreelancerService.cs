using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class FreelancerService : IFreelancerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DateTime _dateTime;
        public FreelancerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dateTime = DateTime.Now;
        }
        public bool Add(Freelancer entity)
        {
            try
            {  
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
                var model = _dbContext.Freelancers.First(x => x.Id == id);
                _dbContext.Freelancers.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IndexVM<Freelancer> GetAll(int page = 1)
        {
            var result = new IndexVM<Freelancer>();
            try
            {
               var quantityOfPerson = 10;

                var model = _dbContext.Freelancers.OrderBy(x => x.CreatedAt)
                     .Include(x => x.ApplicationUser)
                     .Skip((page - 1) * quantityOfPerson)
                     .Take(quantityOfPerson).ToList();

                var totalOfRegister = _dbContext.Freelancers.Count();

                result.Entities = model;
                result.ActualPage = page;
                result.TotalOfRegister = totalOfRegister;
                result.RegisterByPage = quantityOfPerson;
               
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public Freelancer GetById(int id)
        {
            var result = new Freelancer();
            try
            {
                result = _dbContext.Freelancers
                    .Include(x => x.ApplicationUser)
                    .First(x => x.Id == id);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public bool Update(Freelancer entity)
        {
            try
            {
                var model = _dbContext.Freelancers.
                    First(x => x.Id == entity.Id);
                model.Lenguaje = entity.Lenguaje;
                model.Biography = entity.Biography;
                model.PriceHour = entity.PriceHour;
                model.Interest = entity.Interest;
                model.Historial = entity.Historial;
                model.Testimony = entity.Testimony;
                model.UpdateAt = _dateTime;
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
