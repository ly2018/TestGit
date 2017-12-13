function MenuHandle(obj) {
    e.preventDefault();
    var submenu = $(obj).siblings('ul');
    var li = $(obj).parents('li');
    var submenus = $('#sidebar li.submenu ul');
    var submenus_parents = $('#sidebar li.submenu');
    debugger
    if (li.hasClass('open')) {
        if (($(window).width() > 768) || ($(window).width() < 479)) {

            submenu.slideUp();
        } else {
            submenu.fadeOut(250);
        }
        li.removeClass('open');
    } else {
        if (($(window).width() > 768) || ($(window).width() < 479)) {
            submenus.slideUp();
            submenu.slideDown();
        } else {
            submenus.fadeOut(250);
            submenu.fadeIn(250);
        }
        submenus_parents.removeClass('open');
        li.addClass('open');
    }
}
$(function () {
    //返回首页地址
    $("#breadcrumb a.tip-bottom").eq(0).attr('href', '/home/Index');
    //$("img").bind("click", function () {
    //    var src=$(this).attr("src");
    //    if (src.indexOf("_small.jpeg") > -1) {
    //        window.open(src.replace(/_small.jpeg/, ""), "_blank", false);
    //    }
    //});
    //$("img").bind("error", function () {
    //    var src = $(this).attr("src");
    //    if (src.indexOf("_small.jpeg") > -1) {
    //        $(this).attr("src", src.replace(/_small.jpeg/, ""));
    //    }
    //});

    //初始化页面菜单
    //if ($("#content").size() > 0) {
    //    $.get("/Ruiec.Admin/Menu/GetAllMenu?X=" + Math.random(), function (data) {
    //        var HeaderMenu = data.HeaderMenu.rolelist.Permissions,
    //            HeaderMenuHTML = '',
    //            LeftMenuHTML = '',
    //            LeftMenu = data.LeftMenu.rolelist.Permissions;
    //        //实例化头部菜单
    //        if (data.HeaderMenu.rolelist[0].Permissions.length > 0) {
    //            for (var i = 0; i < data.HeaderMenu.rolelist[0].Permissions.length; i++) {
    //                if (data.HeaderMenu.rolelist[0].Permissions[i].Description == "1") {
    //                    if (data.HeaderMenu.rolelist[0].Permissions[i].Name.indexOf("欢迎您") > -1) {
    //                        data.HeaderMenu.rolelist[0].Permissions[i].Name += data.UserName + " ";
    //                    }
    //                    HeaderMenuHTML += G.GetTopMenuHtml(data.HeaderMenu.rolelist[0].Permissions[i], data.HeaderMenu.rolelist[0].Permissions);
    //                }
    //            }
    //        }
    //        //实例化左侧菜单
    //        if (data.LeftMenu.rolelist[0].Permissions.length > 0) {
    //            for (var i = 0; i < data.LeftMenu.rolelist[0].Permissions.length; i++) {
    //                if (data.LeftMenu.rolelist[0].Permissions[i].Description == "1") {
    //                    LeftMenuHTML += G.GetLeftMenuHtml(data.LeftMenu.rolelist[0].Permissions[i]);
    //                }
    //            }
    //        }
    //        $("#top-nav-sys").html(HeaderMenuHTML);
    //        $("#top-sidebar-sys").html(LeftMenuHTML);
    //    })
    //}

    //初始化权限按钮显示隐藏
    //$(".Ruiec-Permission-Btn").each(function () {
    //    if ($(this).hasClass("Ruiec-Permission-Btn") && $(this).attr("datakeys") != "") {
    //        var obj = $(this);
    //        $.post("/Ruiec.Admin/Menu/IsDisplayButton","datakeys=" + $(this).attr("datakeys"), function (x) {
    //            if (x.ret == "0000") {
    //                $(obj).removeClass("Ruiec-Permission-Btn");
    //            }
    //        });
    //    }
    //});

    //返回上一页
    $(".goLastPage").bind("click", function () {
        history.back(-1);
    });
    //单个删除
    $(".singleDelete").bind("click", function () {
        var url = $(this).attr("url");
        if (url == "") {
            alert("请求地址不能为空！");
            return;
        }
        G.SkipSqlInject(url);

        var arrId = [];
        var arrckb = $(this).parent("div").parent("td").parent("tr").eq(0).find("input[type='checkbox']").attr("id");
        arrId.push(arrckb);
        G.CommonDelete(arrId, url);
    });
    //编辑跳转
    $(".singleEdit").bind("click", function () {
        var url = $(this).attr("url");
        if (url == "") {
            alert("请求地址不能为空！");
            return;
        }
        G.SkipSqlInject(url);

        var arrckb = $(this).parent("div").parent("td").parent("tr").eq(0).find("input[type='checkbox']").attr("id");
        G.RedirectToUrl(url + "?ID=" + arrckb);
    });

    //批量删除
    $(".commonDelete").bind("click", function () {
        var url = $(this).attr("url");
        if (url == "") {
            alert("请求地址不能为空！");
            return;
        }
        G.SkipSqlInject(url);
        var arrId = [];
        var arrckb = $("input[type='checkbox']");
        for (var i = 0; i < arrckb.length; i++) {
            if ($(arrckb[i]).attr("id") != '' && $(arrckb[i]).parent("span").eq(0).attr("class") == "checked" && $(arrckb[i]).attr("id") != "title-table-checkbox") {
                arrId.push($(arrckb[i]).attr("id"));
            }
        }
        G.CommonDelete(arrId, url);
    });
});

