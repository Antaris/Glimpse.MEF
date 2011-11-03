namespace Glimpse.MEF.Sample.Controllers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Web.Mvc;

    using Models;
    using Models.Logging;

    /// <summary>
    /// Defines the home controller.
    /// </summary>
    [ExportController("Home"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Controller
    {
        #region Fields
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="HomeController"/>.
        /// </summary>
        /// <param name="logger">The logger.</param>
        [ImportingConstructor]
        public HomeController(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");

            _logger = logger;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Displays the default view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            _logger.Log("Entered HomeController.Index() action.");

            ViewBag.Message = "Welcome to ASP.NET MVC!";

            _logger.Log("Returning result from HomeController.Index() action.");
            return View();
        }

        /// <summary>
        /// Displays the about view.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
        #endregion
    }
}
