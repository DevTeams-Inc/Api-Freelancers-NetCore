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
        bool DeleteByFreelancerAndHability(int freelancerId, int habilityId);
        bool Exist(int freelancerId, int habilityId);
    }
}
