using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Vm
{
    public class ProposalVm
    {
        public int Id { get; set; }
        public int ProyectId { get; set; }
        public string ApplicationUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Offer { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
    }
}
