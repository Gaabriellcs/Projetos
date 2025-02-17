import { Component } from '@angular/core';
import { FaturaService } from '../../services/fatura.service';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { SelectModule } from 'primeng/select';
import { FormsModule } from '@angular/forms';
import { CategoriaService } from '../../services/categoria.service';
import { CalendarModule } from 'primeng/calendar';
import { DatePickerModule } from 'primeng/datepicker';

@Component({
  selector: 'app-conciliacao',
  imports: [TableModule, CommonModule, SelectModule, FormsModule, DatePickerModule ],
  templateUrl: './conciliacao.component.html',
  styleUrl: './conciliacao.component.scss'
})
export class ConciliacaoComponent {

  dataFiltro: Date[] = [];
  fatura: any[] = [];

  categorias: any;

  selectedCity: any;
  constructor(private srv: FaturaService, private srvCategoria: CategoriaService) { }
  
  
  
  buscarFaturas() {
    if (this.dataFiltro && this.dataFiltro.length === 2) {
      const inicio = this.dataFiltro[0]; // Primeiro valor do range
      const fim = this.dataFiltro[1];    // Segundo valor do range
  
      // Verifique se as datas são válidas
      if (inicio && fim) {
        const inicioFormatado = inicio.toISOString().split('T')[0];
        const fimFormatado = fim.toISOString().split('T')[0];
  
        this.srv.Listar(inicioFormatado, fimFormatado).subscribe({
          next: (p) => {
            this.fatura = p;
          },
          error: (err) => {
            console.error("Erro ao buscar faturas:", err);
          }
        });
      } 
    } else {
      console.error("Selecione um intervalo de datas válido");
    }
  }
    

  ngOnInit(): void {
    this.listarFaturas();
    this.listarCategorias();
  }

  listarFaturas() {
    const inicio = new Date(); // Data atual
    const fim = new Date();
    inicio.setDate(inicio.getDate() - 30); // Subtrai 30 dias
    
    this.srv.Listar(inicio.toISOString().split('T')[0], fim.toISOString().split('T')[0])
      .subscribe({
      next: p => {
        this.fatura = p;
      }
    })
  }

  listarCategorias() {
    this.srvCategoria.Listar().subscribe({
      next: p => {
        this.categorias = p
      }
    })
  }


  selecionarCategoria(event: any, Item: any) {
    console.log(event.value.id);
    console.log(Item);

    this.srv.CadastrarCategoria(Item, event.value.id).subscribe({
      next: p => {

      }
    })
  }

}
