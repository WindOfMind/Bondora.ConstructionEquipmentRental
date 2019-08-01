import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments';

export const BASE_URL = `${environment.invoiceHubEndpoint}`;

@Injectable()
export class SignalRService {
  messageReceived = new EventEmitter<string>();

  private hubConnection: signalR.HubConnection;

  public addInvoiceGeneratedListener(): void {
    this.startConnection().then( () =>
      this.hubConnection.on('InvoiceGenerated', (data: string) => {
        this.messageReceived.emit(data);
        this.hubConnection.stop();
      })
    );
  }

  private startConnection(): Promise<void> {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(BASE_URL)
                            .build();

    return this.hubConnection.start();
  }
}
