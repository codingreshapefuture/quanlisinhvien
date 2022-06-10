using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Quanly_Sinhvien
{
    public partial class ManageStudent : Form
    {
        public class SinhVien
        {
            private string tenSinhVien, ngaySinh, maSinhVien, gioiTinh, khoa;
            private float diemRenLuyen, diemTrungBinh;

            public string TenSinhVien { get => tenSinhVien; set => tenSinhVien = value; }
            public string NgaySinh { get => ngaySinh; set => ngaySinh = value; }
            public string MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
            public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
            public string Khoa { get => khoa; set => khoa = value; }
            public float DiemRenLuyen { get => diemRenLuyen; set => diemRenLuyen = value; }
            public float DiemTrungBinh { get => diemTrungBinh; set => diemTrungBinh = value; }
            public SinhVien() { }
            public SinhVien(string ten, string ngaysinh, string masv, string gioitinh, string khoa, float diemRL, float diemTB)
            {
                this.tenSinhVien = ten;
                this.ngaySinh = ngaysinh;
                this.maSinhVien = masv;
                this.gioiTinh = gioitinh;
                this.khoa = khoa;
                this.diemRenLuyen = diemRL;
                this.diemTrungBinh = diemTB;
            }
        }
        public class DocGhiDuLieu
        {
            public StreamWriter fileOut;
            public StreamReader fileIn;
            public void GhiSinhVien(List<SinhVien> DSSV)
            {
                fileOut = new StreamWriter("SinhVien.txt", false);
                for (int i = 0; i < DSSV.Count; i++)
                {
                    fileOut.WriteLine(DSSV[i].TenSinhVien + "#" + DSSV[i].NgaySinh + "#" + DSSV[i].MaSinhVien + "#" + DSSV[i].GioiTinh + "#" + DSSV[i].Khoa + "#" + DSSV[i].DiemRenLuyen + "#" + DSSV[i].DiemTrungBinh);
                }
                fileOut.Close();
            }
            public List<SinhVien> DocSinhVien()
            {
                List<SinhVien> DSSV = new List<SinhVien>();
                try
                {
                    fileIn = new StreamReader("SinhVien.txt");
                    while (!fileIn.EndOfStream)
                    {
                        try
                        {
                            string[] str = fileIn.ReadLine().Split('#');
                            SinhVien laySV = new SinhVien(str[0], str[1], str[2], str[3], str[4], float.Parse(str[5]), float.Parse(str[6]));
                            DSSV.Add(laySV);
                        }
                        catch
                        {
                            break;
                        }
                    }
                    fileIn.Close();
                    return DSSV;
                }
                catch
                {
                    return DSSV;
                }
            }
        }
        DocGhiDuLieu db = new DocGhiDuLieu();
        List<SinhVien> DSSV = new List<SinhVien>();

        public void LoadSinhVien(List<SinhVien> DS)
        {
            for (int i = 0; i < DS.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = DS[i].TenSinhVien;
                listItems.Items.Add(item);
                ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem(item, (DS[i].NgaySinh));
                item.SubItems.Add(subitem);
                ListViewItem.ListViewSubItem subitem1 = new ListViewItem.ListViewSubItem(item, (DS[i].MaSinhVien));
                item.SubItems.Add(subitem1);
                ListViewItem.ListViewSubItem subitem5 = new ListViewItem.ListViewSubItem(item, (DS[i].GioiTinh));
                item.SubItems.Add(subitem5);
                ListViewItem.ListViewSubItem subitem2 = new ListViewItem.ListViewSubItem(item, (DS[i].DiemRenLuyen + ""));
                item.SubItems.Add(subitem2);
                ListViewItem.ListViewSubItem subitem3 = new ListViewItem.ListViewSubItem(item, (DS[i].DiemTrungBinh + ""));
                item.SubItems.Add(subitem3);
                ListViewItem.ListViewSubItem subitem4 = new ListViewItem.ListViewSubItem(item, (DS[i].Khoa));
                item.SubItems.Add(subitem4);
            }
        }
        private void ManageStudent_Load(object sender, EventArgs e)
        {
            DSSV = db.DocSinhVien();
            listItems.Columns.Add("Tên", 200);
            listItems.Columns.Add("Ngày sinh", 100);
            listItems.Columns.Add("MSSV", 80);
            listItems.Columns.Add("Giới tính", 80);
            listItems.Columns.Add("Điểm rèn luyện", 100);
            listItems.Columns.Add("Điểm trung bình", 100);
            listItems.Columns.Add("Khoa", 200);
            LoadSinhVien(DSSV);
            linkLabel1.Links.Add(0, 15, "www.facebook.com/berlin.03");
        }
        public ManageStudent()
        {
            InitializeComponent();

        }

        private void boxDateInput_Click(object sender, EventArgs e)
        {

        }

        private void buttonNhap_Click(object sender, EventArgs e)//Nhap du lieu
        {
            for (int i = 0; i < DSSV.Count; i++)
            {
                if (boxMSSVInput.Text == DSSV[i].MaSinhVien)
                {
                    MessageBox.Show("Mã số sinh viên là duy nhất , đã có sinh viên có mã số này!");
                    return;
                }
            }
            try
            {
                if ((string.IsNullOrEmpty(boxNameInput.Text)) || (string.IsNullOrEmpty(boxDateInput.Text)) || (string.IsNullOrEmpty(boxMSSVInput.Text)) || (string.IsNullOrEmpty(boxPointTryHardInput.Text)) || (string.IsNullOrEmpty(boxPointStudyInput.Text)) || (string.IsNullOrEmpty(boxKhoaInput.Text)))
                {
                    MessageBox.Show("Vui lòng điền đủ thông tin");
                }
                else
                {

                    ListViewItem item = new ListViewItem();
                    item.Text = boxNameInput.Text;
                    listItems.Items.Add(item);

                    ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem(item, (boxDateInput.Text));
                    item.SubItems.Add(subitem);
                    ListViewItem.ListViewSubItem subitem1 = new ListViewItem.ListViewSubItem(item, (boxMSSVInput.Text));
                    item.SubItems.Add(subitem1);
                    ListViewItem.ListViewSubItem subitem5 = new ListViewItem.ListViewSubItem(item, (boxGioiTinhInput.Text));
                    item.SubItems.Add(subitem5);
                    ListViewItem.ListViewSubItem subitem2 = new ListViewItem.ListViewSubItem(item, (boxPointTryHardInput.Text));
                    item.SubItems.Add(subitem2);
                    ListViewItem.ListViewSubItem subitem3 = new ListViewItem.ListViewSubItem(item, (boxPointStudyInput.Text));
                    item.SubItems.Add(subitem3);
                    ListViewItem.ListViewSubItem subitem4 = new ListViewItem.ListViewSubItem(item, (boxKhoaInput.Text));
                    item.SubItems.Add(subitem4);

                    SinhVien newsv = new SinhVien(boxNameInput.Text, boxDateInput.Text, boxMSSVInput.Text, boxGioiTinhInput.Text, boxKhoaInput.Text, float.Parse(boxPointTryHardInput.Text), float.Parse(boxPointStudyInput.Text));
                    DSSV.Add(newsv);
                    db.GhiSinhVien(DSSV);
                    boxNameInput.Clear();
                    boxDateInput.Clear();
                    boxMSSVInput.Clear();
                    boxPointTryHardInput.Clear();
                    boxPointStudyInput.Clear();
                    boxGioiTinhInput.Clear();
                    boxKhoaInput.Clear();
                    boxNameInput.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng điền điểm là số! ");
            }
        }


        private void listItems_SelectedIndexChanged(object sender, EventArgs e)//Lay thong tin sinh vien
        {
            if (listItems.SelectedItems.Count != 0)
            {
                ListViewItem lvi = listItems.SelectedItems[0];
                boxNameInput.Text = lvi.SubItems[0].Text;
                boxDateInput.Text = lvi.SubItems[1].Text;
                boxMSSVInput.Text = lvi.SubItems[2].Text;
                boxGioiTinhInput.Text = lvi.SubItems[3].Text;
                boxPointTryHardInput.Text = lvi.SubItems[4].Text;
                boxPointStudyInput.Text = lvi.SubItems[5].Text;
                boxKhoaInput.Text = lvi.SubItems[6].Text;
            }
        }

        private void buttonXoaSV_Click(object sender, EventArgs e)//Xoa 1 sinh vien
        {
            if (listItems.SelectedItems.Count > 0)
            {
                DialogResult dl = MessageBox.Show("Bạn muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dl == DialogResult.OK)
                {
                    ListViewItem lvi = listItems.SelectedItems[0];
                    for (int i = 0; i < DSSV.Count; i++)
                    {
                        if (lvi.SubItems[2].Text == DSSV[i].MaSinhVien)
                        {
                            DSSV.Remove(DSSV[i]);
                            db.GhiSinhVien(DSSV);
                            listItems.Items.Remove(listItems.SelectedItems[0]);
                            break;
                        }
                    }
                }
            }
            else MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn có muốn thoát?", "Thoát chương trình", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (thoat == DialogResult.OK)
            {
                MessageBox.Show("Chúc bạn một ngày tốt lành !");
                Application.Exit();
            }
        }

        private void buttonXoaHetSV_Click(object sender, EventArgs e)//Xoa het sinh vien
        {

            DialogResult dl = MessageBox.Show("Bạn muốn xóa toàn bộ sinh viên?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dl == DialogResult.OK)
            {

                DialogResult dl1 = MessageBox.Show("Hành động này không thể hoàn tác ! Xác nhận xóa ?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dl1 == DialogResult.OK)
                {
                    listItems.Items.Clear();
                    db.GhiSinhVien(DSSV);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)// Cap nhat du lieu
        {
            if (listItems.SelectedItems.Count != 0)
            {
                ListViewItem lvi = listItems.SelectedItems[0];
                listItems.SelectedItems[0].SubItems[0].Text = boxNameInput.Text;
                listItems.SelectedItems[0].SubItems[1].Text = boxDateInput.Text;
                //listItems.SelectedItems[0].SubItems[2].Text = boxMSSVInput.Text;
                listItems.SelectedItems[0].SubItems[3].Text = boxGioiTinhInput.Text;
                listItems.SelectedItems[0].SubItems[4].Text = boxPointTryHardInput.Text;
                listItems.SelectedItems[0].SubItems[5].Text = boxPointStudyInput.Text;
                listItems.SelectedItems[0].SubItems[6].Text = boxKhoaInput.Text;
            }
            for (int i = 0; i < DSSV.Count; i++)
            {
                if (DSSV[i].MaSinhVien == boxMSSVInput.Text)
                {
                    DSSV[i].TenSinhVien = boxNameInput.Text;
                    DSSV[i].NgaySinh = boxDateInput.Text;
                    DSSV[i].GioiTinh = boxGioiTinhInput.Text;
                    DSSV[i].DiemRenLuyen = float.Parse(boxPointTryHardInput.Text);
                    DSSV[i].DiemTrungBinh = float.Parse(boxPointStudyInput.Text);
                    DSSV[i].Khoa = boxKhoaInput.Text;
                    break;
                }
            }
            db.GhiSinhVien(DSSV);
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            boxNameInput.Clear();
            boxGioiTinhInput.Clear();
            boxKhoaInput.Clear();
            boxMSSVInput.Clear();
            boxPointStudyInput.Clear();
            boxPointTryHardInput.Clear();
            boxDateInput.Clear();
        }

        private void button2_Click(object sender, EventArgs e)//Xep loai sinh vien
        {
            if (listItems.SelectedItems.Count > 0)
            {
                ListViewItem lvi = listItems.SelectedItems[0];
                for (int i = 0; i < DSSV.Count; i++)
                {
                    if (lvi.SubItems[2].Text == DSSV[i].MaSinhVien)
                    {
                        if (DSSV[i].DiemTrungBinh >= 8.0) MessageBox.Show("Sinh viên này xếp loại: Giỏi !");
                        if (DSSV[i].DiemTrungBinh >= 6.5 && DSSV[i].DiemTrungBinh < 8) MessageBox.Show("Sinh viên này xếp loại: Khá !");
                        if (DSSV[i].DiemTrungBinh >= 5 && DSSV[i].DiemTrungBinh < 6.5) MessageBox.Show("Sinh viên này xếp loại: Trung bình !");
                        if (DSSV[i].DiemTrungBinh >= 3 && DSSV[i].DiemTrungBinh < 5) MessageBox.Show("Sinh viên này xếp loại: Yếu !");
                        if (DSSV[i].DiemTrungBinh < 3) MessageBox.Show("Sinh viên không đủ điều kiện lên lớp !");
                    }
                }
                for (int i = 0; i < DSSV.Count; i++)
                {
                    if (lvi.SubItems[2].Text == DSSV[i].MaSinhVien)
                    {
                        if (DSSV[i].DiemRenLuyen >= 90)
                        {
                            MessageBox.Show("Điểm rèn luyện của sinh viên này xếp loại: Xuất sắc !"); break;
                        }

                        if (DSSV[i].DiemRenLuyen >= 80)
                        {
                            MessageBox.Show("Điểm rèn luyện của sinh viên này xếp loại: Tốt !"); break;
                        }

                        if (DSSV[i].DiemRenLuyen >= 65)
                        {
                            MessageBox.Show("Điểm rèn luyện của sinh viên này xếp loại: Khá !"); break;
                        }
                        if (DSSV[i].DiemRenLuyen >= 50)
                        {
                            MessageBox.Show("Điểm rèn luyện của sinh viên này xếp loại: Trung bình !"); break;
                        }
                        if (DSSV[i].DiemRenLuyen >= 35)
                        {
                            MessageBox.Show("Điểm rèn luyện của sinh viên này xếp loại: Yếu !"); break;
                        }
                        MessageBox.Show("Điểm rèn luyện của sinh viên này xếp loại: Kém !");
                    }
                }
            }
            else MessageBox.Show("Vui lòng chọn sinh viên cần xếp hạng");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//Lien he tac gia
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DSSV.Count; i++)
            {
                for (int j = 0; j < DSSV.Count - 1; j++)
                {
                    if (DSSV[j].DiemTrungBinh < DSSV[j + 1].DiemTrungBinh)
                    {
                        SinhVien temp = DSSV[j];
                        DSSV[j] = DSSV[j + 1];
                        DSSV[j + 1] = temp;
                    }
                }
            }
            listItems.Items.Clear();
            LoadSinhVien(DSSV);
            MessageBox.Show("Sắp xếp sinh viên hoàn tất!");
            Thread.Sleep(3000);
            DialogResult dl = MessageBox.Show("Bạn có muốn cập nhật sinh viên?", "Cập nhật danh sách", MessageBoxButtons.YesNo);
            if (dl == DialogResult.Yes)
            {
                db.GhiSinhVien(DSSV);
                MessageBox.Show("Cập nhật sinh viên hoàn tất!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.Title = "Save File";
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "Text File (*.txt)|*.txt";
                saveFileDialog1.OverwritePrompt = true;
                StreamWriter fileOut1 = new StreamWriter(saveFileDialog1.FileName);
                fileOut1.WriteLine("Danh sách sinh viên đã có:");
                for (int i = 0; i < DSSV.Count; i++)
                {
                    fileOut1.WriteLine(DSSV[i].TenSinhVien + "|" + DSSV[i].NgaySinh + "|" + DSSV[i].MaSinhVien + "|" + DSSV[i].GioiTinh + "|" + DSSV[i].Khoa + "|DiemRL:" + DSSV[i].DiemRenLuyen + "|DiemTB:" + DSSV[i].DiemTrungBinh);
                }
                fileOut1.Dispose();
                fileOut1.Close();
            }
        }
    }
}
