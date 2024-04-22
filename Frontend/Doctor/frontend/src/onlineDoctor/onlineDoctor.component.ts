import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { DashboardComponent } from '../pages/dashboard/dashboard.component';

@Component({
  selector: 'od-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, DashboardComponent],
  templateUrl: './onlineDoctor.component.html',
  styleUrl: './onlineDoctor.component.scss'
})
export class AppComponent {
  title = 'frontend';
}
