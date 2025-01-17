import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

const PathUrl = "http://localhost:5220/api/CadastroBanco/"

@Injectable({
  providedIn: 'root'
})
export class BancoService {

  constructor(private http: HttpClient) { }

 

  public ListaBanco():Observable<any>{
    const url = PathUrl + "ListaBanco";
    return this.http.get(url);
  }

  public Cadastrar(descricao: string): Observable<any> {
    const url = PathUrl + 'Cadastrar/' + descricao;
    return this.http.get(url);
  }
}
