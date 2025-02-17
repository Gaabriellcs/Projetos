import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const PathUrl = "http://localhost:5220/api/CadastroFatura/"

@Injectable({
  providedIn: 'root'
})
export class FaturaService {

  constructor(private http: HttpClient) { }

  uploadCsv(banco: any, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    const url = PathUrl + "UploadCsv/" + banco;

    return this.http.post(url, formData);
  }


  public Listar(inicio: string, fim: string): Observable<any> {
    const url = PathUrl + `Listar/` + inicio + '/' + fim;
    return this.http.get(url);
  }

  public CadastrarCategoria(item: number, descricao: number):Observable<any>{
    const url = PathUrl + `CadastrarCategoria/` + item +'/' + descricao;
    return this.http.get(url);
    
  }

}
