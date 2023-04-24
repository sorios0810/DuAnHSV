using System.Web;
using System.Web.Optimization;

namespace Electronic_Ecommerce
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //--------------------------------------------------------JS-------------------------
            //custom search: gợi ý khi gõ chữ vào thanh searchcustom search: gợi ý khi gõ chữ vào thanh search
            bundles.Add(new ScriptBundle("~/bundles/customsearch").Include("~/Assets/Client/js/search.js"));
            //Popup first load page
            //bundles.Add(new ScriptBundle("~/bundles/firstloadpage").Include("~/Assets/Client/js/jquery.firstVisitPopup.js", 
            //    "~/Assets/Client/js/popup_first_loadpage.js"));
            //check ràng buộc đăng nhập, đăng ký,...
            bundles.Add(new ScriptBundle("~/bundles/checkvalidaccount").Include("~/Assets/Client/js/checkuseraccount.js"));
            //Lưu trữ mật khẩu, giỏ hàng,...
            bundles.Add(new ScriptBundle("~/bundles/cookie").Include("~/Assets/Client/js/cookie.js"));
            //dùng liên quan đến add sản phẩm vào giỏ hàng
            bundles.Add(new ScriptBundle("~/bundles/common").Include("~/Assets/Client/js/common.js"));
            //hiển thị hình ảnh dưới dạng slide
            bundles.Add(new ScriptBundle("~/bundles/carouselmin").Include("~/Assets/Client/js/owl.carousel.min.js"));
            //custom thông báo
            bundles.Add(new ScriptBundle("~/bundles/customtoastr").Include("~/Assets/Client/js/custom_toastr.js"));
            //hiển thị tag giảm giá. ở từng sản phẩm có chương trình giảm giá
            bundles.Add(new ScriptBundle("~/bundles/discountstag").Include("~/Assets/Client/js/discount.js"));
            //
            bundles.Add(new ScriptBundle("~/bundles/bootstrapmin").Include("~/Assets/Client/js/bootstrap.min.js"));
            //
            bundles.Add(new ScriptBundle("~/bundles/meanmenu").Include("~/Assets/Client/js/jquery.meanmenu.min.js"));
            //
            bundles.Add(new ScriptBundle("~/bundles/slick").Include("~/Assets/Client/js/slick.js"));
            //
            bundles.Add(new ScriptBundle("~/bundles/wow").Include("~/Assets/Client/js/wow.min.js"));
            //
            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Assets/Client/js/jquery-ui-1.12.1.js"));
            ////
            //bundles.Add(new ScriptBundle("~/bundles/jquerymin").Include("~/Assets/Client/js/jquery-3.6.0.min.js"));
            //hiện thông báo có thể sử dụng 1 trong 3::toastr - sweet alert 2 - jquery toast
            bundles.Add(new ScriptBundle("~/bundles/toast").Include("~/Assets/Client/js/sweetalert2.min.js",
                "~/Assets/Client/js/jquery.toast.js", "~/Assets/Client/js/toastr.min.js", "~/Assets/Client/js/bootbox.js"));
            //
            bundles.Add(new ScriptBundle("~/bundles/waypoint").Include("~/Assets/Client/js/waypoints.min.js"));
            //
            bundles.Add(new ScriptBundle("~/bundles/scrollup").Include("~/Assets/Client/js/scrollUp.min.js"));
            //ràng buộc form nhập
            bundles.Add(new ScriptBundle("~/bundles/jqueryinputmask").Include("~/Assets/Client/js/jquery.inputmask.js", 
                "~/Assets/Client/js/inputmask.js"));
            //xem ảnh full màn, dùng trong product detail khi bấm vào từng ảnh sẽ hiển thị ảnh fullscreen
            bundles.Add(new ScriptBundle("~/bundles/fslightbox").Include("~/Assets/Client/js/fslightbox.js"));

            //------------------------------------------CSS-------------------------------------------------------------------------------------------------
            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include("~/Assets/Client/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundles/material").Include("~/Assets/Client/css/material-design-iconic-font.min.css"));

            bundles.Add(new StyleBundle("~/bundles/venobox").Include("~/Assets/Client/css/venobox.css"));

            bundles.Add(new StyleBundle("~/bundles/slick").Include("~/Assets/Client/css/slick.css"));

            bundles.Add(new StyleBundle("~/bundles/animate").Include("~/Assets/Client/css/animate.css"));

            bundles.Add(new StyleBundle("~/bundles/meanmenu").Include("~/Assets/Client/css/meanmenu.css"));

            bundles.Add(new StyleBundle("~/bundles/jquery-ui").Include("~/Assets/Client/css/plugins/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/bundles/jquery-ui-min").Include("~/Assets/Client/css/jquery-ui.min.css"));

            bundles.Add(new StyleBundle("~/bundles/responsiveweb").Include("~/Assets/Client/css/responsive.css"));

            bundles.Add(new StyleBundle("~/bundles/colorsweb").Include("~/Assets/Client/css/colors1.css"));

            bundles.Add(new StyleBundle("~/bundles/toasts").Include("~/Assets/Client/css/toastr.min.css", "~/Assets/Client/css/plugins/jquery.toast.css"));

            //bundles.Add(new StyleBundle("~/bundles/owl").Include("~/Assets/Client/css/owl.carousel.min.css"));

            bundles.Add(new StyleBundle("~/bundles/helper").Include("~/Assets/Client/css/helper.css"));

            bundles.Add(new StyleBundle("~/bundles/font-awesome").Include("~/Assets/Client/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundles/font-awesome-star").Include("~/Assets/Client/css/fontawesome-stars.css"));

        }
    }
}
