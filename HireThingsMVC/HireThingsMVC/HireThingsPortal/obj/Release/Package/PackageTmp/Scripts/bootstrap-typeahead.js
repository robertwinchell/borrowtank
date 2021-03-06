﻿//!function (a, b) { "use strict"; "undefined" != typeof module && module.exports ? module.exports = b(require("jquery")) : "function" == typeof define && define.amd ? define(["jquery"], function (a) { return b(a) }) : b(a.jQuery) }(this, function (a) { "use strict"; var b = function (b, c) { this.$element = a(b), this.options = a.extend({}, a.fn.typeahead.defaults, c), this.matcher = this.options.matcher || this.matcher, this.sorter = this.options.sorter || this.sorter, this.select = this.options.select || this.select, this.autoSelect = "boolean" == typeof this.options.autoSelect ? this.options.autoSelect : !0, this.highlighter = this.options.highlighter || this.highlighter, this.render = this.options.render || this.render, this.updater = this.options.updater || this.updater, this.displayText = this.options.displayText || this.displayText, this.source = this.options.source, this.delay = this.options.delay, this.$menu = a(this.options.menu), this.$appendTo = this.options.appendTo ? a(this.options.appendTo) : null, this.shown = !1, this.listen(), this.showHintOnFocus = "boolean" == typeof this.options.showHintOnFocus ? this.options.showHintOnFocus : !1, this.afterSelect = this.options.afterSelect, this.addItem = !1 }; b.prototype = { constructor: b, select: function () { var a = this.$menu.find(".active").data("value"); if (this.$element.data("active", a), this.autoSelect || a) { var b = this.updater(a); this.$element.val(this.displayText(b) || b).change(), this.afterSelect(b) } return this.hide() }, updater: function (a) { return a }, setSource: function (a) { this.source = a }, show: function () { var b, c = a.extend({}, this.$element.position(), { height: this.$element[0].offsetHeight }); return b = "function" == typeof this.options.scrollHeight ? this.options.scrollHeight.call() : this.options.scrollHeight, (this.$appendTo ? this.$menu.appendTo(this.$appendTo) : this.$menu.insertAfter(this.$element)).css({ top: c.top + c.height + b, left: c.left }).show(), this.shown = !0, this }, hide: function () { return this.$menu.hide(), this.shown = !1, this }, lookup: function (b) { if (this.query = "undefined" != typeof b && null !== b ? b : this.$element.val() || "", this.query.length < this.options.minLength) return this.shown ? this.hide() : this; var c = a.proxy(function () { a.isFunction(this.source) ? this.source(this.query, a.proxy(this.process, this)) : this.source && this.process(this.source) }, this); clearTimeout(this.lookupWorker), this.lookupWorker = setTimeout(c, this.delay) }, process: function (b) { var c = this; return b = a.grep(b, function (a) { return c.matcher(a) }), b = this.sorter(b), b.length || this.options.addItem ? (b.length > 0 ? this.$element.data("active", b[0]) : this.$element.data("active", null), this.options.addItem && b.push(this.options.addItem), "all" == this.options.items ? this.render(b).show() : this.render(b.slice(0, this.options.items)).show()) : this.shown ? this.hide() : this }, matcher: function (a) { var b = this.displayText(a); return ~b.toLowerCase().indexOf(this.query.toLowerCase()) }, sorter: function (a) { for (var b, c = [], d = [], e = []; b = a.shift() ;) { var f = this.displayText(b); f.toLowerCase().indexOf(this.query.toLowerCase()) ? ~f.indexOf(this.query) ? d.push(b) : e.push(b) : c.push(b) } return c.concat(d, e) }, highlighter: function (b) { var c, d, e, f, g, h = a("<div></div>"), i = this.query, j = b.toLowerCase().indexOf(i.toLowerCase()); if (c = i.length, 0 === c) return h.text(b).html(); for (; j > -1;) d = b.substr(0, j), e = b.substr(j, c), f = b.substr(j + c), g = a("<strong></strong>").text(e), h.append(document.createTextNode(d)).append(g), b = f, j = b.toLowerCase().indexOf(i.toLowerCase()); return h.append(document.createTextNode(b)).html() }, render: function (b) { var c = this, d = this, e = !1; return b = a(b).map(function (b, f) { var g = d.displayText(f); return b = a(c.options.item).data("value", f), b.find("a").html(c.highlighter(g)), g == d.$element.val() && (b.addClass("active"), d.$element.data("active", f), e = !0), b[0] }), this.autoSelect && !e && (b.first().addClass("active"), this.$element.data("active", b.first().data("value"))), this.$menu.html(b), this }, displayText: function (a) { return a.name || a }, next: function (b) { var c = this.$menu.find(".active").removeClass("active"), d = c.next(); d.length || (d = a(this.$menu.find("li")[0])), d.addClass("active") }, prev: function (a) { var b = this.$menu.find(".active").removeClass("active"), c = b.prev(); c.length || (c = this.$menu.find("li").last()), c.addClass("active") }, listen: function () { this.$element.on("focus", a.proxy(this.focus, this)).on("blur", a.proxy(this.blur, this)).on("keypress", a.proxy(this.keypress, this)).on("keyup", a.proxy(this.keyup, this)), this.eventSupported("keydown") && this.$element.on("keydown", a.proxy(this.keydown, this)), this.$menu.on("click", a.proxy(this.click, this)).on("mouseenter", "li", a.proxy(this.mouseenter, this)).on("mouseleave", "li", a.proxy(this.mouseleave, this)) }, destroy: function () { this.$element.data("typeahead", null), this.$element.data("active", null), this.$element.off("focus").off("blur").off("keypress").off("keyup"), this.eventSupported("keydown") && this.$element.off("keydown"), this.$menu.remove() }, eventSupported: function (a) { var b = a in this.$element; return b || (this.$element.setAttribute(a, "return;"), b = "function" == typeof this.$element[a]), b }, move: function (a) { if (this.shown) { switch (a.keyCode) { case 9: case 13: case 27: a.preventDefault(); break; case 38: if (a.shiftKey) return; a.preventDefault(), this.prev(); break; case 40: if (a.shiftKey) return; a.preventDefault(), this.next() } a.stopPropagation() } }, keydown: function (b) { this.suppressKeyPressRepeat = ~a.inArray(b.keyCode, [40, 38, 9, 13, 27]), this.shown || 40 != b.keyCode ? this.move(b) : this.lookup() }, keypress: function (a) { this.suppressKeyPressRepeat || this.move(a) }, keyup: function (a) { switch (a.keyCode) { case 40: case 38: case 16: case 17: case 18: break; case 9: case 13: if (!this.shown) return; this.select(); break; case 27: if (!this.shown) return; this.hide(); break; default: this.lookup() } a.stopPropagation(), a.preventDefault() }, focus: function (a) { this.focused || (this.focused = !0, this.options.showHintOnFocus && this.lookup("")) }, blur: function (a) { this.focused = !1, !this.mousedover && this.shown && this.hide() }, click: function (a) { a.stopPropagation(), a.preventDefault(), this.select(), this.$element.focus() }, mouseenter: function (b) { this.mousedover = !0, this.$menu.find(".active").removeClass("active"), a(b.currentTarget).addClass("active") }, mouseleave: function (a) { this.mousedover = !1, !this.focused && this.shown && this.hide() } }; var c = a.fn.typeahead; a.fn.typeahead = function (c) { var d = arguments; return "string" == typeof c && "getActive" == c ? this.data("active") : this.each(function () { var e = a(this), f = e.data("typeahead"), g = "object" == typeof c && c; f || e.data("typeahead", f = new b(this, g)), "string" == typeof c && (d.length > 1 ? f[c].apply(f, Array.prototype.slice.call(d, 1)) : f[c]()) }) }, a.fn.typeahead.defaults = { source: [], items: 8, menu: '<ul class="typeahead dropdown-menu" role="listbox"></ul>', item: '<li><a href="#" role="option"></a></li>', minLength: 1, scrollHeight: 0, autoSelect: !0, afterSelect: a.noop, addItem: !1, delay: 0 }, a.fn.typeahead.Constructor = b, a.fn.typeahead.noConflict = function () { return a.fn.typeahead = c, this }, a(document).on("focus.typeahead.data-api", '[data-provide="typeahead"]', function (b) { var c = a(this); c.data("typeahead") || c.typeahead(c.data()) }) });


