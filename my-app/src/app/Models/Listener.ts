import { Injectable } from '@angular/core';

@Injectable()
export class Service {
    type: number = 0;
    name: string = null;
    last: string = null;
}


@Injectable()
export class Listener extends Service {
    port: number = 0;
    lastMessages: Message[] = [];
    filtered: Message[] = [];
}

@Injectable()
    export class IMessage{}

@Injectable()
export class Message extends IMessage {
    constructor(
        public type: number,
        public message: string) { super();}
}