﻿using System;
using System.Collections.Generic;

namespace Codartis.SoftVis.Modeling2
{
    /// <summary>
    /// A read-only view of a model. 
    /// A model consists of nodes and relationships between nodes.
    /// The nodes form one or more tree hierarchies (e.g. packages contain other packages and types).
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// The collection of model nodes that are hierarchy roots.
        /// </summary>
        IEnumerable<IModelNode> RootNodes { get; }

        /// <summary>
        /// The collection of relationships that exist between nodes.
        /// </summary>
        IEnumerable<IModelRelationship> Relationships { get; }

        /// <summary>
        /// Returns all relationships attached to the given node (as either a source or target node).
        /// </summary>
        /// <param name="node">A model node.</param>
        /// <returns>A read-only collection of relationships.</returns>
        IEnumerable<IModelRelationship> GetRelationships(IModelNode node);

        /// <summary>
        /// Returns those nodes that are related to the given node with the given type of relationship.
        /// </summary>
        /// <param name="node">A model node.</param>
        /// <param name="relationshipType">A type of relationship.</param>
        /// <param name="recursive">True means that nodes are recursively traversed. False returns only immediately related nodes.</param>
        /// <returns>A read-only collection of nodes.</returns>
        IEnumerable<IModelNode> GetRelatedNodes(IModelNode node, Type relationshipType, bool recursive = false);
    }
}
