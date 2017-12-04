import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpHelpers } from '../utils/HttpHelpers';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

@Injectable()
export class AppServiceChat extends HttpHelpers {

    private _getChatListUrl = 'Chat/GetChatList';

    private _getChatResponseUrl = 'Chat/GetChatResponse';

    private _chatList: Models.Chat[];
    private Suggestions: string[];
    private ContextID: string;
    public isProcessing: boolean;
    private linkedChatId: string;

    public showLikeDislikeButtons: boolean;

    constructor(private http: Http) {
        super(http);

        this.getaction<Models.Chat[]>(this._getChatListUrl).subscribe(
            result => {
                this._chatList = result;
            },
            error => this.errormsg = error);

        this.isProcessing = false;
        this.showLikeDislikeButtons = false;
    }

    get chatlist(): Models.Chat[] {
        return this._chatList;
    }

    get suggestions(): string[] {
        return this.Suggestions;
    }

    get context(): string {
        return this.ContextID;
    }

    AddChatList(chat: Models.Chat) {
        this._chatList.push(<Models.Chat>{
            Message: chat.Message,
            Type: chat.Type
        });
        chat.ContextID = this.ContextID;
        this.isProcessing = true;
        chat.LinkedChatID = this.linkedChatId;
        this.showLikeDislikeButtons = false;
        var status = (<HTMLInputElement>document.getElementById('status'));
        status.innerHTML = "Sent";

        var context = this;
        this.Suggestions = null;

        setTimeout(function () {
            var status = (<HTMLInputElement>document.getElementById('status'));
            status.innerHTML = "Delivered";

            context.postaction(chat, context._getChatResponseUrl).subscribe(
            result => {
                if (!result.haserror) {
                    context._chatList.push(result.element);
                    context.Suggestions = result.element.Suggestions;
                    context.ContextID = result.element.ContextID;
                    context.isProcessing = false;
                    context.showLikeDislikeButtons = context.ShowLikeDislike(result.element.Message);
                    context.linkedChatId = result.element.LinkedChatID;
                    status.innerHTML = "";
                }
                }, error => context.errormsg = error);
        }, 500);
    }

    FormatChatMessage(chat: Models.Chat) {
        if (chat.Message == '<i class="glyphicon glyphicon-thumbs-up">') {
            chat.Message = 'Like';
        }
        else if (chat.Message == '<i class="glyphicon glyphicon-thumbs-down">') {
            chat.Message = 'Dislike';
        }
        return chat;
    }

    ShowLikeDislike(message: string) {
        if (this.Suggestions != null) {
            return false;
        }
        else if (this.ContextID != "") {
            return false;
        }
        else if (message == "Glad you like it" || message == "OK" || message == "Can you share a bit more info about what you didn't like? I will pass it on so that we can do better next time" || message == "Thanks for explaining the issue") {
            return false;
        }
        else {
            return true;
        }
    }
}