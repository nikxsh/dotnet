
export class LoginRequest {

    public username: string  = undefined;
    public password: string  = undefined;
    public confirmPassword: string  = undefined;
    public remember: boolean = false;

    constructor() {

    }
}

export class LoginResponse {

    public userId: string = undefined;
    public isAuthenticated: boolean = false;
    public jwToken: string = undefined;
    public timeOutPeriod: string = undefined;
    public message: string = undefined;
    public isFirstLogin: boolean

    constructor() {

    }
}

export class ResetPassword {
    constructor(
        public name: string = undefined) {
    }
}

export class RegisterRequest {

    public name: string  = undefined;
    public domainName: string  = undefined;
    public email: string  = undefined;
    public phone: string  = undefined;
    public termsAgreed: boolean = false;

    constructor() {

    }
}