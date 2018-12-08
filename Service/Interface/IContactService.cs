using Model;
using Model.Vm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IContactService
    {
        IEnumerable<ContactVm> GetById(string id);
        bool Add(Contact model);
        bool Delete(int id);

        IEnumerable<ContactVm> GetByIdMyContacts(int id);
        bool Exist(ValidateContact model);
    }
}
