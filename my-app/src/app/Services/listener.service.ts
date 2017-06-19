import { Injectable, EventEmitter } from '@angular/core';
import { Listener, Message, Service } from '../Models/Listener';
import { Observable } from "rxjs";
import { Http, RequestOptions } from "@angular/http";
import 'rxjs/Rx';
import { SignalRService } from "./signalr.service";

@Injectable()
export class ListenerService {
    constructor(private http: Http) {
    }
    public actionReceived: EventEmitter<{}> = new EventEmitter<{}>();

    sendLastActionTime(time: string, index: number): void {
        this.actionReceived.emit({ time: time, index: index });
    }

    getServices(): Observable<Service[]> {
        return this.http.get("/listeners")
            .map((response) => response.json());
    }
}