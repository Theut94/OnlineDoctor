import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './onlinePatient/onlinePatient';
import { config } from './onlinePatient/onlinePatient.config.server';

const bootstrap = () => bootstrapApplication(AppComponent, config);

export default bootstrap;
