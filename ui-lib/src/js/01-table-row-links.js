// Table Row Links (only allow a single click, as navigation is triggered after the click - prevents dup operations)
jQuery(document).ready(function($) {    
    $('.row-link').one('click', function() {
        window.location = $(this).data('href');
    });
});