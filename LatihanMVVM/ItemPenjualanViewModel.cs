using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace LatihanMVVM
{
    public class ItemPenjualanViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ItemPenjualan model;

        private ICommand simpanCommand;

        public ItemPenjualanViewModel(ItemPenjualan itemPenjualan = null)
        {
            this.model = itemPenjualan ?? new ItemPenjualan();
        }

        public string NamaBarang
        {
            get { return model.NamaBarang; }
            set
            {
                if (value != model.NamaBarang)
                {
                    model.NamaBarang = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("NamaBarang"));
                }
            }
        }

        public int Jumlah
        {
            get { return model.Jumlah; }
            set
            {
                if (value != model.Jumlah)
                {
                    model.Jumlah = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Jumlah"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Total"));
                }
            }
        }

        public decimal Harga
        {
            get { return model.Harga; }
            set
            {
                if (value != model.Harga)
                {
                    model.Harga = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Harga"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Total"));
                }
            }
        }

        public decimal DiskonPersen
        {
            get { return model.DiskonPersen; }
            set
            {
                if (value != model.DiskonPersen)
                {
                    model.DiskonPersen = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DiskonPersen"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Total"));
                }
            }
        }

        public string Total
        {
            get
            {
                decimal? total = model.Total();
                if (!total.HasValue)
                {
                    return "-";
                }
                else
                {
                    return total.Value.ToString("C");
                }
            }
        }

        public ItemPenjualan Model
        {
            get { return this.model; }
        }

        public ICommand SimpanCommand
        {
            get
            {
                if (this.simpanCommand == null)
                {
                    this.simpanCommand = new SimpanCommand(this);
                }
                return this.simpanCommand;
            }
        }
    }

    public class SimpanCommand : ICommand
    {

        private ItemPenjualanViewModel viewModel;

        public SimpanCommand(ItemPenjualanViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.Model.Total() > 0;
        }

        public void Execute(object parameter)
        {
            using (var db = new LatihanContext())
            {
                db.Database.Log = Console.Write;
                db.DaftarItemPenjualan.Add(viewModel.Model);
                db.SaveChanges();
                MessageBox.Show("Data berhasil disimpan ke database");
            }
        }
    }
}