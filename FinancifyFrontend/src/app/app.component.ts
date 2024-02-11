import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: `./app.component.html`,
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  title = 'Financify';

  constructor(
    router : Router,
    client : HttpClientModule
  ) {

  }

  ngOgInit() {

  }

}
