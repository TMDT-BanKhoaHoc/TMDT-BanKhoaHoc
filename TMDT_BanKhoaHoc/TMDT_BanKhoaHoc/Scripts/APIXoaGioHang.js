$('.btn-Xoa').click(function () {
    var makhoahoc = this.dataset.xoagiohang;
    var btn = this;
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.status === 200 && this.readyState === 4) {
            var code = parseInt(this.responseText);
            if (code >= 0) {
                btn.parentElement.parentElement.parentElement.removeChild(btn.parentElement.parentElement);
                if (code === 0) {
                    $('#giohang').html('<div>\
                            <p style = "font-size: large">\
                                Giỏ hàng trống\
                            </p >\
                        </div >');
                }
                $('#soluong').text(code);
                alert('Đã xóa khóa học khỏi giỏ hàng');
            } else if (code === -1) {
                alert('Khóa học không tồn tại trong giỏ hàng');
            } else if (code === -2) {
                alert('Bạn chưa đăng nhập');
            }
        }
    };
    xhttp.ontimeout = function () {
        alert('Kết nối không thành công');
    };
    xhttp.open("POST", "/HocVien/XoaGioHang?makhoahoc=" + makhoahoc);
    xhttp.send();
});