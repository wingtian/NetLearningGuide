using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class SendMailTest
    {

        /// <summary>
        /// 发邮件方法
        /// </summary>
        /// <param name="sender">发件人账号</param>
        /// <param name="senderpwd">发件人密码</param>
        /// <param name="recipients">收件人</param>
        /// <param name="cc">抄送(可以弄为list)</param>
        /// <param name="theme">主题</param>
        /// <param name="filed">附件</param>
        /// <param name="fileName">附件名</param>
        /// <param name="msg">邮件信息</param>
        /// <exception cref="Exception"></exception>
        private void Sendmail(string sender, string senderpwd, string recipients, string cc, string theme, Stream filed, string fileName, string msg)
        {
            var smtpClient = new SmtpClient("smtphz.qiye.163.com", 25);
            var mailMessage = new MailMessage();
            var mailname = sender;//发件人邮箱用户名
            smtpClient.Credentials = new System.Net.NetworkCredential(mailname, senderpwd);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;//是否为html格式
            mailMessage.From = new MailAddress(mailname);
            mailMessage.To.Add(recipients);
            if (!string.IsNullOrWhiteSpace(cc))
            {
                mailMessage.CC.Add(cc);//抄送
            }
            mailMessage.Subject = theme;//邮件主题
            mailMessage.Attachments.Clear();
            if (filed != null)
                mailMessage.Attachments.Add(new System.Net.Mail.Attachment(filed, fileName, MediaTypeNames.Application.Pdf));

            mailMessage.Body = msg.Replace("\r\n", "<br>");//邮件内容
            try
            {
                smtpClient.Send(mailMessage);

            }
            catch (SmtpException ex)
            {
                throw new Exception("邮箱异常！" + ex.Message);
            }
        }
    }
}
