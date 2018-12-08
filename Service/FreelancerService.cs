using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model;
using Model.Vm;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Service
{
    public class FreelancerService : IFreelancerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFreelancerHabilityService _freelancerHabilityService;
        private readonly IHabilityService _habilityService;
        private readonly IAccountService _accountService;
        private readonly IContactService _contactService;
        private readonly string _imgServer = $"http://192.168.137.103:45455/img/";
        //private readonly string _imgServer = $"http://192.168.1.139:45455/img/";
        //private readonly string _imgServer = $"http://192.168.96.117:45455/img/";

        private readonly DateTime _dateTime;
        public FreelancerService(ApplicationDbContext dbContext,
            IFreelancerHabilityService freelancerHabilityService, 
            IHabilityService habilityService, 
            IAccountService accountService,
            IContactService contactService)
        {
            _dbContext = dbContext;
            _freelancerHabilityService = freelancerHabilityService;
            _habilityService = habilityService;
            _accountService = accountService;
            _dateTime = DateTime.Now;
            _contactService = contactService;
        }

        public bool Add(FreelancerVm entity)
        {
            try
            {
                UpdateByFreelancerUserVm user = new UpdateByFreelancerUserVm
                {
                    Id = entity.ApplicationUserId,
                    PhoneNumber = entity.PhoneNumber,
                    Avatar = entity.Avatar,
                    Address = entity.Address
                };
                _accountService.UpdateByFreelancer(user);

                //preparamos el model del freelancer
                var freelancer = new Freelancer {

                    CreatedAt = _dateTime,
                    ApplicationUserId = entity.ApplicationUserId,
                    Biography = entity.Biography,
                    Address = entity.Address,
                    Interest = entity.Interest,
                    Lenguaje = entity.Lenguaje,
                    Level = entity.Level,
                    PriceHour = entity.PriceHour,
                    Rating = entity.Rating,
                    Lat = entity.Lat,
                    Long = entity.Long,
                    Profesion = entity.Profesion
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
                if (_accountService.Delete(model.ApplicationUserId))
                {
                    _dbContext.Freelancers.Remove(model);
                    _dbContext.SaveChanges();
                }
                
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
                        Address = i.ApplicationUser.Address,
                        Rating = i.Rating,
                        Profesion = i.Profesion,
                        LastName = i.ApplicationUser.LastName,
                        Name = i.ApplicationUser.Name,
                        Avatar = $"{_imgServer}{i.ApplicationUser.Avatar}",
                        Email = i.ApplicationUser.Email,
                        Habilities = h,
                        Lat = i.Lat,
                        Long = i.Long,
                        PhoneNumber = i.ApplicationUser.PhoneNumber,
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
            catch (Exception e)
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
                user.PhoneNumber = entity.PhoneNumber;
                user.LastName = entity.LastName;

                _dbContext.ApplicationUsers.Update(user);
                //freelancer
                freelancer.Lenguaje = entity.Lenguaje;
                freelancer.Biography = entity.Biography;
                freelancer.PriceHour = entity.PriceHour;
                freelancer.Interest = entity.Interest;
                freelancer.ApplicationUser.Address = entity.Address;
                freelancer.Profesion = entity.Profesion;
                freelancer.UpdateAt = _dateTime;
                freelancer.Long = entity.Long;
                freelancer.Lat = entity.Lat;
                _dbContext.Update(freelancer);
                _dbContext.SaveChanges();

                FreelancerHability freelancerHability;
                foreach (var i in entity.Habilities)
                {
                    //si existe no la agreges si no agregala
                    if(!_freelancerHabilityService.Exist(entity.Id, i.Id))
                    {
                        freelancerHability = new FreelancerHability
                        {
                            FreelancerId = entity.Id,
                            HabilityId = i.Id
                        };
                        _freelancerHabilityService.Add(freelancerHability);
                    }                    
                }


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
                var freelancer = _dbContext.Freelancers
                    .Include(x => x.Habilities)
                    .Single(x => x.Id == id);
                var user = _dbContext.ApplicationUsers.Single(x => x.Id == freelancer.ApplicationUserId);
                result.Id = freelancer.Id;
                result.Lenguaje = freelancer.Lenguaje;
                result.PriceHour = freelancer.PriceHour;
                result.Biography = freelancer.Biography;
                result.Interest = freelancer.Interest;
                result.Level = freelancer.Level;
                result.Address = user.Address;
                result.Rating = freelancer.Rating;
                result.Profesion = freelancer.Profesion;
                result.Name = user.Name;
                result.LastName = user.LastName;
                result.Avatar = $"{_imgServer}{user.Avatar}";
                result.Lat = freelancer.Lat;
                result.Long = freelancer.Long;
                result.Email = user.Email;
                result.PhoneNumber = user.PhoneNumber;
                result.ApplicationUserId = user.Id;
                var h = new List<Hability>();
                var r = new List<Rating>();
                foreach (var i in freelancer.Habilities)
                {
                    h.Add(_habilityService.GetById(i.HabilityId));
                }
                result.Habilities = h;
 
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

        public FreelancerVm GetByIdUser(string id)
        {
            var result = new FreelancerVm();
            try
            {
                var freelancer = _dbContext.Freelancers
                    .Include(x => x.Habilities)
                    .First(x => x.ApplicationUserId == id);

                var user = _dbContext.ApplicationUsers.Single(x => x.Id == id);

                result.Id = freelancer.Id;
                result.Lenguaje = freelancer.Lenguaje;
                result.PriceHour = freelancer.PriceHour;
                result.Biography = freelancer.Biography;
                result.Interest = freelancer.Interest;
                result.Level = freelancer.Level;
                result.Address = freelancer.ApplicationUser.Address;
                result.Rating = freelancer.Rating;
                result.Profesion = freelancer.Profesion;
                result.Name = user.Name;
                result.LastName = user.LastName;
                result.Avatar = $"{_imgServer}{user.Avatar}";
                result.Lat = freelancer.Lat;
                result.Long = freelancer.Long;
                result.Email = user.Email;
                result.PhoneNumber = user.PhoneNumber;
                result.ApplicationUserId = user.Id;
                var h = new List<Hability>();

                foreach (var i in freelancer.Habilities)
                {
                    h.Add(_habilityService.GetById(i.HabilityId));
                }
                result.Habilities = h;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public IEnumerable<FreelancerVm> GetTree()
        {
            var model = new List<Freelancer>();
            var result = new List<FreelancerVm>();
            try
            {
                model = _dbContext.Freelancers
                        .Include(x => x.Habilities)
                        .Include(x => x.ApplicationUser)
                        .OrderByDescending(x => x.Rating)
                        .Take(3).ToList();

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

        public IEnumerable<FreelancerVm> Search(string parameter)
        {
            var result = new List<FreelancerVm>();
            try
            {
                var freelancer = _dbContext.Freelancers
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Habilities)
                    .Where(x => 
                    x.ApplicationUser.Name.Contains(parameter) || 
                    x.ApplicationUser.LastName.Contains(parameter) ||
                    x.Profesion.Contains(parameter) ||
                    x.Interest.Contains(parameter)).ToList();
                /*
                 * Si se encuentra un freelancer entonces lo añadira 
                 */
                if (freelancer.Count() != 0)
                {
                    foreach (var i in freelancer)
                    {
                        ApplicationUser user = _dbContext.ApplicationUsers.
                            Single(x => x.Id == i.ApplicationUserId);

                        var h = new List<Hability>();
                        foreach (var j in i.Habilities)
                        {
                            h.Add(_habilityService.GetById(j.HabilityId));
                        }

                        var model = new FreelancerVm
                        {
                            Id = i.Id,
                            Lenguaje = i.Lenguaje,
                            PriceHour = i.PriceHour,
                            Biography = i.Biography,
                            Interest = i.Interest,
                            Level = i.Level,
                            Address = i.ApplicationUser.Address,
                            Rating = i.Rating,
                            Profesion = i.Profesion,
                            Name = user.Name,
                            LastName = user.LastName,
                            Avatar = $"{_imgServer}{user.Avatar}",
                            Lat = i.Lat,
                            Long = i.Long,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            ApplicationUserId = user.Id,
                            Habilities = h
                        };

                        result.Add(model);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public IEnumerable<FreelancerAdminVm> GetAllAdmin()
        {
            var result = new List<FreelancerAdminVm>();
            try
            {
                var model = _dbContext.Freelancers
                    .Include(x => x.ApplicationUser).ToList();
                foreach (var i in model)
                {
                    FreelancerAdminVm user = new FreelancerAdminVm
                    {
                        Id = i.ApplicationUser.Id,
                        FreelancerId = i.Id,
                        Name = i.ApplicationUser.Name,
                        LastName = i.ApplicationUser.LastName,
                        Email = i.ApplicationUser.Email,
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

        public bool UserExist(string id)
        {
            try
            {
                var model = _dbContext.Freelancers
                            .Include(x => x.ApplicationUser)
                            .First(x => x.ApplicationUser.Id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<FreelancerMapVm> GetAllMap()
        {
            var result = new List<FreelancerMapVm>();
            try
            {
                var model = _dbContext.Freelancers.Include(x => x.ApplicationUser).ToList();

                FreelancerMapVm freelancer;
                foreach(var m in model)
                {
                    freelancer = new FreelancerMapVm
                    {
                        FullName = $"{m.ApplicationUser.Name} {m.ApplicationUser.LastName}",
                        Avatar = $"{_imgServer}{m.ApplicationUser.Avatar}",
                        Id = m.ApplicationUserId,
                        Profession = m.Profesion,
                        Long = m.Long,
                        Lat = m.Lat
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

        public bool Contact(ContactVm model)
        {
            try
            {
                MailMessage mssg = new MailMessage();
                mssg.To.Add(model.EmailDestiny);
                mssg.Subject = "Alguien lo ha contactado";
                mssg.SubjectEncoding = Encoding.UTF8;
                mssg.Bcc.Add(model.EmailDestiny);


                string body = $"Ha sido contactado por {model.FullName}" +
                              $"<br/>" +
                              $"{model.Message}"+
                              $"<br/>"+
                              $"Contactalo por este correo {model.EmailFrom}";
                      
                mssg.Body = body;
                mssg.BodyEncoding = Encoding.UTF8;
                mssg.IsBodyHtml = true;
                mssg.From = new MailAddress("orbisalonzo25@gmail.com");

                SmtpClient user = new SmtpClient
                {
                    Credentials = new NetworkCredential("orbisalonzo25@gmail.com", "alonzo26"),
                    Port = 587,
                    EnableSsl = true,
                    Host = "smtp.gmail.com"
                };

                try
                {
                    user.Send(mssg);
                    if (model.ApplicationUserId != null && model.FreelancerId > 0)
                    {
                        Contact contact = new Contact
                        {
                            ApplicationUserId = model.ApplicationUserId,
                            FreelancerId = model.FreelancerId,
                            CreatedAt = _dateTime
                        };
                        _contactService.Add(contact);
                    }
                    
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<FreelancerVm> Filter(int?idHability, int? rating)
        {
            var result = new List<FreelancerVm>();
            var resultadoF = new List<Freelancer>();
            var freelancers = _dbContext.Freelancers.ToList();
            var resultadoFF = new List<Freelancer>();
            var l = new List<FreelancerHability>();

            bool rate(Freelancer x) => !rating.HasValue || rating.Value == x.Rating;
            var habilidadselecionada = new List<Hability>();
            if (idHability != null)
            {
                foreach (var item in _dbContext.Habilities)
                {
                    var hability = _dbContext.Habilities.FirstOrDefault(x => x.Id == idHability.Value);
                    if (hability != null) { habilidadselecionada.Add(hability); break; }
                }
                if (habilidadselecionada.Count() == 1)
                {
                    foreach (var item in habilidadselecionada)
                    {
                        l = _dbContext.FreelancerHabilities.Where(x => x.HabilityId == item.Id).ToList();
                        foreach (var o in l)
                        {
                            var k = _dbContext.Freelancers.Include(x => x.ApplicationUser).Include(x => x.Habilities).Where(x => x.Id == o.FreelancerId)
                                   .Where(rate).ToList();
                            foreach (var d in k)
                            {
                                resultadoF.Add(d);
                            }
                        }
                    }
                    foreach (var q in resultadoF)
                    {
                        resultadoFF.Add(q);
                    }
                }
            }
            else
            {
                foreach (var g in _dbContext.Freelancers.Include(x => x.ApplicationUser).Include(x => x.Habilities).
                                  Where(rate).ToList())
                {
                    resultadoFF.Add(g);
                }

            }
            foreach (var final in resultadoFF)
            {

                var frelancer = new FreelancerVm
                {
                    ApplicationUserId = final.ApplicationUserId,
                    Id = final.Id,
                    Name = final.ApplicationUser.Name,
                    LastName = final.ApplicationUser.LastName,
                    PriceHour = final.PriceHour,
                    Biography = final.Biography,
                    Avatar = $"{_imgServer}{final.ApplicationUser.Avatar}",
                    Profesion = final.Profesion,
                    Long = final.Long,
                    Lat = final.Lat,
                    Address = final.ApplicationUser.Address,
                    Rating = final.Rating,
                    Email = final.ApplicationUser.Email
                };
                var h = new List<Hability>();
                var a = final.Habilities;
                if (final.Habilities != null)
                {
                    foreach (var i in final.Habilities)
                    {
                        h.Add(_habilityService.GetById(i.HabilityId));

                    }
                }
                frelancer.Habilities = h;

                result.Add(frelancer);
            }


            return result;

        }
        
    }
}
