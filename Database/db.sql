SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

CREATE SCHEMA IF NOT EXISTS `thermostatmonitor` DEFAULT CHARACTER SET utf8 ;
USE `thermostatmonitor` ;

-- -----------------------------------------------------
-- Table `thermostatmonitor`.`users`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `thermostatmonitor`.`users` (
  `id` INT(10) NOT NULL AUTO_INCREMENT ,
  `email_address` VARCHAR(255) NULL DEFAULT NULL ,
  `password` VARCHAR(255) NULL DEFAULT NULL ,
  `auth_code` VARCHAR(255) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB
AUTO_INCREMENT = 567
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `thermostatmonitor`.`locations`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `thermostatmonitor`.`locations` (
  `id` INT(10) NOT NULL AUTO_INCREMENT ,
  `user_id` INT(10) NULL DEFAULT NULL ,
  `name` VARCHAR(50) NULL DEFAULT NULL ,
  `api_key` VARCHAR(100) NULL DEFAULT NULL ,
  `zip_code` VARCHAR(50) NULL DEFAULT NULL ,
  `electricity_price` FLOAT(53,0) NULL DEFAULT NULL ,
  `share_data` BIT(1) NULL DEFAULT NULL ,
  `timezone` INT(10) NULL DEFAULT NULL ,
  `daylight_savings` TINYINT(1) NULL DEFAULT NULL ,
  `heat_fuel_price` FLOAT(53,0) NULL DEFAULT NULL ,
  `open_weather_city_id` INT(10) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) ,
  UNIQUE INDEX `ApiKey` (`api_key` ASC) ,
  INDEX `fk_locations_users_idx` (`user_id` ASC) ,
  CONSTRAINT `fk_locations_users`
    FOREIGN KEY (`user_id` )
    REFERENCES `thermostatmonitor`.`users` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 566
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `thermostatmonitor`.`thermostats`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `thermostatmonitor`.`thermostats` (
  `id` INT(10) NOT NULL AUTO_INCREMENT ,
  `ip_address` VARCHAR(50) NULL DEFAULT NULL ,
  `display_name` VARCHAR(50) NULL DEFAULT NULL ,
  `ac_tons` FLOAT(53,0) NULL DEFAULT NULL ,
  `ac_seer` INT(10) NULL DEFAULT NULL ,
  `ac_kilowatts` FLOAT(53,0) NULL DEFAULT NULL ,
  `fan_kilowatts` FLOAT(53,0) NULL DEFAULT NULL ,
  `brand` VARCHAR(50) NULL DEFAULT NULL ,
  `location_id` INT(10) NULL DEFAULT NULL ,
  `keat_btu_per_hour` FLOAT(53,0) NULL DEFAULT NULL ,
  `key_name` VARCHAR(50) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_thermostats_locations_idx` (`location_id` ASC) ,
  CONSTRAINT `fk_thermostats_locations`
    FOREIGN KEY (`location_id` )
    REFERENCES `thermostatmonitor`.`locations` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 569
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `thermostatmonitor`.`cycles`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `thermostatmonitor`.`cycles` (
  `id` INT(10) NOT NULL AUTO_INCREMENT ,
  `thermostat_id` INT(10) NULL DEFAULT NULL ,
  `cycle_type` VARCHAR(50) NULL DEFAULT NULL ,
  `start_date` DATETIME NULL DEFAULT NULL ,
  `end_date` DATETIME NULL DEFAULT NULL ,
  `start_precision` SMALLINT(5) NULL DEFAULT NULL ,
  `end_precision` SMALLINT(5) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `IX_Cycles` (`thermostat_id` ASC, `start_date` ASC) ,
  INDEX `fk_cycles_thermostats_idx` (`thermostat_id` ASC) ,
  CONSTRAINT `fk_cycles_thermostats`
    FOREIGN KEY (`thermostat_id` )
    REFERENCES `thermostatmonitor`.`thermostats` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 275105
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `thermostatmonitor`.`errors`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `thermostatmonitor`.`errors` (
  `id` INT(10) NOT NULL AUTO_INCREMENT ,
  `user_id` INT(10) NULL DEFAULT NULL ,
  `log_date` DATETIME NULL DEFAULT NULL ,
  `error_message` TEXT NULL DEFAULT NULL ,
  `url` VARCHAR(255) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_errors_users_idx` (`user_id` ASC) ,
  CONSTRAINT `fk_errors_users`
    FOREIGN KEY (`user_id` )
    REFERENCES `thermostatmonitor`.`users` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 13
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `thermostatmonitor`.`outside_conditions`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `thermostatmonitor`.`outside_conditions` (
  `id` INT(10) NOT NULL AUTO_INCREMENT ,
  `degrees` INT(10) NULL DEFAULT NULL ,
  `log_date` DATETIME NULL DEFAULT NULL ,
  `location_id` INT(10) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `IX_OutsideConditions` (`location_id` ASC, `log_date` ASC) ,
  INDEX `fk_outside_conditions_locations_idx` (`location_id` ASC) ,
  CONSTRAINT `fk_outside_conditions_locations`
    FOREIGN KEY (`location_id` )
    REFERENCES `thermostatmonitor`.`locations` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 427738
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `thermostatmonitor`.`snapshots`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `thermostatmonitor`.`snapshots` (
  `id` INT(10) NOT NULL AUTO_INCREMENT ,
  `thermostat_id` INT(10) NULL DEFAULT NULL ,
  `start_time` DATETIME NULL DEFAULT NULL ,
  `seconds` INT(10) NULL DEFAULT NULL ,
  `mode` VARCHAR(50) NULL DEFAULT NULL ,
  `inside_temp_high` INT(10) NULL DEFAULT NULL ,
  `inside_temp_low` INT(10) NULL DEFAULT NULL ,
  `inside_temp_average` INT(10) NULL DEFAULT NULL ,
  `outside_temp_high` INT(10) NULL DEFAULT NULL ,
  `outside_temp_low` INT(10) NULL DEFAULT NULL ,
  `outside_temp_average` INT(10) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `IX_Snapshots` (`thermostat_id` ASC, `start_time` ASC) ,
  INDEX `fk_snapshots_thermostats_idx` (`thermostat_id` ASC) ,
  CONSTRAINT `fk_snapshots_thermostats`
    FOREIGN KEY (`thermostat_id` )
    REFERENCES `thermostatmonitor`.`thermostats` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 512164
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `thermostatmonitor`.`temperatures`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `thermostatmonitor`.`temperatures` (
  `id` INT(10) NOT NULL AUTO_INCREMENT ,
  `thermostat_id` INT(10) NULL DEFAULT NULL ,
  `log_date` DATETIME NULL DEFAULT NULL ,
  `degrees` FLOAT(53,0) NULL DEFAULT NULL ,
  `log_precision` SMALLINT(5) NULL DEFAULT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `IX_Temperatures` (`thermostat_id` ASC, `log_date` ASC) ,
  INDEX `fk_temperatures_thermostats_idx` (`thermostat_id` ASC) ,
  CONSTRAINT `fk_temperatures_thermostats`
    FOREIGN KEY (`thermostat_id` )
    REFERENCES `thermostatmonitor`.`thermostats` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1790331
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- procedure cycles_delete
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `cycles_delete`(id int)
BEGIN
	DELETE FROM cycles WHERE cycles.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure cycles_load
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `cycles_load`(id int)
BEGIN
	SELECT * FROM cycles WHERE cycles.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure cycles_load_all
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `cycles_load_all`()
BEGIN
	SELECT * FROM cycles;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure cycles_load_by_thermostat_id
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `cycles_load_by_thermostat_id`(thermostat_id int)
BEGIN
	SELECT * FROM cycles WHERE cycles.thermostat_id = thermostat_id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure cycles_save
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `cycles_save`(id int, thermostat_id int, cycle_type varchar(50), start_date datetime, end_date datetime, start_precision smallint, end_precision smallint)
BEGIN
	IF EXISTS(SELECT id FROM cycles WHERE cycles.id = id) THEN
		UPDATE cycles SET
			cycles.thermostat_id = thermostat_id, 
			cycles.cycle_type = cycle_type, 
			cycles.start_date = start_date, 
			cycles.end_date = end_date, 
			cycles.start_precision = start_precision, 
			cycles.end_precision = end_precision
			WHERE cycles.id = id;
			SELECT id;
	ELSE
		INSERT INTO cycles
			(thermostat_id, cycle_type, start_date, end_date, start_precision, end_precision)
		VALUES
			(thermostat_id, cycle_type, start_date, end_date, start_precision, end_precision);
		SELECT @@Identity;
	END IF;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure errors_delete
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `errors_delete`(id int)
BEGIN
	DELETE FROM errors WHERE errors.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure errors_load
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `errors_load`(id int)
BEGIN
	SELECT * FROM errors WHERE errors.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure errors_load_all
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `errors_load_all`()
BEGIN
	SELECT * FROM errors;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure errors_load_by_user_id
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `errors_load_by_user_id`(user_id int)
BEGIN
	SELECT * FROM errors WHERE errors.user_id = user_id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure errors_save
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `errors_save`(id int, user_id int, log_date datetime, error_message text, url varchar(255))
BEGIN
	IF EXISTS(SELECT id FROM errors WHERE errors.id = id) THEN
		UPDATE errors SET
			errors.user_id = user_id, 
			errors.log_date = log_date, 
			errors.error_message = error_message, 
			errors.url = url
			WHERE errors.id = id;
			SELECT id;
	ELSE
		INSERT INTO errors
			(user_id, log_date, error_message, url)
		VALUES
			(user_id, log_date, error_message, url);
		SELECT @@Identity;
	END IF;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure locations_delete
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `locations_delete`(id int)
BEGIN
	DELETE FROM locations WHERE locations.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure locations_load
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `locations_load`(id int)
BEGIN
	SELECT * FROM locations WHERE locations.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure locations_load_all
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `locations_load_all`()
BEGIN
	SELECT * FROM locations;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure locations_load_by_user_id
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `locations_load_by_user_id`(user_id int)
BEGIN
	SELECT * FROM locations WHERE locations.user_id = user_id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure locations_save
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `locations_save`(id int, user_id int, name varchar(50), api_key varchar(100), zip_code varchar(50), electricity_price float, share_data bit, timezone int, daylight_savings tinyint, heat_fuel_price float, open_weather_city_id int)
BEGIN
	IF EXISTS(SELECT id FROM locations WHERE locations.id = id) THEN
		UPDATE locations SET
			locations.user_id = user_id, 
			locations.name = name, 
			locations.api_key = api_key, 
			locations.zip_code = zip_code, 
			locations.electricity_price = electricity_price, 
			locations.share_data = share_data, 
			locations.timezone = timezone, 
			locations.daylight_savings = daylight_savings, 
			locations.heat_fuel_price = heat_fuel_price, 
			locations.open_weather_city_id = open_weather_city_id
			WHERE locations.id = id;
			SELECT id;
	ELSE
		INSERT INTO locations
			(user_id, name, api_key, zip_code, electricity_price, share_data, timezone, daylight_savings, heat_fuel_price, open_weather_city_id)
		VALUES
			(user_id, name, api_key, zip_code, electricity_price, share_data, timezone, daylight_savings, heat_fuel_price, open_weather_city_id);
		SELECT @@Identity;
	END IF;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure outside_conditions_delete
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `outside_conditions_delete`(id int)
BEGIN
	DELETE FROM outside_conditions WHERE outside_conditions.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure outside_conditions_load
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `outside_conditions_load`(id int)
BEGIN
	SELECT * FROM outside_conditions WHERE outside_conditions.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure outside_conditions_load_all
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `outside_conditions_load_all`()
BEGIN
	SELECT * FROM outside_conditions;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure outside_conditions_load_by_location_id
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `outside_conditions_load_by_location_id`(location_id int)
BEGIN
	SELECT * FROM outside_conditions WHERE outside_conditions.location_id = location_id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure outside_conditions_save
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `outside_conditions_save`(id int, degrees int, log_date datetime, location_id int)
BEGIN
	IF EXISTS(SELECT id FROM outside_conditions WHERE outside_conditions.id = id) THEN
		UPDATE outside_conditions SET
			outside_conditions.degrees = degrees, 
			outside_conditions.log_date = log_date, 
			outside_conditions.location_id = location_id
			WHERE outside_conditions.id = id;
			SELECT id;
	ELSE
		INSERT INTO outside_conditions
			(degrees, log_date, location_id)
		VALUES
			(degrees, log_date, location_id);
		SELECT @@Identity;
	END IF;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure snapshots_delete
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `snapshots_delete`(id int)
BEGIN
	DELETE FROM snapshots WHERE snapshots.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure snapshots_load
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `snapshots_load`(id int)
BEGIN
	SELECT * FROM snapshots WHERE snapshots.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure snapshots_load_all
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `snapshots_load_all`()
BEGIN
	SELECT * FROM snapshots;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure snapshots_load_by_thermostat_id
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `snapshots_load_by_thermostat_id`(thermostat_id int)
BEGIN
	SELECT * FROM snapshots WHERE snapshots.thermostat_id = thermostat_id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure snapshots_save
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `snapshots_save`(id int, thermostat_id int, start_time datetime, seconds int, mode varchar(50), inside_temp_high int, inside_temp_low int, inside_temp_average int, outside_temp_high int, outside_temp_low int, outside_temp_average int)
BEGIN
	IF EXISTS(SELECT id FROM snapshots WHERE snapshots.id = id) THEN
		UPDATE snapshots SET
			snapshots.thermostat_id = thermostat_id, 
			snapshots.start_time = start_time, 
			snapshots.seconds = seconds, 
			snapshots.mode = mode, 
			snapshots.inside_temp_high = inside_temp_high, 
			snapshots.inside_temp_low = inside_temp_low, 
			snapshots.inside_temp_average = inside_temp_average, 
			snapshots.outside_temp_high = outside_temp_high, 
			snapshots.outside_temp_low = outside_temp_low, 
			snapshots.outside_temp_average = outside_temp_average
			WHERE snapshots.id = id;
			SELECT id;
	ELSE
		INSERT INTO snapshots
			(thermostat_id, start_time, seconds, mode, inside_temp_high, inside_temp_low, inside_temp_average, outside_temp_high, outside_temp_low, outside_temp_average)
		VALUES
			(thermostat_id, start_time, seconds, mode, inside_temp_high, inside_temp_low, inside_temp_average, outside_temp_high, outside_temp_low, outside_temp_average);
		SELECT @@Identity;
	END IF;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure temperatures_delete
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `temperatures_delete`(id int)
BEGIN
	DELETE FROM temperatures WHERE temperatures.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure temperatures_load
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `temperatures_load`(id int)
BEGIN
	SELECT * FROM temperatures WHERE temperatures.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure temperatures_load_all
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `temperatures_load_all`()
BEGIN
	SELECT * FROM temperatures;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure temperatures_load_by_thermostat_id
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `temperatures_load_by_thermostat_id`(thermostat_id int)
BEGIN
	SELECT * FROM temperatures WHERE temperatures.thermostat_id = thermostat_id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure temperatures_save
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `temperatures_save`(id int, thermostat_id int, log_date datetime, degrees float, log_precision smallint)
BEGIN
	IF EXISTS(SELECT id FROM temperatures WHERE temperatures.id = id) THEN
		UPDATE temperatures SET
			temperatures.thermostat_id = thermostat_id, 
			temperatures.log_date = log_date, 
			temperatures.degrees = degrees, 
			temperatures.log_precision = log_precision
			WHERE temperatures.id = id;
			SELECT id;
	ELSE
		INSERT INTO temperatures
			(thermostat_id, log_date, degrees, log_precision)
		VALUES
			(thermostat_id, log_date, degrees, log_precision);
		SELECT @@Identity;
	END IF;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure thermostats_delete
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `thermostats_delete`(id int)
BEGIN
	DELETE FROM thermostats WHERE thermostats.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure thermostats_load
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `thermostats_load`(id int)
BEGIN
	SELECT * FROM thermostats WHERE thermostats.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure thermostats_load_all
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `thermostats_load_all`()
BEGIN
	SELECT * FROM thermostats;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure thermostats_load_by_location_id
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `thermostats_load_by_location_id`(location_id int)
BEGIN
	SELECT * FROM thermostats WHERE thermostats.location_id = location_id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure thermostats_save
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `thermostats_save`(id int, ip_address varchar(50), display_name varchar(50), ac_tons float, ac_seer int, ac_kilowatts float, fan_kilowatts float, brand varchar(50), location_id int, keat_btu_per_hour float, key_name varchar(50))
BEGIN
	IF EXISTS(SELECT id FROM thermostats WHERE thermostats.id = id) THEN
		UPDATE thermostats SET
			thermostats.ip_address = ip_address, 
			thermostats.display_name = display_name, 
			thermostats.ac_tons = ac_tons, 
			thermostats.ac_seer = ac_seer, 
			thermostats.ac_kilowatts = ac_kilowatts, 
			thermostats.fan_kilowatts = fan_kilowatts, 
			thermostats.brand = brand, 
			thermostats.location_id = location_id, 
			thermostats.keat_btu_per_hour = keat_btu_per_hour, 
			thermostats.key_name = key_name
			WHERE thermostats.id = id;
			SELECT id;
	ELSE
		INSERT INTO thermostats
			(ip_address, display_name, ac_tons, ac_seer, ac_kilowatts, fan_kilowatts, brand, location_id, keat_btu_per_hour, key_name)
		VALUES
			(ip_address, display_name, ac_tons, ac_seer, ac_kilowatts, fan_kilowatts, brand, location_id, keat_btu_per_hour, key_name);
		SELECT @@Identity;
	END IF;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure users_delete
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `users_delete`(id int)
BEGIN
	DELETE FROM users WHERE users.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure users_load
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `users_load`(id int)
BEGIN
	SELECT * FROM users WHERE users.id = id;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure users_load_all
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `users_load_all`()
BEGIN
	SELECT * FROM users;
END$$

DELIMITER ;
-- -----------------------------------------------------
-- procedure users_save
-- -----------------------------------------------------

DELIMITER $$
USE `thermostatmonitor`$$
CREATE DEFINER=`TMUser`@`%` PROCEDURE `users_save`(id int, email_address varchar(255), password varchar(255), auth_code varchar(255))
BEGIN
	IF EXISTS(SELECT id FROM users WHERE users.id = id) THEN
		UPDATE users SET
			users.email_address = email_address, 
			users.password = password, 
			users.auth_code = auth_code
			WHERE users.id = id;
			SELECT id;
	ELSE
		INSERT INTO users
			(email_address, password, auth_code)
		VALUES
			(email_address, password, auth_code);
		SELECT @@Identity;
	END IF;
END$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
