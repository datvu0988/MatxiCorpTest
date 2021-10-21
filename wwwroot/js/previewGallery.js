
$(document).ready(function () {
    let selectedRoomId = $("#selectedRoomId").val();
    getPhotos(selectedRoomId);
});

//get photos
function getPhotos(roomId) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        url: '/Galleries/GetPhotos?roomId=' + roomId,
        dataType: 'json',
        success: function (response) {
            //empty carousel
            $('#carouselPhotos .carousel-inner').html("");
            //add photos to carousel
            $.each(response, function (key, element) {
                let photoName = "";
                let photoDescription = "";
                let photoPrice = "";
                if (element.name) {
                    photoName = element.name;
                }
                if (element.description) {
                    let description = element.description;
                    photoDescription = description.length > 500 ? description.substring(0, 500) + '...' : description;
                }
                if (element.pirce) {
                    photoPrice = element.pirce + " $";
                }
                let photoIconHtml = '<i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" data-html="true" title=\'<div class="row no-gutters"><div class="col-md-12 tooltip-row">' + photoDescription +
                    '</div><div class="col-md-12 tooltip-row">' + photoPrice +
                    '</div></div>\'></i>'
                if (element.isActivePhoto)
                    $('#carouselPhotos .carousel-inner').append($('<div class="carousel-item item active"><img class="d-block carousel-photo" src=' + element.photoUrl + '><div class="photo-name">' + photoIconHtml + '<p>' + photoName + '</p></div></div>'));
                else
                    $('#carouselPhotos .carousel-inner').append($('<div class="carousel-item item"><img class="d-block carousel-photo" src=' + element.photoUrl + '><div class="photo-name">' + photoIconHtml + '<p>' + photoName + '</p></div></div>'));
            });
            $('[data-toggle="tooltip"]').tooltip();
        },
        failure: function (response) {
            console.log('get photos failed');
        }
    });
}

//bind event when click thumbnail
$("body").on("click", "img.thumbnail", function () {

    //remove and add selected class 
    $("#carouselThumbnail img.thumbnail.selected").each(function () {
        let oldParagraph = $(this).closest("div").find("p");
        let roomName = oldParagraph.text();
        oldParagraph.remove();
        $(this).removeClass("selected");
        $(this).closest("div").append($('<p class="image-title-cell">' + roomName + '</p>'));
    });
    $(this).addClass("selected");

    //re-style the photo name
    let oldParagraph = $(this).closest("div").find("p");
    let roomName = oldParagraph.text();
    oldParagraph.remove();
    $(this).closest("div").prepend($('<p class="image-title-cell large-text mb-3">' + roomName + '</p>'))

    //update selected room and get room photos
    let roomId = $(this).closest("div").find("input[type=hidden]").val();
    $("#selectedRoomId").val(roomId);
    getPhotos(roomId);
})

