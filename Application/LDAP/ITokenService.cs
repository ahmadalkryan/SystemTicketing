using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LDAP
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        void AddToBlackListToken(string token);
        bool IsBlackListToken(string token);
    }
}
