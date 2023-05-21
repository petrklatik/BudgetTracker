CREATE TABLE Transactions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    CategoryId INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    DateCreated DATETIME NOT NULL,
    Description NVARCHAR(100) NULL,
    IsIncome BIT NOT NULL, -- 1 for income, 0 for expense
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);
