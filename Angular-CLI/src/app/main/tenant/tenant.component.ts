//Core
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
//Models
import { ValueObjectPair, MessageHandler, Address } from '../../Models/common.model';
import { Tenant, Profile } from '../../Models/profile.model';
//Services
import { TenantService } from '../../services/tenant.service';
import { LocalStorageService } from '../../services/storage.service';
import { CommonService } from '../../services/common.service';
//Helper
import * as Global from '../../global'
import { getLogoURL, getFormattedDateTime } from '../../helpers/common.utility';
import { HandleError } from '../../helpers/error.utility';

@Component({
  selector: 'app-tenant',
  templateUrl: './tenant.component.html',
  styleUrls: ['./tenant.component.css']
})
export class TenantComponent implements OnInit {

  private profileModalRef: BsModalRef;
  private uploadLogoModalRef: BsModalRef;
  private Title = "Orgnisational Profile";
  private progressing: boolean = false;
  private file: File;
  private submitted = false;
  private logoURL: string = "#";
  private lstStates: Array<ValueObjectPair> = [];
  private profileModel: Tenant;
  private viewModel: Tenant;
  private lstIndustries: Array<ValueObjectPair> = [];
  private message: MessageHandler = new MessageHandler();

  constructor(
    private tenantService: TenantService,
    private localStorageService: LocalStorageService,
    private commonService: CommonService,
    private modalServiceRef: BsModalService) {
    this.profileModel = this.mapTenantResult(new Tenant());
    this.viewModel = this.mapTenantResult(new Tenant());
  }

  ngOnInit() {
    this.getTenantInfo();
    this.getStates();
    this.getIndustryTypes();
  }

  private saveTenantInfo() {
    try {
      this.message.isGlobal = false;
      this.message.text = '';

      this.progressing = true;
      this.profileModel.address.country = "India";
      this.tenantService._saveTenantProfile(this.profileModel)
        .then(result => {
          this.progressing = false;
          if (result.status == 1) {
            this.viewModel = this.mapTenantResult(result.data);
            this.localStorageService._setTenantInfo(this.viewModel);
            this.profileModalRef.hide();
            this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Profile');
            this.message.type = 1;
            this.message.isGlobal = true;
          }
          else {
            this.message.text = result.message;
            this.message.type = 2;
          }
        },
        error => {
          this.progressing = false;
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getIndustryTypes() {
    try {
      this.commonService._getIndustryTypes()
        .subscribe(data => {
          this.lstIndustries = data;
        },
        error => {
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getStates() {
    try {
      this.commonService._getStates()
        .subscribe(data => {
          this.lstStates = data;
        },
        error => {
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getTenantInfo() {
    try {
      this.message.text = '';
      this.tenantService._getTenantProfile()
        .then(result => {
          if (result.status == 1) {
            this.viewModel = this.mapTenantResult(result.data);
            this.logoURL = getLogoURL(result.data.id) + '?' + getFormattedDateTime(new Date().toString());
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

  private editProfile() {
    this.message.text = '';
    let data = this.viewModel;
    this.profileModel = this.mapTenantResult(data);
  }

  private mapTenantResult(data: Tenant) {

    if (data == undefined)
      return new Tenant();
    else {
      let tenantObj = new Tenant();

      if (data.id == undefined)
        tenantObj.id = '';
      else
        tenantObj.id = data.id;

      if (data.orgProfile == undefined)
        tenantObj.orgProfile = new Profile('', '', '', '', '', '', false, false, '', '', '');
      else
        tenantObj.orgProfile =
          new Profile(data.orgProfile.tenantName,
            data.orgProfile.domainName,
            data.orgProfile.industryType,
            data.orgProfile.gstin,
            data.orgProfile.logoUrl,
            data.orgProfile.emailAddress,
            data.orgProfile.isEnable,
            data.orgProfile.isEmailVerified,
            data.orgProfile.bankName,
            data.orgProfile.accountNumber,
            data.orgProfile.ifscCode);

      if (data.address == undefined)
        tenantObj.address = new Address('', '', '', '', '', '');
      else
        tenantObj.address = new Address(data.address.addressLine,
          data.address.city,
          data.address.state,
          data.address.postalCode,
          data.address.phoneNumber,
          data.address.country);

      return tenantObj;
    }
  }

  private fileChanged(e: Event) {
    try {
      var target: HTMLInputElement = e.target as HTMLInputElement;
      for (var i = 0; i < target.files.length; i++) {
        this.uploadFile(target.files[i]);
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private uploadFile(image: File) {

    this.tenantService._uploadLogo(image)
      .then(result => {
        if (result.status == 1) {
          this.logoURL = getLogoURL(result.data) + '?' + getFormattedDateTime(new Date().toString());
          this.uploadLogoModalRef.hide();
        }
      },
      error => {
        this.uploadLogoModalRef.hide();
        HandleError.handle(error);
      });
  }

  private setDefaultState() {

    let state = this.lstStates.find(x => x.code == 'MH');
    this.profileModel.address.state = state.name;
  }

  private onReset() {
    this.message.text = '';
    let data = this.viewModel;
    this.profileModel = this.mapTenantResult(data);
  }

  public openModal(template: TemplateRef<any>) {
    try {
      this.profileModalRef = this.modalServiceRef.show(template, { class: 'modal-avg' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  public openLogoModal(template: TemplateRef<any>) {
    try {
      this.uploadLogoModalRef = this.modalServiceRef.show(template, { class: 'modal-sm' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  // TODO: Remove this when we're done
  get diagnostic() { return JSON.stringify(""); }
}
