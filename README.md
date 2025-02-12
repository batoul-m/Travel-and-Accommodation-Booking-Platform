# Travel and Accommodation Booking Platform API

This API offers powerful functionalities for managing hotels, bookings, cities, and guest interactions. From searching for hotels based on various criteria to managing bookings and sending email notifications, this API has it all.

## Core Functionalities

### User Authentication
- **Sign Up for New Users:** Users can easily create new accounts by providing essential information.
- **Log In to Your Account:** Existing users can authenticate securely to access the platform's features.

### Global Hotel Search
- **Search Based on Multiple Filters:** Find hotels by criteria like hotel name, room type, capacity, price range, and more.
- **Detailed Hotel Information:** Receive in-depth information on hotels that meet the user's search criteria.

### Image Handling
- **Image Upload and Management:** Add, delete, or update images and thumbnails for cities, hotels, and rooms.
  
### Popular Cities Feature
- **Trending Cities:** Display the most visited cities based on user activity and traffic.

### Email Notifications
- **Booking Confirmation:** Send confirmation emails after a booking, detailing the hotel location, pricing, and more.
- **Stay Connected:** Ensure seamless communication with users regarding their bookings.

### Administrative Dashboard
- **Manage Hotels, Cities, and Rooms:** Admins have full control to search, update, add, and delete cities, hotels, and rooms.
- **Efficient Operations:** Manage system entities with ease, keeping everything running smoothly.



## API Endpoints

### Amenities

| HTTP Method | Endpoint               | Description                             |
|-------------|------------------------|-----------------------------------------|
| GET         | /api/amenities          | Retrieve a page of amenities            |
| POST        | /api/amenities          | Create a new amenity                    |
| GET         | /api/amenities/{id}     | Get an amenity specified by ID          |
| PUT         | /api/amenities/{id}     | Update an existing amenity              |

---

### Auth

| HTTP Method | Endpoint                        | Description                           |
|-------------|---------------------------------|---------------------------------------|
| POST        | /api/auth/login                 | Processes a login request             |
| POST        | /api/auth/register-guest        | Processes registering a guest request|

---

### Bookings

| HTTP Method | Endpoint                        | Description                                         |
|-------------|---------------------------------|-----------------------------------------------------|
| POST        | /api/user/bookings              | Create a new booking for the current user           |
| GET         | /api/user/bookings              | Get a page of bookings for the current user         |
| DELETE      | /api/user/bookings/{id}         | Delete an existing booking specified by ID          |
| GET         | /api/user/bookings/{id}         | Get a booking specified by ID for the current user  |
| GET         | /api/user/bookings/{id}/invoice | Get the invoice of a booking specified by ID as PDF |

---

### Cities

| HTTP Method | Endpoint                     | Description                                        |
|-------------|------------------------------|----------------------------------------------------|
| GET         | /api/cities                  | Retrieve a page of cities                          |
| POST        | /api/cities                  | Create a new city                                  |
| GET         | /api/cities/trending         | Returns TOP N most visited cities (trending cities) |
| PUT         | /api/cities/{id}             | Update an existing city specified by ID            |
| DELETE      | /api/cities/{id}             | Delete an existing city specified by ID            |
| PUT         | /api/cities/{id}/thumbnail   | Set the thumbnail of a city specified by ID        |

---

### Discounts

| HTTP Method | Endpoint                                              | Description                            |
|-------------|-------------------------------------------------------|----------------------------------------|
| GET         | /api/room-classes/{roomClassId}/discounts             | Retrieve a page of discounts for a room class |
| POST        | /api/room-classes/{roomClassId}/discounts             | Create a discount for a room class specified by ID |
| GET         | /api/room-classes/{roomClassId}/discounts/{id}        | Get an existing discount by ID         |
| PUT         | /api/room-classes/{roomClassId}/discounts/{id}        | Update an existing discount specified by ID |
| DELETE      | /api/room-classes/{roomClassId}/discounts/{id}        | Delete an existing discount specified by ID |

---

### Guests

