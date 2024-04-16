import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { DashboardComponent } from '../pages/dashboard/dashboard.component';

@Component({
  selector: 'online-patient',
  standalone: true,
  imports: [CommonModule, RouterOutlet, DashboardComponent],
  templateUrl: './onlinePatient.html',
  styleUrl: './onlinePatient.scss'
})
export class AppComponent {
  title = 'onlinePatient';
}
