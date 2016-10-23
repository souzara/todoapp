import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule} from '@angular/Http';
import { MaterializeDirective } from 'angular2-materialize';
import { AppComponent } from './app.component';
import { TodosComponent } from './components/todos.component';
import { TodoService } from './services/todos.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  declarations: [
    AppComponent, TodosComponent, MaterializeDirective
  ],
  providers: [TodoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
