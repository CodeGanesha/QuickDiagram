﻿using Codartis.SoftVis.Modeling;

namespace Codartis.SoftVis.TestHostApp.Modeling
{
    /// <summary>
    /// Defines the directed relationship types.
    /// </summary>
    public static class DirectedRelationshipTypes
    {
        public static readonly DirectedModelRelationshipType BaseType = 
            new DirectedModelRelationshipType(TestModelRelationshipStereotype.Inheritance, RelationshipDirection.Outgoing);

        public static readonly DirectedModelRelationshipType Subtype =
            new DirectedModelRelationshipType(TestModelRelationshipStereotype.Inheritance, RelationshipDirection.Incoming);

        public static readonly DirectedModelRelationshipType ImplementedInterface =
            new DirectedModelRelationshipType(TestModelRelationshipStereotype.Implementation, RelationshipDirection.Outgoing);

        public static readonly DirectedModelRelationshipType ImplementerType =
            new DirectedModelRelationshipType(TestModelRelationshipStereotype.Implementation, RelationshipDirection.Incoming);

    }
}
