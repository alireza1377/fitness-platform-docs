# Project Context

| Item          | Value             |
| ------------- | ----------------- |
| Document ID   | GOV-002           |
| Version       | 1.0               |
| Status        | Draft             |
| Project       | Fitness Platform  |
| Document Type | Governance        |
| Owner         | Architecture Team |
| Language      | English           |
| Last Updated  | 2026-07-16        |

---

# Executive Summary

Fitness Platform is an AI-first, content-driven digital health and fitness ecosystem designed to help users improve their physical well-being through structured educational content, personalized training programs, nutrition guidance, and professional coaching.

The platform is designed around long-term scalability while maintaining a simple architecture suitable for an MVP.

Documentation is considered the primary source of truth for the entire product lifecycle.

Every implementation must originate from approved documentation.

---

# Product Vision

Create a unified fitness ecosystem where users can learn, train, improve, and communicate with professionals through a single platform.

The platform should support beginners, advanced athletes, personal trainers, nutritionists, and future smart fitness devices without requiring major architectural redesign.

---

# Mission

Our mission is to make professional fitness education and coaching accessible through a scalable digital platform powered by structured content, personalized programs, and AI-assisted development.

---

# Project Scope

Version 1 focuses on delivering the essential building blocks of the platform.

Included in scope:

* User authentication
* User profile
* Video streaming
* Courses
* Learning collections
* Trackable content
* Learning progress
* Weight tracking
* BMI tracking
* Goal management
* Subscription management
* Admin Panel
* Professional Portal
* Android TV support

Out of scope for Version 1:

* AI Coach
* Smart Wearables
* Smart Gym Devices
* Social Network
* Marketplace
* Live Streaming
* Community Challenges

These capabilities are intentionally postponed to future releases.

---

# Product Ecosystem

The platform consists of multiple integrated modules.

Core modules include:

* Identity
* User Profile
* Content
* Learning
* Progress
* Nutrition
* Professional Services
* Subscription
* Administration
* Analytics
* Notifications

Each module owns its own business rules while remaining part of a modular monolith architecture.

---

# Target Users

The platform serves several user groups.

### End User

Users consuming educational content, following workout plans, tracking progress, and improving their health.

### Professional

Certified trainers and nutritionists responsible for creating personalized workout or nutrition programs and communicating with assigned users.

Professionals do not freely browse users.
Users submit a request and the platform assigns or connects the request to the appropriate professional according to business rules.

### Administrator

Responsible for platform configuration, user management, content moderation, subscription management, reporting, and operational monitoring.

---

# Core Business Domains

The platform is organized around the following business domains.

| Domain                | Responsibility                                         |
| --------------------- | ------------------------------------------------------ |
| Identity              | Authentication and account management                  |
| User Profile          | Personal information and goals                         |
| Content               | Videos, courses, playlists, collections                |
| Learning              | Enrollment and content consumption                     |
| Progress              | Learning progress and fitness tracking                 |
| Nutrition             | Food database, calorie tracking and nutrition programs |
| Professional Services | Trainers, nutritionists and communication              |
| Subscription          | Access control and premium plans                       |
| Administration        | Platform management                                    |
| Analytics             | Reporting and insights                                 |
| Notification          | User communication                                     |

---

# High-Level Features

The platform supports:

* Single educational videos
* Multi-video courses
* Structured learning paths
* Content collections
* Premium subscriptions
* Free content
* Personalized workout programs
* Personalized nutrition programs
* Video progress tracking
* Course progress tracking
* Collection progress tracking
* Completion history
* Weight history
* BMI history
* Goal tracking
* Water tracking
* Calorie tracking
* Food database
* Professional chat
* Android TV playback
* Future smart device integration

---

# Core Business Rules

The following business rules apply across the entire platform.

1. Documentation is always the source of truth.
2. Every content item has a lifecycle.
3. Every trackable content stores user progress.
4. Course progress is calculated from completed lessons.
5. Collection progress is calculated from its child contents.
6. Users may enroll in multiple courses simultaneously.
7. Progress is isolated per enrollment.
8. Workout programs are assigned by professionals.
9. Nutrition programs are assigned by professionals.
10. Subscription determines accessible content.
11. The first lesson of eligible courses may be free.
12. Free users have limited platform capabilities.
13. Premium users receive access according to subscription plans.
14. Administrative users can audit every critical operation.
15. Every important business event should be traceable.

---

# Architecture Overview

The system follows an AI-first software engineering approach.

Architecture principles include:

* Domain-Driven Design (DDD)
* Modular Monolith
* Clean Architecture
* API-First
* Mobile-First
* Content-First
* Documentation-Driven Development

Business domains are isolated while remaining deployable as a single application during the MVP stage.

This minimizes operational complexity while preserving future scalability.

---

# Technology Direction

The current technology direction is:

| Layer           | Direction                           |
| --------------- | ----------------------------------- |
| Mobile          | Flutter                             |
| Backend         | To Be Decided (Architecture Driven) |
| Database        | PostgreSQL                          |
| Object Storage  | S3-Compatible Storage               |
| Admin Panel     | Web Application                     |
| Architecture    | Modular Monolith                    |
| Documentation   | Markdown + Mermaid                  |
| Version Control | Git + GitHub                        |

---

# Development Principles

The project follows these principles:

* Simplicity before complexity.
* Documentation before implementation.
* Business before technology.
* Architecture before code.
* AI assists development but never replaces architectural decisions.
* Every module must remain independently understandable.
* Every design decision should be documented through ADRs whenever appropriate.

---

# Project Constraints

The project intentionally prioritizes maintainability over premature optimization.

Constraints include:

* Keep the MVP simple.
* Avoid unnecessary abstractions.
* Avoid microservices unless justified.
* Prefer proven technologies.
* Keep business logic inside the domain.
* Preserve backward compatibility whenever possible.
* Optimize for long-term maintainability instead of short-term speed.
