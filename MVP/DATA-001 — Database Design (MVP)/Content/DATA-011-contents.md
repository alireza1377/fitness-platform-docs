# Table: contents

| Property | Value |
|----------|-------|
| Table Name | contents |
| Status | Approved |
| Database | PostgreSQL |
| Module | Content Management |
| Related Tables | program_items |

---

# Purpose

Represents reusable content that can be attached to one or more Program Items.

Content may be a video, article, recipe, PDF, or any educational resource.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Content identifier |
| title | VARCHAR(255) | NOT NULL | Content title |
| description | TEXT | Nullable | Content description |
| content_type | ENUM | NOT NULL | Content type |
| url | TEXT | NOT NULL | Content location |
| duration_seconds | INTEGER | Nullable | Duration for media content |
| visibility | ENUM | NOT NULL | Access level |
| status | ENUM | NOT NULL | Publication status |
| created_at | TIMESTAMP | NOT NULL | Record creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |
| deleted_at | TIMESTAMP | Nullable | Soft delete timestamp |

---

# Enums

## ContentType

- Video
- Article
- PDF
- Recipe
- Image

---

## Visibility

- Free
- Premium

---

## ContentStatus

- Draft
- Published
- Archived

---

# Business Rules

## BR-001

Content can exist without being assigned to any Program Item.

---

## BR-002

A single Content record may be referenced by multiple Program Items.

---

## BR-003

Only Published content can be displayed to users.

---

## BR-004

Soft Delete must be used.

---

# Relationships

contents

1 → N program_items

---

# Indexes

- PK(id)
- INDEX(content_type)
- INDEX(status)
- INDEX(visibility)

---

# Notes

Examples:

- Workout Video
- Exercise Guide
- Nutrition Article
- Healthy Recipe
- Stretching PDF

Content is reusable and independent from Programs.
