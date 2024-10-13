import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WelcomeComponent } from './Welcome.component';
import { BookListComponent } from '../Model/BookList/book-list.component';


const routes: Routes = [
  { path: '', component: WelcomeComponent },
  { path: 'library', component: BookListComponent },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
