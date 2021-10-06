using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atividades
{
    public partial class frmInicializacao : Form
    {
        int contador = 0;
        public frmInicializacao()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (contador < 150)
            {
                contador++;
            }
            else if (this.Opacity > 0)
            {
                this.Opacity -= 0.04;
            }
            else
            {
                this.Close();
            }
        }
    }
}
