export class User {

    constructor(
        public id: string = undefined,
        public tenantId: string = undefined,
        public name: string = undefined,
        public userName: string = undefined,
        public firstName: string = undefined,
        public lastName: string = undefined,
        public email: string = undefined,
        public roleId: string = undefined,
        public roleName: string = undefined,
        public clearPassword: string  = undefined,
        public confirmPassword: string  = undefined,
        public isEnable: boolean = true,
        public companyName: string = undefined) {
    }         
}

export class UserResponse {

    constructor(
        public id: string = undefined,
        public user: User = undefined) {
    }
}

export class Role {

    constructor(                   
        public id: string = undefined,
        public roleName: string = undefined,  
        public isEnable: boolean = true,
        public modulePermissions?: ModulePermission[]
    ) {
    }
}
export class ModulePermission {

    constructor(
        public module: string = undefined,
        public permissions?: Permission[]
    ) {        
    }
}

export class Permission {

    constructor(
        public permissionId: string = undefined,
        public apiCode: string = undefined,
        public hasPermission: boolean = false,        
        public action: string = undefined
    ) {
    }
}

