using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcShopping.Controllers
{
    public class MemberController : BaseController
    {
        // 会员注册页面
        public ActionResult Register()
        {
            return View();
        }

        // 密码杂凑所需的 Salt 亂数值
        private string pwSalt = "A1rySq1oPe2Mh784QQwG6jRAfkdPpDa90J0i";

        // 写入会员资料
        [HttpPost]
        public ActionResult Register([Bind(Exclude="RegisterOn,AuthCode")] Member member)
        {
            // 检查会员是否已存在
            var chk_member = db.Members.Where(p => p.Email == member.Email).FirstOrDefault();
            if (chk_member != null)
            {
                ModelState.AddModelError("Email", "您输入的 Email 已经有人注册过了!");
            }

            if (ModelState.IsValid)
            {
                // 将密码加盐(Salt)之后进行杂凑运算以提升会员密码的安全性
                member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + member.Password, "SHA1");
                // 会员注册时间
                member.RegisterOn = DateTime.Now;
                // 会员验证码，採用 Guid 当成验证码內容，避免有会员使用到重覆的验证码
                member.AuthCode = Guid.NewGuid().ToString();

                db.Members.Add(member);
                db.SaveChanges();

                //SendAuthCodeToMember(member);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        private void SendAuthCodeToMember(Member member)
        {
            // 准备邮件內容
            string mailBody = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MemberRegisterEMailTemplate.htm"));

            mailBody = mailBody.Replace("{{Name}}", member.Name);
            mailBody = mailBody.Replace("{{RegisterOn}}", member.RegisterOn.ToString("F"));
            var auth_url = new UriBuilder(Request.Url)
            {
                Path = Url.Action("ValidateRegister", new { id = member.AuthCode }),
                Query = ""
            };
            mailBody = mailBody.Replace("{{AUTH_URL}}", auth_url.ToString());

            // 发送邮件給会员
            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("YourGmailAccount", "password");
                SmtpServer.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("YourGmailAccount@gmail.com");
                mail.To.Add(member.Email);
                mail.Subject = "【我的电子商务网站】会员注册确认信";
                mail.Body = mailBody;
                mail.IsBodyHtml = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
                // 发生邮件寄送失败，需记录进资料库備查，以免有会员无法登入
            }
        }

        public ActionResult ValidateRegister(string id)
        {
            if (String.IsNullOrEmpty(id))
                return HttpNotFound();
            
            var member = db.Members.Where(p => p.AuthCode == id).FirstOrDefault();

            if (member != null)
            {
                TempData["LastTempMessage"] = "会员验证成功，您現在可以登入网站了!";
                // 验证成功后要将 member.AuthCode 的內容清空
                member.AuthCode = null;
                db.SaveChanges();
            }
            else
            {
                TempData["LastTempMessage"] = "查无此会员验证码，您可能已经验证过了!";
            }

            return RedirectToAction("Login", "Member");
        }

        // 显示会员登入页面
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        // 執行会员登入
        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl)
        {
            if (ValidateUser(email, password))
            {
                FormsAuthentication.SetAuthCookie(email, false);

                if (String.IsNullOrEmpty(returnUrl)) {
                    return RedirectToAction("Index", "Home");
                } else {
                    return Redirect(returnUrl);
                }
            }

            return View();
        }

        private bool ValidateUser(string email, string password)
        {
            var hash_pw = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + password, "SHA1");

            var member = (from p in db.Members
                          where p.Email == email && p.Password == hash_pw
                          select p).FirstOrDefault();

            // 如果 member 物件不为 null 則代表会员的账号、密码输入正確
            if (member != null) {
                if (member.AuthCode == null) {
                    return true;
                } else {
                    ModelState.AddModelError("", "您尚未通过会员验证，请收信並點擊会员验证連結!");
                    return false;
                }
            } else {
                ModelState.AddModelError("", "您输入的账号或密码錯誤");
                return false;
            }
        }

        // 執行会员登出
        public ActionResult Logout()
        {
            // 清除表单验证的 Cookies
            FormsAuthentication.SignOut();

            // 清除所有曾经写入过的 Session 资料
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CheckDup(string Email)
        {
            var member = db.Members.Where(p => p.Email == Email).FirstOrDefault();

            if (member != null)
                return Json(false);
            else
                return Json(true);
        }

    }
}
