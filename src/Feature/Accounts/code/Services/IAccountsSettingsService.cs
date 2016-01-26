﻿namespace Sitecore.Feature.Accounts.Services
{
  using System.Net.Mail;
  using Sitecore.Data;
  using Sitecore.Data.Items;

  public interface IAccountsSettingsService
  {
    string GetPageLink(Item contextItem, ID fieldID);
    MailMessage GetForgotPasswordMailTemplate();
    string GetPageLinkOrDefault(Item contextItem, ID field, Item defaultItem);
    ID GetRegistrationOutcome(Item contextItem);
  }
}