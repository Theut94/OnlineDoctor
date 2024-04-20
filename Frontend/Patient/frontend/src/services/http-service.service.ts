import { Injectable } from '@angular/core';
import { IMeasurement } from '../models/Measurement.model';
import { STATUS_CODES } from 'node:http';

@Injectable({
  providedIn: 'root'
})
export class HttpServiceService {

  constructor() { }

  async postMeassurements(measurement: IMeasurement, country: string){
    try {
      const response = await fetch(`http://127.0.0.1:3000/Measurement?country=${encodeURIComponent(country)}`, { 
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(measurement)
        })
      return response;
    } catch (error) {
      console.error('Error posting measurements:', error);
      return null;
    }
  }
}
