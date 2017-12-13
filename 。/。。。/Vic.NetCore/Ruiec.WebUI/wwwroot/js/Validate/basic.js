// JavaScript Document
$(function(){
	/**
     * @content 处理顶部会员下拉菜单效果
     * @author  lrxiang 
     */
	 $(".Manage_option .iconfont").mouseover(function(){
		$(this).addClass("curr");
		$(this).next().show(); 
	 }); 
	 $(".Manage_option").mouseleave(function(){
		$(this).find(".iconfont").removeClass("curr");
		$(this).find(".drop_wrap").hide(); 
	 });
	  
	/**
     * @content 侧边菜单栏滑动事件处理
     * @author  lrxiang 
     */
	 $(".cate_listTree .cate_name").click(function(event){
		var obj = $(this).find(".expandable").eq(0);
		var e = event || window.event;
		e.stopPropagation(); 
		if(obj.hasClass("close")){
			$(this).next().stop(true,true).slideDown(); 
			$(this).addClass("curr");
			obj.removeClass("close").addClass("open");
			return false;
		}else if(obj.hasClass("open")){
			$(this).next().stop(true,true).slideUp();	
			$(this).removeClass("curr");
			obj.removeClass("open").addClass("close");
			return false;
		}else{
			return true;	
		} 
	 });  

	/**
     * @content 会员列表
     * @author  lrxiang 
     */
	 if($(".container_table").size()>0){
		$(".container_table tbody tr:odd td").css({
			"background-color":"#fafafa"	
		})		 
	 }
	   
	/**
     * @content 表单UI 
     */
    if($.R_CommonUI != undefined){
	    	
	    var R_CommonUI = new $.R_CommonUI(); 
	 	R_CommonUI.InitSingleSelect($(".rule-single-select"));
    }
	
	  
	/**
     * @content 加载菜单树 （权限）
     */
	 if($("#Quanxian_Tree").size()>0){ 
		var theArrays = [] ;  
		var Stringsss = "";
		var k = 0;
		var AjaxUrl = $("#Quanxian_Tree").attr("url");  //接口地址
		$.getScript(AjaxUrl,function(data){ 
			if(typeof data == "string"){
				data = $.parseJSON(data); 	
			} 
			GetTree(data,k);  
		});
		
	   /**
		* @content 递归权限树 
		*/
		function GetTree(data,k){  
			for(var i = 0; i < data.length ; i++){  
				 
				var _cateId = data[i].cateId;  //分类id
				var _cateName = data[i].cateName;  //分类的名称
				var _catePid = data[i].catePid == null ? 0 : data[i].catePid;  //分类id 
				
				var _QuanXianArray = data[i].cateQuanxian;  //权限操作的内容 
				console.log(_QuanXianArray);
				var strVar = "";
					strVar += "<tr>";
					strVar += "	<td style=\"white-space:nowrap;word-break:break-all;overflow:hidden;\" pid=\""+_catePid+"\">"; 
					for(var j = 0 ; j <= k ; j++){ 
						if(j==k){
							strVar += "<span class=\"folder-open\"><\/span>";	
						}else if(j==(k-1)){
							strVar += "<span class=\"folder-line\"><\/span>";	
						}else{
							strVar += "<span style=\"display:inline-block;width:24px;\"></span>";	
						} 
					} 
					strVar += _cateName ;
					strVar += "	<\/td>";
					strVar += "	<td>";
					strVar += "		<span class=\"cbllist\">";
					for(var s in _QuanXianArray){  
						strVar += "		<input id=\"CheckQx_Pid"+_catePid+"_QxId_"+_QuanXianArray[s].quanxianid+"\" name='checkObj' class='quanxian_checkbox gl_pid_"+_catePid+"' pid=\""+_catePid+"\" type=\"checkbox\" value='"+_QuanXianArray[s].quanxianid+"'><label for=\"CheckQx_Pid"+_catePid+"_QxId_"+_QuanXianArray[s].quanxianid+"\">"+_QuanXianArray[s].quanxian+"<\/label><\/span>";	
					}
					strVar += "		<\/span>";	
					strVar += "	<\/td>";
					strVar += "	<td align=\"center\">";
					strVar += "		<input name=\"checkAll\" gl_class='gl_pid_"+_catePid+"' class=\"check_all\" type=\"checkbox\">";
					strVar += "	<\/td>";
					strVar += "<\/tr>";
					
				$("#Quanxian_Tree").append($(strVar));
				if(data[i].childCateList.length > 0 ){  
					GetTree(data[i].childCateList,k+1); 
				}   
			} 
		} 
		
	   /**
		* @content 绑定递归树的check_ALL事件(全选) 
		*/
		$("#Quanxian_Tree").on("click",".check_all",function(){ 
			var _theGl_Class = $(this).attr("gl_class") == undefined ? "" : $(this).attr("gl_class"); 
			var theState =  false; 
			if($(this).is(":checked")){
				theState = true;	
			}
			if(_theGl_Class!=""){
				$(this).parent().parent().find("."+_theGl_Class).each(function(index, element) {
                     $(this).prop("checked",theState);	 
                });	
			} 
		}); 
	 }
	 
		  
	/**
     * @content 区域树 （权限）
     */ 
    if($("#area_TreeBox").size()>0){
    	var theArrays = [] ;  
		var Stringsss = "";
		var k = 0;
		var AjaxUrl = $("#area_TreeBox").attr("url");  //接口地址
		$.getScript(AjaxUrl,function(data){ 
			if(typeof data == "string"){
				data = $.parseJSON(data); 	
			} 
			 
			GetAreaTree(data,k);  
		});
		
	   /**
		* @content 递归权限树 
		*/
		function GetAreaTree(data,k){  
			for(var i = 0; i < data.length ; i++){   
				var _cateId = data[i].ID;  //区域位置id
				var _cateCode = data[i].Code;  //区域位置id
				var _cateName = data[i].Name;  //分类的名称
				var _cateIco = data[i].ImageUrl;  //分类的名称
				var _catePid = data[i].ParentID == null ? 0 : data[i].ParentID;  //分类id   
				var strVar = "";
					strVar += "<tr>";
					strVar += "	<td align=\"center\">";
					strVar += "		<input name=\"checkObj\" Pid='"+_catePid+"' CateId='"+_cateId+"' codeCode='"+_cateCode+"' class=\"check_obj\" type=\"checkbox\">";
					strVar += "	<\/td>";
					strVar += "	<td>";
					strVar += "		<span class=\"cbllist\">"; 
					strVar += "			<img src='"+_cateIco+"' width='30px' height='30px' />";	
					strVar += "		<\/span>";	
					strVar += "	<\/td>"; 
					strVar += "	<td>";
					strVar += "		<span class=\"cbllist\">"; 
					strVar += _cateId;	
					strVar += "		<\/span>";	
					strVar += "	<\/td>";
					strVar += "	<td class='tree_td' style=\"white-space:nowrap;word-break:break-all;overflow:hidden;\" pid=\""+_catePid+"\">"; 
					for(var j = 0 ; j <= k ; j++){ 
						if(j==k){
							strVar += "<span class=\"folder-open\"><\/span>";	
						}else if(j==(k-1)){
							strVar += "<span class=\"folder-line\"><\/span>";	
						}else{
							strVar += "<span style=\"display:inline-block;width:24px;\"></span>";	
						} 
					} 
					strVar += _cateName ;
					strVar += "	<\/td>";
					strVar += "	<td>";
					strVar += "		<span class=\"cbllist\">"; 
					strVar += "			<a  Pid='"+_catePid+"' CateId='"+_cateId+"' codeCode='"+_cateCode+"' class='add_child'  href='javascript:void(0)'>添加子级 </a>";
					strVar += "			<a  Pid='"+_catePid+"' CateId='"+_cateId+"' codeCode='"+_cateCode+"' class='editor_obj' href='javascript:void(0)'>修改 </a>"; 
					strVar += "		<\/span>";	
					strVar += "	<\/td>";
					
					strVar += "<\/tr>";
					
				$("#area_TreeBox").append($(strVar));
				if(data[i].Childs.length > 0 ){  
					GetAreaTree(data[i].Childs,k+1); 
				}   
			} 
		}  
    	
    }
	   
})