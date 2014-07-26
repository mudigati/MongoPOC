using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoPOC.Models;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace MongoPOC.Controllers
{
    public class MongoController : Controller
    {
        MongoClient mongoClient;
        MongoServer mongoServer;
        MongoDatabase mongoDatabase;

        const string ConnectionString = "mongodb://localhost";

        public MongoController()
        {
            mongoClient = new MongoClient(ConnectionString);
            mongoServer = mongoClient.GetServer();
            mongoDatabase = mongoServer.GetDatabase("Kamal");
        }

        // GET: Mongo
        public ActionResult Index()
        {
            MongoCollection employeeCollection = mongoDatabase.GetCollection<Employee>("EmployeeCollection");

            //employeeCollection.RemoveAll();

            //employeeCollection = mongoDatabase.GetCollection<Employee>("EmployeeCollection");

            return View(employeeCollection.AsQueryable<Employee>());
        }

        // GET: Mongo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mongo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mongo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                MongoCollection employeeCollection = mongoDatabase.GetCollection<Employee>("EmployeeCollection");

                Employee e = new Employee();
                e.EmployeeName = collection.Get("EmployeeName");

                employeeCollection.Insert<Employee>(e);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mongo/Edit/5
        public ActionResult Edit(string id)
        {

            ObjectId MongoId;
            ObjectId.TryParse(id, out MongoId);
            MongoCollection employeeCollection = mongoDatabase.GetCollection<Employee>("EmployeeCollection");
            Employee e = employeeCollection.AsQueryable<Employee>()
                .Where(x => x._id == MongoId).SingleOrDefault();
            return View(e);
        }

        // POST: Mongo/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {

                ObjectId MongoId;
                ObjectId.TryParse(id, out MongoId);
                // TODO: Add update logic here
                MongoCollection employeeCollection = mongoDatabase.GetCollection<Employee>("EmployeeCollection");
                Employee e = employeeCollection.AsQueryable<Employee>()
                .Where(x => x._id == MongoId).SingleOrDefault();

                e.EmployeeName = collection["EmployeeName"];

                employeeCollection.Save<Employee>(e);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mongo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mongo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
