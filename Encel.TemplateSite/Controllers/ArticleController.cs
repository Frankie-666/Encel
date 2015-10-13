using System;
using System.Web.Mvc;
using Encel.Logging;
using Encel.TemplateSite.Models;

namespace Encel.TemplateSite.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger _logger = LogManager.GetLogger();

        public ActionResult Index(Article currentContent)
        {
            _logger.Trace(() => $"Arrived in Article controller for {currentContent.ContentUri}");

            //try
            //{
            //    var d = 0;
            //    var b = 2 / d;
            //}
            //catch (Exception ex)
            //{
            //    _logger.Error(() => "Calculation failed.", ex);
            //    throw;
            //}
            
            return View(currentContent);
        }
    }
}