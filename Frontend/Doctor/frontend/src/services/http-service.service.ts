import { Injectable } from '@angular/core';
import { IMeasurement } from '../models/Measurement.model';
import { STATUS_CODES } from 'node:http';
import { IPatient } from '../models/Patient.model';

@Injectable({
  providedIn: 'root'
})
export class HttpServiceService {

  constructor() { }

  async getPatients() {
    try {
      const response = await fetch(`http://127.0.0.1:3001/Patient`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        },
      })
      return response;
    } catch (error) {
      console.error('Error posting measurements:', error);
      return null;
    }
  }

  async postMeassurements(measurement: IMeasurement, country: string) {
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

  async putMeassurements(measurement: IMeasurement, country: string) {
    try {
      const response = await fetch(`http://127.0.0.1:3000/Measurement?country=${encodeURIComponent(country)}`, {
        method: 'PUT',
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

  async createPatient(patient: IPatient) {
    try {
      const response = await fetch(`http://127.0.0.1:3001/Patient`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(patient)
      })
      return response;
    } catch (error) {
      console.error('Error posting measurements:', error);
      return null;
    }
  }

  async deletePatient(patient: IPatient) {
    try {
      const response = await fetch(`http://127.0.0.1:3001/Patient?ssn=${patient.ssn}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json'
        },
      })
      return response;
    } catch (error) {
      console.error('Error posting measurements:', error);
      return null;
    }
  }

}
