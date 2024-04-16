import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './onlinePatient/onlinePatient.config';
import { AppComponent } from './onlinePatient/onlinePatient';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
