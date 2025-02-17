import { Product } from "../../app/models/Product"
import ProductList from "./ProductList"
type props ={
    products:Product[]
}
export default function Catalog({products}:props) {
  return (
    <>
      <ProductList products={products}/>
    </>
  )
}
