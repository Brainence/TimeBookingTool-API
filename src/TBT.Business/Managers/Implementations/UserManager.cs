using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TBT.Business.Helpers;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.Components.Interfaces.Logger;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class UserManager : CrudManager<User, UserModel>, IUserManager
    {
        #region Constructors

        public UserManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.Users, objectMapper, configurationProvider, logger)
        {
        }

        #endregion

        #region Interface Members

        public UserModel GetByEmail(string email)
        {
            //try
            //{
                return ObjectMapper.Map<User, UserModel>(
                     UnitOfWork.Users.GetByEmail(email));
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {email}");
            //    return null;
            //}
        }

        public async Task<List<UserModel>> GetByProjectAsync(int projectId)
        {
            //try
            //{
                return ObjectMapper.Map<IQueryable<User>, List<UserModel>>(
                     await UnitOfWork.Users.GetByProjectAsync(projectId));
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {projectId}");
            //    return null;
            //}
        }

        public override Task<int> AddAsync(UserModel model)
        {
            //try
            //{
                model.Password = PasswordHelpers.HashPassword(model.Password);
                var result = base.AddAsync(model);
                model.Password = string.Empty;
                return result;
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {model.ToString()}");
            //    return null;
            //}
        }

        public override async Task UpdateAsync(UserModel model)
        {
            //try
            //{
                var user = await Repository.GetAsync(model.Id);

                if (user != null) model.Password = user.Password;

                await Repository.DetachAsync(
                    await Repository.GetAsync(model.Id));

                await Repository.UpdateAsync(ObjectMapper.Map<UserModel, User>(model));

                await UnitOfWork.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {model.ToString()}");
            //}
        }

        public async Task<bool> IsPasswordValid(int userId, string password)
        {
            //try
            //{
                return await UnitOfWork.Users.IsPasswordValid(userId, password);
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameters: userId={userId}; password={password}");
            //    return false;
            //}
        }

        public async Task ChangePassword(int userId, string oldPassword, string newPassword)
        {
            //try
            //{
                await UnitOfWork.Users.ChangePassword(userId, oldPassword, newPassword);

                await UnitOfWork.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameters: userId={userId}; oldPassword={oldPassword}; newPassword={newPassword}");
            //}
        }

        #endregion
    }
}
