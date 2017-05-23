CREATE TABLE "ReadModel-ThingyMessage" (
	"Id" BIGSERIAL NOT NULL,
	"ThingyId" VARCHAR(64) NOT NULL,
	"MessageId" VARCHAR(64) NOT NULL,
	"Message" TEXT NOT NULL,
	PRIMARY KEY("Id")
);

CREATE UNIQUE INDEX "IX_ReadModel-ThingyMessage_AggregateId" ON "ReadModel-ThingyMessage" ("MessageId");

CREATE INDEX "IX_ReadModel-ThingyMessage_ThingyId" ON "ReadModel-ThingyMessage" ("ThingyId");