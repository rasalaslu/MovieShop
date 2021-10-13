import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { ReviewsComponent } from './reviews/reviews.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { UserComponent } from './user.component';
import { PurchasesComponent } from "./purchases/purchases.component";



@NgModule({
  declarations: [
    PurchasesComponent,
    FavoritesComponent,
    ReviewsComponent,
    UserComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
  ]
})
export class UserModule { }
