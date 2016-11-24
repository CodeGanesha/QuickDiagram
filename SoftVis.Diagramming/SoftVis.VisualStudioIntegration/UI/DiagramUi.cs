﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Codartis.SoftVis.Diagramming;
using Codartis.SoftVis.UI.Wpf.View;
using Codartis.SoftVis.UI.Wpf.ViewModel;
using Codartis.SoftVis.Util;
using Codartis.SoftVis.Util.UI.Wpf.Dialogs;
using Codartis.SoftVis.Util.UI.Wpf.Resources;
using Codartis.SoftVis.VisualStudioIntegration.App;
using Codartis.SoftVis.VisualStudioIntegration.ImageExport;

namespace Codartis.SoftVis.VisualStudioIntegration.UI
{
    /// <summary>
    /// Provides diagram UI services. Bundles the diagram control and its view model together.
    /// </summary>
    public sealed class DiagramUi : IUiServices
    {
        private const string DiagramStylesXaml = "UI/DiagramStyles.xaml";
        private const double ExportedImageMargin = 10;
        private const string DialogTitle = "Diagram Tool";

        private readonly IHostUiServices _hostUiServices;
        private readonly DiagramControl _diagramControl;
        private readonly DiagramViewModel _diagramViewModel;

        public Dpi ImageExportDpi { get; set; }

        public DiagramUi(IHostUiServices hostUiServices, IArrangedDiagram diagram)
        {
            _hostUiServices = hostUiServices;

            var resourceDictionary = ResourceHelpers.GetResourceDictionary(DiagramStylesXaml, Assembly.GetExecutingAssembly());

            _diagramViewModel = new DiagramViewModel(diagram, minZoom: .1, maxZoom: 10, initialZoom: 1);
            _diagramControl = new DiagramControl(resourceDictionary) { DataContext = _diagramViewModel };

            ImageExportDpi = Dpi.Default;
        }

        public object ContentControl => _diagramControl;

        public void FitDiagramToView() => _diagramViewModel.ZoomToContent();

        public void MessageBox(string message)
        {
            System.Windows.MessageBox.Show(message, DialogTitle);
        }

        public async Task<BitmapSource> CreateDiagramImageAsync(ProgressDialog progressDialog = null)
        {
            try
            {
                var diagramImageCreator = new DataCloningDiagramImageCreator(_diagramViewModel, _diagramControl);
                return await Task.Factory.StartSTA(() =>
                {
                    var progress = progressDialog == null ? null : new Progress<double>(i => progressDialog.SetProgress(i * .9));
                    var cancellationToken = progressDialog?.CancellationToken ?? CancellationToken.None;
                    return diagramImageCreator.CreateImage(ImageExportDpi.Value, ExportedImageMargin, cancellationToken, progress);
                });
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (OutOfMemoryException)
            {
                HandleOutOfMemory();
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception in CreateDiagramImageAsync: {e}");
                throw;
            }
        }

        public ProgressDialog ShowProgressDialog(string text)
        {
            var hostMainWindow = _hostUiServices.GetHostMainWindow();
            var progressDialog = new ProgressDialog(hostMainWindow, text, DialogTitle);
            progressDialog.Show();
            return progressDialog;
        }

        public string SelectSaveFilename(string title, string filter)
        {
            var saveFileDialog = new SaveFileDialog { Title = title, Filter = filter };
            saveFileDialog.ShowDialog();
            return saveFileDialog.FileName;
        }

        private void HandleOutOfMemory()
        {
            MessageBox("Cannot generate the image because it is too large. Please select a smaller DPI value.");
        }
    }
}
