import { Listener, Message, Service } from '../Models/Listener';
import { Component, OnInit, Input, style, NgZone } from '@angular/core';
import { ListenerService } from "../Services/listener.service";
import { SignalRService } from "../Services/signalr.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'listener-list',
    templateUrl: './list.component.html'
})
export class ListComponent implements OnInit {
    constructor(
        private listenerService: ListenerService,
        private zone: NgZone) {
    }
    serviceList: Service[] = [];
    activeService: number = 0;
    limit: number = 5;
    Normal: boolean = true;
    Warning: boolean = true;
    search: string = "";
    last: string[] = [];

    ngOnInit() {
        this.getList();
        this.subscribeToLastActions();
    }

    getList() {
        this.serviceList = [];
        this.listenerService.getServices()
            .subscribe(listeners => {
                this.serviceList = listeners;
            })
    }

    setService(index: number) {
        this.activeService = index;
    }

    subscribeToLastActions() {
        this.listenerService.actionReceived.subscribe(($event) => {
            this.last[$event.index] = $event.time;
        })
    }
}