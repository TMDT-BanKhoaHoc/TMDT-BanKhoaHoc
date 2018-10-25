$('#themhang').click(function () {
    var makhoahoc = this.dataset.themhang;
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.statusText === 'OK' && this.readyState === 4) {
            var code = parseInt(this.responseText);
            if (code >= 0) {
                $('#soluong').text(code);
                alert("Đã thêm vào giỏ hàng");
            } else if (code === -1) {
                alert('Khóa học đã có trong giỏ hàng');
            } else if (code === -2) {
                alert('Bạn đã mua khóa học này');
            } else if (code === -3) {
                alert('Khóa học này không tồn tại');
            } else if (code === -4) {
                alert('Bạn chưa đăng nhập');
            }
        }
    };
    xhttp.open("GET", "/HocVien/ThemGioHang?khoahoc=" + makhoahoc);
    xhttp.send();
});