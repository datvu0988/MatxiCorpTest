
//Upload photos
$("#btnUploadPhotos").click(function () {
    $(this).closest('form').find('input[id^=photoUpload]').click();
})

function uploadPhotosAndCreatEmptyArtworks() {
    $("#formDetailRoom").submit();
}

//Init sortable js
var sortable = Sortable.create(artworkSelections, {
    handle: '.selection',
    animation: 150,
    onUpdate: function () {
        let artworks = [];
        let orders = sortable.toArray();
        if (orders.length > 0) {
            for (let i = 0; i < orders.length; i++) {
                artworks.push({ id: orders[i], displayOrder: i })
            }
            saveOrderArtworks(artworks);
        }
    },

});

//Save order artworks
function saveOrderArtworks(artworks) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        url: '/Artwork/SaveOrderArtworks',
        data: JSON.stringify(artworks),
        success: function () {
            console.log('order successfully');
        },
        failure: function (response) {
            console.log('order failed');
        }
    });
}