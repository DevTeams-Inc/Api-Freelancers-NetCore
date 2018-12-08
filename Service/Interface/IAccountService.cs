using Model;
using Model.Vm;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IAccountService
    {
        bool UpdateByFreelancer(UpdateByFreelancerUserVm entity);
        bool Exist(string id);
        bool Delete(string id);
        bool RecoveryPass(string email , string idUser);
        ApplicationUser GetByEmail(string email);
        ApplicationUser GetById(string id);
    }
}
