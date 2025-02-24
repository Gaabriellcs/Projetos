import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

const PathUrl = "http://localhost:5220/api/CadastroBanco/"

@Injectable({
  providedIn: 'root'
})
export class BancoService {

  constructor(private http: HttpClient) { }

 

  public ListaBanco(ativo :boolean): Observable<any>{
    const url = PathUrl + "ListaBanco/" + ativo;
    return this.http.get(url);
  }

  public Cadastrar(descricao: string, codigoBanco: number): Observable<any> {
    const url = PathUrl + 'Cadastrar/' + descricao + '/' + codigoBanco;
    return this.http.get(url);
  }

  public Inativar(id: number): Observable<any>{
    const url = PathUrl + `Inativar/` + id;
    return this.http.get(url);
  }
  
  public Ativar(id: number): Observable<any>{
    const url = PathUrl + `Ativar/` + id;
    return this.http.get(url);
  }

}
