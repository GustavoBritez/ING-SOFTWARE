using BLL;
using BLL.Perfiles;
using DAL;
using Microsoft.VisualBasic;
using Services;
using Services.Perfiles;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI
{
    public partial class Perfiles : Form, IIdiomaObserver
    {
        private FamiliaBLL _familiaBLL;
        private PatenteBLL _patenteBLL;
        private PerfilBLL _perfilBLL;

        private IdiomaBLL idiomaBLL = new IdiomaBLL();

        public Perfiles()
        {
            InitializeComponent();
            ServicesSessionManager.Instancia.Suscribir(this);
            ActualizarIdioma();

            _familiaBLL = new FamiliaBLL();
            _patenteBLL = new PatenteBLL();
            _perfilBLL = new PerfilBLL();

            dgvFamilias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPerfiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPermisos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvFamilias.MultiSelect = false;
            dgvPerfiles.MultiSelect = false;
            dgvPermisos.MultiSelect = false;

            dgvFamilias.ReadOnly = true;
            dgvPerfiles.ReadOnly = true;
            dgvPermisos.ReadOnly = true;
            ConfigurarEstiloGrillas();
        }

        private void Perfiles_Load(object sender, EventArgs e)
        {
            CargarGrillas();
        }

        private void CargarGrillas()
        {
            try
            {
                Idioma idioma = ServicesSessionManager.Instancia.ObtenerIdioma();

                string nombrePerfil = "Nombre del Perfil";
                string nombreFamilia = "Nombre de Familia";
                string nombrePermiso = "Acciones / Permisos";

                switch (idioma.Nombre)
                {
                    case "English":
                        nombrePerfil = "Profile Name";
                        nombreFamilia = "Family Name";
                        nombrePermiso = "Actions / Permissions";
                        break;

                    case "Portugues":
                        nombrePerfil = "Nome do Perfil";
                        nombreFamilia = "Nome da Família";
                        nombrePermiso = "Ações / Permissões";
                        break;
                }
                List<Perfil> listaPerfiles = _perfilBLL.ObtenerPerfiles();

                dgvPerfiles.DataSource = null;
                dgvPerfiles.DataSource = new List<Perfil>(listaPerfiles);
                dgvPerfiles.Columns["Id"].Visible = false;
                dgvPerfiles.Columns["Nombre"].HeaderText = nombrePerfil;

                List<Perfil> listaFamilias = _familiaBLL.ObtenerFamiliasPerfil();

                dgvFamilias.DataSource = null;
                dgvFamilias.DataSource = new List<Perfil>(listaFamilias);
                dgvFamilias.Columns["Id"].Visible = false;
                dgvFamilias.Columns["Nombre"].HeaderText = nombreFamilia;

                List<Perfil> listaPermisos = _patenteBLL.ObtenerPermisosPerfil();

                dgvPermisos.DataSource = null;
                dgvPermisos.DataSource = new List<Perfil>(listaPermisos);
                dgvPermisos.Columns["Id"].Visible = false;
                dgvPermisos.Columns["Nombre"].HeaderText = nombrePermiso;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            FormManager.Navegar(this, FormManager.ObtenerGestionUsuario());
        }

        #region Arbol visual
        private void MostrarArbolEnTreeView_Perfil(int idPerfilSeleccionado)
        {
            try
            {
                Vista_Familia.Nodes.Clear();

                // 1. Buscamos el perfil y todo lo que tiene asignado
                FamiliaServices perfilRaiz = _perfilBLL.ObtenerArbolPerfil(idPerfilSeleccionado);
                if (perfilRaiz == null) return;

                // 2. Creamos el nodo principal
                TreeNode nodoRaiz = new TreeNode("👤 Perfil: " + perfilRaiz.Nombre);
                nodoRaiz.Tag = perfilRaiz.Id;

                // 3. Dibujamos las familias y permisos que cuelgan de este perfil
                DibujarNodosFamiliasRecursivo(perfilRaiz, nodoRaiz);

                // 4. Mostramos en pantalla
                Vista_Familia.Nodes.Add(nodoRaiz);
                Vista_Familia.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al graficar el árbol del perfil: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarArbolEnTreeView_Familia(int idFamiliaSeleccionada)
        {
            try
            {
                Vista_Familia.Nodes.Clear();

                // 1. Buscamos la familia y sus componentes en la BLL
                FamiliaServices familiaRaiz = _familiaBLL.ObtenerArbolFamiliar(idFamiliaSeleccionada);
                if (familiaRaiz == null) return;

                // 2. Creamos el nodo principal
                TreeNode nodoRaiz = new TreeNode("📦 Familia: " + familiaRaiz.Nombre);
                nodoRaiz.Tag = familiaRaiz.Id;

                // 3. Dibujamos directamente los permisos y subfamilias que tiene adentro
                DibujarNodosFamiliasRecursivo(familiaRaiz, nodoRaiz);

                // 4. Mostramos en pantalla
                Vista_Familia.Nodes.Add(nodoRaiz);
                Vista_Familia.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al graficar el árbol de la familia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DibujarNodosFamiliasRecursivo(FamiliaServices familiaPadre, TreeNode nodoVisualPadre)
        {
            foreach (Perfil hijo in familiaPadre.Hijos)
            {
                if (hijo.EsCompuesto())
                {
                    TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                    nodoHijo.Tag = hijo.Id;

                    FamiliaServices subFamilia = (FamiliaServices)hijo;
                    DibujarNodosFamiliasRecursivo(subFamilia, nodoHijo);

                    nodoVisualPadre.Nodes.Add(nodoHijo);
                }
                else
                {
                    TreeNode nodoHoja = new TreeNode(hijo.Nombre);
                    nodoHoja.Tag = hijo.Id;

                    nodoVisualPadre.Nodes.Add(nodoHoja);
                }
            }
        }

        private void dgvFamilias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFamilias.CurrentRow != null)
            {
                int idFamilia = (int)dgvFamilias.CurrentRow.Cells["Id"].Value;

                MostrarArbolEnTreeView_Familia(idFamilia);
            }
        }

        private void dgvPerfiles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPerfiles.CurrentRow != null)
            {
                int idPerfil = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;

                MostrarArbolEnTreeView_Perfil(idPerfil);
            }
        }
        #endregion

        #region Eliminar
        private void Eliminar_PerfilAFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPerfiles.CurrentRow == null || dgvFamilias.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione el Perfil de la izquierda y la Familia de la grilla central que desea desvincular.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPerfil = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                int idFamilia = (int)dgvFamilias.CurrentRow.Cells["Id"].Value;

                string nombreFamilia = dgvFamilias.CurrentRow.Cells["Nombre"].Value.ToString();

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro que desea quitar esta familia del perfil seleccionado?",
                    "Confirmar desvinculación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    _perfilBLL.EliminarPerfilAFamilia(idPerfil, idFamilia, nombreFamilia);

                    MessageBox.Show("¡Familia desvinculada del perfil con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrillas();
                }
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desvincular la familia: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Eliminar_permisoAlPerfil(object sender, EventArgs e)
        {
            try
            {
                if (dgvPerfiles.CurrentRow == null || dgvPermisos.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione el Perfil de la izquierda y el Permiso que desea desvincular.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPerfil = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                int idPermiso = (int)dgvPermisos.CurrentRow.Cells["Id"].Value;

                // Extraemos el NOMBRE del permiso para pasarlo a la BLL
                string nombrePermiso = dgvPermisos.CurrentRow.Cells["Nombre"].Value.ToString();

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro que desea quitar este permiso del perfil seleccionado?",
                    "Confirmar desvinculación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Le pasamos los 3 datos a la BLL
                    _perfilBLL.EliminarPermisoPerfil(idPerfil, idPermiso, nombrePermiso);

                    MessageBox.Show("Permiso desvinculado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrillas();
                }
            }
            catch (ArgumentException argEx)
            {
                // Acá atrapamos nuestra validación si el permiso no estaba asignado
                MessageBox.Show(argEx.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desvincular el permiso: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Eliminar

        #region Agregar
        /// Cambiar
        private void Agregar_Familia_A_Perfil(object sender, EventArgs e)
        {
            try
            {
                // 1. Validamos que haya selecciones en ambas grillas
                if (dgvPerfiles.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione un Perfil de la grilla izquierda.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvFamilias.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione una Familia de la grilla central.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Extraemos los IDs de las celdas seleccionadas
                int idPerfil = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                int idFamilia = (int)dgvFamilias.CurrentRow.Cells["Id"].Value;

                // Extraemos el nombre del perfil para pasarlo a la validación de la BLL que ya tenés armada
                string nombrePerfil = dgvPerfiles.CurrentRow.Cells["Nombre"].Value.ToString();

                // 3. Llamamos al método de la capa de negocio (reutilizamos el que ya existe)
                _perfilBLL.AgregarFamiliaAlPerfil(idPerfil, idFamilia, nombrePerfil);
                // 4. Avisamos al usuario y refrescamos la vista
                MessageBox.Show("¡Familia asignada al perfil con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarGrillas();
            }
            catch (ArgumentException argEx)
            {
                // Atrapa tu validación personalizada si la relación ya existe
                MessageBox.Show(argEx.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Atrapa cualquier otro error inesperado (como caídas de red o base de datos)
                MessageBox.Show("Error al asignar la familia: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Agregar_Permiso_A_Familia(object sender, EventArgs e)
        {
            try
            {
                // 1. Cambiamos a la grilla de FAMILIAS (Centro)
                if (dgvFamilias.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione una Familia de la grilla central.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Grilla de PERMISOS (Derecha)
                if (dgvPermisos.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione un Permiso de la grilla derecha.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Extraemos los IDs y nombres correctos
                int idFamilia = (int)dgvFamilias.CurrentRow.Cells["Id"].Value;
                int idPermiso = (int)dgvPermisos.CurrentRow.Cells["Id"].Value;

                string nombrePermiso = dgvPermisos.CurrentRow.Cells["Nombre"].Value.ToString();
                string nombreFamilia = dgvFamilias.CurrentRow.Cells["Nombre"].Value.ToString();

                // 4. Llamamos al método correcto en la BLL de Familias
                _familiaBLL.AgregarPermisoAFamilia(idFamilia, idPermiso, nombrePermiso, nombreFamilia);

                MessageBox.Show("¡Permiso asignado a la familia con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarGrillas();
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar el permiso a la familia: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Agregar_Permiso(object sender, EventArgs e)
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

        private void Agregar_Perfil(object sender, EventArgs e)
        {
            try
            {
                using (FrmCrearPermiso frmPopup = new FrmCrearPermiso())
                {
                    frmPopup.Text = "Crear Nuevo Perfil";
                    DialogResult resultado = frmPopup.ShowDialog();
                    if (resultado == DialogResult.OK)
                    {
                        string nombreNuevoPerfil = frmPopup.NombrePermiso;

                        _perfilBLL.CrearNuevoPerfil(nombreNuevoPerfil);

                        MessageBox.Show("¡Perfil creado en el sistema con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarGrillas();
                    }
                }
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Validación", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el perfil: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Agregar_Familia(object sender, EventArgs e)
        {
            try
            {
                using (FrmCrearPermiso frmPopup = new FrmCrearPermiso())
                {
                    DialogResult resultado = frmPopup.ShowDialog();

                    if (resultado == DialogResult.OK)
                    {
                        string nombreNuevaFamilia = frmPopup.NombrePermiso;

                        _familiaBLL.CrearNuevaFamilia(nombreNuevaFamilia);

                        MessageBox.Show("Familia creada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarGrillas();
                    }
                }
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Validación", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear la familia: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region GUI
        private void ConfigurarEstiloGrillas()
        {
            // Definimos los colores institucionales que venimos usando
            Color verdeOscuro = Color.FromArgb(46, 94, 67);
            Color verdeSeleccion = Color.FromArgb(180, 210, 190);
            Color fondoGrilla = Color.White;
            Color colorLineas = Color.FromArgb(200, 220, 205);

            DataGridView[] grillas = { dgvPerfiles, dgvFamilias, dgvPermisos };

            foreach (DataGridView dgv in grillas)
            {
                // Hay que apagar esto para que Windows Forms nos deje pintar el encabezado
                dgv.EnableHeadersVisualStyles = false;

                // --- Estilo del Encabezado (Header) ---
                dgv.ColumnHeadersDefaultCellStyle.BackColor = verdeOscuro;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgv.ColumnHeadersHeight = 35;

                // --- Estilo del Fondo y Filas ---
                dgv.BackgroundColor = fondoGrilla;
                dgv.BorderStyle = BorderStyle.None;
                dgv.GridColor = colorLineas;
                dgv.DefaultCellStyle.BackColor = fondoGrilla;
                dgv.DefaultCellStyle.ForeColor = Color.Black;
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

                // --- Estilo de Selección ---
                dgv.DefaultCellStyle.SelectionBackColor = verdeSeleccion;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

                // --- Comportamientos Generales ---
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.MultiSelect = false;
                dgv.ReadOnly = true;
                dgv.RowHeadersVisible = false; // Oculta la columna vacía de la izquierda
                dgv.AllowUserToAddRows = false; // Saca la fila vacía extra del final
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Hace que las columnas ocupen todo el ancho
            }
            #endregion

        }

        private void Eliminar_Permiso_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPermisos.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione un Permiso de la grilla derecha que desea eliminar del sistema.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPermiso = (int)dgvPermisos.CurrentRow.Cells["Id"].Value;
                string nombrePermiso = dgvPermisos.CurrentRow.Cells["Nombre"].Value.ToString();

                // Actualizamos el mensaje para reflejar el borrado en cascada
                DialogResult respuesta = MessageBox.Show(
                    $"¿Está seguro que desea ELIMINAR el permiso '{nombrePermiso}'?\n\nAl hacerlo, también se desvinculará automáticamente de todos los Perfiles y Familias que lo estén utilizando actualmente.",
                    "Confirmar Borrado en Cascada",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    // Ejecutamos la BLL
                    _patenteBLL.EliminarPermiso(idPermiso, nombrePermiso);

                    MessageBox.Show("¡Permiso eliminado del sistema y desvinculado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrillas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el permiso: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Eliminar_Perfil_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validamos que haya algo seleccionado en la grilla izquierda
                if (dgvPerfiles.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione un Perfil de la grilla izquierda que desea eliminar del sistema.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Extraemos ID y Nombre
                int idPerfil = (int)dgvPerfiles.CurrentRow.Cells["Id"].Value;
                string nombrePerfil = dgvPerfiles.CurrentRow.Cells["Nombre"].Value.ToString();

                // 3. Advertencia de cascada
                DialogResult respuesta = MessageBox.Show(
                    $"¿Está seguro que desea ELIMINAR COMPLETAMENTE el perfil '{nombrePerfil}'?\n\nAl hacerlo, se eliminarán todas sus asignaciones de Familias y Permisos. Si hay usuarios utilizando este perfil, podrían perder el acceso al sistema.",
                    "Confirmar Borrado de Perfil",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    // 4. Mandamos a borrar
                    _perfilBLL.EliminarPerfil(idPerfil, nombrePerfil);

                    // 5. Avisamos y refrescamos
                    MessageBox.Show("¡Perfil eliminado del sistema con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrillas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el perfil: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Eliminar_Familia_Click(object sender, EventArgs e)
        {

        }



        private void TraducirToolStrip(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                string traduccion = idiomaBLL.Traducir(item.Name);

                if (traduccion != item.Name)
                    item.Text = traduccion;

                if (item is ToolStripDropDownItem dropDown)
                {
                    TraducirToolStrip(dropDown.DropDownItems);
                }
            }
        }
        #region Idioma
        public void ActualizarIdioma()
        {
            if (ServicesSessionManager.Instancia.ObtenerIdioma() != null)
            {
                Traducir(this.Controls);
                TraducirToolStrip(toolStripLabel1.DropDownItems);
                TraducirToolStrip(toolStripLabel2.DropDownItems);
                toolStripLabel1.Text = idiomaBLL.Traducir("toolStripLabel1");
                toolStripLabel2.Text = idiomaBLL.Traducir("toolStripLabel2");
            }
        }
        private void Traducir(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (!string.IsNullOrEmpty(control.Name))
                {
                    string traduccion = idiomaBLL.Traducir(control.Name);

                    if (traduccion != control.Name)
                        control.Text = traduccion;
                }

                if (control.HasChildren)
                    Traducir(control.Controls);
            }
        }
        #endregion
    }
}