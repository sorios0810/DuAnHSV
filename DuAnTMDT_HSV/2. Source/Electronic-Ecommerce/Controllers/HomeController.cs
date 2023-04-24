using Electronic_Ecommerce.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using System.Windows.Input;
using Org.BouncyCastle.Asn1.Misc;

namespace Electronic_Ecommerce.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private CmsDbContext db = new CmsDbContext();

        public ActionResult Index()
        {
            //Sản phẩm mới
            var newProducts = db.Database.SqlQuery<Product>("exec sp_SanPhamMoi").ToList();
            ViewBag.NewProduct = newProducts;

            //Sản phẩm bán chạy
            var bestSellingProducts = db.Database.SqlQuery<Product>("exec sp_SanPhamBanChay").ToList();
            ViewBag.BestSelling = bestSellingProducts;

            //Sản phẩm nổi bật
            var hotProducts = db.Database.SqlQuery<Product>("exec sp_SanPhamNoiBat").ToList();
            ViewBag.HotProduct = hotProducts;

            //Danh sách laptop
            var listLaptop = db.Database.SqlQuery<Product>("exec sp_DanhSachLaptop").Take(10).ToList();
            ViewBag.ListLaptop = listLaptop;

            //Danh sách PC
            var listPC = db.Database.SqlQuery<Product>("exec sp_DanhSachPC").Take(10).ToList();
            ViewBag.ListPC = listPC;

            //Danh sách thiết bị văn phòng
            var officeEquipment = db.Database.SqlQuery<Product>("exec sp_DanhSachThietBiVanPhong").Take(10).ToList();
            ViewBag.OfficeEquipment = officeEquipment;

            //Trung bình sao
            ViewBag.AvgFeedback = db.Feedbacks.ToList();
            ViewBag.OrderFeedback = db.OrderDetails.ToList();

            //Lấy danh sách từ lớp Genre để hiển thị. ParentGenreId mà bạn muốn lấy các thể loại con tương ứng
            var genres = db.Database.SqlQuery<Genre>("exec sp_DsLaptopNoiBat @parentGenreId", new SqlParameter("@parentGenreId", 21)).ToList();
            ViewBag.Genres = genres;

            List<New> recentnews = db.News.Where(item => item.status == "1").Take(10).OrderByDescending(item => item.create_at).ThenBy(m => m.ViewCount).ToList();
            ViewBag.Recentnews = recentnews;

            List<Brand> brand = db.Brands.Where(item => item.status == "1").Take(20).OrderByDescending(m => m.Products.Count()).ToList();
            ViewBag.Brand = brand;

            //Banner khuyến mãi
            ViewBag.BannerHeader = db.Banners.OrderByDescending(m => Guid.NewGuid()).Where(m => m.banner_start < DateTime.Now && m.banner_end > DateTime.Now && m.status == "1" && m.banner_type == 1).Take(16).ToList();
            ViewBag.BannerBottom = db.Banners.OrderByDescending(m => m.banner_id).Where(m => m.banner_start < DateTime.Now && m.banner_end > DateTime.Now && m.status == "1" && m.banner_type == 2).Take(4).ToList();
            ViewBag.BannerVertical = db.Banners.OrderByDescending(m => m.banner_id).Where(m => m.banner_start < DateTime.Now && m.banner_end > DateTime.Now && m.status == "1" && m.banner_type == 4).Take(1).ToList();
            
            //Sản phẩm khuyến mãi
            ViewBag.BannerDetail = db.BannerDetails.ToList();

            //Trung bình sao
            ViewBag.AvgFeedback = db.Feedbacks.ToList();
            ViewBag.OrderFeedback = db.OrderDetails.ToList();

            BannerGlobal();
            return View();
        }

        //Error 404 hiện khi sai URL
        public ActionResult PageNotFound()
        {
            BannerGlobal();
            return View();
        }

        //Hiển thị banner toàn layout
        public void BannerGlobal()
        {
            ViewBag.BannerTopHorizontal = db.Banners.OrderByDescending(m => Guid.NewGuid()).Where(m => m.banner_start < DateTime.Now && m.banner_end > DateTime.Now && m.status == "1" && m.banner_type == 3).Take(8).ToList();
        }
    }
}