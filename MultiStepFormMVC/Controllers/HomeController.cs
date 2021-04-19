using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MultiStepFormMVC.Utils;
using MultiStepFormMVC.Models;

namespace MultiStepFormMVC.Utils
{
    public class HomeController : Controller
    {
        private const string SessionName = "SESSION_NAME";
        private const string CustomerSessionKey = "CUSTOMER_SESSION_KEY";

        public ActionResult Index()
        {
            /*
            int numberOfVisits = 0; // default value

            var sessionValue = HttpContext.Session.Get<int?>(SessionName);

            if (sessionValue != null)
            {
                numberOfVisits = (int) sessionValue;
            }

            // increment number of visits
            HttpContext.Session.Set(SessionName, numberOfVisits + 1);

            ViewBag.NumberOfVisits = numberOfVisits;
            */


            return View("BasicDetails");
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

                    return View("AddressDetails");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddressDetails(AddressDetailsViewModel data, string prevBtn, string nextBtn)
        {
            var customer = GetCustomer();

            if (prevBtn != null)
            {
                var basicDetailsModel = new BasicDetailsViewModel();


                basicDetailsModel.CustomerID = customer.CustomerID;
                basicDetailsModel.CompanyName = customer.CompanyName;

                return View("BasicDetails", basicDetailsModel);
            }
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    customer.Address = data.Address;
                    customer.City = data.City;
                    customer.Country = data.Country;
                    customer.PostCode = data.PostCode;

                    SaveCustomer(customer);

                    return View("ContactDetails");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ContactDetails(ContactDetailsViewModel data, string prevBtn, string nextBtn)
        {
            var customer = GetCustomer();

            if (prevBtn != null)
            {
                var addressDetailsViewModel = new AddressDetailsViewModel();

                addressDetailsViewModel.Address = customer.Address;
                addressDetailsViewModel.City = customer.City;
                addressDetailsViewModel.Country = customer.Country;
                addressDetailsViewModel.PostCode = customer.PostCode;

                return View("AddressDetails", addressDetailsViewModel);
            }
            if (nextBtn != null)
            {
                customer.ContactName = data.ContactName;
                customer.Phone = data.Phone;
                customer.Fax = data.Fax;

                SaveCustomer(customer);


                if (ModelState.IsValid)
                {
                    //customer.ContactName = data.ContactName;
                    //customer.Phone = data.Phone;
                    //customer.Fax = data.Fax;


                    //NorthwindEntities db = new NorthwindEntities();
                    //db.Customers.Add(obj);
                    //db.SaveChanges();

                    // Save to db

                    RemoveCustomer();
                    return View("Success");
                }

            }
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