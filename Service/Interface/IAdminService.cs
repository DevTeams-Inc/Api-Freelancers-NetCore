using Model.Vm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IAdminService
    {
        int ProjectsPublics();
        int FreelancersRegisters();
        int TotalOfCategories();
        int TotalOfHabilities();
        IEnumerable<FreelancerVm> BestFreelancer();
    }
}
