using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Mango.Setup.ViewModels.Upload;
using System.IO;
using Mango.Infrastructure.Models;

namespace Mango.Setup.Views
{
    public partial class UploadInpsView : UserControl
    {
        private UploadInpsViewModel viewModel;

        public UploadInpsView(UploadInpsViewModel _viewModel)
        {
            InitializeComponent();
            DataContext = _viewModel;
            viewModel = _viewModel;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel.InpsType == null || viewModel.InpsType.Id <= 0)
                {
                    Utility.DisplayMessage("No INPS Type selected! Please select one.");
                    return;
                }

                //show the file browser dialog to select file
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Filter = "Excel 2003 files (*.xls)|*.xls|Excel 2007 files (*.xlsx)|*.xlsx";

                if (dialog.ShowDialog() == true)
                {
                    if (dialog.File != null && dialog.File.Name != "")
                    {
                        //check the file size. It shoud be less than 1MB
                        if (dialog.File.Length < 1048576)
                        {
                            FileStream stream = dialog.File.OpenRead();
                            byte[] fileByte = new byte[stream.Length];
                            stream.Read(fileByte, 0, fileByte.Length);
                            stream.Dispose();
                            stream.Close();

                            txtfileName.Text = dialog.File.Name;

                            viewModel.ReadInpsExcelFile(dialog.File.Name, fileByte, viewModel.InpsType);
                        }
                        else
                        {
                            Utility.DisplayMessage("Max file size is 1 MB");
                        }
                    }
                    else
                    {
                        Utility.DisplayMessage("Please select file to upload");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}
