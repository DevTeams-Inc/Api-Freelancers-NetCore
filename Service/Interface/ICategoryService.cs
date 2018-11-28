using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface ICategoryService : IRepository<Category>
    {
        IEnumerable<Category> GetTree();
    }
}
