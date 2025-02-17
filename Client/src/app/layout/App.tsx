import { useEffect,useState } from "react"
import { Product } from "../models/Product"
import { Container, createTheme, ThemeProvider } from "@mui/material";
import Catalog from "../../features/catalog/Catalog";
import NavBar from "./NavBar";
function App() {
const[products,setProducts] =useState<Product[]>([]);
useEffect(()=>{
  fetch('http://localhost:5201/api/products')
  .then(response =>response.json())
  .then(data=>setProducts(data))
  .catch(error => console.error('Error fetching products:', error));
},[])

const theme = createTheme({
  palette: {
    mode: 'dark',
   
    },
  
});
  return (
    <ThemeProvider theme={theme}>
    <NavBar/>
    <Container maxWidth='xl' sx={{ mt: 8 }}>
      <Catalog products={products}/>
    </Container>
    </ThemeProvider>
  )
}

export default App
