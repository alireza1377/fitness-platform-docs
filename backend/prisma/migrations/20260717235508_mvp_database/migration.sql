/*
  Warnings:

  - You are about to alter the column `email` on the `User` table. The data in that column could be lost. The data in that column will be cast from `Text` to `VarChar(255)`.
  - You are about to alter the column `phone` on the `User` table. The data in that column could be lost. The data in that column will be cast from `Text` to `VarChar(20)`.

*/
-- CreateEnum
CREATE TYPE "public"."SubscriptionStatus" AS ENUM ('active', 'canceled', 'expired', 'pending');

-- CreateEnum
CREATE TYPE "public"."PaymentStatus" AS ENUM ('pending', 'paid', 'failed', 'refunded');

-- CreateEnum
CREATE TYPE "public"."PaymentProvider" AS ENUM ('zarinpal', 'idpay', 'nextpay', 'manual');

-- CreateEnum
CREATE TYPE "public"."ProgramStatus" AS ENUM ('draft', 'published', 'archived');

-- CreateEnum
CREATE TYPE "public"."ModuleType" AS ENUM ('week', 'phase', 'custom');

-- CreateEnum
CREATE TYPE "public"."ItemType" AS ENUM ('workout', 'nutrition', 'habit', 'rest', 'assessment');

-- CreateEnum
CREATE TYPE "public"."ContentType" AS ENUM ('video', 'article', 'image', 'audio', 'pdf');

-- CreateEnum
CREATE TYPE "public"."UserProgramStatus" AS ENUM ('active', 'completed', 'paused', 'canceled');

-- CreateEnum
CREATE TYPE "public"."ProgressStatus" AS ENUM ('pending', 'completed', 'skipped');

-- AlterTable
ALTER TABLE "public"."User" ALTER COLUMN "email" DROP NOT NULL,
ALTER COLUMN "email" SET DATA TYPE VARCHAR(255),
ALTER COLUMN "phone" SET DATA TYPE VARCHAR(20),
ALTER COLUMN "passwordHash" DROP NOT NULL;

