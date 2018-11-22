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
            var result = new Proposal();
            try
            {
                result = _dbContext.Proposals
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Answers)
                    .First(x => x.Id == id);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public bool Update(Proposal entity)
        {
            try
            {
                var model = _dbContext.Proposals.First(x => x.Id == entity.Id);
                model.Offer = entity.Offer;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.UpdateAt = _dateTime;
                _dbContext.Proposals.Update(model);
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
                var model = _dbContext.Proposals.First(x => x.Id == id);
                _dbContext.Proposals.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ProposalVm GetbyIdVm(int id)
        {
            ProposalVm result;
            try
            {
                var model = _dbContext.Proposals
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Answers)
                    .First(x => x.Id == id);

                result = new ProposalVm
                {
                        Id = model.Id,
                        ApplicationUserId = model.ApplicationUserId,
                        CreatedAt = model.CreatedAt,
                        Description = model.Description,
                        Offer = model.Offer,
                        ProyectId = model.ProyectId,
                        Title = model.Title,
                        UserName = model.ApplicationUser.Name + " " + model.ApplicationUser.LastName,
                        Answers = model.Answers

                };
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
    }
}
