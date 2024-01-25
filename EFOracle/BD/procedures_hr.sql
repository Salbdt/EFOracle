-- COUNTRIES_GET_ALL
CREATE OR REPLACE PROCEDURE COUNTRIES_GET_ALL
IS
p_countries SYS_REFCURSOR
BEGIN
    OPEN p_countries FOR
        SELECT c.country_id, c.country_name, c.region_id
        FROM countries c;
		
	DBMS_SQL.RETURN_RESULT(p_countries);
END;
/

-- COUNTRIES_GET_ALL_PARAM
CREATE OR REPLACE PROCEDURE COUNTRIES_GET_ALL_PARAM (
    p_countries OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN p_countries FOR
        SELECT c.country_id, c.country_name, c.region_id
        FROM countries c;
END;
/

-- COUNTRIES_CREATE
CREATE OR REPLACE PROCEDURE COUNTRIES_CREATE (
    p_country_id    IN countries.country_id%TYPE,
    p_country_name  IN countries.country_name%TYPE,
    p_region_id     IN countries.region_id%TYPE,
    p_country       OUT SYS_REFCURSOR
)
AS
v_country_id        countries.country_id%TYPE;
BEGIN
    INSERT INTO countries VALUES(p_country_id, p_country_name, p_region_id)
    RETURNING country_id INTO v_country_id;

    OPEN p_country FOR
        SELECT c.country_id, c.country_name, c.region_id
        FROM countries c
        WHERE c.country_id = v_country_id;
        
    COMMIT;
END;
/

-- COUNTRIES_UPDATE
CREATE OR REPLACE PROCEDURE COUNTRIES_UPDATE (
    p_country_id    IN countries.country_id%TYPE,
    p_country_name  IN countries.country_name%TYPE,
    p_region_id     IN countries.region_id%TYPE,
    p_affected_rows OUT NUMBER
)
AS
v_country_id        countries.country_id%TYPE;
BEGIN
    UPDATE countries SET
        country_name    = p_country_name,
        region_id       = p_region_id
    WHERE country_id    = p_country_id;

    p_affected_rows := SQL%ROWCOUNT;
        
    COMMIT;
END;
/