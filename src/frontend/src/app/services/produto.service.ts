import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Produto {
  id: number;
  nome: string;
  preco: number;
  estoque: number;
}

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  private apiUrl = '/api/produto';

  constructor(private http: HttpClient) {}

  getProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.apiUrl);
  }
}
