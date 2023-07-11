import java.util.ArrayList;

public class BinarySearchTree<T extends Comparable<T>> {
    String str = "";
    ArrayList<T> result = new ArrayList<>();
    class Node{
        T value;
        Node left;
        Node right;
        Node(T obj)
        {
            value = obj;
        }
    }
    private Node root;
    public BinarySearchTree()
    {
        root = null;
    }

    public void add(T value) throws DuplicateElementException {
        root = insert(root, value);
    }

    private Node insert(Node node, T elem) throws DuplicateElementException {
        if(node == null)
        {
            node = new Node(elem);
        }
        else
        {
            int cmp = elem.compareTo(node.value);
            if(cmp < 0)
            {
                node.left = insert(node.left, elem);
            }
            else if(cmp > 0)
            {
                node.right = insert(node.right, elem);
            }
            else throw new DuplicateElementException();
        }
        return node;
    }

    public boolean contains(T value) {
        Node node = root;
        int cmp = 0;
        while(node != null && (cmp = value.compareTo(node.value)) != 0)
        {
            node = cmp < 0 ? node.left : node.right;
        }
        return node != null;
    }

    public void delete(T value) {
        root = delete(value, root);
    }

    protected Node delete(T elem, Node node)
    {
        if (node != null)
        {
            int cmp = elem.compareTo(node.value);
            if (cmp < 0)
            {
                node.left = delete(elem, node.left);
            }
            else if (cmp > 0)
            {
                node.right = delete(elem, node.right);
            }
            else if (node.left != null && node.right != null)
            {
                node.right = detachMin(node, node.right);
            }
            else
            {
                node = (node.left != null) ? node.left : node.right;
            }
        }
        return node;
    }

    private Node detachMin(Node del, Node node)
    {
        if(node.left != null)
        {
            node.left = detachMin(del, node.left);
        }
        else
        {
            del.value = node.value;
            node = node.right;
        }
        return node;
    }

    public String toStringPreOrder()
    {
        return printPreorder(root);
    }

    String printPreorder(Node node)
    {
        if (node == null)
        {
            return "";
        }
        else
        {
            str += node.value + ", ";

            printPreorder(node.left);

            printPreorder(node.right);
        }
        return str.substring(0, str.length() - 2);
    }

    public String toStringInOrder()
    {
        return printInorder(root);
    }

    public ArrayList<T> SortInOrder()
    {
        return helper(root);
    }

    ArrayList<T> helper(Node node)
    {
        if (node == null)
        {
            return null;
        }
        else
        {
            helper(node.left);

            result.add(node.value);

            helper(node.right);
        }
        return result;
    }

    String printInorder(Node node)
    {
        if (node == null)
        {
            return "";
        }
        else
        {
            printInorder(node.left);

            str += node.value + ", ";

            printInorder(node.right);
        }
        return str.substring(0, str.length() - 2);
    }

    public String toStringPostOrder()
    {
        return printPostorder(root);
    }

    String printPostorder(Node node)
    {
        if (node == null)
        {
            return "";
        }
        else
        {
            printPostorder(node.left);

            printPostorder(node.right);

            str += node.value + ", ";
        }
        return str.substring(0, str.length() - 2);
    }
}