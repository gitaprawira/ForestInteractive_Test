using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForestInteractiveTest.Models;

namespace ForestInteractiveTest.Controllers
{
    public class HomeController : Controller
    {
        private UsersContext db = new UsersContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ContactList()
        {
            List<Contact> model = Contact.GetContactList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Check(string message)
        {
            var result = Transaction.CheckMessage(message);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(string message)
        {
            var result = false;
            if (Request != null)
            {
                var file = Request.Files["FileUpload"];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var filePath = Server.MapPath("~\\UploadedFiles\\") + file.FileName;
                    file.SaveAs(Server.MapPath("~\\UploadedFiles\\") + file.FileName);

                    Contact.DoImport(filePath);
                }

                Transaction model = new Transaction();
                model.DateCreated = DateTime.Now;
                model.UserCreated = "Administrator";
                model.Message = message;
                db.Transaction.Add(model);
                db.SaveChanges();
                result = true;
            }

            return Json(result);
        }
        
        public ActionResult Delete(int id)
        {
            Contact model = new Contact { Id = id };
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}