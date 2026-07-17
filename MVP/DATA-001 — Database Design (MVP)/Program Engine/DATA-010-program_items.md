# Table: program_items

| Property | Value |
|----------|-------|
| Table Name | program_items |
| Status | Approved |
| Database | PostgreSQL |
| Module | Program Management |
| Related Tables | program_modules, contents, progresses |

---

# Purpose

Represents a single activity within a program module.

A Program Item can be a workout, meal, video, article, quiz, or any executable step in a program.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Program item identifier |
| module_id | UUID | FK → program_modules.id | Parent module |
| content_id | UUID | FK → contents.id, Nullable | Related content |
| title | VARCHAR(255) | NOT NULL | Item title |
| item_type | ENUM | NOT NULL | Item type |
| sort_order | INTEGER | NOT NULL | Display order |
| estimated_minutes | INTEGER | Nullable | Estimated completion time |
| is_required | BOOLEAN | DEFAULT TRUE | Required for program completion |
| created_at | TIMESTAMP | NOT NULL | Record creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |
| deleted_at | TIMESTAMP | Nullable | Soft delete timestamp |

---

# Enums

## ProgramItemType

- Workout
- Meal
- Video
- Article
- Quiz
- Task

---

# Business Rules

## BR-001

Every Program Item belongs to exactly one Program Module.

---

## BR-002

Items are displayed according to `sort_order`.

---

## BR-003

A Program Item may optionally reference a Content record.

---

## BR-004

Soft Delete must be used.

---

## Relationships

program_items

N → 1 program_modules

N → 1 contents

1 → N progresses

---

# Indexes

- PK(id)
- INDEX(module_id)
- INDEX(sort_order)
- INDEX(item_type)

---

# Notes

Examples:

Workout Program

- Warm-up
- Push-up
- Squat
- Stretching

Nutrition Program

- Breakfast
- Lunch
- Dinner

Course

- Watch Video
- Read Article
- Take Quiz
