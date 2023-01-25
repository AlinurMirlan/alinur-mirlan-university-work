import { Person, City, Product, Employee } from "./dataTypes";


// class Collection<T extends ShapeType> {
//     private items: Set<T>;
    
//     constructor(initialItems: T[] = []) {
//         this.items = new Set(initialItems);
//     }

//     add(...newItems: T[]): void {
//         newItems.forEach(item => this.items.add(item));
//     }

//     get(name: string): T | undefined {
//         return [...this.items.values()].find(item => item.name === name);
//     }

//     get count(): number {
//         return this.items.size;
//     }
// }

// let productionCollection: Collection<Product> = new Collection(products);
// let product: Product | undefined = productionCollection.get("Hat");
// console.log(product?.price ?? 0);

// class Collection<T extends ShapeType> {
//     private items: Map<string, T>;
//     constructor(initialItems: T[] = []) {
//         this.items = new Map();
//         this.add(...initialItems);
//     }
//     add(...newItems: T[]): void {
//         newItems.forEach(item => this.items.set(item.name, item));
//     }
//     get(name: string): T | undefined {
//         return this.items.get(name);
//     }
//     get count(): number {
//         return this.items.size;
//     }
//     // Iterator cannot be traversed directly. So, you cannot use
//     // for of construct or spread operator with it. Specify Iterable instead.
//     values(): Iterator<T> {
//         return this.items.values();
//     }
// }

// let productionCollection: Collection<Product> = new Collection(products);
// let iterator = productionCollection.values();
// let result = iterator.next();
// while (!result.done) {
//     let product: Product = result.value;
//     console.log(`${product.name} costs ${product.price}`);
//     result = iterator.next();
// }


// class Collection<T extends ShapeType> {
//     private items: Map<string, T>;
//     constructor(initialItems: T[] = []) {
//         this.items = new Map();
//         this.add(...initialItems);
//     }
//     add(...newItems: T[]): void {
//         newItems.forEach(item => this.items.set(item.name, item));
//     }
//     get(name: string): T | undefined {
//         return this.items.get(name);
//     }
//     get count(): number {
//         return this.items.size;
//     }
//     // IterableIterator combines Iterator and Iterable
//     // interfaces, so you may traverse it directly, or use
//     // the iterator with its next() method.
//     values(): IterableIterator<T> {
//         return this.items.values();
//     }
// }

// let set = new Set<number>([1, 2, 3]);
// // You can iterate built-in collections directly, since they implement
// // Iterable interface.
// for (const num of set) {
//     console.log(`Numeral: ${num}`);
// }
// let productionCollection: Collection<Product> = new Collection(products);
// let iterator = productionCollection.values();
// let result = iterator.next();
// while (!result.done) {
//     let product: Product = result.value;
//     console.log(`${product.name} costs ${product.price}`);
//     result = iterator.next();
// }
// // Concise syntax
// for (const product of iterator) {
//     console.log(`${product.name} costs ${product.price}`);
// }



// class Collection<T extends ShapeType> implements Iterable<T> {
//     private items: Map<string, T>;
//     constructor(initialItems: T[] = []) {
//         this.items = new Map();
//         this.add(...initialItems);
//     }
//     // A generator method.
//     *[Symbol.iterator](): Iterator<T> {
//         for (const product of this.items.values()) {
//             yield product;
//         }
//     }
//     // Lazy approach :D
//     // [Symbol.iterator](): Iterator<T> {
//     //     return this.items.values();
//     // }
//     add(...newItems: T[]): void {
//         newItems.forEach(item => this.items.set(item.name, item));
//     }
//     get(name: string): T | undefined {
//         return this.items.get(name);
//     }
//     get count(): number {
//         return this.items.size;
//     }
//     values(): IterableIterator<T> {
//         return this.items.values();
//     }
// }

// let productionCollection: Collection<Product> = new Collection(products);
// for (const product of productionCollection) {
//     console.log(`${product.name} costs ${product.price}`);
// }

// myVar: "name" | "price"
// let myVar: keyof Product = "name";
// myVar = "price";





// myVar = "city";

