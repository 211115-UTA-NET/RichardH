interface Data {
    name: StringConstructor;
    convert: () => string;
}

let data: any = {
    name: '123',
    convert: () => '-123'
};

function output(something: Data): number {
    return something.convert().length;
}

let result = output(data);

window.onload = () => {
document.body.innerHTML = "From TS: " +  result;
}