import { Address } from "./common.model";

export class Tenant {

    constructor(
        public id: string = undefined,
        public orgProfile: Profile = undefined,
        public address: Address = undefined) {       
    }
}

export class Profile {
    
    constructor(
        public tenantName: string = undefined,
        public domainName: string = undefined,
        public industryType: string = undefined,
        public gstin: string = undefined,
        public logoUrl: string = undefined,
        public emailAddress: string = undefined,
        public isEnable: boolean = undefined,
        public isEmailVerified: boolean = undefined,
        public bankName: string = undefined,
        public accountNumber: string = undefined,
        public ifscCode: string = undefined
        ) {
    }
}

export class Industry {
    
    constructor(
        public id: number = undefined,
        public name: string = undefined) {
    }
}