var G = {
    ChangeUrlParam: function (url, name, value) {
        var newUrl = "";
        var reg = new RegExp("(^|)" + name + "=([^&]*)(|$)");
        var tmp = name + "=" + value;
        if (url.match(reg) != null) {
            newUrl = url.replace(eval(reg), tmp);
        }
        else {
            if (url.match("[\?]")) {
                newUrl = url + "&" + tmp;
            }
            else {
                newUrl = url + "?" + tmp;
            }
        }
        return newUrl;
    },
    IncreaseEditorHeight: function (id) {
        var h = parseInt($("#" + id).css('height'), 10);
        $("#" + id).css('height', (h + 200) + "px");
    },
    SkipSqlInject: function (key) {
        //key = key.toLowerCase();
        var re = /^\?(.*)(select%20|insert%20|delete%20from%20|count\(|drop%20table|update%20truncate%20|asc\(|mid\(|char\(|xp_cmdshell|exec%20master|net%20localgroup%20administrators|\"|:|net%20user|\|%20or%20)(.*)$/gi;
        var e = re.test(key);
        if (e) {
            alert("关键字中含有非法字符～");
            return;
        }
    },
    DecreaseEditorHeight: function (id) {
        var h = parseInt($("#" + id).css('height'), 10);
        var hh = h - 200;
        if (hh < 136) {
            hh = 136;
        }
        $("#" + id).css('height', hh);
    },
    //图片上传 （VIC添加）
    ImgUpload: function (tag, toid, ImgPathEnum, IsFullPath) {
        G.SkipSqlInject(tag);
        G.SkipSqlInject(toid);
        G.SkipSqlInject(ImgPathEnum);
        G.SkipSqlInject(IsFullPath);
        var files = document.getElementById("upload" + tag).files;
        if (files.length == 0) return;
        var name = files[0].name || files[0].fileName;
        if (!/\.(gif|jpg|jpeg|png|ico|GIF|JPG|PNG|JPEG|ICO)$/.test(name)) {
            alert("请上传图片格式为：gif|jpg|jpeg|png|ico的图片！");
            return;
        }
        var ImgExt = name.split('.');
        var Ext = ImgExt[ImgExt.length - 1];
        var id = "f" + parseInt(Math.random() * 1000000);
        var url = "/Upload/UploadImgageNew?ImgPathEnum=" + encodeURIComponent(ImgPathEnum) + "&IsFullPath=" + encodeURIComponent(IsFullPath);
        $("#orgimg").attr("style", "display:block;");
        setTimeout(function () {
            var HttpRequest = new XMLHttpRequest();
            HttpRequest.open("POST", url, true);
            HttpRequest.setRequestHeader("Content-Type", "application/octet-stream");
            HttpRequest.setRequestHeader("fileext", Ext);
            HttpRequest.setRequestHeader("X-File-Name", encodeURIComponent(name));
            HttpRequest.onreadystatechange = function () {
                if (HttpRequest.readyState == 4 && HttpRequest.responseText != "" && HttpRequest.status == 200) {
                    $("#orgimg").attr("style", "display:none;");
                    $("#" + toid).val(HttpRequest.responseText);
                }
            }
            HttpRequest.send(files[0]);
        }, 200);
    },
    //初始化下拉菜单
    InintSelect: function (ID) {
        G.SkipSqlInject(ID);
        var data = $("#" + ID + "_hidden").val();
        if (data != "") {
            //alert(data);
            var select = $("#" + ID).find("option[value='" + data + "']");
            select.attr("selected", "selected");
        }
    },
    //批量删除
    CommonDelete: function (arr, postUrl, toUrl) {
        G.SkipSqlInject(postUrl);
        G.SkipSqlInject(toUrl);
        if (arr.length <= 0) {
            alert("请选中您要删除的记录！");
            return;
        }
        if (!confirm("确定要删除吗，删除后将无法恢复？")) {
            return;
        }
        $.post(postUrl, "IDS=" + arr, function (x) {
            if (x.ret == undefined) {
                alert(x.Message);
                return;
            }
            if (x.ret == "0000") {
                alert("删除成功!");
                location.href = location.href;
            } else {
                alert(x.msg);
            }
        });
    },
    //是否存在指定函数 
    IsExitsFunction: function (funcName) {
        try {
            if (typeof (eval(funcName)) == "function") {
                return true;
            }
        } catch (e) { }
        return false;
    },
    //是否存在指定变量 
    IsExitsVariable: function (variableName) {
        try {
            if (typeof (variableName) == "undefined") {
                //alert("value is undefined"); 
                return false;
            } else {
                //alert("value is true"); 
                return true;
            }
        } catch (e) { }
        return false;
    },
    tureLayerMsg: function () {
        $("#layerMsg").remove();
        return true;
    },
    falseLayerMsg: function () {
        $("#layerMsg").remove();
        return false;
    },
    addErrorMsg: function (obj, err) {
        var msg = '\n<span class="help-inline" style="color:red;display:block;">' + err + '</span>';
        if ($(obj).next().attr("class") != "help-inline") {
            $(obj).after(msg);
        }
        $(obj).focus();
    },
    removeErrorMsg: function (obj) {
        if ($(obj).next().attr("class") == "help-inline") {
            $(obj).next().remove();
        }
        //removeErrorMsg(obj);
    },
    RepNumber: function (obj, type) {
        G.ValidateData(obj, type, function (x) { addErrorMsg(obj, x); }, function () { removeErrorMsg(obj); });
    },
    ValidateData: function (obj, type, fail, success) {
        var v = $(obj).val();
        var regEmail = /^[a-zA-Z\d][-\.\w]*@(?:[-\w]+\.)+(?:[a-zA-Z])+$/;
        var errEmail = "邮箱格式错误！";
        var regPwd = /^[a-zA-Z]\w{5,17}$/;
        var errPwd = "密码以字母开头，长度在6~18之间，只能包含字母、数字和下划线！";
        var cferrPwd = "确认密码和密码一样以字母开头，长度在6~18之间，只能包含字母、数字和下划线！";
        var regOCardNum = /^(\d{16}|\d{19})$/;
        var errOCardNum = "卡号必须为16位或者19位数字！";
        var regCellphone = /^0?(1)[0-9]{10}$/;
        var errCellphone = "手机号码格错误，必须为11位数字！";
        var regHomephone = /^0\d{2,3}-?\d{7,8}$/;
        var errHomePhone = "座机格式错，正确格式如：027-1234567或027-12345678 ！";
        var regZipCode = /^[0-9][0-9]{5}$/;
        var errZipCode = "邮编为6位数字！";
        var regString = /^\s*$/g;
        var errUserName = "用户名不为空！ ";
        var errAddress = "地址不为空！";
        var errAccount = "邮箱或手机号码格式错误！";
        var errVerCode = "验证码不能为空！";
        var regBankCard = /^(\d{16}|\d{19})$/;
        var errBankCard = "银行卡号必须为16位或者19位数字！";
        var regFeiFuZhengShu = /^[1-9]\d*|0$/;//非负整数
        var errFeiFuZhengShu = "请输入非负整数";
        var regZhengZhengShu = /^[1-9]\d*$/;//正整数
        var errZhengZhengShu = "请输入正整数";
        var regFeiFuShu = /^\+?(:?(:?\d+\.\d+)|(:?\d+))$/;// /^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|([0-9])$/;//非负整数
        var errFeiFuShu = "请输入非负数";
        var regZSY = /^0?(9)[0-9]{15}$/;
        var errZSY = "中石油油卡号为以9开头的16位数字，请核对后输入！";//16位数字
        var regZSH = /^\d{19}$/;
        var errZSH = "中石化油卡号为19为数，请核对后输入！";//19位数字
        if (type == "0") type = "zsy";
        if (type == "1") type = "zsh";
        if (type == "2") type = "bankcard";
        switch (type) {
            case "email": {
                if (regEmail.test(v)) { success ? success() : ''; } else { fail ? fail(errEmail) : ''; }
            } break;
            case "password": {
                if (regPwd.test(v)) { success ? success() : ''; } else { fail ? fail(errPwd) : ''; }
            } break;
            case "cfpassword": {
                if (regPwd.test(v)) { success ? success() : ''; } else { fail ? fail(cferrPwd) : ''; }
            } break;
            case "cellphone": {
                if (regCellphone.test(v)) { success ? success() : ''; } else { fail ? fail(errCellphone) : ''; }
            } break;
            case "homephone": {
                if (regHomephone.test(v)) { success ? success() : ''; } else { fail ? fail(errHomePhone) : ''; }
            } break;
            case "zipcode": {
                if (regZipCode.test(v)) { success ? success() : ''; } else { fail ? fail(errZipCode) : ''; }
            } break;
            case "username": {
                if (!regString.test(v)) { success ? success() : ''; } else { fail ? fail(errUserName) : ''; }
            } break;
            case "address": {
                if (!regString.test(v)) { success ? success() : ''; } else { fail ? fail(errAddress) : ''; }
            } break;
            case "vercode": {
                if (!regString.test(v)) { success ? success() : ''; } else { fail ? fail(errVerCode) : ''; }
            } break;
            case "account": {
                if (regCellphone.test(v) || regEmail.test(v)) { success ? success() : ''; } else { fail ? fail(errAccount) : ''; }
            } break;
            case "bankcard": {
                if (regBankCard.test(v) || regBankCard.test(v)) { success ? success() : ''; } else { fail ? fail(errBankCard) : ''; }
            } break;
            case "feifuzhengshu": {
                if (regFeiFuZhengShu.test(v) || regFeiFuZhengShu.test(v)) { success ? success() : ''; } else { fail ? fail(errFeiFuZhengShu) : ''; }
            } break;
            case "feifushu": {
                if (regFeiFuShu.test(v) || regFeiFuShu.test(v)) { success ? success() : ''; } else { fail ? fail(errFeiFuShu) : ''; }
            } break;
            case "zhengzhengshu": {
                if (regZhengZhengShu.test(v) || regZhengZhengShu.test(v)) { success ? success() : ''; } else { fail ? fail(errZhengZhengShu) : ''; }
            } break;
            case "ocard": {
                if (regOCardNum.test(v) || regOCardNum.test(v)) { success ? success() : ''; } else { fail ? fail(errOCardNum) : ''; }
            } break;
            case "zsy": {
                if (regZSY.test(v) || regZSY.test(v)) { success ? success() : ''; } else { fail ? fail(errZSY) : ''; }
            } break;
            case "zsh": {
                if (regZSH.test(v) || regZSH.test(v)) { success ? success() : ''; } else { fail ? fail(errZSH) : ''; }
            } break;
            default: {
                if (!regString.test(v)) { success ? success() : ''; } else { fail ? fail("该项不能为空，请正确填写！") : ''; }
            } break;
        }
    },
    QueryList: function (url) {
        var key = $("#msg-box").val();
        var BeginTime = $("#msg-box1").val();
        var EndTime = $("#msg-box2").val();
        G.SkipSqlInject(key);
        G.SkipSqlInject(BeginTime);
        G.SkipSqlInject(EndTime);
        if (url.indexOf("?") > 0) {
            location.href = url + "&&key=" + key + "&&BeginTime=" + BeginTime + "&&EndTime=" + EndTime;
        }
        else {
            location.href = url + "?key=" + key + "&&BeginTime=" + BeginTime + "&&EndTime=" + EndTime;
        }
        //   location.href = url + "?key=" + $("#msg-box").val();
    },

    QueryListTow: function (url) {
        var key = $("#msg-box").val();
        var BeginTime = $("#msg-box1").val();
        var EndTime = $("#msg-box2").val();
        var CurrencyID = $("#CurrencyID").val();
        var CoinID = $("#CoinID").val();
        var Type = $("#Type").val();
        location.href = url + "?Key=" + key + " &&BeginTime=" + BeginTime + "&&EndTime=" + EndTime + "&&CurrencyID=" + CurrencyID + "&&CoinID=" + CoinID + "&&Type=" + Type;
    },
    QueryListTree: function (url) {
        var key = $("#msg-box").val();
        var BeginTime = $("#msg-box1").val();
        var EndTime = $("#msg-box2").val();
        var CurrencyID = $("#CurrencyID").val();
        var CoinID = $("#CoinID").val();
        var Type = $("#Status").val();
        location.href = url + "?Key=" + key + " &&BeginTime=" + BeginTime + "&&EndTime=" + EndTime + "&&CurrencyID=" + CurrencyID + "&&CoinID=" + CoinID + "&&Status=" + Type;
    },
    RedirectToUrl: function (url) {
        location.href = url;
    },
    SetEmptyValue: function (id) {
        $("#" + id).val('');
    },
    ResetForm: function () { location.href = location.href; }
    ,
    GetTopMenuHtml: function (currItem, itemList) {
        var strVar = "";
        var childList = new Array();
        for (var i = 0; i < itemList.length; i++) {
            if (currItem.ID == itemList[i].ParentID) {
                childList.push(itemList[i].ID);
            }
        }
        if (childList.length <= 0) {
            if (currItem.Name == "注销") {
                strVar += "<li class=\"\"><a title=\"\" href=\"" + currItem.Url + "\"><i sysimg=\"" + currItem.Icon + "\"><\/i> <span class=\"text\">" + currItem.Name + "<\/span><\/a><\/li>";
            } else {
                strVar += "<li class=\"\"><a  target=\"mainframe\" title=\"\" href=\"" + currItem.Url + "\"><i sysimg=\"" + currItem.Icon + "\"><\/i> <span class=\"text\">" + currItem.Name + "<\/span><\/a><\/li>";
            }
        } else {
            if (currItem.Name == "控制面板") {
                strVar += "<li class=\"dropdown\" id=\"profile-messages\">";
                strVar += "<a title=\"\" href=\"#\" data-toggle=\"dropdown\" data-target=\"#profile-messages\" class=\"dropdown-toggle\">";

            } else {
                strVar += "<li class=\"dropdown\" id=\"menu-messages\">";
                strVar += "<a title=\"\" href=\"#\" data-toggle=\"dropdown\" data-target=\"#menu-messages\" class=\"dropdown-toggle\">";
            }
            strVar += "<i class=\"icon icon-user\"><\/i> ";
            strVar += "<span class=\"text\">" + currItem.Name + "<\/span>";
            strVar += "<b class=\"caret\"><\/b>";
            strVar += "<\/a>";
            strVar += "<ul class=\"dropdown-menu\">";
            strVar += G.GetTopLiHtml(itemList, childList, currItem);
            strVar += "<\/ul>";
            strVar += "<\/li>";
        }
        return strVar;
    },
    GetTopLiHtml: function (itemList, childList, currItem) {
        var strVar = "";
        for (var i = 0; i < itemList.length; i++) {
            if (childList.indexOf(itemList[i].ID) > -1) {
                if (itemList[i].Name == "注销") {
                    strVar += "            <li><a href=\"" + itemList[i].Url + "\"><i sysimg=\"" + itemList[i].Icon + "\"><\/i> " + itemList[i].Name + "<\/a><\/li>";
                } else {
                    strVar += "            <li><a target=\"mainframe\" href=\"" + itemList[i].Url + "\"><i sysimg=\"" + itemList[i].Icon + "\"><\/i> " + itemList[i].Name + "<\/a><\/li>";
                }
                if (i < itemList.length - 2) {
                    strVar += "            <li class=\"divider\"><\/li>";

                }
            }
        }
        return strVar;
    },
    GetLeftMenuHtml: function (data) {
        var strVar = "";
        var currMenuID = "";
        for (var i = 0; i < data.Childs.length; i++) {
            if (location.href.indexOf(data.Childs[i].Url) > 0) {
                currMenuID = data.Childs[i].ID;
            }
        }
        if (currMenuID != "") {
            strVar += "<li class=\"submenu active open\"> ";
        } else {
            strVar += "<li class=\"submenu\"> ";
        }
        strVar += "<a href=\"" + data.Url + "\" onclick=\"MenuHandle(this);\">";
        strVar += "<i sysimg=\"" + data.Icon + "\"><\/i>";
        strVar += " <span>" + data.Name + "<\/span> ";
        //strVar += "<span class=\"label label-important\">" + data.Name + "<\/span>";
        strVar += "<\/a>";
        if (currMenuID != "") {
            strVar += "<ul style=\"display: block;\">";
        } else {
            strVar += "<ul style=\"display: none;\">";
        }
        if (data.Childs != null) {
            strVar += G.GetLeftLiHtml(data.Childs, currMenuID);
        }
        strVar += "<\/ul>";
        strVar += "<\/li>";
        return strVar;
    },
    GetLeftLiHtml: function (childs, currMenuID) {
        var strVar = "";
        if (childs.length <= 0) return "";
        for (var i = 0; i < childs.length; i++) {
            if (currMenuID != "" && currMenuID == childs[i].ID) {
                strVar += "<li style='background-color:#737B77;' sysimg=\"" + childs[i].Icon + "\"><a   target=\"mainframe\" href=\"" + childs[i].Url + "\">" + childs[i].Name + "<\/a><\/li>";
            } else {
                strVar += "<li  sysimg=\"" + childs[i].Icon + "\"><a target=\"mainframe\" href=\"" + childs[i].Url + "\">" + childs[i].Name + "<\/a><\/li>";
            }
        }
        return strVar;
    },
    //初始化下拉菜单
    InintSelect: function (ID) {
        var data = $("#" + ID + "_hidden").val();
        if (data != "") {
            //alert(data);
            //var select = $("#" + ID).find("option[value='" + data + "']");
            //select.attr("selected", "selected");
            $("#" + ID).val(data);
        }
    },
    //时间查询
    QueryTimeList: function (url) {
        G.SkipSqlInject(url);
        var StartTime = $('#StartTime').val();
        G.SkipSqlInject(StartTime);
        var EndTime = $('#EndTime').val();
        G.SkipSqlInject(EndTime);
        location.href = url + "?StartTime=" + encodeURIComponent(StartTime) + "&EndTime=" + encodeURIComponent(EndTime);
    }
};

//数据导出
function ExportData(url) {
    url += "?";
    var theForm = $('#ExportDataFrom');
    var SizeValue = $("select=[name='TotalSize']").val();
    if (SizeValue == "0" || SizeValue == undefined || SizeValue == "") {
        alert("请选择导出条数");
        return;
    }
    var tagElements = theForm.find('input,select');
    tagElements.each(function () {
        var Name = $(this).attr("name") || "";
        var Value = $(this).val();
        if (Name != "") {
            if (url.indexOf("&") > 0) {
                url += "&" + Name + "=" + Value;
            }
            else {
                url += Name + "=" + Value + "&";
            }
        }
    })
    //console.log(SendData);
    //发送请求
    window.open(url, "_Blank");
    //$.ajax({
    //    type: "Get",
    //    url: url,
    //    data: SendData,
    //    dataType: "json"
    //})
}