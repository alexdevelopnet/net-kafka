import { CurrencyPipe, DecimalPipe, NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Produto, ProdutoService } from '../../services/produto.service';

@Component({
  selector: 'app-produtos-list',
  templateUrl: './produtos-list.component.html',
  styleUrls: ['./produtos-list.component.scss'],
  imports: [
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    NgIf,
    NgFor,
    CurrencyPipe,
    DecimalPipe
  ]
})
export class ProdutosListComponent implements OnInit {
  produtos: Produto[] = [];
  loading = true;

  constructor(private produtoService: ProdutoService) {}

  ngOnInit(): void {
    this.produtoService.getProdutos().subscribe({
      next: (data) => {
        this.produtos = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }
}
