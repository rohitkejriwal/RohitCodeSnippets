"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
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
var http_1 = require('@angular/http');
var HttpHelpers_1 = require('../utils/HttpHelpers');
require('rxjs/Rx');
var AppServiceChat = (function (_super) {
    __extends(AppServiceChat, _super);
    function AppServiceChat(http) {
        var _this = this;
        _super.call(this, http);
        this.http = http;
        this._getChatListUrl = 'Chat/GetChatList';
        this._getChatResponseUrl = 'Chat/GetChatResponse';
        this.getaction(this._getChatListUrl).subscribe(function (result) {
            _this._chatList = result;
        }, function (error) { return _this.errormsg = error; });
        this.isProcessing = false;
        this.showLikeDislikeButtons = false;
    }
    Object.defineProperty(AppServiceChat.prototype, "chatlist", {
        get: function () {
            return this._chatList;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(AppServiceChat.prototype, "suggestions", {
        get: function () {
            return this.Suggestions;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(AppServiceChat.prototype, "context", {
        get: function () {
            return this.ContextID;
        },
        enumerable: true,
        configurable: true
    });
    AppServiceChat.prototype.AddChatList = function (chat) {
        this._chatList.push({
            Message: chat.Message,
            Type: chat.Type
        });
        chat.ContextID = this.ContextID;
        this.isProcessing = true;
        chat.LinkedChatID = this.linkedChatId;
        this.showLikeDislikeButtons = false;
        var status = document.getElementById('status');
        status.innerHTML = "Sent";
        var context = this;
        this.Suggestions = null;
        setTimeout(function () {
            var status = document.getElementById('status');
            status.innerHTML = "Delivered";
            context.postaction(chat, context._getChatResponseUrl).subscribe(function (result) {
                if (!result.haserror) {
                    context._chatList.push(result.element);
                    context.Suggestions = result.element.Suggestions;
                    context.ContextID = result.element.ContextID;
                    context.isProcessing = false;
                    context.showLikeDislikeButtons = context.ShowLikeDislike(result.element.Message);
                    context.linkedChatId = result.element.LinkedChatID;
                    status.innerHTML = "";
                }
            }, function (error) { return context.errormsg = error; });
        }, 500);
    };
    AppServiceChat.prototype.FormatChatMessage = function (chat) {
        if (chat.Message == '<i class="glyphicon glyphicon-thumbs-up">') {
            chat.Message = 'Like';
        }
        else if (chat.Message == '<i class="glyphicon glyphicon-thumbs-down">') {
            chat.Message = 'Dislike';
        }
        return chat;
    };
    AppServiceChat.prototype.ShowLikeDislike = function (message) {
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
    };
    AppServiceChat = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], AppServiceChat);
    return AppServiceChat;
}(HttpHelpers_1.HttpHelpers));
exports.AppServiceChat = AppServiceChat;
//# sourceMappingURL=app.service.chat.js.map