using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Atividades
{
    public partial class frmPrincipal : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public frmPrincipal()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            habilitarAdicionar(true);
            txtAtividade.Focus();
        }

        public void frmPrincipal_Load(object sender, EventArgs e)
        {
            importar();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            pnlPrincipal.Visible = true;
            pnlFeitos.Visible = false;
        }

        private void btnFeitos_Click(object sender, EventArgs e)
        {
            pnlFeitos.Visible = true;
        }

        private void btnConcluir_Click(object sender, EventArgs e)
        {
            

            if ((txtAtividade.Text != "") && (txtDescricao.Text != "") && (cbxPrioridade.Text != ""))
            {
                lstTudo.Items.Add(txtAtividade.Text.Replace("$","S") + "$" 
                    + txtDescricao.Text.Replace("$", "S") + "$" 
                    + cbxPrioridade.Text.Replace("$", "S"));
                filtrar();
                salvar();
                habilitarAdicionar(false);
                limparCampos();
            }
            else
            {
                MessageBox.Show(
                    "Preencha todos os campos",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtAtividade.Focus();
            }
        }

        private void habilitarAdicionar(bool habilitado)
        {
            pnlAdicionar.Visible = habilitado;
            pnlMenu.Enabled = (!habilitado);
            btnAdicionar.Enabled = (!habilitado);
            btnExcluir.Enabled = (!habilitado);
            btnFeito.Enabled = (!habilitado);
            lstMedia.Enabled = (!habilitado);
            lstUrgente.Enabled = (!habilitado);
            lstBaixa.Enabled = (!habilitado);
        }

        private void limparCampos()
        {
            txtAtividade.Clear();
            txtDescricao.Clear();
            cbxPrioridade.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            habilitarAdicionar(false);
            limparCampos();
        }

        private void salvar()
        {
            FileInfo salvarDados = new FileInfo(@".\dados.txt");
            if (!salvarDados.Exists)
            {
                using (StreamWriter sd = salvarDados.CreateText())
                {
                    for (int i = 0; i < lstTudo.Items.Count; i++)
                    {
                        sd.WriteLine(lstTudo.Items[i].ToString());
                    }
                }
            }
            else
            {
                using (StreamWriter sa = salvarDados.CreateText())
                {
                    for (int i = 0; i < lstTudo.Items.Count; i++)
                    {
                        sa.WriteLine(lstTudo.Items[i].ToString());
                    }
                }
            }

            FileInfo salvarDadosUrgente = new FileInfo(@".\dadosurgente.txt");
            if (!salvarDadosUrgente.Exists)
            {
                using (StreamWriter sdu = salvarDadosUrgente.CreateText())
                {
                    for (int i = 0; i < lstUrgente.Items.Count; i++)
                    {
                        sdu.WriteLine(lstUrgente.Items[i].ToString());
                    }
                }
            }
            else
            {
                using (StreamWriter sdu = salvarDadosUrgente.CreateText())
                {
                    for (int i = 0; i < lstUrgente.Items.Count; i++)
                    {
                        sdu.WriteLine(lstUrgente.Items[i].ToString());
                    }
                }
            }

            FileInfo salvarDadosMedio = new FileInfo(@".\dadosmedio.txt");
            if (!salvarDadosMedio.Exists)
            {
                using (StreamWriter sdm = salvarDadosMedio.CreateText())
                {
                    for (int i = 0; i < lstMedia.Items.Count; i++)
                    {
                        sdm.WriteLine(lstMedia.Items[i].ToString());
                    }
                }
            }
            else
            {
                using (StreamWriter sdm = salvarDadosMedio.CreateText())
                {
                    for (int i = 0; i < lstMedia.Items.Count; i++)
                    {
                        sdm.WriteLine(lstMedia.Items[i].ToString());
                    }
                }
            }

            FileInfo salvarDadosBaixo = new FileInfo(@".\dadosbaixo.txt");
            if (!salvarDadosBaixo.Exists)
            {
                using (StreamWriter sdb = salvarDadosBaixo.CreateText())
                {
                    for (int i = 0; i < lstBaixa.Items.Count; i++)
                    {
                        sdb.WriteLine(lstBaixa.Items[i].ToString());
                    }
                }
            }
            else
            {
                using (StreamWriter sdb = salvarDadosBaixo.CreateText())
                {
                    for (int i = 0; i < lstBaixa.Items.Count; i++)
                    {
                        sdb.WriteLine(lstBaixa.Items[i].ToString());
                    }
                }
            }
        }

        private void importar()
        {
            FileInfo abrirDados = new FileInfo(@".\dados.txt");
            if (abrirDados.Exists)
            {
                using (StreamReader ad = abrirDados.OpenText())
                {
                    lstTudo.Items.Clear();
                    string linha = "";
                    while ((linha = ad.ReadLine()) != null)
                    {
                        lstTudo.Items.Add(linha.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "O arquivo" + abrirDados + " não foi encontrado",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            FileInfo abrirDadosUrgente = new FileInfo(@".\dadosurgente.txt");
            if (abrirDadosUrgente.Exists)
            {
                using (StreamReader adu = abrirDadosUrgente.OpenText())
                {
                    lstUrgente.Items.Clear();
                    string linha = "";
                    while ((linha = adu.ReadLine()) != null)
                    {
                        lstUrgente.Items.Add(linha.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "O arquivo" + abrirDados + " não foi encontrado",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            FileInfo abrirDadosMedio = new FileInfo(@".\dadosmedio.txt");
            if (abrirDadosMedio.Exists)
            {
                using (StreamReader adm = abrirDadosMedio.OpenText())
                {
                    lstMedia.Items.Clear();
                    string linha = "";
                    while ((linha = adm.ReadLine()) != null)
                    {
                        lstMedia.Items.Add(linha.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "O arquivo" + abrirDados + " não foi encontrado",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            FileInfo abrirDadosBaixo = new FileInfo(@".\dadosbaixo.txt");
            if (abrirDadosBaixo.Exists)
            {
                using (StreamReader adb = abrirDadosBaixo.OpenText())
                {
                    lstBaixa.Items.Clear();
                    string linha = "";
                    while ((linha = adb.ReadLine()) != null)
                    {
                        lstBaixa.Items.Add(linha.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "O arquivo" + abrirDados + " não foi encontrado",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void filtrar()
        {
            if (cbxPrioridade.Text == "Urgente")
            {
                lstUrgente.Items.Add(txtAtividade.Text.Replace("$", "S"));
            }else if (cbxPrioridade.Text == "Média")
            {
                lstMedia.Items.Add(txtAtividade.Text.Replace("$", "S"));
            }else if(cbxPrioridade.Text == "Baixa")
            {
                lstBaixa.Items.Add(txtAtividade.Text.Replace("$", "S"));
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void lstBaixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBaixa.SelectionMode = SelectionMode.One;
            lstMedia.SelectionMode = SelectionMode.None;
            lstUrgente.SelectionMode = SelectionMode.None;
        }

        private void lstMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBaixa.SelectionMode = SelectionMode.None;
            lstMedia.SelectionMode = SelectionMode.One;
            lstUrgente.SelectionMode = SelectionMode.None;
        }

        private void lstUrgente_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstMedia.SelectionMode = SelectionMode.None;
            lstBaixa.SelectionMode = SelectionMode.None;
            lstUrgente.SelectionMode = SelectionMode.One;
        }

        /*private void excluir()
        {

            if (lstAgenda.SelectedIndex >= 0)
            {
                if (MessageBox.Show(
                    "Deseja realmente excluir o item selecionado?",
                    "Informação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    lstAgenda.Items.RemoveAt(lstAgenda.SelectedIndex);
                    btnAtualizar.Text = "Atualizar";
                }
            }
            else
            {
                MessageBox.Show(
                    "Selecione um registro na lista",
                    "Informação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }*/
    }
}
