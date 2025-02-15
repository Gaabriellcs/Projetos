import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { CategoriaService } from '../../services/categoria.service';
import { CommonModule, NgIf } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-categoria',
  imports: [FormsModule, InputTextModule, ButtonModule, TableModule, CommonModule, NgIf],
  templateUrl: './categoria.component.html',
  styleUrl: './categoria.component.scss'
})
export class CategoriaComponent {
  descricao: any;
  categorias!: any[];

  constructor(private srv: CategoriaService) { }

  ngOnInit(): void {
    this.listar();
  }

  listar() {
    this.srv.Listar().subscribe({
      next: p => {
        this.categorias = p
      }
    })
  }


  cadastraCategoria() {
    this.srv.Cadastrar(this.descricao).subscribe({
      next: p => {
        this.listar();
      }
    })
  }


  ativar(id: number) {
    this.srv.Ativar(id).subscribe({
      next: p => {
        this.listar();
      }
    })
  }

  inativar(id: number) {
    this.srv.Inativar(id).subscribe({
      next: p => {
        this.listar();
      }
    })
  }
}
