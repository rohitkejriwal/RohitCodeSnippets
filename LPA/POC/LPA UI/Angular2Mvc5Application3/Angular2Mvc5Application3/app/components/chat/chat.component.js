"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var app_service_chat_1 = require('../../services/app.service.chat');
var ChatComponent = (function () {
    function ChatComponent(_appService) {
        this._appService = _appService;
    }
    ChatComponent.prototype.ngOnInit = function () {
        this.scrollToBottom();
    };
    ChatComponent.prototype.ngAfterViewChecked = function () {
        this.scrollToBottom();
    };
    ChatComponent.prototype.scrollToBottom = function () {
        try {
            this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
        }
        catch (err) { }
    };
    Object.defineProperty(ChatComponent.prototype, "chatlist", {
        get: function () {
            return this._appService.chatlist;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ChatComponent.prototype, "suggestions", {
        get: function () {
            return this._appService.suggestions;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ChatComponent.prototype, "context", {
        get: function () {
            return this._appService.context;
        },
        enumerable: true,
        configurable: true
    });
    ChatComponent.prototype.suggestionclick = function (suggestion) {
        if (suggestion == 'Like') {
            suggestion = '<i class="glyphicon glyphicon-thumbs-up">';
        }
        else if (suggestion == 'Dislike') {
            suggestion = '<i class="glyphicon glyphicon-thumbs-down">';
        }
        this._appService.AddChatList({
            Message: suggestion,
            Type: "Query"
        });
    };
    ChatComponent.prototype.isQuery = function (itemType) {
        if (itemType != 'Query')
            return 'none';
        else
            return 'flex';
    };
    ChatComponent.prototype.isResponse = function (itemType) {
        if (itemType != 'Response')
            return 'none';
        else
            return 'flex';
    };
    ChatComponent.prototype.isImageTypeResponse = function (itemType) {
        if (itemType == 'image' || itemType == 'IMAGE')
            return true;
        return false;
    };
    ChatComponent.prototype.isTextTypeResponse = function (itemType) {
        if (itemType == 'text' || itemType == 'TEXT')
            return true;
        return false;
    };
    ChatComponent.prototype.addChat = function (item) {
        if (item.valid) {
            var query = this.newQuery;
            this.newQuery = "";
            this._appService.AddChatList({
                Message: query,
                Type: "Query"
            });
            this.newQuery = "";
        }
    };
    ChatComponent.prototype.addChatFromJS = function (item) {
        var query = document.getElementById('chatTextBox').value;
        var authToken = document.getElementById('authToken').value;
        this.newQuery = "";
        this._appService.AddChatList({
            Message: query,
            Type: "Query",
            AuthToken: authToken
        });
        this.newQuery = "";
    };
    __decorate([
        core_1.ViewChild('ChatListContainer'), 
        __metadata('design:type', core_1.ElementRef)
    ], ChatComponent.prototype, "myScrollContainer", void 0);
    ChatComponent = __decorate([
        core_1.Component({
            selector: 'chatlist',
            templateUrl: './app/components/chat/chat.component.html',
            styleUrls: ['./app/components/chat/chat.component.css']
        }), 
        __metadata('design:paramtypes', [app_service_chat_1.AppServiceChat])
    ], ChatComponent);
    return ChatComponent;
}());
exports.ChatComponent = ChatComponent;
//# sourceMappingURL=chat.component.js.map