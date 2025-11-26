using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Ex2
{
    public partial class Form1 : Form
    {
        DataUtil dataUtil = new DataUtil();

        public Form1()
        {
            InitializeComponent();
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTimTheoLuong.Click += btnTimTheoLuong_Click;
            btnTimTheoTinh.Click += btnTimTinh_Click;
            btnHienThi.Click += (s, e) => DisplayData();
            btnTim.Click += btnTimKiem_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData(List<NhanVien> list = null)
        {
            if (list == null) list = dataUtil.GetAllNhanVien();
            dataGridView1.DataSource = list;
        }

        private NhanVien GetNhanVienFromForm()
        {
            return new NhanVien
            {
                MaNV = txtMaNV.Text,
                HoTen = txtHoTen.Text,
                Tuoi = int.Parse(txtTuoi.Text),
                Luong = double.Parse(txtLuong.Text),
                Xa = txtXa.Text,
                Huyen = txtHuyen.Text,
                Tinh = txtTinh.Text,
                DienThoai = txtDienThoai.Text
            };
        }

        // Question 3 & 4: Add with duplicate check
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nv = GetNhanVienFromForm();
                if (dataUtil.AddNhanVien(nv))
                {
                    MessageBox.Show("Thêm thành công!");
                    DisplayData();
                }
                else
                {
                    MessageBox.Show("Trùng mã nhân viên!");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nhập liệu: " + ex.Message); }
        }

        // Question 4: Edit
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nv = GetNhanVienFromForm();
                if (dataUtil.UpdateNhanVien(nv))
                {
                    MessageBox.Show("Sửa thành công!");
                    DisplayData();
                }
                else MessageBox.Show("Không tìm thấy mã nhân viên để sửa.");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // Question 4: Delete with confirmation
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string manv = txtMaNV.Text;
            if (MessageBox.Show($"Bạn có chắc muốn xóa nhân viên {manv}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataUtil.DeleteNhanVien(manv))
                {
                    DisplayData();
                    MessageBox.Show("Đã xóa.");
                }
                else MessageBox.Show("Không tìm thấy mã.");
            }
        }

        // Question 4: Search by ID
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string manv = txtMaNV.Text;
            var nv = dataUtil.FindByID(manv);
            if (nv != null)
            {
                DisplayData(new List<NhanVien> { nv });
            }
            else MessageBox.Show("Không tìm thấy nhân viên.");
        }

        // Question 5: Count & Sum Salary > 1000
        private void btnTimTheoLuong_Click(object sender, EventArgs e)
        {
            var list = dataUtil.GetAllNhanVien().Where(x => x.Luong > 1000).ToList();
            double tongLuong = list.Sum(x => x.Luong);
            int soLuong = list.Count;

            lblKetQua.Text = $"Số NV lương > 1000: {soLuong}. Tổng lương: {tongLuong}";
            DisplayData(list);
        }

        // Question 6: Count by Province
        private void btnTimTinh_Click(object sender, EventArgs e)
        {
            string tinhCanTim = txtTinh.Text; // Or use a separate search box
            var list = dataUtil.GetAllNhanVien().Where(x => x.Tinh.Equals(tinhCanTim, StringComparison.OrdinalIgnoreCase)).ToList();

            lblKetQua.Text = $"Tìm thấy {list.Count} nhân viên tại {tinhCanTim}";
            DisplayData(list);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtTuoi.Text = row.Cells["Tuoi"].Value.ToString();
                txtLuong.Text = row.Cells["Luong"].Value.ToString();
                txtXa.Text = row.Cells["Xa"].Value.ToString();
                txtHuyen.Text = row.Cells["Huyen"].Value.ToString();
                txtTinh.Text = row.Cells["Tinh"].Value.ToString();
                txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
