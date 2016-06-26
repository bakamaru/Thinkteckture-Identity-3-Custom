using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core;
using SageFrame.Security.Entities;

namespace OpenIdentityCustomService.User
{

    public class SfUserDetail
    {
        public bool AcceptedEula { get; set; }
        public string Subject
        {
            get { return this.UserID; }
        }
        public List<Claim> Claims { get; set; }
        public string UserID { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ResPhone { get; set; }
        public string Mobile { get; set; }
        public string CountryCode { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string WebSite { get; set; }
        public DateTime? DateOfBirth { get;  set; }
        public string PostCode { get; set; }
    }
    public class SfUser
    {

        public bool AcceptedEula { get; set; }
        public string Subject
        {
            get { return this.UserID.ToString(); }
        }
        public List<Claim> Claims { get; set; }

        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Get or set user first name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Get or set user last name.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Get or set salt password.
        /// </summary>
        public string PasswordSalt { get; set; }
        /// <summary>
        /// Get or set email/
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Get or set password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Get or set security question.
        /// </summary>
        public string SecurityQuestion { get; set; }
        /// <summary>
        /// Get or set security answer.
        /// </summary>
        public string SecurityAnswer { get; set; }
        /// <summary>
        /// Get or set true for approved.
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Get or set current date.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Get or set 1 for unique email.
        /// </summary>
        public int UniqueEmail { get; set; }
        /// <summary>
        /// Get or set  <see cref="T:SageFrame.Security.Enums.PasswordFormats"/> 
        /// </summary>
        public int PasswordFormat { get; set; }

        /// <summary>
        /// Get or set added date time.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Get or set UserID.
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// Get or set true for active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Get or set last activity date.
        /// </summary>
        public DateTime LastActivityDate { get; set; }
        /// <summary>
        /// Get or set last activity date.
        /// </summary>
        public DateTime LastLoginDate { get; set; }
        /// <summary>
        /// Get or set last password change date.
        /// </summary>
        public DateTime LastPasswordChangeDate { get; set; }
        /// <summary>
        /// Get or set current email.
        /// </summary>
        public string EmailCurrent { get; set; }
        /// <summary>
        /// Get or set true for user existence.
        /// </summary>
        public bool UserExists { get; set; }
        /// <summary>
        /// Get or set list of RoleInfo class.
        /// </summary>
        public List<RoleInfo> LSTRoles { get; set; }
        /// <summary>
        /// Get or set role name.
        /// </summary>
        public string RoleNames { get; set; }





    }
}
