//Core
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormControl } from '@angular/forms';
//Models
import { Role, ModulePermission, Permission } from '../../Models/user.model';
import { MessageHandler } from '../../Models/common.model';
//Service
import { RoleAndPermissionService } from '../../services/rolepermissions.service';
import { LocalStorageService } from '../../services/storage.service';
//Utility
import * as Global from '../../global'
import { HandleError } from '../../helpers/error.utility';
@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  @ViewChild("AddEditPermissionForm")
  addEditPermissionForm: FormControl
  
  private roleModalRef: BsModalRef;
  private title: string = "Roles";
  private roleModel: Role;
  private createModel: Role;
  private roleList: Role[] = [];
  private isNewRole: boolean = false;
  private progressing: boolean = false;
  private selectedRoleIndex: number = -1;
  private message: MessageHandler = new MessageHandler();
  private updatedPermissions: ModulePermission[] = [];
  private checkPermissions: ModulePermission[] = [];
  private isEnabled: boolean = false;

  constructor(
      private roleService: RoleAndPermissionService,
      private localStorageService: LocalStorageService,
      private modalServiceRef: BsModalService) {
      this.roleModel = new Role();
      this.createModel = new Role();
  };

  ngOnInit(): void {
      this.getAllRoles();
  }

  private onRoleSelection(roleId, index) {
      try {
          this.updatedPermissions = new Array<ModulePermission>();
          this.addEditPermissionForm.reset();
          this.message.text = '';
          this.selectedRoleIndex = index;
          this.getRoleById(roleId);
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private getRoleById(roleId) {
      try {
          this.message.text = '';

          this.roleService._getPermissionForRole(roleId)
              .then(result => {
                  if (result.status == 1) {
                      this.roleModel.id = result.data.id;
                      let existingRole = this.roleList.find(x => x.id == result.data.id);
                      this.roleModel.roleName = existingRole.roleName;
                      this.roleModel.isEnable = existingRole.isEnable;
                      this.roleModel.modulePermissions = result.data.modulePermissions;
                      this.resetEditModel();
                      this.checkPermissions = this.deepCopyPermissionData(result.data.modulePermissions);
                      this.isEnabled = existingRole.roleName != 'admin' && existingRole.isEnable;
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
      catch (e) {
        HandleError.handle(e);
      }
  }
  
  private getAllRoles() {
      try {
          this.roleService._getTenantRoles(false)
              .then(result => {
                  if (result.status == 1) {
                      this.roleList = result.data;
                      if (this.roleList.length > 0) {
                          this.selectedRoleIndex = 0;
                          this.getRoleById(this.roleList[0].id);
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

  private addRole() {
      try {
          this.message.text = '';
          this.isNewRole = true;
          this.onReset();
          this.message.isGlobal = false;
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private saveRole() {
      try {
          this.progressing = true;
          this.message.text = '';

          if (this.isNewRole) {
              this.roleService._saveRole(this.createModel)
                  .then(result => {
                      this.progressing = false;
                      if (result.status == 1) {
                          this.roleList.unshift(result.data);
                          this.roleModel.id = result.data.id;
                          this.roleModel.modulePermissions = this.deepCopyPermissionData(result.data.modulePermissions);   
                          this.checkPermissions = this.deepCopyPermissionData(result.data.modulePermissions);
                          this.roleModalRef.hide();
                          this.message.isGlobal = true;
                          this.message.text = Global.UI_ADD_SUCCESS.replace('{0}', 'Role');
                          this.message.type = 1;
                          this.selectedRoleIndex = 0;
                          this.isEnabled = result.data.roleName != 'admin' && result.data.isEnable;
                          this.isNewRole = false;
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
          else {
              this.roleService._editRole(this.createModel)
                  .then(result => {
                      this.progressing = false;
                      if (result.status == 1) {
                          let role = this.roleList.find(x => x.id == result.data.id);
                          role.id = result.data.id;
                          role.isEnable = result.data.isEnable; 
                          role.roleName = result.data.roleName;
                          this.roleModalRef.hide();
                          this.message.isGlobal = true;
                          this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Role');
                          this.message.type = 1;
                          this.selectedRoleIndex = this.roleList.findIndex(x => x.id == this.createModel.id);
                          this.getRoleById(this.createModel.id);
                          this.isEnabled = role.roleName != 'admin' && role.isEnable;
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

  private editRole(id) {
      try {
          this.isNewRole = false;
          this.message.isGlobal = false;
          this.message.text = '';
          this.onReset();
          let role = this.roleList.find(x => x.id == id);
          if (role) {
              this.createModel.id = role.id;
              this.createModel.roleName = role.roleName;
              this.createModel.isEnable = role.isEnable;
          }
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private enableDisableRole(id) {

      try {
          let roleInfo = this.roleList.find(x => x.id == id);

          if (roleInfo) {
              let isEnabled = !roleInfo.isEnable;
              this.roleService._enableDisableRole(id, isEnabled)
                  .then(result => {
                      if (result.status == 1) {
                          this.roleList.find(x => x.id == id).isEnable = isEnabled;
                          this.getRoleById(id);
                          this.selectedRoleIndex = this.roleList.findIndex(x => x.id == id);
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

  private updatePermissionData(permissionId, moduleName: string, checked: boolean) {
      try {
          this.message.text = '';
          let original = this.checkPermissions.find(x => x.module.toLowerCase() == moduleName.toLowerCase()).permissions.find(x => x.permissionId == permissionId);
          let changed = this.roleModel.modulePermissions.find(x => x.module.toLowerCase() == moduleName.toLowerCase()).permissions.find(x => x.permissionId == permissionId);

          var module = new ModulePermission();
          module.module = moduleName;

          let existingModule = this.updatedPermissions.find(x => x.module.toLowerCase() == moduleName.toLowerCase());

          if (original.hasPermission != changed.hasPermission) {
              if (existingModule == undefined) {
                  module.permissions = new Array<Permission>();
                  module.permissions.push(changed);
                  this.updatedPermissions.push(module);
              }
              else
                  existingModule.permissions.push(changed);
          }
          else if (!checked) {
              let index = existingModule.permissions.findIndex(x => x.permissionId == permissionId)
              if (index != undefined)
                  existingModule.permissions.splice(index, 1);
          }
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private savePermissionForRole() {
      try {
          this.progressing = true;
          this.message.text = '';

          let role = this.roleList.find(x => x.id == this.roleModel.id);
          this.roleModel.roleName = role.roleName;
          this.roleModel.isEnable = role.isEnable;           

          this.roleService._savePermissionForRole(Object.assign({}, this.roleModel), this.updatedPermissions)
              .then(result => {
                  this.progressing = false;
                  if (result.status == 1) {
                      this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Permission/s');
                      this.message.type = 1;
                  }
                  else {
                      this.message.text = result.message;
                      this.addEditPermissionForm.reset();
                      this.message.type = 2;
                      this.roleModel.modulePermissions = this.deepCopyPermissionData(this.checkPermissions);
                      this.updatedPermissions.splice(0, this.updatedPermissions.length);
                  }
              },
              error => {
                  HandleError.handle(error);
                  this.updatedPermissions.splice(0, this.updatedPermissions.length);
              });
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private onReset() {
      try {
          this.message.text = '';
          if (this.isNewRole) {
              this.createModel.roleName = '';
              this.createModel.id = '';
              this.createModel.isEnable = true;
          }
          else
              this.resetEditModel();
      }
      catch (e) {
        HandleError.handle(e);
      }
  }

  private resetEditModel() {
      this.createModel.roleName = this.roleModel.roleName;
      this.createModel.id = this.roleModel.id;
      this.createModel.isEnable = this.roleModel.isEnable;
  }

  private deepCopyPermissionData(permissionData: ModulePermission[]): ModulePermission[] {
      var returnData: ModulePermission[] = [];
      try {
          var copiedpermissions: Permission[];
          permissionData.forEach(y => {
              let module = new ModulePermission();
              copiedpermissions = [];
              y.permissions.forEach(n => {
                  let permission = new Permission(n.permissionId, n.apiCode, n.hasPermission, n.action);
                  copiedpermissions.push(permission);
              });
              module.module = y.module;
              module.permissions = copiedpermissions;
              returnData.push(module);
          });
      }
      catch (e) {
        HandleError.handle(e);
      }
      return returnData;
  }
  
  public openModal(template: TemplateRef<any>) {
    try {
      this.roleModalRef = this.modalServiceRef.show(template, { class: 'modal-md' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