// function getValue<T, K extends keyof T>(item: T, keyname: K) {
//     console.log(item[keyname]);
// }

// let product = new Product("Running Shoes", 100);
// getValue(product, "name");


// getValue<Product, "price">(product, "name");




// // priceType: string
// type priceType = Product["price"];
// // allTypes: string | number. { name: string, price: number }
// type allTypes = Product[keyof Product];

// function getValue<T, K extends keyof T>(item: T, keyname: K): T[K] {
//     return item[keyname];
// }

// let product = new Product("Running Shoes", 100);
// let productName: string = getValue(product, "name");
// let productPrice: number = getValue<Product, "price">(product, "price");
// console.log(`${productName}, ${productPrice}`);


// class Collection<T, K extends keyof T> implements Iterable<T> {
//     private items: Map<T[K], T>;
//     constructor(initialItems: T[] = [], private propertyName: K) {
//         this.items = new Map();
//         this.add(...initialItems);
//     }
//     add(...newItems: T[]): void {
//         // We can't use K to directly access the property of T, since generic type information
//         // will be removed once the TS code gets compiled. So, we encapsulate it in the propertyName prop.
//         newItems.forEach(newItem => this.items.set(newItem[this.propertyName], newItem));
//     }
//     get(key: T[K]): T | undefined {
//         return this.items.get(key);
//     }
//     get count(): number {
//         return this.items.size;
//     }
//     *[Symbol.iterator](): Iterator<T> {
//         for (const product of this.items.values()) {
//             yield product;
//         }
//     }
// }
// let productionCollectionByName: Collection<Product, "name"> = new Collection(products, "name");
// console.log(`There are ${productionCollectionByName.count} products`);
// // Storing items based on different key types. Here we have string type.
// let itemByKey: Product | undefined = productionCollectionByName.get("name");
// console.log(`Item: ${itemByKey?.name}, ${itemByKey?.price}`);
// let productionCollectionByPrice = new Collection(products, "price");
// // Boom! keys are now of type number.
// itemByKey = productionCollectionByPrice.get(100);
// console.log(`Item: ${itemByKey?.name}, ${itemByKey?.price}`);

// type MappedProduct = {
//     [P in keyof Product]: Product[P]
// };
// // Equivalent to:
// // type MappedProduct = {
// //     name: string,
// //     price: number
// // }

// type AllowStrings = {
//     [P in keyof Product]: Product[P] | string
// }
// // Equivalent to:
// // type AllowStrings = {
// //     name: string,
// //     price: number | string
// // }

// type ChangeNames = {
//     [P in keyof Product as `${P}Property`]: Product[P]
// }
// // Equivalent to:
// // type MappedProduct = {
// //     nameProperty: string,
// //     priceProperty: number
// // }

// let p: MappedProduct = { name: "Kayak", price: 275 };
// console.log(`Mapped type: ${p.name}, ${p.price}`);

// let q: AllowStrings = { name: "Kayak", price: "apples" };
// console.log(`Changed type #1: ${q.name}, ${q.price}`);

// let r: ChangeNames = { nameProperty: "Kayak", priceProperty: 275 };
// console.log(`Changed type #1: ${r.nameProperty}, ${r.priceProperty}`);

// type Mapped<T> = {
//     [P in keyof T]: T[P]
// };

// let p: Mapped<Product> = { name: "Kayak", price: 275 };
// console.log(`Mapped type: ${p.name}, ${p.price}`);

// let c: Mapped<City> = { name: "London", population: 8136000 };
// console.log(`Mapped type: ${c.name}, ${c.population}`);

// type MakeOptional<T> = {
//     [P in keyof T]? : T[P]
// };
// type MakeRequired<T> = {
//     [P in keyof T]-? : T[P]
// };
// type MakeReadonly<T> = {
//     readonly [P in keyof T]: T[P]
// };
// type MakeReadWrite<T> = {
//     -readonly [P in keyof T]: T[P]
// };

// // Chain of transformations.
// type optionalType = MakeOptional<Product>;
// type requiredType = MakeRequired<optionalType>;
// type readOnlyType = MakeReadonly<requiredType>;
// type readWriteType = MakeReadWrite<readOnlyType>;

