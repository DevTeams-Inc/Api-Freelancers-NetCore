using Model;
using Model.Vm;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IFreelancerService : IRepository<Freelancer>
    {
       IndexVm<FreelancerVm> GetAll(int page = 1);
       FreelancerVm Profile(int id);
       bool AddFreelancerAndHability(FreelancerVm entity);


    }
}
