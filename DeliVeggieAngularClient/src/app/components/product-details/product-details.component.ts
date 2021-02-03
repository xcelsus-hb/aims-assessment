import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {


  currentProduct: Product = {
    name: '',
    description: '',
    price: 0
  };

  constructor(private productsService: ProductsService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.getProduct(this.route.snapshot.params.id);
  }


  getProduct(id: string): void {
    this.productsService.get(id)
      .subscribe(
        data => {
          this.currentProduct = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }

}
