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
        private readonly IFreelancerHabilityService _habilityService;
        private readonly DateTime _dateTime;
        public FreelancerService(ApplicationDbContext dbContext
            , IFreelancerHabilityService habilityService)
        {
            _dbContext = dbContext;
            _habilityService = habilityService;
            _dateTime = DateTime.Now;
        }
        public bool AddFreelancerAndHability(FreelancerVm entity)
        {
            try
            {
                //preparamos el model del freelancer
                var freelancer = new Freelancer {

                    CreatedAt = _dateTime,
                    ApplicationUserId = entity.ApplicationUserId,
                    Biography = entity.Biography,
                    Historial = entity.Historial,
                    Interest = entity.Interest,
                    Lenguaje = entity.Lenguaje,
                    Level = entity.Level,
                    PriceHour = entity.PriceHour,
                    Rating = entity.Rating,
                    Testimony = entity.Testimony            
                };

                //agregamos el freelancer
                _dbContext.Add(freelancer);
                _dbContext.SaveChanges();

           
                FreelancerHability model;
                //buscamos el ultimo id del freelancer
                //e iteramos en el listado de habilidades y los vamos agregando a la tabla
                var id =  _dbContext.Freelancers.Max(x => x.Id);
                foreach(var i in entity.Habilities)
                {
                    model = new FreelancerHability
                    {
                        FreelancerId = id,
                        HabilityId = i.Id
                    };
                    _habilityService.Add(model);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Add(Freelancer entity)
        {
            throw new NotImplementedException();
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

        public IndexVm<FreelancerVm> GetAll(int page = 1)
        {
            var result = new IndexVm<FreelancerVm>();
            var freelancer = new List<FreelancerVm>();
            try
            {
                var quantityOfPerson = 10;
                var model =
                     _dbContext.Freelancers.OrderBy(x => x.Id)
                     .Include(x => x.ApplicationUser)
                     .Include(x => x.Habilities)                
                     .Skip((page - 1) * quantityOfPerson)
                     .Take(quantityOfPerson).ToList();

                /*
                *en cada iteracion va a desglosar habilities para traer la 
                * entidad hija que es hability y de esta forma incluirla en la respuesta
                */
                foreach (var i in model)
                {
                    var n = i.Habilities;
                    /*
                     * creamos una lista para agregarsela al modelo freelancerVm para solo enviar
                     * la lista
                     */
                    var h = new List<Hability>();
                    foreach (var j in n)
                    {
                         h.Add(_dbContext.Habilities.First(x => x.Id == j.HabilityId));
                    }
                    /*
                     * creamos un nuevo modelo para personalizar la informacion enviada al usuarios
                     * de esta manera evitamos enviar toda la informacion del usuario 
                     * y solo enviamos la necesaria
                     */
                    var d = new FreelancerVm()
                    {
                        Id = i.Id,
                        Lenguaje = i.Lenguaje,
                        PriceHour = i.PriceHour,
                        Biography = i.Biography,
                        Interest = i.Interest,
                        Level = i.Level,
                        Historial = i.Historial,
                        Rating = i.Rating,
                        Testimony = i.Testimony,
                        LastName = i.ApplicationUser.LastName,
                        Name = i.ApplicationUser.Name,
                        Avatar = i.ApplicationUser.Avatar,
                        Address = i.ApplicationUser.Address,
                        Habilities = h
                    };
                    freelancer.Add(d);
                }
                var totalOfRegister = model.Count();
                result.Entities = freelancer;
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

        public FreelancerVm Profile(int id)
        {
            var result = new FreelancerVm();
            var fh = new List<FreelancerHability>();
            try
            {

                var freelancer = _dbContext.Freelancers.Single(x => x.Id == id);

                fh = _dbContext.FreelancerHabilities.Where(x => x.FreelancerId == id).ToList();

                //result.Freelancer = freelancer;
                
                foreach(var i in fh)
                {

                }

            }
            catch (Exception)
            {

                throw;
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
