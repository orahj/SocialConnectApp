import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model:any = {};
  constructor(public auth: AccountService, private router:Router,private toastr: ToastrService) { }

  ngOnInit(): void {
  }
login(){
  this.auth.login(this.model).subscribe(response =>{
    this.router.navigateByUrl('/members')
  }, error=>{
    console.log(error);
    this.toastr.error(error.error)
  });
}
logout(){
  this.auth.logout();
  this.router.navigateByUrl('/  ')
}
getCurrentUser(){
  this.auth.currentUser$.subscribe(response =>{
    //this.loggedIn = !!response;
  }, error=>{
    console.log(error);
  })
}
}