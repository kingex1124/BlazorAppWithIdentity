import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {
  constructor(private http: HttpClient) { }
  title = 'AngularSSR';
  callApi() {
    const body = {
      secureAccount: 'kevanchen',
      secureString: '11241qaz@WSX',
      isADUserCheck: true
    };
    console.log("Tag");
    this.http.post('/api/calldotnetapi', body).subscribe({
      next: (response) => {
        console.log('API 回應：', response);
      },
      error: (error) => {
        console.error('呼叫 API 時發生錯誤：', error);
      },
      complete: () => {
        console.log('API 呼叫完成');
      },
    });
  }
}
