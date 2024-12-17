DELIMITER $$
CREATE TRIGGER check_utakmica_before_insert
BEFORE INSERT ON UTAKMICA
FOR EACH ROW
BEGIN
    DECLARE count_referee INT;

    SELECT COUNT(*)
    INTO count_referee
    FROM UTAKMICA AS u
    WHERE u.KOLO_brojKola = NEW.KOLO_brojKola
    AND u.KOLO_SEZONA_godina = NEW.KOLO_SEZONA_godina
    AND u.KOLO_SEZONA_LIGA_naziv = NEW.KOLO_SEZONA_LIGA_naziv
    AND u.sudija = NEW.sudija;

    IF count_referee > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Referee cannot officiate more than one match on the same matchday.';
    END IF;
END$$

DELIMITER ;

DELIMITER $$
CREATE TRIGGER update_statistika_after_insert
AFTER INSERT ON TIM_STATISTIKA_UTAKMICA
FOR EACH ROW
BEGIN
    DECLARE season_year CHAR(9);
    DECLARE season_league VARCHAR(45);
    
    SELECT KOLO_SEZONA_godina, KOLO_SEZONA_LIGA_naziv INTO season_year, season_league
    FROM UTAKMICA WHERE idUtakmica = NEW.UTAKMICA_idUtakmica;
    
    IF EXISTS (SELECT * FROM STATISTIKA WHERE TIM_naziv = NEW.TIM_naziv AND SEZONA_godina = season_year AND SEZONA_Liga_naziv = season_league) THEN
    
    UPDATE STATISTIKA
    SET brojPostignutihGolova = brojPostignutihGolova + NEW.brojPostignutihGolova,
        brojPrimljenihGolova = brojPrimljenihGolova + NEW.brojPrimljenihGolova,
        golRazlika = golRazlika + (NEW.brojPostignutihGolova - NEW.brojPrimljenihGolova),
        brojOdigranihUtakmica = brojOdigranihUtakmica + 1,
        brojPobjeda = CASE
            WHEN NEW.brojPostignutihGolova > NEW.brojPrimljenihGolova THEN brojPobjeda + 1
            ELSE brojPobjeda
        END,
        brojPoraza = CASE
            WHEN NEW.brojPostignutihGolova < NEW.brojPrimljenihGolova THEN brojPoraza + 1
            ELSE brojPoraza
        END,
        brojNerjesenih = CASE
            WHEN NEW.brojPostignutihGolova = NEW.brojPrimljenihGolova THEN brojNerjesenih + 1
            ELSE brojNerjesenih
        END,
        brojBodova = CASE
            WHEN NEW.brojPostignutihGolova < NEW.brojPrimljenihGolova THEN brojBodova
            WHEN NEW.brojPostignutihGolova = NEW.brojPrimljenihGolova THEN brojBodova + 1
            ELSE brojBodova + 3
        END
    WHERE TIM_naziv = NEW.TIM_naziv AND SEZONA_godina = season_year AND SEZONA_Liga_naziv = season_league;
ELSE
    INSERT INTO STATISTIKA (TIM_naziv, SEZONA_godina, SEZONA_Liga_naziv, brojOdigranihUtakmica, brojPostignutihGolova, brojPrimljenihGolova, golRazlika, brojBodova, brojPobjeda, brojPoraza, brojNerjesenih)
    VALUES (NEW.TIM_naziv, season_year, season_league, 1, NEW.brojPostignutihGolova, NEW.brojPrimljenihGolova, NEW.brojPostignutihGolova - NEW.brojPrimljenihGolova,
            CASE
                WHEN NEW.brojPostignutihGolova < NEW.brojPrimljenihGolova THEN 0
                WHEN NEW.brojPostignutihGolova = NEW.brojPrimljenihGolova THEN 1
                ELSE 3
            END,
            CASE
                WHEN NEW.brojPostignutihGolova > NEW.brojPrimljenihGolova THEN 1
                ELSE 0
            END,
            CASE
                WHEN NEW.brojPostignutihGolova < NEW.brojPrimljenihGolova THEN 1
                ELSE 0
            END,
            CASE
                WHEN NEW.brojPostignutihGolova = NEW.brojPrimljenihGolova THEN 1
                ELSE 0
            END);
END IF;
END $$
DELIMITER ;

DELIMITER $$
DROP PROCEDURE IF EXISTS InsertTIM_STATISTIKA_UTAKMICA$$
CREATE PROCEDURE InsertTIM_STATISTIKA_UTAKMICA (
    IN p_TIM_naziv VARCHAR(255),
    IN p_UTAKMICA_idUtakmica INT,
    IN p_brojPostignutihGolova INT,
    IN p_brojPrimljenihGolova INT,
    IN p_jeDomacin TINYINT(1)
)
BEGIN
    DECLARE count_duplicate INT;
    DECLARE kolo_broj INT;
    DECLARE kolo_sezona CHAR(9);
    DECLARE kolo_liga VARCHAR(255);

    SELECT u.KOLO_brojKola, u.KOLO_SEZONA_godina, u.KOLO_SEZONA_LIGA_naziv
    INTO kolo_broj, kolo_sezona, kolo_liga
    FROM UTAKMICA AS u
    WHERE u.idUtakmica = p_UTAKMICA_idUtakmica;

    SELECT COUNT(*)
    INTO count_duplicate
    FROM TIM_STATISTIKA_UTAKMICA AS t
    INNER JOIN UTAKMICA AS u ON t.UTAKMICA_idUtakmica = u.idUtakmica
    WHERE t.TIM_naziv = p_TIM_naziv
    AND u.KOLO_brojKola = kolo_broj
    AND u.KOLO_SEZONA_godina = kolo_sezona
    AND u.KOLO_SEZONA_LIGA_naziv = kolo_liga;

    IF count_duplicate > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'One team cant play more than once in one matchday.';
    END IF;

    INSERT INTO tim_statistika_utakmica (brojPostignutihGolova, brojPrimljenihGolova, jeDomacin,TIM_naziv, UTAKMICA_idUtakmica)
    VALUES (p_brojPostignutihGolova, p_brojPrimljenihGolova, p_jeDomacin, p_TIM_naziv, p_UTAKMICA_idUtakmica);
END$$

DELIMITER ;