/* =============================================================
 * bootstrap3-typeahead.js v3.1.0
 * https://github.com/bassjobsen/Bootstrap-3-Typeahead
 * =============================================================
 * Original written by @mdo and @fat
 * =============================================================
 * Copyright 2014 Bass Jobsen @bassjobsen
 *
 * Licensed under the Apache License, Version 2.0 (the 'License');
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an 'AS IS' BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ============================================================ */


(function (root, factory) {

    'use strict';

    // CommonJS module is defined
    if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory(require('jquery'));
    }
        // AMD module is defined
    else if (typeof define === 'function' && define.amd) {
        define(['jquery'], function ($) {
            return factory($);
        });
    } else {
        factory(root.jQuery);
    }

}(this, function ($) {

    'use strict';
    // jshint laxcomma: true


    /* TYPEAHEAD PUBLIC CLASS DEFINITION
     * ================================= */

    var Typeahead = function (element, options) {
        this.$element = $(element);
        this.options = $.extend({}, $.fn.typeahead.defaults, options);
        this.matcher = this.options.matcher || this.matcher;
        this.sorter = this.options.sorter || this.sorter;
        this.select = this.options.select || this.select;
        this.autoSelect = typeof this.options.autoSelect == 'boolean' ? this.options.autoSelect : true;
        this.highlighter = this.options.highlighter || this.highlighter;
        this.render = this.options.render || this.render;
        this.updater = this.options.updater || this.updater;
        this.displayText = this.options.displayText || this.displayText;
        this.source = this.options.source;
        this.delay = this.options.delay;
        this.$menu = $(this.options.menu);
        this.$appendTo = this.options.appendTo ? $(this.options.appendTo) : null;
        this.shown = false;
        this.listen();
        this.showHintOnFocus = typeof this.options.showHintOnFocus == 'boolean' ? this.options.showHintOnFocus : false;
        this.afterSelect = this.options.afterSelect;
        this.addItem = false;
    };

    Typeahead.prototype = {

        constructor: Typeahead,

        select: function () {
            var val = this.$menu.find('.active').data('value');
            this.$element.data('active', val);
            if (this.autoSelect || val) {
                var newVal = this.updater(val);
                this.$element
                  .val(this.displayText(newVal) || newVal)
                  .change();
                this.afterSelect(newVal);
            }
            return this.hide();
        },

        updater: function (item) {
            return item;
        },

        setSource: function (source) {
            this.source = source;
        },

        show: function () {
            var pos = $.extend({}, this.$element.position(), {
                height: this.$element[0].offsetHeight
            }), scrollHeight;

            scrollHeight = typeof this.options.scrollHeight == 'function' ?
                this.options.scrollHeight.call() :
                this.options.scrollHeight;

            (this.$appendTo ? this.$menu.appendTo(this.$appendTo) : this.$menu.insertAfter(this.$element))
              .css({
                  top: pos.top + pos.height + scrollHeight
              , left: pos.left
              })
              .show();

            this.shown = true;
            return this;
        },

        hide: function () {
            this.$menu.hide();
            this.shown = false;
            return this;
        },

        lookup: function (query) {
            var items;
            if (typeof (query) != 'undefined' && query !== null) {
                this.query = query;
            } else {
                this.query = this.$element.val() || '';
            }

            if (this.query.length < this.options.minLength) {
                return this.shown ? this.hide() : this;
            }

            var worker = $.proxy(function () {

                if ($.isFunction(this.source)) this.source(this.query, $.proxy(this.process, this));
                else if (this.source) {
                    this.process(this.source);
                }
            }, this);

            clearTimeout(this.lookupWorker);
            this.lookupWorker = setTimeout(worker, this.delay);
        },

        process: function (items) {
            var that = this;

            items = $.grep(items, function (item) {
                return that.matcher(item);
            });

            items = this.sorter(items);

            if (!items.length && !this.options.addItem) {
                return this.shown ? this.hide() : this;
            }

            if (items.length > 0) {
                this.$element.data('active', items[0]);
            } else {
                this.$element.data('active', null);
            }

            // Add item
            if (this.options.addItem) {
                items.push(this.options.addItem);
            }

            if (this.options.items == 'all') {
                return this.render(items).show();
            } else {
                return this.render(items.slice(0, this.options.items)).show();
            }
        },

        matcher: function (item) {
            var it = this.displayText(item);
            return ~it.toLowerCase().indexOf(this.query.toLowerCase());
        },

        sorter: function (items) {
            var beginswith = []
              , caseSensitive = []
              , caseInsensitive = []
              , item;

            while ((item = items.shift())) {
                var it = this.displayText(item);
                if (!it.toLowerCase().indexOf(this.query.toLowerCase())) beginswith.push(item);
                else if (~it.indexOf(this.query)) caseSensitive.push(item);
                else caseInsensitive.push(item);
            }

            return beginswith.concat(caseSensitive, caseInsensitive);
        },

        highlighter: function (item) {
            var html = $('<div></div>');
            var query = this.query;
            var i = item.toLowerCase().indexOf(query.toLowerCase());
            var len, leftPart, middlePart, rightPart, strong;
            len = query.length;
            if (len === 0) {
                return html.text(item).html();
            }
            while (i > -1) {
                leftPart = item.substr(0, i);
                middlePart = item.substr(i, len);
                rightPart = item.substr(i + len);
                strong = $('<strong></strong>').text(middlePart);
                html
                    .append(document.createTextNode(leftPart))
                    .append(strong);
                item = rightPart;
                i = item.toLowerCase().indexOf(query.toLowerCase());
            }
            return html.append(document.createTextNode(item)).html();
        },

        render: function (items) {
            var that = this;
            var self = this;
            var activeFound = false;
            items = $(items).map(function (i, item) {
                var text = self.displayText(item);
                i = $(that.options.item).data('value', item);
                i.find('a').html(that.highlighter(text));
                if (text == self.$element.val()) {
                    i.addClass('active');
                    self.$element.data('active', item);
                    activeFound = true;
                }
                return i[0];
            });

            if (this.autoSelect && !activeFound) {
                items.first().addClass('active');
                this.$element.data('active', items.first().data('value'));
            }
            this.$menu.html(items);
            return this;
        },

        displayText: function (item) {
          //awais added this line  
          //  if (item != undefined)
                //awais added this line  
            return item.name || item;
        },

        next: function (event) {
            var active = this.$menu.find('.active').removeClass('active')
              , next = active.next();

            if (!next.length) {
                next = $(this.$menu.find('li')[0]);
            }

            next.addClass('active');
        },

        prev: function (event) {
            var active = this.$menu.find('.active').removeClass('active')
              , prev = active.prev();

            if (!prev.length) {
                prev = this.$menu.find('li').last();
            }

            prev.addClass('active');
        },

        listen: function () {
            this.$element
              .on('focus', $.proxy(this.focus, this))
              .on('blur', $.proxy(this.blur, this))
              .on('keypress', $.proxy(this.keypress, this))
              .on('keyup', $.proxy(this.keyup, this));

            if (this.eventSupported('keydown')) {
                this.$element.on('keydown', $.proxy(this.keydown, this));
            }

            this.$menu
              .on('click', $.proxy(this.click, this))
              .on('mouseenter', 'li', $.proxy(this.mouseenter, this))
              .on('mouseleave', 'li', $.proxy(this.mouseleave, this));
        },

        destroy: function () {
            this.$element.data('typeahead', null);
            this.$element.data('active', null);
            this.$element
              .off('focus')
              .off('blur')
              .off('keypress')
              .off('keyup');

            if (this.eventSupported('keydown')) {
                this.$element.off('keydown');
            }

            this.$menu.remove();
        },

        eventSupported: function (eventName) {
            var isSupported = eventName in this.$element;
            if (!isSupported) {
                this.$element.setAttribute(eventName, 'return;');
                isSupported = typeof this.$element[eventName] === 'function';
            }
            return isSupported;
        },

        move: function (e) {
            if (!this.shown) return;

            switch (e.keyCode) {
                case 9: // tab
                case 13: // enter
                case 27: // escape
                    e.preventDefault();
                    break;

                case 38: // up arrow
                    // with the shiftKey (this is actually the left parenthesis)
                    if (e.shiftKey) return;
                    e.preventDefault();
                    this.prev();
                    break;

                case 40: // down arrow
                    // with the shiftKey (this is actually the right parenthesis)
                    if (e.shiftKey) return;
                    e.preventDefault();
                    this.next();
                    break;
            }
        },

        keydown: function (e) {
            this.suppressKeyPressRepeat = ~$.inArray(e.keyCode, [40, 38, 9, 13, 27]);
            if (!this.shown && e.keyCode == 40) {
                this.lookup();
            } else {
                this.move(e);
            }
        },

        keypress: function (e) {
            if (this.suppressKeyPressRepeat) return;
            this.move(e);
        },

        keyup: function (e) {
            switch (e.keyCode) {
                case 40: // down arrow
                case 38: // up arrow
                case 16: // shift
                case 17: // ctrl
                case 18: // alt
                    break;

                case 9: // tab
                case 13: // enter
                    if (!this.shown) return;
                    this.select();
                    break;

                case 27: // escape
                    if (!this.shown) return;
                    this.hide();
                    break;
                default:
                    this.lookup();
            }

            e.preventDefault();
        },

        focus: function (e) {
            if (!this.focused) {
                this.focused = true;
                if (this.options.showHintOnFocus) {
                    this.lookup('');
                }
            }
        },

        blur: function (e) {
            this.focused = false;
            if (!this.mousedover && this.shown) this.hide();
        },

        click: function (e) {
            e.preventDefault();
            this.select();
            this.$element.focus();
        },

        mouseenter: function (e) {
            this.mousedover = true;
            this.$menu.find('.active').removeClass('active');
            $(e.currentTarget).addClass('active');
        },

        mouseleave: function (e) {
            this.mousedover = false;
            if (!this.focused && this.shown) this.hide();
        }

    };


    /* TYPEAHEAD PLUGIN DEFINITION
     * =========================== */

    var old = $.fn.typeahead;

    $.fn.typeahead = function (option) {
        var arg = arguments;
        if (typeof option == 'string' && option == 'getActive') {
            return this.data('active');
        }
        return this.each(function () {
            var $this = $(this)
              , data = $this.data('typeahead')
              , options = typeof option == 'object' && option;
            if (!data) $this.data('typeahead', (data = new Typeahead(this, options)));
            if (typeof option == 'string') {
                if (arg.length > 1) {
                    data[option].apply(data, Array.prototype.slice.call(arg, 1));
                } else {
                    data[option]();
                }
            }
        });
    };

    $.fn.typeahead.defaults = {
        source: []
    , items: 8
    , menu: '<ul class="typeahead dropdown-menu" role="listbox"></ul>'
    , item: '<li><a href="#" role="option"></a></li>'
    , minLength: 1
    , scrollHeight: 0
    , autoSelect: true
    , afterSelect: $.noop
    , addItem: false
    , delay: 0
    };

    $.fn.typeahead.Constructor = Typeahead;


    /* TYPEAHEAD NO CONFLICT
     * =================== */

    $.fn.typeahead.noConflict = function () {
        $.fn.typeahead = old;
        return this;
    };


    /* TYPEAHEAD DATA-API
     * ================== */

    $(document).on('focus.typeahead.data-api', '[data-provide="typeahead"]', function (e) {
        var $this = $(this);
        if ($this.data('typeahead')) return;
        $this.typeahead($this.data());
    });

}));
