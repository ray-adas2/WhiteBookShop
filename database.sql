CREATE TABLE BookTB1 (
    BId     INT IDENTITY(1,1) PRIMARY KEY,   -- 图书编号，自增主键
    BTitle  NVARCHAR(50) NOT NULL,            -- 书名
    BAuthor NVARCHAR(50) NOT NULL,            -- 作者
    BCat    NVARCHAR(50) NOT NULL,            -- 分类
    BQty    INT NOT NULL,                     -- 数量
    BPrice  MONEY NOT NULL                   -- 价格
);
GO

CREATE TABLE UserTB1 (
    UId       INT IDENTITY(20000, 1) PRIMARY KEY, -- 从 20000 开始，自增 1
    UName     NVARCHAR(20) NOT NULL,               -- 用户名
    UPhone    VARCHAR(11) NOT NULL,                -- 手机号
    UAddress  NVARCHAR(50) NOT NULL,               -- 地址
    UPassword VARCHAR(50) NOT NULL                 -- 密码
);
GO

CREATE TABLE BillTB1 (
    BillId  INT IDENTITY(30000, 1) PRIMARY KEY,
    UName   NVARCHAR(50) NOT NULL,
    Amount  MONEY NOT NULL
);
GO

INSERT INTO UserTB1 (UName, UPhone, UAddress, UPassword)
VALUES
(N'张三', '13800000001', N'北京市海淀区', '123456'),
(N'李四', '13800000002', N'上海市浦东新区', '123456'),
(N'王五', '13800000003', N'广州市天河区', '123456');
GO

INSERT INTO BookTB1 (BTitle, BAuthor, BCat, BQty, BPrice)
VALUES
(N'C# 从入门到精通', N'明日科技', N'编程', 50, 89.00),
(N'数据库系统原理', N'王珊', N'计算机', 30, 65.50),
(N'计算机网络', N'谢希仁', N'计算机', 40, 79.00),
(N'.NET WinForm 开发实战', N'张晓明', N'编程', 20, 99.00);
GO

INSERT INTO BillTB1 (UName, Amount)
VALUES
(N'张三', 178.00),
(N'李四', 89.00),
(N'王五', 99.00);
GO
