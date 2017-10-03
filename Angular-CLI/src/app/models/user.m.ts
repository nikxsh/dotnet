export class User {

    constructor(
        public id?: string,
        public tenantId?: string,
        public name?: string,
        public userName?: string,
        public firstName?: string,
        public lastName?: string,
        public email?: string,
        public roleId?: string,
        public roleName?: string,
        public clearPassword: string = "",
        public confirmPassword: string = "",
        public isEnable: boolean = true,
        public companyName?: string) {
    }         
}

export class UserResponse {

    constructor(
        public id?: string,
        public user?: User) {
    }
}

export class Role {

    constructor(                   
        public id?: string,
        public roleName?: string,  
        public isEnable: boolean = true,
        public modulePermissions?: ModulePermission[]
    ) {
    }
}
export class ModulePermission {

    constructor(
        public module?: string,
        public permissions?: Permission[]
    ) {        
    }
}

export class Permission {

    constructor(
        public permissionId?: string,
        public apiCode?: string,
        public hasPermission: boolean = false,        
        public action?: string
    ) {
    }
}

