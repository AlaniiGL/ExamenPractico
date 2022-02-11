using ExamenPráctico.Domain;
using ExamenPráctico.Models;
using Tr3scoCore.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenPráctico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClientDashboard clientDashboard = new ClientDashboard();
        private readonly AccountDashboard accountDashboard = new AccountDashboard();
        private readonly TransactionsDashboard transactionsDashboard = new TransactionsDashboard();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Users(ClientViewModel viewModel)
        {
            var users = clientDashboard.GetUsers(viewModel);

            var clients = new ClientViewModel
            {
                ClientModelList = users,
            };

            return View(clients);
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(ClientModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(JsonResponseUtility.GetResponse(false, "Datos no válidos"), JsonRequestBehavior.AllowGet);
                }

                clientDashboard.AddClient(viewModel);
                return Json(JsonResponseUtility.GetResponse(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonResponseUtility.GetResponse(false, e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CreateSavingAccount(ulong? id, string searchQuery)
        {
            var userData = accountDashboard.GetUserData(id, searchQuery);

            var viewModel = new CreateAcountViewModel
            {
                Client = userData,
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateSavingAccount(CreateAcountViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(JsonResponseUtility.GetResponse(false, "Datos no válidos"), JsonRequestBehavior.AllowGet);
                }

                var data = new CreateSavingAccountModel
                {
                    Id_Client = viewModel.Client.Id_Client,
                    Account_Balance = viewModel.AccountModel.Account_Balance,
                };

                accountDashboard.AddAccount(data);

                return Json(JsonResponseUtility.GetResponse(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonResponseUtility.GetResponse(false, e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ViewClientAccount(ulong id)
        {
            var accounts = accountDashboard.ViewUserAccounts(id);

            return View(accounts);
        }

        public ActionResult EditAccount(ulong id, ulong account, ulong transaction)
        {
            if (id == null)
            {
                return RedirectToAction("Users");
            }

            var userAccount = accountDashboard.GetAccount(account);
            var transactionType = transactionsDashboard.GetTransactionType(transaction);

            userAccount.TransactionKind = transactionType.TransactionName;
            userAccount.Transaction = transaction;
            userAccount.Id_Client = id;

            var getClient = clientDashboard.GetUser(id);
            userAccount.ClientName = getClient.ClientName;
            userAccount.ClientLastName1 = getClient.ClientLastName1;
            userAccount.ClientLastName2 = getClient.ClientLastName2;


            return View(userAccount);
        }

        [HttpPost]
        public ActionResult EditAccount(ShowAccountDetailsModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(JsonResponseUtility.GetResponse(false, "Datos no válidos"), JsonRequestBehavior.AllowGet);
                }

                var account = accountDashboard.GetDataAccount(viewModel.Id_Account);

                var updateAccount = new EditAccountModel
                {
                    Id_Account = viewModel.Id_Account,
                };

                if (viewModel.Transaction == 1)
                {
                    updateAccount.Id_TransactionType = viewModel.Transaction;
                    updateAccount.Amount = account.Account_Balance + viewModel.TransactionAmount;
                    updateAccount.Previous_Balance = account.Account_Balance;
                    updateAccount.TransactionAmount = viewModel.TransactionAmount;

                    accountDashboard.MoneyInOut(updateAccount);
                    return Json(JsonResponseUtility.GetResponse(true), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    updateAccount.Id_TransactionType = viewModel.Transaction;
                    updateAccount.Previous_Balance = account.Account_Balance;
                    updateAccount.Amount = account.Account_Balance - viewModel.TransactionAmount;
                    updateAccount.TransactionAmount = viewModel.TransactionAmount;

                    if (updateAccount.Amount < 0)
                    {
                        return Json(JsonResponseUtility.GetResponse(false, "No quedan fondos suficientes"), JsonRequestBehavior.AllowGet);
                    }

                    accountDashboard.MoneyInOut(updateAccount);
                    return Json(JsonResponseUtility.GetResponse(true), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(JsonResponseUtility.GetResponse(false, e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShowTransactions (ulong? id)
        {
            var transactions = transactionsDashboard.GetTransactions(id);

            var user = new ClientModel();

            bool IsCustom = false;

            if (id.HasValue)
            {
                IsCustom = true;
                user = clientDashboard.GetUser(id);
            }

            var record = new TransactionViewModel
            {
                transactionList = transactions,
                IsCustom = IsCustom,
                Client = user
            };

            return View(record);
        }
    }
}