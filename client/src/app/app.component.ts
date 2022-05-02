import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Social Application';
  users: any;
  constructor(private http: HttpClient, private auth: AccountService){

  }
  ngOnInit() {
    this.setCurrentuser();
  }
  
  setCurrentuser(){
    const user = JSON.parse(localStorage.getItem('user'));
    this.auth.setCurrentUser(user);
  }
}
