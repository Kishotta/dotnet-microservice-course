import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { InventoryService } from '../services/inventory.service';

@Component({
  selector: 'app-inventory-grant-form',
  templateUrl: './inventory-grant-form.component.html',
  styleUrls: ['./inventory-grant-form.component.scss']
})
export class InventoryGrantFormComponent implements OnInit {
    id!: string;
    form: FormGroup;

    constructor(
        private _formBuilder: FormBuilder,
        private _inventoryService: InventoryService,
        private _dialog: MatDialogRef<InventoryGrantFormComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        this.id = data.id;
        this.form = this._formBuilder.group({
            userId: '',
            quantity: 0
        });
    }

    ngOnInit(): void {
      this.form.patchValue(this.data);
    }

    onFormSubmit() {
        if (!this.form.valid) return;

        const data = {
            catalogItemId: this.id,
            ...this.form.value
        }
        this._inventoryService
            .grantInventoryItem(data)
            .subscribe({
                next: () => {
                    this.form.reset();
                    this._dialog.close(true);
                },
                error: (err) => {
                    console.log(err);
                }
            });
    }
}
