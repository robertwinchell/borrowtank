using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASOL.HireThings.Service
{
    public class CustomUserStore : IUserStore<IHireThingsUser>, IUserPasswordStore<IHireThingsUser>, IUserEmailStore<IHireThingsUser>
    {
        TempUserDAL _dal;

        public CustomUserStore()
        {
            _dal = new TempUserDAL();
        }

        public Task<IHireThingsUser> FindByNameAsync(string Email)
        {
            TempUserDAL dal = new TempUserDAL();
            IHireThingsUser user = new HireThingsUser() { UserName = Email };
            user = dal.Select(user);
            return Task.FromResult<IHireThingsUser>(user);
        }
        
        public  System.Threading.Tasks.Task CreateAsync(IHireThingsUser user)
        {
            if (user != null)
            {
                TempUserDAL dal = new TempUserDAL();

                //string sdsd=dal.SaveStrAsync(user, CancellationToken.None).Result.ToString();
                //user.Id = sdsd;

                return dal.SaveStrAsync(user, CancellationToken.None);
                
            }
            return Task.FromResult(false);
        }

        public Task<string> GetPasswordHashAsync(IHireThingsUser user)
        {
            return Task.FromResult<string>(user.PINHash.ToString());
        }
        public Task SetPasswordHashAsync(IHireThingsUser user, string passwordHash)
        {
            return Task.FromResult<string>(user.PINHash = passwordHash);
        }


        public Task SendEmailAsync(string userId, string subject, string body)
        {
            return Task.FromResult<bool>(5 == 5);
        }

        public Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            IdentityResult res = IdentityResult.Success;
            return Task.FromResult<IdentityResult>(res);
        }


        #region Not implemented methods
        public System.Threading.Tasks.Task DeleteAsync(IHireThingsUser user)
        {
            return Task.FromResult(0);
        }

        public async System.Threading.Tasks.Task<IHireThingsUser> FindByIdAsync(string userId)
        {
            TempUserDAL dal = new TempUserDAL();            
            return  await dal.SelectByIDAsync(Convert.ToInt64(userId), Convert.ToInt64(userId), CancellationToken.None);
        }

        /// <summary>
        /// Update PINHASH 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public System.Threading.Tasks.Task UpdateAsync(IHireThingsUser user)
        {
            TempUserDAL dal = new TempUserDAL();
            return Task.FromResult<bool>(dal.UpdatePINHash(user));
        }

        public Task<bool> HasPasswordAsync(IHireThingsUser user)
        {
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region IUserEmailStore methods // Static implementation for now

        public Task<IHireThingsUser> FindByEmailAsync(string email)
        {
            TempUserDAL dal = new TempUserDAL();
            IHireThingsUser user = new HireThingsUser() { UserName = email };
            user = dal.Select(user);
            return Task.FromResult<IHireThingsUser>(user);
        }

        public Task<string> GetEmailAsync(IHireThingsUser user)
        {
            return Task.FromResult("true");
        }


        public Task<bool> GetEmailConfirmedAsync(IHireThingsUser user)
        {
            return Task.FromResult(true);
        }

        public Task SetEmailAsync(IHireThingsUser user, string email)
        {
            return Task.FromResult(0);
        }

        public async Task SetEmailConfirmedAsync(IHireThingsUser user, bool confirmed)
        {
            TempUserDAL dal = new TempUserDAL();
            user.IsEmailConfirmed = confirmed;
            await dal.SetEmailConfirmedAsync(user, CancellationToken.None);
            //return Task.FromResult<IHireThingsUser>(user);
        }

        #endregion
    }
}