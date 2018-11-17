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
        private readonly IFreelancerHabilityService _freelancerHabilityService;
        private readonly IHabilityService _habilityService;

        private readonly DateTime _dateTime;
        public FreelancerService(ApplicationDbContext dbContext
            , IFreelancerHabilityService freelancerHabilityService , 
            IHabilityService habilityService)
        {
            _dbContext = dbContext;
            _freelancerHabilityService = freelancerHabilityService;
            _habilityService = habilityService;
            _dateTime = DateTime.Now;
        }
        public bool Add(FreelancerVm entity)
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
                    _freelancerHabilityService.Add(model);
                }
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

        public IndexVm<FreelancerVm> GetAll(int page = 1)
        {
            var result = new IndexVm<FreelancerVm>();
            var freelancer = new List<FreelancerVm>();
            try
            {
                var quantityOfPerson = 6;
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
                         h.Add(_habilityService.GetById(j.HabilityId));
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
                        Email = i.ApplicationUser.Email,
                        Habilities = h,
                        ApplicationUserId = i.ApplicationUser.Id
                        
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

        public bool Update(FreelancerVm entity)
        {
            try
            {
                var freelancer = _dbContext.Freelancers.
                    First(x => x.Id == entity.Id);
                var user = _dbContext.ApplicationUsers.First(x => x.Id == freelancer.ApplicationUserId);
                //ApplicationUser
                user.Name = entity.Name;
                user.LastName = entity.LastName;
                _dbContext.ApplicationUsers.Update(user);
                //freelancer
                freelancer.Lenguaje = entity.Lenguaje;
                freelancer.Biography = entity.Biography;
                freelancer.PriceHour = entity.PriceHour;
                freelancer.Interest = entity.Interest;
                freelancer.Historial = entity.Historial;
                freelancer.Testimony = entity.Testimony;
                freelancer.UpdateAt = _dateTime;
                _dbContext.Update(freelancer);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public FreelancerVm GetById(int id)
        {
            var result = new FreelancerVm();
            try
            {
                var freelancer = _dbContext.Freelancers.Single(x => x.Id == id);
                var user = _dbContext.ApplicationUsers.Single(x => x.Id == freelancer.ApplicationUserId);
                result.Id = freelancer.Id;
                result.Lenguaje = freelancer.Lenguaje;
                result.PriceHour = freelancer.PriceHour;
                result.Biography = freelancer.Biography;
                result.Interest = freelancer.Interest;
                result.Level = freelancer.Level;
                result.Historial = freelancer.Historial;
                result.Rating = freelancer.Rating;
                result.Testimony = freelancer.Testimony;
                result.Name = user.Name;
                result.LastName = user.LastName;
                result.Avatar = user.Avatar;
                result.Address = user.Address;
                result.Email = user.Email;
                result.ApplicationUserId = user.Id;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public IEnumerable<FreelancerVm> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
