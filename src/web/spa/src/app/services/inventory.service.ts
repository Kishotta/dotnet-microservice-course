import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

    constructor(private _http: HttpClient) { }

    grantInventoryItem(data: any): Observable<any> {
        return this._http.post('https://localhost:5002/items', data);
    }
}
