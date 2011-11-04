Glimpse.MEF
===========

A Glimpse plugin used to interrogate and list information about the container and the attempts at exports.

Getting Started
---------------

To get started, either build and add a reference to Glimpse.MEF.dll, or use Nuget to install the package "Glimpse.MEF".

This plugin works by replacing your `CompositionContainer`, so simply replace this:

    var container = new CompositionContainer(new DirectoryCatalog("bin"));
    
With:

    var container = new GlimpseCompositionContainer(new DirectoryCatalog("bin"));
    
Visualising your Container
--------------------------

The first thing you can do once Glimpse is enabled, is check out the new Plugin tab "MEF Container". This tab will display all information about your container, such as the catalogs currently registered, and the parts available in them:

![1]

This should allow you to easily see when parts are missing from your container. I hope to expand on this future to support `ExportProvider` descriptors also.
*Fun note - Glimpse itself is built on the Managed Extensibility Framework (MEF) which means you can visualise all the parts that make up this awesome tool :-)*

Visualising your Composition
----------------------------

The next tab is the "MEF Imports" tab, which details all the attempts to get exports from your container:

![2]

With this view, we can see what parts are being composed, this includes any dependencies that are automatically injected into the composed parts. 
The above image represents a basic example of the following class being exported as an MVC Controller, with dependency injection:

    [ExportController("Home"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        
        [ImportingConstructor]
        public HomeController(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");

            _logger = logger;
        }
        
        public ActionResult Index()
        {
            _logger.Log("Entered HomeController.Index() action.");

            ViewBag.Message = "Welcome to ASP.NET MVC!";

            _logger.Log("Returning result from HomeController.Index() action.");
            return View();
        }
    }



 [1]: http://www.fidelitydesign.net/wp-content/uploads/2011/11/MEF-Container.png "MEF Container Tab"
 [2]: http://www.fidelitydesign.net/wp-content/uploads/2011/11/MEF-Imports.png "MEF Imports Tab"