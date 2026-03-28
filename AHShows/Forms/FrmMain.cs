using System.ComponentModel;
using System.Reflection;
using AHShows.Exceptions;
using AHShows.ViewModels;

namespace AHShows.Forms;

public partial class FrmMain : Form
{
    private string _selectedFolder;

    public FrmMain()
    {
        InitializeComponent();
        _selectedFolder = string.Empty;
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
            listBoxOutput.Items.Clear();
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
        string[] directories = Directory.GetDirectories(folderSettings.FolderName, "SHOW*");
        foreach (var dir in directories)
        {
            string showFolder = Path.GetFileName(dir);
            string showDat = Path.Combine(dir, "SHOW.DAT");
            string showName = "(Onbekend)";

            if (File.Exists(showDat))
            {
                byte[] buf = File.ReadAllBytes(showDat);
                showName = Tools.Ascii.ReadNullTerminatedAscii(buf, 0);
            }

            backgroundWorker.ReportProgress(0, $"-- Show folder: {showFolder}; Show name: {showName} --");
            if (folderSettings.IncludeScenes)
            {
                foreach (string sceneFile in Directory.GetFiles(dir, "SCENE*.DAT"))
                {
                    byte[] sbuf = File.ReadAllBytes(sceneFile);
                    string sceneName = Tools.Ascii.ReadNullTerminatedAscii(sbuf, 20);
                    backgroundWorker.ReportProgress(0, $"--- Scene file: {Path.GetFileName(sceneFile)}; Scene name: {sceneName}");
                }
            }
        }
    }

    private void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        listBoxOutput.Items.Add(e.UserState as string);
    }

    private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        btnProcess.Enabled = true;
        btnExport.Enabled = true;
    }

    private void btnEmpty_Click(object sender, EventArgs e)
    {
        listBoxOutput.Items.Clear();
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        if (listBoxOutput.Items.Count <= 0)
        {
            MessageBox.Show("Er zijn geen regels aanwezig. Er kan geen export gemaakt.", "AH Shows", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        DialogResult result = saveExportFileDialog.ShowDialog();
        if (result == DialogResult.OK && saveExportFileDialog.FileName != "")
        {
            using (StreamWriter writer = new StreamWriter(saveExportFileDialog.OpenFile()))
            {
                foreach (var item in listBoxOutput.Items)
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