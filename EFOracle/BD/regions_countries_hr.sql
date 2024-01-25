DROP USER HR CASCADE;
CREATE USER HR IDENTIFIED BY HR;
GRANT CONNECT,RESOURCE,dba TO HR;
GRANT UNLIMITED TABLESPACE TO HR;
ALTER PROFILE DEFAULT LIMIT PASSWORD_REUSE_TIME UNLIMITED;
ALTER PROFILE DEFAULT LIMIT PASSWORD_LIFE_TIME UNLIMITED;

CONNECT HR/HR@LOCALHOST:1521/XEPDB1

-- REGIONS table holds region information for locations
-- HR.LOCATIONS table has a foreign key to this table.
CREATE TABLE regions
    ( region_id      NUMBER
                     CONSTRAINT region_id_nn NOT NULL
    ,                CONSTRAINT reg_id_pk
                        PRIMARY KEY (region_id)
    , region_name    VARCHAR2(25)
    );

-- COUNTRIES table holds country information for customers and company locations.
-- OE.CUSTOMERS table and HR.LOCATIONS have a foreign key to this table.
CREATE TABLE countries
    ( country_id      CHAR(2)
                      CONSTRAINT country_id_nn NOT NULL
    ,                 CONSTRAINT country_c_id_pk
        	         PRIMARY KEY (country_id)
    , country_name    VARCHAR2(40)
    , region_id       NUMBER
    ,                 CONSTRAINT countr_reg_fk
        	         FOREIGN KEY (region_id)
          	         REFERENCES regions (region_id)
    )
    ORGANIZATION INDEX;

-- insert data into the REGIONS table
BEGIN
INSERT INTO regions VALUES (1, 'Europe'					);
INSERT INTO regions VALUES (2, 'Americas'				);
INSERT INTO regions VALUES (3, 'Asia'					);
INSERT INTO regions VALUES (4, 'Middle East and Africa'	);  
END;
/

-- insert data into the COUNTRIES table
BEGIN  
INSERT INTO countries VALUES ('IT', 'Italy'						, 1);  
INSERT INTO countries VALUES ('JP', 'Japan'						, 3);  
INSERT INTO countries VALUES ('US', 'United States of America'	, 2);  
INSERT INTO countries VALUES ('CA', 'Canada'					, 2);  
INSERT INTO countries VALUES ('CN', 'China'						, 3);  
INSERT INTO countries VALUES ('IN', 'India'						, 3);  
INSERT INTO countries VALUES ('AU', 'Australia'					, 3);  
INSERT INTO countries VALUES ('ZW', 'Zimbabwe'					, 4);  
INSERT INTO countries VALUES ('SG', 'Singapore'					, 3);
INSERT INTO countries VALUES ('UK', 'United Kingdom'			, 1);
INSERT INTO countries VALUES ('FR', 'France'					, 1);
INSERT INTO countries VALUES ('DE', 'Germany'					, 1);
INSERT INTO countries VALUES ('ZM', 'Zambia'					, 4);  
INSERT INTO countries VALUES ('EG', 'Egypt'						, 4);  
INSERT INTO countries VALUES ('BR', 'Brazil'					, 2);  
INSERT INTO countries VALUES ('CH', 'Switzerland'				, 1);  
INSERT INTO countries VALUES ('NL', 'Netherlands'				, 1);  
INSERT INTO countries VALUES ('MX', 'Mexico'					, 2);  
INSERT INTO countries VALUES ('KW', 'Kuwait'					, 4);  
INSERT INTO countries VALUES ('IL', 'Israel'					, 4);  
INSERT INTO countries VALUES ('DK', 'Denmark'					, 1);  
INSERT INTO countries VALUES ('ML', 'Malaysia'					, 3);  
INSERT INTO countries VALUES ('NG', 'Nigeria'					, 4);  
INSERT INTO countries VALUES ('AR', 'Argentina'					, 2);  
INSERT INTO countries VALUES ('BE', 'Belgium'					, 1);
END;
/