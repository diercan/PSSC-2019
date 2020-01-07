import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MatSnackBarModule } from '@angular/material'
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { MatIconModule, MatIcon } from '@angular/material/icon/';
import { HttpClientModule } from '@angular/common/http'
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSliderModule } from '@angular/material/slider';
import { MatDialogModule } from '@angular/material/dialog';
import { RouterModule } from '@angular/router';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {MatRadioModule} from '@angular/material/radio';
import {MatFormFieldModule} from '@angular/material';
import { MenuComponent } from './menu/menu.component';
@NgModule({
    imports: [
      CommonModule,
      MatSelectModule,
      MatInputModule,
      MatButtonModule,
      MatStepperModule,
      MatSlideToggleModule,
      MatButtonToggleModule,
      MatDatepickerModule,
      MatNativeDateModule,
      MatDialogModule,
      ReactiveFormsModule,
      FormsModule,
      MatIconModule,
      HttpClientModule,
      MatChipsModule,
      MatTooltipModule,
      MatAutocompleteModule,
      MatTabsModule,
      MatSliderModule,
      MatDialogModule,
      RouterModule,
      MatMenuModule,
      MatProgressSpinnerModule,
      MatProgressBarModule,
      MatRadioModule,
      MatSnackBarModule,
      MatFormFieldModule,
      
      
    ],
    exports: [
      MatSelectModule,
      MatInputModule,
      MatButtonModule,
      MatStepperModule,
      MatSlideToggleModule,
      MatButtonToggleModule,
      MatDatepickerModule,
      MatNativeDateModule,
      MatDialogModule,
      ReactiveFormsModule,
      MatIconModule,
      HttpClientModule,
      MatChipsModule,
      MatTooltipModule,
      MatAutocompleteModule,
      MatTabsModule,
      MatSliderModule,
      MatDialogModule,
      MatProgressSpinnerModule,
      MatProgressBarModule,
      MatRadioModule,
      MatSnackBarModule,
      MatFormFieldModule,
      MenuComponent
    ],
    entryComponents: [
      
    ],
    declarations: [MenuComponent]
  })
  export class SharedModule { }
  