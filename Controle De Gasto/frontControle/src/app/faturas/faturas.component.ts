import { Component } from '@angular/core';
import { TableModule } from 'primeng/table';
import { SelectModule } from 'primeng/select';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { FaturaService } from '../../services/fatura.service';
import { BancoService } from '../../services/banco.service';
import { CommonModule, NgIf } from '@angular/common';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-faturas',
  imports: [
    TableModule,
    FormsModule,
    ButtonModule,
    SelectModule,
    CommonModule,
    ReactiveFormsModule,
     
    DialogModule
  ],
  templateUrl: './faturas.component.html',
  styleUrls: ['./faturas.component.scss']  // Correção aqui de styleUrl para styleUrls
})
export class FaturasComponent {
  selectedFile!: File;
  bancosDados: any[] = [];
  bancoSelecionado: any = null;
  displaySuccess: boolean = false;

  constructor(private srv: FaturaService, private bancos: BancoService) {}

  ngOnInit(): void {
    this.listaBanco();
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  listaBanco() {
    this.bancos.ListaBanco(true).subscribe({
      next: p => {
        this.bancosDados = p;
      }
    });
  }

  uploadFile() {
    if (this.selectedFile && this.bancoSelecionado) {
      this.srv.uploadCsv(this.bancoSelecionado.id, this.selectedFile).subscribe({
        next: (response) => {
          this.showSuccessModal();
          setTimeout(() => {
            this.hideSuccessModal();
          }, 2000);
        },
        error: (error) => console.error('Erro ao enviar o arquivo:', error)
      });
    }
  }
  
  


  showSuccessModal() {
    this.displaySuccess = true;
  }

  hideSuccessModal() {
    this.displaySuccess = false;
  }
}
