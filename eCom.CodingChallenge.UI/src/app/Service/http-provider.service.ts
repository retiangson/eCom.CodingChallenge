import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WebApiService } from './web-api.service';

var apiUrl = "https://localhost:7023/";

var httpLink = {
  getAllContacts: apiUrl + "api/contact/contacts",
  createContact: apiUrl + "api/contact/contacts",
  updateContact: apiUrl + "api/contact/contacts/",
  getContact: apiUrl + "api/contact/contacts/",
  deleteContact: apiUrl + "api/contact/contacts/",
  getCallList: apiUrl + "api/contact/contacts/call-list",
}
@Injectable({
  providedIn: 'root'
})
export class HttpProviderService {

  constructor(private webApiService: WebApiService) { }

  public getAllContact(): Observable<any> {
    return this.webApiService.get(httpLink.getAllContacts);
  }

  public createContact(model: any): Observable<any> {
    return this.webApiService.post(httpLink.createContact, model);
  }  

  public updateContact(id: any, model: any): Observable<any> {
    return this.webApiService.put(httpLink.updateContact + id, model);
  }  

  public getContact(model: any): Observable<any> {
    return this.webApiService.get(httpLink.getContact + model);
  }

  public deleteContact(model: any): Observable<any> {
    return this.webApiService.delete(httpLink.deleteContact + model);
  }

  public getCallList(): Observable<any> {
    return this.webApiService.get(httpLink.getCallList);
  }  
}
