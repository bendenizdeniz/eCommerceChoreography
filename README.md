Project Name: E-Commerce Choreography

Overview:
This project lays the groundwork for a robust e-commerce application, employing the **Saga Design Pattern** with eventual consistency via the **Choreography** architecture.
Microservices within this ecosystem communicate seamlessly through an **event bus**, offering a scalable and resilient solution for managing intricate business processes in a distributed environment.

Features:

Saga Design Pattern: Embracing the Saga design pattern facilitates the orchestration of distributed transactions across multiple microservices while maintaining eventual consistency.

Choreography Architecture: Microservices communicate using the Choreography architecture, with each service publishing events to the event bus to trigger actions in others. This approach enhances scalability and reduces coupling between services.

Event Bus: The system relies on an event bus to enable seamless communication between microservices. Asynchronous event publication and subscription efficiently handle complex business processes.

Docker: All dependent technologies are containerized and orchestrated using Docker, ensuring a uniform environment across diverse deployments.

Components:

Order Service: Manages the creation, modification, and fulfillment of customer orders, utilizing MSSQL.

Payment Service: Handles payment processing for completed orders.

Stock Service: Manages the shipping process, updates order status, and provides tracking information using PostgreSQL.

Additional Considerations:

Configuration and Running Instructions: Refer to documentation for guidance on configuring and running the project, including specific configurations for the event bus, microservices, and Docker.

Scalability and Resilience Benefits: The Choreography architecture not only enhances scalability but also reduces inter-service dependencies, ensuring resilience in a distributed environment.

Future Work: Explore potential enhancements or areas for improvement, inviting contributions and collaboration to further refine the project.

This project showcases a sophisticated yet agile infrastructure for handling e-commerce operations, laying the foundation for scalable, resilient, and efficient microservices communication. For detailed instructions and potential contributions, please refer to the project documentation.
