using System;
using System.IO;
using System.Windows.Forms;

namespace TextFile
{
    public partial class FrmLab1 : Form
    {
        public FrmLab1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string getInput = txtInput.Text;

            FrmFileName frmFileName = new FrmFileName();
            frmFileName.ShowDialog();

            if (!string.IsNullOrEmpty(FrmFileName.SetFileName))
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fullPath = Path.Combine(docPath, FrmFileName.SetFileName);

                try
                {
                    using (StreamWriter outputFile = new StreamWriter(fullPath))
                    {
                        outputFile.WriteLine(getInput);
                        Console.WriteLine(getInput);
                    }

                    MessageBox.Show($"File '{FrmFileName.SetFileName}' successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    FrmFileName.SetFileName = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
