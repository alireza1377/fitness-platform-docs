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

Unlike the `programs` table, which stores reusable program templates, this table stores the user's individual journey, progress, and execution state.

Each enrollment is independent and preserves historical data.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Enrollment identifier |
| user_id | UUID | FK → users.id | Assigned user |
| program_id | UUID | FK → programs.id | Assigned program |
| assigned_by | ENUM | NOT NULL | Assignment source |
| program_version | INTEGER | NOT NULL | Assigned program version |
| status | ENUM | NOT NULL | Current enrollment status |
| progress_percentage | DECIMAL(5,2) | DEFAULT 0 | Overall completion percentage |
| current_module_id | UUID | FK → program_modules.id, Nullable | Current module |
| current_program_item_id | UUID | FK → program_items.id, Nullable | Current program item |
| assigned_at | TIMESTAMP | NOT NULL | Assignment timestamp |
| started_at | TIMESTAMP | Nullable | First activity timestamp |
| completed_at | TIMESTAMP | Nullable | Completion timestamp |
| paused_at | TIMESTAMP | Nullable | Pause timestamp |
| archived_at | TIMESTAMP | Nullable | Archive timestamp |
| last_activity_at | TIMESTAMP | Nullable | Last user activity |
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
- Archived

---

## AssignmentSource

- AI
- Professional
- Administrator
- SelfAssigned

---

# Business Rules

## BR-001

A user may have multiple active program enrollments.

---

## BR-002

A user cannot have more than one active enrollment for the same program version.

---

## BR-003

The same program may be completed multiple times.

Each new attempt creates a new enrollment record.

---

## BR-004

Progress is calculated automatically based on completed Program Items.

---

## BR-005

Completed enrollments become read-only.

---

## BR-006

Cancelled and Archived enrollments preserve all historical data.

---

## BR-007

Program templates remain immutable.

User progress must never modify the original program.

---

## BR-008

The assigned program version must be stored to ensure historical consistency.

---

## BR-009

Navigation state is tracked using both the current module and current program item.

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
- INDEX(assigned_at)
- INDEX(last_activity_at)
- INDEX(current_module_id)
- INDEX(current_program_item_id)
- UNIQUE(user_id, program_id, program_version, status)

---

# Lifecycle

Assigned

↓

InProgress

↓

Paused

↓

InProgress

↓

Completed

↓

Archived

or

↓

Cancelled

---

# Notes

This table stores only the user's relationship with a program.

Program templates remain reusable and immutable.

The current module, current item, progress, timestamps, and execution state belong exclusively to this table.

Detailed completion records for individual activities are stored in the `progresses` table.

A user may complete the same program multiple times during their lifetime. Each attempt creates a new enrollment, enabling accurate analytics, recommendations, and AI-driven personalization.
