using System;
using System.Collections.Generic;
using System.Windows;

using System.Text.RegularExpressions;

namespace WhatIsGigabyte
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const ulong _KB = 1000;
        const ulong _MB = 1000 * _KB;
        const ulong _GB = 1000 * _MB;
        const ulong _TB = 1000 * _GB;

        List<StuffWithSize> information_Stuff = new List<StuffWithSize>();

        public MainWindow()
        {
            InitializeComponent();

            information_Stuff.Add(new StuffWithSize() { Name = "Текстовые символы", Size = 1 });
            information_Stuff.Add(new StuffWithSize() { Name = "Фотографии", Size = 2 * _MB });
            information_Stuff.Add(new StuffWithSize() { Name = "Минут Skype (аудио)", Size = 570 * _KB });  //https://support.skype.com/ru/faq/FA1417/kakaa-skorost-internet-soedinenia-nuzna-dla-raboty-skype
            information_Stuff.Add(new StuffWithSize() { Name = "Минут Skype (аудио + видео)", Size = 2000 * _KB });  //https://support.skype.com/ru/faq/FA1417/kakaa-skorost-internet-soedinenia-nuzna-dla-raboty-skype
            information_Stuff.Add(new StuffWithSize() { Name = "Минут аудио (CD качество)", Size = 10584 * _KB });            //wiki
            information_Stuff.Add(new StuffWithSize() { Name = "CD диски", Size = 700 * _MB });
            information_Stuff.Add(new StuffWithSize() { Name = "DVD диски", Size = 4 * _GB + 700 * _MB });
        }


        public class StuffWithSize
        {
            public string Name;
            public ulong Size; //Bytes
        }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            var digits_Pattern = @"^\d+$";

            if (TextBox_MB.Text == "")
                TextBox_MB.Text = "0";

            if (TextBox_GB.Text == "")
                TextBox_GB.Text = "0";

            if (TextBox_TB.Text == "")
                TextBox_TB.Text = "0";

            var all_Input = TextBox_MB.Text + TextBox_GB.Text + TextBox_TB.Text;

            if (!Regex.IsMatch(all_Input, digits_Pattern))
            {
                TextBox_Result.Text = "Проверьте правильность ввода. Допустимы только числа, начиная с нуля.";
                return;
            }

            try
            {            
                ulong total_Bytes = ulong.Parse(TextBox_MB.Text) * _MB + ulong.Parse(TextBox_GB.Text) * _GB + ulong.Parse(TextBox_TB.Text) * _TB;

                TextBox_Result.Text = "";
                foreach(StuffWithSize stuff in information_Stuff)
                {
                    if (total_Bytes % (double)stuff.Size == 0)
                        TextBox_Result.Text += String.Format("{0}: {1:N0}", stuff.Name, total_Bytes / (double)stuff.Size) + "\n";
                    else
                        TextBox_Result.Text += String.Format("{0}: {1:N2}", stuff.Name, total_Bytes / (double)stuff.Size) + "\n";
                }
            }
            catch
            {
                TextBox_Result.Text = "В данной версии программы такие большие размеры не поддерживаются.";
            }
        }
    }
}
