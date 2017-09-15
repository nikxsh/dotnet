import { Component, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  modalRef: BsModalRef;
  title = 'Angular world!';
  
   employeeList = [
     { id: 1001, Name: 'Nikhilesh Shinde', Dob: new Date(1990,2,14).toDateString(),Designation : 'Software Engineer'},
     { id: 1002, Name: 'John Snow', Dob: new Date(1990,4,14).toDateString(),Designation : 'King'},
     { id: 1003, Name: 'Arya Stark', Dob: new Date(1990,7,14).toDateString(),Designation : 'Warrior'},
     { id: 1004, Name: 'Emma Watson', Dob: new Date(1990,11,14).toDateString(),Designation : 'Wizard'}    
    ]

  constructor(private modalService: BsModalService){
  }
 
   public openModal(template: TemplateRef<any>){
      this.modalRef = this.modalService.show(template);
   } 
}
