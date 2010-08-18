<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Northwind._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
            <Columns>
                <asp:BoundField DataField="Category_CategoryName" HeaderText="Category_CategoryName"
                    SortExpression="Category_CategoryName" />
                <asp:BoundField DataField="Category_Description" HeaderText="Category_Description"
                    SortExpression="Category_Description" />
                <asp:BoundField DataField="Supplier_CompanyName" HeaderText="Supplier_CompanyName"
                    SortExpression="Supplier_CompanyName" />
                <asp:BoundField DataField="Supplier_ContactName" HeaderText="Supplier_ContactName"
                    SortExpression="Supplier_ContactName" />
                <asp:BoundField DataField="Supplier_ContactTitle" HeaderText="Supplier_ContactTitle"
                    SortExpression="Supplier_ContactTitle" />
                <asp:BoundField DataField="Supplier_Address" HeaderText="Supplier_Address" SortExpression="Supplier_Address" />
                <asp:BoundField DataField="Supplier_City" HeaderText="Supplier_City" SortExpression="Supplier_City" />
                <asp:BoundField DataField="Supplier_Region" HeaderText="Supplier_Region" SortExpression="Supplier_Region" />
                <asp:BoundField DataField="Supplier_PostalCode" HeaderText="Supplier_PostalCode"
                    SortExpression="Supplier_PostalCode" />
                <asp:BoundField DataField="Supplier_Country" HeaderText="Supplier_Country" SortExpression="Supplier_Country" />
                <asp:BoundField DataField="Supplier_Phone" HeaderText="Supplier_Phone" SortExpression="Supplier_Phone" />
                <asp:BoundField DataField="Supplier_Fax" HeaderText="Supplier_Fax" SortExpression="Supplier_Fax" />
                <asp:BoundField DataField="Supplier_HomePage" HeaderText="Supplier_HomePage" SortExpression="Supplier_HomePage" />
                <asp:BoundField DataField="ProductID" HeaderText="ProductID" SortExpression="ProductID" />
                <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                <asp:BoundField DataField="SupplierID" HeaderText="SupplierID" SortExpression="SupplierID" />
                <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" SortExpression="CategoryID" />
                <asp:BoundField DataField="QuantityPerUnit" HeaderText="QuantityPerUnit" SortExpression="QuantityPerUnit" />
                <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" />
                <asp:BoundField DataField="UnitsInStock" HeaderText="UnitsInStock" SortExpression="UnitsInStock" />
                <asp:BoundField DataField="UnitsOnOrder" HeaderText="UnitsOnOrder" SortExpression="UnitsOnOrder" />
                <asp:BoundField DataField="ReorderLevel" HeaderText="ReorderLevel" SortExpression="ReorderLevel" />
                <asp:CheckBoxField DataField="Discontinued" HeaderText="Discontinued" SortExpression="Discontinued" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
            SelectCountMethod="Count" SelectMethod="Select" 
            SortParameterName="orderBy" TypeName="Northwind.Web.ProductsViewSource">
            <SelectParameters>
                <asp:Parameter Name="condition" Type="Object" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
