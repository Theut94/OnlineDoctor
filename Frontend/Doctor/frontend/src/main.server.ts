import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './onlineDoctor/onlineDoctor.component';
import { config } from './onlineDoctor/onlineDoctor.config.server';

const bootstrap = () => bootstrapApplication(AppComponent, config);

export default bootstrap;
