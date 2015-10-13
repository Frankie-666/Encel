using System.Web.Mvc;
using $rootnamespace$.Models;

namespace $rootnamespace$.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Index(Article currentContent)
        {
            return View(currentContent);
        }
    }
}