// Tabs
jQuery(document).ready(function($) {

    $('.tab').click(function(){
        if(!$(this).hasClass('active')) {
            $('.tab').removeClass('active');
            $(this).addClass('active');

            var $panel = $(this).data('tab');
            var id = '#';
            var $panel_id = id.concat($panel);
            
            $('.tabbed-table').removeClass('active');
            $($panel_id).addClass('active');
        }
    });

});