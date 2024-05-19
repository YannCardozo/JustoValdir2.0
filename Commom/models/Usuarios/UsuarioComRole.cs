using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commom.models.Usuarios
{
    public class UsuarioComRole
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public IList<string> Roles { get; set; }
    }
}
