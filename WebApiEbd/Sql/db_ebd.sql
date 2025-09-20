CREATE TABLE "country_origin" (
	"id" serial PRIMARY KEY,
	"name" VARCHAR(100) UNIQUE NOT NULL
);
CREATE TABLE "brand" (
	"id" serial PRIMARY KEY,
	"name" VARCHAR(100) UNIQUE NOT NULL,
	"country_origin_id" INTEGER NOT NULL
);
CREATE TABLE "provider" (
	"id" serial PRIMARY KEY,
	"ruc" VARCHAR(11) UNIQUE NOT NULL,
	"name" VARCHAR(200) NOT NULL,
	"address" VARCHAR(255),
	"district" VARCHAR(100) NOT NULL,
	"province" VARCHAR(100) NOT NULL,
	"department" VARCHAR NOT NULL
);
ALTER TABLE "provider"
ADD COLUMN "status" VARCHAR(20) NOT NULL,
	ADD COLUMN "email" VARCHAR(200),
	ADD COLUMN "phone" VARCHAR(15);
CREATE TABLE "contract" (
	"id" serial PRIMARY KEY,
	"title" VARCHAR(200) NOT NULL,
	"start_date" date NOT NULL,
	"end_date" date NOT NULL,
	"amount" NUMERIC(12, 2) NOT NULL,
	"user_id" INTEGER NOT NULL,
	"status" VARCHAR(20) NOT NULL DEFAULT 'PLACED' CHECK (
		"status" IN ('PLACED', 'CONFIRM', 'PROCESSED', 'COMPLETE')
	),
	"route" TEXT,
	"created_at" TIMESTAMP DEFAULT NOW(),
	"updated_at" TIMESTAMP DEFAULT NOW(),
	"provider_id" INTEGER NOT NULL,
	"order_id" INTEGER
);
CREATE TABLE "purchase_order" (
	"id" serial PRIMARY KEY,
	"created_at" TIMESTAMP DEFAULT NOW(),
	"total" NUMERIC(12, 2) NOT NULL,
	"status" VARCHAR(20) NOT NULL DEFAULT 'DRAFT' CHECK (
		"status" IN ('DRAFT', 'PLACED', 'APPROVED', 'REJECTED')
	),
	"user_id" INTEGER NOT NULL,
	"provider_id" INTEGER NOT NULL
);
CREATE TABLE "purchase_order_device" (
	"order_id" INTEGER,
	"device_id" INTEGER,
	"quantity" INTEGER NOT NULL DEFAULT 1,
	"price" NUMERIC(12, 2) NOT NULL,
	PRIMARY KEY ("order_id", "device_id")
);
CREATE TABLE "department" (
	"id" serial PRIMARY KEY,
	"name" VARCHAR(200) UNIQUE NOT NULL
);
CREATE TABLE "role" (
	"id" serial PRIMARY KEY,
	"name" VARCHAR(50) UNIQUE NOT NULL,
	"description" VARCHAR NOT NULL
);
CREATE TABLE "user" (
	"id" serial PRIMARY KEY,
	"email" VARCHAR(200) UNIQUE NOT NULL,
	"username" VARCHAR(50) UNIQUE NOT NULL,
	"password" TEXT NOT NULL,
	"name" VARCHAR(100),
	"phone" VARCHAR(9),
	"status" CHAR(1) NOT NULL DEFAULT 'A' CHECK ("status" IN ('A', 'I')),
	"gender" CHAR(1) NOT NULL CHECK ("gender" IN ('M', 'F')),
	"avatar_url" TEXT,
	"role_id" INTEGER NOT NULL,
	"created_at" TIMESTAMP DEFAULT (NOW()),
	"updated_at" TIMESTAMP DEFAULT (NOW()),
	"department_id" INTEGER
);
CREATE TABLE "device" (
	"id" serial PRIMARY KEY,
	"name" VARCHAR(200) NOT NULL,
	"description" TEXT,
	"price" NUMERIC(12, 2) NOT NULL,
	"model" VARCHAR(100),
	"serial_number" VARCHAR(100) UNIQUE NOT NULL,
	"status" VARCHAR(20) NOT NULL DEFAULT 'ACTIVE' CHECK (
		"status" IN ('ACTIVE', 'INACTIVE', 'REPAIR', 'DISPOSED')
	),
	"created_at" TIMESTAMP DEFAULT (NOW()),
	"updated_at" TIMESTAMP DEFAULT (NOW()),
	"brand_id" INTEGER NOT NULL
);
CREATE TABLE "contracts_device" (
	"contract_id" INTEGER,
	"device_id" INTEGER,
	"rental_price" NUMERIC(12, 2) NOT NULL,
	PRIMARY KEY ("contract_id", "device_id")
);
CREATE TABLE "movement" (
	"id" serial PRIMARY KEY,
	"date" TIMESTAMP DEFAULT (NOW()),
	"comment" TEXT,
	"type" VARCHAR(20) NOT NULL,
	"device_id" INTEGER NOT NULL,
	"user_origin_id" INTEGER,
	"user_destination_id" INTEGER,
	"created_by" INTEGER NOT NULL
);
CREATE INDEX ON "contract" ("end_date");
CREATE INDEX ON "movement" ("date");
ALTER TABLE "brand"
ADD FOREIGN KEY ("country_origin_id") REFERENCES "country_origin" ("id");
ALTER TABLE "contract"
ADD FOREIGN KEY ("user_id") REFERENCES "user" ("id");
ALTER TABLE "contract"
ADD FOREIGN KEY ("provider_id") REFERENCES "provider" ("id");
ALTER TABLE "contract"
ADD FOREIGN KEY ("order_id") REFERENCES "purchase_order" ("id");
ALTER TABLE "purchase_order"
ADD FOREIGN KEY ("user_id") REFERENCES "user" ("id");
ALTER TABLE "purchase_order"
ADD FOREIGN KEY ("provider_id") REFERENCES "provider" ("id");
ALTER TABLE "purchase_order_device"
ADD FOREIGN KEY ("order_id") REFERENCES "purchase_order" ("id");
ALTER TABLE "purchase_order_device"
ADD FOREIGN KEY ("device_id") REFERENCES "device" ("id");
ALTER TABLE "user"
ADD FOREIGN KEY ("role_id") REFERENCES "role" ("id");
ALTER TABLE "user"
ADD FOREIGN KEY ("department_id") REFERENCES "department" ("id");
ALTER TABLE "device"
ADD FOREIGN KEY ("brand_id") REFERENCES "brand" ("id");
ALTER TABLE "contracts_device"
ADD FOREIGN KEY ("contract_id") REFERENCES "contract" ("id");
ALTER TABLE "contracts_device"
ADD FOREIGN KEY ("device_id") REFERENCES "device" ("id");
ALTER TABLE "movement"
ADD FOREIGN KEY ("device_id") REFERENCES "device" ("id");
ALTER TABLE "movement"
ADD FOREIGN KEY ("user_origin_id") REFERENCES "user" ("id");
ALTER TABLE "movement"
ADD FOREIGN KEY ("user_destination_id") REFERENCES "user" ("id");
ALTER TABLE "movement"
ADD FOREIGN KEY ("created_by") REFERENCES "user" ("id");
--Insersiones
INSERT INTO role (id, name, description)
VALUES (
		1,
		'ADMINISTRADOR',
		'Gestiona el sistema, usuarios y configuraciones generales'
	),
	(
		2,
		'INVENTARISTA',
		'Registra, actualiza y controla el inventario de activos'
	),
	(
		3,
		'AUDITOR',
		'Supervisa y valida la información del inventario y operaciones'
	);
