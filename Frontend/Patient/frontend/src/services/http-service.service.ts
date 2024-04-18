import { Injectable } from '@angular/core';
import { IMeasurement } from '../models/Measurement.model';

@Injectable({
  providedIn: 'root'
})
export class HttpServiceService {

  constructor() { }

  async postMeassurements(meassurement: IMeasurement){
    const response = await fetch('http://measurement:3000/Meassurements', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(meassurement)
    })
    .then(() => true)
    .catch(() => false);
  }
}
