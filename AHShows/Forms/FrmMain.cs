using System.ComponentModel;
using System.Reflection;
using AHShows.Exceptions;
using AHShows.Sq.Dat;
using AHShows.Sq.Dat.Parsing;
using AHShows.ViewModels;

namespace AHShows.Forms;

public partial class FrmMain : Form
{
    private string _selectedFolder;
    private List<SqShow> _shows;

    public FrmMain()
    {
        InitializeComponent();
        _selectedFolder = string.Empty;
        _shows = new List<SqShow>();
        SetVersionNumberInTitle();
    }

    private void SetVersionNumberInTitle()
    {
        Version? v = Assembly.GetExecutingAssembly().GetName().Version;
        if (v != null)
        {
            Text += $" - v{v.Major}.{v.Minor}.{v.Build}";
        }
    }


    private void tbFolder_DragDrop(object sender, DragEventArgs e)
    {
        try
        {
            IDataObject? dataObject = e.Data;
            if (dataObject == null)
            {
                return;
            }

            object? data = dataObject.GetData(DataFormats.FileDrop);
            if (data == null)
            {
                return;
            }

            string[] items = (string[])data;
            if (items.Length <= 0 || !Directory.Exists(items[0]))
            {
                return;
            }

            tbFolder.Text = items[0];
            _selectedFolder = items[0];
        }
        catch
        {
            // ignored
        }
    }

