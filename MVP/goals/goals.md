# Table: goals

| Property | Value |
|----------|-------|
| Table Name | goals |
| Status | Approved |
| Database | PostgreSQL |
| Module | Goal Management |
| Related Tables | users |

---

# Purpose

Stores personal goals defined by users.

Goals help users track their long-term health and fitness objectives.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Goal identifier |
| user_id | UUID | FK → users.id | Goal owner |
| goal_type | ENUM | NOT NULL | Goal category |
| target_value | DECIMAL(10,2) | Nullable | Target value |
| target_date | DATE | Nullable | Target completion date |
| current_value | DECIMAL(8,2) | Nullable    | Current progress toward the goal |

| status | ENUM | NOT NULL | Goal status |
| created_at | TIMESTAMP | NOT NULL | Record creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |
| deleted_at | TIMESTAMP | Nullable | Soft delete timestamp |

---

# Enums

## GoalType

- LoseWeight
- GainWeight
- BuildMuscle
- ImproveFitness
- MaintainWeight
- Custom

---

## GoalStatus

- Active
- Completed
- Cancelled

---

# Business Rules

## BR-001

A user may have multiple active goals.

---

## BR-002

Goals belong to exactly one user.

---

## BR-003

Completed goals become read-only.

---

## BR-004

Soft Delete must be used.

---

# Relationships

goals

N → 1 users

---

# Indexes

- PK(id)
- INDEX(user_id)
- INDEX(goal_type)
- INDEX(status)

---

# Notes

Examples:

- Lose 10 kg
- Gain 5 kg muscle
- Reach 15% body fat
- Exercise 4 times per week
