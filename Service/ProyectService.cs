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
    public class ProyectService : IProyectService
    {
        private readonly ApplicationDbContext _dbContext;
        private DateTime _datetime;

        public ProyectService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _datetime = DateTime.Now;
        }

        public bool Add(ProyectVm entity)
        {
            try
            {
                var model = new Proyect
                {
                    ApplicationUserId = entity.ApplicationUserId,
                    Title = entity.Title,
                    Description = entity.Description,
                    Required_Skill = entity.Required_Skill,
                    Scope = entity.Scope,
                    Price = entity.Price,
                    CategoryId = entity.CategoryId,
                    CreatedAt = _datetime   
                };
                _dbContext.Add(model);
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
                var model = _dbContext.Proyects.First(x => x.Id == id);
                _dbContext.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        public IndexVm<ProyectVm> GetAll(int page = 1)
        {
            var result = new IndexVm<ProyectVm>();
            var proyects = new List<ProyectVm>();
            try
            {
                int quantityOfProyects = 6;
                var model = _dbContext.Proyects.OrderBy(x => x.CreatedAt)
                           .Include(x => x.ApplicationUser)
                           .Include(x => x.Category)
                           .Include(x => x.Proposal)
                           .Skip((page - 1) * quantityOfProyects)
                           .Take(quantityOfProyects).ToList();

                foreach (var i in model)
                {
                    var proposals = new List<ProposalVm>();
                    foreach (var j in i.Proposal)
                    {
                        var vmProposal = new ProposalVm
                        {
                            Id = j.Id,
                            ApplicationUserId = j.ApplicationUserId,
                            CreatedAt = j.CreatedAt,
                            Description = j.Description,
                            Offer = j.Offer,
                            ProyectId = j.ProyectId,
                            Title = j.Title,
                            UserName = j.ApplicationUser.Name + " " + j.ApplicationUser.LastName

                        };
                        proposals.Add(vmProposal);
                    }

                    var vm = new ProyectVm
                    {
                        Id = i.Id,
                        Title = i.Title,
                        ApplicationUserId = i.ApplicationUserId,
                        Avatar = i.ApplicationUser.Avatar,
                        CategoryId = i.CategoryId,
                        NameCategory = i.Category.Name,
                        CreatedAt = i.CreatedAt,
                        Description = i.Description,
                        Price = i.Price,
                        //recuerda que mandaras una string
                        Required_Skill = i.Required_Skill,
                        Scope = i.Scope,
                        Proposal = proposals,
                        UserName = i.ApplicationUser.Name + " " + i.ApplicationUser.LastName
                    };
                    proyects.Add(vm);
                }

                var totalOfRegister = proyects.Count();
                result.Entities = proyects;
                result.ActualPage = page;
                result.RegisterByPage = quantityOfProyects;
                result.TotalOfRegister = totalOfRegister;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public ProyectVm GetById(int id)
        {
            var result = new ProyectVm();
            try
            {
                var model = _dbContext.Proyects
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Category)
                    .First(x => x.Id == id);
                result = new ProyectVm
                {
                    Id = model.Id,
                    Title = model.Title,
                    ApplicationUserId = model.ApplicationUserId,
                    Avatar = model.ApplicationUser.Avatar,
                    CategoryId = model.CategoryId,
                    NameCategory = model.Category.Name,
                    CreatedAt = model.CreatedAt,
                    Description = model.Description,
                    Price = model.Price,
                    Required_Skill = model.Required_Skill,
                    Scope = model.Scope,
                    UserName = model.ApplicationUser.Name + " " + model.ApplicationUser.LastName

                };
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public bool Update(ProyectVm entity)
        {
            try
            {
                var model = _dbContext.Proyects.First(x => x.Id == entity.Id);
                model.Price = entity.Price;
                model.Title = entity.Title;
                model.Scope = entity.Scope;
                model.Description = entity.Description;
                model.UpdateAt = _datetime;
                model.CategoryId = entity.CategoryId;
                _dbContext.Proyects.Update(model);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProyectVm> GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
