// import { Alert, AlertTitle, Button, ButtonGroup, Container, List, ListItem, Typography } from "@mui/material";
// import { useLazyGet400ErrorQuery, useLazyGet401ErrorQuery, useLazyGet404ErrorQuery, useLazyGet500ErrorQuery, useLazyGetValidationErrorQuery } from "./errorApi";
// import { useState } from "react";

// export default function AboutPage() {
//   const [validationErrors, setValidationErrors] = useState<string[]>([]);

//   const [trigger400Error] = useLazyGet400ErrorQuery();
//   const [trigger401Error] = useLazyGet401ErrorQuery();
//   const [trigger404Error] = useLazyGet404ErrorQuery();
//   const [trigger500Error] = useLazyGet500ErrorQuery();
//   const [triggerValidationError] = useLazyGetValidationErrorQuery();

//   const getValidatonError = async () => {
//     try {
//       await triggerValidationError().unwrap();
//     } catch (error: unknown) {
//       if (error && typeof error === 'object' && 'message' in error 
//           && typeof (error as {message: unknown}).message === 'string') {
//         const errorArray = (error as {message: string}).message.split(', ');
//         setValidationErrors(errorArray);
//       }

//     }
//   }

//   return (
//     <Container maxWidth='lg'>
//       <Typography gutterBottom variant="h3">Errors for testing</Typography>
//       <ButtonGroup fullWidth>
//         <Button variant="contained" onClick={() => trigger400Error()
//           .catch(err => console.log(err))}>
//           Test 400 Error
//         </Button>
//         <Button variant="contained" onClick={() => trigger401Error()
//           .catch(err => console.log(err))}>
//           Test 401 Error
//         </Button>
//         <Button variant="contained" onClick={() => trigger404Error()
//           .catch(err => console.log(err))}>
//           Test 404 Error
//         </Button>
//         <Button variant="contained" onClick={() => trigger500Error()
//           .catch(err => console.log(err))}>
//           Test 500 Error
//         </Button>
//         <Button variant="contained" onClick={getValidatonError}>
//           Test Validation Error
//         </Button>
//       </ButtonGroup>
//       {validationErrors.length > 0 && (
//         <Alert severity="error">
//           <AlertTitle>Validation errors</AlertTitle>
//           <List>
//             {validationErrors.map(err => (
//               <ListItem key={err}>{err}</ListItem>
//             ))}
//           </List>
//         </Alert>
//       )}
//     </Container>
//   )
// }
import { Container, Typography, Box, Card, CardContent } from "@mui/material";

export default function AboutPage() {
  return (
    <Container maxWidth="md" sx={{ mt: 5, textAlign: "center" }}>
      <Typography variant="h3" gutterBottom color="primary">
        About Us
      </Typography>
      <Typography variant="h6" color="textSecondary" paragraph>
        Welcome to our E-Commerce platform! We are dedicated to providing the best shopping experience with high-quality products and excellent customer service.
      </Typography>
      <Box display="flex" justifyContent="center" mt={4}>
        <Card sx={{ maxWidth: 600, p: 2 }}>
          <CardContent>
            <Typography variant="h5" color="secondary" gutterBottom>
              Our Mission
            </Typography>
            <Typography variant="body1" color="textPrimary">
              Our goal is to make online shopping easy, secure, and enjoyable for everyone. We focus on delivering top-notch products and seamless transactions.
            </Typography>
          </CardContent>
        </Card>
      </Box>
    </Container>
  );
}
