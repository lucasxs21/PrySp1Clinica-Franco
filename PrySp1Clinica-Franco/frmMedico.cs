using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace PrySp1Clinica_Franco
{
    public partial class frmMedico : Form
    {
        public frmMedico()
        {
            InitializeComponent();
        }
        private const string PATH_ARCHIVO_M = "Medico.txt";


        //metodo para limpiar txt y deshabilitar boton
        private void Inicializar()
        {
            txtMatricula.Text = "";
            txtNombre.Text = "";
            txtIdent.Text = "";
            btnRegistrar.Enabled = false;
        }
        private void frmMedico_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        private bool ValidarDatos()
        {
            bool resultado = false;
            if (txtMatricula.Text != "")
            {
                if (txtNombre.Text != "")
                {
                    if (txtIdent.Text != "")
                    {
                        ClsArchivo medico = new ClsArchivo();
                        medico.NombreArchivo = PATH_ARCHIVO_M;
                        if (medico.BuscarRepetido(txtMatricula.Text) == false)
                        {
                            resultado = true;
                        }
                    }
                }
            }
            return resultado;
        }

        private ClsMedico NuevoMedico()
        {
            ClsMedico nuevomedico = new ClsMedico();
            nuevomedico.matricula = int.Parse(txtMatricula.Text);
            nuevomedico.nombre = txtNombre.Text;
            nuevomedico.especialidad = int.Parse(txtIdent.Text);
            return nuevomedico;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                ClsMedico nuevomedico = NuevoMedico();
                ClsArchivo medicos = new ClsArchivo();
                medicos.NombreArchivo = PATH_ARCHIVO_M;
                medicos.GrabarMedico(nuevomedico);
                Inicializar();
            }
            else
            {
                MessageBox.Show("Datos incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtIdent_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricula.Text != null && txtNombre.Text != null && txtIdent.Text != null)
            {
                btnRegistrar.Enabled = true;
            }
        }

        private void txtMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
