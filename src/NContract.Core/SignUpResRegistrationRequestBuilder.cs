namespace NContract.Core
{
    internal static class SignUpResRegistrationRequestBuilder
    {
        public static dynamic Build(string userEmail,
            string restaurantEmail,
            string restaurantName,
            string userPassword = "12345678",
            string userLanguageCode = "en-GB",
            string userFirstname = "David",
            string userLastname = "Ross",
            string restaurantPostcode = "N1ABC",
            string restaurantCountry = "GBR",
            string restaurantCulture = "en-GB",
            string restaurantCity = "London",
            string restaurantAddress = "123 Oxford Circus",
            string restaurantTimezone = "London/Europe",
            string restaurantPhone = "0765123123",
            string restaurantWebsite = "www.restaurant.com"
            )
        {
            return new
            {
                User = new
                {
                    Email = userEmail,
                    Password = userPassword,
                    LanguageCode = userLanguageCode,
                    FirstName = userFirstname,
                    LastName = userLastname
                },
                Restaurant = new
                {
                    Name = restaurantName,
                    PostCode = restaurantPostcode,
                    Country = restaurantCountry,
                    Culture = restaurantCulture,
                    City = restaurantCity,
                    Email = restaurantEmail,
                    Address = restaurantAddress,
                    TimeZone = restaurantTimezone,
                    PhoneNumber = restaurantPhone,
                    Url = restaurantWebsite,
                }
            };
        }
    }
}