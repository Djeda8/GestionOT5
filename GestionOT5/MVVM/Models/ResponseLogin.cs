using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.MVVM.Models
{
    public class ResponseLogin
    {
        public bool LoginOk { get; set; }
        public string? Message { get; set; }
        public User User { get; set; }
    }
}
