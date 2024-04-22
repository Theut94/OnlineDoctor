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
import {MatSelectModule} from '@angular/material/select';
import { NgFor } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { IPatient } from '../../models/Patient.model';
import { MatIconModule } from '@angular/material/icon';
import { NewPatientFormComponent } from '../../dialogs/new-patient-form/new-patient-form.component';

@Component({
  selector: 'od-dashboard',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatDatepickerModule, ReactiveFormsModule, MatButtonModule, NgIf, NgStyle, NgFor, MatSelectModule, MatExpansionModule, MatIconModule],
  templateUrl: './dashboard.html',
  providers: [provideNativeDateAdapter()],
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit {

  patients: IPatient[] = [];

    //{
    //  mail: "john@example.com",
    //  name: "Johnnyboy Doe Marksman",
    //  measurements: [
    //    {
    //      id: "1",
    //      date: new Date("2024-04-15"),
    //      systolic: 120,
    //      diastolic: 80,
    //      patientSSN: "123-45-6789",
    //      hasBeenSeen: false
    //    },
    //    {
    //      id: "2",
    //      date: new Date("2024-04-10"),
    //      systolic: 130,
    //      diastolic: 85,
    //      patientSSN: "123-45-6789",
    //      hasBeenSeen: true
    //    }
    //  ],
    //  ssn: "123-45-6789"
    //},
    //{
    //  mail: "jane@example.com",
    //  name: "Jane Smith",
    //  measurements: [
    //    {
    //      id: "1",
    //      date: new Date("2024-04-18"),
    //      systolic: 125,
    //      diastolic: 75,
    //      patientSSN: "987-65-4321",
    //      hasBeenSeen: false
    //    },
    //    {
    //      id: "2",
    //      date: new Date("2024-04-12"),
    //      systolic: 128,
    //      diastolic: 78,
    //      patientSSN: "987-65-4321",
    //      hasBeenSeen: true
    //    }
    //  ],
    //  ssn: "987-65-4321"
    //}


  panelOpenState = false;

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    const day = date.getDate();
    const month = date.getMonth() + 1; // Month is zero-based, so add 1
    const year = date.getFullYear().toString().slice(-2); // Get last two digits of the year

    return `${day}/${month}/${year}`;
  }

  toggleReviewStatusSeen(measurement: IMeasurement) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        title: 'Are you sure you want to change the review status?',
        confirmText: 'Yes, change status',
        cancelText: 'Cancel'
      }
    });

    dialogRef.afterClosed().subscribe(async result => {
      console.log("result = ", result)
      if (result) {
        const newMeasurement: IMeasurement = {
          date: measurement.date,
          diastolic: measurement.diastolic,
          hasBeenSeen: true,
          patientSSN: measurement.patientSSN,
          systolic: measurement.systolic,
          id: measurement.id
        }
        const request = await this.http.putMeassurements(newMeasurement, this.selectedCountry)
          .then((request) => request)
          .catch((request) => request);

        if (!request) {
          this.dialog.open(ErrorDialogComponent, { data: { title: 'An error occured while submitting the form', errors: ['Please try again later'] } });
          this.spinner = false;
          return;
        }

        if (request.status === 403) {
          this.dialog.open(ErrorDialogComponent, { data: { title: 'An error occured while submitting the form', errors: ['This service is not available in your country'] } });
          this.spinner = false;
          return;
        }
        if (request.status === 200) {
          measurement.hasBeenSeen = true;
          return;
        }
        this.dialog.open(ErrorDialogComponent, { data: { title: 'An error occured while submitting the form', errors: ['Please try again later'] } });
      }
    });
  }

  toggleReviewStatusUnseen(measurement: any) {
    const dialogRef = this.dialog.open(ErrorDialogComponent, { data: { title: 'Review Retraction', errors: ['You are unauthorized to modify a finalized review'] } });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        //Future feature :P

        //measurement.hasBeenSeen = !measurement.hasBeenSeen;
      }
    });
  }


  deletePatient(event: MouseEvent, patient: IPatient) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        title: `Are you sure you want to delete ${patient.name} permanently?`,
        confirmText: 'Yes, delete permanently',
        cancelText: 'No, cancel'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.diastolic.reset();
        this.systolic.reset();
        this.ssn.reset();
        this.date = new Date();
      }
    });
  }


  markSeen() {
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


  openNewPatientForm(): void {
    const dialogRef = this.dialog.open(NewPatientFormComponent, {
      data: {
        title: `Create a new patient`,
        confirmText: 'Yes, create this patient',
        cancelText: 'No, cancel'
      },
      width: '500px',
      
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log("patient recieved in dashboard")
        console.log(result)

        if (result.name && result.mail && result.ssn) {
          console.log("create patient now")
          // return result to post create patient.
          // add measurement empty array ?
        }
      }
    });
  }






  //#region "Properties"
  date: Date = new Date();

  contrySelection: string [] = ['Sweden', 'Norway', 'Denmark', 'Finland'];
  selectedCountry: string = 'Denmark';

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

  async ngOnInit(): Promise<void> {

    const request = await this.http.getPatients()
      .then((request) => request)
      .catch((request) => request);
      console.log("request = ")
      console.log(request)
    if (!request) {
      this.dialog.open(ErrorDialogComponent, { data: { title: 'An error occured while submitting the form', errors: ['Please try again later'] } });
      this.spinner = false;
      return;
    }

    if (request.status === 403) {
      this.dialog.open(ErrorDialogComponent, { data: { title: 'An error occured while submitting the form', errors: ['This service is not available in your country'] } });
      this.spinner = false;
      return;
    }
    if (request.status === 200) {
      const data = await request.json();
      console.log("data = ")
      console.log(data)
      this.patients = data;
      return;
    }
  }

  setDate(event: MatDatepickerInputEvent<Date>) {
    this.date = event.value ? event.value : new Date();
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
        hasBeenSeen: false,
      };

      const request = await this.http.postMeassurements(measurement, this.selectedCountry)
      .then((request) =>  request)
      .catch((request) =>  request);
      
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

