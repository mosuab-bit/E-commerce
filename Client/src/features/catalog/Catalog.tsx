import { useEffect, useState } from "react";
import { Product } from "../../app/models/Product"
import ProductList from "./ProductList"

export default function Catalog() {
  const[products,setProducts] =useState<Product[]>([]);
  useEffect(()=>{
    fetch('http://localhost:5201/api/products')
    .then(response =>response.json())
    .then(data=>setProducts(data))
    .catch(error => console.error('Error fetching products:', error));
  },[])
  
  return (
    <>
      <ProductList products={products}/>
    </>
  )
}
