using Multitrans.Models;
using Multitrans.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.Util;
using System.Xml;
using WebGrease.Css.Ast;
using static Multitrans.Models.Tempon;

namespace Multitrans.Controllers
{
    public class AdminController : Controller
    {
        private IAuthentificationRepository _authentificationRepository;
		private IUserRepository _userRepository; 
		private IAgenceRepository _agenceRepository;
        private IOperateurRepository _operateurRepository;
        private IOperationRepository _operationRepository;
        private ISoldeCloturerJourneeRepository _soldeCloturerJourneeRepository;
        private ISoldeDebuterJourneeRepository _soldeDebuterJourneeRepository;
		private ITransactionRepository _transactionRepository;
		private ISoldeReelRepository _soldeReelRepository;
		private IDepenseExtratRepository _depense_extratRepository;

		public AdminController(IAuthentificationRepository authentificationRepository,
						ITransactionRepository transactionRepository,
			IOperateurRepository operateurRepository,
			IDepenseExtratRepository depense_extratRepository,
			ISoldeReelRepository soldeReelRepository,
			IOperationRepository operationRepository,
             ISoldeCloturerJourneeRepository soldeCloturerJourneeRepository,
            ISoldeDebuterJourneeRepository soldeDebuterJourneeRepository,
            IUserRepository userRepository,
			IAgenceRepository agenceRepository
								)
        {
			_transactionRepository = transactionRepository;
			_operationRepository = operationRepository;
			_depense_extratRepository = depense_extratRepository;
			_soldeReelRepository = soldeReelRepository;
			_operateurRepository = operateurRepository;
            _userRepository = userRepository;
			_agenceRepository = agenceRepository;
			_authentificationRepository = authentificationRepository;
            _soldeCloturerJourneeRepository = soldeCloturerJourneeRepository;
            _soldeDebuterJourneeRepository = soldeDebuterJourneeRepository;


        }
        public ActionResult Dashboard()
        {

            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {

                    Reponse reponseOperateur = _operateurRepository.ListeOperateur(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseUser = _userRepository.ListeUser(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseAgence = _agenceRepository.ListeAgence(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));




                    StatistiqueData statistiqueData = new StatistiqueData
                    {
                        agences = Utils.ToObjectList<Agence>(reponseAgence.result),
                        operateurs = Utils.ToObjectList<Operateur>(reponseOperateur.result),
                        users = Utils.ToObjectList<User>(reponseUser.result)                       

                    };



                    return View(statistiqueData);


                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                TempData["sms"] = "problème serveur";
                return RedirectToAction("Index", "Home");

            }
        }

		public ActionResult Statistique(int? pageNo, int? pageSize, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, string sortBy, long? DateDebut, long? DateFin)
		{
			Reponse reponseExtratDepense = _depense_extratRepository.ListeDepenseExtrat(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
			Reponse reponseOperateur = _operateurRepository.ListeOperateur(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
			Reponse reponseOperation = _operationRepository.ListeOperation(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
            Reponse reponseDebuterJournee = _soldeDebuterJourneeRepository.ListeSoldeDebuterJournee(Convert.ToInt64(Session["structureID"]), agenceID, caissierID, operateurID, null, null, DateDebut, DateFin, pageNo, pageSize, sortBy, Convert.ToString(Session["token"]));
            Reponse reponseTransactions = _transactionRepository.ListeTransactionRapports(Convert.ToInt64(Session["structureID"]), agenceID, caissierID, operateurID, operationID, Etat, DateDebut, DateFin,sortBy, Convert.ToString(Session["token"]));
			Reponse reponseUser = _userRepository.ListeUser(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
			Reponse reponseAgence = _agenceRepository.ListeAgence(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));

            Reponse reponseDebuterJourneeRapport = _soldeDebuterJourneeRepository.ListeSoldeDebuterJournee(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
            Reponse reponseCloturerJourneeRapport = _soldeCloturerJourneeRepository.ListeSoldeCloturerJournee(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
            Reponse reponseSoldeReelRapport = _soldeReelRepository.soldeReels(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));


            List<Models.Operateur> operateurs = Utils.ToObjectList<Models.Operateur>(reponseOperateur.result);
            List<Agence> agences = Utils.ToObjectList<Agence>(reponseAgence.result);
            List<Models.Operation> operations = Utils.ToObjectList<Models.Operation>(reponseOperation.result);
            List<Transaction> transactions = Utils.ToObjectList<Transaction>(reponseTransactions.result);
            List<User> users = Utils.ToObjectList<User>(reponseUser.result);
            List<SoldeDebuterJournee> soldeDebuterJournees = Utils.ToObjectList<SoldeDebuterJournee>(reponseDebuterJournee.result);
            List<Depense_extrat> depense_extrats = Utils.ToObjectList<Depense_extrat>(reponseExtratDepense.result);

            List<SoldeDebuterJournee> DebuterJourneeRapports = Utils.ToObjectList<SoldeDebuterJournee>(reponseDebuterJourneeRapport.result);
            List<SoldeCloturerJournee> CloturerJourneeRapports = Utils.ToObjectList<SoldeCloturerJournee>(reponseCloturerJourneeRapport.result);
            List<SoldeReel> SoldeReelRapports = Utils.ToObjectList<SoldeReel>(reponseSoldeReelRapport.result);


            var lst = (from solde in soldeDebuterJournees
                                      select new RapportDataList
                                     {
                                          
                                         id=solde.id,
                                         dec= transactions.Where(g=>g.sens.Equals("DECAISSEMENT")).Sum(p => p.montant),
                                         enc= transactions.Where(g => g.sens.Equals("ENCAISSEMENT")).Sum(p => p.montant),
                                         agenceID= solde.agenceID,
                                         benefice=  transactions.Sum(p => p.commission),
                                         caissierID= solde.idU,
                                         commission= transactions.Sum(p => p.commission),
                                         Date= solde.date,
                                         depense= depense_extrats.Where(g => g.type.Equals("DEPENSE")).Sum(p => p.montant),
                                         extrat= depense_extrats.Where(g => g.type.Equals("EXTRAT")).Sum(p => p.montant),
                                         frais= transactions.Sum(p => p.frais),
                                         surplux= transactions.Sum(p => p.surplux),
                                         taxe= transactions.Sum(p => p.taxe)
                                     }).ToList();



            RapportData StatistiqueRapportDats = new RapportData
            {
                CloturerJourneeRapports= CloturerJourneeRapports,
                DebuterJourneeRapports= DebuterJourneeRapports,
                SoldeReelRapports= SoldeReelRapports,
                operateurs=operateurs,
                operations= operations,
				rapportDataList= lst,
                agences= agences,
                users= users,
                agenceID = agenceID,
				caissierID = caissierID,
				operateurID = operateurID,
				operationID = operationID,
				pageNo = pageNo,
				pageSize = pageSize,
				Etat = Etat,
				DateDebut = DateDebut,
				DateFin = DateFin

			};

			return View(StatistiqueRapportDats);
		}
        // PARTIE DEPENSE EXTRAT

        [HttpPost]
        public ActionResult AjouterDepense_extrat(Depense_extrat utl)
        {
            try
            {
                if (Session["agenceID"] != null && Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {

                    utl.agenceID = Convert.ToInt64(Session["agenceID"]);
                    utl.structureID = Convert.ToInt64(Session["structureID"]);
                    utl.idU = Convert.ToInt64(Session["utilisateurID"]);
                    Reponse reponse = _depense_extratRepository.AjouterDepenseExtrat(utl, Convert.ToString(Session["token"]));
                    return Json(new { code = reponse.code, message = reponse.message });

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

                    return Json(new { code = 500, message = "*impossible d'enregistrer *" });
                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
        }


        public ActionResult ListeExtatDepenses(int? pageNo, int? pageSize, long? agenceID, long? caissierID, string sortBy, string type, long? DateDebut, long? DateFin)
        {

           try
            {
                if ( Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {
                    Reponse depense_extratRepository = _depense_extratRepository.ListeDepenseExtrat(Convert.ToInt64(Session["structureID"]), agenceID, caissierID, DateDebut, DateFin,type, pageNo, pageSize, sortBy, Convert.ToString(Session["token"]));
                    Reponse reponseUser = _userRepository.ListeUser(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseAgence = _agenceRepository.ListeAgence(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));

                StatistiqueRapportData StatistiqueRapportDats = new StatistiqueRapportData
                    {
                       agences = Utils.ToObjectList<Agence>(reponseAgence.result),
                       users = Utils.ToObjectList<User>(reponseUser.result),
                       depenseExtrats = Utils.ToObjectList<Depense_extrat>(depense_extratRepository.result),
						type=type,
					    agenceID = agenceID,
                        caissierID = caissierID,                       
                        pageNo = pageNo,
                        pageSize = pageSize,
                        DateDebut = DateDebut,
                        DateFin = DateFin

                    };

                    return View(StatistiqueRapportDats);


                }
                else
                {
                    TempData["sms"] = "Veuillez vous reconnectez";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {

                TempData["sms"] = "problème serveur";
                return RedirectToAction("Index", "Home");

            } 

        }

       
        // PARTIE SOLDE JOURNEE

        [HttpPost]
		public ActionResult AjouterSoldeDebuterJournee(SoldeDebuterJourneeDto utl)
		{
			try
			{
				if (Session["agenceID"] != null && Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
				{

					utl.agenceID = Convert.ToInt64(Session["agenceID"]);
					utl.structureID = Convert.ToInt64(Session["structureID"]);
					utl.idU = Convert.ToInt64(Session["utilisateurID"]);
					Reponse reponse = _soldeDebuterJourneeRepository.AjouterSoldeDebuterJournee(utl, Convert.ToString(Session["token"]));
					return Json(new { code = reponse.code, message = reponse.message });

				}
				else
				{
					TempData["sms"] = "la session est écoulée";
					return RedirectToAction("Index", "Home");
				}
			}
			catch (Exception)
			{
				
					TempData["sms"] = "la session est écoulée";
					return RedirectToAction("Index", "Home");
				
			}
		}
        public ActionResult SoldeDebuterJournees(int? pageNo, int? pageSize, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, string sortBy, long? DateDebut, long? DateFin)
        {
            try
            {
                if ( Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {
                    Reponse reponseOperateur = _operateurRepository.ListeOperateur(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseOperation = _operationRepository.ListeOperation(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseDebuterJournee = _soldeDebuterJourneeRepository.ListeSoldeDebuterJournee(Convert.ToInt64(Session["structureID"]), agenceID, caissierID, operateurID, operationID, Etat, DateDebut, DateFin, pageNo, pageSize, sortBy, Convert.ToString(Session["token"]));
                    Reponse reponseUser = _userRepository.ListeUser(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseAgence = _agenceRepository.ListeAgence(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));

                    StatistiqueRapportData StatistiqueRapportDats = new StatistiqueRapportData
                    {
                        agences = Utils.ToObjectList<Agence>(reponseAgence.result),
                        operations = Utils.ToObjectList<Models.Operation>(reponseOperation.result),
                        users = Utils.ToObjectList<User>(reponseUser.result),
                        soldeDebuterJournees = Utils.ToObjectList<SoldeDebuterJournee>(reponseDebuterJournee.result),
                        operateurs = Utils.ToObjectList<Operateur>(reponseOperateur.result),
                        agenceID = agenceID,
                        caissierID = caissierID,
                        operateurID = operateurID,
                        operationID = operationID,
                        pageNo = pageNo,
                        pageSize = pageSize,
                        Etat = Etat,
                        DateDebut = DateDebut,
                        DateFin = DateFin

                    };

                    return View(StatistiqueRapportDats);


                }
                else
                {
                    TempData["sms"] = "Veuillez vous reconnectez";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {

                TempData["sms"] = "problème serveur";
                return RedirectToAction("Index", "Home");

            }
           
        }
        [HttpPost]
        public ActionResult AjouterSoldeCloturerJournee(SoldeCloturerJourneeDto utl)
        {
            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {

                    utl.idU = Convert.ToInt64(Session["utilisateurID"]);
                    utl.agenceID = Convert.ToInt64(Session["agenceID"]);
                    utl.structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _soldeCloturerJourneeRepository.AjouterSoldeCloturerJournee(utl, Convert.ToString(Session["token"]));
                    return Json(new { code = reponse.code, message = reponse.message });

                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
               
                    TempData["sms"] = "problème serveur";
                    return RedirectToAction("Index", "Home");
                
            }
        }

       
        [HttpGet]
        public ActionResult SoldeCloturerJournees(int? pageNo, int? pageSize, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, string sortBy, long? DateDebut, long? DateFin)
        {
            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {
                    Reponse reponseOperateur = _operateurRepository.ListeOperateur(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseOperation = _operationRepository.ListeOperation(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseDebuterJournee = _soldeCloturerJourneeRepository.ListeSoldeCloturerJournee(Convert.ToInt64(Session["structureID"]), agenceID, caissierID, operateurID, operationID, Etat, DateDebut, DateFin, pageNo, pageSize, sortBy, Convert.ToString(Session["token"]));
                    Reponse reponseUser = _userRepository.ListeUser(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponseAgence = _agenceRepository.ListeAgence(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));

                    StatistiqueRapportData StatistiqueRapportDats = new StatistiqueRapportData
                    {
                        agences = Utils.ToObjectList<Agence>(reponseAgence.result),
                        operations = Utils.ToObjectList<Models.Operation>(reponseOperation.result),
                        users = Utils.ToObjectList<User>(reponseUser.result),
						soldeCloturerJournees= Utils.ToObjectList<SoldeCloturerJournee>(reponseDebuterJournee.result),
                        operateurs = Utils.ToObjectList<Operateur>(reponseOperateur.result),
                        agenceID = agenceID,
                        caissierID = caissierID,
						operateurID = operateurID,
                        operationID = operationID,
                        pageNo = pageNo,
                        pageSize = pageSize,
                        Etat = Etat,
                        DateDebut = DateDebut,
                        DateFin = DateFin

                    };

                    return View(StatistiqueRapportDats);


                }
                else
                {
                    TempData["sms"] = "Veuillez vous reconnectez";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {

                TempData["sms"] = "problème serveur";
                return RedirectToAction("Index", "Home");

            }

        }

        // PARTIE AGENCE

        [HttpPost]
		public ActionResult AjouterAgence(AgenceDtoRequest utl)
		{
			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
				{


					utl.structureID = Convert.ToInt64(Session["structureID"]);
					utl.date_creation = DateTime.Now.Ticks;
					utl.status = true;
					Reponse reponse = _agenceRepository.AjouterAgence(utl, Convert.ToString(Session["token"]));
					return Json(new { code = reponse.code, message = reponse.message });

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

					return Json(new { code = 500, message = "*impossible d'enregistrer cet utilisateur*" });
				}
				else
				{
					TempData["sms"] = "la session est écoulée";
					return RedirectToAction("Index", "Home");
				}
			}
		}

		[HttpPost]
		public ActionResult desactiverAgence(long? No)
		{


			try
			{

				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{

					if (No != null)
					{
						Reponse reponse = _agenceRepository.bloquerAgence(Convert.ToInt64(No), Convert.ToString(Session["token"]));

						return Json(new { code = reponse.code, message = reponse.message });


					}
					else
					{
						return Json(new { code = 201,message = " ce compte n'existe pas " });

					}
				}
				else
				{
					return Json(new { code = 201, message = " La session est écoulée " });



				}

			}
			catch (Exception)
			{

				return Json(new { code = 500, message = " une erreur majeur est survenue" });

			}

		}

		[HttpGet]
		public ActionResult ListesAgences()
		{

			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{


					Reponse reponseAgence = _agenceRepository.ListeAgence(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					if (reponseAgence.code == 200)
					{
						var agences = Utils.ToObjectList<AgenceDtoRequest>(reponseAgence.result);

						Reponse reponseOperateur = _operateurRepository.ListeOperateur(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));

						if (reponseOperateur.code == 200)
						{
							var operateurs = Utils.ToObjectList<Operateur>(reponseOperateur.result);
							var result = new Tuple<List<AgenceDtoRequest>, List<Operateur>>(agences, operateurs);
							return View(result);

						}
						else
						{
							var tpl = new List<Operateur>();
							var result = new Tuple<List<AgenceDtoRequest>, List<Operateur>>(agences, tpl);
							return View(result);
						}



					}
					else
					{
						
						var result = new Tuple<List<AgenceDtoRequest>, List<Operateur>>(new List<AgenceDtoRequest>(),new List<Operateur>());
						return View(result);
						
					}


				}
				else
				{
					TempData["sms"] = "la session est écoulée";
					return RedirectToAction("Index", "Home");
				}
			}
			catch
			{
				return RedirectToAction("Index", "Admin");

			}



		}

		// TRANSACTIONS
		[HttpGet]
		public ActionResult ListeTransactions(int? pageNo, int? pageSize, long? agenceID, long? caissierID, long? operateurID, long? operationID,string Etat, string sortBy, long? DateDebut, long? DateFin)
		{

			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{
					
					Reponse reponseOperateur = _operateurRepository.ListeOperateur(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					Reponse reponseOperation = _operationRepository.ListeOperation(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					Reponse reponseUser = _userRepository.ListeUser(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					Reponse reponseAgence = _agenceRepository.ListeAgence(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					
				    Reponse reponse = _transactionRepository.ListeTransactions(Convert.ToInt64(Session["structureID"]),agenceID,caissierID,operateurID,operationID,Etat, DateDebut, DateFin, pageNo,pageSize,sortBy, Convert.ToString(Session["token"]));

			

				StatistiqueData statistiqueData = new StatistiqueData
				{
					agences = Utils.ToObjectList<Agence>(reponseAgence.result),
					operateurs = Utils.ToObjectList<Operateur>(reponseOperateur.result),
					operations = Utils.ToObjectList<Models.Operation>(reponseOperation.result),
					transactions = Utils.ToObjectList<Transaction>(reponse.result),
					users = Utils.ToObjectList<User>(reponseUser.result),
					agenceID= agenceID,
					caissierID= caissierID,
					operateurID= operateurID,
					operationID= operationID,
					pageNo= pageNo,
					pageSize= pageSize,
					Etat=Etat,
					DateDebut= DateDebut,
					DateFin= DateFin
					
				};



					return View(statistiqueData);


				}
				else
				{
					TempData["sms"] = "la session est écoulée";
					return RedirectToAction("Index", "Home");
				}
			}
			catch
			{
				TempData["sms"] = "problème serveur";
				return RedirectToAction("Index", "Home");

			} 



		}
		// PARTIE UTILISATEUR
		[HttpGet]
		public ActionResult AjouterCompte()
		{
			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
				{

					Reponse reponse = _userRepository.ListeUser(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					if (reponse.code == 200)
					{
						return View(reponse.result);

					}
					else
					{
						var tpl = new List<User>();
						ViewBag.sms = reponse.message;
						return View(tpl);
					}
				}
				else
				{
					return RedirectToAction("index", "Home");
				}
			}
			catch (Exception)
			{
				return RedirectToAction("Dashboard", "Admin");
			}
		}

		[HttpPost]
		public ActionResult AjouterCompte(User utl)
		{
			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
				{
					utl.structureID = Convert.ToInt64(Session["structureID"]);
					utl.status = true;
					Reponse reponse = _userRepository.AjouterUser(utl,Convert.ToString(Session["token"]));
					return Json(new { code = reponse.code, message = reponse.message });

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
					
					return Json(new { code = 500, message = "*impossible d'enregistrer cet utilisateur*" });
				}
				else
				{
					TempData["sms"] = "la session est écoulée";
					return RedirectToAction("Index", "Home");
				}
			}
		}
		
		[HttpGet]
		public ActionResult modifierCompte(long? No)
		{
			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{
					Reponse reponseSave = _userRepository.ChercherUser(No, Convert.ToString(Session["token"]));
					return Json(new { code = reponseSave.code, message = reponseSave.message });


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
		public void desactiverCompte(long? No)
		{


			try
			{

				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{

					if (No != null)
					{
						Reponse reponse = _userRepository.bloquerUser(Convert.ToInt64(No),Convert.ToString(Session["token"]));


						TempData["lockUser"] = reponse.message;


					}
					else
					{
						TempData["lockUser"] = " ce compte n'existe pas ";
					}
				}
				else
				{
					TempData["lockUser"] = " La session est écoulée";

				}

			}
			catch (Exception)
			{
				TempData["lockUser"] = " une erreur majeur est survenue";


			}

		}

		[HttpGet]
		public ActionResult ListesComptes()
		{

			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{
					Reponse reponseUser = _userRepository.ListeUser(Convert.ToInt64(Session["structureID"]),Convert.ToString(Session["token"]));
					Reponse reponseAgence = _agenceRepository.ListeAgence(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					if (reponseUser.code == 200)
					{
						var users = Utils.ToObjectList<User>(reponseUser.result);

						if (reponseAgence.code == 200)
						{

							var agences = Utils.ToObjectList<Agence>(reponseAgence.result);
							var tpl = new Tuple<List<User>, List<Agence>>(users, agences);
							return View(tpl);

						}
						else
						{
							var tpl = new Tuple<List<User>, List<Agence>>(users, null);
							return View(tpl);
						}					

					}
					else
					{
						var tpl = new Tuple<List<User>, List<Agence>>(null, null);
						return View(tpl);
					}			


				}
				else
				{
					TempData["sms"] = "la session est écoulée";
					return RedirectToAction("Index", "Home");
				}
			}
			catch
			{
				return RedirectToAction("Index", "Admin");

			}



		}
        // PARTIE OPERATION
        [HttpPost]
        public ActionResult AjouterOperation(Models.Operation utl)
        {
            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {


                    utl.structureID = Convert.ToInt64(Session["structureID"]);
                    utl.status = true;
                    Reponse reponse = _operationRepository.AjouterOperation(utl, Convert.ToString(Session["token"]));
                    return Json(new { code = reponse.code, message = reponse.message });

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

                    return Json(new { code = 500, message = "*impossible d'enregistrer cet utilisateur*" });
                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public ActionResult desactiverOperation(long? No)
        {


            try
            {

                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {

                    if (No != null)
                    {
                        Reponse reponse = _operationRepository.bloquerOperation(Convert.ToInt64(No), Convert.ToString(Session["token"]));

                        return Json(new { code = reponse.code, message = reponse.message });


                    }
                    else
                    {
                        return Json(new { code = 201, message = " cette opération n'existe pas " });

                    }
                }
                else
                {
                    return Json(new { code = 201, message = " La session est écoulée " });



                }

            }
            catch (Exception)
            {

                return Json(new { code = 500, message = " une erreur majeur est survenue" });

            }

        }

        [HttpGet]
        public ActionResult ListesOperations()
        {

            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {
                    Reponse reponse = _operationRepository.ListeOperation(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        var agences = Utils.ToObjectList<Models.Operation>(reponse.result);
                        ViewBag.message = reponse.message;
                        return View(agences);

                    }
                    else
                    {
                        var tpl = new List<Models.Operation>();
                        ViewBag.message = reponse.message;
                        return View(tpl);
                    }


                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Admin");

            }



        }
        // PARTIE OPERATEUR
        [HttpPost]
        public ActionResult AjouterOperateur(Operateur utl)
        {
            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {


                    utl.structureID = Convert.ToInt64(Session["structureID"]);
                    utl.status = true;
                    Reponse reponse = _operateurRepository.AjouterOperateur(utl, Convert.ToString(Session["token"]));
                    return Json(new { code = reponse.code, message = reponse.message });

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

                    return Json(new { code = 500, message = "*impossible d'enregistrer cet utilisateur*" });
                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public ActionResult desactiverOperateur(long? No)
        {


            try
            {

                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {

                    if (No != null)
                    {
                        Reponse reponse = _operateurRepository.bloquerOperateur(Convert.ToInt64(No), Convert.ToString(Session["token"]));

                        return Json(new { code = reponse.code, message = reponse.message });


                    }
                    else
                    {
                        return Json(new { code = 201, message = " ce compte n'existe pas " });

                    }
                }
                else
                {
                    return Json(new { code = 201, message = " La session est écoulée " });



                }

            }
            catch (Exception)
            {

                return Json(new { code = 500, message = " une erreur majeur est survenue" });

            }

        }

        [HttpGet]
        public ActionResult ListesOperateurs()
        {

            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {
                    Reponse reponse = _operateurRepository.ListeOperateur(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        var agences = Utils.ToObjectList<Operateur>(reponse.result);
                        ViewBag.message = reponse.message;
                        return View(agences);

                    }
                    else
                    {
                        var tpl = new List<Operateur>();
                        ViewBag.message = reponse.message;
                        return View(tpl);
                    }


                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Admin");

            }



        }

    }
}