import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterOutlet } from '@angular/router';
import { ProdutosListComponent } from "./components/produtos-list/produtos-list.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ProdutosListComponent, MatToolbarModule, MatButtonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'frontend';
}
