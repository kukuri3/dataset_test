using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll",
    CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int mciSendString(string command,
           System.Text.StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        private string aliasName = "MediaFile";

        public Form1()
        {
            InitializeComponent();
            //dataSet1.ReadXml("data.xml");

            dataGridView1.DataSource = dataSet1;
            // 選択モードを行単位での選択のみにする
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DataGridView1のセルを読み取り専用にする
            dataGridView1.ReadOnly = true;
            //列ヘッダーを非表示にする
            dataGridView1.ColumnHeadersVisible = false;
            //行ヘッダーを非表示にする
            dataGridView1.RowHeadersVisible = false;
            //ヘッダーとすべてのセルの内容に合わせて、列の幅を自動調整する
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //ヘッダーとすべてのセルの内容に合わせて、行の高さを自動調整する
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataSet1.WriteXml("data.xml");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataSet1.ReadXml("data.xml");
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            // グリッドを基準としたカーソル位置のポイントを取得
            Point p = dataGridView1.PointToClient(Cursor.Position);
            // 取得したポイントからHitTestでセル位置取得
            DataGridView.HitTestInfo ht = dataGridView1.HitTest(p.X, p.Y);
            MessageBox.Show("row（行）:" + ht.RowIndex + " col（列）:" + ht.ColumnIndex);
            int r = ht.RowIndex;
            if(r>0)dataGridView1.Rows.RemoveAt(r);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //再生するファイル名
            string fileName = "http://kukuri3.mydns.jp:17315/[%E3%83%8A%E3%82%B7%E3%83%A7%E3%82%B8%E3%82%AA%E3%83%81%E3%83%A3%E3%83%B3%E3%83%8D%E3%83%AB]%E3%83%A1%E3%83%BC%E3%83%87%E3%83%BC%EF%BC%81%EF%BC%93%EF%BC%9A%E8%88%AA%E7%A9%BA%E6%A9%9F%E4%BA%8B%E6%95%85%E3%81%AE%E7%9C%9F%E5%AE%9F%E3%81%A8%E7%9C%9F%E7%9B%B8%E3%80%8C%E5%BE%A1%E5%B7%A3%E9%B7%B9%E3%81%AE%E5%B0%BE%E6%A0%B9%E3%80%8D_20150921_1400_1500.mp4";

            string cmd;
            //ファイルを開く
            cmd = "open \"" + fileName + "\" alias " + aliasName;
            if (mciSendString(cmd, null, 0, IntPtr.Zero) != 0)
                return;
            //再生する
            cmd = "play " + aliasName;
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }
    }
}