| HTTP Method | Endpoint                           | Description                                         |
|-------------|------------------------------------|-----------------------------------------------------|
| GET         | /api/user/recently-visited-hotels  | Retrieve the recently N visited hotels by the current user |

---

### Hotels

| HTTP Method | Endpoint                           | Description                                         |
|-------------|------------------------------------|-----------------------------------------------------|
| GET         | /api/hotels                        | Retrieve a page of hotels                           |
| POST        | /api/hotels                        | Create a new hotel                                  |
| GET         | /api/hotels/search                 | Search and filter hotels based on specific criteria |
| GET         | /api/hotels/featured-deals         | Retrieve N hotel featured deals                    |
| GET         | /api/hotels/{id}                   | Get hotel by ID for guest                           |
| PUT         | /api/hotels/{id}                   | Update an existing hotel specified by ID            |
| DELETE      | /api/hotels/{id}                   | Delete an existing hotel specified by ID            |
| GET         | /api/hotels/{id}/room-classes      | Get room classes for a hotel specified by ID for Guests |
| PUT         | /api/hotels/{id}/thumbnail         | Set the thumbnail of a hotel specified by ID        |
| POST        | /api/hotels/{id}/gallery           | Add a new image to a hotel's gallery specified by ID |

---

### Owners

| HTTP Method | Endpoint                        | Description                          |
|-------------|---------------------------------|--------------------------------------|
| GET         | /api/owners                     | Retrieve a page of owners            |
| POST        | /api/owners                     | Create a new owner                   |
| GET         | /api/owners/{id}                | Get an existing owner by ID          |
| PUT         | /api/owners/{id}                | Update an existing owner             |

---

### Reviews

| HTTP Method | Endpoint                                 | Description                                                    |
|-------------|------------------------------------------|----------------------------------------------------------------|
| GET         | /api/hotels/{hotelId}/reviews            | Retrieve a page of reviews for a hotel specified by ID         |
| POST        | /api/hotels/{hotelId}/reviews            | Create a new review for a hotel specified by ID                |
| GET         | /api/hotels/{hotelId}/reviews/{id}       | Get a review specified by ID for a hotel specified by ID        |
| PUT         | /api/hotels/{hotelId}/reviews/{id}       | Update an existing review specified by ID for a hotel specified by ID |
| DELETE      | /api/hotels/{hotelId}/reviews/{id}       | Delete an existing review specified by ID for a hotel specified by ID |

---

### RoomClasses

| HTTP Method | Endpoint                        | Description                            |
|-------------|---------------------------------|----------------------------------------|
| GET         | /api/room-classes               | Retrieve a page of room classes        |
| POST        | /api/room-classes               | Create a new room class                |
| PUT         | /api/room-classes/{id}          | Update an existing room class specified by ID |
| DELETE      | /api/room-classes/{id}          | Delete an existing room class specified by ID |
| POST        | /api/room-classes/{id}/gallery  | Add a new image to a room class's gallery specified by ID |

---

### Rooms

| HTTP Method | Endpoint                                    | Description                                |
|-------------|---------------------------------------------|--------------------------------------------|
| GET         | /api/room-classes/{roomClassId}/rooms       | Retrieve a page of rooms for a room class  |
| POST        | /api/room-classes/{roomClassId}/rooms       | Create a new room in a room class specified by ID |
| GET         | /api/room-classes/{roomClassId}/rooms/available | Retrieve a page of available rooms for a room class |
| PUT         | /api/room-classes/{roomClassId}/rooms/{id}  | Update an existing room with ID in a room class specified by ID |
| DELETE      | /api/room-classes/{roomClassId}/rooms/{id}  | Delete a room by ID in a room class specified by ID |



## System Architecture

### Overview: Clean Architecture

The system leverages **Clean Architecture**, a layered approach that separates concerns for better scalability, maintainability, and testability. It ensures that the core business logic remains decoupled from external systems, such as databases and UI components.



### External Layers

- **Web Layer**:  
  This layer focuses on **handling HTTP requests** and orchestrating the communication between the client and server. It contains **Controllers** that are responsible for processing the incoming requests, validating data, and formatting the responses.

