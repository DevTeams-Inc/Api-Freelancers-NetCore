using Model;
using Model.Vm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IRatingService
    {
        bool Add(Rating model);
        int Rating(int id);
        IEnumerable<RatingVm> GetById(int id);
    }
}
