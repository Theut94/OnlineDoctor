import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './onlineDoctor/onlineDoctor.config';
import { AppComponent } from './onlineDoctor/onlineDoctor.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
