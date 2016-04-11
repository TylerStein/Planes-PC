/*Generic Child-Sibling Search Tree
 * For procedural level generation at runtime
 * Created from scratch by Tyler Stein
 * April 3, 2016
 */

public class Node
{
    //Node's contained data
    private object data;

    //Next node down the tree
    private Node firstChild;

    //Next sibling in line
    private Node nextSibling;

    public Node() : this(null) { } //Empty constructor
    public Node(object data) : this(data, null, null) { } //Data-only constructor
    public Node(object data, Node child) : this(data, child, null) { } //Data and child constructor
    public Node(object data, Node child, Node sibling){ //Full-argument constructor
        this.data = data;
        this.firstChild = child;
        this.nextSibling = sibling;
    }

    public object Value{
        get{
            return data;
        }
        set{
            data = value;
        }
    }
    public Node FirstChild{
        get{
            return firstChild;
        }
        set{
            firstChild = value;
        }
    }
    public Node NextSibling
    {
        get
        {
            return nextSibling;
        }
        set
        {
            nextSibling = value;
        }
    }
}

public class TreeStructure
{
    private Node root;

    public TreeStructure() : this(null) { }
    public TreeStructure(object rootData) {
        this.root = new Node(rootData);
    }

    public virtual void Clear(){
        root = null;
    }
    public virtual bool IsEmpty()
    {
        return root == null;
    }

    public Node Root{
        get{
            return root;
        }
        set{
            root = value;
        }
    }
    
    //TODO: REMOVE, GET_PARENT

    //Inserts a new child or sibling of the child, depending on what's availible
    public void InsertAt(Node _parent, object data)
    {
        //Check for a child
        if (_parent.FirstChild != null)
        {
            //Has child
            InsertSibling(_parent.FirstChild, data);

        }else{
            //Parent node had no child, add one
            _parent.FirstChild = new Node(data);
        }
    }

    //Recursive method returns the node containing the given data OR the given root node (if nothing found)
    public Node FindByValue(Node _root, object data)
    {
        //Check if this node is the one being searched for
        if (_root.Value == data)
        {
            //This node's data equals the search data
            return _root;
        }

        //Check for sibling
        if (_root.NextSibling != null)
        {
            //Current node has a sibling, recursive check in its direction for data
            Node sibling = FindByValue(_root.NextSibling, data);

            //Check if result is data
            if (sibling.Value == data)
            {
                //Success, data found, return the found node!
                return sibling;
            }
        }

        //Check for child
        if (_root.FirstChild != null)
        {
            //Current node has a child, recursive check in its direction for data
            Node child = FindByValue(_root.FirstChild, data);

            //Check if result is data
            if (child.Value == data)
            {
                //Success, data found, return the found node!
                return child;
            }
        }
    

        //Node not found!
        return _root;
    }



    //Private helper function inserts a sibling at the end of the row
    private void InsertSibling(Node _firstSibling, object data)
    {
        //Create a temp node holding the next sibling in line
        Node tmp = _firstSibling.NextSibling;

        //Navigate to the last sibling
        while (tmp != null)
        {
            tmp = tmp.NextSibling;
        }

        //Attatch the new node to the end of the last sibling
        tmp.NextSibling = new Node(data);
    }
}
