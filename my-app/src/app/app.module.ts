import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './Components/app.component';
import { ListenerComponent } from './Components/listener.component';
import { UdpListenerComponent } from './Components/udplistener.component';
import { TcpListenerComponent } from './Components/tcplistener.component';
import { ListComponent } from './Components/list.component';
import { ListenerService } from './Services/listener.service';
import { SignalRService } from "./Services/signalr.service";

@NgModule({
    declarations: [
        AppComponent,
        ListenerComponent,
        UdpListenerComponent,
        TcpListenerComponent,
        ListComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule
    ],
    providers: [
        ListenerService,
        SignalRService,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
