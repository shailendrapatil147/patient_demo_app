export interface IState {
    Id: number;
    Name: string;
}

export class State implements IState {
    constructor(public Id: number,
    public Name: string){}
}
