# Travel-and-Accommodation-Booking-Platform

This API facilitates the management of hotel-related operations, including booking management, hotel and city data handling, and guest services.
Key Features
User Authentication & Registration
    • User Sign-Up: Enables new users to register by providing necessary details.
    • User Login: Allows registered users to securely access booking features.
Hotel Search & Filtering
    • Search by Various Criteria: Users can find hotels based on hotel name, room type, capacity, price range, and more.
    • Detailed Search Results: Displays comprehensive information about hotels matching the search query.
Image Management
    • Image Handling: Supports adding, updating, and deleting images for cities, hotels, and rooms.
Popular Cities Display
    • Trending Cities: Highlights the most visited cities based on user traffic.
Email Notifications
    • Booking Confirmation Emails: Sends emails upon successful bookings, including total price, hotel location, and other relevant details.
    • Improved User Communication: Ensures users stay informed about their reservations.
Admin Management
    • Full Control Over System Entities: Admins can search, add, update, and delete cities, hotels, rooms, and more.
    • Streamlined Operations: A user-friendly interface simplifies administrative tasks.

Endpoints
Amenities
HTTP Method
Endpoint
Description
GET
/api/amenities
Retrieve a list of amenities
POST
/api/amenities
Create a new amenity
GET
/api/amenities/{id}
Get details of a specific amenity
PUT
/api/amenities/{id}
Update an existing amenity
Auth
HTTP Method
Endpoint
Description
POST
/api/auth/login
Processes user login
POST
/api/auth/register-guest
Registers a new guest user
Bookings
HTTP Method
Endpoint
Description
POST
/api/user/bookings
Create a new booking for the current user
GET
/api/user/bookings
Retrieve bookings for the current user
DELETE
/api/user/bookings/{id}
Remove a specific booking
GET
/api/user/bookings/{id}
Get booking details
GET
/api/user/bookings/{id}/invoice
Generate a booking invoice as a PDF
Cities
HTTP Method
Endpoint
Description
GET
/api/cities
Retrieve a list of cities
POST
/api/cities
Create a new city
GET
/api/cities/trending
Fetch the most visited cities
PUT
/api/cities/{id}
Update an existing city
DELETE
/api/cities/{id}
Delete a specific city
PUT
/api/cities/{id}/thumbnail
Set a thumbnail for a city
Discounts
HTTP Method
Endpoint
Description
GET
/api/room-classes/{roomClassId}/discounts
Retrieve available discounts
POST
/api/room-classes/{roomClassId}/discounts
Create a discount for a room class
GET
/api/room-classes/{roomClassId}/discounts/{id}
Retrieve a specific discount
PUT
/api/room-classes/{roomClassId}/discounts/{id}
Delete an existing discount
Guests
HTTP Method
Endpoint
Description
GET
/api/user/recently-visited-hotels
Retrieve the user's recently visited hotels
Hotels
HTTP Method
Endpoint
Description
GET
/api/hotels
Retrieve a list of hotels
POST
/api/hotels
Add a new hotel
GET
/api/hotels/search
Search and filter hotels
GET
/api/hotels/featured-deals
Retrieve special hotel deals
GET
/api/hotels/{id}
Get details of a hotel
PUT
/api/hotels/{id}
Update an existing hotel
DELETE
/api/hotels/{id}
Remove a specific hotel
GET
/api/hotels/{id}/room-classes
Retrieve room classes for a hotel
PUT
/api/hotels/{id}/thumbnail
Set a hotel thumbnail
POST
/api/hotels/{id}/gallery
Add an image to the hotel's gallery
Owners
HTTP Method
Endpoint
Description
GET
/api/owners
Retrieve a list of owners
POST
/api/owners
Add a new owner
GET
/api/owners/{id}
Get details of an owner
PUT
/api/owners/{id}
Update an existing owner
Reviews
HTTP Method
Endpoint
Description
GET
/api/hotels/{hotelId}/reviews
Retrieve hotel reviews
POST
/api/hotels/{hotelId}/reviews
Add a new review
GET
/api/hotels/{hotelId}/reviews/{id}
Retrieve a specific review
PUT
/api/hotels/{hotelId}/reviews/{id}
Update an existing review
DELETE
/api/hotels/{hotelId}/reviews/{id}
Remove a review

Architecture Overview
Clean Architecture
    • Web Layer: Handles HTTP requests through controllers.
    • Infrastructure Layer: Manages databases, authentication, email, and image storage.
    • Application Layer: Contains business logic and service orchestration.
    • Domain Layer: Core business rules and domain entities.
Technology Stack
    • Backend: C#, ASP.NET Core
    • Database: SQL Server with Entity Framework Core
    • Storage: Firebase Storage for scalable image hosting
API Documentation & Security
    • Swagger/OpenAPI: Provides interactive API documentation.
    • JWT Authentication: Secure access with JSON Web Tokens.
Design Patterns Used
    • RESTful API: Ensures a structured and scalable API design.
    • Repository Pattern: Separates data access from business logic.
    • Unit of Work: Manages transactions across multiple data operations.
Logging & Monitoring
    • Serilog: Captures logs for debugging and tracking application events.
Security Measures
    • Data Encryption: Uses Microsoft.AspNet.Identity.IPasswordHasher for password security.
To access admin's functionalities, authenticate with these credentials:
    • Email:admin@travelbooking.com
    • Password: 11aaAA@@