- **Infrastructure Layer**:  
  The infrastructure layer deals with **external systems and resources**. This includes managing access to the database, interacting with external services like email, handling image storage (e.g., Firebase), generating PDFs, and managing authentication. The infrastructure layer ensures smooth interaction with these external systems and provides necessary data to the application.


### Core Layers

- **Application Layer**:  
  This is where the core **business logic** resides. It coordinates operations between various components and ensures that business rules are executed properly. The application layer acts as an intermediary, receiving data from the infrastructure layer, applying business logic, and passing it to the domain layer when necessary.

- **Domain Layer**:  
  The domain layer holds the **core business rules** and entities of the application. This layer is isolated from external dependencies like databases or web frameworks. Its primary role is to represent real-world concepts and ensure the system behaves as expected based on the business model.

---

## Technology Stack Overview

### Programming and Frameworks

- **C#**:  
  The core language of choice for developing the application. C# provides a rich set of features that support object-oriented programming, making it a suitable option for building scalable enterprise applications.

- **ASP.NET Core**:  
  A lightweight, high-performance framework for building cross-platform web APIs. ASP.NET Core powers the API endpoints, ensuring optimal performance while maintaining flexibility and scalability.

---

### Database Layer

- **Entity Framework Core (EF Core)**:  
  EF Core simplifies the interaction between the application and the database. It provides an **ORM (Object-Relational Mapping)** framework, which makes database operations more intuitive by allowing developers to work with database entities as objects.

- **SQL Server**:  
  SQL Server is the chosen relational database for storing and retrieving application data. Known for its **reliability** and **scalability**, SQL Server ensures efficient data management and access, critical for handling large datasets in the system.

---

### File Storage and Image Management

- **Firebase Storage**:  
  Used for storing images (like hotel photos or user profiles), **Firebase Storage** is integrated into the system for **cloud-based storage**. It offers seamless API integration and scalable storage solutions, ensuring smooth and efficient image management.

---

## API Documentation and Interaction

- **Swagger/OpenAPI**:  
  The API is documented using **Swagger/OpenAPI**, which defines the structure and endpoints. Swagger generates interactive documentation, making it easy for both developers and users to understand and test the API.

- **Swagger UI**:  
  **Swagger UI** provides a **web interface** to explore and interact with the API, enabling developers and testers to manually verify endpoints and responses without needing to write separate test code.

---

## Security and Authentication

- **JWT (JSON Web Tokens)**:  
  **JWT** is used to ensure **secure data transmission** between clients and the server. It facilitates **stateless authentication**, allowing users to authenticate once and maintain their session without the need to repeatedly send credentials.

---

## Monitoring and Logging

- **Serilog**:  
  To ensure effective logging of system events, **Serilog** is used. It captures detailed logs that help in monitoring the application, identifying issues, and providing insights into system behavior.

---

## Design Patterns and Principles

The architecture of this system embraces several design patterns to ensure flexibility and maintainability:

- **RESTful Principles**:  
  The system follows **RESTful design** principles, ensuring that APIs are simple, scalable, and stateless. REST promotes the use of standard HTTP methods (GET, POST, PUT, DELETE) and response codes.

- **Repository Pattern**:  
  The **Repository Pattern** abstracts the data access logic from the business logic. This decouples the data layer from the core application, making the system more maintainable and testable.

- **Unit of Work**:  
  The **Unit of Work** pattern ensures that multiple operations across different repositories can be committed as a single transaction. This guarantees **data consistency** and **atomicity**.

- **Options Pattern**:  
  The **Options Pattern** is used for centralized configuration management, enabling the system to easily manage configurations across different environments.

---

## Security Best Practices

- **Password Hashing**:  
  User passwords are securely hashed using **Microsoft.AspNet.Identity.IPasswordHasher**. This ensures that even if the database is compromised, user passwords are not exposed in plain text.

## Note : 

To access admin's functionalities, authenticate with these credentials:

Email: admin@travelbooking.com

Password: 11aaAA@@
