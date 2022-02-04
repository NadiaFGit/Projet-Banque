using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Examen.Models;
using Microsoft.Extensions.Configuration;

namespace Examen.Controllers
{
    public class HomeController : Controller
    {
        private SystemDAO bankDAO;
        public HomeController(IConfiguration _configuration)
        {
            bankDAO = new SystemDAO(_configuration);
        }

        public IActionResult Index()
        {
            return View(bankDAO.GetBanks());
        }

        public IActionResult CustomersForBank(int id) // id de la banque
        {
            ViewBag.bankName = bankDAO.GetBanks().Find(b => b.ID == id).Name;
            return View(bankDAO.GetCustomersForBank(id));
        }

        public IActionResult AccountsForCustomer(int id) // id du client
        {
            ViewBag.customerName = bankDAO.GetCustomers().Find(c => c.ID == id).LastName;
            return View(bankDAO.GetAccountsForCustomer(id));
        }

        public IActionResult FormAddCustomer() // affichage formulaire
        {
            ViewBag.banks = bankDAO.GetBanks();
            return View();
        }

        public IActionResult AddCustomer(string nom, string prenom, string telephone, int banqueid) // soumission du formulaire et ajout dans la base
        {
            Customer customer = new Customer(){
                LastName = nom,
                FirstName = prenom,
                Phone = telephone,
                BankID = banqueid
            };

            bankDAO.AddCustomer(customer);
            return RedirectToAction("CustomersForBank", new {id = banqueid});
        }

        public IActionResult Operations(int id) // id du client, // affichage formulaire
        {
            var customer = bankDAO.GetCustomers().Find(c => c.ID == id);
            var accounts = bankDAO.GetAccountsForCustomer(id);
            ViewBag.Customer = customer;
            ViewBag.Accounts = accounts;
            return View();
        }

        public IActionResult ValidOperation(int clientid, string operation, int compteid,  float montant) // soumission du formulaire et ajout dans la base
        {
            if(operation == "retrait")
                montant *= -1;

            float oldSold = bankDAO.GetAccountsForCustomer(clientid).Where(c => c.ID == compteid).First().Balance;  

            Account account = new Account(){
                ID = compteid,
                Balance = oldSold + montant
            };

            bankDAO.UpdateAccount(account);

            return RedirectToAction("AccountsForCustomer", new {id = clientid});
        }
    }
}
