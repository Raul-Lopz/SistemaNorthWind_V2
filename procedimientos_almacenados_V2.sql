
/*Procedimiento para obtener los países sin repetir de la tabla Customers*/
CREATE PROC proc_paises_costumers
AS
begin 
	SELECT distinct Country
	from dbo.Customers
	order by Country asc
END

/*procedimiento para obtener los clientes en base a un país dado*/
CREATE PROC proc_clientes_por_pais
	@pais nvarchar(15)
AS
BEGIN
	SELECT CustomerID,CompanyName,ContactName
	FROM dbo.Customers
	WHERE Country=@pais
END

/*procedimiento para obtener las ordenes en base al id del cliente*/
CREATE PROC proc_ordenes_por_clienteid
	@ID nchar(5)
AS
BEGIN

	SELECT top(10) o.OrderID,c.CompanyName,o.OrderDate,o.RequiredDate,o.ShipCity,o.Freight
	FROM dbo.Orders as o
	LEFT JOIN dbo.Customers as c ON (o.CustomerID=c.CustomerID)
	WHERE o.CustomerID=@ID
	ORDER BY o.OrderDate desc
END

/*procedimiento para obtener la sumatoria del campo Freight de la tabla Orders*/
CREATE PROC proc_suma_transporte
	@ID nchar(5)
AS
BEGIN
	SELECT sum(Freight) as total_transporte
	FROM dbo.Orders 
	WHERE CustomerID=@ID
END

/*procedimiento para registrar una nueva orden*/
CREATE PROC proc_nueva_orden(
	@CustomerID nchar(5),
	@ShipCity nvarchar(15),
	@Freight money)
AS
BEGIN
	INSERT INTO dbo.Orders (CustomerID,OrderDate,RequiredDate,ShipCity,Freight) VALUES(@CustomerID,GETDATE(),GETDATE(),@ShipCity,@Freight)
END

/*procedimiento para obtener un nuevo ID de orden que será registrado*/
CREATE PROC proc_obtener_ultima_orden
AS
BEGIN
	SELECT TOP(1)OrderID + 1 AS Nuevo_OrderID
	FROM dbo.Orders
	ORDER by OrderID DESC
END

/*procedimiento para actualizar el campo Freight de una orden*/
CREATE PROC proc_actualizar_freight
	@OrderID int,
	@Freight money
AS
BEGIN
	    UPDATE Orders
		SET Freight=@Freight
		WHERE OrderID=@OrderID
END

/*procedimiento para eliminar una orden*/
CREATE PROC proc_eliminar_orden
	@OrderID int
AS
BEGIN
	DELETE FROM Orders WHERE OrderID=@OrderID
END


/*procedimiento para obtener una orden por id*/
CREATE PROC proc_obtener_orden_id(
	@OrderID int)
AS
BEGIN
	SELECT OrderID,CustomerID,OrderDate,RequiredDate,Freight,ShipCity
	FROM dbo.Orders
	WHERE OrderID=@OrderID
END