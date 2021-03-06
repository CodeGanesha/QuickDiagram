﻿using System.Collections.Generic;
using Codartis.SoftVis.Diagramming;
using Codartis.SoftVis.Modeling;

namespace Codartis.SoftVis.UI.Wpf.ViewModel
{
    /// <summary>
    /// Abstract base class for diagram shape view models.
    /// </summary>
    public abstract class DiagramShapeViewModelBase : ModelObserverViewModelBase
    {
        public IDiagramShape DiagramShape { get; protected set; }

        protected DiagramShapeViewModelBase(IModelService modelService, IDiagramService diagramService,
            IDiagramShape diagramShape)
            :base(modelService, diagramService)
        {
            DiagramShape = diagramShape;
        }

        public virtual IEnumerable<MiniButtonViewModelBase> CreateMiniButtonViewModels()
        {
            yield break;
        }

        public override string ToString() => DiagramShape.ToString();
    }
}
