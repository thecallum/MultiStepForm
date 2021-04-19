using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MultiStepFormMVC.Utils;

namespace MultiStepFormMVC.Utils
{
    public class HomeController : Controller
    {
        private const string SessionName = "SESSION_NAME";

        public ActionResult Index()
        {
            int numberOfVisits = 0; // default value

            var sessionValue = HttpContext.Session.Get<int?>(SessionName);

            if (sessionValue != null)
            {
                numberOfVisits = (int) sessionValue;
            }

            // increment number of visits
            HttpContext.Session.Set(SessionName, numberOfVisits + 1);

            ViewBag.NumberOfVisits = numberOfVisits;

            return View("BasicDetails");
        }
    }
}