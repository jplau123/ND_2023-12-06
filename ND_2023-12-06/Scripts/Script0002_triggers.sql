CREATE OR REPLACE FUNCTION update_departamentas_modified()
RETURNS TRIGGER AS $$
BEGIN
    NEW.modified_at = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER departamentas_modified
BEFORE UPDATE ON departamentas
FOR EACH ROW
EXECUTE FUNCTION update_departamentas_modified();

CREATE OR REPLACE FUNCTION update_paskaita_modified()
RETURNS TRIGGER AS $$
BEGIN
    NEW.modified_at = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER paskaita_modified
BEFORE UPDATE ON paskaita
FOR EACH ROW
EXECUTE FUNCTION update_paskaita_modified();

CREATE OR REPLACE FUNCTION update_studentas_modified()
RETURNS TRIGGER AS $$
BEGIN
    NEW.modified_at = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER studentas_modified
BEFORE UPDATE ON studentas
FOR EACH ROW
EXECUTE FUNCTION update_studentas_modified();
