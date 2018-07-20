﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using TBT.Business.EmailService.Interfaces;
using TBT.Business.Helpers;
using TBT.Business.Implementations;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.Logger;
using TBT.Components.Interfaces.ObjectMapper;
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
            return ObjectMapper.Map<User, UserModel>(UnitOfWork.Users.GetByEmail(email));
        }

        public async Task<List<UserModel>> GetByCompanyIdAsync(int companyId)
        {
            return ObjectMapper.Map<IQueryable<User>, List<UserModel>>(
                await UnitOfWork.Users.GetByCompanyId(companyId));
        }

        public override Task<int> AddAsync(UserModel model)
        {
            model.Password = PasswordHelpers.HashPassword(model.Password);
            var resultId = base.AddAsync(model);
            model.Password = "";
            return resultId;
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
            if (sender == null)
            {
                return false;
            }
            var builder = new StringBuilder(File.ReadAllText(HostingEnvironment.MapPath(@"~/Templates/EmailTemplates/AbsenceTemplate.html")));
            builder.Replace(Constants.MailConstants.FirstName, sender.FirstName);
            builder.Replace(Constants.MailConstants.LastName, sender.LastName);
            builder.Replace(Constants.MailConstants.Time, data.Date);
            builder.Replace(Constants.MailConstants.Mesage, data.Text);
            var emailMessage = new MailMessage
            {
                From = new MailAddress(sender.Username, sender.Username),
                Subject = $"{data.Type}     {data.Date}",
                Priority = MailPriority.Normal,
                Body = builder.ToString(),
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            emailMessage.To.Add(Constants.SmtpSettingsConstants.DefaultSmtpSettings.Username);
            return await ServiceLocator.Current.Get<IEmailService>().SendMailAsync(emailMessage);
        }

        #endregion
    }
}
