import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';



const PathUrl = 'http://localhost:5220/api/Dashboard/';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

   constructor(private http: HttpClient) { }


   public TrazDashboard(): Observable<any>{
    const url = PathUrl + `TrazDashboard`;
    return this.http.get(url);
   }


   
}
