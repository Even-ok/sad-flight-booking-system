function load() {
    $.ajax({
        url: "/AccountManager/CheckCookie",
        async: false,
        type: 'post',
        contentType: "application/json",
        dataType: "json",
        traditional: true,
        success: function (data) {
            console.log(data);
            var data = eval("(" + data + ")");  
            if (data.UserID != null) {
                var userid = document.getElementById("id");
                userid.value = data.UserID;
                var psw = document.getElementById("password");
                psw.value = data.password;

            }
        }
    })
}

function login() {

    var u = $("input[name=username]");
    var p = $("input[name=password]");
    $("#submit").live('click', function () {
        if (u.val() == '' || p.val() == '') {
            alert("用户名或密码不能为空！");
            location.reload();
            return false;
        }
        else {
            var reg = /^[0-9A-Za-z]+$/;
            if (!reg.exec(u.val())) {
                alert("用户名中有非法字符！");
                location.reload();
                return false;
            }
            var id = u.val();
            var psw = p.val();
            var state = $("#markcheckbox1").prop('checked');
            var FLAG = "2";
            var queryJson = [];
            var row = {};
            row.name = id;
            row.password = psw;
            row.state = $("#markcheckbox1").prop('checked');
            queryJson.push(row);
            console.log(queryJson[0]);

            $.ajax({
                url: "/AccountManager/UserLogin",
                async: false,
                type: 'post',
                contentType: "application/json",
                data: JSON.stringify(queryJson[0]),
                dataType: "json",
                traditional: true,
                success: function (data) {
                    console.log(data);
                    var data = eval("(" + data + ")");  
                    FLAG = data.LoginFlag;
                    if (FLAG === "1") {
                        alert("该用户名不存在！");
                        return false;
                    }
                    //console.log(FLAG);
                    else if (FLAG === "2") {

                        alert("密码输入错误！");
                        return false;
                    }

                    //data.LoginFlag
                    //0->成功
                    //1->用户名不存在
                    //2->密码不匹配
                },
            });
            if (FLAG == "0") {
                alert("登陆成功！");
                window.location.replace("../Flight/Index");
            }
            else
                alert("登陆失败！");
        }
    });
    window.onload = function () {
        $(".connect p").eq(0).animate({ "left": "0%" }, 600);
        $(".connect p").eq(1).animate({ "left": "0%" }, 400);
    }
    function is_hide() { $(".alert").animate({ "top": "-40%" }, 300) }
    function is_show() { $(".alert").show().animate({ "top": "45%" }, 300) }
}

function checkID() {
    var usern = /^[a-zA-Z0-9_]{1,}$/; 
    if (!usern.test($("#userid").val())) {


        alert("用户名只能由字母数字下划线组成");

        $("#userid").value = '';

        $("#userid").focus()(function () {
            this.setAttribute("background-color", "#f0f0f0");
            return false;
        })

    }
    if ($("#userid").val().length == 0) {
        alert("账号不能为空！");
        $("#userid").focus();
        return false;
    }
    if ($("#userid").val().length < 8) {
        alert("账号长度必须大于等于8个字符！");
        $("#userid").focus();
        return false;
    }
    return true;
}

function checkPassword() {
    if ($("#password").val().length == 0 || $("#ensurePassword").val().length == 0) {
        alert("密码输入不能为空");
        return false;
    }
    if ($("#password").val() != $("#ensurePassword").val()) {
        alert("两次密码输入不一致，请重新输入！");
        $("#password").focus();
        return false;
    }
    if ($("#password").val().length < 8 || $("#password").val().length > 20) {
        alert("密码长度不符合要求，请重新输入！");
        $("#password").focus();
        return false;
    }
    return true;
}

function checkUserName() {
    var reg = /^[\u4e00-\u9fa5]{3,15}$/
    var realName = $("#username").val();
    if (realName == null || realName == "") {
        $("#username").css("color", "red");
        $("#username").text("真实姓名不能为空");
        return false;

    } else if (!reg.test(realName)) {
        $("#username").css("color", "red");
        $("#username").text("真实姓名是3-15个汉字");
        return false;

    }
        return true;
    }

function checkPhoneNumber() {
    var reg = /^(13[0-9]|14[5|7]|15[0|1|2|3|4|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$/
    var phone = $("#phoneNumber").val();
    if (phone == null || phone == "") {
        $("#phoneNumber").focus();
        alert("手机号不能为空！")
        return false;
    }
     else if (!reg.test(phone)) {
        $("#phoneNumber").focus();
        alert("手机号不合法！")
        return false;

     } 
        return true;
}

function checkIdCard() {
    var reg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/
    var idCard = $("#userIdcard").val();
    if (idCard == null || idCard == "") {
        $("#userIdcard").focus();
        alert("身份证号不能为空！")
        return false;

    } else if (!reg.test(idCard)) {
        $("#userIdcard").focus();
        alert("身份证号不合法！")
        return false;

    } else 
        return true;
}

function register() {

    var id = $("#userid").val().replace(/\s/gi, '');
    var psw = $("#password").val().replace(/\s/gi, '');
    var us_name = $("#username").val().replace(/\s/gi, '');
    var phone_number = $("#phoneNumber").val().replace(/\s/gi, '');
    var email = $("#email").val().replace(/\s/gi, '');
    var userIdcard = $("#userIdcard").val().replace(/\s/gi, '');
    if (checkID() && checkPassword() && checkUserName() && checkPhoneNumber() && checkIdCard()) {


        var FLAG = "2";
        var queryJson = [];
        var row = {};
        row.UserID = id;
        row.UserName = us_name;
        row.password = psw;
        row.PhoneNumber = phone_number;
        row.userEmail = email;
        row.userIdcard = userIdcard;
        queryJson.push(row);
        console.log(queryJson[0]);

        $.ajax({
            url: "/AccountManager/UserRegister",
            async: false,
            type: 'post',
            contentType: "application/json",
            data: JSON.stringify(queryJson[0]),
            dataType: "json",
            traditional: true,
            success: function (data) {
                console.log(data);
                var data = eval("(" + data + ")");  
                FLAG = data.RegisterFlag;
                if (FLAG === "0") {

                    alert("该用户账号已存在！");
                    return false;
                }
                console.log(FLAG);

                //data:RegisterFlag
                //0->用户已存在
                //1->注册成功
            },
            error: function (message) {
                alert("请求查询数据失败！");
            }
        });
        if (FLAG == "1")
        {
            alert("注册成功！");
            window.location.replace("mylogin.html")
        }
        else
            alert("注册失败！");
    }
    else return false;
}