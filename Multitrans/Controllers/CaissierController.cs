using Multitrans.Models;
using Multitrans.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using static Multitrans.Models.Tempon;

namespace Multitrans.Controllers
{
    public class CaissierController : Controller
    {
        private IAuthentificationRepository _authentificationRepository;
        private IOperateurRepository _operateurRepository;
        private IOperationRepository _operationRepository;
        private ISoldeCloturerJourneeRepository _soldeCloturerJourneeRepository;
        private ISoldeDebuterJourneeRepository _soldeDebuterJourneeRepository;
		private ITransactionRepository _transactionRepository;
		private ISoldeReelRepository _soldeReelRepository;
		private IDepenseExtratRepository _depense_extratRepository;

		public CaissierController(IAuthentificationRepository authentificationRepository,
			ITransactionRepository transactionRepository,
			IDepenseExtratRepository depense_extratRepository,
			ISoldeReelRepository soldeReelRepository,
			 ISoldeCloturerJourneeRepository soldeCloturerJourneeRepository,
            ISoldeDebuterJourneeRepository soldeDebuterJourneeRepository,
             IOperateurRepository operateurRepository,
            IOperationRepository operationRepository)
        {
			_depense_extratRepository = depense_extratRepository;
			_soldeReelRepository = soldeReelRepository;

			_transactionRepository = transactionRepository;
			_authentificationRepository = authentificationRepository;
            _operationRepository = operationRepository;
            _operateurRepository = operateurRepository;
            _soldeCloturerJourneeRepository = soldeCloturerJourneeRepository;
            _soldeDebuterJourneeRepository = soldeDebuterJourneeRepository;
        }
        public ActionResult Dashboard()
        {
            try
           {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {
					Reponse reponseExtratDepense = _depense_extratRepository.ListeDepenseExtratParCaisseEncours(Convert.ToInt64(Session["utilisateurID"]), Convert.ToInt64(Session["structureID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
					Reponse reponseOperateur = _operateurRepository.ListeOperateurCaisse(Convert.ToInt64(Session["structureID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
                    Reponse reponseOperation = _operationRepository.ListeOperation(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
                    Reponse reponse = _soldeDebuterJourneeRepository.ListeSoldeDebuterJournee(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					Reponse reponseTransaction = _transactionRepository.ListeTransactionCaissierEncours(Convert.ToInt64(Session["utilisateurID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
                    Reponse reponseSoldeReel = _soldeReelRepository.soldeReelActuel(Convert.ToInt64(Session["utilisateurID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
					List<Transaction> transactions = null;
					List<Operation> operations = null;
                    List<Operateur> operateurs = null;
					List<Depense_extrat> depense_extrats = null;

					SoldeReel soldeReel = null;

					bool resulat = false;

                    if (reponseOperateur.code == 200)
                    {
                        operateurs = Utils.ToObjectList<Operateur>(reponseOperateur.result);

                        if (reponseOperation.code == 200)
                        {

							if (reponseTransaction.code == 200)
							{

								if (reponseSoldeReel.code == 200)
								{



									if (reponseExtratDepense.code == 200)
									{

										depense_extrats= Utils.ToObjectList<Depense_extrat>(reponseExtratDepense.result);
										soldeReel = Utils.ToObject<SoldeReel>(reponseSoldeReel.result);
										transactions = Utils.ToObjectList<Transaction>(reponseTransaction.result);
										operations = Utils.ToObjectList<Operation>(reponseOperation.result);


									}
									else
									{
										depense_extrats = new List<Depense_extrat>();
										soldeReel = Utils.ToObject<SoldeReel>(reponseSoldeReel.result);
										transactions = Utils.ToObjectList<Transaction>(reponseTransaction.result);
										operations = Utils.ToObjectList<Operation>(reponseOperation.result);
									}


								}
								else
								{
									depense_extrats = new List<Depense_extrat>();
									soldeReel = new SoldeReel { SoldeReelItems = new List<SoldeReelItem>() };
									transactions = Utils.ToObjectList<Transaction>(reponseTransaction.result);
									operations = Utils.ToObjectList<Operation>(reponseOperation.result);
								}								

							}
							else
							{
								depense_extrats = new List<Depense_extrat>();
								soldeReel = new SoldeReel { SoldeReelItems = new List<SoldeReelItem>() };
								transactions = new List<Transaction>();
								operations = Utils.ToObjectList<Operation>(reponseOperation.result);
							}

                        }
                        else
                        {
							depense_extrats = new List<Depense_extrat>();
							soldeReel = new SoldeReel { SoldeReelItems = new List<SoldeReelItem>() };
							transactions = new List<Transaction>();
							operations = Utils.ToObjectList<Operation>(reponseOperation.result);
                        }

                    }
                    else
                    {
						depense_extrats = new List<Depense_extrat>();
						soldeReel = new SoldeReel { SoldeReelItems = new List<SoldeReelItem>()};
						transactions = new List<Transaction>();
						operations = new List< Operation >();
                        operateurs = new List<Operateur>();
                    }


                    if (reponse.code == 200)
                    {

                        List<SoldeDebuterJournee> soldeDebuterJournee = Utils.ToObjectList<SoldeDebuterJournee>(reponse.result);
                         resulat=soldeDebuterJournee.OrderByDescending(o=>o.id).Select(l=>l.status).FirstOrDefault();



                    }

                    var dashboard = new DashboadData
                    {
                        depenseExtrat= depense_extrats,
						soldeReelActuel = soldeReel,
						transactions =transactions,
                        operateurs= operateurs,
                        operations= operations,
                        soldeDebuterJourneeTermine=resulat
					};
                    return View(dashboard);

                }
                else
                {
                    TempData["sms"] = "la session est écoulée";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
				TempData["sms"] = "Un problème serveur";
				return RedirectToAction("Index", "Home");

            } 
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
                    utl.idU= Convert.ToInt64(Session["utilisateurID"]);
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

		
		public ActionResult ListeDepenses()
		{

			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{
					Reponse reponse = _depense_extratRepository.ListeDepenseExtratParCaisse(Convert.ToInt64(Session["structureID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToInt64(Session["utilisateurID"]), Convert.ToString(Session["token"]));
					if (reponse.code == 200)
					{
						List<Depense_extrat> dep_extrat = Utils.ToObjectList<Depense_extrat>(reponse.result);
						List<Depense_extrat> depenses = dep_extrat.Where(p=>p.type == "DEPENSE").OrderByDescending(p=>p.id).ToList();
						ViewBag.message = reponse.message;
						return View(depenses);



					}
					else
					{
						var tuple = new List<Depense_extrat>();
						ViewBag.message = reponse.message;
						return View(tuple);
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
				return RedirectToAction("Index", "Caissier");

			}

		}

		public ActionResult ListeExtrats()
		{

			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{
					Reponse reponse = _depense_extratRepository.ListeDepenseExtratParCaisse(Convert.ToInt64(Session["structureID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToInt64(Session["utilisateurID"]), Convert.ToString(Session["token"]));
					if (reponse.code == 200)
					{
						List<Depense_extrat> dep_extrat = Utils.ToObjectList<Depense_extrat>(reponse.result);
						List<Depense_extrat> depenses = dep_extrat.Where(p => p.type == "EXTRAT").OrderByDescending(p => p.id).ToList();
						ViewBag.message = reponse.message;
						return View(depenses);



					}
					else
					{
						var tuple = new List<Depense_extrat>();
						ViewBag.message = reponse.message;
						return View(tuple);
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
				return RedirectToAction("Index", "Caissier");

			}

		}
		// PARTIE SOLDEDEBUTERJOURNEE

		[HttpPost]
        public ActionResult AjouterSoldeDebuterJournee(SoldeDebuterJourneeDto utl)
        {
            try
            {
                if (Session["agenceID"] != null && Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {

                    utl.agenceID= Convert.ToInt64(Session["agenceID"]);
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

        [HttpPost]
        public ActionResult desactiverSoldeDebuterJournee(long? No)
        {


            try
            {

                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {

                    if (No != null)
                    {
                        Reponse reponse = _soldeDebuterJourneeRepository.bloquerSoldeDebuterJournee(Convert.ToInt64(No), Convert.ToString(Session["token"]));

                        return Json(new { code = reponse.code, message = reponse.message });


                    }
                    else
                    {
                        return Json(new { code = 201, message = " il n'existe pas " });

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
        public ActionResult SoldeDebuterJournees()
        {

            try
            {
                if (Session["utilisateurID"] != null && Session["agenceID"] != null && Session["FullName"] != null && Session["token"] != null)
                {
                    Reponse reponse = _soldeDebuterJourneeRepository.ListeSoldeDebuterJournee(Convert.ToInt64(Session["utilisateurID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        List<SoldeDebuterJournee> agences = Utils.ToObjectList<SoldeDebuterJournee>(reponse.result);
                        Reponse reponseOperateur = _operateurRepository.ListeOperateurCaisse(Convert.ToInt64(Session["structureID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
                         
                        if (reponseOperateur.code == 200)
                        {
                            List<Operateur> op = Utils.ToObjectList<Operateur>(reponseOperateur.result);

                            var tuple = new Tuple<List<SoldeDebuterJournee>, List<Operateur>>(agences,op);
                            ViewBag.message = reponse.message;
                            return View(tuple);

                        }
                        else
                        {
                            var tuple = new Tuple<List<SoldeDebuterJournee>, List<Operateur>>(agences,new List<Operateur>());

                            ViewBag.message = reponse.message;
                            return View(tuple);
                        }

                        

                    }
                    else
                    {
                        var tuple = new Tuple<List<SoldeDebuterJournee>, List<Operateur>>(new List<SoldeDebuterJournee>(), new List<Operateur>());
                        ViewBag.message = reponse.message;
                        return View(tuple);
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
                return RedirectToAction("Index", "Caissier");

            }



        }

      
        
        
        
        
        // PARTIE SOLDECLOTURERJOURNEE

        [HttpPost]
        public ActionResult AjouterSoldeCloturerJournee(SoldeCloturerJourneeDto utl)
        {
            try
            {
                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
                {

                    utl.idU= Convert.ToInt64(Session["utilisateurID"]);
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

        [HttpPost]
        public ActionResult desactiverSoldeCloturerJournee(long? No)
        {


            try
            {

                if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
                {

                    if (No != null)
                    {
                        Reponse reponse = _soldeCloturerJourneeRepository.bloquerSoldeCloturerJournee(Convert.ToInt64(No), Convert.ToString(Session["token"]));

                        return Json(new { code = reponse.code, message = reponse.message });


                    }
                    else
                    {
                        return Json(new { code = 201, message = " il n'existe pas " });

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
		public ActionResult SoldeCloturerJournees()
		{

			try
			{
				if (Session["utilisateurID"] != null && Session["agenceID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{
					Reponse reponse = _soldeCloturerJourneeRepository.ListeSoldeCloturerJournee(Convert.ToInt64(Session["utilisateurID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
					if (reponse.code == 200)
					{
						List<SoldeCloturerJournee> agences = Utils.ToObjectList<SoldeCloturerJournee>(reponse.result);

                        Reponse reponseOperateur = _operateurRepository.ListeOperateurCaisse(Convert.ToInt64(Session["structureID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));


                        if (reponseOperateur.code == 200)
						{
							List<Operateur> op = Utils.ToObjectList<Operateur>(reponseOperateur.result);

							var tuple = new Tuple<List<SoldeCloturerJournee>, List<Operateur>>(agences, op);
							ViewBag.message = reponse.message;
							return View(tuple);

						}
						else
						{
							var tuple = new Tuple<List<SoldeCloturerJournee>, List<Operateur>>(agences, new List<Operateur>());

							ViewBag.message = reponse.message;
							return View(tuple);
						}



					}
					else
					{
						var tuple = new Tuple<List<SoldeDebuterJournee>, List<Operateur>>(new List<SoldeDebuterJournee>(), new List<Operateur>());
						ViewBag.message = reponse.message;
						return View(tuple);
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
				return RedirectToAction("Index", "Caissier");

			}



		}
		// FAIRE DEPOT
		[HttpPost]
		public ActionResult FaireTransaction(Depot_Retrait depot_retrait)
		{
			try
			{
				if (Session["agenceID"] !=null && Session["utilisateurID"] != null && Session["structureID"] != null && Session["token"] != null)
				{
                    Transaction transaction = new Transaction
                    {
                        agenceID = Convert.ToInt64(Session["agenceID"]),
                        commission = depot_retrait.Commission,
                        etat = depot_retrait.Etat,
                        frais = depot_retrait.Frais,
                        idU = Convert.ToInt64(Session["utilisateurID"]),
                        montant = depot_retrait.Montant,
                        numero = depot_retrait.Telephone,
                        operateurID = depot_retrait.Operateur,
                        description=depot_retrait.description,
                        operationID = depot_retrait.Operation,
                        sens = depot_retrait.Sens,
                        structureID = Convert.ToInt64(Session["structureID"]),
                        surplux = depot_retrait.Surplux,
                        taxe = depot_retrait.Taxe,
                        id = (depot_retrait.id != null) ? depot_retrait.id : null
					};


					Reponse reponse = _transactionRepository.AjouterTransaction(transaction, Convert.ToString(Session["token"]));
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
		[HttpGet]
		public ActionResult ListeTransactions()
		{

			try
			{
				if (Session["utilisateurID"] != null && Session["structureID"] != null && Session["FullName"] != null && Session["token"] != null)
				{
					Reponse reponseOperateur = _operateurRepository.ListeOperateurCaisse(Convert.ToInt64(Session["structureID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
					Reponse reponseOperation = _operationRepository.ListeOperation(Convert.ToInt64(Session["structureID"]), Convert.ToString(Session["token"]));
					Reponse reponse = _transactionRepository.ListeTransactionCaissier(Convert.ToInt64(Session["utilisateurID"]), Convert.ToInt64(Session["agenceID"]), Convert.ToString(Session["token"]));
					List<Operation> operations = null;
					List<Operateur> operateurs = null;
					List<Transaction> transactions = null;
					if (reponse.code == 200)
					{
						transactions = Utils.ToObjectList<Transaction>(reponse.result);

						if (reponseOperateur.code == 200)
						{
							operateurs = Utils.ToObjectList<Operateur>(reponseOperateur.result);


							if (reponseOperation.code == 200)
							{
								operations = Utils.ToObjectList<Operation>(reponseOperation.result);

							}
							else
							{
								operations = new List<Operation>();

							}

						}
						else
						{
							operations = new List<Operation>();
							operateurs = new List<Operateur>();

						}

					}
					else
					{
						operations = new List<Operation>();
						operateurs = new List<Operateur>();
						transactions = new List<Transaction>();
					}
                    var tuple = new Tuple<List<Transaction>, List<Operation>, List<Operateur>>(transactions, operations, operateurs);
					return View(tuple);


				}
				else
				{
					TempData["sms"] = "la session est écoulée";
					return RedirectToAction("Index", "Home");
				}
			}
			catch
			{
				return RedirectToAction("Index", "Caissier");

			}



		}


	}
}