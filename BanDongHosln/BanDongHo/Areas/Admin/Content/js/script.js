
// javascript function show detail of a product
function DetailPopup(header, masp) {
    // Get the popup
    var popup = document.getElementById('myDetailPopup');
    // Get the <span> element that closes the popup
    var span = document.getElementById("span-close-detail");

    //var btn = ducument.getElementById("btn-cancel-detail")

    // Get the <div> header
    var a = document.getElementById('hd-popup-content-detail');
    // Get the <span> content

    a.innerText = header;

    popup.style.display = "block";

    $.ajax(
    {
        type: 'post',
        url: '/Admin/Product/Detail?masp=' + masp,
        success: function (result) {
            $('#DetailPopup').html(result);
        },
        error: function () {
            alert('load fail');
        }
    });

    span.onclick = function () {
        popup.style.display = "none";
    }
}

// javascript function for remove a product
function DeletePopup(header, masp) {
    // Get the popup
    var popup = document.getElementById('myDeletePopup');
    // Get the <span> element that closes the popup
    var span = document.getElementById("span-close-delete");

    var btn = document.getElementById('btn-cancel-delete');
    // Get the <div> header
    var a = document.getElementById('hd-popup-content-delete');
    // Get the <span> content
    var b = document.getElementById('bd-popup-content-delete');

    a.innerText = header;
    b.innerText = 'Bạn có muốn xóa sản phẩm này?';

    popup.style.display = "block";
    // When the user clicks on <span> (x), close the popup
    span.onclick = function () {
        popup.style.display = "none";
    }
    btn.onclick = function () {
        popup.style.display = "none";
    }

    function handler() {
        $.ajax(
        {
            type: 'POST',
            url: '/Admin/Product/deleteProduct?masp=' + masp,
            dataType: 'json',      
            success: function (data) {
                if (data.result == true) {
                    alert("remove successful");
                    getListProduct(1);                 
                } else {
                    alert("remove not succsessful");
                }

            },
            error: function () {
                alert("remove fail");
            }
        });
        popup.style.display = "none";
        $('#btn-ok-delete').unbind('click', handler);
    }
    $('#btn-ok-delete').bind('click', handler);
}

// javascript function for edit infor for a product
function UpdatePopup(element, header, masp) {
    // Get the popup
    var popup = document.getElementById('myPopup');
    // Get the <span> element that closes the popup
    var span = document.getElementById("span-close-popup");

    // Get the <div> header
    var a = document.getElementById('hd-popup-content');
    // Get the <span> content

    a.innerText = header;

    popup.style.display = "block";

    $.ajax(
    {
        type: 'POST',
        url: '/Admin/Product/Update?masp=' + masp,
        success: function (result) {
            $('#UserPopup').html(result);
            $('#btn-cancel-update').click(function () {
                popup.style.display = "none";
            });
            $('#btn-ok-update').bind('click', handler);
        },
        error: function () {
            alert('load fail');
        }
    });


    span.onclick = function () {
        popup.style.display = "none";
    }

    function handler() {
      
        var sanpham = {
            MASP: masp,
            TENSP: $('#TENSP').val(),
            SOLUONG: $('#SOLUONG').val(),
            MOTA : $('#MOTA').val(),
            MATH: $('#MATH').val(),
            DANHGIA: $('#DANHGIA').val(),
            MALOAISP: $('#MALOAISP').val(),
            DONGIA: parseFloat($('#DONGIA').val()),
            HINHLON: $('#HINHLON').val(),
            HINHNHO: $('#HINHNHO').val()
        };

        
        $.ajax(
        {
            type: 'POST',
            url: '/Admin/Product/updateProduct',
            data: sanpham,
            processData: true,
            success: function (result) {
                alert("update successful");
                var a = $(element).parent().parent().find('td');
                $(a[1]).text(result.TENSP);
                $(a[2]).text(result.SOLUONG);
                $(a[3]).text(result.TENTH);
                $(a[4]).text(result.TENLOAISP);
                $(a[5]).text(result.DONGIA.toFixed().replace(/(\d)(?=(\d{3})+(,|$))/g, '$1,'));
            },
            error: function () {
                alert("update fail");
            }
        });
        popup.style.display = "none";
        $('#btn-ok-update').unbind('click', handler);
    }

}

