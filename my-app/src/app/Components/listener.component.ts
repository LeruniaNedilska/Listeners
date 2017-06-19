import { Listener, Message, Service } from '../Models/Listener';
import { Component, OnInit, Input, style, NgZone } from '@angular/core';
import { ListenerService } from "../Services/listener.service";
import { SignalRService } from "../Services/signalr.service";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'listener',
    templateUrl: './listener.component.html'
})
export class ListenerComponent implements OnInit {
    constructor(
        protected signalr: SignalRService,
        protected listenerService: ListenerService,
        protected zone: NgZone) {
    }
    name: string = "Listener";
    ind: number = 0;
    lastMessages: Message[] = [];
    limit: number = 5;
    @Input() activeService: number;
    @Input() search: string = "";
    @Input() Normal: boolean = true;
    @Input() Warning: boolean = true;

    ngOnInit() {
        this.subscribeToEvents();
        this.signalr.initMessages();
    }

    getStyle(type: number): string {
        if (type == 0) {
            return "list-group-item list-group-item-info"
        }
        else {
            return "list-group-item list-group-item-warning"
        };
    }

    filtered(messages: Message[]) {
        if (this.search != "") {
            let input = this.search.toLowerCase();
            return messages.filter(message => {
                if (this.Normal && this.Warning) {
                    return message.message.toLowerCase().indexOf(input) > -1;
                }
                else {
                    return message.message.toLowerCase().indexOf(input) > -1 && message.type == +(!this.Normal);
                }
            })
        }
        else
            return messages.filter(message => {
                if (this.Normal && this.Warning) {
                    return message;
                }
                else {
                    return message.type == +(!this.Normal);
                }
            })
    }

    private subscribeToEvents(): void {
        let self = this;

        this.signalr.messageReceived.subscribe(($event) => {
            this.zone.run(() => {
                if ($event.index == self.ind) {
                    if (self.lastMessages.length >= self.limit) {
                        self.lastMessages.pop();
                    }
                    self.lastMessages.unshift($event.message);
                    self.listenerService.sendLastActionTime(Date.now.toString(), this.ind);

                }
            })
        })

        this.signalr.messagesLoaded.subscribe(($event) => {
            this.zone.run(() => {
                if ($event.index == self.ind) {
                    self.lastMessages = $event.messages.reverse();
                }
            })
        })

        this.signalr.limitSet.subscribe(limit => {
            this.zone.run(() => {
                self.limit = limit;
            })
        })
    }

}