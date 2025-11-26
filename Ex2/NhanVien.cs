using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    internal class NhanVien
    {
        public NhanVien()
        {
        }

        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public double Luong { get; set; }
        public string Xa { get; set; }
        public string Huyen { get; set; }
        public string Tinh { get; set; }
        public string DienThoai { get; set; }

        public NhanVien(string maNV, string hoTen, int tuoi, double luong, string xa, string huyen, string tinh, string dienThoai)
        {
            MaNV = maNV;
            HoTen = hoTen;
            Tuoi = tuoi;
            Luong = luong;
            Xa = xa;
            Huyen = huyen;
            Tinh = tinh;
            DienThoai = dienThoai;
        }
    }
}