// let p: readWriteType = { name: "Kayak", price: 275 };
// // You cannot use readonly modifier inside an object literal. It'll be automatically
// // assigned once the object is constructed.
// // let q: readOnlyType = { readonly name: "Kayak", readonly price: 2 };
// let q: readOnlyType = { name: "Kayak", price: 2 };

// type MakeReadWrite<T> = {
//     -readonly [P in keyof T]: T[P]
// };

// type MakeReadWrite<T> = {
//     -readonly [P in keyof T]: T[P]
// };

// // Using built-in mappings.
// type optionalType = Partial<Product>;
// type requiredType = Required<optionalType>;
// type readOnlyType = Readonly<requiredType>;
// type readWriteType = MakeReadWrite<readOnlyType>;

// let p: readWriteType = { name: "Kayak", price: 275 };
// // You cannot use readonly modifier inside an object literal. It'll be automatically
// // assigned once the object is constructed.
// // let q: readOnlyType = { readonly name: "Kayak", readonly price: 2 };
// let q: readOnlyType = { name: "Kayak", price: 2 };


// // Equivalent to built-in Pick<T, K>
// type SelectProperties<T, K extends keyof T> = {
//     [P in K]: T[P]
// };

// let p1: SelectProperties<Product, "name"> = { name: "Kayak" };
// let p2: Pick<Product, "name"> = { name: "Kayak" };
// let p3: Omit<Product, "price"> = { name: "Kayak" };
// console.log(`Custom mapped type: ${p1.name}`);
// console.log(`Built-in mapped type (Pick): ${p2.name}`);
// console.log(`Built-in mapped type (Omit): ${p3.name}`);

// type CustomMapped<T, K extends keyof T> = {
//     readonly [P in K]?: T[P]
// }

// type BuiltInMapped<T, K extends keyof T> = Readonly<Partial<Pick<T, K>>>;

// let p1: CustomMapped<Product, "name"> = { name: "Kayak" };
// let p2: BuiltInMapped<Product, "name"> = { name: "Kayak" };
// console.log(`Custom mapped type: ${p1.name}`);
// console.log(`Built-in mapped type: ${p1.name}`);

// type CustomMapped<K extends keyof any, T> = {
//     [P in K] : T
// };

// let p1: CustomMapped<"name" | "city", string> = { name: "Bob", city: "London" };
// // Built-in Record type mapper
// let p2: Record<"name" | "city", string> = { name: "Bob", city: "London" };
// console.log(`Custom mapped type: ${p1.name}, ${p1.city}`);
// console.log(`Built-in mapped type: ${p2.name}, ${p2.city}`);

// type resultType<T extends boolean> = T extends true ? string : number;
// // firstVal: number
// let firstVal: resultType<true> = "String Value";
// // secondVal: string
// let secondVal: resultType<false> = 100;

// let condition: boolean = true;
// // Besides literal value types, you can also use the values of variables to
// // resolve a conditional type.
// let thirdVal: resultType<typeof condition> = "String Value";
// condition = false;
// let fourthVal: resultType<typeof condition> = 100;
// let bool: boolean;
// let fifthVal: resultType<typeof bool> = 100;

// let mismatchCheck: resultType<false> = "String Value";

// type references = "London" | "Bob" | "Kayak";
// type nestedType<T extends references> = T extends "London" ? City : T extends "Bob" ? Person : Product;
// let firstVal: nestedType<"London"> = new City("London", 8136000);
// let secondVal: nestedType<"Bob"> = new Person("Bob", "London");
// let thridVal: nestedType<"Kayak"> = new Product("Kayak", 275);

// type resultType<T extends boolean> = T extends true ? string : number;

// class Collection<T> {
//     private items: T[];

//     constructor(...initialItems: T[]) {
//         this.items = initialItems || [];
//     }

//     // U extends boolean means that U can be either of type true, false or boolean. 
//     // format: U takes in a boolean value, which will be captured as a literal value type,
//     // which then is used to resolve the type of the function result.
//     total<P extends keyof T, U extends boolean>(propName: P, format: U): resultType<U> {
//         let totalValue = this.items.reduce((t, item) => t += Number(item[propName]), 0);
//         return format ? `$${totalValue.toFixed()}` : totalValue as any;
//     }
// }

