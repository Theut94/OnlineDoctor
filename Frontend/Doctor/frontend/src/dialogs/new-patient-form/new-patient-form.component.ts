import { Component, Inject, Input } from '@angular/core';
import {
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
  MAT_DIALOG_DATA,
  MatDialog,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select'; // Import MatSelectModule
import { MatDatepickerInputEvent, MatDatepickerModule } from '@angular/material/datepicker'; // Import MatDatepickerModule
import { MatNativeDateModule } from '@angular/material/core'; // Import MatNativeDateModule
import { ErrorDialogComponent } from '../error-dialog/error-dialog.component';



@Component({
  selector: 'op-new-patient-form',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
  ],
  templateUrl: './new-patient-form.html',
  styleUrl: './new-patient-form.scss'
})
export class NewPatientFormComponent {

  placeholderBecauseAngularIsRetarded: any = {};
  patient: any = {};

  constructor(
    public dialogRef: MatDialogRef<NewPatientFormComponent>,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  cancel(): void {
    this.dialogRef.close(false);
  }

  submitForm(): void {
    console.log('Patient created = ', this.placeholderBecauseAngularIsRetarded);

    if (this.placeholderBecauseAngularIsRetarded.name && this.placeholderBecauseAngularIsRetarded.mail && this.placeholderBecauseAngularIsRetarded.ssn) {
      this.patient = this.placeholderBecauseAngularIsRetarded
      this.dialogRef.close(this.patient);
    } else {
      const dialogRefg = this.dialog.open(ErrorDialogComponent, { data: { title: 'An error occurred while submitting the form', errors: ['All fields are required'] } });
    }
  }
}
