export interface IPatient {
    Id: number;
    SurName: string;
    Name: string;
    DOB: Date;
    Gender: string;
    CityId: number;
}

export class Patient implements IPatient {
    constructor(public Id: number,
    public SurName: string,
    public Name: string,
    public DOB: Date,
    public Gender: string,
    public CityId: number){}
}
