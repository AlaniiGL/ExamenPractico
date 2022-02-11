CREATE TABLE client (
  Id_Client BIGINT(20) NOT NULL AUTO_INCREMENT,
  ClientName VARCHAR(255) NOT NULL,
  ClientLastName1 VARCHAR(255) NOT NULL,
  ClientLastName2 VARCHAR(255) NOT NULL,
  ClientCreateDate DATETIME NOT NULL,
  ClientUpdateDate DATETIME NOT NULL,
  PRIMARY KEY (Id_Client)
  );

CREATE TABLE account (
  Id_Account BIGINT(20) NOT NULL AUTO_INCREMENT,
  Id_Client BIGINT(20) NOT NULL,
  Account_Balance BIGINT(20) NOT NULL,
  AccountCreateDate DATETIME NOT NULL,
  AccountUpdateDate DATETIME NOT NULL,
  PRIMARY KEY (Id_Account)
);

CREATE TABLE transactions (
  Id_Transaction BIGINT(20) NOT NULL AUTO_INCREMENT,
  Id_Account BIGINT(20) NOT NULL,
  Id_TransactionType BIGINT(20) NOT NULL,
  Transaction_Quantity BIGINT(20) NOT NULL ,
  Previous_Balance BIGINT(20) NOT NULL,
  Subsecuent_Balance BIGINT(20) NOT NULL,
  Transaction_Date DATETIME NOT NULL,
  PRIMARY KEY (Id_Transaction)
);

CREATE TABLE transactiontype (
  Id_TransactionType BIGINT (20) NOT NULL AUTO_INCREMENT,
  TransactionName VARCHAR(255) NOT NULL,
  PRIMARY KEY (Id_TransactionType)
);

ALTER TABLE account ADD CONSTRAINT Client_FK FOREIGN KEY (Id_Client)
    REFERENCES client (Id_Client);

ALTER TABLE transactions ADD CONSTRAINT Account_FK FOREIGN KEY (Id_Account)
    REFERENCES account (Id_Account);

ALTER TABLE transactions ADD CONSTRAINT TransactionType_FK FOREIGN KEY (Id_TransactionType)
    REFERENCES transactiontype (Id_TransactionType);