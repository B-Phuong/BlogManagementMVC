function ShowImg(ImageFile, previewImg) {
    if (ImageFile.files && ImageFile.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImg).attr('src', e.target.result);
        }
        // doc duong dan cua du lieu
        reader.readAsDataURL(ImageFile.files[0]);
    }
}