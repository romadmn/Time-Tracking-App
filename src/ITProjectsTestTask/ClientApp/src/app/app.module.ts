import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {TaskEditFormComponent} from './shared/components/task-edit-form/task-edit-form.component';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/components/nav-menu/nav-menu.component';
import {MatTableModule} from '@angular/material/table';
import {ProjectsComponent} from './shared/components/projects/projects.component';
import {RefDirective} from './shared/directives/ref.directive';
import {ProjectEditFormComponent} from './shared/components/project-edit-form/project-edit-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatSelectModule} from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule, MatNativeDateModule, MatPaginatorModule, MatSortModule} from '@angular/material';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ProjectsComponent,
    ProjectEditFormComponent,
    RefDirective,
    TaskEditFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    MatTableModule,
    FormsModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {path: '', component: ProjectsComponent, pathMatch: 'full'},
    ]),
    BrowserAnimationsModule,
    MatSortModule,
    MatInputModule,
    MatPaginatorModule
  ],
  providers: [],
  entryComponents: [ProjectEditFormComponent, TaskEditFormComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
