using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Vm
{
    public class IndexVM<T> : BaseModel where T : class   
    {
        public List<T> Entities { get; set; }
    }
}
