using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IFreelancerHabilityService : IRepository<FreelancerHability>
    {
        bool Add(FreelancerHability model);
    }
}
