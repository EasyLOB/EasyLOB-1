using Microsoft.AspNet.Identity;
using System;
using EasyLOB.Security.Data;
using EasyLOB.Identity;
using EasyLOB.Library;
using EasyLOB.Persistence;

namespace EasyLOB.Security.Persistence
{
    public class SecurityUserRepository : SecurityGenericRepositoryEF<User>
    {
        #region Methods

        public SecurityUserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override bool Create(ZOperationResult operationResult, User entity)
        {
            try
            {
                ApplicationUser user = new ApplicationUser { UserName = entity.UserName, Email = entity.Email, EmailConfirmed = true };
                IdentityResult identityResult = IdentityHelperEF.UserManager.Create(user, entity.PasswordHash);
                if (!identityResult.Succeeded)
                {
                    operationResult.ParseIdentityResult(identityResult);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        public override bool Delete(ZOperationResult operationResult, User entity)
        {
            try
            {
                ApplicationUser user = IdentityHelperEF.UserManager.FindById(entity.Id);
                if (user != null)
                {
                    IdentityResult identityResult = IdentityHelperEF.UserManager.Delete(user);
                    if (!identityResult.Succeeded)
                    {
                        operationResult.ParseIdentityResult(identityResult);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        public override bool Update(ZOperationResult operationResult, User entity)
        //public override async void Update(ZOperationResult operationResult, User entity)
        {
            try
            {
                ApplicationUser user = IdentityHelperEF.UserManager.FindById(entity.Id);

                user.Email = entity.Email;
                IdentityResult validEmail = new IdentityResult();
                //validEmail = await IdentityHelperEF.UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    operationResult.ParseIdentityResult(validEmail);
                }

                IdentityResult validPassword = new IdentityResult();
                if (!String.IsNullOrEmpty(entity.PasswordHash))
                {
                    //validPassword = await IdentityHelperEF.UserManager.PasswordValidator.ValidateAsync(entity.PasswordHash);
                    if (validPassword.Succeeded)
                    {
                        user.PasswordHash = IdentityHelperEF.UserManager.PasswordHasher.HashPassword(entity.PasswordHash);
                    }
                    else
                    {
                        operationResult.ParseIdentityResult(validPassword);
                    }
                }

                if (validEmail.Succeeded && validPassword.Succeeded)
                {
                    IdentityResult identityResult = IdentityHelperEF.UserManager.Update(user);
                    if (!identityResult.Succeeded)
                    {
                        operationResult.ParseIdentityResult(identityResult);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        #endregion Methods
    }
}