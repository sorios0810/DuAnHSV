using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Entities;
using Electronic_Ecommerce;
using Electronic_Ecommerce.Common.Helpers;
using PagedList;

namespace Electronic_Ecommerce.Areas.Admin.Controllers
{
    public class APIController : BaseController
    {
        private CmsDbContext db = new CmsDbContext();
        public ActionResult APIIndex(string search,string show, int ? size,int ? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.GetRole() == 4)
                {
                    var pageSize = (size ?? 9);
                    var pageNumber = (page ?? 1);
                    var list = from a in db.API_Key
                               orderby a.id descending
                               select a;
                    return View(list.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return Redirect("~/Account/SignIn");
            }
        }
        //sửa tag
        public JsonResult APIEdit(string apiname, string client_id, string client_secret, int id)
        {
            Boolean result;
            if (User.Identity.GetRole()==4 )
            {
                API_Key api = db.API_Key.Find(id);
                api.api_name = apiname;
                api.client_id = client_id;
                api.client_secret = client_secret;
                api.update_at = DateTime.Now;
                db.SaveChanges();
                result = true;
                Notification.set_flash("Cập nhật API id " + id + " thành công", "success");
            }
            else
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //thay đổi trạng thái
        [HttpPost]
        public JsonResult ChangeStatus(int id, bool state = false)
        {
            bool result;
            if (User.Identity.GetRole() == 4)
            {
                API_Key aPI = db.API_Key.FirstOrDefault(m => m.id == id);
                aPI.active = state;
                aPI.update_at = DateTime.Now;
                db.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
