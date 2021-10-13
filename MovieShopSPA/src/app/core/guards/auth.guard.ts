import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanLoad {


  constructor(private authService: AuthenticationService, private router: Router) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.checkIsAuthenticated(state.url);
  }

  canLoad(route: Route): Observable<boolean> {
    const url = `/${route.path}`
    return this.checkIsAuthenticated(url);
  }

  checkIsAuthenticated(usr: string): Observable<boolean> {
    return this.authService.isLoggedIn.pipe(map(resp => {

      if (resp) {
        return true;
      }
      else {
        this.router.navigate(['account/login']);
        return false;
      }
    }))
  }

}