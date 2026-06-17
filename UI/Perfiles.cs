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

        #region Agregar
        private void familiaAlPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPerfiles.CurrentRow == null || dgvFamilias.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione un Perfil de la izquierda y una Familia de la derecha.", "Atención");
                    return;
                }

                int idPerfilPadre = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                int idFamiliaHija = (int)dgvFamilias.CurrentRow.Cells["Id"].Value;

                if (idPerfilPadre == idFamiliaHija)
                {
                    MessageBox.Show("Un perfil no puede contenerse a sí mismo.", "Operación inválida");
                    return;
                }

                _familiaBLL.AgregarFamiliaAPerfil(idPerfilPadre, idFamiliaHija);

                MessageBox.Show("Perfil asignada a Familia con éxito", "Éxito");

                MostrarArbolEnTreeView(idPerfilPadre);

            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Validación");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void permisoAlPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPerfiles.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione el Perfil al que le quiere agregar el permiso.", "Atención");
                    return;
                }

                if (dgvPermisos.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione un Permiso de la lista para asignar.", "Atención");
                    return;
                }

                int idPerfilPadre = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                int idPermisoHijo = (int)dgvPermisos.CurrentRow.Cells["Id"].Value;

                _patenteBLL.AgregarPermisoAPerfil(idPerfilPadre, idPermisoHijo);

                MessageBox.Show("Permiso asignado al perfil con éxito", "Éxito");
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Validación");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerMenuPrincipal());
        }

        #region Eliminar
        private void familiaAlPerfilToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validamos que haya una fila seleccionada en AMBAS grillas
                if (dgvPerfiles.CurrentRow == null || dgvFamilias.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione el Perfil de la izquierda y la Familia de la derecha que desea desvincular.", "Atención");
                    return;
                }

                // 2. Extraemos los IDs ocultos
                int idPerfilPadre = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                int idFamiliaHija = (int)dgvFamilias.CurrentRow.Cells["Id"].Value;

                // 3. Pedimos confirmación antes de disparar a la base de datos
                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro que desea quitar esta familia del perfil seleccionado?",
                    "Confirmar desvinculación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                // Si el usuario hace clic en "Sí", procedemos con el borrado
                if (respuesta == DialogResult.Yes)
                {
                    // 4. Mandamos a la BLL para que ejecute el DELETE en la tabla intermedia
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
                // 1. Instanciamos tu nuevo formulario usando un bloque 'using' para liberar memoria al terminar
                using (FrmCrearPermiso frmPopup = new FrmCrearPermiso())
                {
                    // 2. Abrimos el form. El código se "pausa" acá hasta que el usuario hace clic en Aceptar o Cancelar.
                    DialogResult resultado = frmPopup.ShowDialog();

                    // 3. Verificamos qué botón apretó adentro del popup
                    if (resultado == DialogResult.OK)
                    {
                        // 4. Leemos la propiedad pública que armaste
                        string nombreNuevoPermiso = frmPopup.NombrePermiso;

                        // 5. Mandamos a la BLL
                        _patenteBLL.CrearNuevoPermiso(nombreNuevoPermiso);

                        MessageBox.Show("Permiso creado en el sistema con éxito", "Éxito");

                        // 6. Recargamos la grilla para que se vea
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