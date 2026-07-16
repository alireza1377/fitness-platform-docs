# Database Design (MVP)

| Property | Value |
|----------|-------|
| Document ID | DATA-001 |
| Title | Database Design (MVP) |
| Version | 1.0 |
| Status | Draft |
| Database | PostgreSQL |

---

# 1. Overview

This document defines the initial database model for the MVP.

The schema focuses only on entities required to deliver the MVP.

---

# 2. Tables

| Table | Purpose |
|---------|-------------|
| users | User accounts |
| profiles | Personal information |
| goals | User goals |
| measurements | Body measurements |
| subscriptions | User subscription plans |
| professionals | Trainers and nutritionists |
| programs | Fitness and nutrition programs |
| program_items | Activities inside a program |
| contents | Educational content |
| progresses | User progress |

---

# 3. Relationships

users
├── 1 profile
├── N goals
├── N measurements
├── N progresses
├── 1 subscription

programs
├── N program_items
├── N contents

professionals
├── N users

progresses
├── 1 user
├── 1 program
├── 0..1 content

# Table: users

## Purpose

Stores authentication and account information.

---

| Column | Type | Constraints |
|---------|------|-------------|
| id | UUID | PK |
| email | VARCHAR(255) | UNIQUE, Nullable |
| phone | VARCHAR(20) | UNIQUE, Nullable |
| password_hash | TEXT | NOT NULL |
| status | ENUM | Active, Inactive, Suspended |
| email_verified | BOOLEAN | Default false |
| phone_verified | BOOLEAN | Default false |
| last_login_at | TIMESTAMP | Nullable |
| created_at | TIMESTAMP | NOT NULL |
| updated_at | TIMESTAMP | NOT NULL |
| deleted_at | TIMESTAMP | Nullable |

---

## Rules

- At least one of email or phone must exist.
- Passwords are never stored in plain text.
- Soft delete is used.
- Authentication data is separated from profile data.

---

## Relationships

User

1 → 1 Profile

1 → N Goals

1 → N Measurements

1 → N Progress Records

1 → 1 Subscription
