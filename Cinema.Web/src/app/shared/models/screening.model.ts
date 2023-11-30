import { Hall } from "./hall.model";
import { Movie } from "./movie.model";

export class Screening{
    id: number;
    date: Date;
    price: number;
    hall: Hall;
    movie: Movie;
}