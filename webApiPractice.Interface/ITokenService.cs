using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiPractice.Contracts;

namespace webApiPractice.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
