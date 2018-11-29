using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Vm
{
    public class FreelancerAdminVm : UserVm
    {
        //la propiedad heredada que es id = applicationUserId
        //esta propiedad es la del freelancer 
        public int FreelancerId { get; set; }
    }
}
