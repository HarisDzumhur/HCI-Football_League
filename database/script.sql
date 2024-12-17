-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema FootballLeague
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `FootballLeague` ;

-- -----------------------------------------------------
-- Schema FootballLeague
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `FootballLeague` DEFAULT CHARACTER SET utf8 ;
USE `FootballLeague` ;

-- -----------------------------------------------------
-- Table `FootballLeague`.`LIGA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`LIGA` (
  `naziv` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`naziv`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`TIM`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`TIM` (
  `naziv` VARCHAR(30) NOT NULL,
  `brojTrofeja` INT NOT NULL,
  `datumOsnivanja` DATE NOT NULL,
  `grad` VARCHAR(30) NOT NULL,
  `trener` VARCHAR(50) NOT NULL,
  `stadion` VARCHAR(50) NOT NULL,
  `logo` MEDIUMBLOB NOT NULL,
  `jeObrisan` TINYINT(1) NOT NULL,
  PRIMARY KEY (`naziv`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`IGRAC`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`IGRAC` (
  `brojDresa` INT NOT NULL,
  `brojZutihKartona` INT NOT NULL,
  `brojCrvenihKartona` INT NOT NULL,
  `odigraneMinute` INT NOT NULL,
  `brojGolova` INT NOT NULL,
  `brojAsistencija` INT NOT NULL,
  `idIgrac` INT NOT NULL AUTO_INCREMENT,
  `ime` VARCHAR(20) NOT NULL,
  `prezime` VARCHAR(40) NOT NULL,
  `brojGodina` INT NOT NULL,
  `TIM_naziv` VARCHAR(30) NOT NULL,
  `jeObrisan` TINYINT(1) NOT NULL,
  PRIMARY KEY (`idIgrac`),
  INDEX `fk_IGRAC_TIM1_idx` (`TIM_naziv` ASC) VISIBLE,
  CONSTRAINT `fk_IGRAC_TIM1`
    FOREIGN KEY (`TIM_naziv`)
    REFERENCES `FootballLeague`.`TIM` (`naziv`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`SEZONA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`SEZONA` (
  `godina` CHAR(9) NOT NULL,
  `LIGA_naziv` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`godina`, `LIGA_naziv`),
  INDEX `fk_SEZONA_LIGA1_idx` (`LIGA_naziv` ASC) VISIBLE,
  CONSTRAINT `fk_SEZONA_LIGA1`
    FOREIGN KEY (`LIGA_naziv`)
    REFERENCES `FootballLeague`.`LIGA` (`naziv`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`KOLO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`KOLO` (
  `brojKola` INT NOT NULL,
  `SEZONA_godina` CHAR(9) NOT NULL,
  `SEZONA_LIGA_naziv` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`brojKola`, `SEZONA_godina`, `SEZONA_LIGA_naziv`),
  INDEX `fk_KOLO_SEZONA1_idx` (`SEZONA_godina` ASC, `SEZONA_LIGA_naziv` ASC) VISIBLE,
  CONSTRAINT `fk_KOLO_SEZONA1`
    FOREIGN KEY (`SEZONA_godina` , `SEZONA_LIGA_naziv`)
    REFERENCES `FootballLeague`.`SEZONA` (`godina` , `LIGA_naziv`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`UTAKMICA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`UTAKMICA` (
  `idUtakmica` INT NOT NULL AUTO_INCREMENT,
  `datumUtakmice` DATE NOT NULL,
  `KOLO_brojKola` INT NOT NULL,
  `KOLO_SEZONA_godina` CHAR(9) NOT NULL,
  `KOLO_SEZONA_LIGA_naziv` VARCHAR(45) NOT NULL,
  `sudija` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idUtakmica`),
  INDEX `fk_UTAKMICA_KOLO1_idx` (`KOLO_brojKola` ASC, `KOLO_SEZONA_godina` ASC, `KOLO_SEZONA_LIGA_naziv` ASC) VISIBLE,
  CONSTRAINT `fk_UTAKMICA_KOLO1`
    FOREIGN KEY (`KOLO_brojKola` , `KOLO_SEZONA_godina` , `KOLO_SEZONA_LIGA_naziv`)
    REFERENCES `FootballLeague`.`KOLO` (`brojKola` , `SEZONA_godina` , `SEZONA_LIGA_naziv`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 0;


-- -----------------------------------------------------
-- Table `FootballLeague`.`STATISTIKA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`STATISTIKA` (
  `brojPobjeda` INT NOT NULL,
  `brojPoraza` INT NOT NULL,
  `brojNerjesenih` INT NOT NULL,
  `brojOdigranihUtakmica` INT NOT NULL,
  `brojPostignutihGolova` INT NOT NULL,
  `brojPrimljenihGolova` INT NOT NULL,
  `golRazlika` INT NOT NULL,
  `brojBodova` INT NOT NULL,
  `TIM_naziv` VARCHAR(30) NOT NULL,
  `SEZONA_godina` CHAR(9) NOT NULL,
  `SEZONA_Liga_naziv` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`TIM_naziv`, `SEZONA_godina`, `SEZONA_Liga_naziv`),
  INDEX `fk_STATISTIKA_TIM1_idx` (`TIM_naziv` ASC) VISIBLE,
  INDEX `fk_STATISTIKA_SEZONA1_idx` (`SEZONA_godina` ASC, `SEZONA_Liga_naziv` ASC) VISIBLE,
  CONSTRAINT `fk_STATISTIKA_TIM1`
    FOREIGN KEY (`TIM_naziv`)
    REFERENCES `FootballLeague`.`TIM` (`naziv`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_STATISTIKA_SEZONA1`
    FOREIGN KEY (`SEZONA_godina`)
    REFERENCES `FootballLeague`.`SEZONA` (`godina`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`TIM_STATISTIKA_UTAKMICA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`TIM_STATISTIKA_UTAKMICA` (
  `brojPostignutihGolova` INT NOT NULL,
  `brojPrimljenihGolova` INT NOT NULL,
  `jeDomacin` TINYINT(1) NOT NULL,
  `TIM_naziv` VARCHAR(30) NOT NULL,
  `UTAKMICA_idUtakmica` INT NOT NULL,
  PRIMARY KEY (`TIM_naziv`, `UTAKMICA_idUtakmica`),
  INDEX `fk_TIM_STATISTIKA_UTAKMICA_TIM1_idx` (`TIM_naziv` ASC) VISIBLE,
  INDEX `fk_TIM_STATISTIKA_UTAKMICA_UTAKMICA1_idx` (`UTAKMICA_idUtakmica` ASC) VISIBLE,
  CONSTRAINT `fk_TIM_STATISTIKA_UTAKMICA_TIM1`
    FOREIGN KEY (`TIM_naziv`)
    REFERENCES `FootballLeague`.`TIM` (`naziv`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TIM_STATISTIKA_UTAKMICA_UTAKMICA1`
    FOREIGN KEY (`UTAKMICA_idUtakmica`)
    REFERENCES `FootballLeague`.`UTAKMICA` (`idUtakmica`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`IGRAC_STATISTIKA_UTAKMICA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`IGRAC_STATISTIKA_UTAKMICA` (
  `brojGolovaNaUtakmici` INT NOT NULL,
  `brojAsistencijaNaUtakmici` INT NOT NULL,
  `starter` TINYINT(1) NOT NULL,
  `zutiKarton` TINYINT(1) NOT NULL,
  `crveniKarton` TINYINT(1) NOT NULL,
  `odigraneMinuteNaUtakmici` INT NOT NULL,
  `TIM_STATISTIKA_UTAKMICA_TIM_naziv` VARCHAR(30) NOT NULL,
  `TIM_STATISTIKA_UTAKMICA_UTAKMICA_idUtakmica` INT NOT NULL,
  `IGRAC_idIgrac` INT NOT NULL,
  PRIMARY KEY (`TIM_STATISTIKA_UTAKMICA_TIM_naziv`, `TIM_STATISTIKA_UTAKMICA_UTAKMICA_idUtakmica`, `IGRAC_idIgrac`),
  INDEX `fk_IGRAC_STATISTIKA UTAKMICA_TIM_STATISTIKA_UTAKMICA1_idx` (`TIM_STATISTIKA_UTAKMICA_TIM_naziv` ASC, `TIM_STATISTIKA_UTAKMICA_UTAKMICA_idUtakmica` ASC) VISIBLE,
  INDEX `fk_IGRAC_STATISTIKA_UTAKMICA_IGRAC1_idx` (`IGRAC_idIgrac` ASC) VISIBLE,
  CONSTRAINT `fk_IGRAC_STATISTIKA UTAKMICA_TIM_STATISTIKA_UTAKMICA1`
    FOREIGN KEY (`TIM_STATISTIKA_UTAKMICA_TIM_naziv` , `TIM_STATISTIKA_UTAKMICA_UTAKMICA_idUtakmica`)
    REFERENCES `FootballLeague`.`TIM_STATISTIKA_UTAKMICA` (`TIM_naziv` , `UTAKMICA_idUtakmica`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_IGRAC_STATISTIKA_UTAKMICA_IGRAC1`
    FOREIGN KEY (`IGRAC_idIgrac`)
    REFERENCES `FootballLeague`.`IGRAC` (`idIgrac`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`POSTAVKE`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`POSTAVKE` (
  `tema` ENUM('Red', 'Blue', 'Green', 'Purple') NOT NULL DEFAULT 'Purple',
  `jezik` ENUM('ENG', 'BIH') NOT NULL DEFAULT 'ENG',
  `idPostavke` INT NOT NULL AUTO_INCREMENT,
  `font` ENUM('Comic Sans MS', 'Times New Roman', 'Calibri') NOT NULL DEFAULT 'Calibri',
  PRIMARY KEY (`idPostavke`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`KORISNIK`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`KORISNIK` (
  `idKorisnik` INT NOT NULL AUTO_INCREMENT,
  `korisnickoIme` VARCHAR(20) NOT NULL,
  `lozinka` VARCHAR(255) NOT NULL,
  `POSTAVKE_idPostavke` INT NOT NULL,
  `jeBlokiran` TINYINT(1) NOT NULL,
  `ime` VARCHAR(15) NOT NULL,
  `prezime` VARCHAR(30) NOT NULL,
  `jeAdministrator` TINYINT(1) NOT NULL,
  PRIMARY KEY (`idKorisnik`),
  UNIQUE INDEX `korisnickoIme_UNIQUE` (`korisnickoIme` ASC) VISIBLE,
  INDEX `fk_KORISNIK_POSTAVKE1_idx` (`POSTAVKE_idPostavke` ASC) VISIBLE,
  CONSTRAINT `fk_KORISNIK_POSTAVKE1`
    FOREIGN KEY (`POSTAVKE_idPostavke`)
    REFERENCES `FootballLeague`.`POSTAVKE` (`idPostavke`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `FootballLeague`.`KORISNIK_TIM`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FootballLeague`.`KORISNIK_TIM` (
  `KORISNIK_idKorisnik` INT NOT NULL,
  `TIM_naziv` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`KORISNIK_idKorisnik`, `TIM_naziv`),
  INDEX `fk_KORISNIK_has_TIM_TIM1_idx` (`TIM_naziv` ASC) VISIBLE,
  INDEX `fk_KORISNIK_has_TIM_KORISNIK1_idx` (`KORISNIK_idKorisnik` ASC) VISIBLE,
  CONSTRAINT `fk_KORISNIK_has_TIM_KORISNIK1`
    FOREIGN KEY (`KORISNIK_idKorisnik`)
    REFERENCES `FootballLeague`.`KORISNIK` (`idKorisnik`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_KORISNIK_has_TIM_TIM1`
    FOREIGN KEY (`TIM_naziv`)
    REFERENCES `FootballLeague`.`TIM` (`naziv`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
