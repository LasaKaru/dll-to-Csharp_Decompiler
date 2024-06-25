using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.Metadata;
using System;
using System.IO;
using System.Text.RegularExpressions;


namespace DecompilerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    // Assuming you've chosen and integrated a decompiler library (replace with actual code)
    

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DLL files (*.dll)|*.dll";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string decompiledCode = DecompileDll(filePath);
                    codeTextBox.Text = decompiledCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while decompiling the DLL: " + ex.Message);
                }
            }
        }

        private string DecompileDll(string filePath)
        {
            // Create an instance of the decompiler
            var decompiler = new CSharpDecompiler(filePath, new DecompilerSettings());

            // Get the module definition
            var module = new PEFile(filePath);

            // Decompile the module and get the code
            string code = decompiler.DecompileWholeModuleAsString();
            return code;
        }

        private void CleanButton_Click(object sender, RoutedEventArgs e)
        {
            string rawCode = codeTextBox.Text;
            string cleanedCode = CleanDecompiledCode(rawCode);
            codeTextBox.Text = cleanedCode;
        }

        private string CleanDecompiledCode(string code)
        {
            // Example: Remove comments
            code = Regex.Replace(code, @"//.*?$|/\*.*?\*/", "", RegexOptions.Singleline | RegexOptions.Multiline);

            // Example: Remove attributes
            code = Regex.Replace(code, @"\[.*?\]", "", RegexOptions.Singleline);

            // Additional cleaning rules can be added here

            // Trim extra white spaces
            code = Regex.Replace(code, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

            return code;
        }
    }
}