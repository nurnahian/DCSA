using DCSA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DCSA.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        //DCSAEntities1 dbobj = new DCSAEntities1();
       
        public ActionResult login()
        {

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult userChack(user model)
        {
            
            if (ModelState.IsValid)
            {
                using (DCSAEntities1 db = new DCSAEntities1())
                {  
                    var v = db.users.Where(a =>a.user_name.Equals(model.user_name) && a.pass.Equals(model.pass)).FirstOrDefault();
                    if (v != null)
                    {
                        var role = db.users.Where(a => a.user_role.Equals("admin") && a.user_name.Equals(model.user_name)).FirstOrDefault();
                        
                        
                        //string r = role.user_role.ToString();
                        if (role !=null)
                        {
                            Session["log"] = v.user_name.ToString();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Session["log"] = v.user_name.ToString();
                            return RedirectToAction("user");
                        }
                            
                    }
                }
            }

            return View("login");
        }
        public ActionResult Index()
        {
            if (Session["log"] != null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public ActionResult user()
        {
            if (Session["log"] != null)
            {
                return View("~/Views/Home/About.cshtml");
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}