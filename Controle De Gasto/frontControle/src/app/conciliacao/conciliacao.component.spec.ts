import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConciliacaoComponent } from './conciliacao.component';

describe('ConciliacaoComponent', () => {
  let component: ConciliacaoComponent;
  let fixture: ComponentFixture<ConciliacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConciliacaoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConciliacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
