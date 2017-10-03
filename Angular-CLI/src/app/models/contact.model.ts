import { Address } from "./common.model";

export class Catalogue {

    public id: string;
    public firstName: string;
    public lastName: string;
    public companyName: string;
    public email: string;
    public telephoneNumber: string;
    public phoneNumber: string;
    public type: ContactType;
    public billingAddress: Address;
    public shippingAddress: Address;
    public isEnable: boolean;
    public gstin: string; 

    constructor(billingAddress: Address = undefined, shippingAddress: Address = undefined, contactType: ContactType = ContactType.Customer) {
        this.isEnable = true;
        this.type = contactType;
        this.billingAddress = billingAddress;
        this.shippingAddress = shippingAddress;
    } 

    public getFullName() {
        return this.firstName + " " + this.lastName;
    }
}  

export enum ContactType { Customer, Vendor }
