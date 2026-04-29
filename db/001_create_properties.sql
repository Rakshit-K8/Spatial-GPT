CREATE TABLE properties (
    id          SERIAL PRIMARY KEY,
    title       TEXT NOT NULL,
    type        TEXT NOT NULL,
    price       BIGINT NOT NULL,
    city        TEXT NOT NULL,
    area        TEXT,
    furnished   BOOLEAN DEFAULT false,
    geom        GEOMETRY(Point, 4326)
);

CREATE INDEX idx_properties_geom  ON properties USING GIST (geom);
CREATE INDEX idx_properties_type  ON properties (type);
CREATE INDEX idx_properties_price ON properties (price);