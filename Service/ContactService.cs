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
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DateTime _dateTime;
        public ContactService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dateTime = DateTime.Now;
        }

        public bool Add(Contact model)
        {
            try
            {
                model.CreatedAt = _dateTime;
                _dbContext.Contacts.Add(model);
                _dbContext.SaveChanges();
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public IEnumerable<ContactVm> GetById(string id)
        {
            var result = new List<ContactVm>();
            try
            {
                var model = _dbContext.Contacts.Include(x => x.ApplicationUser).
                                             Include(x => x.Freelancer.ApplicationUser).Where(x => x.ApplicationUserId == id).ToList();
                foreach (var i in model)
                {
                    ContactVm contact = new ContactVm
                    {
                        Id = i.Id,
                        ApplicationUserId = i.Freelancer.ApplicationUserId,
                        FullName = $"{i.Freelancer.ApplicationUser.Name} {i.Freelancer.ApplicationUser.LastName}"
                    };
                    result.Add(contact);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public IEnumerable<ContactVm> GetByIdMyContacts(int id)
        {
            var result = new List<ContactVm>();
            try
            {
                var model = _dbContext.Contacts.Include(x => x.ApplicationUser).Where(x => x.FreelancerId == id).ToList();
                foreach (var i in model)
                {
                    ContactVm contact = new ContactVm
                    {
                        Id = i.Id,
                        ApplicationUserId = i.ApplicationUserId,
                        FullName = $"{i.ApplicationUser.Name} {i.ApplicationUser.LastName}"
                    };
                    result.Add(contact);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public bool Delete(int id)
        {
            try
            {
                var model = _dbContext.Contacts.First(x => x.Id == id);
                _dbContext.Contacts.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Exist(ValidateContact model)
        {
            try
            {
                _dbContext.Contacts.First(x => x.ApplicationUserId == model.UserId || x.FreelancerId == model.FreelancerId);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

    }
}
