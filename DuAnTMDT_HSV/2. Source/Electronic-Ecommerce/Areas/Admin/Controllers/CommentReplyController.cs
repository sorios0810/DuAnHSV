using Electronic_Ecommerce.Common.Helpers;
using Domain.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace Electronic_Ecommerce.Areas.Admin.Controllers
{
    public class CommentReplyController : BaseController
    {
        readonly CmsDbContext db = new CmsDbContext();
        public ActionResult CmtIndex(int? size, int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Access() == true || User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true ||
                User.Identity.Permiss_Modify() == true || User.Identity.Permiss_Delete() == true)
                {
                    ViewBag.rep_comment = db.ReplyComments.ToList();
                    var pageSize = (size ?? 10);
                    var pageNumber = (page ?? 1);
                    var list = from a in db.NewComments
                               where (a.status != "0")
                               orderby a.create_at descending
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
        //danh sách reply comment
        public ActionResult ReplyIndex(int? size, int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Access() == true || User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true ||
                User.Identity.Permiss_Modify() == true || User.Identity.Permiss_Delete() == true)
                {
                    var pageSize = (size ?? 10);
                    var pageNumber = (page ?? 1);
                    var list = from a in db.ReplyComments
                               orderby a.create_at descending
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
        //trả lời bình luận
        public async Task <ActionResult> ReplyComment(int comment_id,string reply_comment_content,ReplyComment ReplyComments)
        {
            bool result; 
            try
            {
                if (User.Identity.Permiss_Modify() == true)
                {
                    ReplyComments.comment_id = comment_id;
                    ReplyComments.reply_comment_content = reply_comment_content;
                    ReplyComments.account_id = User.Identity.GetUserId();
                    ReplyComments.status = "2";
                    ReplyComments.create_at = DateTime.Now;
                    db.ReplyComments.Add(ReplyComments);
                    var comment = db.NewComments.FirstOrDefault(m => m.comment_id == comment_id);
                    comment.status = "2";
                    db.Entry(comment).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    result = true;
                    Notification.set_flash("Đã trả lời comment id " + comment_id + "", "success");
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //duyệt tất cả bình luận
        public JsonResult ApprovedAllComment()
        {
            bool result;
            try
            {
                if (User.Identity.Permiss_Update() == true || User.Identity.Permiss_Modify() == true)
                {
                    List<NewComment> comment = db.NewComments.ToList();
                    foreach (var item in comment)
                    {
                        if (item.status == "1" && User.Identity.Permiss_Update() == true)
                        {
                            item.status = "2";//nếu trạng thái chờ duyệt "1" thì chuyển qua đã duyệt "2"
                            db.SaveChanges();
                        }
                    }
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //duyệt từng bình luận
        [HttpPost]
        public JsonResult AcceptComment(int comment_id)
        {
            bool result = false;
            try
            {
                if (User.Identity.Permiss_Update() == true || User.Identity.Permiss_Modify() == true)
                {
                    var comment = db.NewComments.FirstOrDefault(m => m.comment_id == comment_id && m.status == "1");
                    if (comment != null)
                    {
                        comment.status = "2";//đã duyệt
                        db.SaveChanges();
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //xóa bình luận
        [HttpPost]
        public JsonResult DeleteComment(int del_comment_id)
        {
            bool result = false;
            try
            {
                if (User.Identity.Permiss_Delete() == true)
                {
                    var comment = db.NewComments.FirstOrDefault(m => m.comment_id == del_comment_id && m.status == "1");
                    if (comment != null)
                    {
                        db.NewComments.Remove(comment);
                        db.SaveChanges();
                        result = true;
                        Notification.set_flash("Xóa thành công id "+ del_comment_id + "", "success");
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //duyệt tất cả reply comment
        public JsonResult ApprovedAllReplyComment()
        {
            bool result = false;
            try
            {
                if (User.Identity.Permiss_Update() == true || User.Identity.Permiss_Modify() == true)
                {
                    List<ReplyComment> repcomment = db.ReplyComments.ToList();
                    foreach (var item in repcomment)
                    {
                        if (item.status == "1")
                        {
                            item.status = "2";
                            db.SaveChanges();
                            result = true;
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //duyệt từng reply comment
        [HttpPost]
        public JsonResult AcceptReplyComment(int rep_comment_id)
        {
            bool result = false;
            try
            {
                if (User.Identity.Permiss_Update() == true || User.Identity.Permiss_Modify() == true)
                {
                    var repcomment = db.ReplyComments.FirstOrDefault(m => m.reply_comment_id == rep_comment_id && m.status == "1");
                    if (repcomment != null)
                    {
                        repcomment.status = "2";
                        db.SaveChanges();
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //xóa reply comment
        [HttpPost]
        public JsonResult DeleteRepComment(int del_rep_comment_id)
        {
            bool result = false;
            try
            {
                if (User.Identity.Permiss_Delete() == true)
                {
                    var repcomment = db.ReplyComments.FirstOrDefault(m => m.reply_comment_id == del_rep_comment_id && m.status == "1");
                    if (repcomment != null)
                    {
                        db.ReplyComments.Remove(repcomment);
                        db.SaveChanges();
                        result = true;
                        Notification.set_flash("Xóa thành công id " + del_rep_comment_id + "", "success");
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}