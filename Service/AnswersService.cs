using Model;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AnswersService : IAnswersService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DateTime _dateTime;
        public AnswersService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dateTime = DateTime.Now;
        }
        public bool Add(Answer entity)
        {
            try
            {
                entity.CreatedAt = _dateTime;
                _dbContext.Answers.Add(entity);
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
            throw new NotImplementedException();
        }

        public IEnumerable<Answer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Answer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Answer entity)
        {
            throw new NotImplementedException();
        }
    }
}
