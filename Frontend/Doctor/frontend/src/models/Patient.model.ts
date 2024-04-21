import { IMeasurement } from "./Measurement.model";

export interface IPatient {
  mail: string,
  name: string,
  measurements: IMeasurement[],
  ssn: string,
}
