# Table: subscriptions

| Property | Value |
|----------|-------|
| Table Name | subscriptions |
| Status | Approved |
| Database | PostgreSQL |
| Related Tables | users, payments |

---

# Purpose

Stores the current subscription assigned to each user.

The subscription determines the user's access level (Free or Premium) and remains independent from payment transactions.

Payment history is stored in the `payments` table.

---

# Columns

| Column | Type | Constraints | Description |
|---------|------|-------------|-------------|
| id | UUID | Primary Key | Subscription identifier |
| user_id | UUID | FK → users.id, UNIQUE | Subscription owner |
| plan | ENUM | NOT NULL | Subscription plan |
| status | ENUM | NOT NULL | Current subscription status |
| starts_at | TIMESTAMP | NOT NULL | Subscription start date |
| expires_at | TIMESTAMP | Nullable | Expiration date (NULL for Free plan) |
| auto_renew | BOOLEAN | Default FALSE | Automatically renew subscription |
| created_at | TIMESTAMP | NOT NULL | Record creation time |
| updated_at | TIMESTAMP | NOT NULL | Last update time |

---

# Enums

## SubscriptionPlan

- Free
- Premium

---

## SubscriptionStatus

- Pending
- Active
- Expired
- Cancelled

---

# Business Rules

## BR-001

Every user must have exactly one active subscription record.

---

## BR-002

Every newly registered user automatically receives the Free plan.

---

## BR-003

Premium content is accessible only when:

- plan = Premium
- status = Active
- expires_at > Current Date

---

## BR-004

Free subscriptions never expire.

For Free plans:

expires_at = NULL

---

## BR-005

Cancelled subscriptions immediately lose Premium access.

---

## BR-006

Expired subscriptions automatically behave as Free users until a new Premium subscription is activated.

---

## BR-007

Subscription changes must never delete historical payment records.

---

# Relationships

subscriptions

N → 1 users

1 → N payments

---

# Indexes

- PK(id)
- UNIQUE(user_id)
- INDEX(plan)
- INDEX(status)
- INDEX(expires_at)

---

# Lifecycle

Created

↓

Pending

↓

Active

↓

Expired / Cancelled

↓

New Premium Purchase

↓

Active

---

# Notes

This table stores only the current subscription state.

Financial information, payment gateway responses, invoices, refunds, and transaction history belong to the `payments` table.

Keeping subscriptions and payments separated simplifies future support for:

- Multiple payment gateways
- Monthly and yearly plans
- Discount codes
- Refunds
- Invoices
- Recurring billing
- Enterprise subscriptions

without changing the subscription model.
