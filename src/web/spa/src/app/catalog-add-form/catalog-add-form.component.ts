import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CatalogService } from '../services/catalog.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-catalog-add-form',
  templateUrl: './catalog-add-form.component.html',
  styleUrls: ['./catalog-add-form.component.scss']
})
export class CatalogAddFormComponent {
    form: FormGroup;

    constructor(
        private _formBuilder: FormBuilder,
        private _catalogService: CatalogService,
        private _dialog: MatDialogRef<CatalogAddFormComponent>
    ) {
        this.form = this._formBuilder.group({
            name: '',
            description: '',
            price: 0
        });
    }

    onFormSubmit() {
        if(!this.form.valid) return;

        this._catalogService
            .createCatalogItem(this.form.value)
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
