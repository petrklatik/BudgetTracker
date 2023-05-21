CREATE TABLE UserTransactions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    TransactionId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (TransactionId) REFERENCES Transactions(Id)
);
