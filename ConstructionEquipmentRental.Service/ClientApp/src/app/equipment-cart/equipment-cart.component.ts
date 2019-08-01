import { Component, Input } from '@angular/core';
import { CartItem } from '../models/cart-item';
import { InvoiceService } from '../data-services/invoice.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-equipment-cart',
  templateUrl: './equipment-cart.component.html',
  styleUrls: ['./equipment-cart.component.scss']
})
export class EquipmentCartComponent {
  @Input() cartItems: CartItem[];

  private downloadingInvoice = false;

  constructor(private invoiceService: InvoiceService) { }

  get disabled(): boolean {
    return !this.cartItems || this.cartItems.length === 0 || this.downloadingInvoice;
  }

  onGetInvoiceButtonClicked(): void {
    if (!this.cartItems || this.cartItems.length === 0) {
      return;
    }

    this.downloadingInvoice = true;

    this.invoiceService.downloadInvoice(this.cartItems)
      .pipe(first())
      .subscribe(result => {
        if (result) {
          this.downloadingInvoice = false;
        }
      });
  }
}
