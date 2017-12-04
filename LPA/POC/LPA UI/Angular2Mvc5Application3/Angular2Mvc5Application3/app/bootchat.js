"use strict";
var platform_browser_dynamic_1 = require('@angular/platform-browser-dynamic');
var app_service_chat_1 = require('./services/app.service.chat');
var http_1 = require('@angular/http');
var chat_component_1 = require('./components/chat/chat.component');
//enableProdMode();
platform_browser_dynamic_1.bootstrap(chat_component_1.ChatComponent, [http_1.HTTP_PROVIDERS, app_service_chat_1.AppServiceChat]);
//# sourceMappingURL=bootchat.js.map