using Model;
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

        public IEnumerable<Contact> GetById(string id)
        {
            var result = new List<Contact>();
            try
            {
                result = _dbContext.Contacts.Where(x => x.ApplicationUserId == id).ToList();
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
