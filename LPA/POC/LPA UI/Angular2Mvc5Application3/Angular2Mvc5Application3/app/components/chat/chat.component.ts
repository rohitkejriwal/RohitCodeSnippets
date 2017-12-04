import { Component, AfterViewChecked, ElementRef, ViewChild, OnInit } from '@angular/core';
import { NgControl } from '@angular/common';
import { AppServiceChat } from '../../services/app.service.chat';
import { Angular2AutoScroll } from 'angular2-auto-scroll/lib/angular2-auto-scroll.directive';

@Component({
    selector: 'chatlist',
    templateUrl: './app/components/chat/chat.component.html',
    styleUrls: ['./app/components/chat/chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewChecked {
    newQuery: string;

    constructor(private _appService: AppServiceChat) {
    }

    @ViewChild('ChatListContainer') private myScrollContainer: ElementRef;

    ngOnInit() {
        this.scrollToBottom();
    }

    ngAfterViewChecked() {
        this.scrollToBottom();
    }

    scrollToBottom(): void {
        try {
            this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
        } catch (err) { }
    }    

    get chatlist(): Models.Chat[] {
        return this._appService.chatlist;
    }

    get suggestions(): string[] {
        return this._appService.suggestions;
    }

    get context(): string {
        return this._appService.context;
    }

    suggestionclick(suggestion: string) {

        if (suggestion == 'Like') {
            suggestion = '<i class="glyphicon glyphicon-thumbs-up">';
        }
        else if (suggestion == 'Dislike') {
            suggestion = '<i class="glyphicon glyphicon-thumbs-down">';
        }

        this._appService.AddChatList(<Models.Chat>{
            Message: suggestion,
            Type: "Query"
        });
    }

    isQuery(itemType: string) {
        if (itemType != 'Query')
            return 'none';
        else
            return 'flex';
    }

    isResponse(itemType: string) {
        if (itemType != 'Response')
            return 'none';
        else
            return 'flex';
    }

    isImageTypeResponse(itemType: string) {
        if (itemType == 'image' || itemType == 'IMAGE')
            return true;

        return false;
    }

    isTextTypeResponse(itemType: string) {
        if (itemType == 'text' || itemType == 'TEXT')
            return true;

        return false;
    }

    addChat(item: NgControl) {
        if (item.valid) {
            var query = this.newQuery;
            this.newQuery = "";
            this._appService.AddChatList(<Models.Chat>{
                Message: query,
                Type: "Query"
            });

            this.newQuery = "";
        }
    }

    addChatFromJS(item: Element) {
        var query = (<HTMLInputElement>document.getElementById('chatTextBox')).value;
        var authToken = (<HTMLInputElement>document.getElementById('authToken')).value;
        this.newQuery = "";
        this._appService.AddChatList(<Models.Chat>{
            Message: query,
            Type: "Query",
            AuthToken: authToken
        });
        this.newQuery = "";
    }
}