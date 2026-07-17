# Table: measurements

| Property | Value |
|----------|-------|
| Table Name | measurements |
| Status | Approved |
| Database | PostgreSQL |
| Module | Measurement Tracking |
| Related Tables | users |

---

# Purpose

Stores users' body measurements over time.

Measurements allow users to track their physical progress throughout their health journey.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Measurement identifier |
| user_id | UUID | FK → users.id | Measurement owner |
| measurement_type | ENUM | NOT NULL | Measurement category |
| value | DECIMAL(8,2) | NOT NULL | Recorded value |
| unit | VARCHAR(20) | NOT NULL | Measurement unit |
| measured_at | TIMESTAMP | NOT NULL | Measurement date |
| created_at | TIMESTAMP | NOT NULL | Record creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |

---

# Enums

## MeasurementType

- Weight
- Height
- BodyFat
- BMI
- Waist
- Chest
- Hip
- Arm
- Thigh
- Custom

---

# Business Rules

## BR-001

Every measurement belongs to exactly one user.

---

## BR-002

Users may record unlimited measurements.

---

## BR-003

Measurements are immutable.

If a value changes, a new record must be created.

---

# Relationships

measurements

N → 1 users

---

# Indexes

- PK(id)
- INDEX(user_id)
- INDEX(measurement_type)
- INDEX(measured_at)

---

# Notes

Examples:

Weight → 82 kg

Body Fat → 18 %

Waist → 92 cm

Height → 180 cm

Measurements provide historical data for progress charts and analytics.
