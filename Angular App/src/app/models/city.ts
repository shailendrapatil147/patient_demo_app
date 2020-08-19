export interface ICity {
    Id: number;
    Name: string;
    StateId: number;
}

export class City implements ICity {
    constructor(public Id: number,
    public Name: string,
    public StateId: number){}
}
