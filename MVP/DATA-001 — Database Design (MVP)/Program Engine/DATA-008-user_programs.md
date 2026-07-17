# Table: user_programs

| Property | Value |
|----------|-------|
| Table Name | user_programs |
| Status | Approved |
| Database | PostgreSQL |
| Module | Program Management |
| Related Tables | users, programs, program_modules, program_items |

---

# Purpose

Represents a user's enrollment in a program.

This table stores each user's individual journey, progress, and execution state for a program.

Each enrollment is independent and preserves historical data.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Enrollment identifier |
| user_id | UUID | FK → users.id | Assigned user |
| program_id | UUID | FK → programs.id | Assigned program |
| status | ENUM | NOT NULL | Current enrollment status |
| progress_percentage | DECIMAL(5,2) | DEFAULT 0 | Overall completion percentage |
| current_module_id | UUID | FK → program_modules.id, Nullable | Current module |
| current_program_item_id | UUID | FK → program_items.id, Nullable | Current item |
| assigned_at | TIMESTAMP | NOT NULL | Assignment timestamp |
| started_at | TIMESTAMP | Nullable | First activity timestamp |
| completed_at | TIMESTAMP | Nullable | Completion timestamp |
| paused_at | TIMESTAMP | Nullable | Pause timestamp |
| last_activity_at | TIMESTAMP | Nullable | Last activity timestamp |
| created_at | TIMESTAMP | NOT NULL | Record creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |

---

# Enums

## UserProgramStatus

- Assigned
- InProgress
- Paused
- Completed
- Cancelled

---

# Business Rules

## BR-001

A user may have multiple active programs.

---

## BR-002

A user cannot have more than one active enrollment for the same program.

---

## BR-003

Progress is calculated automatically based on completed Program Items.

---

## BR-004

Completed enrollments become read-only.

---

## BR-005

Cancelled enrollments preserve historical data.

---

# Relationships

user_programs

N → 1 users

N → 1 programs

N → 1 program_modules

N → 1 program_items

1 → N progresses

---

# Indexes

- PK(id)
- INDEX(user_id)
- INDEX(program_id)
- INDEX(status)
- INDEX(last_activity_at)
- UNIQUE(user_id, program_id)

---

# Notes

This table stores only the user's relationship with a program.

Programs remain reusable templates.

The user's current module, current item, progress, and timestamps are stored here.

Detailed activity completion is stored in the `progresses` table.
