// JavaScript Document
$(function () {
    /**
     * @content 处理顶部会员下拉菜单效果
     * @author  lrxiang 
     */
    $(".Manage_option .iconfont").mouseover(function () {
        $(this).addClass("curr");
        $(this).next().show();
    });
    $(".Manage_option").mouseleave(function () {
        $(this).find(".iconfont").removeClass("curr");
        $(this).find(".drop_wrap").hide();
    });

    /**
     * @content 侧边菜单栏滑动事件处理
     * @author  lrxiang 
     */
    $(".cate_listTree .cate_name").click(function (event) {
        var obj = $(this).find(".expandable").eq(0);
        var e = event || window.event;
        e.stopPropagation();
        if (obj.hasClass("close")) {
            $(this).next().stop(true, true).slideDown();
            $(this).addClass("curr");
            obj.removeClass("close").addClass("open");
            return false;
        } else if (obj.hasClass("open")) {
            $(this).next().stop(true, true).slideUp();
            $(this).removeClass("curr");
            obj.removeClass("open").addClass("close");
            return false;
        } else {
            return true;
        }
    });

    /**
     * @content 会员列表
     * @author  lrxiang 
     */
    if ($(".container_table").size() > 0) {
        $(".container_table tbody tr:odd td").css({
            "background-color": "#fafafa"
        })
    }

    /**
     * @content 表单UI 
     */
    if ($.R_CommonUI != undefined) {

        var R_CommonUI = new $.R_CommonUI();
        if ($(".rule-single-select").size() > 0) {
            for (var i = 0 ; i < $(".rule-single-select").size() ; i++) {
                var theObj = $(".rule-single-select").eq(i);
                R_CommonUI.InitSingleSelect(theObj);
            }
        }
        if ($(".rule-single-checkbox").size() > 0) {
            for (var i = 0 ; i < $(".rule-single-checkbox").size() ; i++) {
                var theObj = $(".rule-single-checkbox").eq(i);
                R_CommonUI.InitSingleCheckbox(theObj);
            }
        }
    }

    //处理菜单的事件
    $("#content-header a").click(function () {
        var _theHref = $(this).attr("href");
        if (_theHref == "#") {
            return false;
        }
    })

    /**
     * @content 加载菜单树 （权限）
     */
    if ($("#Quanxian_Tree").size() > 0) {
        var theArrays = [];
        var Stringsss = "";
        var k = 0;
        var AjaxUrl = $("#Quanxian_Tree").attr("url");  //接口地址
        $.getScript(AjaxUrl, function (data) {
            if (typeof data == "string") {
                data = $.parseJSON(data);
            }
            GetTree(data, k);
        });

        /**
         * @content 递归权限树 
         */
        function GetTree(data, k,CatePid) {
            for (var i = 0; i < data.length ; i++) {

                var CatePid = CatePid || 0;
                var _cateId = data[i].cateId;  //分类id
                var _cateName = data[i].cateName;  //分类的名称
                var _catePid = data[i].catePid == null ? CatePid : data[i].catePid;  //分类id 

                var _QuanXianArray = data[i].cateQuanxian;  //权限操作的内容  
                var strVar = "";
                strVar += "<tr>";   
                strVar += "	<td style=\"white-space:nowrap;word-break:break-all;overflow:hidden;\" pid=\"" + _catePid + "\">";
                for (var j = 0 ; j <= k ; j++) {
                    if (j == k) {
                        strVar += "<span class=\"folder-open\"><\/span>";
                    } else if (j == (k - 1)) {
                        strVar += "<span class=\"folder-line\"><\/span>";
                    } else {
                        strVar += "<span style=\"display:inline-block;width:24px;\"></span>";
                    }
                }
                strVar += _cateName;
                strVar += "	<\/td>";
                strVar += "	<td style=\"text-align: center;\">";
                strVar += "		<span class=\"cbllist\">";
                for (var s in _QuanXianArray) {
                    if (_QuanXianArray[s].state == 1) {
                        strVar += "		<input id=\"CheckQx_Pid" + _catePid + "_QxId_" + _QuanXianArray[s].quanxianid + "\" name='checkObj' checked='checked' class='quanxian_checkbox gl_pid_" + _catePid + "' pid=\"" + _catePid + "\" type=\"checkbox\" value='" + _QuanXianArray[s].quanxianid + "'><label for=\"CheckQx_Pid" + _catePid + "_QxId_" + _QuanXianArray[s].quanxianid + "\">" + _QuanXianArray[s].quanxian + "<\/label><\/span>";
                    } else {
                        strVar += "		<input id=\"CheckQx_Pid" + _catePid + "_QxId_" + _QuanXianArray[s].quanxianid + "\" name='checkObj' class='quanxian_checkbox gl_pid_" + _catePid + "' pid=\"" + _catePid + "\" type=\"checkbox\" value='" + _QuanXianArray[s].quanxianid + "'><label for=\"CheckQx_Pid" + _catePid + "_QxId_" + _QuanXianArray[s].quanxianid + "\">" + _QuanXianArray[s].quanxian + "<\/label><\/span>";
                    }
                }
                strVar += "		<\/span>";
                strVar += "	<\/td>";
                strVar += "	<td style=\"text-align: center;\">";
                strVar += "		<input name=\"checkAll\" gl_class='gl_pid_" + _catePid + "' class=\"check_all\" type=\"checkbox\">";
                strVar += "	<\/td>";
                strVar += "<\/tr>";

                $("#Quanxian_Tree").append($(strVar));
                if (data[i].childCateList&&data[i].childCateList.length > 0) {
                    GetTree(data[i].childCateList, k + 1, _cateId);
                }
            }
        }

        /**
         * @content 绑定递归树的check_ALL事件(全选) 
         */
        $("#Quanxian_Tree").on("click", ".check_all", function () {
            var _theGl_Class = $(this).attr("gl_class") == undefined ? "" : $(this).attr("gl_class");
            var theState = false;
            if ($(this).is(":checked")) {
                $(this).parent().parent().find("." + _theGl_Class).each(function (index, element) {
                    $(this).prop("checked", true);

                    var _thePid = $(this).attr("pid"); //父级id
                    if (_thePid == "0") {
                        var _the_id = $(this).val(); //当前id
                        $("#Quanxian_Tree").find(".quanxian_checkbox[pid='" + _the_id + "']").each(function () {
                            var _theTxt = $(this).next().text();
                            if (_theTxt == "查看") {
                                $(this).prop("checked", true);
                            }
                        }); 

                    } else {
                        var _parentsObjs = $("#Quanxian_Tree").find(".quanxian_checkbox[value='" + _thePid + "']").eq(0); //父级的查看的点
                        if (!_parentsObjs.is(":checked")) {
                            _parentsObjs.prop("checked", true);
                        } 
                    }
                });
            } else {
                $(this).parent().parent().find("." + _theGl_Class).each(function (index, element) {
                    $(this).prop("checked", false);
                });
            }
            
        });

        /**
        * @content 权限树的选中事件
        */
        $("#Quanxian_Tree").on("click", ".quanxian_checkbox", function () {
            if ($(this).is(":checked")) {
                var _thParent_id = $(this).attr("pid"); //父级id
                var _the_id = $(this).val(); //当前id
                if (_thParent_id != 0) {
                    var ParentObj = $("#Quanxian_Tree").find(".quanxian_checkbox[value='" + _thParent_id + "']").eq(0);

                    if (ParentObj.is(":checked")) {
                        return true;
                    } else {
                        ParentObj.prop("checked", true);
                    }

                } else {
                    $("#Quanxian_Tree").find(".quanxian_checkbox[pid='" + _the_id + "']").each(function () {
                        var _theTxt = $(this).next().text();
                        if (_theTxt == "查看") {
                            $(this).prop("checked", true);
                        }
                    });
                }

            } else {
                var _the_id = $(this).val(); //当前id
                var _the_pid = $(this).attr("pid");
                var _theText = $(this).next().text(); //是否等于查看
                if (_theText == "查看") {
                    //$("#Quanxian_Tree").find(".quanxian_checkbox[pid='" + _the_pid + "']").prop("checked", false);
                    $("#Quanxian_Tree").find(".quanxian_checkbox[pid='" + _the_id + "']").each(function () {
                        if ($(this).is(":checked")) {
                            $(this).prop("checked", false);
                        }

                        $(this).parents("tr").find(".check_all").eq(0).prop("checked", false);
                    });
                }
            }
        });


    }


    /**
     * @content 区域树 
     */
    if ($("#area_TreeBox").size() > 0) {
        var theArrays = [];
        var Stringsss = "";
        var k = 0;
        var AjaxUrl = $("#area_TreeBox").attr("url");  //接口地址
        $.getScript(AjaxUrl, function (data) {
            if (typeof data == "string") {
                data = $.parseJSON(data);
            }

            GetAreaTree(data, k);
        });

        /**
         * @content 递归权限树 
         */
        function GetAreaTree(data, k) {
            for (var i = 0; i < data.length ; i++) {
                var _cateId = data[i].ID;  //区域位置id
                var _cateCode = data[i].Code;  //区域位置id
                var _cateName = data[i].Name;  //分类的名称
                var _cateIco = data[i].ImageUrl;  //分类的名称
                var _catePid = data[i].ParentID == null ? 0 : data[i].ParentID;  //分类id   
                var strVar = "";
                strVar += "<tr>";
                strVar += "	<td align=\"center\">";
                strVar += "		<input name=\"checkObj\" Pid='" + _catePid + "' CateId='" + _cateId + "' codeCode='" + _cateCode + "' class=\"check_obj\" type=\"checkbox\">";
                strVar += "	<\/td>";
                strVar += "	<td>";
                strVar += "		<span class=\"cbllist\">";
                strVar += "			<img src='" + _cateIco + "' width='30px' height='30px' />";
                strVar += "		<\/span>";
                strVar += "	<\/td>";
                strVar += "	<td>";
                strVar += "		<span class=\"cbllist\">";
                strVar += _cateId;
                strVar += "		<\/span>";
                strVar += "	<\/td>";
                strVar += "	<td class='tree_td' style=\"white-space:nowrap;word-break:break-all;overflow:hidden;\" pid=\"" + _catePid + "\">";
                for (var j = 0 ; j <= k ; j++) {
                    if (j == k) {
                        strVar += "<span class=\"folder-open\"><\/span>";
                    } else if (j == (k - 1)) {
                        strVar += "<span class=\"folder-line\"><\/span>";
                    } else {
                        strVar += "<span style=\"display:inline-block;width:24px;\"></span>";
                    }
                }
                strVar += _cateName;
                strVar += "	<\/td>";
                strVar += "	<td>";
                strVar += "		<span class=\"cbllist\">";
                strVar += "			<a  Pid='" + _catePid + "' CateId='" + _cateId + "' codeCode='" + _cateCode + "' class='add_child'  href='javascript:void(0)'>添加子级 </a>";
                strVar += "			<a  Pid='" + _catePid + "' CateId='" + _cateId + "' codeCode='" + _cateCode + "' class='editor_obj' href='javascript:void(0)'>修改 </a>";
                strVar += "		<\/span>";
                strVar += "	<\/td>";

                strVar += "<\/tr>";

                $("#area_TreeBox").append($(strVar));
                if (data[i].Childs.length > 0) {
                    GetAreaTree(data[i].Childs, k + 1);
                }
            }
        }

    }


    /**
     * @content 菜单列表 （树形结构）
     */
    if ($("#Cate_TreeBoxTree").size() > 0) {
        var theArrays = [];
        var Stringsss = "";
        var k = 0;
        var AjaxUrl = $("#Cate_TreeBoxTree").attr("url");  //接口地址
        $.getScript(AjaxUrl, function (data) {
            if (typeof data == "string") {
                data = $.parseJSON(data);
                //console.log(data);
            }

            GetCateTree(data, k, "");
        });
        $("#lbtnSearchTree").click(function () {
            var keyword = $("#txtKeywordsTree").val();

            var AjaxUrl = $("#Cate_TreeBoxTree").attr("url") + "?keyword=" + keyword;  //接口地址
            $.getScript(AjaxUrl, function (data) {
                if (typeof data == "string") {
                    data = $.parseJSON(data);
                    console.log(data);
                }
                $("#Cate_TreeBoxTree tbody").html("");
                GetCateTree(data, k, keyword);
            });
        })

        /**
         * @content 递归权限树 
         */
        function GetCateTree(data, k, keyword) {
            for (var i = 0; i < data.length ; i++) {
                var _cateId = data[i].ID;  //区域位置id
                var _cateCode = data[i].Code;  //区域位置id
                var _cateName = data[i].Name;  //分类的名称
                var _Sort = data[i].Sort;
                var _cateIco = data[i].Icon;  //分类的名称
                var _catePid = data[i].ParentID == "" ? 0 : data[i].ParentID;  //分类id  
                var _catePName = data[i].ParentName == "" ? "" : data[i].ParentName;//父级名称
                var strVar = "";
                strVar += "<tr>";
                strVar += "	<td>";
                strVar += "		<input name=\"checkObj\" value='" + _cateId + "' Pid='" + _catePid + "' CateId='" + _cateId + "' codeCode='" + _cateCode + "' type=\"checkbox\">";
                strVar += "	<\/td>";
                strVar += "		<span class=\"cbllist\">";
                strVar += "<td align=\"center\">";
                strVar += _cateCode;
                strVar += "		<\/span>";
                strVar += "	<\/td>";
                strVar += "	<td class='tree_td' style=\"white-space:nowrap;word-break:break-all;overflow:hidden;\" pid=\"" + _catePid + "\">";
                for (var j = 0 ; j <= k ; j++) {
                    if (j == k) {
                        strVar += "<span class=\"folder-open\"><\/span>";
                    } else if (j == (k - 1)) {
                        strVar += "<span class=\"folder-line\"><\/span>";
                    } else {
                        strVar += "<span style=\"display:inline-block;width:24px;\"></span>";
                    }
                }
                strVar += _cateName;
                strVar += "	<\/td>";
                strVar += "	<td>";
                strVar += _catePName;
                strVar += "	<\/td>";
                strVar += "	<td style=\"text-align: center;\">";
                strVar += "		<span class=\"cbllist\" align=\"center\">";
                strVar += "			<a  Pid='" + _catePid + "' CateId='" + _cateId + "' codeCode='" + _cateCode + "' class='btn btn-primary btn-mini'  href='/Ruiec.Admin/Menu/Create?pid=" + _cateId + "'>添加子级 </a>";
                strVar += "			<a  Pid='" + _catePid + "' CateId='" + _cateId + "' codeCode='" + _cateCode + "' class='btn btn-primary btn-mini' href='/Ruiec.Admin/Menu/Edit?fid=" + _cateId + "'>修改 </a>";
                strVar += "		<\/span>";
                strVar += "	<\/td>";
                strVar += "<\/tr>";
                $("#Cate_TreeBoxTree").append($(strVar));
                if (keyword == "" || keyword==undefined) {
                    if (data[i].Childs.length > 0) {
                        GetCateTree(data[i].Childs, k + 1);
                    }
                }
            }
        }

    }



    //页面开始就加载
    //左侧高度
    if ($(".admin_fl").size() || $(".user_logo").size() || $(".administrator").size()) {
        var WinHeight = $(window).height() - $(".user_logo").height() - $(".administrator").height();
        var HomeLeftCateList_Cate = $(".admin_fl");
        HomeLeftCateList_Cate.each(function (index, element) {
            $(this).css({
                "height": WinHeight + "px",
                "overflow-x": "hidden",
                "overflow-y": "auto"
            })
        });
    }
    if ($(".contents_fr").size()) {
        //右侧宽度
        var WinWidth = $(window).width() - $(".contents_fl").width();
        var HomeLeftCateList_Cate = $(".contents_fr");
        HomeLeftCateList_Cate.each(function (index, element) {
            $(this).css({
                "width": WinWidth + "px"
            })
        });
    }

    if ($(".contents_main").size() || $(".user_logo").size()) {
        //右侧下内容高度
        var WinContHeight = $(window).height() - $(".user_top").height();
        var HomeLeftCateList_Cate = $(".contents_main");
        HomeLeftCateList_Cate.each(function (index, element) {
            $(this).css({
                "height": WinContHeight + "px",
                "overflow-x": "hidden",
                "overflow-y": "auto"
            })
        });
    }

    if ($(".contents_main").size() || $(".contents_main_top").size()) {
        //右侧导航内容高度
        var WinCenterHeight = $(".contents_main").height() - $(".contents_main_top").height() - 32;
        var HomeLeftCateList_Cate = $(".wapper_main");
        HomeLeftCateList_Cate.each(function (index, element) {
            $(this).css({
                "height": WinCenterHeight + "px",
                "overflow-x": "hidden",
                "overflow-y": "auto"
            })
        });
    }
})