// let data = new Collection(new Product("Kayak", 275), new Product("Lifejacket", 48.95));
// let firstVal: string = data.total("price", true);
// console.log(`Formatted value: ${firstVal}`);
// let secondVal: number = data.total("price", false);
// console.log(`Unformatted value: ${secondVal}`);

// type Filter<T, U> = T extends U ? never : T;
// // Filter<Person | City | Product, City> = Filter<Person, City> | Filter<City, City> | Filter<Product, City>

// // filteredType: Person | Product
// let filteredType: Filter<Person | City | Product, City>;

// function FilterArray<T, U>(data: T[], predicate: (item: any) => item is U): Filter<T, U>[] {
//     return data.filter(item => !predicate(item)) as any;
// }

// let dataArray = [new Product("Kayak", 275), new Person("Bob", "London"), new Product("Lifejacket", 48.95)];

// function isProduct(item: any): item is Product {
//     return item instanceof Product;
// }

// let filteredData: Person[] = FilterArray(dataArray, isProduct);
// filteredData.forEach(person => console.log(person.name));

// Changes the properties of a particular type to be of the specified one.
// type changePros<T, U, V> = {
//     [P in keyof T]: T[P] extends U ? V: T[P]
// }

// type modifiedProduct = changePros<Product, number, string>;

// function convertProduct(p: Product): modifiedProduct {
//     return { name: p.name, price: `$${p.price.toFixed(2)}`};
// }

// let product = new Product("Hat", 100);
// let newProduct: modifiedProduct = convertProduct(product);
// console.log(newProduct.price);

// type unionOfTypeNames<T, U> = {
//     [P in keyof T]: T[P] extends U ? P : never
// };

// type propertiesOfType<T, U> = unionOfTypeNames<T, U>[keyof T];

// function total<T, P extends propertiesOfType<T, number>>(data: T[], propName: P): number {
//     return data.reduce((t, item) => t += Number(item[propName]), 0);
// }

// let products = [new Product("Hat", 100), new Product("Gloves", 42)];
// console.log(total(products, "price"));





// function getValue<T, P extends keyof T>(data: T, propName: P): T[P] {
//     if (Array.isArray(data)) {
//         return data[0][propName];
//     } else {
//         return data[propName];
//     }
// }

// let products = [new Product("Hat", 100), new Product("Gloves", 42)];
// console.log(getValue(products, "price"));
// console.log(getValue(products[0], "price"));

// You can think of (infer U) as a placeholder for a type that will be inferred by
// the compiler.
// type targetKeys<T> = T extends (infer U)[] ? keyof U: keyof T;
// function getValue<T, P extends targetKeys<T>>(data: T, propName: P): T[P] {
//     if (Array.isArray(data)) {
//         return data[0][propName];
//     } else {
//         return data[propName];
//     }
// }

// let products = [new Product("Hat", 100), new Product("Gloves", 42)];
// console.log(getValue(products, "price"));
// console.log(getValue(products[0], "price"));

// type Result<T> = T extends (...args: any) => (infer R) ? R : never;

// function processArray<T, Func extends (item: T) => any>(data: T[], func: Func): Result<Func>[] {
//     return data.map(item => func(item));
// }

// let selectName = (p: Product) => p.name;

// let selectPrice = (p: Product) => p.price;
// let products = [new Product("Hat", 100), new Product("Gloves", 42)];
// let names: string[] = processArray(products, selectName);
// let prices = processArray(products, selectPrice);
// names.forEach(name => console.log(`Name: ${name}`));

function makeObject<T extends new (...args: any) => any>(constructor: T, ...args: ConstructorParameters<T>): InstanceType<T> {
    return new constructor(...args as any[]);
}

let prod: Product = makeObject(Product, "Kayak", 275);
let city: City = makeObject(City, "London", 8136000);
[prod, city].forEach(item => console.log(`Name: ${item.name}`));