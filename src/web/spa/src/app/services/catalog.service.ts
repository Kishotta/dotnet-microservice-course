import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  constructor(private _http: HttpClient) { }

  getCatalogItems(): Observable<any> {
    return this._http.get('https://localhost:5001/items');
  }

  createCatalogItem(data: any): Observable<any> {
    return this._http.post('https://localhost:5001/items', data);
  }

  updateCatalogItem(id: string, data: any): Observable<any> {
    return this._http.put(`https://localhost:5001/items/${id}`, data);
  }

  deleteCatalogItem(id: string): Observable<any> {
    return this._http.delete(`https://localhost:5001/items/${id}`);
  }
}
