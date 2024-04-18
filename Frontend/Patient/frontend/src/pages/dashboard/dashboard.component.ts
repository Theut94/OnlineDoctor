import { Component, OnInit } from '@angular/core';
import {MatDatepickerInputEvent, MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import {AbstractControl, FormControl, ReactiveFormsModule, ValidatorFn, Validators} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../dialogs/confirmation-dialog/confirmation-dialog.component';
import { SuccessDialogComponent } from '../../dialogs/success-dialog/success-dialog.component';
import { HttpServiceService } from '../../services/http-service.service';
import { NgIf, NgStyle } from '@angular/common';
import { IMeasurement } from '../../models/Measurement.model';
import { ErrorDialogComponent } from '../../dialogs/error-dialog/error-dialog.component';
import { title } from 'process';

@Component({
  selector: 'op-dashboard',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatDatepickerModule, ReactiveFormsModule, MatButtonModule, NgIf, NgStyle],
  templateUrl: './dashboard.html',
  providers:[provideNativeDateAdapter()],
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit{
  //#region "Properties"
  date: Date = new Date();

  spinner = false;
  //#endregion

  //#region formControls

  diastolicErrorMessage = '';
  systolicErrorMessage = '';
  ssnErrorMessage = '';

  systolic = new FormControl('', [
    Validators.required,
    this.rangeValidator(70, 200)
  ]);

  diastolic = new FormControl('', [
    Validators.required,
    this.rangeValidator(60, 120)
  ]);

  ssn = new FormControl('', [
    Validators.required,
    this.numericValidator(),
    Validators.maxLength(10),
    Validators.minLength(10)
  ]);
  //#endregion

  constructor(public dialog: MatDialog, public http: HttpServiceService) {}

  ngOnInit(): void {

  }

  setDate(event: MatDatepickerInputEvent<Date>) {
    this.date = event.value ? event.value : new Date();
  }

  resetForm() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.diastolic.reset();
        this.systolic.reset();
        this.ssn.reset();
        this.date = new Date();
      }
    });
  }

  async submitForm() {
    if (this.systolic.valid && this.diastolic.valid && this.ssn.valid &&
      (this.systolic.touched || this.diastolic.touched || this.ssn.touched)) {
      // If all form controls are valid, proceed with submission
      this.spinner = true;

      const measurement: IMeasurement = {
        date: this.date,
        systolic: Number(this.systolic.value),
        diastolic: Number(this.diastolic.value),
        patientSSN: this.ssn.value ? this.ssn.value : '00000000000',
      };

      const request = await this.http.postMeassurements(measurement).then((request) => request).catch((request) => request);
      
      if (request){
        const dialogRef = this.dialog.open(SuccessDialogComponent);
      } else {
        const dialogRef = this.dialog.open(ErrorDialogComponent, {data: {title: 'An error occured while submitting the form', errors: ['Please try again later']}});
      }
      this.spinner = false;
    } else {

      this.diastolicErrorMessage = this.diastolic.invalid ? 'Invalid diastolic value. Should be a value between 60 and 120.' : '';
      this.systolicErrorMessage = this.systolic.invalid ? 'Invalid systolic value. Should be a value between 70 and 200.' : '';
      this.ssnErrorMessage = this.ssn.invalid ? 'Invalid SSN. Should be numeric and follow this format DDMMYYXXXX.' : '';


      const errors: string[] = [];

      if (this.diastolicErrorMessage !== '') {
        errors.push(this.diastolicErrorMessage);
      }

      if (this.systolicErrorMessage !== '') {
        errors.push(this.systolicErrorMessage);
      }

      if (this.ssnErrorMessage !== '') {
        errors.push(this.ssnErrorMessage);
      }

      const dialogRef = this.dialog.open(ErrorDialogComponent, {
        data: { title: 'insufficient form data!', errors }
      });
    }
    
  }

  rangeValidator(min: number, max: number): ValidatorFn {
    return (control: AbstractControl<any, any>) => {
      const value = control.value;
      if (value !== null && (isNaN(value) || value < min || value > max)) {
        return { range: true };
      }
      return null;
    }
  }

  numericValidator(): ValidatorFn {
    return (control: AbstractControl<any>): { [key: string]: any } | null => {
      const value = control.value;
      if (value !== null && isNaN(value)) {
        return { numeric: true };
      }
      return null;
    };
  }
}

