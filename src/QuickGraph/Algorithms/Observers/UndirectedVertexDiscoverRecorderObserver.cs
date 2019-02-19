using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TVertex">type of a vertex</typeparam>
    /// <typeparam name="TEdge">type of an edge</typeparam>
    /// <reference-ref
    ///     idref="boost"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class UndirectedVertexDiscoverRecorderObserver<TVertex, TEdge> :
        IObserver<IDistanceRecorderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IList<TVertex> discoveredVertices;

        public UndirectedVertexDiscoverRecorderObserver()
            : this(new List<TVertex>())
        { }

        public UndirectedVertexDiscoverRecorderObserver(
            IList<TVertex> discoveredVertices)
        {
            Contract.Requires(discoveredVertices != null);

            this.discoveredVertices = discoveredVertices;
        }

        public IList<TVertex> DiscoveredVertices
        {
            get { return this.discoveredVertices; }
        }

        public IDisposable Attach(IDistanceRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            //algorithm.TreeEdge += TreeEdge;
            algorithm.DiscoverVertex += DiscoverVertex;
            return new DisposableAction(() => algorithm.DiscoverVertex -= DiscoverVertex);
        }

        private void DiscoverVertex(TVertex vertex)
        {
            discoveredVertices.Add(vertex);
        }
    }
}
