using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace OpenIdentityCustomService.Configuration
{

    public static class CustomStandardScopes
    {
        /// <summary>
        /// All identity scopes.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// All.
        /// 
        /// </value>
        public static IEnumerable<Scope> All
        {
            get
            {
                return (IEnumerable<Scope>)new Scope[5]
                {
          StandardScopes.OpenId,
          StandardScopes.Profile,
          StandardScopes.Email,
          StandardScopes.Phone,
          StandardScopes.Address
                };
            }
        }

        /// <summary>
        /// All identity scopes (always include claims in token).
        /// 
        /// </summary>
        /// 
        /// <value>
        /// All always include.
        /// 
        /// </value>
        public static IEnumerable<Scope> AllAlwaysInclude
        {
            get
            {
                return (IEnumerable<Scope>)new Scope[5]
                {
          StandardScopes.OpenId,
          StandardScopes.ProfileAlwaysInclude,
          StandardScopes.EmailAlwaysInclude,
          StandardScopes.PhoneAlwaysInclude,
          StandardScopes.AddressAlwaysInclude
                };
            }
        }

        /// <summary>
        /// Gets the "openid" scope.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The open identifier.
        /// 
        /// </value>
        public static Scope OpenId
        {
            get
            {
                return new Scope()
                {
                    Name = "openid",
                    Required = true,
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>()
          {
            new ScopeClaim("sub", true)
          }
                };
            }
        }

        /// <summary>
        /// Gets the "profile" scope.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The profile.
        /// 
        /// </value>
        public static Scope Profile
        {
            get
            {
                return new Scope()
                {
                    Name = "profile",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    IncludeAllClaimsForUser = true,
                    //true means will add claims of profile
                    Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["profile"], (Func<string, ScopeClaim>)(claim => new ScopeClaim(claim, true))))

                    //  Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["profile"], (Func<string, ScopeClaim>) (claim => new ScopeClaim(claim, false))))
                };
            }
        }

        /// <summary>
        /// Gets the "profile" scope (always include claims in token).
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The profile always include.
        /// 
        /// </value>
        public static Scope ProfileAlwaysInclude
        {
            get
            {
                return new Scope()
                {
                    Name = "profile",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["profile"], (Func<string, ScopeClaim>)(claim => new ScopeClaim(claim, true))))
                };
            }
        }

        /// <summary>
        /// Gets the "email" scope.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The email.
        /// 
        /// </value>
        public static Scope Email
        {
            get
            {
                return new Scope()
                {
                    Name = "email",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["email"], (Func<string, ScopeClaim>)(claim => new ScopeClaim(claim, false))))
                };
            }
        }

        /// <summary>
        /// Gets the "email" scope (always include claims in token).
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The email always include.
        /// 
        /// </value>
        public static Scope EmailAlwaysInclude
        {
            get
            {
                return new Scope()
                {
                    Name = "email",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["email"], (Func<string, ScopeClaim>)(claim => new ScopeClaim(claim, true))))
                };
            }
        }

        /// <summary>
        /// Gets the "phone" scope.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The phone.
        /// 
        /// </value>
        public static Scope Phone
        {
            get
            {
                return new Scope()
                {
                    Name = "phone",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["phone"], (Func<string, ScopeClaim>)(claim => new ScopeClaim(claim, false))))
                };
            }
        }

        /// <summary>
        /// Gets the "phone" scope (always include claims in token).
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The phone always include.
        /// 
        /// </value>
        public static Scope PhoneAlwaysInclude
        {
            get
            {
                return new Scope()
                {
                    Name = "phone",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["phone"], (Func<string, ScopeClaim>)(claim => new ScopeClaim(claim, true))))
                };
            }
        }

        /// <summary>
        /// Gets the "address" scope.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The address.
        /// 
        /// </value>
        public static Scope Address
        {
            get
            {
                return new Scope()
                {
                    Name = "address",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["address"], (Func<string, ScopeClaim>)(claim => new ScopeClaim(claim, false))))
                };
            }
        }

        /// <summary>
        /// Gets the "address" scope (always include claims in token).
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The address always include.
        /// 
        /// </value>
        public static Scope AddressAlwaysInclude
        {
            get
            {
                return new Scope()
                {
                    Name = "address",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = Enumerable.ToList<ScopeClaim>(Enumerable.Select<string, ScopeClaim>(Constants.ScopeToClaimsMapping["address"], (Func<string, ScopeClaim>)(claim => new ScopeClaim(claim, true))))
                };
            }
        }

        /// <summary>
        /// Gets the "all_claims" scope.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// All claims.
        /// 
        /// </value>
        public static Scope AllClaims
        {
            get
            {
                return new Scope()
                {
                    Name = "all_claims",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    IncludeAllClaimsForUser = true
                };
            }
        }

        /// <summary>
        /// Gets the "roles" scope.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The roles.
        /// 
        /// </value>
        public static Scope Roles
        {
            get
            {
                return new Scope()
                {
                    Name = "roles",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = new List<ScopeClaim>()
          {
            new ScopeClaim("role", false)
          }
                };
            }
        }

        /// <summary>
        /// Gets the "roles" scope (always include claims in token).
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The roles always include.
        /// 
        /// </value>
        public static Scope RolesAlwaysInclude
        {
            get
            {
                return new Scope()
                {
                    Name = "roles",
                    Type = ScopeType.Identity,
                    Emphasize = true,
                    Claims = new List<ScopeClaim>()
          {
            new ScopeClaim("role", true)
          }
                };
            }
        }

        /// <summary>
        /// Gets the "offline_access" scope.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The offline access.
        /// 
        /// </value>
        public static Scope OfflineAccess
        {
            get
            {
                return new Scope()
                {
                    Name = "offline_access",
                    Type = ScopeType.Resource,
                    Emphasize = true
                };
            }
        }
    }
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
                {
                    ////////////////////////
                    // identity scopes
                    ////////////////////////

                    StandardScopes.OpenId,
                    CustomStandardScopes.Profile,
                    StandardScopes.Email,
                    StandardScopes.Address,
                    StandardScopes.OfflineAccess,
                    StandardScopes.RolesAlwaysInclude,
                    StandardScopes.AllClaims,

                    ////////////////////////
                    // resource scopes
                    ////////////////////////

                    new Scope
                    {
                        Name = "read",
                        DisplayName = "Read data",
                        Type = ScopeType.Resource,
                        Emphasize = false,

                        ScopeSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        },

                    },
                    new Scope
                    {
                        Name = "write",
                        DisplayName = "Write data",
                        Type = ScopeType.Resource,
                        Emphasize = true,

                        ScopeSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        }
                    },
                    new Scope
                    {
                        Name = "idmgr",
                        DisplayName = "IdentityManager",
                        Type = ScopeType.Resource,
                        Emphasize = true,
                        ShowInDiscoveryDocument = false,

                        Claims = new List<ScopeClaim>
                        {
                            new ScopeClaim(Constants.ClaimTypes.Name),
                            new ScopeClaim(Constants.ClaimTypes.Role)
                        }
                    }
                };
        }
    }
}
