import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const PathUrl = "http://localhost:5220/api/CadastroUsuario/"

@Injectable({
  providedIn: 'root'
})


export class CadastroUsuarioService {

  constructor(private http: HttpClient) { }

  public CriaConta( usuario: any ):Observable<any>{
    const url = PathUrl + `CriaConta`;
    return this.http.post(url, usuario);
  }
}
