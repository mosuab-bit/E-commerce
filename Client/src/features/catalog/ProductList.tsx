import { Box } from "@mui/material"
import { Product } from "../../app/models/Product"
import ProductCard from "./ProductCard"

type Props = {
    products:Product[]
}

export default function ProductList({products}:Props) {
  return (
    <Box sx={{display:'flex'}}>
        {products.map(product =>(
            <ProductCard key={product.id} product={product}/>
        ))}
    </Box>
  )
}
