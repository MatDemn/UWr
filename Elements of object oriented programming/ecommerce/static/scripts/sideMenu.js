window.addEventListener('load', function() {
    var bt = document.getElementById('menuOpenImg');
    bt.addEventListener('click', function() {
        var menu = document.getElementsByClassName('container')[0];
        if(menu.id == "") {
            menu.setAttribute("id", "shown");
        }
        else {
            menu.setAttribute("id", "");
        }
    });
});