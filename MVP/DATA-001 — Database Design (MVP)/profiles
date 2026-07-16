# Table: profiles

## Purpose

Stores personal, physical, and fitness-related information used to personalize the user experience.

---

| Column | Type | Constraints |
|---------|------|-------------|
| id | UUID | PK |
| user_id | UUID | FK → users.id, UNIQUE |
| first_name | VARCHAR(100) | NOT NULL |
| last_name | VARCHAR(100) | Nullable |
| gender | ENUM | Male, Female, Other |
| birth_date | DATE | Nullable |
| height_cm | SMALLINT | Nullable |
| current_weight_kg | DECIMAL(5,2) | Nullable |
| fitness_level | ENUM | Beginner, Intermediate, Advanced |
| activity_level | ENUM | Sedentary, Light, Moderate, Active |
| goal_summary | VARCHAR(255) | Nullable |
| avatar_url | TEXT | Nullable |
| created_at | TIMESTAMP | NOT NULL |
| updated_at | TIMESTAMP | NOT NULL |

---

## Rules

- Every user has exactly one profile.
- Profile data can be updated by the user.
- Height and weight represent the latest known values.
- Historical body measurements are stored separately in the `measurements` table.

---

## Relationships

Profile

1 → 1 User
