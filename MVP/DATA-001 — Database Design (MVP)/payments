# Table: payments

| Property | Value |
|----------|-------|
| Table Name | payments |
| Status | Approved |
| Database | PostgreSQL |
| Related Tables | users, subscriptions, payment_providers |

---

# Purpose

Stores all payment transactions made by users.

Every payment attempt, whether successful or unsuccessful, must be recorded.

Payment records are immutable and serve as the financial audit trail of the platform.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Payment identifier |
| user_id | UUID | FK → users.id | User who initiated the payment |
| subscription_id | UUID | FK → subscriptions.id | Related subscription |
| provider_id | UUID | FK → payment_providers.id | Payment gateway |
| amount | DECIMAL(12,2) | NOT NULL | Payment amount |
| currency | VARCHAR(10) | NOT NULL | Currency code (e.g. IRR, USD) |
| status | ENUM | NOT NULL | Payment status |
| authority | VARCHAR(255) | Nullable | Gateway authority/token |
| transaction_id | VARCHAR(255) | Nullable | Gateway transaction/reference ID |
| gateway_response | JSONB | Nullable | Raw gateway response |
| paid_at | TIMESTAMP | Nullable | Successful payment time |
| failure_reason | TEXT | Nullable | Failure description |
| created_at | TIMESTAMP | NOT NULL | Record creation time |
| updated_at | TIMESTAMP | NOT NULL | Last update time |

---

# Enums

## PaymentStatus

- Pending
- Processing
- Succeeded
- Failed
- Cancelled
- Refunded

---

# Business Rules

## BR-001

Every payment attempt must create a payment record.

---

## BR-002

Payment records must never be deleted.

---

## BR-003

A subscription becomes Active only after a payment reaches the Succeeded status.

---

## BR-004

Failed or Cancelled payments must not activate a subscription.

---

## BR-005

The complete gateway response should be stored in `gateway_response` for troubleshooting and auditing.

---

## BR-006

Each successful payment should contain both:

- authority
- transaction_id

---

## BR-007

Refund operations create a new payment state by updating the status to Refunded while preserving the original payment data.

---

# Relationships

payments

N → 1 users

N → 1 subscriptions

N → 1 payment_providers

---

# Indexes

- PK(id)
- INDEX(user_id)
- INDEX(subscription_id)
- INDEX(provider_id)
- INDEX(status)
- INDEX(created_at)
- UNIQUE(transaction_id)

---

# Lifecycle

Pending

↓

Processing

↓

Succeeded
        │
        └────→ Refunded

or

↓

Failed

or

↓

Cancelled

---

# Notes

The `payments` table is the platform's financial source of truth.

No payment record should ever be physically deleted or modified in a way that removes historical financial information.

The `gateway_response` column stores the raw response from the payment gateway to simplify debugging, reconciliation, and auditing.
