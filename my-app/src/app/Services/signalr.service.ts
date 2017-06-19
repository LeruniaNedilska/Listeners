import { Injectable, EventEmitter } from '@angular/core';
import { Message,  IMessage } from '../Models/Listener';
import { BehaviorSubject } from 'rxjs';


declare var $: any;

@Injectable()
export class SignalRService {
    private hub: any;
    public messageReceived: EventEmitter<{}>;
    public messagesLoaded: EventEmitter<{}>;
    public limitSet: EventEmitter<number>;
    public listenerCount: number = 2;
    constructor() {
        let self = this;
        this.hub = $.connection.listenerHub;
        this.messageReceived = new EventEmitter<{}>();
        this.messagesLoaded = new EventEmitter<{}>();
        this.limitSet = new EventEmitter<number>();
        this.registerOnServerEvents();
    }
    public initMessages(): void {
        let self = this;
            if ($.connection.hub.state === $.signalR.connectionState.disconnected) {
                $.connection.hub.start().done(function () {
                    self.hub.server.getLimit();
                    for (let i = 0; i < self.listenerCount; i++) {
                        self.hub.server.getAll(i);
                    }
                });
            }
    }

    private registerOnServerEvents(): void {
        let self = this;
        this.hub.client.addMessage = (message: string, type: number, name: string) => {
            self.hub.server.addMessage(message, type, name);
        };

        this.hub.client.messageAdded = (message: string, type: number, index: number) => {
            let newMessage = new Message(type, message);
            self.messageReceived.emit({ message: newMessage, index: index });
        };

        this.hub.client.messageAll = (allMessages: IMessage[], listener: number) => {
            self.messagesLoaded.emit({ messages: allMessages, index: listener });
        };

        this.hub.client.setLimit = (limit: number) => {
            self.limitSet.emit(limit);
        }
    }
}