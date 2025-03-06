import { Container, Typography, Grid, Card, CardContent } from "@mui/material";
import PhoneIcon from "@mui/icons-material/Phone";
import EmailIcon from "@mui/icons-material/Email";
import LocationOnIcon from "@mui/icons-material/LocationOn";

export default function ContactPage() {
  return (
    <Container maxWidth="md" sx={{ mt: 5, textAlign: "center" }}>
      <Typography variant="h3" gutterBottom color="primary" fontWeight="bold">
        Contact Us
      </Typography>
      <Typography variant="h6" color="textSecondary" paragraph>
        Feel free to reach out to us through any of the following channels.
      </Typography>
      
      <Grid container spacing={4} justifyContent="center" sx={{ mt: 4 }}>
        <Grid item xs={12} md={4}>
          <Card sx={{ p: 3, boxShadow: 3, borderRadius: 2, textAlign: "center" }}>
            <CardContent>
              <PhoneIcon color="primary" sx={{ fontSize: 40 }} />
              <Typography variant="h6" gutterBottom>
                Phone
              </Typography>
              <Typography variant="body1" color="textSecondary">
                +123 456 7890
              </Typography>
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={4}>
          <Card sx={{ p: 3, boxShadow: 3, borderRadius: 2, textAlign: "center" }}>
            <CardContent>
              <EmailIcon color="primary" sx={{ fontSize: 40 }} />
              <Typography variant="h6" gutterBottom>
                Email
              </Typography>
              <Typography variant="body1" color="textSecondary">
                support@ecommerce.com
              </Typography>
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={4}>
          <Card sx={{ p: 3, boxShadow: 3, borderRadius: 2, textAlign: "center" }}>
            <CardContent>
              <LocationOnIcon color="primary" sx={{ fontSize: 40 }} />
              <Typography variant="h6" gutterBottom>
                Address
              </Typography>
              <Typography variant="body1" color="textSecondary">
                123 E-Commerce St, Business City
              </Typography>
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </Container>
  );
}
