using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Rekenmachine
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            Display.Focus();
        }

        #region public Variables
        Double Number_A;
        Double Number_B;
        Double X;
        bool Number_BInput = false;
        bool Decimal_Input = false;
        bool Is_Input = false;
        String Calculator;
        public int? Memory = 0;
        #endregion

        #region Display
        //Only numbers allowed as input textbox
        private void NumbersOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        #region Buttons [0-9]

        //insert number zero when clicking 0 button 
        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("0");
        }

        //insert number one when clicking 1 button 
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("1");
        }

        //insert number two when clicking 2 button 
        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("2");
        }

        //insert number three when clicking 3 button 
        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("3");
        }

        //insert number Four when clicking 4 button 
        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("4");
        }

        //insert number five when clicking 5 button 
        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("5");
        }

        //insert number six when clicking 6 button 
        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("6");
        }

        //insert number seven when clicking 7 button 
        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("7");
        }

        //insert number eight when clicking 8 button 
        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("8");
        }

        //insert number nine when clicking 9 button 
        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            InsertNumber("9");
        }
        #endregion

        #region Button Operations

        #region Buttons [CE C]
        private void Button_CE_Click(object sender, RoutedEventArgs e)
        {
            this.Display.Text = string.Empty;
            Number_BInput = false;
            Number_A = 0;
            Number_B = 0;

            Step.Content = "";
            History1.Content = "";
            History2.Content = "";
            History3.Content = "";

        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            this.Display.Text = string.Empty;
            Number_BInput = false;

        }
        #endregion

        #region Buttons [, =]
        private void Button_Komma_Click(object sender, RoutedEventArgs e)
        {
            if (!Decimal_Input)
            {
                Button_Is.Focus();
                InsertNumber(",");
                Decimal_Input = true;
            }
        }

        public void Button_Is_Click(object sender, RoutedEventArgs e)
        {
            Is_Input = true;
            if (X == Number_A)
            {
                Number_B = Double.Parse(Display.Text);
            }

            switch (Calculator)
            {
                case "÷":
                    Display.Text = (Number_A / Number_B).ToString();
                    Number_A = Number_A / Number_B;
                    break;

                case "x":
                    Display.Text = (Number_A * Number_B).ToString();
                    Number_A = Number_A * Number_B;
                    break;

                case "-":
                    Display.Text = (Number_A - Number_B).ToString();
                    Number_A = Number_A - Number_B;
                    break;

                case "+":
                    Display.Text = (Number_A + Number_B).ToString();
                    Number_A = Number_A + Number_B;
                    break;

            }

            if (Euro.IsChecked == true)
            {
                Double Uitkomst = double.Parse(Display.Text);
                var s = string.Format("{0:0.00}", Uitkomst);
                Display.Text = s;

            }

            Step.Content = "";

            if (History1.Content.ToString() == "")
            {
                History1.Content = X.ToString() + " " + Calculator + " " + Number_B.ToString() + " = " + Number_A.ToString();
            }
            else if (History2.Content.ToString() == "")
            {
                History2.Content = X.ToString() + " " + Calculator + " " + Number_B.ToString() + " = " + Number_A.ToString();
            }
            else if (History3.Content.ToString() == "")
            {
                History3.Content = X.ToString() + " " + Calculator + " " + Number_B.ToString() + " = " + Number_A.ToString();
            }
            else
            {
                History1.Content = History2.Content;
                History2.Content = History3.Content;
                History3.Content = X.ToString() + " " + Calculator + " " + Number_B.ToString() + " = " + Number_A.ToString();
            }
        }
        #endregion

        #region Buttons [÷ x - +]
        private void Button_Delen_Click(object sender, RoutedEventArgs e)
        {
            Calculation("÷");
        }

        private void Button_Keer_Click(object sender, RoutedEventArgs e)
        {
            Calculation("x");
        }

        private void Button_Min_Click(object sender, RoutedEventArgs e)
        {
            Calculation("-");
        }

        private void Button_Plus_Click(object sender, RoutedEventArgs e)
        {
            Calculation("+");
        }
        #endregion

        #region Buttons [% €]
        private void Button_Procent_Click(object sender, RoutedEventArgs e)
        {
            if (Number_A != 0)
            {
                Double Percentage = Double.Parse(Display.Text);

                switch (Calculator)
                {
                    case "÷":
                    case "x":
                        Display.Text = (Percentage / 100).ToString();
                        break;

                    case "-":
                    case "+":
                        Display.Text = ((Percentage / 100) * Number_A).ToString();
                        break;
                }
            }
        }

        private void Euro_Checked(object sender, RoutedEventArgs e)
        {
            Toggle.Content = "€";
        }
        private void Euro_Unchecked(object sender, RoutedEventArgs e)
        {
            Toggle.Content = "";
        }
        #endregion

        #endregion

        #region Functions
        //insert number(x) at the right position 
        private void InsertNumber(string value)
        {
            if (Number_BInput)
            {
                this.Display.Text = string.Empty;
            }
            if (Is_Input)
            {
                this.Display.Text = string.Empty;
            }


            //store selectionstart
            var selectionIndex = this.Display.SelectionStart;

            if (Display.SelectedText == "")
            {
                //insert number(x)
                this.Display.Text = this.Display.Text.Insert(this.Display.SelectionStart, value);
            }
            else
            {
                this.Display.SelectedText = "";
                selectionIndex = this.Display.SelectionStart;
                this.Display.Text = this.Display.Text.Insert(this.Display.SelectionStart, value);
            }
            //collect stored selectionstart
            this.Display.SelectionStart = selectionIndex + value.Length;

            Number_BInput = false;
            Is_Input = false;
        }

        public void Calculation(string value)
        {
            X = 0;
            //store selectionstart
            //Textbox_1.Text = Number_A.ToString()
            Number_A = Double.Parse(Display.Text);

            X = Number_A;
            //
            Calculator = value;
            //
            Number_BInput = true;
            Decimal_Input = false;

            Step.Content = Number_A.ToString() + " " + Calculator;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Decimal) || (e.Key == Key.OemComma))
            {
                if (!Decimal_Input)
                {
                    Button_Is.Focus();
                    InsertNumber(",");
                    Decimal_Input = true;
                }
            }

            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    Button_Is.Focus();
                    InsertNumber("0");
                    break;
                case Key.D1:
                case Key.NumPad1:
                    Button_Is.Focus();
                    InsertNumber("1");
                    break;
                case Key.D2:
                case Key.NumPad2:
                    Button_Is.Focus();
                    InsertNumber("2");
                    break;
                case Key.D3:
                case Key.NumPad3:
                    Button_Is.Focus();
                    InsertNumber("3");
                    break;
                case Key.D4:
                case Key.NumPad4:
                    Button_Is.Focus();
                    InsertNumber("4");
                    break;
                case Key.D5:
                case Key.NumPad5:
                    Button_Is.Focus();
                    InsertNumber("5");
                    break;
                case Key.D6:
                case Key.NumPad6:
                    Button_Is.Focus();
                    InsertNumber("6");
                    break;
                case Key.D7:
                case Key.NumPad7:
                    Button_Is.Focus();
                    InsertNumber("7");
                    break;
                case Key.D8:
                case Key.NumPad8:
                    Button_Is.Focus();
                    InsertNumber("8");
                    break;
                case Key.D9:
                case Key.NumPad9:
                    Button_Is.Focus();
                    InsertNumber("9");
                    break;
                case Key.Add:
                    Calculation("+");
                    break;
                case Key.Subtract:
                    Calculation("-");
                    break;
                case Key.Multiply:
                    Calculation("x");
                    break;
                case Key.Divide:
                    Calculation("÷");
                    break;
            }
        }
        #endregion

        #region Memory 
        private void Button_MS_Click(object sender, RoutedEventArgs e)
        {
            Memory = int.Parse(Display.Text);
            MemoryDisplay.Content = Memory;
        }

        private void Button_MR_Click(object sender, RoutedEventArgs e)
        {
            if (Memory != null)
            {
                Display.Text = Memory.ToString();
            }
        }

        private void Button_MPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Memory != null)
            {
                int? DisplayInt = int.Parse(Display.Text);
                DisplayInt = Memory + DisplayInt;
                Display.Text = DisplayInt.ToString();
            }
        }

        private void Button_MMin_Click(object sender, RoutedEventArgs e)
        {
            if (Memory != null)
            {
                int? DisplayInt = int.Parse(Display.Text);
                DisplayInt = Memory - DisplayInt;
                Display.Text = DisplayInt.ToString();
            }
        }
        private void Button_MC_Click(object sender, RoutedEventArgs e)
        {
            Memory = null;
            MemoryDisplay.Content = Memory;
        }
        #endregion
    }
}
