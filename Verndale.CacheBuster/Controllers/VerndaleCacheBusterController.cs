using System.Linq;
using System.Web.Mvc;
using EPiServer.Logging;
using EPiServer.PlugIn;
using Verndale.CacheBuster.Models;
using Verndale.CacheBuster.Repositories;
using Verndale.CacheBuster.Services;
using Shell = EPiServer.Shell;
using PlugInArea = EPiServer.PlugIn.PlugInArea;

namespace Verndale.CacheBuster.Controllers
{
    [GuiPlugIn(Area = PlugInArea.AdminMenu, UrlFromModuleFolder = "CacheBuster", DisplayName = "Cache Buster")]
    public class VerndaleCacheBusterController : Controller
    {
        #region Properties

        protected readonly ICacheRepository CacheRepository;
        private static readonly ILogger Logger = LogManager.GetLogger();

        #endregion

        #region Constructor

        public VerndaleCacheBusterController(ICacheRepository cacheRepository)
        {
            Logger.Debug("Constructor VerndaleCacheBusterController()");
            CacheRepository = cacheRepository;
        }

        #endregion

        public ActionResult Index()
        {
            Logger.Debug("Begin Index()");
            return View(GetViewLocation("Index"));
        }

        public ActionResult Remove(string key, string term, int page)
        {
            Logger.Debug($"Begin Remove({key}, {term}, {page})");
            CacheRepository.Remove(key);
            return RedirectToAction("Retrieve", new { term = term, page = page });
        }

        private static string GetViewLocation(string viewName)
        {
            return $"{Shell.Paths.ProtectedRootPath}Verndale.CacheBuster/Views/CacheBuster/{viewName}.cshtml";
        }

        public ActionResult Retrieve(string term, int? page)
        {
            Logger.Debug($"Begin Retrieve({term ?? "NULL"}, {page})");

            const int pageSize = 15;

            if (!page.HasValue)
            {
                page = 1;
            }

            if (term == null)
            {
                term = string.Empty;
            }

            var cacheItems = CacheRepository.GetByTerm(term);
            var model = new Pagination<CacheItem>(cacheItems.AsQueryable(), page.Value, pageSize);

            ViewBag.Term = term;

            return View(GetViewLocation("Index"), model: model);
        }
    }
}