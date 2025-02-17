import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { provideHttpClient } from '@angular/common/http'; // 引入 provideHttpClient
import { AppComponent } from './app.component';


@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule
    ],
  providers: [
    provideClientHydration(withEventReplay()),
    provideHttpClient()
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
