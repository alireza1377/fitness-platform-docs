# Table: progresses

| Property | Value |
|----------|-------|
| Table Name | progresses |
| Status | Approved |
| Database | PostgreSQL |
| Module | Progress Tracking |
| Related Tables | user_programs, program_items |

---

# Purpose

Stores the completion history of Program Items for each user enrollment.

Each record represents a completed activity within a user's program.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Progress identifier |
| user_program_id | UUID | FK → user_programs.id | User enrollment |
| program_item_id | UUID | FK → program_items.id | Completed program item |
| status | ENUM | NOT NULL | Completion status |
| completed_at | TIMESTAMP | Nullable | Completion timestamp |
| created_at | TIMESTAMP | NOT NULL | Record creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |

---

# Enums

## ProgressStatus

- NotStarted
- InProgress
- Completed
- Skipped

---

# Business Rules

## BR-001

A Progress record belongs to exactly one User Program.

---

## BR-002

A Progress record belongs to exactly one Program Item.

---

## BR-003

A user can complete each Program Item only once within the same User Program.

---

## BR-004

Only records with the status `Completed` contribute to the overall program progress.

---

## BR-005

Skipped items are excluded from the completion percentage unless they are marked as required.

---

# Relationships

progresses

N → 1 user_programs

N → 1 program_items

---

# Indexes

- PK(id)
- INDEX(user_program_id)
- INDEX(program_item_id)
- INDEX(status)
- UNIQUE(user_program_id, program_item_id)

---

# Notes

The progress percentage displayed to the user is calculated from this table.

Examples:

Workout Program

- Warm-up → Completed
- Push-up → Completed
- Stretching → InProgress

Nutrition Program

- Breakfast → Completed
- Lunch → Skipped

Course

- Video Lesson → Completed
- Quiz → InProgress
