import java.util.*;

public class Graph<T> {
    int [][] adjMatrix;
    ArrayList<T> sources = new ArrayList<>();
    ArrayList<T> desties = new ArrayList<>();

    public Graph(List<Edge<T>> edges) {
        sources.add(edges.get(0).getSource());
        desties.add(edges.get(0).getDestination());
        for (Edge<T> edge : edges) {
            int countSource = 0;
            int countDest = 0;
            for (T source : sources) {
                if (edge.getSource() == source) {
                    countSource++;
                }
            }

            if(countSource == 0) sources.add(edge.getSource());

            for (T desty : desties) {
                if (edge.getDestination() == desty) {
                    countDest++;
                }
            }

            if(countDest == 0) desties.add(edge.getDestination());
        }

        adjMatrix = new int[sources.size()][desties.size()];

        for (Edge<T> edge : edges) {
            for(T source: sources)
            {
                for(T desty: desties)
                {
                    if(source == edge.getSource() && desty == edge.getDestination())
                    {
                        adjMatrix[sources.indexOf(source)][desties.indexOf(desty)] = edge.getWeight();
                    }
                }
            }
        }
    }

    public ArrayList<Edge<T>> edgeList()
    {
        ArrayList<Edge<T>> listOfEdges = new ArrayList<>();
        for(int i = 0; i < adjMatrix.length; i++)
        {
            for(int j = 0; j < adjMatrix[i].length; j++)
            {
                if(adjMatrix[i][j] != 0)
                {
                    listOfEdges.add(new Edge<>(sources.get(i), desties.get(j), adjMatrix[i][j]));
                }
            }
        }

        return listOfEdges;
    }

    public ArrayList<Edge<T>> edges(T source)
    {
        ArrayList<Edge<T>> neighbours = new ArrayList<>();

        for(int i = 0; i < edgeList().size(); i++)
        {
            if (edgeList().get(i).getSource() == source || edgeList().get(i).getDestination() == source)
            {
                neighbours.add(edgeList().get(i));
            }
        }

        return neighbours;
    }

    public String depthFirst(T startNode) throws NoSuchElementException {
        if (startNode == "doesn't exist") throw new NoSuchElementException();
        StringBuilder s = new StringBuilder();

        Stack<T> stack = new Stack<>();
        ArrayList<T> visited = new ArrayList<>();
        ArrayList<T> pushed = new ArrayList<>();
        stack.push(startNode);
        pushed.add(startNode);
        visited.add(startNode);

        outer:
        while(!stack.array.isEmpty()) {
            T vertex = stack.top();
            ArrayList<Edge<T>> neighbors = edges(vertex);

            if (neighbors.isEmpty()) {
                stack.pop();
                continue;
            }

            for (Edge<T> neighbor : neighbors) {
                T destination = neighbor.getDestination();
                if (!pushed.contains(destination)) {
                    stack.push(destination);
                    pushed.add(destination);
                    visited.add(destination);
                    continue outer;
                }
            }
            stack.pop();
        }

        for (T t : visited) {
            s.append(t).append(", ");
        }

        return s.substring(0, s.length() - 2);
    }

    public String breadthFirst(T startNode) throws NoSuchElementException {
        if (startNode == "doesn't exist") throw new NoSuchElementException();

        StringBuilder s = new StringBuilder();

        Queue<T> queue = new Queue<>();
        ArrayList<T> enqueued = new ArrayList<>();
        ArrayList<T> visited = new ArrayList<>();

        queue.enqueue(startNode);
        enqueued.add(startNode);

        while (true) {
            T vertex = queue.dequeue();
            if(vertex == null) break;

            visited.add(vertex);

            ArrayList<Edge<T>> neighborEdges = edges(vertex);

            for (Edge<T> neighborEdge : neighborEdges) {
                if (!enqueued.contains(neighborEdge.getDestination())) {
                    queue.enqueue(neighborEdge.getDestination());
                    enqueued.add(neighborEdge.getDestination());
                }
            }
        }

        for (T t : visited) {
            s.append(t).append(", ");
        }

        return s.substring(0, s.length() - 2);
    }
}
