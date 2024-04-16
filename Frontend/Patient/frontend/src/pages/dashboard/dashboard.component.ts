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

@Component({
  selector: 'op-dashboard',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatDatepickerModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './dashboard.html',
  providers:[provideNativeDateAdapter()],
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit{
  //#region "Properties"
  date: Date| null = null;

  diastolicErrorMessage: string = "";
  systolicErrorMessage: string = "";
  //#endregion

  //#region formControls
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
    this.numericValidator()
  ]);
  //#endregion

  constructor(public dialog: MatDialog) {}

  ngOnInit(): void {
    this.date = new Date();
  }

  setDate(event: MatDatepickerInputEvent<Date>) {
    this.date = event.value;
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

  submitForm() {
    const dialogRef = this.dialog.open(SuccessDialogComponent);
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

