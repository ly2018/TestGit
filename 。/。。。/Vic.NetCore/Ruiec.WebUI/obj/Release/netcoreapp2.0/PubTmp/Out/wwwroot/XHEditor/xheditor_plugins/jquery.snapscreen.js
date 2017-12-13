
(function ($) {
    var SnapScreen_SnapPlugin, SnapScreen_Document;
    $.fn.SnapPlugin = function (options) {
        var plugins = {
            SnapScreen: { c: 'SnapScreen', t: '屏幕截图 (Ctrl+Alt+A)', s: 'Ctrl+Alt+A', i: function () { }, e: function () { var _this = this; var path = $('body').SnapScreen(_this, options); } }
        };
        return plugins;
    }
    $.fn.SnapScreen = function (haddle, options) {
        var defaultOptions = {
            snapscreenServerPort: location.port //屏幕截图的server端端口
            , snapscreenImgAlign: '' //截图的图片默认的排版方式
            , snapscreenHost: location.hostname//屏幕截图的server端文件所在的网站地址或者ip，请不要加http://
            , snapscreenServerUrl: "/common/Uploadsnapimage.ashx" //屏幕截图的server端保存程序
        };
        var options = $.extend({}, defaultOptions, options);
        return this.each(function () {
            var _this = $(this);
            if (!SnapScreen_SnapPlugin) {
                var container = document.body;
                SnapScreen_Document = container.ownerDocument || container.SnapScreen_Documentument;
                SnapScreen_SnapPlugin = SnapScreen_Document.createElement("object");
                try {
                    SnapScreen_SnapPlugin.type = "application/x-pluginbaidusnap";
                } catch (e) {
                    ShowPluginTips();
                    return;
                }
                SnapScreen_SnapPlugin.style.cssText = "position:absolute;left:-9999px;";
                SnapScreen_SnapPlugin.setAttribute("width", "0");
                SnapScreen_SnapPlugin.setAttribute("height", "0");
                container.appendChild(SnapScreen_SnapPlugin);
            }
            var onSuccess = function (rs) {
                $("#JS_SnapLoading").hide();
                try {
                    rs = eval("(" + rs + ")");
                } catch (e) {
                    return;
                }
                if (rs.state != 'SUCCESS') {
                    alert(rs.state);
                    return;
                }
                try {
                    haddle.pasteHTML("<p><img src=\"" + rs.url + "\" alt=\"\" align='" + options.snapscreenImgAlign + "'/></p>");
                } catch (e) {
                    _this.append("<img src=\"" + rs.url + "\" alt=\"\" align='" + options.snapscreenImgAlign + "'/>");
                }
            };
            var ShowLoadingTips = function () {
                if ($("#JS_SnapLoading").length == 0) {
                    $("<div class=\"SnapScreen_Loading\" id=\"JS_SnapLoading\"></div>").appendTo($("body"));
                }
                $("#JS_SnapLoading").show();
                //开始截图上传
            };
            try {
                var port = options.snapscreenServerPort + '';
                options.snapscreenServerUrl = options.snapscreenServerUrl.split(options.snapscreenHost);
                options.snapscreenServerUrl = options.snapscreenServerUrl[1] || options.snapscreenServerUrl[0];
                if (options.snapscreenServerUrl.indexOf(":" + port) === 0) {
                    options.snapscreenServerUrl = options.snapscreenServerUrl.substring(port.length + 1);
                }

                var ret = SnapScreen_SnapPlugin.saveSnapshot(options.snapscreenHost, options.snapscreenServerUrl, port);
                ShowLoadingTips();
                setTimeout(function () {
                    onSuccess(ret);
                }, 10)
            } catch (e) {
                ShowPluginTips();
            }
        });
    }
})(jQuery);


var ShowPluginTips = function () {
    if ($("#JS_SnapBox").length == 0) {
        $("<div class=\"SnapScreen_Box\" id=\"JS_SnapBox\"><div class=\"SnapScreen_Dialog\"><span>截图</span><a class=\"SnapScreen_Close\" href=\"javascript:void(0)\" onclick=\"ClosePluginTips()\">关闭</a></div><div class=\"SnapScreen_Content\"><h2>使用截图功能需要首先安装截图插件！</h2><dl><dt><a target=\"_blank\" href=\"http://attachment.tasktoday.com/Snapscreen.exe\">点此下载</a></dt><dd>第一步，下载截图插件并运行安装。</dd><dd>第二步，插件安装完成后即可使用，如不生效，请刷新页面后再试！</dd></dl></div></div>").appendTo($("body"));
    }
    $("#JS_SnapBox").show();
    if ($("#JS_SnapBg").length == 0) {
        $("<div class=\"SnapScreen_Bg\" id=\"JS_SnapBg\"></div>").appendTo($("body"));
    }
    $("#JS_SnapBg").show();
};
var ClosePluginTips = function () {
    $("#JS_SnapBox").hide();
    $("#JS_SnapBg").hide();
};