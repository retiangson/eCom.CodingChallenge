import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCallListComponent } from './view-call-list.component';

describe('ViewCallListComponent', () => {
  let component: ViewCallListComponent;
  let fixture: ComponentFixture<ViewCallListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewCallListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewCallListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
