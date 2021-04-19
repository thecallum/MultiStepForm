using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MultiStepFormMVC.Utils;
using MultiStepFormMVC.Models;

namespace MultiStepFormMVC.Utils
{
    public class HomeController : Controller
    {
        private const string CustomerSessionKey = "CUSTOMER_SESSION_KEY";

        public ActionResult Index()
        {
            return View("Start");
        }

        [HttpGet]
        public ActionResult BasicDetails()
        {
            var customer = GetCustomer();

            var viewModel = new BasicDetailsViewModel
            {
                CustomerID = customer.CustomerID,
                CompanyName = customer.CompanyName
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BasicDetails(BasicDetailsViewModel data, string prevBtn, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    var customer = GetCustomer();

                    customer.CustomerID = data.CustomerID;
                    customer.CompanyName = data.CompanyName;

                    SaveCustomer(customer);

                    return RedirectToAction("AddressDetails");
                }
            }

             return View(data);
        }

        [HttpGet]
        public ActionResult AddressDetails()
        {
            var customer = GetCustomer();

            var viewModel = new AddressDetailsViewModel
            {
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                PostCode = customer.PostCode
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddressDetails(AddressDetailsViewModel data, string prevBtn, string nextBtn)
        {
            if (prevBtn != null)
            {
                return RedirectToAction("BasicDetails");
            }

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    var customer = GetCustomer();

                    customer.Address = data.Address;
                    customer.City = data.City;
                    customer.Country = data.Country;
                    customer.PostCode = data.PostCode;

                    SaveCustomer(customer);

                    return RedirectToAction("ContactDetails");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult ContactDetails()
        {
            var customer = GetCustomer();

            var viewModel = new ContactDetailsViewModel
            {
               ContactName = customer.ContactName,
               Fax = customer.Fax,
               Phone = customer.Phone
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ContactDetails(ContactDetailsViewModel data, string prevBtn, string nextBtn)
        {
            if (prevBtn != null)
            {
                return RedirectToAction("AddressDetails");
            }

            if (nextBtn != null) { 

                if (ModelState.IsValid)
                {
                    var customer = GetCustomer();

                    customer.ContactName = data.ContactName;
                    customer.Phone = data.Phone;
                    customer.Fax = data.Fax;

                    SaveCustomer(customer);

                    // Save to db

                    RemoveCustomer();

                    return RedirectToAction("Success");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Success()
        {
            return View();
        }

        private void SaveCustomer(Customer customer)
        {
            HttpContext.Session.Set<Customer>(CustomerSessionKey, customer);
        }

        private Customer GetCustomer()
        {
            var customer = HttpContext.Session.Get<Customer>(CustomerSessionKey);
            if (customer != null) return customer;

            // not found in session
            var newCustomer = new Customer();

            return newCustomer;
        }

        private void RemoveCustomer()
        {
            HttpContext.Session.Remove(CustomerSessionKey);
        }
    }
}