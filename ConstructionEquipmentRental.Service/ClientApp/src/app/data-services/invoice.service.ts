import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments';
import { CartItem } from '../models/cart-item';
import { SignalRService } from './signalr.service';
import { OrderItem, OrderItems } from '../models/api/order-items';
import { map, mergeMap } from 'rxjs/operators';
import { saveAs } from 'file-saver';
import { Observable } from 'rxjs';

export const BASE_URL = `${environment.invoiceApiEndpoint}`;

@Injectable()
export class InvoiceService {
  constructor(private http: HttpClient, private signalRService: SignalRService) { }

  downloadInvoice(cartItems: CartItem[]): Observable<boolean> {
    this.signalRService.addInvoiceGeneratedListener();

    const orderItems: OrderItems = {
      orderItems: cartItems.map(cartItem => this.mapToOrderItem(cartItem))
    };

    return this.http.post<string[]>(BASE_URL, orderItems)
      .pipe(
        mergeMap(() =>
        this.signalRService.messageReceived
          .pipe(
            map(response => {
              const blob = new Blob([response], {type: 'text/plain;charset=utf-16'});
              saveAs(blob, 'invoice.txt');
              return true;
            })
          )
        )
      );
  }

  private mapToOrderItem(cartItem: CartItem): OrderItem {
    return {
      equipmentName: cartItem.name,
      rentalDays: cartItem.rentalDays
    };
  }
}
