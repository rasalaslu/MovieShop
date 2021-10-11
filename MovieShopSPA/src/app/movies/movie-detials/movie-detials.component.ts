import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/shared/models/movie';
import { MovieService } from 'src/app/core/services/movie.service';

@Component({
  selector: 'app-movie-detials',
  templateUrl: './movie-detials.component.html',
  styleUrls: ['./movie-detials.component.css']
})
export class MovieDetialsComponent implements OnInit {

  id: number = 0;
  movie!: Movie;
  constructor(private route: ActivatedRoute, private movieService: MovieService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(p => {
      this.id = Number(p.get('id'));
      // get movie Details
      this.movieService.getMovieDetails(this.id).subscribe(resp => {
        this.movie = resp;
      });
      // see if movie is purchased by user, first check if user is authenticated
    });
  }

}
