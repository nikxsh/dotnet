
export class LoginRequest {

    public username: string = '';
    public password: string = '';
    public confirmPassword: string = '';
    public remember: boolean = false;

    constructor() {

    }
}

export class LoginResponse {

    public userId: string;
    public isAuthenticated: boolean = false;
    public jwToken: string;
    public timeOutPeriod: string;
    public message: string;
    public isFirstLogin: boolean

    constructor() {

    }
}

export class ResetPassword {
    constructor(
        public name?: string) {
    }
}

export class RegisterRequest {

    public name: string = '';
    public domainName: string = '';
    public email: string = '';
    public phone: string = '';
    public termsAgreed: boolean = false;

    constructor() {

    }
}