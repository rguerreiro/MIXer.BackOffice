using System;
using Nancy.Authentication.Forms;

namespace Mixer.BackOffice.Infrastructure.Authentication
{
    public class UsernameMapper : IUsernameMapper
    {
        #region Methods
        public string GetUsernameFromIdentifier(Guid indentifier)
        {
            return "Rodrigo";
        }
        #endregion
    }
}