namespace AHShows.Forms;

partial class FrmMain
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
    tbFolder = new System.Windows.Forms.TextBox();
    lblFolder = new System.Windows.Forms.Label();
    btnProcess = new System.Windows.Forms.Button();
    lblLogs = new System.Windows.Forms.Label();
    btnExport = new System.Windows.Forms.Button();
    btnEmpty = new System.Windows.Forms.Button();
    backgroundWorker = new System.ComponentModel.BackgroundWorker();
    saveExportFileDialog = new System.Windows.Forms.SaveFileDialog();
    btnSelectDirectory = new System.Windows.Forms.Button();
    cbScenes = new System.Windows.Forms.CheckBox();
    lblShows = new System.Windows.Forms.Label();
    tabShows = new System.Windows.Forms.TabControl();
    listBoxLogs = new System.Windows.Forms.ListBox();
    SuspendLayout();
    // 
    // tbFolder
    // 
    tbFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
    tbFolder.Location = new System.Drawing.Point(118, 6);
    tbFolder.Name = "tbFolder";
    tbFolder.ReadOnly = true;
    tbFolder.Size = new System.Drawing.Size(585, 27);
    tbFolder.TabIndex = 1;
    tbFolder.DragDrop += tbFolder_DragDrop;
    tbFolder.DragEnter += tbFolder_DragEnter;
    tbFolder.DoubleClick += tbFolder_DoubleClick;
    // 
    // lblFolder
    // 
    lblFolder.Location = new System.Drawing.Point(12, 9);
    lblFolder.Name = "lblFolder";
    lblFolder.Size = new System.Drawing.Size(100, 23);
    lblFolder.TabIndex = 0;
    lblFolder.Text = "Map";
    // 
    // btnProcess
    // 
    btnProcess.Location = new System.Drawing.Point(118, 39);
    btnProcess.Name = "btnProcess";
    btnProcess.Size = new System.Drawing.Size(95, 36);
    btnProcess.TabIndex = 3;
    btnProcess.Text = "Verwerken";
    btnProcess.UseVisualStyleBackColor = true;
    btnProcess.Click += btnProcess_Click;
    // 
    // lblLogs
    // 
    lblLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
    lblLogs.Location = new System.Drawing.Point(12, 401);
    lblLogs.Name = "lblLogs";
    lblLogs.Size = new System.Drawing.Size(100, 23);
    lblLogs.TabIndex = 3;
    lblLogs.Text = "Logs";
    // 
    // btnExport
    // 
    btnExport.Location = new System.Drawing.Point(320, 39);
    btnExport.Name = "btnExport";
    btnExport.Size = new System.Drawing.Size(95, 36);
    btnExport.TabIndex = 7;
    btnExport.Text = "Export";
    btnExport.UseVisualStyleBackColor = true;
    btnExport.Click += btnExport_Click;
    // 
    // btnEmpty
    // 
    btnEmpty.Location = new System.Drawing.Point(219, 39);
    btnEmpty.Name = "btnEmpty";
    btnEmpty.Size = new System.Drawing.Size(95, 36);
    btnEmpty.TabIndex = 6;
    btnEmpty.Text = "Wissen";
    btnEmpty.UseVisualStyleBackColor = true;
    btnEmpty.Click += btnEmpty_Click;
    // 
    // backgroundWorker
    // 
    backgroundWorker.WorkerReportsProgress = true;
    backgroundWorker.DoWork += BackgroundWorkerDoWork;
    backgroundWorker.ProgressChanged += BackgroundWorkerProgressChanged;
    backgroundWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
    // 
    // saveExportFileDialog
    // 
    saveExportFileDialog.DefaultExt = "log";
    saveExportFileDialog.Filter = "txt files (*.txt)|*.txt";
    saveExportFileDialog.Title = "Export resultaten";
    // 
    // btnSelectDirectory
    // 
    btnSelectDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
    btnSelectDirectory.Location = new System.Drawing.Point(726, 9);
    btnSelectDirectory.Name = "btnSelectDirectory";
    btnSelectDirectory.Size = new System.Drawing.Size(44, 27);
    btnSelectDirectory.TabIndex = 2;
    btnSelectDirectory.Text = "...";
    btnSelectDirectory.UseVisualStyleBackColor = true;
    btnSelectDirectory.Click += btnSelectDirectory_Click;
    // 
    // cbScenes
    // 
    cbScenes.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
    cbScenes.Checked = true;
    cbScenes.CheckState = System.Windows.Forms.CheckState.Checked;
    cbScenes.Location = new System.Drawing.Point(690, 46);
    cbScenes.Name = "cbScenes";
    cbScenes.Size = new System.Drawing.Size(80, 24);
    cbScenes.TabIndex = 4;
    cbScenes.Text = "Scenes";
    cbScenes.UseVisualStyleBackColor = true;
    // 
    // lblShows
    // 
    lblShows.Location = new System.Drawing.Point(12, 81);
    lblShows.Name = "lblShows";
    lblShows.Size = new System.Drawing.Size(100, 23);
    lblShows.TabIndex = 9;
    lblShows.Text = "Shows:";
    // 
    // tabShows
    // 
    tabShows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
    tabShows.Location = new System.Drawing.Point(118, 81);
    tabShows.Name = "tabShows";
    tabShows.SelectedIndex = 0;
    tabShows.Size = new System.Drawing.Size(652, 314);
    tabShows.TabIndex = 10;
    // 
    // listBoxLogs
    // 
    listBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
    listBoxLogs.FormattingEnabled = true;
    listBoxLogs.Location = new System.Drawing.Point(118, 401);
    listBoxLogs.Name = "listBoxLogs";
    listBoxLogs.Size = new System.Drawing.Size(652, 144);
    listBoxLogs.TabIndex = 11;
    // 
    // FrmMain
    // 
    AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
    AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    BackColor = System.Drawing.SystemColors.Control;
    ClientSize = new System.Drawing.Size(782, 553);
    Controls.Add(listBoxLogs);
    Controls.Add(tabShows);
    Controls.Add(lblShows);
    Controls.Add(cbScenes);
    Controls.Add(btnSelectDirectory);
    Controls.Add(btnEmpty);
    Controls.Add(btnExport);
    Controls.Add(lblLogs);
    Controls.Add(btnProcess);
    Controls.Add(lblFolder);
    Controls.Add(tbFolder);
    Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
    Location = new System.Drawing.Point(19, 19);
    Text = "AH Shows";
    ResumeLayout(false);
    PerformLayout();
}

private System.Windows.Forms.ListBox listBoxLogs;

private System.Windows.Forms.TabControl tabShows;

private System.Windows.Forms.Label lblShows;

private System.Windows.Forms.CheckBox cbScenes;

    private System.Windows.Forms.Button btnSelectDirectory;

    private System.Windows.Forms.SaveFileDialog saveExportFileDialog;

    private System.ComponentModel.BackgroundWorker backgroundWorker;

    private System.Windows.Forms.Label lblLogs;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.Button btnEmpty;

    private System.Windows.Forms.Button btnProcess;


    private System.Windows.Forms.TextBox tbFolder;
    private System.Windows.Forms.Label lblFolder;

#endregion
}