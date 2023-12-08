CREATE TABLE departamentas (
	id uuid PRIMARY KEY,
	pavadinimas VARCHAR(255) NOT NULL,
	is_deleted boolean NOT NULL DEFAULT FALSE,
	created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	modified_at TIMESTAMP
);

CREATE TABLE paskaita (
	id uuid PRIMARY KEY,
	pavadinimas VARCHAR(255) NOT NULL,
	is_deleted boolean NOT NULL DEFAULT FALSE,
	created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	modified_at TIMESTAMP
);

CREATE TABLE studentas (
	id uuid PRIMARY KEY,
	vardas VARCHAR(255) NOT NULL,
	is_deleted boolean NOT NULL DEFAULT FALSE,
	departamentas_id uuid,
	created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	modified_at TIMESTAMP,
	FOREIGN KEY (departamentas_id) REFERENCES departamentas(id)
);

CREATE TABLE departamento_paskaita (
	id serial PRIMARY KEY,
	departamentas_id uuid,
	paskaita_id uuid,
	FOREIGN KEY (departamentas_id) REFERENCES departamentas(id),
	FOREIGN KEY (paskaita_id) REFERENCES paskaita(id)
);