INSERT INTO country_origin (id, name)
VALUES (1, 'China'),
	(2, 'Estados Unidos'),
	(3, 'Japón'),
	(4, 'Corea del Sur'),
	(5, 'Taiwán'),
	(6, 'Alemania'),
	(7, 'India'),
	(8, 'Vietnam');
INSERT INTO brand (id, name, country_origin_id)
VALUES (1, 'Lenovo', 1),
	(2, 'Huawei', 1),
	(3, 'Apple', 2),
	(4, 'Dell', 2),
	(5, 'HP', 2),
	(6, 'Microsoft', 2),
	(7, 'Sony', 3),
	(8, 'Toshiba', 3),
	(9, 'Panasonic', 3),
	(10, 'Samsung', 4),
	(11, 'LG', 4),
	(12, 'Asus', 5),
	(13, 'Acer', 5),
	(14, 'MSI', 5),
	(15, 'Medion', 6);
INSERT INTO department (id, name)
VALUES (1, 'Tecnología de la Información'),
	(2, 'Redes y Comunicaciones'),
	(3, 'Soporte Técnico'),
	(4, 'Mantenimiento de Hardware'),
	(5, 'Seguridad Informática'),
	(6, 'Gestión de Inventarios'),
	(7, 'Compras y Proveedores'),
	(8, 'Logística y Almacén'),
	(9, 'Desarrollo de Software'),
	(10, 'Auditoría de Sistemas');