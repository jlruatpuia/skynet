using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet
{
    class Server2Client
    {
        public int Count { get; set; }
        public double Value { get; set; }
        public string Message { get; set; }
        public DataTable dataTable { get; set; }
        public DataSet dataSet { get; set; }

    }

    class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }

    class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Balance { get; set; }
    }
    class CustomerAccount
    {
        public int CustomerAccountID { get; set; }
        public int CustomerID { get; set; }
        public DateTime TransDate { get; set; }
        public string Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
    }

    class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public double BuyingValue { get; set; }
        public double SellingValue { get; set; }
        public int Quantity { get; set; }
        public string BarCode { get; set; }
        public string Message { get; set; }
    }
    class ProductDetail
    {
        public int ProductDetailID { get; set; }
        public int ProductID { get; set; }
        public double BuyingValue { get; set; }
        public double SellingValue { get; set; }
        public int Quantity { get; set; }
    }
    class Purchase
    {
        public int PurchaseID { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierID { get; set; }
        public double Amount { get; set; }
        public double Payment { get; set; }
        public double Balance { get; set; }
    }
    class PurchaseDetail
    {
        public int PurchaseDetailID { get; set; }
        public string InvoiceNo { get; set; }
        public int ProductID { get; set; }
        public double BuyingValue { get; set; }
        public double SellingValue { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
    class Sale
    {
        public int SaleID { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerID { get; set; }
        public double Amount { get; set; }
        public double Discount { get; set; }
        public double Payment { get; set; }
        public double Balance { get; set; }
    }
    class SaleDetail
    {
        public int SaleDetailID { get; set; }
        public string InvoiceNo { get; set; }
        public int ProductID { get; set; }
        public double BuyingValue { get; set; }
        public double SellingValue { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
    class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
    }
    class SupplierAccount
    {
        public int SupplierAccountID { get; set; }
        public int SupplierID { get; set; }
        public DateTime TransDate { get; set; }
        public string Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
    }
}
