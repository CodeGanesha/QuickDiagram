﻿using System;

namespace Codartis.SoftVis.VisualStudioIntegration.Commands
{
    /// <summary>
    /// Decreases the font size of the diagram.
    /// </summary>
    internal sealed class DecreaseFontSizeCommand : FontSizeCommandBase
    {
        public DecreaseFontSizeCommand(IPackageServices packageServices)
            : base(VsctConstants.SoftVisCommandSetGuid, VsctConstants.DecreaseFontSizeCommand, packageServices)
        { }

        public override void Execute(object sender, EventArgs e)
        {
            var diagramWindow = PackageServices.GetDiagramWindow();
            diagramWindow.FontSize = DecreaseFontSize(diagramWindow.FontSize);
        }
    }
}
