# Pizzeria Bianco
link: https://www.facebook.com/profile.php?id=61557981250331

Pizzeria Bianco is a web application project originally developed as a university assignment. The project implements a Clean Architecture approach and focuses primarily on building a robust backend and database structure. Due to the limited utility of gRPC in this context, gRPC projects have been omitted from the final application.

The application is currently incomplete and requires a full frontend design to be implemented in Razor Pages. Once the graphical layout is finalized, the frontend can be integrated, and additional tests can be developed.

## Technologies Used
## API
  - **Controllers**: Handle requests related to the website. Each browser request is mapped to a specific controller method, managing the corresponding logic for that endpoint.
  - **API Services**: 
    - **AddressVerificationService**: This service is responsible for verifying the validity of a delivery address in real-time. It checks both the correctness of the address and the distance from the pizzeria to ensure it falls within the delivery range.
   
## Data 
  - **AppDbContext**: Manages access to database models and provides a session with the database for querying and saving data.
  - **AppDbContextFactory**: Generates the database context, allowing for the creation of the database.
  - **SeedData**: Populates the database with initial data, such as menu items and product information.
  - **Models**: Represents the entities used in the pizzeria application, including Orders, Users, Products, and other related entities.

## Tests
  - **API Tests**: Test the API endpoints to ensure they work as expected.
  - **Data Tests**: Validate database functionality, such as CRUD operations and data integrity.

## Project Status
Currently, Pizzeria Bianco is in the backend development stage. The following tasks remain:
1. **Frontend Design**: Complete the graphical design of the website. Once available, it will be integrated with Razor Pages to provide a dynamic and interactive frontend.
2. **Additional Tests**: Add more comprehensive tests for API functionality and database interactions.
3. **Feature Enhancements**:  Implement any new features that may arise based on user feedback or functional requirements.


