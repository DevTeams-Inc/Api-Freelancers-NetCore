using Microsoft.EntityFrameworkCore;
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
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly string _spa = "http://localhost:8080/";
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
                model.Address = entity.Address;
                model.PhoneNumber = entity.PhoneNumber;
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 

        public bool Exist(string id)
        {
            try
            {
                _dbContext.ApplicationUsers.First(x => x.Id == id);
                try
                {
                    var model = _dbContext.Freelancers.Include(x => x.ApplicationUser).First(x => x.ApplicationUser.Id == id);
                        return false;
                }
                catch (Exception)
                {
                    return true;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var model = _dbContext.ApplicationUsers.First(x => x.Id == id);
                _dbContext.ApplicationUsers.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RecoveryPass(string email , string idUser)
        {
            try
            {
                MailMessage mssg = new MailMessage();
                mssg.To.Add(email);
                mssg.Subject = "Restablecer Contraseña";
                mssg.SubjectEncoding = Encoding.UTF8;
                mssg.Bcc.Add(email);
                string body = $"Para restablecer la contraseña pulse enlace <br> <a href='{_spa}reset/password/{idUser}'>Restablecer</a>";
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

        public ApplicationUser GetByEmail(string email)
        {
            var model = new ApplicationUser();
            try
            {
                model = _dbContext.ApplicationUsers.First(x => x.Email == email);
            }
            catch (Exception)
            {
                model = null;
            }

            return model;
        }

        public ApplicationUser GetById(string id) {

            var model = new ApplicationUser();
            try
            {
                model = _dbContext.ApplicationUsers.First(x => x.Id == id);
            }
            catch (Exception)
            {
                model = null;
            }

            return model;

        }
    }
}
