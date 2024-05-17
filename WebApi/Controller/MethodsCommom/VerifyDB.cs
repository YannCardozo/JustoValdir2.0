using Infra.Configuração;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controller.MethodsCommom
{
    public class VerifyDB
    {
        //metodo para validar operações de banco e deixar a api menos verbosa
        public static async Task<bool> VerificarCpfExistente(ContextBase context, string cpf)
        {
            var usuarioComCpf = await context.Users.AnyAsync(u => u.CPF == cpf);

            return usuarioComCpf;
        }
    }
}
