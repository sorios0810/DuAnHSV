using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Electronic_Ecommerce.Common.Helpers;
using Electronic_Ecommerce.DTOs;
using Domain.Entities;
using PagedList;
using Domain;
using Microsoft.Ajax.Utilities;

namespace Electronic_Ecommerce.Areas.Admin.Controllers
{
    public class ProductsAdminController : BaseController
    {
        private readonly CmsDbContext _db = new CmsDbContext();
        //View list sản phẩm
        public ActionResult ProductIndex(int? size, int? page, string productName, string show, string sortOrder) // hiển thị tất cả sp online
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Access() == true || User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true ||
                User.Identity.Permiss_Modify() == true || User.Identity.Permiss_Delete() == true)
                {
                    var pageSize = size ?? 10;
                    var pageNumber = page ?? 1;
                    ViewBag.countTrash = _db.Products.Count(a => a.status == "0"); //  đếm tổng sp có trong thùng rác
                    ViewBag.countProductsAdmin = _db.Products.Count(a => a.status != "2"); //  đếm tổng sp
                    ViewBag.CurrentSort = sortOrder;
                    ViewBag.ResetSort = "";
                    ViewBag.DateSortParm = sortOrder == "date_asc" ? "date_desc" : "date_asc";
                    ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
                    ViewBag.PriceSortParm = sortOrder == "price_asc" ? "price_desc" : "price_asc";
                    ViewBag.QuantitySortParm = sortOrder == "quantity_asc" ? "quantity_desc" : "quantity_asc";
                    ViewBag.GenreSortParm = sortOrder == "genre_asc" ? "genre_desc" : "genre_asc";
                    ViewBag.BrandSortParm = sortOrder == "brand_asc" ? "brand_desc" : "brand_asc";
                    ViewBag.ViewSortParm = "view_desc";
                    ViewBag.BuySortParm = "buy_desc";
                    ViewBag.LaptopSortParm = "laptop_sort";
                    ViewBag.ComponentSortParm = "component_sort";
                    ViewBag.MonitorSortParm = "monitor_sort";
                    ViewBag.TableSortParm = "table_sort";
                    ViewBag.AccessorySortParm = "accessory_sort";
                    var list = from a in _db.Products
                               join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                               join d in _db.Brands on a.brand_id equals d.brand_id
                               join inv in _db.Inventories on a.product_id equals inv.product_id
                               join e in _db.Discounts on a.discount_id equals e.discount_id
                               where a.status == "1"
                               orderby a.product_id descending
                               select new ProductDTO
                               {
                                   product_name = a.product_name,
                                   quantity = inv.quantity_on_hand,
                                   price = a.selling_price,
                                   Image = a.image,
                                   genre_name = c.genre_name,
                                   taxes_value = a.Tax.taxes_value,
                                   status = a.status,
                                   brand_name = d.brand_name,
                                   discount_name = e.discount_name,
                                   discount_start = e.discount_start,
                                   discount_end = e.discount_end,
                                   discount_price = e.discount_price,
                                   discount_status = e.status,
                                   product_id = a.product_id,
                                   slug = a.slug,
                               };
                    //sắp xếp
                    switch (sortOrder)
                    {
                        case "date_desc":
                            ViewBag.sortname = "Mới nhất";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where a.status == "1"
                                   orderby a.create_at descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       taxes_value = a.Tax.taxes_value,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "date_asc":
                            ViewBag.sortname = "Cũ nhất";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where a.status == "1"
                                   orderby a.product_id ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       taxes_value = a.Tax.taxes_value,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "name_asc":
                            ViewBag.sortname = "Tên sản phẩm (A - Z)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.product_name ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "name_desc":
                            ViewBag.sortname = "Tên sản phẩm (Z - A)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.product_name descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;

                        case "laptop_sort":
                            ViewBag.sortname = "Laptop";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1" && a.GenreDetail.Genre.ParentGenre.id == 2)
                                   orderby a.product_id descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "component_sort":
                            ViewBag.sortname = "Linh kiện";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1" && a.GenreDetail.Genre.ParentGenre.id == 4)
                                   orderby a.product_id descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "monitor_sort":
                            ViewBag.sortname = "Màn hình";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1" && a.GenreDetail.Genre.ParentGenre.id == 10)
                                   orderby a.product_id descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "table_sort":
                            ViewBag.sortname = "Bàn - ghế";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1" && a.GenreDetail.Genre.ParentGenre.id == 8)
                                   orderby a.product_id descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "accessory_sort":
                            ViewBag.sortname = "Phụ kiện";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1" && a.GenreDetail.Genre.ParentGenre.id == 3)
                                   orderby a.product_id descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "price_asc":
                            ViewBag.sortname = "Giá (thấp - cao)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby (a.selling_price - a.Discount.discount_price) ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "price_desc":
                            ViewBag.sortname = "Giá (cao - thấp)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby (a.selling_price - a.Discount.discount_price) descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "buy_desc":
                            ViewBag.sortname = "Mua nhiều";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.buyturn descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "view_desc":
                            ViewBag.sortname = "Xem nhiều";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.view descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "quantity_asc":
                            ViewBag.sortname = "Số lượng (ít - nhiều)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby i.quantity_on_hand ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "quantity_desc":
                            ViewBag.sortname = "Số lượng (nhiều - ít)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby i.quantity_on_hand descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "genre_asc":
                            ViewBag.sortname = "Tên thể loại (A - Z)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.GenreDetail.Genre.genre_name ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "genre_desc":
                            ViewBag.sortname = "Tên thể loại (Z - A)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.GenreDetail.Genre.genre_name descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "brand_asc":
                            ViewBag.sortname = "Tên thương hiệu (A - Z)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.Brand.brand_name ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        case "brand_desc":
                            ViewBag.sortname = "Tên thương hiệu (A - Z)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.Brand.brand_name descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       slug = a.slug,
                                   };
                            break;
                        default: // Name ascending
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "1")
                                   orderby a.product_id descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       product_id = a.product_id,
                                       discount_name = e.discount_name,
                                       taxes_value = a.Tax.taxes_value,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       slug = a.slug,
                                   };
                            break;
                    }
                    //search filter
                    if (string.IsNullOrEmpty(productName)) return View(list.ToPagedList(pageNumber, pageSize));
                    switch (show)
                    {
                        //tìm kiếm tất cả
                        case "1":
                            list = list.Where(s => s.product_name.Trim().Contains(productName) ||
                                                   s.product_id.ToString().Trim().Contains(productName) ||
                                                   s.genre_name.Trim().Contains(productName)
                                                   || s.quantity.ToString().Trim().Contains(productName) ||
                                                   s.brand_name.ToString().Trim().Contains(productName) ||
                                                   s.price.ToString().Trim().Contains(productName));
                            break;
                        //theo tên sản phẩm
                        case "2":
                            list = list.Where(s => s.product_name.Contains(productName));
                            break;
                        //theo giá sản phẩm
                        case "3":
                            list = list.Where(s => s.price.ToString().Contains(productName));
                            break;
                        //theo id sản phẩm
                        case "4":
                            list = list.Where(s => s.product_id.ToString().Contains(productName));
                            break;
                        //theo thương hiệu
                        case "5":
                            list = list.Where(s => s.brand_name.Trim().Contains(productName));
                            break;
                        //theo thể loại
                        case "6":
                            list = list.Where(s => s.genre_name.Trim().Contains(productName));
                            break;
                        //theo ct giảm giá
                        case "7":
                            return View("ProductIndex", list.ToPagedList(pageNumber, 50));
                    }
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
        //gợi ý tìm kiếm
        [HttpPost]
        public JsonResult GetProductAdminSearch(string Prefix)
        {
            var search = (from c in _db.Products
                where c.product_name.Contains(Prefix)
                orderby c.product_name ascending
                select new { c.product_name, c.image, c.selling_price });
            return Json(search, JsonRequestBehavior.AllowGet);
        }
        //View list trash sản phẩm
        public ActionResult ProductTrash(string product_name, string show, int? size, int? page,string sortOrder) // hiển thị tất cả sp online
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Access() == true || User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true ||
                User.Identity.Permiss_Modify() == true || User.Identity.Permiss_Delete() == true)
                {
                    var pageSize = size ?? 10;
                    var pageNumber = page ?? 1;
                    ViewBag.CurrentSort = sortOrder;
                    ViewBag.ResetSort = "";
                    ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "id_asc" : "";
                    ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
                    ViewBag.UpdateParam = sortOrder == "update_asc" ? "update_desc" : "update_asc";
                    ViewBag.PriceSortParm = sortOrder == "price_asc" ? "price_desc" : "price_asc";
                    ViewBag.QuantitySortParm = sortOrder == "quantity_asc" ? "quantity_desc" : "quantity_asc";
                    ViewBag.GenreSortParm = sortOrder == "genre_asc" ? "genre_desc" : "genre_asc";
                    ViewBag.BrandSortParm = sortOrder == "brand_asc" ? "brand_desc" : "brand_asc";
                    ViewBag.ViewSortParm = "view_desc";
                    ViewBag.BuySortParm = "buy_desc";
                    var list = from a in _db.Products
                               join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                               join d in _db.Brands on a.brand_id equals d.brand_id
                               join i in _db.Inventories on a.product_id equals i.product_id
                               join e in _db.Discounts on a.discount_id equals e.discount_id
                               where a.status == "0"
                               orderby a.update_at descending // giảm dần
                               select new ProductDTO
                               {
                                   product_name = a.product_name,
                                   quantity = i.quantity_on_hand,
                                   price = a.selling_price,
                                   Image = a.image,
                                   update_at = a.update_at,
                                   genre_name = c.genre_name,
                                   brand_name = d.brand_name,
                                   status = a.status,
                                   product_id = a.product_id,
                                   discount_name = e.discount_name,
                                   count_Banner_detail = a.BannerDetails.Count,
                                   count_Order_detail = a.OrderDetails.Count,
                                   count_feedback = a.Feedbacks.Count
                               };
                    switch (sortOrder)
                    {
                        case "update_asc":
                            ViewBag.sortname = "Cũ nhất";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.update_at ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "update_desc":
                            ViewBag.sortname = "Mới nhất";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.update_at descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "name_asc":
                            ViewBag.sortname = "Tên sản phẩm (A - Z)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.product_name ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "name_desc":
                            ViewBag.sortname = "Tên sản phẩm (Z - A)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.product_name descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "price_asc":
                            ViewBag.sortname = "Giá (thấp - cao)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby (a.selling_price - a.Discount.discount_price) ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "price_desc":
                            ViewBag.sortname = "Giá (cao - thấp)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby (a.selling_price - a.Discount.discount_price) descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "buy_desc":
                            ViewBag.sortname = "Mua nhiều";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.buyturn descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "view_desc":
                            ViewBag.sortname = "Xem nhiều";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.view descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "quantity_asc":
                            ViewBag.sortname = "Số lượng (ít- nhiều)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby i.quantity_on_hand ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "quantity_desc":
                            ViewBag.sortname = "Số lượng (nhiều - ít)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby i.quantity_on_hand descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "genre_asc":
                            ViewBag.sortname = "Tên thể loại (A - Z)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.GenreDetail.Genre.genre_name ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "genre_desc":
                            ViewBag.sortname = "Tên thể loại (Z - A)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.GenreDetail.Genre.genre_name descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "brand_asc":
                            ViewBag.sortname = "Tên thương hiệu (A - Z)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.Brand.brand_name ascending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                        case "brand_desc":
                            ViewBag.sortname = "Tên thương hiệu (Z - A)";
                            list = from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join i in _db.Inventories on a.product_id equals i.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   where (a.status == "0")
                                   orderby a.Brand.brand_name descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = i.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       genre_name = c.genre_name,
                                       status = a.status,
                                       brand_name = d.brand_name,
                                       discount_name = e.discount_name,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_status = e.status,
                                       product_id = a.product_id,
                                       count_Banner_detail = a.BannerDetails.Count,
                                       count_Order_detail = a.OrderDetails.Count,
                                       count_feedback = a.Feedbacks.Count
                                   };
                            break;
                    }
                    if (!string.IsNullOrEmpty(product_name))
                    {
                        switch (show)
                        {
                            //tìm kiếm tất cả
                            case "1":
                                list = list.Where(s => s.product_name.Trim().Contains(product_name) ||
                                                       s.product_id.ToString().Trim().Contains(product_name) ||
                                                       s.genre_name.Trim().Contains(product_name)
                                                       || s.quantity.ToString().Trim().Contains(product_name) ||
                                                       s.brand_name.ToString().Trim().Contains(product_name) ||
                                                       s.price.ToString().Trim().Contains(product_name));
                                break;
                            //theo tên sản phẩm
                            case "2":
                                list = list.Where(s => s.product_name.ToString().Trim().Contains(product_name));
                                break;
                            //theo giá sản phẩm
                            case "3":
                                list = list.Where(s => s.price.ToString().Contains(product_name));
                                break;
                            //theo id sản phẩm
                            case "4":
                                list = list.Where(s => s.product_id.ToString().Trim().Contains(product_name));
                                break;
                            //theo thương hiệu
                            case "5":
                                list = list.Where(s => s.brand_name.Trim().Contains(product_name));
                                break;
                            //theo thể loại
                            case "6":
                                list = list.Where(s => s.genre_name.Trim().Contains(product_name));
                                break;
                            //theo ct giảm giá
                            case "7":
                                return View("ProductTrash", list.ToPagedList(pageNumber, 50));
                        }
                    }
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
        //Thông tin sản phẩm
        public ActionResult ProductDetails(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Read() == true || User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true ||
                User.Identity.Permiss_Modify() == true || User.Identity.Permiss_Delete() == true)
                {
                    var product = (from a in _db.Products
                                   join c in _db.Genres on a.GenreDetail.Genre.genre_id equals c.genre_id
                                   join d in _db.Brands on a.brand_id equals d.brand_id
                                   join inv in _db.Inventories on a.product_id equals inv.product_id
                                   join e in _db.Discounts on a.discount_id equals e.discount_id
                                   join i in _db.ProductImages on a.product_id equals i.product_id
                                   where a.product_id == id
                                   orderby a.create_at descending // giảm dần
                                   select new ProductDTO
                                   {
                                       product_name = a.product_name,
                                       quantity = inv.quantity_on_hand,
                                       price = a.selling_price,
                                       Image = a.image,
                                       image_1 = i.image_1,
                                       image_2 = i.image_2,
                                       image_3 = i.image_3,
                                       image_4 = i.image_4,
                                       image_5 = i.image_5,
                                       genre_name = c.genre_name,
                                       brand_name = d.brand_name,
                                       product_id = a.product_id,
                                       description = a.description,
                                       specification = a.specification,
                                       parent_genre_name = a.GenreDetail.Genre.ParentGenre.name,
                                       create_at = a.create_at,
                                       create_by = a.create_by,
                                       status = a.status,
                                       seo_title = a.title_seo,
                                       buyturn = a.buyturn,
                                       view = a.view,
                                       update_at = a.update_at,
                                       update_by = a.update_by,
                                       discount_start = e.discount_start,
                                       discount_end = e.discount_end,
                                       discount_price = e.discount_price,
                                       discount_name = e.discount_name,
                                       discount_status = e.status,
                                       discount_id = e.discount_id,
                                       product_img_id = i.product_image_id,
                                       genre_id = c.genre_id,
                                       brand_id = d.brand_id,
                                       count_post = a.NewProducts.Count
                                   }).FirstOrDefault();
                    ViewBag.countfeedback =
                        _db.Feedbacks.Count(a => a.status != "2" && a.product_id == id); //  đếm tổng đánh giá
                    if (product == null || id == null)
                    {
                        Notification.set_flash("Không tồn tại: " + product.product_name + "", "warning");
                        return RedirectToAction("ProductIndex");
                    }
                    return View(product);
                }
                else
                {
                    Notification.set_flash("Bạn không có quyền sử dụng chức năng này", "danger");
                    return RedirectToAction("ProductIndex");
                }
            }
            else
            {
                return Redirect("~/Account/SignIn");
            }
        }
        //View thêm sản phẩm
        public ActionResult ProductCreate()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Create() == true)
                {
                    ViewBag.ListParentGenre = new SelectList(_db.ParentGenres.Where(m => m.status == "1").OrderBy(m => m.name), "id", "name", 0);
                    ViewBag.ListDiscount = new SelectList(_db.Discounts.Where(m => m.status == "1" && m.discount_type == 1).OrderByDescending(m => m.discount_id), "discount_id", "discount_name", 0);
                    ViewBag.ListBrand = new SelectList( _db.Brands.Where(m => (m.status == "1")).OrderBy(m => m.brand_name),"brand_id", "brand_name", 0);
                    ViewBag.ListTaxes = new SelectList(_db.Taxes.OrderBy(m => m.taxes_name),"taxes_id", "taxes_name", 0);
                    ViewBag.ListGenre = new SelectList(_db.Genres.Where(m => (m.status == "1")).OrderBy(m => m.genre_name),"genre_id", "genre_name", 0);
                    return View();
                }
                else
                {
                    Notification.set_flash("Bạn không có quyền sử dụng chức năng này", "danger");
                    return RedirectToAction("ProductIndex");
                }
            }
            else
            {
                return Redirect("~/Account/SignIn");
            }
        }
        //Code xử lý thêm sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> ProductCreate(ProductDTO Prodtos, Product product, ProductImage product_image)
        {
            // ViewBag.ListParentGenre chỉ để hiện trên view
            ViewBag.ListParentGenre = new SelectList(_db.ParentGenres.Where(m => m.status == "1").OrderBy(m => m.name),"id", "name", 0);
            ViewBag.ListDiscount = new SelectList( _db.Discounts.Where(m => m.status == "1" && m.discount_type == 1).OrderByDescending(m => m.discount_id), "discount_id", "discount_name", 0);
            ViewBag.ListBrand = new SelectList(_db.Brands.Where(m => (m.status == "1" || m.status == "0")).OrderBy(m => m.brand_name), "brand_id", "brand_name", 0);
            ViewBag.ListGenre = new SelectList(_db.Genres.Where(m => (m.status == "1" || m.status == "0")).OrderBy(m => m.genre_name), "genre_id", "genre_name", 0);
            ViewBag.ListTaxes = new SelectList(_db.Taxes.OrderBy(m => m.taxes_name), "taxes_id", "taxes_name", 0);
            var discount = _db.Discounts.FirstOrDefault(m => m.discount_id == Prodtos.discount_id);
            var slug = SlugGenerator.SlugGenerator.GenerateSlug(Prodtos.slug);
            try
            {
                if (Prodtos.slug == null)
                {
                    product.slug = SlugGenerator.SlugGenerator.GenerateSlug(Prodtos.product_name);
                }
                else
                {
                    var checkslug = _db.Products.Any(m => m.slug == slug);
                    if (checkslug)
                    {
                        product.slug = slug + "-" + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm") + new Random().Next(1, 1000);
                    }
                    else
                    {
                        product.slug = SlugGenerator.SlugGenerator.GenerateSlug(Prodtos.slug);
                    }
                }
                product.image = Prodtos.Image;
                //add data vào table product
                product.brand_id = Prodtos.brand_id;
                product.discount_id = Prodtos.discount_id;
                product.GenreDetail.Genre.genre_id = Prodtos.genre_id;
                product.status = Prodtos.status;
                product.title_seo = Prodtos.seo_title;
                product.view = 0;
                product.specification = Prodtos.specification;
                product.description = Prodtos.description;
                product.buyturn = 0;
                product.create_at = DateTime.Now;
                product.update_at = DateTime.Now;
                product.create_by = User.Identity.GetEmail();
                product.update_by = User.Identity.GetEmail();
                product.tax_id = Prodtos.taxes_id;
                _db.Products.Add(product);

                //add data vào table product_image
                var product_id = product.product_id;
                product_image.product_id = product_id;
                product_image.genre_id = product.GenreDetail.Genre.genre_id;
                product_image.image_1 = Prodtos.image_1;
                product_image.image_2 = Prodtos.image_2;
                product_image.image_3 = Prodtos.image_3;
                product_image.image_4 = Prodtos.image_4;
                product_image.image_5 = Prodtos.image_5;
                product_image.create_at = DateTime.Now;
                product_image.update_at = DateTime.Now;
                product_image.create_by = User.Identity.GetEmail();
                product_image.update_by = User.Identity.GetEmail();
                product_image.status = product.status;
                _db.ProductImages.Add(product_image);
                if (discount.discount_price > product.selling_price)
                {
                    Notification.set_flash("Giảm giá phải nhỏ hơn trị giá sản phẩm", "danger");
                    return View(Prodtos);
                }
                else
                {
                    await _db.SaveChangesAsync();
                }
                Notification.set_flash("Thêm thành công", "success");
                return RedirectToAction("ProductIndex");
            }
            catch
            {
                Notification.set_flash("Thêm sản phẩm không thành công", "danger");
            }
            return View(Prodtos);
        }
        //Thêm nhanh thể loại
        [HttpPost]
        public ActionResult CreateGenres(Genre genre, ProductDTO product_DTOs)
        {
            bool result;
            try
                {
                genre.parent_genre_id = product_DTOs.parent_genre_id;
                genre.genre_name = product_DTOs.genre_name;
                genre.status = "1";
                genre.create_at = DateTime.Now;
                genre.create_by = User.Identity.GetEmail();
                genre.update_at = DateTime.Now;
                genre.update_by = User.Identity.GetEmail();
                genre.genre_image = "/Images/ImagesCollection/no-image-available.png";
                _db.Genres.Add(genre);
                _db.SaveChanges();
                Notification.set_flash("Thêm thành công thể loại: " + genre.genre_name + "", "success");
                result = true;
            }
            catch
            {
                result = false;
                Notification.set_flash("Thêm thể loại không thành công", "danger");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Thêm nhanh VAT
        [HttpPost]
        public ActionResult CreateTaxes(Tax taxes, ProductDTO product_DTOs)
        {
            bool result;
            try
            {
                taxes.taxes_name = product_DTOs.taxes_name;
                taxes.taxes_value = product_DTOs.taxes_value;
                taxes.create_at = DateTime.Now;
                taxes.update_at = DateTime.Now;
                _db.Taxes.Add(taxes);
                _db.SaveChanges();
                Notification.set_flash("Thêm thành công VAT: " + taxes.taxes_name + "", "success");
                result = true;
            }
            catch
            {
                result = false;
                Notification.set_flash("Thêm VAT không thành công", "danger");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //Thêm nhanh giảm giá
        [HttpPost]
        public ActionResult CreateDiscounts(Discount discount, ProductDTO product_DTOs)
        {
            bool result; 
            try
            {
                discount.discount_name = product_DTOs.discount_name;
                discount.create_at = DateTime.Now;
                discount.create_by = User.Identity.GetEmail();
                discount.update_at = DateTime.Now;
                discount.discount_type = 1;
                discount.status = product_DTOs.discount_status;
                discount.discount_price = product_DTOs.discount_price;
                discount.discount_start = product_DTOs.discount_start;
                discount.discount_end = product_DTOs.discount_end;
                discount.quantity = "0";
                discount.discount_global = "0";
                discount.update_by = User.Identity.GetEmail();
                _db.Discounts.Add(discount);
                _db.SaveChanges();
                Notification.set_flash("Thêm thành công CTGG: " + discount.discount_name + "", "success");
                result = true;
            }
            catch
            {
                result = false;
                Notification.set_flash("Thêm mã giảm giá không thành công!", "danger");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Thêm nhanh thương hiệu
        [HttpPost]
        public ActionResult CreateBrands(Brand brand, ProductDTO product_DTOs)
        {
            bool result;
            try
            {
                brand.brand_name = product_DTOs.brand_name;
                brand.create_at = DateTime.Now;
                brand.create_by = User.Identity.GetEmail();
                brand.brand_image = "/Images/ImagesCollection/no-image-available.png";
                brand.update_at = DateTime.Now;
                brand.status = "1";
                brand.update_by = User.Identity.GetEmail();
                _db.Brands.Add(brand);
                _db.SaveChanges();
                Notification.set_flash("Thêm thành công thương hiệu: " + brand.brand_name + "", "success");
                result = true;
            }
            catch
            {
                result = false;
                Notification.set_flash("Thêm thương hiệu không thành công", "danger");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //View chỉnh sửa thông tin sản phẩm
        public ActionResult ProductEdit(int id, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (String.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null && Request.UrlReferrer.ToString().Length > 0)
                {
                    return RedirectToAction("ProductEdit", new { returnUrl = Request.UrlReferrer.ToString() });
                }
                var inventory = _db.Inventories.FirstOrDefault(x => x.product_id == id);
                if (inventory == null)
                {
                    Notification.set_flash("Không tồn tại sản phẩm trong kho", "warning");
                    return RedirectToAction("ProductIndex");
                }
                var product = _db.Products.FirstOrDefault(x => x.product_id == id);
                if ((User.Identity.Permiss_Modify() == true) && (product.status == "1" || product.status == "0"))
                {
                    ProductDTO ProductDTO = new ProductDTO
                    {
                        price = product.selling_price,
                        taxes_id = (int)product.tax_id,
                        genre_id = product.GenreDetail.Genre.genre_id,
                        product_id = product.product_id,
                        slug = product.slug,
                        brand_id = product.brand_id,
                        discount_id = product.discount_id,
                        product_name = product.product_name,
                        count_Banner_detail = product.BannerDetails.Count,
                        seo_title = product.title_seo,
                        Image = product.image,
                        quantity = inventory.quantity_on_hand,
                        status = product.status,
                        specification = product.specification,
                        description = product.description
                    };
                    ViewBag.countfeedback = _db.Feedbacks.Count(a => a.status == "2" && a.product_id == id);
                    ViewBag.ListDiscount = new SelectList(_db.Discounts.Where(m => m.status == "1" && m.discount_type == 1).OrderByDescending(m => m.discount_id), "discount_id", "discount_name", 0);
                    ViewBag.ListBrand = new SelectList(_db.Brands.Where(m => m.status == "1" || m.status == "0").OrderBy(m => m.brand_name), "brand_id", "brand_name", 0);
                    ViewBag.ListGenre = new SelectList(_db.Genres.Where(m => m.status == "1" || m.status == "0").OrderBy(m => m.genre_name), "genre_id", "genre_name", 0);
                    ViewBag.ListTaxes = new SelectList(_db.Taxes.OrderBy(m => m.taxes_name), "taxes_id", "taxes_name", 0);
                    if (product == null)
                    {
                        Notification.set_flash("Không tồn tại: " + product.product_name + "", "warning");
                        return RedirectToAction("ProductIndex");
                    }
                    return View(ProductDTO);
                }
                else
                {
                    Notification.set_flash("Bạn không có quyền sử dụng chức năng này", "danger");
                    return RedirectToAction("ProductIndex");
                }
            }
            else
            {
                return Redirect("~/Account/SignIn");
            }
        }
        //Code xử lý thông tin sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ProductEdit(Product products,ProductDTO ProductDTO, string returnUrl, int id)
        {
            ViewBag.ListDiscount = new SelectList(_db.Discounts.Where(m => m.status == "1" && m.discount_type == 1).OrderByDescending(m => m.discount_id), "discount_id", "discount_name", 0);
            ViewBag.ListBrand = new SelectList(_db.Brands.Where(m => m.status == "1" || m.status == "0").OrderBy(m => m.brand_name), "brand_id", "brand_name", 0);
            ViewBag.ListGenre = new SelectList(_db.Genres.Where(m => m.status == "1" || m.status == "0").OrderBy(m => m.genre_name), "genre_id", "genre_name", 0);
            ViewBag.ListTaxes = new SelectList(_db.Taxes.OrderBy(m => m.taxes_name), "taxes_id", "taxes_name", 0);
            var product = _db.Products.SingleOrDefault(x => x.product_id == id);
            var discount = _db.Discounts.FirstOrDefault(m => m.discount_id == ProductDTO.discount_id);
            var inventory = _db.Inventories.FirstOrDefault(x => x.product_id == id);
            if (inventory == null)
            {
                Notification.set_flash("Không tồn tại sản phẩm trong kho", "warning");
                return RedirectToAction("ProductIndex");
            }
            try
            {
                product.product_name = ProductDTO.product_name;
                inventory.quantity_on_hand = ProductDTO.quantity;
                product.description = ProductDTO.description;
                product.tax_id = ProductDTO.taxes_id;
                product.status = ProductDTO.status;
                product.title_seo = ProductDTO.seo_title;
                product.selling_price = ProductDTO.price;
                product.specification = ProductDTO.specification;
                product.brand_id = ProductDTO.brand_id;
                product.image = ProductDTO.Image;
                product.discount_id = ProductDTO.discount_id;
                product.tax_id = ProductDTO.taxes_id;
                product.update_at = DateTime.Now;
                product.update_by = User.Identity.GetEmail();
                if (discount.discount_price > product.selling_price)
                {
                    Notification.set_flash("Giảm giá phải nhỏ hơn giá sản phẩm", "danger");
                    return View(ProductDTO);
                }
                else
                {
                    _db.SaveChanges();
                    Notification.set_flash("Cập nhật thành công id" + id + "", "success");
                }

                if (!String.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("ProductIndex");
            }
            catch
            {
                Notification.set_flash("Lỗi", "danger");
            }
            return View(ProductDTO);
        }
        //Xóa sản phẩm
        [HttpPost]
        public JsonResult ProductDelete(int id)
        {
            string result;
            if (User.Identity.Permiss_Delete() == true)
            {
                var product = _db.Products.FirstOrDefault(m => m.product_id == id);
                var newsProducts = _db.NewProducts.FirstOrDefault(a => a.product_id == id);
                var product_image = _db.ProductImages.FirstOrDefault(m => m.product_id == id);
                if (product.OrderDetails.Count > 0)
                {
                    result = "ExitOrder";
                }
                else if (product.BannerDetails.Count > 0)
                {
                    result = "ExitBanner";
                }
                else if (product.Feedbacks.Count > 0)
                {
                    result = "ExitFeedback";
                }
                else
                {
                    if (product.NewProducts.Count > 0)
                    {
                        _db.NewProducts.Remove(newsProducts);
                        _db.SaveChanges();
                    }
                    //xóa product image trước vì dính có id của product_id trong product_image
                    if (product_image.product_id == id)
                    {
                        _db.ProductImages.Remove(product_image);
                        _db.SaveChanges();
                    }
                    _db.Products.Remove(product);
                    _db.SaveChanges();
                    Notification.set_flash("Xóa id"+id+ " thành công", "success");
                    result = "Success";
                }
            }
            else
            {
                result = "false";
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //thay đổi trạng thái sản phẩm
        [HttpPost]
        public JsonResult ChangeStatus(int id, int state = 0)
        {
            bool result;
            if (User.Identity.Permiss_Update() == true || User.Identity.Permiss_Modify() == true)
            {
                Product product = _db.Products.FirstOrDefault(m => m.product_id == id);
                int title = product.product_id;
                product.status = state.ToString();
                product.update_at = DateTime.Now;
                product.update_by = User.Identity.GetEmail();
                _db.SaveChanges();
                result = true;
                Notification.set_flash("Đổi trạng thái 'id " + id + "' thành công", "success");
            }
            else
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}