<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrincipal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPrincipal))
        Me.txtSql = New System.Windows.Forms.RichTextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkUtilizarAClausulaAS = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbAlinharComOAlias = New System.Windows.Forms.RadioButton()
        Me.rdbAlinharComATabela = New System.Windows.Forms.RadioButton()
        Me.chkAlinharAliasDasTabelas = New System.Windows.Forms.CheckBox()
        Me.chkSepararOperadoresLogicosComUmEspaco = New System.Windows.Forms.CheckBox()
        Me.chkQuebrarLinhaACadaColuna = New System.Windows.Forms.CheckBox()
        Me.grpQuebrarLinhaACadaColuna = New System.Windows.Forms.GroupBox()
        Me.chkQuebrarLinhaACadaParametroFuncao = New System.Windows.Forms.CheckBox()
        Me.rdbVirgulaInicioLinha = New System.Windows.Forms.RadioButton()
        Me.rdbVirgulaFinalLinha = New System.Windows.Forms.RadioButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnFormatar = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpQuebrarLinhaACadaColuna.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSql
        '
        Me.txtSql.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSql.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSql.Location = New System.Drawing.Point(12, 147)
        Me.txtSql.Name = "txtSql"
        Me.txtSql.Size = New System.Drawing.Size(761, 413)
        Me.txtSql.TabIndex = 0
        Me.txtSql.Text = resources.GetString("txtSql.Text")
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(761, 129)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.chkUtilizarAClausulaAS)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.chkAlinharAliasDasTabelas)
        Me.TabPage1.Controls.Add(Me.chkSepararOperadoresLogicosComUmEspaco)
        Me.TabPage1.Controls.Add(Me.chkQuebrarLinhaACadaColuna)
        Me.TabPage1.Controls.Add(Me.grpQuebrarLinhaACadaColuna)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(753, 103)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "SELECT"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkUtilizarAClausulaAS
        '
        Me.chkUtilizarAClausulaAS.AutoSize = True
        Me.chkUtilizarAClausulaAS.Checked = True
        Me.chkUtilizarAClausulaAS.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUtilizarAClausulaAS.Location = New System.Drawing.Point(508, 6)
        Me.chkUtilizarAClausulaAS.Name = "chkUtilizarAClausulaAS"
        Me.chkUtilizarAClausulaAS.Size = New System.Drawing.Size(125, 17)
        Me.chkUtilizarAClausulaAS.TabIndex = 9
        Me.chkUtilizarAClausulaAS.Text = "Utilizar a cláusula AS"
        Me.chkUtilizarAClausulaAS.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbAlinharComOAlias)
        Me.GroupBox1.Controls.Add(Me.rdbAlinharComATabela)
        Me.GroupBox1.Location = New System.Drawing.Point(501, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(145, 71)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'rdbAlinharComOAlias
        '
        Me.rdbAlinharComOAlias.AutoSize = True
        Me.rdbAlinharComOAlias.Location = New System.Drawing.Point(6, 46)
        Me.rdbAlinharComOAlias.Name = "rdbAlinharComOAlias"
        Me.rdbAlinharComOAlias.Size = New System.Drawing.Size(114, 17)
        Me.rdbAlinharComOAlias.TabIndex = 6
        Me.rdbAlinharComOAlias.Text = "Alinhar com o Alias"
        Me.rdbAlinharComOAlias.UseVisualStyleBackColor = True
        '
        'rdbAlinharComATabela
        '
        Me.rdbAlinharComATabela.AutoSize = True
        Me.rdbAlinharComATabela.Checked = True
        Me.rdbAlinharComATabela.Location = New System.Drawing.Point(6, 23)
        Me.rdbAlinharComATabela.Name = "rdbAlinharComATabela"
        Me.rdbAlinharComATabela.Size = New System.Drawing.Size(121, 17)
        Me.rdbAlinharComATabela.TabIndex = 5
        Me.rdbAlinharComATabela.TabStop = True
        Me.rdbAlinharComATabela.Text = "Alinhar com a tabela"
        Me.rdbAlinharComATabela.UseVisualStyleBackColor = True
        '
        'chkAlinharAliasDasTabelas
        '
        Me.chkAlinharAliasDasTabelas.AutoSize = True
        Me.chkAlinharAliasDasTabelas.Checked = True
        Me.chkAlinharAliasDasTabelas.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAlinharAliasDasTabelas.Location = New System.Drawing.Point(262, 29)
        Me.chkAlinharAliasDasTabelas.Name = "chkAlinharAliasDasTabelas"
        Me.chkAlinharAliasDasTabelas.Size = New System.Drawing.Size(144, 17)
        Me.chkAlinharAliasDasTabelas.TabIndex = 7
        Me.chkAlinharAliasDasTabelas.Text = "Alinhar Alias das Tabelas"
        Me.chkAlinharAliasDasTabelas.UseVisualStyleBackColor = True
        '
        'chkSepararOperadoresLogicosComUmEspaco
        '
        Me.chkSepararOperadoresLogicosComUmEspaco.AutoSize = True
        Me.chkSepararOperadoresLogicosComUmEspaco.Checked = True
        Me.chkSepararOperadoresLogicosComUmEspaco.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSepararOperadoresLogicosComUmEspaco.Location = New System.Drawing.Point(262, 6)
        Me.chkSepararOperadoresLogicosComUmEspaco.Name = "chkSepararOperadoresLogicosComUmEspaco"
        Me.chkSepararOperadoresLogicosComUmEspaco.Size = New System.Drawing.Size(233, 17)
        Me.chkSepararOperadoresLogicosComUmEspaco.TabIndex = 6
        Me.chkSepararOperadoresLogicosComUmEspaco.Text = "Separar operadores lógicos com um espaço"
        Me.chkSepararOperadoresLogicosComUmEspaco.UseVisualStyleBackColor = True
        '
        'chkQuebrarLinhaACadaColuna
        '
        Me.chkQuebrarLinhaACadaColuna.AutoSize = True
        Me.chkQuebrarLinhaACadaColuna.Checked = True
        Me.chkQuebrarLinhaACadaColuna.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkQuebrarLinhaACadaColuna.Location = New System.Drawing.Point(22, 6)
        Me.chkQuebrarLinhaACadaColuna.Name = "chkQuebrarLinhaACadaColuna"
        Me.chkQuebrarLinhaACadaColuna.Size = New System.Drawing.Size(160, 17)
        Me.chkQuebrarLinhaACadaColuna.TabIndex = 2
        Me.chkQuebrarLinhaACadaColuna.Text = "Quebrar linha a cada coluna"
        Me.chkQuebrarLinhaACadaColuna.UseVisualStyleBackColor = True
        '
        'grpQuebrarLinhaACadaColuna
        '
        Me.grpQuebrarLinhaACadaColuna.Controls.Add(Me.chkQuebrarLinhaACadaParametroFuncao)
        Me.grpQuebrarLinhaACadaColuna.Controls.Add(Me.rdbVirgulaInicioLinha)
        Me.grpQuebrarLinhaACadaColuna.Controls.Add(Me.rdbVirgulaFinalLinha)
        Me.grpQuebrarLinhaACadaColuna.Location = New System.Drawing.Point(15, 6)
        Me.grpQuebrarLinhaACadaColuna.Name = "grpQuebrarLinhaACadaColuna"
        Me.grpQuebrarLinhaACadaColuna.Size = New System.Drawing.Size(241, 91)
        Me.grpQuebrarLinhaACadaColuna.TabIndex = 5
        Me.grpQuebrarLinhaACadaColuna.TabStop = False
        '
        'chkQuebrarLinhaACadaParametroFuncao
        '
        Me.chkQuebrarLinhaACadaParametroFuncao.AutoSize = True
        Me.chkQuebrarLinhaACadaParametroFuncao.Checked = True
        Me.chkQuebrarLinhaACadaParametroFuncao.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkQuebrarLinhaACadaParametroFuncao.Location = New System.Drawing.Point(7, 69)
        Me.chkQuebrarLinhaACadaParametroFuncao.Name = "chkQuebrarLinhaACadaParametroFuncao"
        Me.chkQuebrarLinhaACadaParametroFuncao.Size = New System.Drawing.Size(234, 17)
        Me.chkQuebrarLinhaACadaParametroFuncao.TabIndex = 8
        Me.chkQuebrarLinhaACadaParametroFuncao.Text = "Quebrar Linha a cada Parâmetro de Função"
        Me.chkQuebrarLinhaACadaParametroFuncao.UseVisualStyleBackColor = True
        '
        'rdbVirgulaInicioLinha
        '
        Me.rdbVirgulaInicioLinha.AutoSize = True
        Me.rdbVirgulaInicioLinha.Location = New System.Drawing.Point(6, 46)
        Me.rdbVirgulaInicioLinha.Name = "rdbVirgulaInicioLinha"
        Me.rdbVirgulaInicioLinha.Size = New System.Drawing.Size(143, 17)
        Me.rdbVirgulaInicioLinha.TabIndex = 6
        Me.rdbVirgulaInicioLinha.Text = "Vírgula no início da linha"
        Me.rdbVirgulaInicioLinha.UseVisualStyleBackColor = True
        '
        'rdbVirgulaFinalLinha
        '
        Me.rdbVirgulaFinalLinha.AutoSize = True
        Me.rdbVirgulaFinalLinha.Checked = True
        Me.rdbVirgulaFinalLinha.Location = New System.Drawing.Point(6, 23)
        Me.rdbVirgulaFinalLinha.Name = "rdbVirgulaFinalLinha"
        Me.rdbVirgulaFinalLinha.Size = New System.Drawing.Size(136, 17)
        Me.rdbVirgulaFinalLinha.TabIndex = 5
        Me.rdbVirgulaFinalLinha.TabStop = True
        Me.rdbVirgulaFinalLinha.Text = "Vírgula no final da linha"
        Me.rdbVirgulaFinalLinha.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(753, 103)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnFormatar
        '
        Me.btnFormatar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFormatar.Location = New System.Drawing.Point(332, 566)
        Me.btnFormatar.Name = "btnFormatar"
        Me.btnFormatar.Size = New System.Drawing.Size(124, 33)
        Me.btnFormatar.TabIndex = 2
        Me.btnFormatar.Text = "Formatar"
        Me.btnFormatar.UseVisualStyleBackColor = True
        '
        'FrmPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(785, 611)
        Me.Controls.Add(Me.btnFormatar)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.txtSql)
        Me.Name = "FrmPrincipal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SQL Indenter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpQuebrarLinhaACadaColuna.ResumeLayout(False)
        Me.grpQuebrarLinhaACadaColuna.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents txtSql As RichTextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents chkQuebrarLinhaACadaColuna As CheckBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btnFormatar As Button
    Friend WithEvents grpQuebrarLinhaACadaColuna As GroupBox
    Friend WithEvents rdbVirgulaInicioLinha As RadioButton
    Friend WithEvents rdbVirgulaFinalLinha As RadioButton
    Friend WithEvents chkSepararOperadoresLogicosComUmEspaco As CheckBox
    Friend WithEvents chkQuebrarLinhaACadaParametroFuncao As System.Windows.Forms.CheckBox
    Friend WithEvents chkUtilizarAClausulaAS As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbAlinharComOAlias As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAlinharComATabela As System.Windows.Forms.RadioButton
    Friend WithEvents chkAlinharAliasDasTabelas As System.Windows.Forms.CheckBox
End Class
