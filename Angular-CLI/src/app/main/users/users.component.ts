//Core
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';

//Models
import { Role, User, UserResponse } from '../../Models/user.m';
import { MessageHandler } from '../../Models/common.m';
import { Tenant } from '../../Models/profile.m';

//Services
import { UserService } from '../../services/user.s';
import { LocalStorageService } from '../../services/storage.s';
import { TenantService } from '../../services/tenant.s';
import { RoleAndPermissionService } from '../../services/rolepermissions.s';

//Helpers
import * as Global from '../../global'
import { matchingPasswords } from '../../helpers/validators.h';
import { HandleError } from '../../helpers/error.h';

@Component({
  selector: 'app-user',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UserComponent implements OnInit {

  private addEditUserModalRef: BsModalRef;
  private title: string = "Users";
  private addEditUserForm: FormGroup;
  private userListCount: number;
  private userRoles: Role[] = [];
  private userModel: User;
  private addNewUser: boolean;
  private pager: any = {};
  private pagedUsers: User[] = [];
  private lstUsers: UserResponse[] = [];
  private progressing: boolean = false;
  private message: MessageHandler = new MessageHandler();


  constructor(private userServiceRef: UserService,
    private localStorageServiceRef: LocalStorageService,
    private formBuilder: FormBuilder,
    private tenantService: TenantService,
    private roleServiceRef: RoleAndPermissionService,
    private modalServiceRef: BsModalService) {
    this.userModel = new User();
  };

  ngOnInit(): void {
    this.getTenantRoles();
    this.InItUserForm();
    this.getAllUsers();
  }

  private InItUserForm() {

    this.addEditUserForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      clearPassword: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.nullValidator],
      role: ['', Validators.required]
    }, { validator: matchingPasswords('clearPassword', 'confirmPassword') })
  }

  private addUser() {
    try {
      this.message.text = '';
      this.userModel = new User();
      this.userModel.roleName = this.userRoles[0].roleName;
      this.addNewUser = true;
      let tenantInfo = JSON.parse(this.localStorageServiceRef._getTenantInfo()) as Tenant;
      this.userModel.companyName = tenantInfo.orgProfile.tenantName;

      this.addEditUserForm.get('clearPassword').enable();
      this.addEditUserForm.get('clearPassword').setValidators(Validators.required);
      this.addEditUserForm.get('confirmPassword').enable();
      this.addEditUserForm.get('confirmPassword').setValidators(Validators.required);
      this.addEditUserForm.get('name').enable();
      this.addEditUserForm.get('name').setValidators(Validators.compose([Validators.required, Validators.minLength(4)]));
    }
    catch (e) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private saveUser() {
    try {
      this.progressing = true;
      this.userModel.roleId = this.userRoles.find(x => x.roleName.toLocaleLowerCase() == this.userModel.roleName.toLocaleLowerCase()).id;
      this.message.isGlobal = false;
      this.message.text = '';
      if (this.addNewUser) {
        this.userServiceRef._addUser(this.userModel)
          .then(result => {

            this.progressing = false;

            if (result.status == 1) {
              this.lstUsers.push(result.data);
              this.addEditUserModalRef.hide();
              this.message.isGlobal = true;
              this.message.text = Global.UI_ADD_SUCCESS.replace('{0}', 'User');
              this.message.type = 1;
              this.addEditUserForm.reset();
            }
            else {
              this.message.text = result.message;
              this.message.type = 2;
            }
          },
          error => {
            HandleError.handle(error);
            this.progressing = false;
            this.addEditUserModalRef.hide();
          });
      }
      else if (this.addEditUserForm.dirty) {
        this.userServiceRef._editUser(this.userModel)
          .then(result => {

            this.progressing = false;

            if (result.status == 1) {
              Object.assign(this.lstUsers.find(x => x.id == result.data.id), result.data);
              this.addEditUserModalRef.hide();
              this.message.isGlobal = true;
              this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'User');
              this.message.type = 1;
              this.addEditUserForm.reset();
            }
            else {
              this.message.text = result.message;
              this.message.type = 2;
            }
          },
          error => {
            HandleError.handle(error);
            this.progressing = false;
            this.addEditUserModalRef.hide();
          });
      }
      else {
        this.progressing = false;
        this.message.text = "Atleast one field must be changed";
        this.message.type = 2;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private editUser(userId) {
    try {
      this.addNewUser = false;

      this.message.text = '';
      this.addEditUserForm.get('clearPassword').disable();
      this.addEditUserForm.get('confirmPassword').disable();
      this.addEditUserForm.get('name').disable();

      if (userId != undefined && userId != '') {
        let item = this.lstUsers.find(x => x.id == userId);
        item.user.id = item.id;
        Object.assign(this.userModel, item.user);
        this.userModel.name = item.user.userName;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private enableDisableUser(id) {
    try {
      this.message.text = '';
      let userInfo = this.lstUsers.find(x => x.id == id);
      if (userInfo) {
        let isEnabled = !userInfo.user.isEnable;
        this.userServiceRef._enableDisableUser(id, isEnabled)
          .then(result => {
            if (result.status == 1) {
              this.lstUsers.find(x => x.id == id).user.isEnable = isEnabled;
            }
            else {
              this.message.text = result.message;
              this.message.type = 2;
            }
          },
          error => {
            HandleError.handle(error);
          });
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getTenantRoles() {
    try {
      this.roleServiceRef._getTenantRoles(true)
        .then(result => {
          if (result.status == 1)
            this.userRoles = result.data;
        },
        error => {
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getAllUsers() {
    try {
      this.message.text = '';
      this.userServiceRef._getAllUsers(this.pager.startIndex, this.pager.endIndex + 1)
        .then(result => {
          if (result.status == 1) {
            this.lstUsers = result.data;
            if (result.data.length <= 0) {
              this.message.text = Global.UI_EMPTY_RESULT.replace('{0}', 'Users');
              this.message.type = 3;
            }
          }
        },
        error => {
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }

  }

  private onReset() {
    this.message.text = '';
    this.userModel = new User();
  }

  public openModal(template: TemplateRef<any>) {
    this.addEditUserModalRef = this.modalServiceRef.show(template);
  }
}
