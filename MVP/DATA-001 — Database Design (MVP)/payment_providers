# Table: payment_providers

| Property | Value |
|----------|-------|
| Table Name | payment_providers |
| Status | Approved |
| Database | PostgreSQL |
| Related Tables | payments |

---

# Purpose

Stores the payment gateways supported by the platform.

The table allows the system to switch or add payment providers without modifying the payment model.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Provider identifier |
| name | VARCHAR(100) | NOT NULL | Provider display name |
| code | VARCHAR(50) | UNIQUE, NOT NULL | Internal provider code |
| is_active | BOOLEAN | Default TRUE | Indicates whether the provider is available |
| created_at | TIMESTAMP | NOT NULL | Record creation time |
| updated_at | TIMESTAMP | NOT NULL | Last update time |

---

# Business Rules

## BR-001

Each provider must have a unique code.

---

## BR-002

Only active providers can be used to create new payments.

---

## BR-003

Disabling a provider must not affect existing payment records.

---

# Relationships

payment_providers

1 → N payments

---

# Seed Data

| Name | Code |
|------|------|
| ZarinPal | zarinpal |
| IDPay | idpay |
| Stripe | stripe |
| PayPal | paypal |

---

# Indexes

- PK(id)
- UNIQUE(code)
- INDEX(is_active)

---

# Notes

The platform should reference providers using the `code` field internally.

Example:

- zarinpal
- stripe
- paypal

The display name may change without affecting the application logic.
