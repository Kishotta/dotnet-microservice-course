import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CatalogService } from '../services/catalog.service';

@Component({
  selector: 'app-catalog-edit-form',
  templateUrl: './catalog-edit-form.component.html',
  styleUrls: ['./catalog-edit-form.component.scss']
})
export class CatalogEditFormComponent implements OnInit {
    id!: string;
    form: FormGroup;

    constructor(
        private _formBuilder: FormBuilder,
        private _catalogService: CatalogService,
        private _dialog: MatDialogRef<CatalogEditFormComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        this.id = data.id;
        this.form = this._formBuilder.group({
            name: '',
            description: '',
            price: 0
        });
    }

    ngOnInit(): void {
      this.form.patchValue(this.data);
    }

    onFormSubmit() {
        if(this.form.valid) {
            this._catalogService
                .updateCatalogItem(this.id, this.form.value)
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
}
