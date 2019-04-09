function changePhoto(thisFile) {
    var filepath = thisFile.value;
    var removeButton = $(thisFile).parent(".attachment").find(".attachment--remove");

    // Check file size limit
    if ($(thisFile)[0].files[0].size > 10485760) { //10MB
        $(thisFile).parent(".attachment").addClass("has-error");
        $(thisFile).siblings(".attachment--error").html("O arquivo é muito grande. Por favor, envie um arquivo com menos de 10 MB.");
        $(thisFile).value = "";
    } else {
        $(thisFile).parent(".attachment").removeClass("has-error");
        appendFile(filepath, $(thisFile));
        $(thisFile).siblings(".attachment--error").html("");

        removeButton.on("click", function() {
            removeFile($(thisFile));
        });
    }
};

// Append file path
function appendFile(filepath, thisFile) {
    var m = filepath.match(/([^\/\\]+)$/);
    var filename = m[1];
    thisFile.parent(".attachment").find(".file-name").html(filename);
    thisFile.parent(".attachment").addClass("is-attached");
}

// Remove file function
function removeFile(thisFile) {
    thisFile.val("");
    thisFile.parent(".attachment").removeClass("is-attached");
}

// Drag and drop functionality
$(".attachment").on("drag dragstart dragend dragover dragenter dragleave drop", function(e) {
    e.preventDefault();
    e.stopPropagation();
}).on("dragover dragenter", function() {
    $(this).addClass("is-dragover");
}).on("dragleave dragend drop", function() {
    $(this).removeClass("is-dragover");
});