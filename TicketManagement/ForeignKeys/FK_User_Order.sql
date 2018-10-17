ALTER TABLE dbo.[Order]
ADD CONSTRAINT FK_User_Order FOREIGN KEY (UserId)     
    REFERENCES dbo.[User] (Id)
