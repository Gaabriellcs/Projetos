import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const PathUrl = 'http://localhost:5220/api/CadastroCategoria/';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

  constructor(private http: HttpClient) { }

  public Cadastrar(descricao: string): Observable<any> {
    const url = PathUrl + `Cadastrar/` + descricao;
    return this.http.get(url);
  }

  public Listar(): Observable<any> {
    const url = PathUrl + `Listar`;
    return this.http.get(url);
  }

  public Ativar(id: number): Observable<any> {
    const url = PathUrl + `Ativar/` + id;
    return this.http.get(url);
  }


  public Inativar(id: number): Observable<any> {
    const url = PathUrl + `Inativar/` + id;
    return this.http.get(url);
  }
}
