import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.scss']
})
export class AddContactComponent implements OnInit {
  addContactForm: contactForm = new contactForm();
  addPhone: phoneForm = new phoneForm()

  @ViewChild("contactForm")
  contactForm!: NgForm;
  isSubmitted: boolean = false;
  constructor(private router: Router, private httpProvider: HttpProviderService, private toastr: ToastrService) { }

  ngOnInit(): void { }

  AddContact(isValid: any) {
    this.isSubmitted = true;
    if (isValid) {

      if (this.addPhone.Home != "") {
        this.addContactForm.Phone.push({ Number: this.addPhone.Home, PhoneTypeId: 1 })
      }
      if (this.addPhone.Work != "") {
        this.addContactForm.Phone.push({ Number: this.addPhone.Work, PhoneTypeId: 2 })
      }
      if (this.addPhone.Mobile != "") {
        this.addContactForm.Phone.push({ Number: this.addPhone.Mobile, PhoneTypeId: 3 })
      }

      this.httpProvider.createContact(this.addContactForm).subscribe(async data => {
        if (data.status == 200) {
          this.toastr.success("Contact Sucessfuly Saved");
          setTimeout(() => {
            this.router.navigate(['/Home']);
          }, 500);
        }
      },
        async error => {
          this.toastr.error(error.message);
          setTimeout(() => {
            this.router.navigate(['/Home']);
          }, 500);
        });
    }
  }
}

export class contactForm {
  Name: Name = new Name;
  Address: Address = new Address;
  Phone: Phone[] = [];
}
export class phoneForm {
  Home: string = "";
  Work: string = "";
  Mobile: string = "";
}

export class Name {
  First: string = "";
  Middle: string = "";
  Last: string = "";
  Email: string = "";
}

export class Address {
  Street: string = "";
  City: string = "";
  State: string = "";
  Zip: string = "";
}

export class Phone {
  Number: string = "";
  PhoneTypeId: number = 0
}