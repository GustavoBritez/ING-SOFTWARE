using BLL.Perfiles;
using Microsoft.VisualBasic;
using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI
{
    public partial class Perfiles : Form
    {
        private FamiliaBLL _familiaBLL;
        private PatenteBLL _patenteBLL;

        public Perfiles()
        {
            InitializeComponent();
            _familiaBLL = new FamiliaBLL();
            _patenteBLL = new PatenteBLL();

            dgvFamilias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPerfiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPermisos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvFamilias.MultiSelect = false;
            dgvPerfiles.MultiSelect = false;
            dgvPermisos.MultiSelect = false;

            dgvFamilias.ReadOnly = true;
            dgvPerfiles.ReadOnly = true;
            dgvPermisos.ReadOnly = true;

        }

        private void Perfiles_Load(object sender, EventArgs e)
        {
            CargarGrillas();
        }

        private void CargarGrillas()
        {
            try
            {
                List<Componente> listaFamilias = _familiaBLL.ObtenerFamiliasPerfil();

                dgvPerfiles.DataSource = null;
                dgvPerfiles.DataSource = new List<Componente>(listaFamilias);

                dgvPerfiles.Columns["Id"].Visible = false;
                dgvPerfiles.Columns["Nombre"].HeaderText = "Nombre del Perfil";

                dgvFamilias.DataSource = null;
                dgvFamilias.DataSource = new List<Componente>(listaFamilias);

                dgvFamilias.Columns["Id"].Visible = false;
                dgvFamilias.Columns["Nombre"].HeaderText = "Nombre de Familia";

                List<Componente> listaPermisos = _patenteBLL.ObtenerPermisosPerfil();

                dgvPermisos.DataSource = null;
                dgvPermisos.DataSource = new List<Componente>(listaPermisos);
                dgvPermisos.Columns["Id"].Visible = false;
                dgvPermisos.Columns["Nombre"].HeaderText = "Acciones / Permisos";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());
        }

        #region Eliminar
        private void familiaAlPerfilToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPerfiles.CurrentRow == null || dgvFamilias.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione el Perfil de la izquierda y la Familia de la derecha que desea desvincular.", "Atención");
                    return;
                }

                int idPerfilPadre = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                int idFamiliaHija = (int)dgvFamilias.CurrentRow.Cells["Id"].Value;

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro que desea quitar esta familia del perfil seleccionado?",
                    "Confirmar desvinculación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    _familiaBLL.EliminarFamiliaPerfil(idPerfilPadre, idFamiliaHija);

                    MessageBox.Show("¡Familia desvinculada del perfil con éxito!", "Éxito");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la base de datos: " + ex.Message, "Error Crítico");
            }
        }
        private void permisoAlPerfilToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPerfiles.CurrentRow == null || dgvPermisos.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione el Perfil de la izquierda y el Permiso que desea desvincular.", "Atención");
                    return;
                }

                int idPerfilPadre = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                int idPermisoHijo = (int)dgvPermisos.CurrentRow.Cells["Id"].Value;

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro que desea quitar este permiso del perfil seleccionado?",
                    "Confirmar desvinculación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    _patenteBLL.EliminarPermisoPerfil(idPerfilPadre, idPermisoHijo);

                    MessageBox.Show("Permiso desvinculado del perfil con éxito", "Éxito");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Arbol visual
        private void MostrarArbolEnTreeView(int idFamiliaSeleccionada)
        {
            try
            {
                Vista_Familia.Nodes.Clear();

                FamiliaServices familiaRaiz = _familiaBLL.ObtenerArbolFamiliar(idFamiliaSeleccionada);

                if (familiaRaiz == null) return;

                TreeNode nodoRaiz = new TreeNode(familiaRaiz.Nombre);

                nodoRaiz.Tag = familiaRaiz.Id;

                DibujarNodosFamiliasRecursivo(familiaRaiz, nodoRaiz);

                Vista_Familia.Nodes.Add(nodoRaiz);
                Vista_Familia.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al graficar el árbol: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DibujarNodosFamiliasRecursivo(FamiliaServices familiaPadre, TreeNode nodoVisualPadre)
        {
            foreach (Componente hijo in familiaPadre.Hijos)
            {
                if (hijo.EsCompuesto())
                {
                    TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                    nodoHijo.Tag = hijo.Id;

                    FamiliaServices subFamilia = (FamiliaServices)hijo;

                    DibujarNodosFamiliasRecursivo(subFamilia, nodoHijo);

                    nodoVisualPadre.Nodes.Add(nodoHijo);
                }
            }
        }

        private void dgvFamilias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFamilias.CurrentRow != null)
            {
                int idFamilia = (int)dgvFamilias.CurrentRow.Cells["Id"].Value;
                MostrarArbolEnTreeView(idFamilia);
            }
        }

        #endregion

        private void permisoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmCrearPermiso frmPopup = new FrmCrearPermiso())
                {
                    DialogResult resultado = frmPopup.ShowDialog();

                    if (resultado == DialogResult.OK)
                    {
                        string nombreNuevoPermiso = frmPopup.NombrePermiso;

                        _patenteBLL.CrearNuevoPermiso(nombreNuevoPermiso);

                        MessageBox.Show("Permiso creado en el sistema con éxito", "Éxito");

                        CargarGrillas();
                    }
                }
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Validación");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el permiso: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}