    private void tbFolder_DragEnter(object sender, DragEventArgs e)
    {
        try
        {
            IDataObject? dataObject = e.Data;
            if (dataObject == null)
            {
                return;
            }
            // Controleren of er bestanden/mappen worden gesleept
            if (dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                object? data = dataObject.GetData(DataFormats.FileDrop);
                if (data == null)
                {
                    return;
                }

                string[] items = (string[])data;
                
                if (items.Length > 0 && Directory.Exists(items[0]))
                {
                    e.Effect = DragDropEffects.Copy; // Map accepteren
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
        catch
        {
            // ignored
        }

    }

    private void tbFolder_DoubleClick(object sender, EventArgs e)
    {
        ShowDirectoryBrowser();
    }

    private void ShowDirectoryBrowser()
    {
        using (FolderBrowserDialog fbd = new())
        {
            fbd.Description = "Kies een map";
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbFolder.Text = fbd.SelectedPath;
            _selectedFolder = fbd.SelectedPath;
        }
    }

    private void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            ProcessFolder(_selectedFolder);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "AH Shows", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ProcessFolder(string? folder)
    {
        if (folder == null)
        {
            throw new FolderNotSelectedException();
        }
        if (!Directory.Exists(folder))
        {
            throw new FolderDoesNotExistsException();
        }

        if (folder.Length > 0 && folder[folder.Length - 1] != '\\')
        {
            folder += "\\";
        }

        if (!Directory.Exists(folder + "SHOWS"))
        {
            throw new FolderNotValidAhSqDirectoryException();
        }

        // Er is een "geldige" gevonden. Laten we hem verwerken.
        if (!backgroundWorker.IsBusy)
        { 
            listBoxLogs.Items.Clear();
            btnProcess.Enabled = false;
            btnExport.Enabled = false;
            FolderSettings settings = new FolderSettings()
                                      {
                                          FolderName = folder + "SHOWS",
                                          IncludeScenes = cbScenes.Checked
                                      };
                            
            backgroundWorker.RunWorkerAsync(settings);
        }
        else
        {
            throw new FolderProcessingIsBusyException();
        }
    }

    private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
    {
        FolderSettings? folderSettings = e.Argument as FolderSettings;
        if (folderSettings == null)
        {
            return;
        }
        
        var reader = new SqDatReader(
            new ShowDatParser(), 
            new NvDataDatParser(),
            new SceneDatParser()
        );
        
        
        string[] directories = Directory.GetDirectories(folderSettings.FolderName, "SHOW*");
        List<SqShow> shows = new List<SqShow>();
        foreach (var dir in directories)
        {
            SqShow show = reader.Read(dir);

            backgroundWorker.ReportProgress(0, $"-- Show folder: {show.FolderName}; Show name: {show.Name}; --");
            foreach (SqPreAmp input in show.Inputs)
            {
                backgroundWorker.ReportProgress(0, $"--- Pre Amp: {input.ChannelId}; Gain: {input.GainEffective}; Phantom power: {input.PhantomPower}; Pad: {input.Pad}; ---");
            }
            
            if (folderSettings.IncludeScenes)
            {
                foreach (var scene in show.Scenes)
                {
                    backgroundWorker.ReportProgress(0, $"--- Scene file: {scene.FileName}; Scane number: {scene.Number}; Scene name: {scene.Name}; ---");
                    backgroundWorker.ReportProgress(0, $"--- Input channels ---");
                    foreach (var input in scene.Inputs)
                    {
                        string state =
                            $"--- Input channel: {input.Number}; Name: {input.Name}; Color: {Tools.ColorConverter.ConvertColor(input.Color ?? Color.Black)}; ---";
                        backgroundWorker.ReportProgress(0, state);
                    }
                    
                }
            }
            shows.Add(show);
        }
        e.Result = shows;
    }

    private void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        listBoxLogs.Items.Add(e.UserState as string);
    }

    private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        _shows = e.Result as List<SqShow>;
        
        btnProcess.Enabled = true;
        btnExport.Enabled = true;
        tabShows.TabPages.Clear();
        RenderShows();
    }

    private void RenderShows()
    {
        foreach (SqShow show in _shows)
        {
            AddShow(show);
        }
    }
    
    private void AddShow(SqShow show)
    {
        TabPage showTab = new TabPage(show.Name);

        TabControl showTabControl = new TabControl
        {
            Dock = DockStyle.Fill
        };

        // // Algemene informatie
        TabPage algemeenTab = CreateAlgemeenTab(show);
        //
        // // Scenes
        // TabPage scenesTab = CreateScenesTab(show);
        //
        // // Inputs
        // TabPage inputsTab = CreateInputsTab(show);
        //
        showTabControl.TabPages.Add(algemeenTab);
        // showTabControl.TabPages.Add(scenesTab);
        // showTabControl.TabPages.Add(inputsTab);

        showTab.Controls.Add(showTabControl);

        tabShows.TabPages.Add(showTab);
    }
    
    private TabPage CreateAlgemeenTab(SqShow show)
    {
        TabPage tab = new TabPage("Algemeen");

        TableLayoutPanel table = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            ColumnCount = 2,
            RowCount = 2,
            Padding = new Padding(10)
        };

        table.ColumnStyles.Add(
            new ColumnStyle(SizeType.AutoSize));

        table.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 100));

        table.Controls.Add(
            new Label
            {
                Text = "Naam:",
                AutoSize = true
            },
            0, 0);

        table.Controls.Add(
            new TextBox
            {
                Text = show.Name,
                Dock = DockStyle.Fill
            },
            1, 0);

        table.Controls.Add(
            new Label
            {
                Text = "Folder:",
                AutoSize = true
            },
            0, 1);

        table.Controls.Add(
            new TextBox
            {
                Text = show.FolderName,
                Dock = DockStyle.Fill,
                ReadOnly = true
            },
            1, 1);

        tab.Controls.Add(table);

        return tab;
    }

    private void btnEmpty_Click(object sender, EventArgs e)
    {
        listBoxLogs.Items.Clear();
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        if (listBoxLogs.Items.Count <= 0)
        {
            MessageBox.Show("Er zijn geen regels aanwezig. Er kan geen export gemaakt.", "AH Shows", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        DialogResult result = saveExportFileDialog.ShowDialog();
        if (result == DialogResult.OK && saveExportFileDialog.FileName != "")
        {
            using (StreamWriter writer = new StreamWriter(saveExportFileDialog.OpenFile()))
            {
                foreach (var item in listBoxLogs.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            MessageBox.Show("Bestand opgeslagen!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }

    private void btnSelectDirectory_Click(object sender, EventArgs e)
    {
        ShowDirectoryBrowser();
    }
}