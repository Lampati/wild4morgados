namespace CompiladorGargar
{
    partial class CompiladorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelColumna = new System.Windows.Forms.Label();
            this.labelFila = new System.Windows.Forms.Label();
            this.labelToken = new System.Windows.Forms.Label();
            this.labelLexema = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Lexema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.espacio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fila = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxArchivoFuente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonResetear = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLexicografico = new System.Windows.Forms.TabPage();
            this.tabPageSintactico = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonAnalizadorSintacticoTODO = new System.Windows.Forms.Button();
            this.buttonAnalizadorSintactico = new System.Windows.Forms.Button();
            this.dataGridViewErrores = new System.Windows.Forms.DataGridView();
            this.FilaError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripcionError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewSintactico = new System.Windows.Forms.DataGridView();
            this.Pila = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CadenaTexto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageTablaAnalisis = new System.Windows.Forms.TabPage();
            this.dataGridViewTablaAnalisis = new System.Windows.Forms.DataGridView();
            this.tabPageArmadorCodigo = new System.Windows.Forms.TabPage();
            this.textBoxCodigo = new System.Windows.Forms.TextBox();
            this.buttonGenerarCodigo = new System.Windows.Forms.Button();
            this.tabPageIDE = new System.Windows.Forms.TabPage();
            this.txtBxIDE = new System.Windows.Forms.TextBox();
            this.dataGridViewErroresIDE = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonCompilarIde = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageLexicografico.SuspendLayout();
            this.tabPageSintactico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSintactico)).BeginInit();
            this.tabPageTablaAnalisis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTablaAnalisis)).BeginInit();
            this.tabPageArmadorCodigo.SuspendLayout();
            this.tabPageIDE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErroresIDE)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 149);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Obtener Token";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lexema:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Token:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fila:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Columna:";
            // 
            // labelColumna
            // 
            this.labelColumna.AutoSize = true;
            this.labelColumna.Location = new System.Drawing.Point(151, 103);
            this.labelColumna.Name = "labelColumna";
            this.labelColumna.Size = new System.Drawing.Size(0, 17);
            this.labelColumna.TabIndex = 8;
            // 
            // labelFila
            // 
            this.labelFila.AutoSize = true;
            this.labelFila.Location = new System.Drawing.Point(151, 74);
            this.labelFila.Name = "labelFila";
            this.labelFila.Size = new System.Drawing.Size(0, 17);
            this.labelFila.TabIndex = 7;
            // 
            // labelToken
            // 
            this.labelToken.AutoSize = true;
            this.labelToken.Location = new System.Drawing.Point(151, 47);
            this.labelToken.Name = "labelToken";
            this.labelToken.Size = new System.Drawing.Size(0, 17);
            this.labelToken.TabIndex = 6;
            // 
            // labelLexema
            // 
            this.labelLexema.AutoSize = true;
            this.labelLexema.Location = new System.Drawing.Point(151, 21);
            this.labelLexema.Name = "labelLexema";
            this.labelLexema.Size = new System.Drawing.Size(0, 17);
            this.labelLexema.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(569, 366);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Todos los token";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Lexema,
            this.Token,
            this.espacio,
            this.Fila,
            this.Columna});
            this.dataGridView1.Location = new System.Drawing.Point(323, 9);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(736, 334);
            this.dataGridView1.TabIndex = 10;
            // 
            // Lexema
            // 
            this.Lexema.HeaderText = "Lexema";
            this.Lexema.Name = "Lexema";
            this.Lexema.ReadOnly = true;
            this.Lexema.Width = 150;
            // 
            // Token
            // 
            this.Token.HeaderText = "Token";
            this.Token.Name = "Token";
            this.Token.ReadOnly = true;
            this.Token.Width = 150;
            // 
            // espacio
            // 
            this.espacio.HeaderText = "espacio antes?";
            this.espacio.Name = "espacio";
            this.espacio.ReadOnly = true;
            // 
            // Fila
            // 
            this.Fila.HeaderText = "Fila";
            this.Fila.Name = "Fila";
            this.Fila.ReadOnly = true;
            this.Fila.Width = 40;
            // 
            // Columna
            // 
            this.Columna.HeaderText = "Columna";
            this.Columna.Name = "Columna";
            this.Columna.ReadOnly = true;
            this.Columna.Width = 40;
            // 
            // textBoxArchivoFuente
            // 
            this.textBoxArchivoFuente.Location = new System.Drawing.Point(155, 487);
            this.textBoxArchivoFuente.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxArchivoFuente.Multiline = true;
            this.textBoxArchivoFuente.Name = "textBoxArchivoFuente";
            this.textBoxArchivoFuente.ReadOnly = true;
            this.textBoxArchivoFuente.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxArchivoFuente.Size = new System.Drawing.Size(845, 249);
            this.textBoxArchivoFuente.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 449);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Archivo fuente";
            // 
            // buttonResetear
            // 
            this.buttonResetear.Location = new System.Drawing.Point(740, 366);
            this.buttonResetear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonResetear.Name = "buttonResetear";
            this.buttonResetear.Size = new System.Drawing.Size(140, 23);
            this.buttonResetear.TabIndex = 13;
            this.buttonResetear.Text = "Resetear";
            this.buttonResetear.UseVisualStyleBackColor = true;
            this.buttonResetear.Click += new System.EventHandler(this.buttonResetear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageLexicografico);
            this.tabControl1.Controls.Add(this.tabPageSintactico);
            this.tabControl1.Controls.Add(this.tabPageTablaAnalisis);
            this.tabControl1.Controls.Add(this.tabPageArmadorCodigo);
            this.tabControl1.Controls.Add(this.tabPageIDE);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1028, 853);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPageLexicografico
            // 
            this.tabPageLexicografico.BackColor = System.Drawing.Color.Transparent;
            this.tabPageLexicografico.Controls.Add(this.buttonResetear);
            this.tabPageLexicografico.Controls.Add(this.label5);
            this.tabPageLexicografico.Controls.Add(this.textBoxArchivoFuente);
            this.tabPageLexicografico.Controls.Add(this.dataGridView1);
            this.tabPageLexicografico.Controls.Add(this.button2);
            this.tabPageLexicografico.Controls.Add(this.labelColumna);
            this.tabPageLexicografico.Controls.Add(this.labelFila);
            this.tabPageLexicografico.Controls.Add(this.labelToken);
            this.tabPageLexicografico.Controls.Add(this.labelLexema);
            this.tabPageLexicografico.Controls.Add(this.label4);
            this.tabPageLexicografico.Controls.Add(this.label3);
            this.tabPageLexicografico.Controls.Add(this.label2);
            this.tabPageLexicografico.Controls.Add(this.label1);
            this.tabPageLexicografico.Controls.Add(this.button1);
            this.tabPageLexicografico.Location = new System.Drawing.Point(4, 25);
            this.tabPageLexicografico.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageLexicografico.Name = "tabPageLexicografico";
            this.tabPageLexicografico.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageLexicografico.Size = new System.Drawing.Size(1020, 824);
            this.tabPageLexicografico.TabIndex = 0;
            this.tabPageLexicografico.Text = "Lexicografico";
            this.tabPageLexicografico.UseVisualStyleBackColor = true;
            // 
            // tabPageSintactico
            // 
            this.tabPageSintactico.BackColor = System.Drawing.Color.Transparent;
            this.tabPageSintactico.Controls.Add(this.button3);
            this.tabPageSintactico.Controls.Add(this.buttonAnalizadorSintacticoTODO);
            this.tabPageSintactico.Controls.Add(this.buttonAnalizadorSintactico);
            this.tabPageSintactico.Controls.Add(this.dataGridViewErrores);
            this.tabPageSintactico.Controls.Add(this.dataGridViewSintactico);
            this.tabPageSintactico.Location = new System.Drawing.Point(4, 25);
            this.tabPageSintactico.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageSintactico.Name = "tabPageSintactico";
            this.tabPageSintactico.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageSintactico.Size = new System.Drawing.Size(1020, 824);
            this.tabPageSintactico.TabIndex = 1;
            this.tabPageSintactico.Text = "Sintactico";
            this.tabPageSintactico.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(365, 387);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(164, 29);
            this.button3.TabIndex = 4;
            this.button3.Text = "Siguientes 50 pasos";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonAnalizadorSintacticoTODO
            // 
            this.buttonAnalizadorSintacticoTODO.Location = new System.Drawing.Point(536, 388);
            this.buttonAnalizadorSintacticoTODO.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAnalizadorSintacticoTODO.Name = "buttonAnalizadorSintacticoTODO";
            this.buttonAnalizadorSintacticoTODO.Size = new System.Drawing.Size(152, 28);
            this.buttonAnalizadorSintacticoTODO.TabIndex = 3;
            this.buttonAnalizadorSintacticoTODO.Text = "Analizar Todo";
            this.buttonAnalizadorSintacticoTODO.UseVisualStyleBackColor = true;
            this.buttonAnalizadorSintacticoTODO.Click += new System.EventHandler(this.buttonAnalizadorSintacticoTODO_Click);
            // 
            // buttonAnalizadorSintactico
            // 
            this.buttonAnalizadorSintactico.Location = new System.Drawing.Point(206, 388);
            this.buttonAnalizadorSintactico.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAnalizadorSintactico.Name = "buttonAnalizadorSintactico";
            this.buttonAnalizadorSintactico.Size = new System.Drawing.Size(152, 28);
            this.buttonAnalizadorSintactico.TabIndex = 2;
            this.buttonAnalizadorSintactico.Text = "Siguiente Paso";
            this.buttonAnalizadorSintactico.UseVisualStyleBackColor = true;
            this.buttonAnalizadorSintactico.Click += new System.EventHandler(this.buttonAnalizadorSintactico_Click);
            // 
            // dataGridViewErrores
            // 
            this.dataGridViewErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewErrores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FilaError,
            this.ColumnaError,
            this.TipoError,
            this.DescripcionError});
            this.dataGridViewErrores.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewErrores.Location = new System.Drawing.Point(4, 424);
            this.dataGridViewErrores.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewErrores.Name = "dataGridViewErrores";
            this.dataGridViewErrores.RowHeadersVisible = false;
            this.dataGridViewErrores.RowTemplate.Height = 24;
            this.dataGridViewErrores.Size = new System.Drawing.Size(1012, 396);
            this.dataGridViewErrores.TabIndex = 1;
            // 
            // FilaError
            // 
            this.FilaError.HeaderText = "Fila";
            this.FilaError.Name = "FilaError";
            this.FilaError.ReadOnly = true;
            this.FilaError.Width = 40;
            // 
            // ColumnaError
            // 
            this.ColumnaError.HeaderText = "Columna";
            this.ColumnaError.Name = "ColumnaError";
            this.ColumnaError.ReadOnly = true;
            this.ColumnaError.Width = 40;
            // 
            // TipoError
            // 
            this.TipoError.HeaderText = "Tipo";
            this.TipoError.Name = "TipoError";
            this.TipoError.ReadOnly = true;
            // 
            // DescripcionError
            // 
            this.DescripcionError.HeaderText = "Descripcion";
            this.DescripcionError.Name = "DescripcionError";
            this.DescripcionError.ReadOnly = true;
            this.DescripcionError.Width = 1000;
            // 
            // dataGridViewSintactico
            // 
            this.dataGridViewSintactico.AllowUserToAddRows = false;
            this.dataGridViewSintactico.AllowUserToDeleteRows = false;
            this.dataGridViewSintactico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSintactico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pila,
            this.CadenaTexto});
            this.dataGridViewSintactico.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSintactico.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewSintactico.Name = "dataGridViewSintactico";
            this.dataGridViewSintactico.ReadOnly = true;
            this.dataGridViewSintactico.RowHeadersVisible = false;
            this.dataGridViewSintactico.RowTemplate.Height = 24;
            this.dataGridViewSintactico.Size = new System.Drawing.Size(837, 380);
            this.dataGridViewSintactico.TabIndex = 0;
            // 
            // Pila
            // 
            this.Pila.HeaderText = "Pila";
            this.Pila.Name = "Pila";
            this.Pila.ReadOnly = true;
            this.Pila.Width = 400;
            // 
            // CadenaTexto
            // 
            this.CadenaTexto.HeaderText = "Cadena Texto";
            this.CadenaTexto.Name = "CadenaTexto";
            this.CadenaTexto.ReadOnly = true;
            this.CadenaTexto.Width = 300;
            // 
            // tabPageTablaAnalisis
            // 
            this.tabPageTablaAnalisis.BackColor = System.Drawing.Color.Transparent;
            this.tabPageTablaAnalisis.Controls.Add(this.dataGridViewTablaAnalisis);
            this.tabPageTablaAnalisis.Location = new System.Drawing.Point(4, 25);
            this.tabPageTablaAnalisis.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageTablaAnalisis.Name = "tabPageTablaAnalisis";
            this.tabPageTablaAnalisis.Size = new System.Drawing.Size(1020, 824);
            this.tabPageTablaAnalisis.TabIndex = 2;
            this.tabPageTablaAnalisis.Text = "TablaAnalisis";
            this.tabPageTablaAnalisis.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTablaAnalisis
            // 
            this.dataGridViewTablaAnalisis.AllowUserToAddRows = false;
            this.dataGridViewTablaAnalisis.AllowUserToDeleteRows = false;
            this.dataGridViewTablaAnalisis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTablaAnalisis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTablaAnalisis.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewTablaAnalisis.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewTablaAnalisis.Name = "dataGridViewTablaAnalisis";
            this.dataGridViewTablaAnalisis.ReadOnly = true;
            this.dataGridViewTablaAnalisis.RowHeadersWidth = 100;
            this.dataGridViewTablaAnalisis.RowTemplate.Height = 24;
            this.dataGridViewTablaAnalisis.Size = new System.Drawing.Size(1020, 824);
            this.dataGridViewTablaAnalisis.TabIndex = 0;
            // 
            // tabPageArmadorCodigo
            // 
            this.tabPageArmadorCodigo.Controls.Add(this.textBoxCodigo);
            this.tabPageArmadorCodigo.Controls.Add(this.buttonGenerarCodigo);
            this.tabPageArmadorCodigo.Location = new System.Drawing.Point(4, 25);
            this.tabPageArmadorCodigo.Name = "tabPageArmadorCodigo";
            this.tabPageArmadorCodigo.Size = new System.Drawing.Size(1020, 824);
            this.tabPageArmadorCodigo.TabIndex = 4;
            this.tabPageArmadorCodigo.Text = "Codigo";
            this.tabPageArmadorCodigo.UseVisualStyleBackColor = true;
            // 
            // textBoxCodigo
            // 
            this.textBoxCodigo.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxCodigo.Location = new System.Drawing.Point(0, 0);
            this.textBoxCodigo.Multiline = true;
            this.textBoxCodigo.Name = "textBoxCodigo";
            this.textBoxCodigo.ReadOnly = true;
            this.textBoxCodigo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCodigo.Size = new System.Drawing.Size(1020, 547);
            this.textBoxCodigo.TabIndex = 1;
            // 
            // buttonGenerarCodigo
            // 
            this.buttonGenerarCodigo.Location = new System.Drawing.Point(460, 586);
            this.buttonGenerarCodigo.Name = "buttonGenerarCodigo";
            this.buttonGenerarCodigo.Size = new System.Drawing.Size(102, 48);
            this.buttonGenerarCodigo.TabIndex = 0;
            this.buttonGenerarCodigo.Text = "Generar Codigo";
            this.buttonGenerarCodigo.UseVisualStyleBackColor = true;
            this.buttonGenerarCodigo.Click += new System.EventHandler(this.buttonGenerarCodigo_Click);
            // 
            // tabPageIDE
            // 
            this.tabPageIDE.Controls.Add(this.buttonCompilarIde);
            this.tabPageIDE.Controls.Add(this.dataGridViewErroresIDE);
            this.tabPageIDE.Controls.Add(this.txtBxIDE);
            this.tabPageIDE.Location = new System.Drawing.Point(4, 25);
            this.tabPageIDE.Name = "tabPageIDE";
            this.tabPageIDE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIDE.Size = new System.Drawing.Size(1020, 824);
            this.tabPageIDE.TabIndex = 5;
            this.tabPageIDE.Text = "IDE Texto";
            this.tabPageIDE.UseVisualStyleBackColor = true;
            // 
            // txtBxIDE
            // 
            this.txtBxIDE.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBxIDE.Location = new System.Drawing.Point(3, 3);
            this.txtBxIDE.Multiline = true;
            this.txtBxIDE.Name = "txtBxIDE";
            this.txtBxIDE.Size = new System.Drawing.Size(1014, 628);
            this.txtBxIDE.TabIndex = 0;
            // 
            // dataGridViewErroresIDE
            // 
            this.dataGridViewErroresIDE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewErroresIDE.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridViewErroresIDE.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewErroresIDE.Location = new System.Drawing.Point(3, 667);
            this.dataGridViewErroresIDE.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewErroresIDE.Name = "dataGridViewErroresIDE";
            this.dataGridViewErroresIDE.RowHeadersVisible = false;
            this.dataGridViewErroresIDE.RowTemplate.Height = 24;
            this.dataGridViewErroresIDE.Size = new System.Drawing.Size(1014, 154);
            this.dataGridViewErroresIDE.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Fila";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Columna";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 40;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tipo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Descripcion";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 1000;
            // 
            // buttonCompilarIde
            // 
            this.buttonCompilarIde.Location = new System.Drawing.Point(3, 637);
            this.buttonCompilarIde.Name = "buttonCompilarIde";
            this.buttonCompilarIde.Size = new System.Drawing.Size(145, 23);
            this.buttonCompilarIde.TabIndex = 3;
            this.buttonCompilarIde.Text = "Compilar";
            this.buttonCompilarIde.UseVisualStyleBackColor = true;
            this.buttonCompilarIde.Click += new System.EventHandler(this.buttonCompilarIde_Click);
            // 
            // CompiladorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 853);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CompiladorForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Compilador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageLexicografico.ResumeLayout(false);
            this.tabPageLexicografico.PerformLayout();
            this.tabPageSintactico.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSintactico)).EndInit();
            this.tabPageTablaAnalisis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTablaAnalisis)).EndInit();
            this.tabPageArmadorCodigo.ResumeLayout(false);
            this.tabPageArmadorCodigo.PerformLayout();
            this.tabPageIDE.ResumeLayout(false);
            this.tabPageIDE.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErroresIDE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelColumna;
        private System.Windows.Forms.Label labelFila;
        private System.Windows.Forms.Label labelToken;
        private System.Windows.Forms.Label labelLexema;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBoxArchivoFuente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lexema;
        private System.Windows.Forms.DataGridViewTextBoxColumn Token;
        private System.Windows.Forms.DataGridViewTextBoxColumn espacio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fila;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna;
        private System.Windows.Forms.Button buttonResetear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLexicografico;
        private System.Windows.Forms.TabPage tabPageSintactico;
        private System.Windows.Forms.DataGridView dataGridViewSintactico;
        private System.Windows.Forms.DataGridView dataGridViewErrores;
        private System.Windows.Forms.Button buttonAnalizadorSintactico;
        private System.Windows.Forms.TabPage tabPageTablaAnalisis;
        private System.Windows.Forms.DataGridView dataGridViewTablaAnalisis;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilaError;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaError;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoError;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripcionError;
        private System.Windows.Forms.Button buttonAnalizadorSintacticoTODO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pila;
        private System.Windows.Forms.DataGridViewTextBoxColumn CadenaTexto;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPageArmadorCodigo;
        private System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Button buttonGenerarCodigo;
        private System.Windows.Forms.TabPage tabPageIDE;
        private System.Windows.Forms.TextBox txtBxIDE;
        private System.Windows.Forms.DataGridView dataGridViewErroresIDE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button buttonCompilarIde;
    }
}

