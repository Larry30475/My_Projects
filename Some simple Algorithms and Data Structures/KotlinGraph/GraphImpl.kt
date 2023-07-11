class GraphImpl : Graph {

    private val adjacencies: HashMap<Vertex, ArrayList<Edge>> = HashMap()

    override fun createVertex(data: String): Vertex {
        val vertex = Vertex(adjacencies.count(), data)
        adjacencies[vertex] = ArrayList()
        return vertex
    }

    override fun addDirectedEdge(source: Vertex, destination: Vertex, weight: Int) {
        val edge = Edge(source, destination, weight)
        adjacencies[source]?.add(edge)
    }

    override fun add(source: Vertex, destination: Vertex, weight: Int) {
        addDirectedEdge(source, destination, weight)
    }

    override fun edges(source: Vertex): ArrayList<Edge> {
        return adjacencies[source] ?: arrayListOf()
    }

    override fun weight(source: Vertex, destination: Vertex): Int? {
        return edges(source).firstOrNull { it.destination == destination }?.weight
    }

    override fun toString(): String {
        return buildString {
            adjacencies.forEach { (vertex, edges) ->
                val edgeString = edges.joinToString { it.destination.data }
                append("${vertex.data} ---> [ $edgeString ]\n")
            }
        }
    }

    fun breadthFirstSearch(source: Vertex): ArrayList<Vertex> {
        val queue = QueueStack()
        val enqueued = ArrayList<Vertex>()
        val visited = ArrayList<Vertex>()

        queue.enqueue(source)
        enqueued.add(source)

        while (true) {
            val vertex = queue.dequeue() ?: break

            visited.add(vertex)

            val neighborEdges = edges(vertex)
            neighborEdges.forEach {
                if (!enqueued.contains(it.destination)) {
                    queue.enqueue(it.destination)
                    enqueued.add(it.destination)
                }
            }
        }

        return visited
    }

    fun depthFirstSearch(source: Vertex): ArrayList<Vertex> {
        val stack = StackImpl()
        val visited = arrayListOf<Vertex>()
        val pushed = mutableSetOf<Vertex>()

        stack.push(source)
        pushed.add(source)
        visited.add(source)

        outer@ while (true) {
            if (stack.array.isEmpty()) break

            val vertex = stack.top()
            val neighbors = edges(vertex)

            if (neighbors.isEmpty()) {
                stack.pop()
                continue
            }

            for (i in 0 until neighbors.size) {
                val destination = neighbors[i].destination
                if (destination !in pushed) {
                    stack.push(destination)
                    pushed.add(destination)
                    visited.add(destination)
                    continue@outer
                }
            }
            stack.pop()
        }

        return visited
    }
}