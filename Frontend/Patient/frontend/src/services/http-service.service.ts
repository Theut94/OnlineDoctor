import { Injectable } from '@angular/core';
import { IMeasurement } from '../models/Measurement.model';

@Injectable({
  providedIn: 'root'
})
export class HttpServiceService {

  constructor() { }

  async postMeassurements(measurement: IMeasurement){
    const response = await fetch('http://127.0.0.1:3000/Measurement', { 
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(measurement)
    })
    .then(() => true)
    .catch(() => false);
    
    return response;
  }
}
