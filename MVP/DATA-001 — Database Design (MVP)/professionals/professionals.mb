# Table: professionals

| Property | Value |
|----------|-------|
| Table Name | professionals |
| Status | Approved |
| Database | PostgreSQL |
| Module | Professional Management |
| Related Tables | users, programs |

---

# Purpose

Represents professionals who create and manage health programs.

A Professional may be a fitness coach, nutritionist, physiotherapist, psychologist, or other health expert.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Professional identifier |
| user_id | UUID | FK → users.id, UNIQUE | Linked user account |
| profession | ENUM | NOT NULL | Professional role |
| bio | TEXT | Nullable | Short biography |
| is_verified | BOOLEAN | DEFAULT FALSE | Verification status |
| created_at | TIMESTAMP | NOT NULL | Record creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |
| deleted_at | TIMESTAMP | Nullable | Soft delete timestamp |

---

# Enums

## Profession

- FitnessCoach
- Nutritionist
- Physiotherapist
- Psychologist
- Doctor

---

# Business Rules

## BR-001

Each Professional must be linked to exactly one User account.

---

## BR-002

A User can have at most one Professional profile.

---

## BR-003

Only verified professionals can publish programs.

---

## BR-004

Soft Delete must be used.

---

# Relationships

professionals

1 → 1 users

1 → N programs

---

# Indexes

- PK(id)
- UNIQUE(user_id)
- INDEX(profession)

---

# Notes

Professional-specific profile information is stored here.

General account information remains in the `users` table.
