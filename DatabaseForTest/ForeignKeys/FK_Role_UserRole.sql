ALTER TABLE dbo.UserRole
ADD CONSTRAINT FK_Role_UserRole FOREIGN KEY (RoleId)     
    REFERENCES dbo.Role (Id)
