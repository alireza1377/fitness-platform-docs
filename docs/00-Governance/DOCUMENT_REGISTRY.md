# Document Registry

| Item          | Value             |
| ------------- | ----------------- |
| Document ID   | GOV-003           |
| Version       | 1.0               |
| Status        | Approved          |
| Project       | Fitness Platform  |
| Document Type | Governance        |
| Owner         | Architecture Team |
| Language      | English           |
| Last Updated  | 2026-07-16        |

---

# Purpose

The Document Registry is the central index of all official project documents.

Its purpose is to:

* Maintain a complete inventory of project documentation.
* Define ownership and document status.
* Track document versions.
* Provide a single entry point for humans and AI assistants.
* Preserve documentation consistency throughout the project lifecycle.

Every official document must be registered before it is considered part of the project knowledge base.

---

# Document Lifecycle

```text
Planned
    ↓
Draft
    ↓
In Review
    ↓
Approved
    ↓
Released
    ↓
Deprecated (Optional)
```

---

# Status Definitions

| Status     | Description                                 |
| ---------- | ------------------------------------------- |
| Planned    | Document is scheduled but not yet created.  |
| Draft      | Initial version under development.          |
| In Review  | Under technical or business review.         |
| Approved   | Accepted as the official version.           |
| Released   | Published and used by the development team. |
| Deprecated | No longer recommended for use.              |

---

# Document Naming Convention

Document IDs follow this pattern:

```text
<Category>-<Number>
```

Examples:

```text
GOV-001
PROD-001
BUS-003
ARCH-001
DATA-001
API-001
```

---

# Categories

| Prefix | Category                     |
| ------ | ---------------------------- |
| GOV    | Governance                   |
| PROD   | Product                      |
| BUS    | Business                     |
| ARCH   | Architecture                 |
| DATA   | Database                     |
| API    | API Design                   |
| SEC    | Security                     |
| DEV    | Development                  |
| TEST   | Testing                      |
| OPS    | Operations                   |
| ADR    | Architecture Decision Record |

---

# Official Document Index

| ID       | Document                       | Category     | Status   | Version |
| -------- | ------------------------------ | ------------ | -------- | ------- |
| GOV-001  | AI Manifest                    | Governance   | Approved | 1.0     |
| GOV-002  | Project Context                | Governance   | Draft    | 1.0     |
| GOV-003  | Document Registry              | Governance   | Approved | 1.0     |
| GOV-004  | Documentation Standard         | Governance   | Planned  | -       |
| GOV-005  | Coding Standard                | Governance   | Planned  | -       |
| GOV-006  | Versioning Strategy            | Governance   | Planned  | -       |
| GOV-007  | Contribution Guide             | Governance   | Planned  | -       |
| GOV-008  | ADR Guideline                  | Governance   | Planned  | -       |
| PROD-001 | Product Vision                 | Product      | Planned  | -       |
| PROD-002 | Product Requirements Document  | Product      | Planned  | -       |
| PROD-003 | Product Roadmap                | Product      | Planned  | -       |
| BUS-001  | Business Capability Map        | Business     | Approved | 2.0     |
| BUS-002  | Context Map                    | Business     | Approved | 2.0     |
| BUS-003  | Domain Glossary                | Business     | Approved | 2.0     |
| BUS-004  | Use Case Model                 | Business     | Approved | 2.0     |
| ARCH-001 | Software Architecture Document | Architecture | Planned  | -       |
| ARCH-002 | C4 Model                       | Architecture | Planned  | -       |
| ARCH-003 | Backend Architecture           | Architecture | Planned  | -       |
| ARCH-004 | Frontend Architecture          | Architecture | Planned  | -       |
| ARCH-005 | Deployment Architecture        | Architecture | Planned  | -       |
| DATA-001 | Database Design                | Database     | Planned  | -       |
| DATA-002 | ER Diagram                     | Database     | Planned  | -       |
| API-001  | REST API Specification         | API          | Planned  | -       |
| API-002  | API Error Standard             | API          | Planned  | -       |
| SEC-001  | Security Architecture          | Security     | Planned  | -       |
| DEV-001  | Development Workflow           | Development  | Planned  | -       |
| TEST-001 | Testing Strategy               | Testing      | Planned  | -       |
| OPS-001  | Deployment Guide               | Operations   | Planned  | -       |

---

# Document Dependency Rules

1. Governance documents have the highest priority.
2. Product documents define business intent.
3. Business documents define domain behavior.
4. Architecture documents translate business into technical design.
5. Database and API documents implement architectural decisions.
6. Lower-level documents must never contradict higher-level documents.

---

# Repository Rules

* Every document must have a unique Document ID.
* Every document must contain version information.
* Every document must include Change History.
* Every document must include AI Metadata.
* Every document must be written in English.
* Mermaid must be used for all diagrams.
* Markdown is the standard documentation format.

---

# AI Metadata

```yaml
document_id: GOV-003
version: 1.0
status: Approved

depends_on:
  - GOV-001

used_by:
  - Entire Repository

priority: Critical
```

---

# Knowledge Graph

```yaml
produces:
  - Repository Index

consumes:
  - All Official Documents

affects:
  - Entire Documentation

owner:
  - Governance
```

---

# Change History

| Version | Date       | Description      |
| ------- | ---------- | ---------------- |
| 1.0     | 2026-07-16 | Initial version. |
