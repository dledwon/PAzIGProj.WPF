using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PAzIGProj.Model;
using PAzIGProj.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PAzIGProj.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _twoWayTest;
        public string TwoWayTest
        {
            get { return _twoWayTest; }
            set
            {
                _twoWayTest = value;
                RaisePropertyChanged();
            }
        }
        //

        private IProductManager _productManager;

        private ObservableCollection<Product> _products;
        private string _newProductName;
        private double _newProductPrice;

        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            private set
            {
                Set(() => Products, ref _products, value);
            }
        }
        public string NewProductName
        {
            get { return _newProductName; }
            set
            {
                _newProductName = value;
                RaisePropertyChanged();
                AddProductCommand.RaiseCanExecuteChanged();
            }
        }
        public double NewProductPrice
        {
            get { return _newProductPrice; }
            set
            {
                _newProductPrice = value;
                RaisePropertyChanged();
                AddProductCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddProductCommand { get; private set; }

        public MainViewModel(IProductManager productManager)
        {
            TwoWayTest = "Test bindingu dwukierunkowego";
            //
            _productManager = productManager;

            AddProductCommand = new RelayCommand(AddProduct, AddProductCanExecute);

            RefreshProductList();
        }

        public void AddProduct()
        {
            _productManager.AddProduct(new Product(NewProductName, NewProductPrice));
            NewProductName = "";
            NewProductPrice = 0;
            RefreshProductList(); // lub Products.Add() - czyli niezale¿nie zarz¹dzamy stanem bazy i viewmodelu (przyda³oby siê wtedy sprawdziæ, czy uda³o siê dodaæ do db - AddProduct powinien zwracaæ bool)
        }
        public bool AddProductCanExecute()
        {
            return !(NewProductName == null || NewProductPrice == 0);
        }
        private void RefreshProductList()
        {
            Products = new ObservableCollection<Product>(_productManager.GetProducts());
        }

    }
}