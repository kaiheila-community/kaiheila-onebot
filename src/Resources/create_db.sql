BEGIN TRANSACTION;

CREATE TABLE "Assets" (
    "Url" TEXT NOT NULL CONSTRAINT "PK_Assets" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Path" TEXT NOT NULL
);

COMMIT;