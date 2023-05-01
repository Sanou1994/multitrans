using Multitrans.Models;
using Multitrans.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using static Multitrans.Models.Tempon;

namespace Multitrans.Controllers
{
    public class HomeController : Controller
    {
        private IAuthentificationRepository _authentificationRepository;
        private IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository,IAuthentificationRepository authentificationRepository)
        {
            _authentificationRepository = authentificationRepository;
            _userRepository = userRepository;

        }
        public ActionResult Index()
        {
            Session["token"] = null;
            Session["utilisateurID"] = null;
			Session["agenceID"] = null;
			Session["structureID"] = null;
            Session["fullName"] = null;
            Session["role"] = null;
            return View();
        }
        public ActionResult Logout()
        {
			Session["token"] = null;
			Session["utilisateurID"] = null;
			Session["agenceID"] = null;
			Session["structureID"] = null;
			Session["fullName"] = null;
			Session["role"] = null;
			return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public ActionResult profil()
        {
           try
           {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {
                    
                    Reponse reponse = _userRepository.ChercherUser(Convert.ToInt64(Session["utilisateurID"]), Convert.ToString(Session["token"]));

                    if (reponse.code == 200)
                    {
                    var userGot = Utils.ToObject<User>(reponse.result);

                        ViewBag.Surname = userGot.prenom;
                        ViewBag.Name = userGot.nom;
                        ViewBag.Phone = userGot.telephone;
                        ViewBag.pseudo = userGot.pseudo;
                        ViewBag.Email = userGot.email;
                        ViewBag.message = reponse.message;
                        ViewBag.id = Convert.ToInt64(Session["utilisateurID"]);

                        return View();


                    }
                    else
                    {
                        ViewBag.sms = reponse.message;
                        return View();
                    }




                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {
                ViewBag.sms = " une erreur majeur est survenue";
                return View();

            }
        }
        [HttpPost]
        public ActionResult profil(User utl)
        {
            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {
                    utl.id = Convert.ToInt64(Session["utilisateurID"]);
                    Reponse reponse = _userRepository.ChercherUser(Convert.ToInt64(Session["utilisateurID"]), Convert.ToString(Session["token"]));
                   

                    if (reponse.code == 200)
                    {
                        User userGot = Utils.ToObject<User>(reponse.result);
                        utl.agenceID = userGot.agenceID;
                        utl.dateCreation = userGot.dateCreation;
                        utl.role = userGot.role;
                        Reponse reponseSave = _userRepository.AjouterUser(utl, Convert.ToString(Session["token"]));
                        if (reponseSave.code == 200)
                        {
                           
                            return Json(new { code = reponseSave.code, message = reponseSave.message });

                        }
                        else
                        {
                            return Json(new { code = reponseSave.code, sms = reponseSave.message });

                        }

                    }
                    else
                    {
                        return Json(new { code = reponse.code, sms = reponse.message });

                    }



                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null)
                {
                    return Json(new { code = 500, message = "*impossible de modifier cet utilisateur*" });

                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }

            }
        }

        [HttpPost]
        public ActionResult Index(string phone, string pwd)
        {

          try
            {

                Reponse reponse = _authentificationRepository.Seconnecter(phone, pwd);
                if (reponse.code == 200)
                {
					var userGot = Utils.ToObject<User>(reponse.result);
					Session["token"] = userGot.monToken;
					Session["utilisateurID"] = userGot.id;
					Session["structureID"] = userGot.structureID;
					Session["fullName"] = userGot.prenom + " " + userGot.nom;
                    Session["role"] = userGot.role;
					Session["agenceID"] = userGot.agenceID;

                    switch (userGot.role)
                    {

                        case "MANAGER":
                            return RedirectToAction("Dashboard", "Admin");
                        case "C":
                            return RedirectToAction("Dashboard", "Caissier");
                        default: return RedirectToAction("Dashboard", "Superviseur");
                    }


				}
				else
                {
                    TempData["sms"] = reponse.message;
					return View();
				}

           }
            catch (Exception)
            {
                TempData["sms"] = "Un problème serveur";
				return RedirectToAction("Index", "Home");

			}  
        }
       
    }
}