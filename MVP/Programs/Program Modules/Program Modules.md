# Table: program_modules

| Property | Value |
|----------|-------|
| Table Name | program_modules |
| Status | Approved |
| Database | PostgreSQL |
| Module | Program Management |
| Related Tables | programs, program_items |

---

# Purpose

Represents a logical section within a program.

Modules organize related Program Items into meaningful groups such as weeks, phases, or chapters.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Module identifier |
| program_id | UUID | FK → programs.id | Parent program |
| title | VARCHAR(255) | NOT NULL | Module title |
| description | TEXT | Nullable | Module description |
| sort_order | INTEGER | NOT NULL | Display order within the program |
| is_required | BOOLEAN | DEFAULT TRUE | Required for program completion |
| created_at | TIMESTAMP | NOT NULL | Record creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |
| deleted_at | TIMESTAMP | Nullable | Soft delete timestamp |

---

# Business Rules

## BR-001

Every module belongs to exactly one program.

---

## BR-002

Modules are displayed according to `sort_order`.

---

## BR-003

A Published program must contain at least one module.

---

## BR-004

Soft Delete must be used.

---

# Relationships

program_modules

N → 1 programs

1 → N program_items

---

# Indexes

- PK(id)
- INDEX(program_id)
- INDEX(sort_order)

---

# Notes

Examples:

Workout Program

- Week 1
- Week 2
- Week 3

Nutrition Program

- Week 1
- Week 2

Course

- Chapter 1
- Chapter 2
