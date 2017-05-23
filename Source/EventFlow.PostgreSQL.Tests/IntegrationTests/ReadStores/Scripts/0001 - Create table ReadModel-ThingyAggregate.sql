CREATE TABLE "ReadModel-ThingyAggregate"(
	"PingsReceived" INTEGER NOT NULL,
	"DomainErrorAfterFirstReceived" BIT NOT NULL,

	-- -------------------------------------------------
	"Id" BIGSERIAL NOT NULL,
	"AggregateId" VARCHAR(64) NOT NULL,
	"CreateTime" timestamp with time zone NOT NULL,
	"UpdatedTime" timestamp with time zone NOT NULL,
	"LastAggregateSequenceNumber" INTEGER NOT NULL,
	PRIMARY KEY("Id")
);

CREATE UNIQUE INDEX "IX_ReadModel-ThingyAggregate_AggregateId" ON "ReadModel-ThingyAggregate" ("AggregateId");