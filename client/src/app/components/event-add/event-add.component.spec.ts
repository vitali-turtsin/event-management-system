import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventAddComponent } from './event-add.component';

describe('AddEventComponent', () => {
  let component: EventAddComponent;
  let fixture: ComponentFixture<EventAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventAddComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EventAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
