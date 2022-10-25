import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddContactComponent } from './add-contact/add-contact.component';
import { EditContactComponent } from './edit-contact/edit-contact.component';
import { HomeComponent } from './home/home.component';
import { ViewCallListComponent } from './view-call-list/view-call-list.component';
import { ViewContactComponent } from './view-contact/view-contact.component';

const routes: Routes = [
  { path: '', redirectTo: 'Home', pathMatch: 'full'},
  { path: 'Home', component: HomeComponent },
  { path: 'ViewCallList', component: ViewCallListComponent },
  { path: 'AddContact', component: AddContactComponent },
  { path: 'EditContact/:Id', component: EditContactComponent },
  { path: 'ViewContact/:Id', component: ViewContactComponent } 
];
 @NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }