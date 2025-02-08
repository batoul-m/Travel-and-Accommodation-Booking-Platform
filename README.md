# Travel-and-Accommodation-Booking-Platform

This API facilitates the management of hotel-related operations, including booking management, hotel and city data handling, and guest services.

## Key Features:

### **User Authentication & Registration**

- User Sign-Up: Enables new users to register by providing necessary details.
    
- User Login: Allows registered users to securely access booking features.
    
### **Hotel Search & Filtering**                                                                                              

- Search by Various Criteria: Users can find hotels based on hotel name, room type, capacity, price range, and more.
    
- Detailed Search Results: Displays comprehensive information about hotels matching the search query.
    
### **Image Management**

- Image Handling: Supports adding, updating, and deleting images for cities, hotels, and rooms.

### **Popular Cities Display**

- Trending Cities: Highlights the most visited cities based on user traffic.
   
### **Email Notifications**

- Booking Confirmation Emails: Sends emails upon successful bookings, including total price, hotel location, and other
   
### **relevant details.**

- Improved User Communication: Ensures users stay informed about their reservations.
    
### **Admin Management**

- Full Control Over System Entities: Admins can search, add, update, and delete cities, hotels, rooms, and more.
- Streamlined Operations: A user-friendly interface simplifies administrative tasks.

## **Endpoints**
### ****Amenities****

| HTTP Method | Endpoint              | Description                          |
|-------------|-----------------------|--------------------------------------|
| GET         | /api/amenities         | Retrieve a page of amenities        |
| POST        | /api/amenities         | Create a new amenity                |
| GET         | /api/amenities/{id}    | Get an amenity specified by ID      |
| PUT         | /api/amenities/{id}    | Update an existing amenity          |

### **Auth**
| HTTP Method | Endpoint                    | Description                          |
|-------------|-----------------------------|--------------------------------------|
| POST        | /api/auth/login              | Processes a login request            |
| POST        | /api/auth/register-guest     | Processes registering a guest request|

### **Bookings**
| HTTP Method | Endpoint                  | Description                                              |
|-------------|---------------------------|----------------------------------------------------------|
| POST        | /api/user/bookings         | Create a new Booking for the current user                |
| GET         | /api/user/bookings         | Get a page of bookings for the current user              |
| DELETE      | /api/user/bookings/{id}    | Delete an existing booking specified by ID                |
| GET         | /api/user/bookings/{id}    | Get a booking specified by ID for the current user        |
| GET         | /api/user/bookings/{id}/invoice | Get the invoice of a booking specified by ID as PDF for the current user |

### **Cities**
| HTTP Method | Endpoint              | Description                                           |
|-------------|-----------------------|-------------------------------------------------------|
| GET         | /api/cities           | Retrieve a page of cities                             |
| POST        | /api/cities           | Create a new city                                     |
| GET         | /api/cities/trending  | Returns TOP N most visited cities (trending cities)    |
| PUT         | /api/cities/{id}      | Update an existing city specified by ID               |
| DELETE      | /api/cities/{id}      | Delete an existing city specified by ID               |
| PUT         | /api/cities/{id}/thumbnail | Set the thumbnail of a city specified by ID         |

### **Discounts**
| HTTP Method | Endpoint                             | Description                                |
|-------------|--------------------------------------|--------------------------------------------|
| GET         | /api/room-classes/{roomClassId}/discounts | Retrieve a page of discounts for a room class |
| POST        | /api/room-classes/{roomClassId}/discounts | Create a discount for a room class specified by ID |
| GET         | /api/room-classes/{roomClassId}/discounts/{id} | Get an existing discount by ID         |
| PUT         | /api/room-classes/{roomClassId}/discounts/{id} | Delete an existing discount specified by ID |

### **Guests**
| HTTP Method | Endpoint                        | Description                                          |
|-------------|---------------------------------|------------------------------------------------------|
| GET         | /api/user/recently-visited-hotels | Retrieve the recently N visited hotels by the current user |

### **Hotels**
| HTTP Method | Endpoint                  | Description                                              |
|-------------|---------------------------|----------------------------------------------------------|
| GET         | /api/hotels               | Retrieve a page of hotels                                |
| POST        | /api/hotels               | Create a new hotel                                       |
| GET         | /api/hotels/search        | Search and filter hotels based on specific criteria     |
| GET         | /api/hotels/featured-deals| Retrieve N hotel featured deals                         |
| GET         | /api/hotels/{id}          | Get hotel by ID for guest                               |
| PUT         | /api/hotels/{id}          | Update an existing hotel specified by ID                |
| DELETE      | /api/hotels/{id}          | Delete an existing hotel specified by ID                |
| GET         | /api/hotels/{id}/room-classes | Get room classes for a hotel specified by ID for Guests |
| PUT         | /api/hotels/{id}/thumbnail | Set the thumbnail of a hotel specified by ID           |
| POST        | /api/hotels/{id}/gallery  | Add a new image to a hotel's gallery specified by ID    |

### **Owners**
| HTTP Method | Endpoint                | Description                                 |
|-------------|-------------------------|---------------------------------------------|
| GET         | /api/owners             | Retrieve a page of owners                  |
| POST        | /api/owners             | Create a new owner                         |
| GET         | /api/owners/{id}        | Get an existing owner by ID                |
| PUT         | /api/owners/{id}        | Update an existing owner                   |

### **Reviews**
| HTTP Method | Endpoint                                | Description                                          |
|-------------|-----------------------------------------|------------------------------------------------------|
| GET         | /api/hotels/{hotelId}/reviews           | Retrieve a page of reviews for a hotel specified by ID |
| POST        | /api/hotels/{hotelId}/reviews           | Create a new review for a hotel specified by ID       |
| GET         | /api/hotels/{hotelId}/reviews/{id}      | Get a review specified by ID for a hotel specified by ID |
| PUT         | /api/hotels/{hotelId}/reviews/{id}      | Update an existing review specified by ID for a hotel specified by ID |
| DELETE      | /api/hotels/{hotelId}/reviews/{id}      | Delete an existing review specified by ID for a hotel specified by ID |

### **RoomClasses**
| HTTP Method | Endpoint                        | Description                                            |
|-------------|---------------------------------|--------------------------------------------------------|
| GET         | /api/room-classes               | Retrieve a page of room classes                        |
| POST        | /api/room-classes               | Create a new room class                                |
| PUT         | /api/room-classes/{id}          | Update an existing room class specified by ID          |
| DELETE      | /api/room-classes/{id}          | Delete an existing room class specified by ID          |
| POST        | /api/room-classes/{id}/gallery  | Add a new image to a room class's gallery specified by ID |

### **Room **
| HTTP Method | Endpoint                                  | Description                                                  |
|-------------|-------------------------------------------|--------------------------------------------------------------|
| GET         | /api/room-classes/{roomClassId}/rooms     | Retrieve a page of rooms for a room class                     |
| POST        | /api/room-classes/{roomClassId}/rooms     | Create a new room in a room class specified by ID             |
| GET         | /api/room-classes/{roomClassId}/rooms/available | Retrieve a page of available rooms for a room class           |
| PUT         | /api/room-classes/{roomClassId}/rooms/{id} | Update an existing room with ID in a room class specified by ID |
| DELETE      | /api/room-classes/{roomClassId}/rooms/{id} | Delete a room by ID in a room class specified by ID           |

## **Architecture Overview**
### **Clean Architecture**

- Web Layer: Handles HTTP requests through controllers.
    
- Infrastructure Layer: Manages databases, authentication, email, and image storage.
    
- Application Layer: Contains business logic and service orchestration.
    
- Domain Layer: Core business rules and domain entities.
    
## **Technology Stack**

- Backend: C#, ASP.NET Core
    
- Database: SQL Server with Entity Framework Core

- Storage: Firebase Storage for scalable image hosting
    
## **API Documentation & Security**

- Swagger/OpenAPI: Provides interactive API documentation.
    
- JWT Authentication: Secure access with JSON Web Tokens.
    
## **Design Patterns Used**

- RESTful API: Ensures a structured and scalable API design.
    
- Repository Pattern: Separates data access from business logic.
    
- Unit of Work: Manages transactions across multiple data operations.
    
## **Logging & Monitoring**

- Serilog: Captures logs for debugging and tracking application events.
    
## **Security Measures**

- Data Encryption: Uses Microsoft.AspNet.Identity.IPasswordHasher for password security.

    
### Note : 

To access admin's functionalities, authenticate with these credentials:

 - Email:admin@travelbooking.com
 - 
- Password: 11aaAA@@

