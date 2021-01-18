import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  constructor(private authService: MsalService, private client: HttpClient) { }

  ngOnInit(): void {
    this.authService.handleRedirectObservable().subscribe({
      next: (result: AuthenticationResult) => {
        if (result) {
          this.authService.instance.setActiveAccount(result.account);
          console.log(result);
        }
      },
      error: (error) => console.log(error)
    });
  }

  loadFromDb() {
    this.client.get('https://web-a56tu4bzkgjjw.azurewebsites.net/api/database').subscribe(data =>
      console.log(data));
  }

}
