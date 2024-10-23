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

namespace SistemaCadastro
{
    public partial class Sistema : Form
    {

        public Sistema()
        {
            InitializeComponent();
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            marcador.Height = btnCadastra.Height;
            marcador.Top = btnCadastra.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }
        

        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcador.Height = btnBusca.Height;
            marcador.Top = btnBusca.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }







        private void Sistema_Load(object sender, EventArgs e)
        {

            listaCBGeneros();
            clear();
            listaBandas();
            listaAlteraGenero();
            
        }

        public void listaCBGeneros()
        {
            ConectaBanco con = new ConectaBanco();
            DataTable tabelaDados = new DataTable();
            tabelaDados = con.listaGeneros();
            cbGenero.DataSource = tabelaDados;
            cbGenero.DisplayMember = "genero";
            cbGenero.ValueMember = "idgenero";
        }
        public void listaAlteraGenero()
        {
            ConectaBanco con = new ConectaBanco();
            DataTable tabelaDados = new DataTable();
            tabelaDados = con.listaGeneros();
            cbAlteraGenero.DataSource = tabelaDados;
            cbAlteraGenero.DisplayMember = "genero";
            cbAlteraGenero.ValueMember = "idgenero";
        }




        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            (dgBandas.DataSource as DataTable).DefaultView.RowFilter = string.Format("nome like '{0}%'", txtBusca.Text);
        }

        private void btnRemoveBanda_Click(object sender, EventArgs e)
        {
            int linha = dgBandas.CurrentRow.Index;
            int id = Convert.ToInt32(dgBandas.Rows[linha].Cells["idbandas"].Value.ToString());
            DialogResult resp = MessageBox.Show("Tem certeza que deseja excluir?",
                "Remove Banda", MessageBoxButtons.OKCancel);

            if (resp == DialogResult.OK)
            {
                ConectaBanco con = new ConectaBanco();

                bool retorno = con.deletaBanda(id);
                if (retorno == true)
                {
                    MessageBox.Show("banda deletada com sucesso");
                    listaBandas();
                }
                else
                {
                    MessageBox.Show(con.mensagem);
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int linha = dgBandas.CurrentRow.Index;
            int id = Convert.ToInt32(dgBandas.Rows[linha].Cells["idbandas"].Value.ToString());
            ConectaBanco con = new ConectaBanco();
            Banda b = new Banda();

            txtAlteraNome.Text = dgBandas.Rows[linha].Cells["nome"].Value.ToString();
            cbAlteraGenero.DisplayMember = dgBandas.Rows[linha].Cells["genero"].Value.ToString();
            txtAlteraIntegrantes.Text = dgBandas.Rows[linha].Cells["integrantes"].Value.ToString();


        }

         private void btnConfirmaAlteracao_Click(object sender, EventArgs e)
        {
           


        }

        private void bntAddGenero_Click(object sender, EventArgs e)
        {
          
        }
        public void clear()
        {
            txtnome.Clear();
            txtranking.Clear();
            cbGenero.Text = "";
            txtintegrantes.Clear();
            txtnome.Focus();
        }

        private void BtnConfirmaCadastro_Click(object sender, EventArgs e)
        {
            ConectaBanco conection = new ConectaBanco();
            Banda newBanda = new Banda();

            newBanda.Nome = txtnome.Text;
            newBanda.Integrantes = Convert.ToInt32(txtintegrantes.Text);
            newBanda.Genero = Convert.ToInt32( cbGenero.SelectedValue.ToString());
            newBanda.Ranking = Convert.ToInt32(txtranking.Text);

             bool retorno = conection.insereBanda(newBanda);

            if (!retorno) {
                MessageBox.Show(conection.mensagem);
            }

            clear();
            listaBandas();

        }
        public void listaBandas()
        {
            ConectaBanco con = new ConectaBanco();
            DataTable dt = new DataTable();
            dt = con.listaBandas();
            dgBandas.DataSource = dt;
            dgBandas.Columns["idbandas"].Visible = false;
        }

        private void dgBandas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void cbAlteraGenero_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void txtAlteraNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAlteraIntegrantes_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAlteraRanking_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabAlterar_Click(object sender, EventArgs e)
        {

        }
    }
}