// javascript function for add a new product
function CreatePopup(header) {
    // Get the popup
    var popup = document.getElementById('myPopup');
    // Get the <span> element that closes the popup
    var span = document.getElementById("span-close-popup");
    // Get the <div> header
    var a = document.getElementById('hd-popup-content');
    // Get the <span> content

    a.innerText = header;

    popup.style.display = "block";

    $.ajax(
    {
        type: 'POST',
        url: '/Admin/Product/Create',
        cache: false,
        success: function (result) {
            $('#UserPopup').html(result);
            $('#btn-cancel-create').click(function () {
                popup.style.display = "none";
            });
            $('#formCreateProduct').submit(function (e) {
                Handler(e);
            });
            //$('#btn-ok-create').bind('click', Handler);

        },
        error: function () {
            alert('load fail');
        }
    });

    span.onclick = function () {
        popup.style.display = "none";
    }

    function Handler(e) {
    
        var sanpham = {
            TENSP: $('#TENSP').val(),
            SOLUONG: $('#SOLUONG').val(),
            MOTA: $('#MOTA').val(),
            MATH: $('#MATH').val(),
            DANHGIA: $('#DANHGIA').val(),
            MALOAISP: $('#MALOAISP').val(),
            DONGIA: parseFloat($('#DONGIA').val()),
            HINHLON: getFileName($('#HINHLON').val()),
            HINHNHO: getFileName($('#HINHLON').val()).split('.')[0]
        };

        $.ajax(
        {
            type: 'POST',
            url: '/Admin/Product/addProduct',
            data: sanpham,
            //dataType: 'json',
            cache: false,
            processData: true,
            success: function (data) {
                //if (data.result == true) {
                //    alert("create successful");
                //    getListProduct(1);
                //} else {
                //    alert("create not successful!");
                //}
            },
            error: function () {
                //alert("create fail");
            }
        });
        e.preventDefault();
       // popup.style.display = "none";
        //$('#formCreateProduct').unbind('submit', Handler(event));
        //$('#btn-ok-create').unbind('click', Handler);
    }
    
    
}

function getFileName(filePath) {
    return filePath.substr(filePath.lastIndexOf('\\') + 1);
}

function getListProduct(page) {
    $.ajax(
        {
            type: 'POST',
            url: '/Admin/Product/Product?page=' + page,
            success: function (result) {
                $('#product-list').html(result);
            },
            error: function () {
                alert('load product fail');
            }
        });
}

function isEmpty(str) {
    return !str.replace(/^\s+/g, '').length;
}

function isPhoneNumber(phone) {
    var re = /^[0][0-9]{9,10}$/;
    return re.test(phone);
}

function isIdentity(cmnd) {
    var re = /^[0-9]{9}$/;
    var rec = /^[0-9]{12}$/;

    if (re.test(cmnd)) {
        return true;
    } else {
        if (rec.test(cmnd)) {
            return true;
        }
        return false;
    }
}

function isDate(date) {
    if (!/^\d{4}\-\d{1,2}\-\d{1,2}$/.test(date))
        return false;

    // Parse the date parts to integers
    var parts = dateString.split("-");
    var day = parseInt(parts[2], 10);
    var month = parseInt(parts[1], 10);
    var year = parseInt(parts[0], 10);

    // Check the ranges of month and year
    if (year < 1000 || year > 3000 || month == 0 || month > 12)
        return false;

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Adjust for leap years
    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    // Check the range of the day
    return day > 0 && day <= monthLength[month - 1];
}









