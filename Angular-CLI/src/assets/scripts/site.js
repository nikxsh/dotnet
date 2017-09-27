!function (a, b, c) {
    function d(b, c) {
        this.element = a(b), this.settings = a.extend({}, f, c), this._defaults = f, this._name = e, this.init()
    }
    var e = "metisMenu",
        f = {
            toggle: !0,
            doubleTapToGo: !1
        };
    d.prototype = {
        init: function () {
            var b = this.element,
                d = this.settings.toggle,
                f = this;
            this.isIE() <= 9 ? (b.find("li.active").has("ul").children("ul").collapse("show"), b.find("li").not(".active").has("ul").children("ul").collapse("hide")) : (b.find("li.active").has("ul").children("ul").addClass("collapse in"), b.find("li").not(".active").has("ul").children("ul").addClass("collapse")), f.settings.doubleTapToGo && b.find("li.active").has("ul").children("a").addClass("doubleTapToGo"), b.find("li").has("ul").children("a").on("click." + e, function (b) {
                return b.preventDefault(), f.settings.doubleTapToGo && f.doubleTapToGo(a(this)) && "#" !== a(this).attr("href") && "" !== a(this).attr("href") ? (b.stopPropagation(), void (c.location = a(this).attr("href"))) : (a(this).parent("li").toggleClass("active").children("ul").collapse("toggle"), void (d && a(this).parent("li").siblings().removeClass("active").children("ul.in").collapse("hide")))
            })
        },
        isIE: function () {
            for (var a, b = 3, d = c.createElement("div"), e = d.getElementsByTagName("i"); d.innerHTML = "<!--[if gt IE " + ++b + "]><i></i><![endif]-->", e[0];) return b > 4 ? b : a
        },
        doubleTapToGo: function (a) {
            var b = this.element;
            return a.hasClass("doubleTapToGo") ? (a.removeClass("doubleTapToGo"), !0) : a.parent().children("ul").length ? (b.find(".doubleTapToGo").removeClass("doubleTapToGo"), a.addClass("doubleTapToGo"), !1) : void 0
        },
        remove: function () {
            this.element.off("." + e), this.element.removeData(e)
        }
    }, a.fn[e] = function (b) {
        return this.each(function () {
            var c = a(this);
            c.data(e) && c.data(e).remove(), c.data(e, new d(this, b))
        }), this
    }
}(jQuery, window, document);

+function ($) {
    $("#menu").metisMenu();

    $("#sidebar-menu").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("active");
    });   

    $("#menu li a").click(function (e) {
        e.preventDefault();
        var current = $(this); 
        current.parent().siblings().find("i").find("i").removeClass("fa-angle-up");  
        current.parent().siblings().find("i").find("i").addClass("fa-angle-down");                      
        current.find("i").find("i").toggleClass("fa-angle-down", "fa-angle-up");        
        current.find("i").find("i").toggleClass("fa-angle-up", "fa-angle-down");  
    });
}(jQuery);

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
+function ($) {
    $(window).bind("load resize", function () {
        var topOffset = 50;
        var width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            //$('div.navbar-collapse').addClass('collapse');
            $("#sidebar-wrapper").css("margin-top", "50px");
            $("#wrapper").toggleClass("active");
            topOffset = 100; // 2-row-menu
        } else {
            //$('div.navbar-collapse').removeClass('collapse');
            $("#sidebar-wrapper").css("margin-top", "");
        }

        var height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

    var url = window.location;
    // var element = $('ul.nav a').filter(function() {
    //     return this.href == url;
    // }).addClass('active').parent().parent().addClass('in').parent();
    var element = $('ul.nav a').filter(function () {
        return this.href == url;
    }).addClass('active').parent();

    while (true) {
        if (element.is('li')) {
            element = element.parent().addClass('in').parent();
        } else {
            break;
        }
    }
}(jQuery);