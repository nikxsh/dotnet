import { Address } from './common.m'

export class Tenant {

    constructor(
        public id?: string,
        public orgProfile: Profile = new Profile(),
        public address: Address = new Address()) {       
    }
}

export class Profile {
    
    constructor(
        public tenantName?: string,
        public domainName?: string,
        public industryType?: string,
        public gstin?: string,
        public logoUrl?: string,
        public emailAddress?: string,
        public isEnable?: boolean,
        public isEmailVerified?: boolean,
        public bankName?: string,
        public accountNumber?: string,
        public ifscCode?: string,
        ) {
    }
}

export class Industry {
    
    constructor(
        public id?: number,
        public name?: string) {
    }
}