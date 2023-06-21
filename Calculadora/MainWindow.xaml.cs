using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Calculadora.MainWindow;

namespace Calculadora
{
    public partial class MainWindow : Window
    {
        //PUNTO DECIMAL
        string DecimalSeparator => CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        decimal PrimerValor { get; set; }
        decimal? SegundoValor { get; set; }

        IOperacion OperacionActual;

        public MainWindow()
        {
            InitializeComponent();
            btnPunto.Content = DecimalSeparator;
            btnSuma.Tag = new Sumar();
            btnResta.Tag = new Restar();
            btnDivision.Tag = new Dividir();
            btnMultiplicacion.Tag = new Multiplicar();
            btnModulo.Tag = new Modulo();
        }

        //ACCION BOTON 0,1,2,3,4,5,6,7,8,9 (BORRAR EL CERO QUE ESTA A LA IZQUIERDA CUANDO SE INGRESA EL PRIMER VALOR)
        private void botonClick(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text == "0")
                txtInput.Text = "";
                txtInput.Text = $"{txtInput.Text}{((Button)sender).Content}";
        }

        //BOTON PUNTO
        private void btnPunto_Click(object sender, RoutedEventArgs e)
        {
            if (!txtInput.Text.Contains(","))
            { 
                txtInput.Text += ",";
            }
        }

        //BOTON RETROCEDER
        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text == "0")
                return;

            txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length - 1);
            if (txtInput.Text == "")
                txtInput.Text = "0";
        }
        //BOTONES LIMPIAR 
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
           => txtInput.Text = "0";

        private void btnLimpiarTodo_Click(object sender, RoutedEventArgs e)
        {
            PrimerValor = 0;
            OperacionActual = null;
            txtInput.Text = "0";
        }
         
        //BOTON OPERACION BASICA (SUMA,RESTA,MULTIPLICACION,DIVISION)
        private void BotonOperacion_Click(object sender, RoutedEventArgs e)
        {
            if (OperacionActual == null)
                PrimerValor = Convert.ToDecimal(txtInput.Text);

                OperacionActual = (IOperacion)((Button)sender).Tag;
                SegundoValor = null;
                txtInput.Text = "";
        }

        //INTERFAZ VALOR 1, VALOR 2
        public interface IOperacion
        {
            decimal HacerOperacion(decimal val1, decimal val2);
        }

        //OPERECIONES (INTERFAZ)
        public class Sumar : IOperacion
        {
            public decimal HacerOperacion(decimal val1, decimal val2) => val1 + val2;
        }
        public class Restar : IOperacion
        {
            public decimal HacerOperacion(decimal val1, decimal val2) => val1 - val2;
        }
        public class Dividir : IOperacion
        {
            public decimal HacerOperacion(decimal val1, decimal val2) => val1 / val2;
        }
        public class Multiplicar : IOperacion
        {
            public decimal HacerOperacion(decimal val1, decimal val2) => val1 * val2;
        }
        public class Modulo : IOperacion
        {
            public decimal HacerOperacion(decimal val1, decimal val2) => val1 % val2;
        }

        //BOTON IGUAL
        private void btnIgual_Click(object sender, RoutedEventArgs e)
        {
            if (OperacionActual == null)
                return;
            if (txtInput.Text == "")
                return;

            decimal val2 = SegundoValor ?? Convert.ToDecimal(txtInput.Text);
            { 
            txtInput.Text = (PrimerValor = OperacionActual.HacerOperacion(PrimerValor, (decimal)(SegundoValor = val2))).ToString();
            }
        }
        //BOTON CAMBIO SIGNO
        private void btnSigno_Click(object sender, RoutedEventArgs e)
        {
            PrimerValor = Convert.ToDecimal(txtInput.Text);
            PrimerValor *= -1;
            txtInput.Text = PrimerValor.ToString();
        }

        //BOTON AL CUADRADO
        private void botonCuadrado(object sender, RoutedEventArgs e)
        {
            PrimerValor = Convert.ToDecimal(txtInput.Text);
            PrimerValor = PrimerValor * PrimerValor;
            txtInput.Text = PrimerValor.ToString();
        }

        //RAIZ CUADRADA
        private void botonRaiz(object sender, RoutedEventArgs e)
        {
            PrimerValor = Convert.ToDecimal(txtInput.Text);
            PrimerValor = (decimal)Math.Sqrt((double)PrimerValor);
            txtInput.Text = PrimerValor.ToString();
        }
    }
}
