import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieDetialsComponent } from './movie-detials.component';

describe('MovieDetialsComponent', () => {
  let component: MovieDetialsComponent;
  let fixture: ComponentFixture<MovieDetialsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovieDetialsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieDetialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
