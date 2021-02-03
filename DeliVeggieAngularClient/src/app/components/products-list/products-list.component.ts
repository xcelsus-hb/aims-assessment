import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {


  products?: Product[];
  currentProduct?: Product;
  currentIndex = -1;
  name = '';

  constructor(private tutorialService: ProductsService) { }

  ngOnInit(): void {
    this.retrieveProducts();
  }


  retrieveProducts(): void {
    this.tutorialService.getAll()
      .subscribe(
        data => {
          this.products = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }

  setActiveTutorial(product: Product, index: number): void {
    this.currentProduct = product;
    this.currentIndex = index;
  }

}
