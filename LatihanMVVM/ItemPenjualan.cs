using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LatihanMVVM
{
    public class ItemPenjualan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(50)]
        public string NamaBarang { get; set; }

        public ItemPenjualan()
        {
            DiskonPersen = 0;
        }

        public int Jumlah { get; set; }

        public decimal Harga { get; set; }

        public decimal DiskonPersen { get; set; }

        public decimal Total()
        {
            decimal total = Jumlah * Harga;
            return total - (DiskonPersen / 100 * total);
        }
    }
}