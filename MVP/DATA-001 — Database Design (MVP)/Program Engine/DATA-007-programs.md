# Table: programs

| Property | Value |
|----------|-------|
| Table Name | programs |
| Status | Approved |
| Database | PostgreSQL |
| Module | Program Management |
| Related Tables | professionals, program_items, user_programs |

---

# Purpose

Represents a reusable health program template.

A program defines the structure, objectives, and metadata of a health journey.

Programs are reusable and are **never assigned directly to users**.

User enrollment and progress are managed through the `user_programs` table.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Program identifier |
| title | VARCHAR(255) | NOT NULL | Program title |
| slug | VARCHAR(255) | UNIQUE, NOT NULL | SEO-friendly unique identifier |
| description | TEXT | Nullable | Program description |
| type | ENUM | NOT NULL | Program category |
| difficulty | ENUM | NOT NULL | Difficulty level |
| visibility | ENUM | NOT NULL | Access level |
| status | ENUM | NOT NULL | Publication status |
| estimated_duration_days | SMALLINT | Nullable | Estimated completion duration in days |
| estimated_total_minutes | INTEGER | Nullable | Estimated total training time |
| thumbnail_url | TEXT | Nullable | Cover image |
| is_featured | BOOLEAN | DEFAULT FALSE | Featured program |
| metadata | JSONB | Nullable | Flexible configuration for future features |
| version | INTEGER | DEFAULT 1 | Program version |
| professional_id | UUID | FK → professionals.id | Program owner |
| published_at | TIMESTAMP | Nullable | Publication timestamp |
| created_at | TIMESTAMP | NOT NULL | Creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |
| deleted_at | TIMESTAMP | Nullable | Soft delete timestamp |

---

# Enums

## ProgramType

- Workout
- Nutrition
- Hybrid
- Yoga
- Rehabilitation

---

## DifficultyLevel

- Beginner
- Intermediate
- Advanced

---

## Visibility

- Free
- Premium

---

## ProgramStatus

- Draft
- Published
- Archived

---

# Business Rules

## BR-001

A program must contain at least one Program Item before it can be published.

---

## BR-002

Only programs with the **Published** status can be assigned to users.

---

## BR-003

Archived programs remain available for historical records but cannot be assigned to new users.

---

## BR-004

Programs use Soft Delete.

Historical data must never be physically removed.

---

## BR-005

Each program belongs to exactly one professional.

---

## BR-006

Published programs are immutable.

Significant modifications create a new program version instead of modifying the published version.

---

## BR-007

A program template may be assigned to unlimited users simultaneously.

---

## BR-008

Premium programs require an active Premium subscription.

---

## Relationships

programs

1 → N program_items

1 → N user_programs

N → 1 professionals

---

# Indexes

- PK(id)
- UNIQUE(slug)
- INDEX(type)
- INDEX(status)
- INDEX(visibility)
- INDEX(is_featured)
- INDEX(professional_id)
- INDEX(published_at)

---

# Lifecycle

Draft

↓

Published

↓

Archived

---

# Notes

Programs are reusable templates.

User-specific information such as:

- Assignment
- Start Date
- Progress
- Completion Status
- Pause / Resume
- Current Position

is stored in the `user_programs` table.

The `metadata` column allows future expansion without requiring database schema changes.

Examples include:

- Required equipment
- Weekly sessions
- Estimated calories
- AI-generated recommendations
- Tags
- Custom configuration
