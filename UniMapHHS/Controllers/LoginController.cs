using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UniMapHHS.Mocking;
using UniMapHHS.Models;

namespace UniMapHHS.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMock DBHandler;

        public LoginController(DbAppContext context, IMock mock)
        {
            DBHandler = mock;
            mock.SetContext(context);
        }

        public IActionResult Index(string lang)
        {
            ViewBag.glossary = GetGlossary(lang);
            ViewBag.lang = lang != null ? lang : "EN";
            return View();
        }

        private string getLangPref(string lang)
        {
            string username = DBHandler.GetTempUsername(this);
            if (username != "Guest")
            {
                User user = DBHandler.GetUsers().FirstOrDefault(x => x.Username.Equals(username.ToString(), StringComparison.OrdinalIgnoreCase));
                if (user != null && (user.langPref != null || user.langPref != ""))
                {
                    return user.langPref;
                }
            }
            return lang;
        }

        public Dictionary<int, string> GetGlossary(string lang)
        {
            var glossaryTable = DBHandler.GetGlossary();

            if (lang == "NL")
            {
                return glossaryTable.ToDictionary(id => id.GlossaryId, lang => lang.Dutch);
            }
            else if (lang == "SP")
            {
                return glossaryTable.ToDictionary(id => id.GlossaryId, lang => lang.Spanish);
            }
            else
            {
                return glossaryTable.ToDictionary(id => id.GlossaryId, lang => lang.English);
            }
        }

        public IActionResult Logout(string lang)
        {
            DBHandler.SetTempUsername(this, "Guest");
            DBHandler.SetTempRole(this, "Guest");
            return RedirectToAction("Index", "Home", new { lang = lang });
        }

        [HttpPost]
        public string CheckUserexistence(string username, string password)
        {
            User user = DBHandler.GetUsers().FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user != null && user.Password.Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                SaveUserInformation(username);
                string lang = getLangPref(ViewBag.lang);
                return lang;
            }
            else
            {
                return "ERROR";
            }
        }

        private void SaveUserInformation(string username)
        {
            string role = CheckUserRole(username);

            DBHandler.SetTempUsername(this ,username);
            DBHandler.SetTempRole(this, role);
        }

        private string CheckUserRole(string username)
        {
            if (username.Contains("@student.hhs.nl"))
            {
                return "Student";
            }
            else if (username.Contains("@hhs.nl"))
            {
                return "Teacher";
            }

            return "Guest";
        }
    }
}
