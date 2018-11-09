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
       IndexVM<Freelancer> GetAll(int page = 1);
    }
}
