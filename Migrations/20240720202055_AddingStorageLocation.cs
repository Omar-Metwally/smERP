using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smERP.Migrations
{
    /// <inheritdoc />
    public partial class AddingStorageLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeID",
                table: "AttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseTransaction_Branches_BranchID",
                table: "BaseTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseTransaction_Employees_EmployeeID",
                table: "BaseTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Company_CompanyID",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_BranchManagerID",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyTransactions_BaseTransaction_BaseTransactionID",
                table: "BuyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyTransactions_BaseTransaction_ID",
                table: "BuyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyTransactions_Suppliers_SupplierID",
                table: "BuyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_DepartmentHeadID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branches_BranchID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_BaseTransaction_ID",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_BaseTransaction_ReferencingTransactionID",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Customers_CustomerID",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Suppliers_SupplierID",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_AttributeValues_AttributeValueID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductSKUs_ProductSKUID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Products_ProductID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_BaseTransaction_ID",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_Branches_FromBranchID",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_Branches_ToBranchID",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_Employees_FromEmployeeID",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_Employees_ToEmployeeID",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSKUImage_ProductSKUs_ProductSKUID",
                table: "ProductSKUImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSKUs_Products_ProductID",
                table: "ProductSKUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Products_ProductID",
                table: "ProductSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierID",
                table: "ProductSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_SellTransactions_BaseTransaction_BaseTransactionID",
                table: "SellTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SellTransactions_BaseTransaction_ID",
                table: "SellTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SellTransactions_Customers_CustomerID",
                table: "SellTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionChanges_BaseTransaction_BaseTransactionID",
                table: "TransactionChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_BaseTransaction_BaseTransactionID",
                table: "TransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseTransaction",
                table: "BaseTransaction");

            migrationBuilder.RenameTable(
                name: "BaseTransaction",
                newName: "BaseTransactions");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "TransactionItems",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "ProductSKUID",
                table: "TransactionItems",
                newName: "ProductSkuId");

            migrationBuilder.RenameColumn(
                name: "BaseTransactionID",
                table: "TransactionItems",
                newName: "BaseTransactionId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TransactionItems",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionItems_BaseTransactionID",
                table: "TransactionItems",
                newName: "IX_TransactionItems_BaseTransactionId");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "TransactionChanges",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "BaseTransactionID",
                table: "TransactionChanges",
                newName: "BaseTransactionId");

            migrationBuilder.RenameColumn(
                name: "TransactionChangeID",
                table: "TransactionChanges",
                newName: "TransactionChangeId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionChanges_BaseTransactionID",
                table: "TransactionChanges",
                newName: "IX_TransactionChanges_BaseTransactionId");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "SellTransactions",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "BaseTransactionID",
                table: "SellTransactions",
                newName: "BaseTransactionId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "SellTransactions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_SellTransactions_CustomerID",
                table: "SellTransactions",
                newName: "IX_SellTransactions_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_SellTransactions_BaseTransactionID",
                table: "SellTransactions",
                newName: "IX_SellTransactions_BaseTransactionId");

            migrationBuilder.RenameColumn(
                name: "SupplierID",
                table: "ProductSuppliers",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductSuppliers",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSuppliers_SupplierID",
                table: "ProductSuppliers",
                newName: "IX_ProductSuppliers_SupplierId");

            migrationBuilder.RenameColumn(
                name: "ProductVariantID",
                table: "ProductSKUs",
                newName: "ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductSKUs",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProductSKUs",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSKUs_ProductID",
                table: "ProductSKUs",
                newName: "IX_ProductSKUs_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductSKUID",
                table: "ProductSKUImage",
                newName: "ProductSkuId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProductSKUImage",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSKUImage_ProductSKUID",
                table: "ProductSKUImage",
                newName: "IX_ProductSKUImage_ProductSkuId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "ToEmployeeID",
                table: "ProductMoveTransactions",
                newName: "ToEmployeeId");

            migrationBuilder.RenameColumn(
                name: "FromEmployeeID",
                table: "ProductMoveTransactions",
                newName: "FromEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProductMoveTransactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ToBranchID",
                table: "ProductMoveTransactions",
                newName: "ToStorageLocationId");

            migrationBuilder.RenameColumn(
                name: "FromBranchID",
                table: "ProductMoveTransactions",
                newName: "FromStorageLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMoveTransactions_ToEmployeeID",
                table: "ProductMoveTransactions",
                newName: "IX_ProductMoveTransactions_ToEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMoveTransactions_FromEmployeeID",
                table: "ProductMoveTransactions",
                newName: "IX_ProductMoveTransactions_FromEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMoveTransactions_ToBranchID",
                table: "ProductMoveTransactions",
                newName: "IX_ProductMoveTransactions_ToStorageLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMoveTransactions_FromBranchID",
                table: "ProductMoveTransactions",
                newName: "IX_ProductMoveTransactions_FromStorageLocationId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductAttributes",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "AttributeValueID",
                table: "ProductAttributes",
                newName: "AttributeValueId");

            migrationBuilder.RenameColumn(
                name: "ProductSKUID",
                table: "ProductAttributes",
                newName: "ProductSkuId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_ProductID",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_AttributeValueID",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_AttributeValueId");

            migrationBuilder.RenameColumn(
                name: "SupplierID",
                table: "PaymentTransactions",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "ReferencingTransactionID",
                table: "PaymentTransactions",
                newName: "ReferencingTransactionId");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "PaymentTransactions",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PaymentTransactions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_SupplierID",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_ReferencingTransactionID",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_ReferencingTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_CustomerID",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_CustomerId");

            migrationBuilder.RenameColumn(
                name: "ManagerID",
                table: "Employees",
                newName: "ManagerId");

            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "Employees",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "BranchID",
                table: "Employees",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                newName: "IX_Employees_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BranchID",
                table: "Employees",
                newName: "IX_Employees_BranchId");

            migrationBuilder.RenameColumn(
                name: "DepartmentHeadID",
                table: "Departments",
                newName: "DepartmentHeadId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Departments",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_DepartmentHeadID",
                table: "Departments",
                newName: "IX_Departments_DepartmentHeadId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Company",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryID",
                table: "Categories",
                newName: "ParentCategoryId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.RenameColumn(
                name: "SupplierID",
                table: "BuyTransactions",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "BaseTransactionID",
                table: "BuyTransactions",
                newName: "BaseTransactionId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BuyTransactions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_BuyTransactions_SupplierID",
                table: "BuyTransactions",
                newName: "IX_BuyTransactions_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_BuyTransactions_BaseTransactionID",
                table: "BuyTransactions",
                newName: "IX_BuyTransactions_BaseTransactionId");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Branches",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "BranchManagerID",
                table: "Branches",
                newName: "BranchManagerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Branches",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_CompanyID",
                table: "Branches",
                newName: "IX_Branches_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_BranchManagerID",
                table: "Branches",
                newName: "IX_Branches_BranchManagerId");

            migrationBuilder.RenameColumn(
                name: "AttributeValueID",
                table: "AttributeValues",
                newName: "AttributeValueId");

            migrationBuilder.RenameColumn(
                name: "AttributeID",
                table: "AttributeValues",
                newName: "AttributeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AttributeValues",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeValues_AttributeID",
                table: "AttributeValues",
                newName: "IX_AttributeValues_AttributeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Attributes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "BaseTransactions",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "BranchID",
                table: "BaseTransactions",
                newName: "BranchId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BaseTransactions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_BaseTransaction_EmployeeID",
                table: "BaseTransactions",
                newName: "IX_BaseTransactions_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseTransaction_BranchID",
                table: "BaseTransactions",
                newName: "IX_BaseTransactions_BranchId");

            migrationBuilder.AddColumn<int>(
                name: "StorageLocationId",
                table: "SellTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LogoImage",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CoverImage",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "StorageLocationId",
                table: "BuyTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseTransactions",
                table: "BaseTransactions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StorageLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageLocation_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellTransactions_StorageLocationId",
                table: "SellTransactions",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyTransactions_StorageLocationId",
                table: "BuyTransactions",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageLocation_BranchId",
                table: "StorageLocation",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeId",
                table: "AttributeValues",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseTransactions_Branches_BranchId",
                table: "BaseTransactions",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseTransactions_Employees_EmployeeId",
                table: "BaseTransactions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Company_CompanyId",
                table: "Branches",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_BranchManagerId",
                table: "Branches",
                column: "BranchManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyTransactions_BaseTransactions_BaseTransactionId",
                table: "BuyTransactions",
                column: "BaseTransactionId",
                principalTable: "BaseTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyTransactions_BaseTransactions_Id",
                table: "BuyTransactions",
                column: "Id",
                principalTable: "BaseTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyTransactions_StorageLocation_StorageLocationId",
                table: "BuyTransactions",
                column: "StorageLocationId",
                principalTable: "StorageLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyTransactions_Suppliers_SupplierId",
                table: "BuyTransactions",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_DepartmentHeadId",
                table: "Departments",
                column: "DepartmentHeadId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_BaseTransactions_Id",
                table: "PaymentTransactions",
                column: "Id",
                principalTable: "BaseTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_BaseTransactions_ReferencingTransactionId",
                table: "PaymentTransactions",
                column: "ReferencingTransactionId",
                principalTable: "BaseTransactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Customers_CustomerId",
                table: "PaymentTransactions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Suppliers_SupplierId",
                table: "PaymentTransactions",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_AttributeValues_AttributeValueId",
                table: "ProductAttributes",
                column: "AttributeValueId",
                principalTable: "AttributeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductSKUs_ProductSkuId",
                table: "ProductAttributes",
                column: "ProductSkuId",
                principalTable: "ProductSKUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Products_ProductId",
                table: "ProductAttributes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_BaseTransactions_Id",
                table: "ProductMoveTransactions",
                column: "Id",
                principalTable: "BaseTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_Employees_FromEmployeeId",
                table: "ProductMoveTransactions",
                column: "FromEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_Employees_ToEmployeeId",
                table: "ProductMoveTransactions",
                column: "ToEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_StorageLocation_FromStorageLocationId",
                table: "ProductMoveTransactions",
                column: "FromStorageLocationId",
                principalTable: "StorageLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_StorageLocation_ToStorageLocationId",
                table: "ProductMoveTransactions",
                column: "ToStorageLocationId",
                principalTable: "StorageLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSKUImage_ProductSKUs_ProductSkuId",
                table: "ProductSKUImage",
                column: "ProductSkuId",
                principalTable: "ProductSKUs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSKUs_Products_ProductId",
                table: "ProductSKUs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Products_ProductId",
                table: "ProductSuppliers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierId",
                table: "ProductSuppliers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellTransactions_BaseTransactions_BaseTransactionId",
                table: "SellTransactions",
                column: "BaseTransactionId",
                principalTable: "BaseTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellTransactions_BaseTransactions_Id",
                table: "SellTransactions",
                column: "Id",
                principalTable: "BaseTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SellTransactions_Customers_CustomerId",
                table: "SellTransactions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SellTransactions_StorageLocation_StorageLocationId",
                table: "SellTransactions",
                column: "StorageLocationId",
                principalTable: "StorageLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionChanges_BaseTransactions_BaseTransactionId",
                table: "TransactionChanges",
                column: "BaseTransactionId",
                principalTable: "BaseTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_BaseTransactions_BaseTransactionId",
                table: "TransactionItems",
                column: "BaseTransactionId",
                principalTable: "BaseTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeId",
                table: "AttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseTransactions_Branches_BranchId",
                table: "BaseTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseTransactions_Employees_EmployeeId",
                table: "BaseTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Company_CompanyId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Employees_BranchManagerId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyTransactions_BaseTransactions_BaseTransactionId",
                table: "BuyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyTransactions_BaseTransactions_Id",
                table: "BuyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyTransactions_StorageLocation_StorageLocationId",
                table: "BuyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyTransactions_Suppliers_SupplierId",
                table: "BuyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_DepartmentHeadId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_BaseTransactions_Id",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_BaseTransactions_ReferencingTransactionId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Customers_CustomerId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Suppliers_SupplierId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_AttributeValues_AttributeValueId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductSKUs_ProductSkuId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Products_ProductId",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_BaseTransactions_Id",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_Employees_FromEmployeeId",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_Employees_ToEmployeeId",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_StorageLocation_FromStorageLocationId",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMoveTransactions_StorageLocation_ToStorageLocationId",
                table: "ProductMoveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSKUImage_ProductSKUs_ProductSkuId",
                table: "ProductSKUImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSKUs_Products_ProductId",
                table: "ProductSKUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Products_ProductId",
                table: "ProductSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierId",
                table: "ProductSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_SellTransactions_BaseTransactions_BaseTransactionId",
                table: "SellTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SellTransactions_BaseTransactions_Id",
                table: "SellTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SellTransactions_Customers_CustomerId",
                table: "SellTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SellTransactions_StorageLocation_StorageLocationId",
                table: "SellTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionChanges_BaseTransactions_BaseTransactionId",
                table: "TransactionChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_BaseTransactions_BaseTransactionId",
                table: "TransactionItems");

            migrationBuilder.DropTable(
                name: "StorageLocation");

            migrationBuilder.DropIndex(
                name: "IX_SellTransactions_StorageLocationId",
                table: "SellTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BuyTransactions_StorageLocationId",
                table: "BuyTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseTransactions",
                table: "BaseTransactions");

            migrationBuilder.DropColumn(
                name: "StorageLocationId",
                table: "SellTransactions");

            migrationBuilder.DropColumn(
                name: "StorageLocationId",
                table: "BuyTransactions");

            migrationBuilder.RenameTable(
                name: "BaseTransactions",
                newName: "BaseTransaction");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "TransactionItems",
                newName: "TransactionID");

            migrationBuilder.RenameColumn(
                name: "ProductSkuId",
                table: "TransactionItems",
                newName: "ProductSKUID");

            migrationBuilder.RenameColumn(
                name: "BaseTransactionId",
                table: "TransactionItems",
                newName: "BaseTransactionID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TransactionItems",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionItems_BaseTransactionId",
                table: "TransactionItems",
                newName: "IX_TransactionItems_BaseTransactionID");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "TransactionChanges",
                newName: "TransactionID");

            migrationBuilder.RenameColumn(
                name: "BaseTransactionId",
                table: "TransactionChanges",
                newName: "BaseTransactionID");

            migrationBuilder.RenameColumn(
                name: "TransactionChangeId",
                table: "TransactionChanges",
                newName: "TransactionChangeID");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionChanges_BaseTransactionId",
                table: "TransactionChanges",
                newName: "IX_TransactionChanges_BaseTransactionID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "SellTransactions",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "BaseTransactionId",
                table: "SellTransactions",
                newName: "BaseTransactionID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SellTransactions",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_SellTransactions_CustomerId",
                table: "SellTransactions",
                newName: "IX_SellTransactions_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_SellTransactions_BaseTransactionId",
                table: "SellTransactions",
                newName: "IX_SellTransactions_BaseTransactionID");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "ProductSuppliers",
                newName: "SupplierID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductSuppliers",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSuppliers_SupplierId",
                table: "ProductSuppliers",
                newName: "IX_ProductSuppliers_SupplierID");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "ProductSKUs",
                newName: "ProductVariantID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductSKUs",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductSKUs",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSKUs_ProductId",
                table: "ProductSKUs",
                newName: "IX_ProductSKUs_ProductID");

            migrationBuilder.RenameColumn(
                name: "ProductSkuId",
                table: "ProductSKUImage",
                newName: "ProductSKUID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductSKUImage",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSKUImage_ProductSkuId",
                table: "ProductSKUImage",
                newName: "IX_ProductSKUImage_ProductSKUID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryID");

            migrationBuilder.RenameColumn(
                name: "ToEmployeeId",
                table: "ProductMoveTransactions",
                newName: "ToEmployeeID");

            migrationBuilder.RenameColumn(
                name: "FromEmployeeId",
                table: "ProductMoveTransactions",
                newName: "FromEmployeeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductMoveTransactions",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ToStorageLocationId",
                table: "ProductMoveTransactions",
                newName: "ToBranchID");

            migrationBuilder.RenameColumn(
                name: "FromStorageLocationId",
                table: "ProductMoveTransactions",
                newName: "FromBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMoveTransactions_ToEmployeeId",
                table: "ProductMoveTransactions",
                newName: "IX_ProductMoveTransactions_ToEmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMoveTransactions_FromEmployeeId",
                table: "ProductMoveTransactions",
                newName: "IX_ProductMoveTransactions_FromEmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMoveTransactions_ToStorageLocationId",
                table: "ProductMoveTransactions",
                newName: "IX_ProductMoveTransactions_ToBranchID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMoveTransactions_FromStorageLocationId",
                table: "ProductMoveTransactions",
                newName: "IX_ProductMoveTransactions_FromBranchID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductAttributes",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "AttributeValueId",
                table: "ProductAttributes",
                newName: "AttributeValueID");

            migrationBuilder.RenameColumn(
                name: "ProductSkuId",
                table: "ProductAttributes",
                newName: "ProductSKUID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_AttributeValueId",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_AttributeValueID");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "PaymentTransactions",
                newName: "SupplierID");

            migrationBuilder.RenameColumn(
                name: "ReferencingTransactionId",
                table: "PaymentTransactions",
                newName: "ReferencingTransactionID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "PaymentTransactions",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentTransactions",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_SupplierId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_ReferencingTransactionId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_ReferencingTransactionID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_CustomerId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_CustomerID");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Employees",
                newName: "ManagerID");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Employees",
                newName: "DepartmentID");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Employees",
                newName: "BranchID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                newName: "IX_Employees_DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                newName: "IX_Employees_BranchID");

            migrationBuilder.RenameColumn(
                name: "DepartmentHeadId",
                table: "Departments",
                newName: "DepartmentHeadID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Departments",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_DepartmentHeadId",
                table: "Departments",
                newName: "IX_Departments_DepartmentHeadID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Company",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                table: "Categories",
                newName: "ParentCategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryID");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "BuyTransactions",
                newName: "SupplierID");

            migrationBuilder.RenameColumn(
                name: "BaseTransactionId",
                table: "BuyTransactions",
                newName: "BaseTransactionID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BuyTransactions",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_BuyTransactions_SupplierId",
                table: "BuyTransactions",
                newName: "IX_BuyTransactions_SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_BuyTransactions_BaseTransactionId",
                table: "BuyTransactions",
                newName: "IX_BuyTransactions_BaseTransactionID");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Branches",
                newName: "CompanyID");

            migrationBuilder.RenameColumn(
                name: "BranchManagerId",
                table: "Branches",
                newName: "BranchManagerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Branches",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_CompanyId",
                table: "Branches",
                newName: "IX_Branches_CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_BranchManagerId",
                table: "Branches",
                newName: "IX_Branches_BranchManagerID");

            migrationBuilder.RenameColumn(
                name: "AttributeValueId",
                table: "AttributeValues",
                newName: "AttributeValueID");

            migrationBuilder.RenameColumn(
                name: "AttributeId",
                table: "AttributeValues",
                newName: "AttributeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AttributeValues",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeValues_AttributeId",
                table: "AttributeValues",
                newName: "IX_AttributeValues_AttributeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Attributes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "BaseTransaction",
                newName: "EmployeeID");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "BaseTransaction",
                newName: "BranchID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BaseTransaction",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_BaseTransactions_EmployeeId",
                table: "BaseTransaction",
                newName: "IX_BaseTransaction_EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_BaseTransactions_BranchId",
                table: "BaseTransaction",
                newName: "IX_BaseTransaction_BranchID");

            migrationBuilder.AlterColumn<string>(
                name: "LogoImage",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoverImage",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseTransaction",
                table: "BaseTransaction",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeID",
                table: "AttributeValues",
                column: "AttributeID",
                principalTable: "Attributes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseTransaction_Branches_BranchID",
                table: "BaseTransaction",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseTransaction_Employees_EmployeeID",
                table: "BaseTransaction",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Company_CompanyID",
                table: "Branches",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Employees_BranchManagerID",
                table: "Branches",
                column: "BranchManagerID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyTransactions_BaseTransaction_BaseTransactionID",
                table: "BuyTransactions",
                column: "BaseTransactionID",
                principalTable: "BaseTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyTransactions_BaseTransaction_ID",
                table: "BuyTransactions",
                column: "ID",
                principalTable: "BaseTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyTransactions_Suppliers_SupplierID",
                table: "BuyTransactions",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID",
                principalTable: "Categories",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_DepartmentHeadID",
                table: "Departments",
                column: "DepartmentHeadID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branches_BranchID",
                table: "Employees",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentID",
                table: "Employees",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_BaseTransaction_ID",
                table: "PaymentTransactions",
                column: "ID",
                principalTable: "BaseTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_BaseTransaction_ReferencingTransactionID",
                table: "PaymentTransactions",
                column: "ReferencingTransactionID",
                principalTable: "BaseTransaction",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Customers_CustomerID",
                table: "PaymentTransactions",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Suppliers_SupplierID",
                table: "PaymentTransactions",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_AttributeValues_AttributeValueID",
                table: "ProductAttributes",
                column: "AttributeValueID",
                principalTable: "AttributeValues",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductSKUs_ProductSKUID",
                table: "ProductAttributes",
                column: "ProductSKUID",
                principalTable: "ProductSKUs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Products_ProductID",
                table: "ProductAttributes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_BaseTransaction_ID",
                table: "ProductMoveTransactions",
                column: "ID",
                principalTable: "BaseTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_Branches_FromBranchID",
                table: "ProductMoveTransactions",
                column: "FromBranchID",
                principalTable: "Branches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_Branches_ToBranchID",
                table: "ProductMoveTransactions",
                column: "ToBranchID",
                principalTable: "Branches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_Employees_FromEmployeeID",
                table: "ProductMoveTransactions",
                column: "FromEmployeeID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMoveTransactions_Employees_ToEmployeeID",
                table: "ProductMoveTransactions",
                column: "ToEmployeeID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSKUImage_ProductSKUs_ProductSKUID",
                table: "ProductSKUImage",
                column: "ProductSKUID",
                principalTable: "ProductSKUs",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSKUs_Products_ProductID",
                table: "ProductSKUs",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Products_ProductID",
                table: "ProductSuppliers",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierID",
                table: "ProductSuppliers",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellTransactions_BaseTransaction_BaseTransactionID",
                table: "SellTransactions",
                column: "BaseTransactionID",
                principalTable: "BaseTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellTransactions_BaseTransaction_ID",
                table: "SellTransactions",
                column: "ID",
                principalTable: "BaseTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellTransactions_Customers_CustomerID",
                table: "SellTransactions",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionChanges_BaseTransaction_BaseTransactionID",
                table: "TransactionChanges",
                column: "BaseTransactionID",
                principalTable: "BaseTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_BaseTransaction_BaseTransactionID",
                table: "TransactionItems",
                column: "BaseTransactionID",
                principalTable: "BaseTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
