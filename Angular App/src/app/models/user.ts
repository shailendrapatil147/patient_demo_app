import { Address } from "./address";

export class User {
    public id: number;
    public code: string;
    public firstName: string;
    public lastName: string;
    public mobile: string;
    public email: string;

    public address: Address;
}
