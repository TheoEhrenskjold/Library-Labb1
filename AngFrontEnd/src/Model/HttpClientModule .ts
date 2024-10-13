import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AppComponent } from '../app/app.component';

@NgModule({
  declarations: [
    AppComponent,
    
  ],
  imports: [
    HttpClientModule,  
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
