Project Name: E-Commerce Choreography

Overview
This project serves as the foundational infrastructure for an e-commerce application, leveraging the **Saga Design Pattern** with **eventual consistency** through the **Choreography architecture**.
The microservices within this system communicate through an event bus, providing a scalable and resilient solution for handling complex business processes in a distributed environment.

Features
Saga Design Pattern: The project adopts the Saga design pattern, allowing for the orchestration of distributed transactions across multiple microservices while maintaining eventual consistency.

Choreography Architecture: The microservices communicate using the Choreography architecture, where each service publishes events to the event bus to trigger actions in other services.
This approach enhances scalability and reduces the coupling between services.

Event Bus: The system relies on an event bus to facilitate seamless communication between microservices.
Events are published and subscribed to asynchronously, enabling efficient handling of business processes.

Components

Order Service: Manages the creation, modification, and fulfillment of customer orders.

Payment Service: Handles payment processing for completed orders.

Stock Service: Takes care of the shipping process, updating order status and providing tracking information.
