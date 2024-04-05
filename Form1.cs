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

namespace ejemplo_de_problema_real
{
    public partial class Form1 : Form
    {
        private string filePath = "empleados.txt"; 

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            int edad = (int)numEdad.Value;
            string cargo = txtCargo.Text;

            txtNombre.Clear();
            txtCargo.Clear();
           

            string empleado = $"{nombre},{edad},{cargo}";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(empleado);
            }

            MessageBox.Show("Empleado agregado correctamente.");
        }

      

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            string nombreBuscar = txtBuscar.Text;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] datos = line.Split(',');

                    if (datos[0] == nombreBuscar)
                    {
                        MessageBox.Show($"Nombre: {datos[0]}\nEdad: {datos[1]}\nCargo: {datos[2]}");
                        return;
                    }
                }
            }
            txtBuscar.Clear();

            MessageBox.Show("Empleado no encontrado.");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string nombreEliminar = txtEliminar.Text;

            string tempFile = Path.GetTempFileName();

            using (StreamReader reader = new StreamReader(filePath))
            using (StreamWriter writer = new StreamWriter(tempFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] datos = line.Split(',');

                    if (datos[0] != nombreEliminar)
                    {
                        writer.WriteLine(line);
                    }
                }
            }

            txtEliminar.Clear();

            File.Delete(filePath);
            File.Move(tempFile, filePath);

            MessageBox.Show("Empleado eliminado correctamente.");
        }
    }
    
}
