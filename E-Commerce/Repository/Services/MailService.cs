using E_Commerce.Models.VMs;
using E_Commerce.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Mail;


using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MimeKit.Text;
using System.Drawing.Printing;

namespace E_Commerce.Repository.Services
{
    public class MailService : IMailService
    {
        VMEmailSettings Mail_Settings = null;
        public MailService(IOptions<VMEmailSettings> options)
        {
            Mail_Settings = options.Value;
        }
        public bool SendMail(VmMailData Mail_Data)
        {
            try
            { 

                string fullEmailBody = $"<html>" +
                        $"<style>" +
                            $"h1,h2,h3,h4{{margin:0px;font-family:calibri;}}" +
                            $"td[dir='rtl']{{font - weight: bold;vertical-align: top;font-family:calibri;}}" +
                            $"td[dir='rtl'] a{{font - weight: normal;}}" +
                            $"td{{font - family:calibri;}}" +
                            $".adc-email-body-wrapper td{{font - family:calibri;color:#423f37;}}" +
                            $"</style>" +
                            $"<body style='margin:0px;padding:0px; font-family:calibri;'>" +
                                $"<table width='100%' cellspacing='0' cellpadding='0' border='0' style='width:100%;'>" +
                                $"<tr>" +
                                $"<td" +
                                $" style='width:100%;background:#e0d4ac;color:#FFF;margin:0px; padding:10px 20px; min-height:60px;font-family:calibri;'>" +
                                    $"<table style='width:100%;'>" +
                                            $"<tr>" +
                                                $"<td style='width:28%;text-align: left;vertical-align:middle;'>" +
                                                $"<h1 style='margin:0px;margin-bottom:5px;line-height: 1.1em;'><a href='https://www.alampk.com' style='color:#565349;text-decoration:none;'>Alam App</a></h1>" +
                                                $"<h2 style='margin:0px;line-height: 1.1em;'><a href='https://www.alampk.com' style='color:#565349;text-decoration:none;'>alam pk incorporation</a></h2>" +
                                                $"</td>" +
                                                $"<td style='text-align:center;'><a href='https://www.alampk.com' style='color:#565349;text-decoration:none;'><img alt='ALAM LOGO' src='https://www.codingsips.com/wp-content/themes/kidc/img/logo.png' height='60' style='height:60px;'></a></td>" +
                                                $"<td style='width:28%;text-align: right;vertical-align:middle;'>" +
                                                $"<h1 style='margin:0px;margin-bottom:5px;line-height: 1.1em;'><a href='https://www.alampk.com' style='color:#565349;text-decoration:none;'>تطبيق عالم</a></h1>" +
                                                $"<h2 style='margin:0px;line-height: 1.1em;'><a href='https://www.alampk.com' style='color:#565349;text-decoration:none;'>شركة علم بي كيه التأسيسية</a></h2>" +
                                                $"</td>" +
                                            $"</tr>" +
                                        $"</a>" +
                                    $"</table>" +
                                $"</td>" +
                                $"</tr>" +
                                $"<tr>" +
                                $"<td class='adc-email-body-wrapper' style='background-color:#f5f5f5; background-image:url(https://www.alampk.com/content/img/arabicbg.png);background-size:20px;line-height: 1.5em;color:#423f37;margin:0px; padding:10px;display:block; min-height:350px;font-family:verdana;box-shadow: 0px 2px 4px #555 inset, 0px -2px 4px #555 inset;'>" +
                                    Mail_Data.EmailBody+ 
                                $"</td>" +
                                $"</tr>" +
                                $"<tr>" +
                                $"<td style='background:#EEE;color:#555;margin:0px; padding:10px;display:block; font-size:11px;'>" +
                                    $"This message (including any attachments) may contain confidential, proprietary, privileged and/or private information. The information is intended to be for the use of the individual or entity designated above. If you are not the intended recipient of this message, please notify the sender immediately, and delete the message and any attachments. Any disclosure, reproduction, distribution or other use of this message or any attachments by an individual or entity other than the intended recipient is prohibited."+
                                $"</td>" +
                                $"</tr>" +
                                $"<tr>" +
                                $"<td style='box-shadow: 0px -1px 4px #555;width:100%;background:#e0d4ac;color:#565349;font-size:12px;text-align:center; margin:0px; min-height:30px;padding:20px;'>" +
                                    $"<p>  <a href='https://www.alampk.com' style='color:#565349;'>ALAM</a>  &copy; All rights reserved {DateTime.Now.Year.ToString()}-{(DateTime.Now.Year + 1)}</p>" +
                                $"</td>" +
                                $"</tr>" +
                                $"</table>" +
                            $"</body>" +
                            $"</html>";


                MimeMessage email_Message = new MimeMessage();
                MailboxAddress email_From = new MailboxAddress(Mail_Settings.Name, Mail_Settings.EmailId);
                email_Message.From.Add(email_From); 
                MailboxAddress email_To = new MailboxAddress(Mail_Data.EmailToName, "f.alam@adcs.ae");

                email_Message.To.Add(email_To);
                email_Message.Subject = Mail_Data.EmailSubject; 
                email_Message.Body = new TextPart(TextFormat.Html) { Text = fullEmailBody }; 
                MailKit.Net.Smtp.SmtpClient MailClient = new MailKit.Net.Smtp.SmtpClient();
                MailClient.Connect(Mail_Settings.Host, Mail_Settings.Port, Mail_Settings.UseSSL);
                MailClient.Authenticate(Mail_Settings.EmailId, Mail_Settings.Password);
                MailClient.Send(email_Message);

                MailClient.Disconnect(true);
                MailClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                // Exception Details
                return false;
            }
        }
    }
}
