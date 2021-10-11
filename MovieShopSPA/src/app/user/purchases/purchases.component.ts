import { Component, Input, OnInit } from '@angular/core';
import { MovieCard } from 'src/app/shared/models/movieCard';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {
  
  @Input() movieCard!: MovieCard;
  constructor() { }

  ngOnInit(): void {
  }

}
