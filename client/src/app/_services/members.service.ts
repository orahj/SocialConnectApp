import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
@Injectable({
  providedIn: 'root'
})
export class MembersService {
baseUrl = environment.apiUrl;
member:Member[] =[];
  constructor(private http: HttpClient) { }

  getMembers(){
    if(this.member.length > 0) return of(this.member);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members =>{
        this.member = members;
        return members;
      })
    );
  }

  getMember(username:string){
    const member = this.member.find(x=>x.username === username);
    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/'+username);
  }
  updateMember(member:Member){

    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() =>{
        const index = this.member.indexOf(member)
        this.member[index] = member;
      })
    );
  }
}
