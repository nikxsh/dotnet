//Core
import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ActivatedRoute, Params } from '@angular/router';
//Models
import { Catalogue, ContactType } from '../../Models/contact.model';
import { ValueObjectPair, MessageHandler, Address, PagingRequest } from '../../Models/common.model';
//Services
import { CommonService } from '../../services/common.service';
import { TenantService } from '../../services/tenant.service';
//Utility
import * as Global from '../../global'
import { HandleError } from '../../helpers/error.utility';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  private contactModalRef: BsModalRef;
  private catalogueModel: Catalogue;
  private lstStates: Array<ValueObjectPair> = [];
  private lstCountries: Array<ValueObjectPair> = [];
  private lstContacts: Array<Catalogue> = [];
  private sameAsBillingAddress: boolean = false;
  private isNewContact: boolean = true;
  private progressing: boolean = false;
  private Title = "Contacts";
  private pager: any = {};
  private subscriber: any;
  private message: MessageHandler = new MessageHandler();

  constructor(private commonService: CommonService,
    private tenantService: TenantService,
    private route: ActivatedRoute,
    private modalServiceRef: BsModalService) {
  }

  ngOnInit() {
    try {

      this.subscriber = this.route.params.forEach((params: Params) => {
        this.message.text = '';
        let contactType = +params['type'];
        if (contactType == ContactType.Vendor || contactType == ContactType.Customer) {

          if (contactType == ContactType.Customer)
            this.catalogueModel = new Catalogue(new Address(), new Address());
          else if (contactType == ContactType.Vendor)
            this.catalogueModel = new Catalogue(new Address());

          this.catalogueModel.type = contactType;
          this.getContacts();
          this.getStates();
          this.getCountries();
        }
        else {
          this.message.text = Global.UI_DONT_TAMPER;
          this.message.type = 2;
        }
      });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private contactTypes = [
    { value: 0, display: 'Customer' },
    { value: 1, display: 'Vendor' }
  ];

  private getContacts() {
    try {
      var request = new PagingRequest(this.pager.startIndex, this.pager.endIndex + 1)
      this.message.type = 2;
      if (this.catalogueModel.type == ContactType.Customer)
        this.tenantService._getCustomerContacts(request)
          .then(result => {
            if (result.status == 1) {
              this.lstContacts = result.data;
              if (result.data.length <= 0) {
                this.message.text = result.message;
                this.message.type = 3;
              }
            }
            else {
              this.message.text = result.message;
              this.message.type = 2;
            }
          },
          error => {
            HandleError.handle(error);
          });
      else if (this.catalogueModel.type == ContactType.Vendor)
        this.tenantService._getVendorContacts(request)
          .then(result => {
            if (result.status == 1) {
              this.lstContacts = result.data;
              if (result.data.length <= 0) {
                this.message.text = result.message;
                this.message.type = 3;
              }
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
    catch (exeception) {
      this.message.text = Global.UI_ERROR;
      this.message.type = 2;
    }
  }

  private saveContact() {
    try {
      this.message.text = '';
      this.message.isGlobal = false;

      this.progressing = true;
      if (this.catalogueModel.type == ContactType.Customer) {
        if (this.isNewContact) {
          this.tenantService._addCustomerContact(this.catalogueModel, this.catalogueModel.type)
            .then(result => {
              this.progressing = false;
              if (result.status == 1) {
                this.onReset();
                this.lstContacts.unshift(result.data);
                this.message.isGlobal = true;
                this.message.text = Global.UI_ADD_SUCCESS.replace('{0}', 'Contact');
                this.message.type = 1;
                this.contactModalRef.hide();
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
        else {
          this.tenantService._editCustomerContact(this.catalogueModel, this.catalogueModel.type)
            .then(result => {
              this.progressing = false;
              if (result.status == 1) {
                this.onReset();
                let item = this.lstContacts.find(x => x.id == result.data.id);
                Object.assign(item, result.data);
                this.message.isGlobal = true;
                this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Contact');
                this.message.type = 1;
                this.contactModalRef.hide();
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
      }
      else if (this.catalogueModel.type == ContactType.Vendor) {
        if (this.isNewContact) {
          this.tenantService._addVendorContact(this.catalogueModel, this.catalogueModel.type)
            .then(result => {
              this.progressing = false;
              if (result.status == 1) {
                this.onReset();
                this.lstContacts.push(result.data);
                this.message.isGlobal = true;
                this.message.text = Global.UI_ADD_SUCCESS.replace('{0}', 'Vendor');
                this.message.type = 1;
                this.contactModalRef.hide();
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
        else {
          this.tenantService._editVendorContact(this.catalogueModel, this.catalogueModel.type)
            .then(result => {
              this.progressing = false;
              if (result.status == 1) {
                this.onReset();
                let item = this.lstContacts.find(x => x.id == result.data.id);
                Object.assign(item, result.data);
                this.message.isGlobal = true;
                this.message.text = Global.UI_EDIT_SUCCESS.replace('{0}', 'Vendor');
                this.message.type = 1;
                this.contactModalRef.hide();
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
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private addContact() {
    this.message.text = '';
    this.isNewContact = true;
  }

  private editContact(id) {
    try {
      this.message.text = '';
      this.isNewContact = false;

      let item = this.lstContacts.find(x => x.id == id);
      if (item) {
        this.catalogueModel = Object.assign({}, item);
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private enableDisableContact(id) {
    try {
      this.message.text = '';
      let contactInfo = this.lstContacts.find(x => x.id == id);

      if (contactInfo) {
        let isEnabled = !contactInfo.isEnable;
        if (contactInfo.type == ContactType.Customer || contactInfo.type == ContactType.Vendor)
          this.tenantService._enableDisableContact(id, isEnabled, contactInfo.type)
            .then(result => {
              if (result.status == 1) {
                this.lstContacts.find(x => x.id == id).isEnable = isEnabled;
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

  private getStates() {
    try {
      this.commonService._getStates()
        .subscribe(data => {
          this.lstStates = data;
          this.setDefaultState();
        },
        error => {
          this.message.text = Global.UI_ERROR;
          this.message.type = 2;
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private getCountries() {
    try {
      this.commonService._getCountries()
        .subscribe(data => {
          this.lstCountries = data;
          this.setDefaultCountry();
        },
        error => {
          this.message.text = Global.UI_ERROR;
          this.message.type = 2;
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private setDefaultState() {
    try {
      let state = this.lstStates.find(x => x.code == 'MH');
      if (this.catalogueModel.type == ContactType.Customer) {
        this.catalogueModel.billingAddress.state = state.name;
        this.catalogueModel.shippingAddress.state = state.name;
      }
      else if (this.catalogueModel.type == ContactType.Vendor) {
        this.catalogueModel.billingAddress.state = state.name;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private setDefaultCountry() {
    try {
      let country = this.lstCountries.find(x => x.code == 'IN');
      if (this.catalogueModel.type == ContactType.Customer) {
        this.catalogueModel.billingAddress.country = country.name;
        this.catalogueModel.shippingAddress.country = country.name;
      }
      else if (this.catalogueModel.type == ContactType.Vendor) {
        this.catalogueModel.billingAddress.country = country.name;
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private copyBillingAddress(e) {
    try {
      if (e.target.checked) {
        this.catalogueModel.shippingAddress.addressLine = this.catalogueModel.billingAddress.addressLine;
        this.catalogueModel.shippingAddress.city = this.catalogueModel.billingAddress.city;
        this.catalogueModel.shippingAddress.state = this.catalogueModel.billingAddress.state;
        this.catalogueModel.shippingAddress.postalCode = this.catalogueModel.billingAddress.postalCode;
        this.catalogueModel.shippingAddress.country = this.catalogueModel.billingAddress.country;
      }
      else {
        this.catalogueModel.shippingAddress.addressLine = '';
        this.catalogueModel.shippingAddress.city = '';
        this.catalogueModel.shippingAddress.postalCode = '';
        this.setDefaultState();
        this.setDefaultCountry();
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  private onReset() {
    try {
      this.message.text = '';

      if (this.isNewContact) {

        if (this.catalogueModel.type == ContactType.Customer)
          this.catalogueModel = new Catalogue(new Address(), new Address(), this.catalogueModel.type);
        else if (this.catalogueModel.type == ContactType.Vendor)
          this.catalogueModel = new Catalogue(new Address(), undefined, this.catalogueModel.type);

        this.sameAsBillingAddress = false;
        this.setDefaultState();
        this.setDefaultCountry();
      }
      else {

        let item = this.lstContacts.find(x => x.id == this.catalogueModel.id);

        Object.assign(this.catalogueModel, item);
        Object.assign(this.catalogueModel.billingAddress, item.billingAddress);

        if (this.catalogueModel.type == ContactType.Customer)
          Object.assign(this.catalogueModel.shippingAddress, item.shippingAddress);
      }
    }
    catch (e) {
      HandleError.handle(e);
    }
  }

  public openModal(template: TemplateRef<any>) {
    try {
      this.contactModalRef = this.modalServiceRef.show(template, { class: 'modal-lg' });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
