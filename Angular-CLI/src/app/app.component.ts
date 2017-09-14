import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Angular world!';
  
   employeeList = [
     { id: 1001, Name: 'Nikhilesh Shinde', Dob: new Date(1990,2,14).toDateString()},
     { id: 1002, Name: 'John Snow', Dob: new Date(1990,4,14).toDateString()},
     { id: 1003, Name: 'Arya Stark', Dob: new Date(1990,7,14).toDateString()},
     { id: 1004, Name: 'Emma Watson', Dob: new Date(1990,11,14).toDateString()}    
    ]
}
