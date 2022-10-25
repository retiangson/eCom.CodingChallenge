import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpProviderService } from '../Service/http-provider.service';
import { WebApiService } from '../Service/web-api.service';

@Component({
  selector: 'app-view-call-list',
  templateUrl: './view-call-list.component.html',
  styleUrls: ['./view-call-list.component.scss']
})
export class ViewCallListComponent implements OnInit {

  callListView : any= [];
   
  constructor(public webApiService: WebApiService, private route: ActivatedRoute, private httpProvider : HttpProviderService) { }
  
  ngOnInit(): void {   
    this.getCallList();
  }

  getCallList() {       
    this.httpProvider.getCallList().subscribe((data : any) => {      
      if (data != null && data.body != null) {
        var resultData = data.body;
        if (resultData) {
          debugger;
          this.callListView = resultData;
        }
      }
    },
    (error :any)=> { }); 
  }

}