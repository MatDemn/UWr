namespace PO_L4_Zad2
{
    public class Node<T> 
        // obiekty Node są pojedynczymi składnikami listy
    {
        private T value;
        private Node<T> next;
        private Node<T> prev;

        public Node() // konstruktor node, elementu listy
        {
            next = null;
            prev = null;
            value = default(T);
        }

        public T Value // właściwość Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Node<T> Next    // właściwość next
        {
            get { return this.next; }
            set { this.next = value; }
        }
        
        public Node<T> Prev    // właściwość prev
        {
            get { return this.prev; }
            set { this.prev = value; }
        }


    }
}