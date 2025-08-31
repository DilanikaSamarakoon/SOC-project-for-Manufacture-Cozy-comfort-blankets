using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using FontAwesome.Sharp;
using TharukaPOS.Forms.Models;
using TharukaPOS.Forms.Views;

namespace TharukaPOS.Forms.Presenters
{
    public class ProductPresenter
    {
        private IProductView view;
        private IProductRepository repository;
        private BindingSource productBindingSource;
        private IEnumerable<ProductModel> productList;


        //Constuctor
        public ProductPresenter(IProductView view, IProductRepository repository)
        {
            this.productBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            //Subscribe event handler methods to view events
            this.view.SearchEvent += SearchProduct;
            this.view.AddNewEvent += AddNewProduct;
            this.view.EditEvent += LoadSelectedProductToEdit;
            this.view.DeleteEvent += DeleteSelectedProduct;
            this.view.SaveEvent += SaveProduct;
            this.view.CancelEvent += CancelAction;

            //Set pets binding source
            this.view.SetProductListBindingSource(productBindingSource);

            //Load pet list view
            LoadAllProductList();

            //Show view
            this.view.Show();
        }

        private void LoadAllProductList()
        {
            productList = repository.GetAll();
            productBindingSource.DataSource = productList; //Set data source.
        }

        private void SearchProduct(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                productList = repository.GetByValue(this.view.SearchValue);
            else productList = repository.GetAll();
            productBindingSource.DataSource = productList;
        }

        private void AddNewProduct(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private void LoadSelectedProductToEdit(object sender, EventArgs e)
        {
            var product = (ProductModel)productBindingSource.Current;
            view.ProductId = product.ProductId.ToString();
            view.ProductTitle = product.ProductName;
            view.Weight = product.Weight.ToString();
            view.Quantity = product.Quantity.ToString();
           
            view.BuyingPrice = product.BuyingPrice.ToString();
            view.SellingPrice = product.SellingPrice.ToString();
            view.IsEdit = true;
        }



        private void SaveProduct(object sender, EventArgs e)
        {
            var model = new ProductModel();

            try
            {
                // Attempt to parse all numeric fields
                if (!int.TryParse(view.ProductId, out int productId))
                    throw new FormatException("Invalid Product ID");

                if (!decimal.TryParse(view.Weight, out decimal weight))
                    throw new FormatException("Invalid Weight");

                if (!int.TryParse(view.Quantity, out int quantity))
                    throw new FormatException("Invalid Quantity");

              

                if (!decimal.TryParse(view.BuyingPrice, out decimal buyingPrice))
                    throw new FormatException("Invalid Buying Price");

                if (!decimal.TryParse(view.SellingPrice, out decimal sellingPrice))
                    throw new FormatException("Invalid Selling Price");

                // Set values to model
                model.ProductId = productId;
                model.ProductName = view.ProductTitle;
                model.Weight = weight;
                model.Quantity = quantity;
               
                model.BuyingPrice = buyingPrice;
                model.SellingPrice = sellingPrice;

                new Common.ModelDataValidation().Validate(model);

                if (view.IsEdit)
                {
                    repository.Edit(model);
                    view.Message = "Product edited successfully";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Product added successfully";
                }

                view.IsSuccessful = true;
                LoadAllProductList();
                CleanviewFields();
            }
            catch (FormatException ex)
            {
                view.IsSuccessful = false;
                view.Message = "Input format error: " + ex.Message;
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error: " + ex.Message;
            }
        }


        private void CleanviewFields()
        {
            view.ProductId = "0";
            view.ProductTitle = "";
            view.Weight = "";
            view.Quantity = "";
           
            view.BuyingPrice = "";
            view.SellingPrice = "";
           
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        private void DeleteSelectedProduct(object sender, EventArgs e)
        {
            try
            {
                var product = (ProductModel)productBindingSource.Current;
                repository.Delete(product.ProductId);
                view.IsSuccessful=true;
                view.Message = "Product deleted succesfully";
                LoadAllProductList();

            }
            catch (Exception ex)
            {
                view.IsSuccessful=false;
                view.Message = "An error occured, could not delete Product";
            }
        }

        

        

        
    }
}
