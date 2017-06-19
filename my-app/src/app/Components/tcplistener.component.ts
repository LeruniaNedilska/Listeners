import { Component, NgZone } from '@angular/core';
import { ListenerComponent } from './listener.component';
import { SignalRService } from "../Services/signalr.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ListenerService } from "../Services/listener.service";

@Component({
    selector: 'tcplistener',
    templateUrl: './listener.component.html'
})
export class TcpListenerComponent extends ListenerComponent {
    constructor(
        protected signalr: SignalRService,
        protected listenerService: ListenerService,
        protected zone: NgZone) {
        super(signalr, listenerService, zone);
        this.ind = 1;
        this.name = "TcpListener";
    }
}