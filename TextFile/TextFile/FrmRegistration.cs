using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextFile
{
    public partial class FrmRegistration : Form
    {
        public FrmRegistration()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentNo.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(txtContactNo.Text) ||
                string.IsNullOrWhiteSpace(cbProgram.Text) ||
                string.IsNullOrWhiteSpace(cbGender.Text))
            {
                MessageBox.Show("Error: Please fill in all the required fields before registering.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtLastName.Text.Any(char.IsDigit) || txtFirstName.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Error: First Name and Last Name cannot contain numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!long.TryParse(txtStudentNo.Text, out _))
            {
                MessageBox.Show("Error: Student Number must contain only numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentNo.Focus();
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Error: Please enter a valid age.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAge.Focus();
                return;
            }

            if (!long.TryParse(txtContactNo.Text, out _))
            {
                MessageBox.Show("Error: Contact Number must contain only numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactNo.Focus();
                return;
            }
            string studentNo = txtStudentNo.Text;
            string fileName = studentNo + ".txt";

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullPath = Path.Combine(docPath, fileName);

            string fullName = $"{txtLastName.Text}, {txtFirstName.Text}, {txtMI.Text}.";
            string formattedBirthday = dtpBirthday.Value.ToString("yyyy-MM-dd");

            string[] fileContent = new string[]
            {
                $"Student No.: {studentNo}",
                $"Full Name: {fullName}",
                $"Program: {cbProgram.Text}",
                $"Gender: {cbGender.Text}",
                $"Age: {txtAge.Text}",
                $"Birthday: {formattedBirthday}",
                $"Contact No.: {txtContactNo.Text}"
            };

            try
            {
                using (StreamWriter outputFile = new StreamWriter(fullPath))
                {
                    foreach (string line in fileContent)
                    {
                        outputFile.WriteLine(line);
                    }
                }

                MessageBox.Show($"Registration successfully saved as {fileName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}