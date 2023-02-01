export function createElement(tag, props, ...children) {
    function addChild(elem, child) {
        elem.appendChild(child instanceof Node ? child : document.createTextNode(child.toString()));
    }
    if (typeof tag === "function") {
        return Object.assign(new tag(), { props: props || {} });
    }
    const elem = Object.assign(document.createElement(tag), props || {});
    children.forEach(child => Array.isArray(child) ? child.forEach(c => addChild(elem, c)) : addChild(elem, child));
    return elem;
}
