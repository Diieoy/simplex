ALTER TABLE dbo.UserRole
ADD CONSTRAINT FK_User_UserRole FOREIGN KEY (UserId)     
    REFERENCES dbo.[User] (Id)
