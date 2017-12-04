import {enableProdMode} from '@angular/core';
import {bootstrap}    from '@angular/platform-browser-dynamic'
import { AppServiceChat } from './services/app.service.chat';
import {HTTP_PROVIDERS} from '@angular/http';
import { ChatComponent } from './components/chat/chat.component';

//enableProdMode();
bootstrap(ChatComponent, [HTTP_PROVIDERS, AppServiceChat]); 