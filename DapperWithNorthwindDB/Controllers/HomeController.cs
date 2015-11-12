using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Repositories;
using ModelLayer;
using System.Windows;
using System.Windows.Forms;

namespace DapperWithNorthwindDB.Controllers
{
    public class HomeController : Controller
    {
        private RegionRepo _repo = new RegionRepo();

        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult CreateRegion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRegionPost()
        {
            Region region = new Region();

            region.RegionDescription = Request.Form["regionDescription"];
            _repo.Create(region);

            return RedirectToAction("Index");
        }

        public ActionResult RetrieveRegion(int id)
        {
            Region region = _repo.Retrieve(id);

            return View(region);
        }

        public ActionResult EditRegion(int id)
        {
            Region region = _repo.Retrieve(id);

            return View(region);
        }

        [HttpPost]
        public ActionResult EditRegionPost(Region region)
        {
            _repo.Update(region);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteRegion(int id)
        {
            try
            {
                _repo.Delete(id);
            }
            catch (SqlException e)
            {
                MessageBox.Show("FOREIGN KEY EXCEPTION: " + e, "SQL Server Exception");
                return RedirectToAction("RetrieveRegion/" + id);
            }

            return RedirectToAction("Index");
        }
    }
}