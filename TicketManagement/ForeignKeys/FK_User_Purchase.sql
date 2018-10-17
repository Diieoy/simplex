ALTER TABLE dbo.Purchase
ADD CONSTRAINT FK_User_Purchase FOREIGN KEY (UserId)     
    REFERENCES dbo.[User] (Id)