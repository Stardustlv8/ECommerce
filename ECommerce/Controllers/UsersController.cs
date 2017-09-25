using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Classes;

namespace ECommerce.Controllers
{
    public class UsersController : Controller
    {
        private ECommerceContext db = new ECommerceContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.City).Include(u => u.Company).Include(u => u.Department);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(), 
                "CityId", "Name");

            ViewBag.CompanyId = new SelectList(
                CombosHelper.GetCompanies(), 
                "CompanyId", "Name");

            ViewBag.DepartmentId = new SelectList(
                CombosHelper.GetDepartments(), 
                "DepartmentId", "Name");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                if (user.PhotoFile!=null)
                {
                    var folder = "~/Content/Users";
                    var file = string.Format($"{user.UserId}.jpg");
                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format($"{folder}/{file}");
                        user.Photo = pic;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(), 
                "CityId", "Name", 
                user.CityId);

            ViewBag.CompanyId = new SelectList(
                CombosHelper.GetCompanies(), 
                "CompanyId", "Name", 
                user.CompanyId);

            ViewBag.DepartmentId = new SelectList(
                CombosHelper.GetDepartments(), 
                "DepartmentId", "Name", 
                user.DepartmentId);

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(), 
                "CityId", "Name", 
                user.CityId);

            ViewBag.CompanyId = new SelectList(
                CombosHelper.GetCompanies(), 
                "CompanyId", "Name", 
                user.CompanyId);

            ViewBag.DepartmentId = new SelectList(
                CombosHelper.GetDepartments(), 
                "DepartmentId", "Name", 
                user.DepartmentId);

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.PhotoFile != null)
                {
                    var folder = "~/Content/Users";
                    var file = string.Format($"{user.UserId}.jpg");
                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format($"{folder}/{file}");
                        user.Photo = pic;
                    }

                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(), 
                "CityId", "Name", 
                user.CityId);

            ViewBag.CompanyId = new SelectList(
                CombosHelper.GetCompanies(), 
                "CompanyId", "Name", 
                user.CompanyId);

            ViewBag.DepartmentId = new SelectList(
                CombosHelper.GetDepartments(), 
                "DepartmentId", "Name", 
                user.DepartmentId);

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult GetCities(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(m => m.DepartmentId == departmentId);
            return Json(cities);
        }
        public JsonResult GetCompanies(int cityId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var companies = db.Companies.Where(m => m.CityId == cityId);
            return Json(companies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
