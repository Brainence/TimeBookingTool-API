using System.Collections.Generic;
using System.Linq;
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
using System;
using TBT.Business.Infrastructure.CastleWindsor;
using System.Net.Mail;
using TBT.Business.EmailService.Interfaces;
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
            return ObjectMapper.Map<User, UserModel>(UnitOfWork.Users.GetByEmail(email));
        }

        public async Task<List<UserModel>> GetByCompanyIdAsync(int companyId)
        {
            return ObjectMapper.Map<IQueryable<User>, List<UserModel>>(
                     await UnitOfWork.Users.GetByCompanyId(companyId));
            //var users = ObjectMapper.Map<IQueryable<User>, List<UserModel>>(
            //         await UnitOfWork.Users.GetByCompanyId(companyId));
            //foreach (var user in users)
            //{
            //    if (user.CompanyId.HasValue)
            //    {
            //        user.Company = new CompanyModel()
            //        {
            //            Id = user.CompanyId.Value
            //        };
            //    }
            //    foreach (var project in user.Projects)
            //    {
            //        project.Activities.Clear();
            //    }
            //}
            //return users;
        }

        public override Task<int> AddAsync(UserModel model)
        {
            model.Password = PasswordHelpers.HashPassword(model.Password);
            var result = base.AddAsync(model);
            model.Password = string.Empty;
            return result;
        }

        public override async Task UpdateAsync(UserModel model)
        {
            var user = await Repository.GetAsync(model.Id);

            if (user != null) model.Password = user.Password;

            await Repository.DetachAsync(
                await Repository.GetAsync(model.Id));

            await Repository.UpdateAsync(ObjectMapper.Map<UserModel, User>(model));

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsPasswordValid(int userId, string password)
        {
            return await UnitOfWork.Users.IsPasswordValid(userId, password);
        }

        public async Task ChangePassword(int userId, string oldPassword, string newPassword)
        {
            await UnitOfWork.Users.ChangePassword(userId, oldPassword, newPassword);

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> SendEmail(EmailData data)
        {
            var sender = UnitOfWork.Users.GetByEmail(data.Email);
            if (sender is null)
            {
                return false;
            }

            var emailService = ServiceLocator.Current.Get<IEmailService>();
            var emailMessage = new MailMessage
            {
                From = new MailAddress(sender.Username, sender.Username),
                Subject = $"{data.Type}     {data.Date}",
                Priority = MailPriority.Normal,
                Body = $@"<p>Employee: {sender.FirstName} {sender.LastName} </p>
                       <p>Date: {data.Date}</p> 
                       <p>Comment: {data.Text}</p>",
                BodyEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true
            };

            emailMessage.To.Add(Constants.SmtpSettingsConstants.DefaultSmtpSettings.Username);

            return await emailService.SendMailAsync(emailMessage);
        }

        #endregion
    }
}
