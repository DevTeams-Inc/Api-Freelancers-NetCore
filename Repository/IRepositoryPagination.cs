using Model.Vm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IRepositoryPagination<TEntity > where TEntity  : class
    {
        IndexVm<TEntity> GetAll(int page = 1);
    }
}
