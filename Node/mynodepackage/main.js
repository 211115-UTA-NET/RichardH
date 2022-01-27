var data = {
    name: '123',
    convert: function () { return '-123'; }
};
function output(something) {
    return something.convert().length;
}
var result = output(data);
window.onload = function () {
    document.body.innerHTML = "From TS: " + result;
};
