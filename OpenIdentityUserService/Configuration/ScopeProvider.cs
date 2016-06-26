using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;

namespace OpenIdentityCustomService.Configuration
{
    public class ScopeProvider : IScopeStore
    {
        private List<Scope> _appScopes = Scopes.Get().ToList();
        public async Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null)
                throw new ArgumentNullException("scopeNames");

            var scopes = from s in _appScopes
                         where scopeNames.ToList().Contains(s.Name)
                         select s;

            return await Task.FromResult<IEnumerable<Scope>>(scopes.ToList());
        }

        public async Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            if (publicOnly)
            {
                return await Task.FromResult(_appScopes.Where(s => s.ShowInDiscoveryDocument));
            }

            return await Task.FromResult(_appScopes);
        }
    }
}
