import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { Movie } from 'src/app/shared/models/movie';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }
  getTopGrossingMovies(): Observable<MovieCard[]> {

    // call API (HttpClient => XMLHttpRequest) => Http Get => JSON => typesrcipt model
      // Database => I/O bound, async & await
      // JS async promises => Observables, Observer pattern, publish/subscribe pattern
      // Observables => only wehn you subscribe then get notification => emit multiple values over time
      // Angular => Observables extensivelu in the framework
      // Angular

    // Always Angular services return Observables 
    return this.http.get<MovieCard[]>(`${environment.apiUrl}movies/toprevenue`);

    //return this.http.get('https://localhost:5001/api/Movies/toprevenue').pipe(map(resp => resp as Moviecard[]));

    // rxjs as js LINQ
    // map in JS RXJS => Select
    // Where => filter

  }

  getMovieDetails(id: number) {
    // call API method that returns movie detials
    return this.http.get<Movie>(`${environment.apiUrl}movies/${id}`);
  }
  
}
