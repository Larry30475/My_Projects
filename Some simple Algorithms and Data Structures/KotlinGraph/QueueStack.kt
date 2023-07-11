class QueueStack {
    var array: ArrayList<Vertex> = ArrayList()

    fun isEmpty(): Boolean {
        return array.isEmpty()
    }

    fun enqueue(value: Vertex) {
        array.add(value)
    }

    fun dequeue(): Vertex? {
        if (array.size == 0) return null
        return array.removeAt(0)
    }
}