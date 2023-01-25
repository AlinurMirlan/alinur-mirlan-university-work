export class Product {
    constructor(public id: number, public name: string, public price: number) {}
}

export enum Sport {
    Running, Soccer, Watersports, Other
}

export class SportsProduct extends Product {
    private _sports: Sport[];

    get sports(): Sport[] {
        return this._sports;
    }

    constructor(public id: number, public name: string, public price: number, ...sportsArray: Sport[]) {
        super(id, name, price);
        this._sports = sportsArray;
    }

    usedForSport(s: Sport): boolean {
        return this._sports.includes(s);
    }
}