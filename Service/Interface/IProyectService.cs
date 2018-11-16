using Model;
using Model.Vm;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IProyectService : IRepository<ProyectVm> , IRepositoryPagination<ProyectVm>
    {
    }
}
