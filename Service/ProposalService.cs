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
    public class ProposalService : IProposalService
    {
        private readonly ApplicationDbContext _dbContext;
        private DateTime _dateTime;
        public ProposalService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dateTime = DateTime.Now;
        }

        public bool Add(Proposal entity)
        {
            try
            {
                entity.CreatedAt = _dateTime;
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Proposal> GetAll()
        {
            var result = new List<Proposal>();
            try
            {
                result = _dbContext.Proposals.ToList();
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public IEnumerable<ProposalVm> GetAllVm()
        {
            var result = new List<ProposalVm>();
            try
            {

                var model = _dbContext.Proposals
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Answers)
                    .Include(x => x.Proyect).ToList();

                foreach (var i in model)
                {
                    var vm = new ProposalVm
                    {
                        Id = i.Id,
                        Address = i.ApplicationUser.Address,
                        ApplicationUserId = i.ApplicationUserId,
                        CreatedAt = i.CreatedAt,
                        Description = i.Description,
                        Offer = i.Offer,
                        ProyectId = i.ProyectId,
                        Title = i.Title,
                        UserName = i.ApplicationUser.Name + " " + i.ApplicationUser.LastName
                    };

                    result.Add(vm);
                }


            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public Proposal GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Proposal entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
