// jQuery extension functions
(function ($) {
    // General function that signifies the element in scope is eligible to be showing a scrollbar
    $.fn.hasScrollBar = function () {
        return this.get(0).scrollHeight > Math.ceil(this.height());
    }
})(jQuery);
