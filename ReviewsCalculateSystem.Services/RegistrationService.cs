using ReviewsCalculateSystem.Models;
using ReviewsCalculateSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReviewsCalculateSystem.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ReviewDbContext db;
        public RegistrationService()
        {
            db = new ReviewDbContext();
        }

        public JsonResult UserRegistration(Registration model)
        {
            var Key = SendMail.GetUniqueKey();
            model.ActivationKey = Key;
            db.Registrations.Add(model);
            db.SaveChanges();
            SendMail.Mail(model);
            return new JsonResult
            {
                Data = "IsOk",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UserRegistrationConfirm(int Id, string Key)
        {
            int noOfRowUpdated = db.Database.ExecuteSqlCommand($"UPDATE Registrations SET IsActive='{true}' where RegistrationId={Id} and ActivationKey='{Key}'");
            if (noOfRowUpdated > 0)
            {
                return new JsonResult
                {
                    Data = "IsOk",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = "NotOk",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

    public interface IRegistrationService
    {
        JsonResult UserRegistration(Registration model);
        JsonResult UserRegistrationConfirm(int Id, string Key);
    }














    public static class SendMail
    {
        public static void Mail(Registration model)
        {
            
            string link = "http://localhost:49786/api/admin/RegistrationConfirm?Id="+model.RegistrationId+"&Key="+ model.ActivationKey;


            string mailBodyhtml = "<html><body><h1>Registration Confirm Email</h1><a href='http://localhost:49786/api/admin/RegistrationConfirm?Id="+model.RegistrationId+ "&Key="+model.ActivationKey + "'"+">Click Here</a></body></html>";
            var msg = new MailMessage("testm0559@gmail.com", model.Email,"Mail Confirmation", mailBodyhtml);
            msg.To.Add(model.Email);
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587); //if your from email address is "from@hotmail.com" then host should be "smtp.hotmail.com"**
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("testm0559@gmail.com", "testmail@#123");
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
            Console.WriteLine("Email Sended Successfully");
        }

        public static string GetUniqueKey()
        {
            int size = 10;
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
