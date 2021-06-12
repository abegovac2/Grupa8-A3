using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Utils
{
    public interface IMailSender
    {
        public void Send(string name, string email, string message);
    }
}