-- CreateTable
CREATE TABLE "public"."Subscription" (
    "id" TEXT NOT NULL,
    "userId" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "price" DECIMAL(10,2) NOT NULL,
    "startDate" TIMESTAMP(3) NOT NULL,
    "endDate" TIMESTAMP(3) NOT NULL,
    "status" "public"."SubscriptionStatus" NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Subscription_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "public"."Payment" (
    "id" TEXT NOT NULL,
    "userId" TEXT NOT NULL,
    "amount" DECIMAL(10,2) NOT NULL,
    "provider" "public"."PaymentProvider" NOT NULL,
    "status" "public"."PaymentStatus" NOT NULL,
    "transactionId" TEXT,
    "authority" TEXT,
    "refId" TEXT,
    "paidAt" TIMESTAMP(3),
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "Payment_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "public"."Program" (
    "id" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "slug" TEXT NOT NULL,
    "description" TEXT,
    "thumbnailUrl" TEXT,
    "difficulty" INTEGER NOT NULL,
    "estimatedDays" INTEGER,
    "estimatedWeeks" INTEGER,
    "isPremium" BOOLEAN NOT NULL DEFAULT false,
    "status" "public"."ProgramStatus" NOT NULL DEFAULT 'draft',
    "createdBy" TEXT,
    "updatedBy" TEXT,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Program_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "public"."ProgramModule" (
    "id" TEXT NOT NULL,
    "programId" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "description" TEXT,
    "sortOrder" INTEGER NOT NULL,
    "moduleType" "public"."ModuleType" NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "ProgramModule_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "public"."ProgramItem" (
    "id" TEXT NOT NULL,
    "moduleId" TEXT NOT NULL,
    "contentId" TEXT,
    "title" TEXT NOT NULL,
    "description" TEXT,
    "itemType" "public"."ItemType" NOT NULL,
    "dayNumber" INTEGER,
    "sortOrder" INTEGER NOT NULL,
    "isRequired" BOOLEAN NOT NULL DEFAULT true,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "ProgramItem_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "public"."Content" (
    "id" TEXT NOT NULL,
    "title" TEXT NOT NULL,
    "description" TEXT,
    "type" "public"."ContentType" NOT NULL,
    "url" TEXT NOT NULL,
    "thumbnailUrl" TEXT,
    "duration" INTEGER,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Content_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "public"."UserProgram" (
    "id" TEXT NOT NULL,
    "userId" TEXT NOT NULL,
    "programId" TEXT NOT NULL,
    "status" "public"."UserProgramStatus" NOT NULL DEFAULT 'active',
    "startedAt" TIMESTAMP(3),
    "completedAt" TIMESTAMP(3),
    "currentDay" INTEGER NOT NULL DEFAULT 1,
    "progressPercent" DOUBLE PRECISION NOT NULL DEFAULT 0,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "UserProgram_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "public"."Progress" (
    "id" TEXT NOT NULL,
    "userProgramId" TEXT NOT NULL,
    "programItemId" TEXT NOT NULL,
    "status" "public"."ProgressStatus" NOT NULL DEFAULT 'pending',
    "completedAt" TIMESTAMP(3),
    "note" TEXT,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "Progress_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE INDEX "Subscription_userId_idx" ON "public"."Subscription"("userId");

-- CreateIndex
CREATE INDEX "Subscription_status_idx" ON "public"."Subscription"("status");

-- CreateIndex
CREATE INDEX "Payment_userId_idx" ON "public"."Payment"("userId");

-- CreateIndex
CREATE INDEX "Payment_status_idx" ON "public"."Payment"("status");

-- CreateIndex
CREATE UNIQUE INDEX "Program_slug_key" ON "public"."Program"("slug");

-- CreateIndex
CREATE INDEX "Program_status_idx" ON "public"."Program"("status");

-- CreateIndex
CREATE INDEX "ProgramModule_programId_idx" ON "public"."ProgramModule"("programId");

-- CreateIndex
CREATE INDEX "ProgramModule_sortOrder_idx" ON "public"."ProgramModule"("sortOrder");

-- CreateIndex
CREATE INDEX "ProgramItem_moduleId_idx" ON "public"."ProgramItem"("moduleId");

-- CreateIndex
CREATE INDEX "ProgramItem_sortOrder_idx" ON "public"."ProgramItem"("sortOrder");

-- CreateIndex
CREATE INDEX "UserProgram_userId_idx" ON "public"."UserProgram"("userId");

-- CreateIndex
CREATE INDEX "UserProgram_programId_idx" ON "public"."UserProgram"("programId");

-- CreateIndex
CREATE UNIQUE INDEX "UserProgram_userId_programId_key" ON "public"."UserProgram"("userId", "programId");

-- CreateIndex
CREATE INDEX "Progress_userProgramId_idx" ON "public"."Progress"("userProgramId");

-- CreateIndex
CREATE INDEX "Progress_programItemId_idx" ON "public"."Progress"("programItemId");

-- CreateIndex
CREATE INDEX "Progress_status_idx" ON "public"."Progress"("status");

-- CreateIndex
CREATE UNIQUE INDEX "Progress_userProgramId_programItemId_key" ON "public"."Progress"("userProgramId", "programItemId");

-- AddForeignKey
ALTER TABLE "public"."Subscription" ADD CONSTRAINT "Subscription_userId_fkey" FOREIGN KEY ("userId") REFERENCES "public"."User"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "public"."Payment" ADD CONSTRAINT "Payment_userId_fkey" FOREIGN KEY ("userId") REFERENCES "public"."User"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "public"."ProgramModule" ADD CONSTRAINT "ProgramModule_programId_fkey" FOREIGN KEY ("programId") REFERENCES "public"."Program"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "public"."ProgramItem" ADD CONSTRAINT "ProgramItem_moduleId_fkey" FOREIGN KEY ("moduleId") REFERENCES "public"."ProgramModule"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "public"."ProgramItem" ADD CONSTRAINT "ProgramItem_contentId_fkey" FOREIGN KEY ("contentId") REFERENCES "public"."Content"("id") ON DELETE SET NULL ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "public"."UserProgram" ADD CONSTRAINT "UserProgram_userId_fkey" FOREIGN KEY ("userId") REFERENCES "public"."User"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "public"."UserProgram" ADD CONSTRAINT "UserProgram_programId_fkey" FOREIGN KEY ("programId") REFERENCES "public"."Program"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "public"."Progress" ADD CONSTRAINT "Progress_userProgramId_fkey" FOREIGN KEY ("userProgramId") REFERENCES "public"."UserProgram"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "public"."Progress" ADD CONSTRAINT "Progress_programItemId_fkey" FOREIGN KEY ("programItemId") REFERENCES "public"."ProgramItem"("id") ON DELETE CASCADE ON UPDATE CASCADE;
