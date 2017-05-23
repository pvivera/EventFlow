DO $$
BEGIN

IF NOT EXISTS (
    SELECT 1
    FROM   pg_catalog.pg_class c
    JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
    WHERE  c.relname = 'EventFlow'
    ) THEN

    CREATE TABLE IF NOT EXISTS "EventFlow" (
    "GlobalSequenceNumber" BIGSERIAL NOT NULL,
    "BatchId" UUID NOT NULL,
    "AggregateId" VARCHAR(255) NOT NULL,
    "AggregateName" VARCHAR(255) NOT NULL,
    "Data" BYTEA NOT NULL,
    "Metadata" BYTEA NOT NULL,
    "AggregateSequenceNumber" INTEGER NOT NULL,
    PRIMARY KEY("GlobalSequenceNumber")
    );
END IF;

IF NOT EXISTS (
    SELECT 1
    FROM   pg_catalog.pg_class c
    JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
    WHERE  c.relname = 'IX_EventFlow_AggregateId_AggregateSequenceNumber'
    ) THEN

    CREATE INDEX "IX_EventFlow_AggregateId_AggregateSequenceNumber" ON "EventFlow" ("AggregateId", "AggregateSequenceNumber");
END IF;

END$$;