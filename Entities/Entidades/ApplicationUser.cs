using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades
{
    public class ApplicationUser : IdentityUser
    {

        //aqui implementamos a coluna USR_CPF para a tabela asp net users
        [Column("USR_CPF")]
        public string CPF { get; set; }

        [Column("USR_ADM")]
        public bool Administrador { get; set; }
        [Column("USR_ADV")]
        public bool Advogado { get; set; }
        [Column("USR_REFRESHTOKEN")]
        public string? RefreshToken { get; set; }
        [Column("USR_REFRESHTOKENEXPIRYTIME")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
