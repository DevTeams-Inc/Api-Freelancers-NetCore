using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IAuthService
    {
        ApplicationUser GetByEmail(string email);
        bool SendEmail(UserVm model);
        bool ValidateEmail(string id);
        bool ValidateUser(string email);
        ApplicationUser GetById(string id);
    }
}
