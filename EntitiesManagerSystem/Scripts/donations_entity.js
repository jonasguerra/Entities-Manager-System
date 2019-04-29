$(document).ready(function () {
    start_pagination();

    ellipsis($('.post-box .post-text'), 200);
})

function start_pagination() {
    var items = $(".post-box");
    var numItems = items.length;
    var perPage = 2;
    items.slice(perPage).hide();
    if(items.length > perPage){
        $('#pagination-container').show()
    }else{
        $('#pagination-container').hide()
    }
    $('#pagination-container').pagination({
        items : numItems,
        itemsOnPage : perPage,
        prevText : "&laquo;",
        nextText : "&raquo;",
        onPageClick : function(pageNumber) {
            var showFrom = perPage * (pageNumber - 1);
            var showTo = showFrom + perPage;
            items.hide().slice(showFrom, showTo).show();
        }
    });
}


function ellipsis(elem, max) {
    elem.each(function() {
        var textLength = $(this).text().length;
        var text = $(this).text();
        if (textLength > max) {
            $(this).text(text.substr(0, max - 3) + '…');
        };
    });
};