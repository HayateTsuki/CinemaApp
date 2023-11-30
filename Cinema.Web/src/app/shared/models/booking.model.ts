import { Screening } from "./screening.model";

export class Booking{
    row: number;
    seat: number;
    isConfirmed: boolean;
    date: Date;
    screening: Screening;
}
