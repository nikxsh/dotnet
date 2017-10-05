//Core
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
//Model
import { MessageHandler } from '../../Models/common.model';
import { BaseInventory } from '../../models/inventory.model';
//Service
import { LocalStorageService } from '../../services/storage.service';
import { InventoryService } from '../../services/inventory.service';
//Helper
import { HandleError } from '../../helpers/error.utility';

@Component({
  selector: 'app-inventorywip',
  templateUrl: './inventorywip.component.html',
  styleUrls: ['./inventorywip.component.css']
})
export class InventoryWipComponent implements OnInit {

  private title: string = "WIP Inventory Dashboard";
  private progressing: false;
  private message: MessageHandler = new MessageHandler();
  private lstInventory: BaseInventory[] = [];
  private currentDate: Date;
  private minDate: string;

  constructor(
    private router: Router,
    private storageService: LocalStorageService,
    private inventoryService: InventoryService) {
  }

  ngOnInit() {
    this.message.text = '';
    this.getAllWipInventory();
  }

  private getAllWipInventory() {
    try {
      this.message.text = '';
      this.inventoryService._getAllWipProductInventory()
        .then(result => {
          if (result.status == 1) {
            this.lstInventory = result.data.map(x => new BaseInventory(x.id, x.productCode, x.productName, 0, 0, x.wip, x.available, x.uom, x.date, 0));
          }
          else {
            this.message.text = result.message;
            this.message.type = 2;
          }
        },
        error => {
          HandleError.handle(error);
        });
    }
    catch (e) {
      HandleError.handle(e);
    }
  }
}
