DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'eventdatamodel_list_type') THEN

	CREATE TYPE eventdatamodel_list_type AS
	(
		AggregateId VARCHAR(255),
		AggregateName VARCHAR(255),
		AggregateSequenceNumber INTEGER,
		BatchId UUID,
		Data BYTEA,
		Metadata BYTEA
	);
	
	END IF;
END$$;