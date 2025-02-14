using System.Security.Claims;
using ControleGasto.Dados;

namespace ControleGasto
{
    public class Util
    {
        private readonly Dados.DB _db;

        public Util(Dados.DB db)
        {
            _db = db;
        }

        public int BuscaUsuario(ClaimsPrincipal user)
        {
            var uniqueName = user.Identity?.Name; // Obtendo o nome do usuário autenticado

            if (string.IsNullOrEmpty(uniqueName))
            {
                return 0;
            }

            var usuario = _db.Usuarios.FirstOrDefault(p => p.Usuario == uniqueName);

            return usuario?.Id ?? 0;
        }
    }
}
