CREATE UNIQUE INDEX departamentas
ON departamentas (id);

CREATE UNIQUE INDEX paskaita
ON paskaita (id);

CREATE UNIQUE INDEX studentas
ON studentas (id);

CREATE INDEX studentas_departamentas_id
ON studentas (departamentas_id);

CREATE INDEX departamento_paskaita
ON departamento_paskaita (departamentas_id, paskaita_id);