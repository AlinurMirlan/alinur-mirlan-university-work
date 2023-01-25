// export keyword lets the objects to be used outside its module.
export function calculateTax(price) {
    return Number(price) * 1.2;
}

// A default function doesn't have to specify a name, it's the importer
// of the module that has to.
export default function calcTaxAndSum(...prices) {
    return prices.reduce((total, p) => total += calculateTax(p), 0);
}


