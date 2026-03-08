# lms_b2p
# Domain Validation Walkthrough

All the requested database domain models for the LMS application have been accurately created in the `iucs.lms.domain` project. We have covered the requirements for User Authentication, User Access Roles, Curriculum Structure, Course Management, Batch Management, Digital Library, Exams & Assessment, and Payments. 

## Completed Changes

### Module 1 & 2: Authentication and User Types
-   [User.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/User.cs), [Role.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Role.cs), [UserRole.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/UserRole.cs)
-   [Menu.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Menu.cs), [RoleMenu.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/RoleMenu.cs)
-   [UserSession.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/UserSession.cs), [UserDevice.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/UserDevice.cs)

### Module 3: Curriculum Structure
-   [Board.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Board.cs)
-   [Class.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Class.cs)
-   [Subject.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Subject.cs)
-   [Topic.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Topic.cs)

### Module 4, 5, 6, 7 & 8: Courses, Batch Management, and Dashboards
-   [Course.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Course.cs), [CourseContent.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/CourseContent.cs)
-   [Batch.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Batch.cs), [BatchStudent.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/BatchStudent.cs), [BatchTeacher.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/BatchTeacher.cs)
-   [LiveSession.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/LiveSession.cs)

### Module 9 & 10: Digital Library and Assessments
-   [Book.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Book.cs)
-   [Quiz.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Quiz.cs), [QuizQuestion.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/QuizQuestion.cs), [QuizAttempt.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/QuizAttempt.cs)

### Module 13: Payments and Subscriptions
-   [PaymentTransaction.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/PaymentTransaction.cs)
-   [Subscription.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/Subscription.cs)
-   [RefundRequest.cs](file:///c:/IUCS/Product/GitHub%20Projects/B2P/iucs.lms.api/iucs.lms.domain/Entities/RefundRequest.cs)

## Validation Results

We executed `dotnet build` inside the `iucs.lms.domain` folder to ensure there are no compilation errors during the creation of these entities.
-   **Build Output**: `Build succeeded in 6.2s` `Exit code: 0`

These domain entities provide the foundational structure necessary for EF Core to link with your PostgreSQL/SQL Server database. They accurately cover all of the specified features in your request map including Role-based access control, Curriculum tracking, Live interactions mapping, and Payment Gateways models.
