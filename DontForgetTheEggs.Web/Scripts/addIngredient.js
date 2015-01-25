$(function() {
    $(".select-ingredient-js").click(function (e) {
        e.preventDefault();
        var $element = $(this);
        var id = $element.data("id");
        var name = $element.data("name");

        console.log($element);
        console.log(id);
        console.log(name);

        $("#ingredientName").val(name);
        $("#IngredientId").val(id);
        document.location.hash = '#selectQuantity';
    });
})