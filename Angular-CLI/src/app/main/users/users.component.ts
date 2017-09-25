import { Component, OnInit, TemplateRef } from '@angular/core';
import { Utilty } from '../../shared/utility';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-user',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UserComponent implements OnInit {
  
  modalRef: BsModalRef;

  employeeList = [
    {
      id: 1001,
      Name: 'Nikhilesh Shinde',
      Dob: Utilty.FormatDate(new Date(1990, 2, 14).toDateString()),
      Designation: 'Software Engineer'
    },
    {
      id: 1002,
      Name: 'John Snow',
      Dob: Utilty.FormatDate(new Date(1990, 4, 14).toDateString()),
      Designation: 'King'
    },
    {
      id: 1003,
      Name: 'Arya Stark',
      Dob: Utilty.FormatDate(new Date(1990, 7, 14).toDateString()),
      Designation: 'Warrior'
    },
    {
      id: 1004,
      Name: 'Emma Watson',
      Dob: Utilty.FormatDate(new Date(1990, 11, 14).toDateString()),
      Designation: 'Wizard'
    }
  ]

  constructor(private modalService: BsModalService) { }

  ngOnInit() {
  }
  
  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
}
