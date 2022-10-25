import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';
@Component({
  selector: 'app-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.scss']
})
export class EditContactComponent implements OnInit {
  editContactForm: contactForm = new contactForm();
  editPhoneForm: phoneForm = new phoneForm();


  @ViewChild("contactForm")
  contactForm!: NgForm;

  isSubmitted: boolean = false;
  contactId: any;

  constructor(private toastr: ToastrService, private route: ActivatedRoute, private router: Router,
    private httpProvider: HttpProviderService) { }

  ngOnInit(): void {
    this.contactId = this.route.snapshot.params['Id'];
    this.getContactDetailById();
  }
  getContactDetailById() {
    this.httpProvider.getContact(this.contactId).subscribe((data: any) => {
      if (data != null && data.body != null) {
        var resultData = data.body;
        if (resultData) {
          this.editContactForm.Name.Id = resultData.Name.Id;
          this.editContactForm.Name.First = resultData.Name.First;
          this.editContactForm.Name.Middle = resultData.Name.Middle;
          this.editContactForm.Name.Last = resultData.Name.Last;
          this.editContactForm.Name.Email = resultData.Name.Email;
          this.editContactForm.Address.Street = resultData.Address.Street;
          this.editContactForm.Address.City = resultData.Address.City;
          this.editContactForm.Address.State = resultData.Address.State;
          this.editContactForm.Address.Zip = resultData.Address.Zip;
          debugger;
          resultData.Phone.forEach((phone: any) => {
            debugger;
            if (phone.Type == 'Home') {
              this.editPhoneForm.Home = phone.Number
            }

            if (phone.Type == 'Work') {
              this.editPhoneForm.Work = phone.Number
            }

            if (phone.Type == 'Mobile') {
              this.editPhoneForm.Mobile = phone.Number
            }
          });
        }
      }
    },
      (error: any) => { });
  }

  EditContact(isValid: any) {
    this.isSubmitted = true;

    if (this.editPhoneForm.Home != "") {
      this.editContactForm.Phone.push({ Number: this.editPhoneForm.Home, PhoneTypeId: 1 })
    }
    if (this.editPhoneForm.Work != "") {
      this.editContactForm.Phone.push({ Number: this.editPhoneForm.Work, PhoneTypeId: 2 })
    }
    if (this.editPhoneForm.Mobile != "") {
      this.editContactForm.Phone.push({ Number: this.editPhoneForm.Mobile, PhoneTypeId: 3 })
    }

    if (isValid) {
      this.httpProvider.updateContact(this.contactId, this.editContactForm).subscribe(async data => {
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
  Id: number = 0;
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