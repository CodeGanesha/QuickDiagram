﻿using Codartis.SoftVis.Diagramming.Implementation;

namespace Codartis.SoftVis.Diagramming.UnitTests.TestSubjects
{
    internal sealed class TestDiagramNode : DiagramNode
    {
        public TestDiagramNode(string name = null) 
            : base(new TestModelNode(name))
        {
        }
    }
}
