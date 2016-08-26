using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WcfService2
{
    public class CustomPrincipal: IPrincipal
    {
        IIdentity IPrincipal.Identity
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool IPrincipal.IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public CustomPrincipal(IIdentity client)
        {

        }
    }
}