interface Graph {
    fun createVertex(data: String): Vertex

    fun addDirectedEdge(source: Vertex, destination: Vertex, weight: Int)

    fun add(source: Vertex, destination: Vertex, weight: Int)

    fun edges(source: Vertex): ArrayList<Edge>

    fun weight(source: Vertex, destination: Vertex): Int?
}