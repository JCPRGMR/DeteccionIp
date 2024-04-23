using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace DeteccionIp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnIdetificar_Click(object sender, EventArgs e)
        {
            string ipString = txtIp.Text;

            if (IPAddress.TryParse(ipString, out IPAddress ip))
            {
                // Determinar si es válida y de qué clase es
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    byte[] bytes = ip.GetAddressBytes();
                    if (bytes[0] >= 1 && bytes[0] <= 126)
                    {
                        txtCategoria.Text = "A";
                        txtTipo.Text = (bytes[0] == 10 ? "privada" : "pública");
                    }
                    else if (bytes[0] >= 128 && bytes[0] <= 191)
                    {
                        txtCategoria.Text = "B";
                        txtTipo.Text = (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31 ? "privada" : "pública");
                    }
                    else if (bytes[0] >= 192 && bytes[0] <= 223)
                    {
                        txtCategoria.Text = "C";
                        txtTipo.Text = (bytes[0] == 192 && bytes[1] == 168 ? "privada" : "pública");
                    }
                    else
                    {
                        lblError.Text = "La IP no pertenece a ninguna clase conocida";
                    }
                }
                else
                {
                    lblError.Text = "La IP no es una dirección IPv4 válida";
                }
            }
            else
            {
                lblError.Text = "La cadena proporcionada no es una dirección IP válida";
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            txtCategoria.Clear();
            txtIp.Clear();
            txtTipo.Clear();
        }
    }
}
