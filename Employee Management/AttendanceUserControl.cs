﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Employee_Management
{
    public partial class AttendanceUserControl1 : UserControl
    {

        public static int ID;
        public int sortID;
        public AttendanceUserControl1()
        {
            InitializeComponent();
        }

        private void AttendanceUserControl_Load(object sender, EventArgs e)
        {
            AttendanceClass a = new AttendanceClass();
            DataTable dt = a.Select();
            dataGridView.DataSource = dt;
            lblInTime.Text = DateTime.Now.ToString("HH:mm");
            a.ArrivedTime = lblInTime.Text;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



            AttendanceClass a = new AttendanceClass();
            if (e.ColumnIndex == dataGridView.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                ID = Convert.ToInt32(dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[2].Value);
                UpdateAttendance update = new UpdateAttendance();
                update.ShowDialog();
                
                

            }

            if (e.ColumnIndex == dataGridView.Columns["Delete"].Index && e.RowIndex >= 0)
            {
               DialogResult result= MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
                 
                if ( result==DialogResult.Yes)
                {
                      ID = Convert.ToInt32(dataGridView.Rows[dataGridView.CurrentRow.Index].Cells[2].Value);

                      bool success = a.Delete(ID);


                        if (success == true)
                         {
                             MessageBox.Show("Deleted successfully");
                         }
                        else
                         {
                            MessageBox.Show("Cannot delete");
                         }

                }

            }

        }
        AttendanceClass a = new AttendanceClass();
        int Id = 0;

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            if (txtEmployeeId.Text == string.Empty
               //txtArrivedTime.Text == string.Empty
               )
            {
                MessageBox.Show("Plaese fill the empty fields!");
            }
            else if (txtEmployeeId.Text == string.Empty)
            {
                MessageBox.Show("Please enter an Employee Id!");
            }
            /*  else if(txtArrivedTime.Text == string.Empty)
              {
                  MessageBox.Show("Plaese enter the arrived time");

              }
              */
            else if (!int.TryParse(txtEmployeeId.Text, out Id))
            {
                MessageBox.Show("Employee ID is invaid");
            }






            else
            {

                a.EmployeeId = Int32.Parse(txtEmployeeId.Text);
                a.Date = dateTimePicker1.Text;

               // lblInTime.Text = DateTime.Now.ToString("HH:mm");
                a.ArrivedTime = lblInTime.Text;
                //a.ArrivedTime = lblInTime.Text;
                // a.ArrivedTime = txtArrivedTime.Text;
                // a.LeftTime = int.Parse(txtLeaftTime.Text);

                /* Type dataType = a.EmployeeId.GetType();
                 if (!dataType.Equals(typeof(int)))
                 {
                     MessageBox.Show("Invalid Employee Id");
                 }
                 */

                bool success = a.Insert(a);
                if (success == true)
                {
                    MessageBox.Show("Successfully inserted");
                }
                else
                {
                    MessageBox.Show("Cannot insert");
                }

                DataTable dt = a.Select();
                dataGridView.DataSource = dt;
            }
        }

        private void TabPage3_Click(object sender, EventArgs e)
        {
            AttendanceClass a = new AttendanceClass();
            DataTable dt = a.Select();
            dataGridView.DataSource = dt;
        }

        static string myConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConnectionString);
            int key = Int32.Parse(txtSearch.Text);
            string searchDate = dateTimePickerSearch.Text;

           
            SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT AttendID,EmpID,date,inTime,outTime FROM Attendance WHERE  EmpID=" + key + "AND date='" + searchDate + "'", conn);
          
            DataTable dt = new DataTable();
            adapter1.Fill(dt);
            dataGridView.DataSource = dt;
        }

        private void BtnViewAll_Click(object sender, EventArgs e)
        {
            DataTable dt = a.Select();
            dataGridView.DataSource = dt;
        }

        private void TabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        AttendanceClass at = new AttendanceClass();
        private void Button7_Click(object sender, EventArgs e)
        {
                          
            DisplayAttendanceReport display = new DisplayAttendanceReport();
            display.ShowDialog();

            int sortID = Int32.Parse(txtEmpIDReport.Text);


            at.SortByID(sortID);

            AttendanceReport report = new AttendanceReport();
            report.ShowDialog();

            DataTable dt = new DataTable();
            dataGridView.DataSource = dt;



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
