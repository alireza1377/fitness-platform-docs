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

Represents reusable digital content used throughout the platform.

Content is independent from programs and can be referenced by multiple Program Items.

The Content Engine supports educational materials, multimedia, AI-generated resources, and future content types.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Content identifier |
| title | VARCHAR(255) | NOT NULL | Content title |
| slug | VARCHAR(255) | UNIQUE, NOT NULL | SEO-friendly identifier |
| description | TEXT | Nullable | Short description |
| content_type | ENUM | NOT NULL | Content type |
| source_type | ENUM | NOT NULL | Storage source |
| url | TEXT | Nullable | Resource URL |
| thumbnail_url | TEXT | Nullable | Preview image |
| duration_seconds | INTEGER | Nullable | Media duration |
| language | VARCHAR(10) | DEFAULT 'en' | Content language |
| visibility | ENUM | NOT NULL | Access level |
| status | ENUM | NOT NULL | Publication status |
| version | INTEGER | DEFAULT 1 | Content version |
| metadata | JSONB | Nullable | Flexible content configuration |
| published_at | TIMESTAMP | Nullable | Publish timestamp |
| created_at | TIMESTAMP | NOT NULL | Creation timestamp |
| updated_at | TIMESTAMP | NOT NULL | Last update timestamp |
| deleted_at | TIMESTAMP | Nullable | Soft delete timestamp |

---

# Enums

## ContentType

- Video
- Audio
- PDF
- Article
- Recipe
- ExerciseDemo
- ImageGallery
- Quiz
- Assessment
- ExternalLink
- AIGenerated
- Custom

---

## SourceType

- Internal
- External
- AI
- CDN

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

Content may exist without belonging to any program.

---

## BR-002

A single Content record may be referenced by multiple Program Items.

---

## BR-003

Published content becomes immutable.

Major updates create a new version.

---

## BR-004

Content uses Soft Delete.

Historical references must remain valid.

---

## BR-005

Premium content requires an active Premium subscription.

---

## BR-006

Business-specific configuration must be stored inside the `metadata` field.

---

# Relationships

contents

1 → N program_items

---

# Indexes

- PK(id)
- UNIQUE(slug)
- INDEX(content_type)
- INDEX(source_type)
- INDEX(status)
- INDEX(visibility)
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

The Content Engine is platform-wide.

Examples:

Video

Audio

PDF

Recipe

Exercise Demonstration

Article

Quiz

AI Generated Lesson

External Link

The metadata field allows unlimited extensibility.

Examples include:

- Video quality
- Subtitle languages
- Calories
- Equipment
- Difficulty
- AI prompt
- File size
- CDN information
- Tags
- SEO metadata
