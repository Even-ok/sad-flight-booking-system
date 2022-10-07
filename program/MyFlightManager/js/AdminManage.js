function adminlogin() {

    var u = $("input[name=username]");
    var p = $("input[name=password]");
    $("#submit").live('click', function () {
        if (u.val() == '' || p.val() == '') {
            $("#ts").html("用户名或密码不能为空~");
            is_show();
            return false;
        }
        else {
            var reg = /^[0-9A-Za-z]+$/;
            if (!reg.exec(u.val())) {
                $("#ts").html("用户名错误");
                is_show();
                return false;
            }

            var id = u.val();
            var psw = p.val();
            var FLAG = "2";
            var queryJson = [];
            var row = {};
            row.name = id;
            row.password = psw;
            queryJson.push(row);
            console.log(queryJson[0]);
            $.ajax({
                url: "/AccountManager/AdminLogin1",
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

                        alert("该管理员账号不存在！");
                        return false;
                    }
                    //console.log(FLAG);
                    else if (FLAG === "2") {

                        alert("密码输入错误！");
                        return false;
                    }
                    else if (FLAG == "3") {
                        alert("该管理员正在登陆！")
                        return false;
                    }

                    //data.LoginFlag
                    //0->成功
                    //1->用户名不存在
                    //2->密码不匹配
                     //3->管理员正在使用
                },
            });
            if (FLAG == "0") {
                alert("登陆成功！");
                //注意这个地方要跳转到管理员界面！
                window.location.replace("../Feedback/displayFeedback");
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