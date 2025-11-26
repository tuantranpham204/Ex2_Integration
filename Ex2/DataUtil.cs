using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Ex2
{
    internal class DataUtil
    {
        private string fileName = "congty.xml";
        private XmlDocument doc;
        private XmlElement root;

        public DataUtil()
        {
            doc = new XmlDocument();
            if (File.Exists(fileName))
            {
                doc.Load(fileName);
                root = doc.DocumentElement;
            }
            else
            {
                // Create file if it doesn't exist
                XmlElement congty = doc.CreateElement("congty");
                doc.AppendChild(congty);
                root = congty;
                doc.Save(fileName);
            }
        }

        public List<NhanVien> GetAllNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();
            XmlNodeList nodes = root.SelectNodes("nhanvien");

            foreach (XmlNode node in nodes)
            {
                list.Add(new NhanVien
                {
                    MaNV = node.Attributes["manv"].Value,
                    HoTen = node.SelectSingleNode("hoten").InnerText,
                    Tuoi = int.Parse(node.SelectSingleNode("tuoi").InnerText),
                    Luong = double.Parse(node.SelectSingleNode("luong").InnerText),
                    Xa = node.SelectSingleNode("diachi/xa").InnerText,
                    Huyen = node.SelectSingleNode("diachi/huyen").InnerText,
                    Tinh = node.SelectSingleNode("diachi/tinh").InnerText,
                    DienThoai = node.SelectSingleNode("dienthoai").InnerText
                });
            }
            return list;
        }

        public bool AddNhanVien(NhanVien nv)
        {
            // Check duplicate ID (Question 4)
            if (FindByID(nv.MaNV) != null) return false;

            XmlElement nhanvien = doc.CreateElement("nhanvien");
            nhanvien.SetAttribute("manv", nv.MaNV);

            XmlElement hoten = doc.CreateElement("hoten"); hoten.InnerText = nv.HoTen;
            XmlElement tuoi = doc.CreateElement("tuoi"); tuoi.InnerText = nv.Tuoi.ToString();
            XmlElement luong = doc.CreateElement("luong"); luong.InnerText = nv.Luong.ToString();

            XmlElement diachi = doc.CreateElement("diachi");
            XmlElement xa = doc.CreateElement("xa"); xa.InnerText = nv.Xa;
            XmlElement huyen = doc.CreateElement("huyen"); huyen.InnerText = nv.Huyen;
            XmlElement tinh = doc.CreateElement("tinh"); tinh.InnerText = nv.Tinh;
            diachi.AppendChild(xa);
            diachi.AppendChild(huyen);
            diachi.AppendChild(tinh);

            XmlElement dienthoai = doc.CreateElement("dienthoai"); dienthoai.InnerText = nv.DienThoai;

            nhanvien.AppendChild(hoten);
            nhanvien.AppendChild(tuoi);
            nhanvien.AppendChild(luong);
            nhanvien.AppendChild(diachi);
            nhanvien.AppendChild(dienthoai);

            root.AppendChild(nhanvien);
            doc.Save(fileName);
            return true;
        }

        public bool UpdateNhanVien(NhanVien nv)
        {
            XmlNode oldNode = root.SelectSingleNode($"nhanvien[@manv='{nv.MaNV}']");
            if (oldNode == null) return false;

            oldNode.SelectSingleNode("hoten").InnerText = nv.HoTen;
            oldNode.SelectSingleNode("tuoi").InnerText = nv.Tuoi.ToString();
            oldNode.SelectSingleNode("luong").InnerText = nv.Luong.ToString();
            oldNode.SelectSingleNode("diachi/xa").InnerText = nv.Xa;
            oldNode.SelectSingleNode("diachi/huyen").InnerText = nv.Huyen;
            oldNode.SelectSingleNode("diachi/tinh").InnerText = nv.Tinh;
            oldNode.SelectSingleNode("dienthoai").InnerText = nv.DienThoai;

            doc.Save(fileName);
            return true;
        }

        public bool DeleteNhanVien(string manv)
        {
            XmlNode node = root.SelectSingleNode($"nhanvien[@manv='{manv}']");
            if (node != null)
            {
                root.RemoveChild(node);
                doc.Save(fileName);
                return true;
            }
            return false;
        }

        public NhanVien FindByID(string manv)
        {
            var list = GetAllNhanVien();
            return list.FirstOrDefault(x => x.MaNV == manv);
        }
    }
}
