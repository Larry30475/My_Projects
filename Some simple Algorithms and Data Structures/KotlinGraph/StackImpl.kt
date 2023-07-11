class StackImpl {
    var array: ArrayList<Vertex> = ArrayList()
    var topIndex: Int = 0

    fun top(): Vertex {
        if (array.isEmpty()) throw IllegalStateException()
        return array.get(topIndex - 1)
    }

    fun pop(): Vertex {
        if (array.isEmpty()) throw IllegalStateException()
        val temp = array.get(--topIndex)
        array.remove(temp)
        return temp
    }

    fun push(value: Vertex) {
        array.add(value)
        topIndex++
    }
}