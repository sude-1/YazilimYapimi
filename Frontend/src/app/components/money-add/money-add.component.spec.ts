import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoneyAddComponent } from './money-add.component';

describe('MoneyAddComponent', () => {
  let component: MoneyAddComponent;
  let fixture: ComponentFixture<MoneyAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoneyAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoneyAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
