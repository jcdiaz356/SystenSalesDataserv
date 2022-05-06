using Business;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class FrmCategory : Form
    {

        private string NombreAnterior;


        private FrmLoading FrmLoading;


        public FrmCategory()
        {
            InitializeComponent();
        }

        private  void FrmCategory_Load(object sender, EventArgs e)

        {
          //  Show();
            this.List();
          //  Hide();
        }

        private  void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
               // string Rpta = "";

                if (TxtNombre.Text == string.Empty)
                {
                    this.MessageError("Ingrese el nombre");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre");

                }
                else
                {
                    Task<string> Rpta =   BCategory.Insert(0,TxtNombre.Text,TxtDescripcion.Text);
                    if (Rpta.Equals("OK"))
                    {
                        this.MessageOk("Se inserto correctamente los dats");
                        this.Clear();
                        this.List();
                    } 
                    else
                    {

                        DialogResult dialogResult = MessageBox.Show( Rpta.Result, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);

                      //  this.MessageError(Rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Clear();
            TabGeneral.SelectedIndex = 0;

        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                this.Clear();
                BtnActualizar.Visible = true;
                BtnGuardar.Visible = false;

                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);

                NombreAnterior = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Descripcion"].Value);

                TabGeneral.SelectedIndex = 1;
            } catch (Exception ex)
            {


                MessageBox.Show("Seleccionar desde la celda nombre");
            }
            
        }
 
        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";

                if (TxtId.Text == string.Empty || TxtNombre.Text == string.Empty)
                {
                    this.MessageError("Ingrese el nombre");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre");

                }
                else
                {
                    //Rpta = BCategory.Update(Convert.ToInt32(TxtId.Text), NombreAnterior , TxtNombre.Text, TxtDescripcion.Text);
                    //if (Rpta.Equals("OK"))
                    //{
                    //    this.MessageOk("Se inserto correctamente los dats");
                    //    this.Clear();
                    //    this.List();
                    //}
                    //else
                    //{

                    //    this.MessageError(Rpta);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

  
        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                BtnActivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                BtnActivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
            }
        }

        //*********************************
        // Metodos Personale----------------
        //***********************************

        private async void List()
        {
            try
            {


                PbrLoading.Value = 100;
                LblLoading.Text = "Cargando categorias, espere un momento ...";


                Task<DataTable> result = Business.BCategory.List();

                DgvListado.DataSource = await result;
                this.Formato();
                this.Clear();
                LblTotal.Text = "Total rfegistros: " + Convert.ToString(DgvListado.Rows.Count) ;

                PbrLoading.Value = 0;
                LblLoading.Text = "";

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }
        }
        private void Formato()
        {
            try
            {

                DgvListado.Columns[0].Visible = false;
                DgvListado.Columns[1].Visible = false;
                DgvListado.Columns[2].Width = 150;
                DgvListado.Columns[3].Width = 400;
                DgvListado.Columns[3].HeaderText = "Description";
                 

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }
        }

        private void Buscar()
        {
            try
            {

                DgvListado.DataSource = Business.BCategory.Search(TxtBuscar.Text);
                this.Formato();
                LblTotal.Text = "Total rfegistros: " + Convert.ToString(DgvListado.Rows.Count);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }
        }

        private void Clear()
        {
            TxtBuscar.Clear();
            TxtId.Clear();
            TxtNombre.Clear();
            TxtDescripcion.Clear();
            BtnGuardar.Visible = true;
            ErrorIcono.Clear();
            BtnActualizar.Visible = false;


            DgvListado.Columns[0].Visible = false;
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;

        }

      
        private void MessageError(String message)
        {
            MessageBox.Show(message, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MessageOk(String message)
        {
            MessageBox.Show(message, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas eliminar el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Id;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Id = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = BCategory.Delete(Id);

                            if (Rpta.Equals("OK"))
                            {
                                this.MessageOk("Se eliminó el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MessageError(Rpta);
                            }
                        }
                    }
                    this.List();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas activar el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int id;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            id = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = BCategory.Active(id);

                            if (Rpta.Equals("OK"))
                            {
                                this.MessageOk("Se activó el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MessageError(Rpta);
                            }
                        }
                    }
                    this.List();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas activar el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Id;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Id = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = BCategory.Desactive(Id);

                            if (Rpta.Equals("OK"))
                            {
                                this.MessageOk("Se activó el registro: " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MessageError(Rpta);
                            }
                        }
                    }
                    this.List();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        //public void Show()
        //{

        //    FrmLoading = new FrmLoading();
        //    FrmLoading.ShowDialog();


        //}

        //public void Hide()
        //{

        //    if (FrmLoading != null)
        //        FrmLoading.Close();
              
        //}


    }
}
