import { Component } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { ChartModule } from 'primeng/chart';
@Component({
  selector: 'app-dashboard',
  imports: [ChartModule ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {


  constructor(private srv: DashboardService) { }


  chartData: any;
  chartOptions: any;


  ngOnInit(): void {

    this.srv.TrazDashboard().subscribe({
      next: p => {
        this.chartData = p;
        this.chartOptions = {
          responsive: true,
          // maintainAspectRatio: false,
          plugins: {
            legend: {
              labels: {
                color: '#495057'
              }
            }
          }
        };
      }
    })
  }

}

