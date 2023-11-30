import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStoreService } from '../../services/local-store.service';

@Component({
  selector: 'cinema-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  constructor(private router: Router, private localStoreService: LocalStoreService) { }

  ngOnInit(): void {
  }

  logout() {
    this.localStoreService.removeToken();
    this.router.navigate(["/identity"]);
  }
}
