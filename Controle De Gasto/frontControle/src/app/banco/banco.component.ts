import { Component } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { BancoService } from '../../services/banco.service';


@Component({
  selector: 'app-banco',
  imports: [FormsModule, InputTextModule, ButtonModule, TableModule, CommonModule],
  templateUrl: './banco.component.html',
  styleUrl: './banco.component.scss'
})
export class BancoComponent {
  cadastrobanco: string = "";
  bancos!: any[];

  constructor(private srv: BancoService) { }

  ngOnInit(): void {
    this.listaBanco();
  }

  listaBanco(){
    this.srv.ListaBanco().subscribe({
      next: p=>{
        this.bancos = p;
      }
    })

  }

  cadastraBanco() {
    this.srv.Cadastrar(this.cadastrobanco).subscribe({
      next: p => {
        this.listaBanco();
      }
    })
  }
}
