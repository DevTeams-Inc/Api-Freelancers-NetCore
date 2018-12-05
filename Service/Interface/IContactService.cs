using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IContactService
    {
        IEnumerable<Contact> GetById(string id);
        bool Add(Contact model);
    }
}
