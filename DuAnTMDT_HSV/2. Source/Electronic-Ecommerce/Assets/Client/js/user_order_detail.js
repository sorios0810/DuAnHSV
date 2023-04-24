$('#open_cancel_order').click(function(){
    $('#cancel_my_order').modal('show')
})
$('#btn__back').click(function () {
    $('#cancel_my_order').modal('hide')
})
$("#btn_cancel__order").click(function () {
    var data = $("#formcancel_order").serialize();
    $("#cancel_my_order").modal("hide")
    $.ajax({
        type: "post",
        url: "/Account/CancelOrder",
        data: data,
        success: function (result) {
            if (result == true) {
                $('.order_cancel').text('Đơn hàng đã bị hủy');
                $('.order_cancel').addClass('alert-danger');
                $('.order_cancel').removeClass('alert-warning');
                $(".remove_order_cancel").remove();
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top',
                    showConfirmButton: false,
                    timer: 2500,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })
                Toast.fire({
                    icon: 'success',
                    title: 'Huỷ đơn hàng thành công'
                })
            }
            else {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top',
                    showConfirmButton: false,
                    timer: 2500,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })
                Toast.fire({
                    icon: 'error',
                    title: 'Lỗi, đơn hàng không thể hủy'
                })
            }
        },
        error: function () {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top',
                showConfirmButton: false,
                timer: 2500,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })
            Toast.fire({
                icon: 'error',
                title: 'Gửi thất bại'
            })
        }
    })
})
