import { Component } from '@angular/core'

@Component({
    selector: 'top-links',
    templateUrl: './Templates/Navigation/Links.html'
})

export class LinksComponent {

    GetMessages = [
        {
            id: 1, name: 'Nikhilesh Shinde', date: 'Today', message: 'Hello there!!!'
        },
        {
            id: 1, name: 'Asawari lol', date: 'Yesterday', message: 'Why this kolawari'
        },
        {
            id: 1, name: 'John Smith', date: 'Yesterday', message: 'Sky full of lighters!'
        }
    ];
} 