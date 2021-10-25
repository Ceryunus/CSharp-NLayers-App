using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Northwind.Business.Concrete;
using Northwind.Business.Abstract;
using Northwind.Business.DependencyResolves.Ninject;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Concrete.NHibernate;
using Northwind.Entities.Concrete;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         //dependency injection 
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>(); 
        }

        private IProductService _productService;
        private ICategoryService _categoryService;
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            LoadProducts();
            LoadCategories();//categoy tablosunu getirdis
            LoadAddOperationCategories();
        }

        private void LoadAddOperationCategories()//ekleme ve update islemi icin kategorinin doldurulması 
        {
            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";
        }

        private void LoadCategories()//kategorilerin cbx e doldurma islemi
        {
            cbxCategory.DataSource= _categoryService.GetAll();
            cbxCategory.DisplayMember= "CategoryName";//combobaxın içindeki değerler 
            cbxCategory.ValueMember = "CategoryId";//combobox secim yapınca bize o seçımın ıd sini verme 

        }

        private void LoadProducts()//dgw nin icinin doldurlması
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)//kategoriye gore listeleme
        {
            try
            {
                dgwProduct.DataSource = _productService.GetByCategoryId(Convert.ToInt32(cbxCategory.SelectedValue));

            }
            catch
            {
            }
        }

        private void tbxProductName_TextChanged(object sender, EventArgs e)//arana kutusunda her yazıldıgında yazılan stringin cekme
        {
            dgwProduct.DataSource = _productService.GetByProductName(tbxProductName.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)//urun ekleme 
        {
            try
            {
                _productService.Add(new Product
                {

                    CategoryID = Convert.ToInt32(cbxCategoryId.SelectedValue),
                    ProductName = tbxProductName2.Text,
                    UnitPrice = Convert.ToDecimal(tbxPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStockAmount.Text),
                    QuantityPerUnit = tbxQuantityPerUnit.Text
                });
                LoadProducts();
                lblResult.Text = "ADDED";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)//update işlemei icin datagridview de tıklanan productun guncelleme islemi
        {
            _productService.Update(new Product
            {
                ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                CategoryID = Convert.ToInt32(cbxCategoryId.SelectedValue),
                ProductName = tbxProductName2.Text,
                UnitPrice = Convert.ToDecimal(tbxPrice.Text),
                UnitsInStock = Convert.ToInt16(tbxStockAmount.Text),
                QuantityPerUnit = tbxQuantityPerUnit.Text
            });
            LoadProducts();
            lblResult.Text = "UPDATED";

        }



        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)//texboxlara verilerin yerlestirilmesi
        {
            var row = dgwProduct.CurrentRow;
            tbxProductName2.Text = row.Cells[2].Value.ToString();
            cbxCategoryId.SelectedValue = row.Cells[1].Value; 
            tbxPrice.Text = row.Cells[3].Value.ToString();
            tbxStockAmount.Text= row.Cells[4].Value.ToString();
            tbxQuantityPerUnit.Text= row.Cells[5].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwProduct.CurrentRow != null)
                    _productService.Delete(new Product
                    {
                        ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)
                    });
                else
                {
                    MessageBox.Show("Select a product");
                }
                LoadProducts();
                lblResult.Text = "DELETED";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            } 
            
        }
    }
}
