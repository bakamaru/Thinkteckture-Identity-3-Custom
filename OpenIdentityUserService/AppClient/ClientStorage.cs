using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;

namespace OpenIdentityCustomService.AppClient
{
   public  class ClientStorage: IClientStore
    {

        private List<Client> appClients=AppClients.Current(); 
       public  Task<Client> FindClientByIdAsync(string clientId)
       {
           return Task.FromResult(appClients.SingleOrDefault(u => u.ClientId == clientId));
       }
    }
}
