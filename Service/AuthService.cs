using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Model;
using Repository;
using Service.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Service
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public ApplicationUser GetById(string id)
        {
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

        public bool SendEmail(UserVm model)
        {
            try
            {
                var md = _dbContext.ApplicationUsers.First(x => x.Email == model.Email);


                MailMessage mssg = new MailMessage();
                mssg.To.Add(model.Email);
                mssg.Subject = "Confirmacion de Correo";
                mssg.SubjectEncoding = Encoding.UTF8;
                mssg.Bcc.Add(model.Email);


                string body = string.Empty;
                using (StreamReader reader = new StreamReader("wwwroot//index.html"))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{id}", md.Id);

                mssg.Body = body;
                mssg.BodyEncoding = Encoding.UTF8;
                mssg.IsBodyHtml = true;
                mssg.From = new MailAddress("orbisalonzo25@gmail.com");

                SmtpClient user = new SmtpClient();
                user.Credentials = new NetworkCredential("orbisalonzo25@gmail.com" , "alonzo25");
                user.Port = 587;
                user.EnableSsl = true;
                user.Host = "smtp.gmail.com";

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
            catch (Exception e)
            {
                return false;
            }
        }
        //actualizamos el campo emailConfirmed y con esto confirmamos que esta registrado el usuario
        public bool ValidateEmail(string id)
        {
            try
            {
                var model = _dbContext.ApplicationUsers.First(x => x.Id == id);
                model.EmailConfirmed = true;
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //valida si el usuario ha confirmado el email
        public bool ValidateUser(string email)
        {
            var model = new ApplicationUser();
                model = _dbContext.ApplicationUsers.Single(x => x.Email == email);
            //el 1 representa que el email ha sido confirmado (true)
                if (model.EmailConfirmed == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
    }
}
